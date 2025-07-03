import pandas as pd
from sklearn.preprocessing import StandardScaler
from sklearn.neural_network import MLPRegressor
from skl2onnx import convert_sklearn
from skl2onnx.common.data_types import FloatTensorType
import os
import numpy as np

# Veriyi yükle
data = pd.read_excel("Datasets/Yeni_Sentetik_Ogrenci_Veriseti.xlsx")

# Kullanılmayacak sütunları çıkar
data = data.drop(columns=["Ders_1", "Ders_2", "Ders_3", "Ders_4", "Ders_5", "Ders_6", "Ders_7", "Ders_8", "Ders_9", "Ders_10"])

bolumler = [1,2,3]

for bolum in bolumler:
    if(bolum == 1):
        print(f"\n ===> Bilgisayar Mühendisligi Gano Tahmin modeli oluşturuluyor...")
    elif(bolum == 2):
        print(f"\n ===> Hukuk Gano Tahmin modeli oluşturuluyor...")
    elif(bolum == 3):
        print(f"\n ===> Gazetecilik Gano Tahmin modeli oluşturuluyor...")

    # Bölüm verisi
    df_bolum = data[data["Bolum"] == bolum].reset_index(drop=True)

    X = df_bolum.drop(columns=["GANO", "Bolum"])
    y = df_bolum["GANO"]

    # Normalizasyon
    scaler = StandardScaler()
    X_scaled = scaler.fit_transform(X)

    # Model tanımı ve eğitimi
    model = MLPRegressor(activation= 'tanh', alpha = 1, early_stopping = True,               # Bu parametreler Asiri ogrenmeden kacinmak icin. Bu parametreler ayarlanmazsa -25.000 gibi cok kotu R2 degeri veriyor.
    hidden_layer_sizes = 99, learning_rate = 'constant', learning_rate_init = 0.01, max_iter = 3000,      # hidden_layer_sizes = 99 da daha iyi sonuçlar veriyor -> 1.Bölüm: 0,81 / 2.Bölüm: 0,75 / 3.Bölüm: 0.87
    random_state = 42, solver = 'lbfgs', tol = 0.01, verbose = True)

    model.fit(X_scaled, y)

    # ONNX dönüşümü
    initial_type = [('input', FloatTensorType([None, X_scaled.shape[1]]))]
    onnx_model = convert_sklearn(model, initial_types=initial_type)
    

    # Kayıt
    if(bolum == 1):
        model_path = "Models/GanoModels/BM_Gano_Model.onnx" 
    elif(bolum == 2):
        model_path = "Models/GanoModels/H_Gano_Model.onnx" 
    elif(bolum == 3):
        model_path = "Models/GanoModels/G_Gano_Model.onnx" 


    means = np.round(scaler.mean_, 4)
    formatted = ', '.join([f"{val:.4f}f" for val in means])
    print(f"\n{model_path} mean:\n{formatted}")

    scales = np.round(scaler.scale_,4)
    formatted = ', '.join([f"{val:.4f}f" for val in scales])
    print(f"\n{model_path} scale :\n{formatted}")
  

    with open(model_path, "wb") as f:
        f.write(onnx_model.SerializeToString())

    print(f"{model_path} başarıyla oluşturuldu!")
