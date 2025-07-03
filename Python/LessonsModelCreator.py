from skl2onnx.common.data_types import FloatTensorType
from sklearn.multioutput import MultiOutputRegressor
from sklearn.preprocessing import StandardScaler
from sklearn.neural_network import MLPRegressor
from skl2onnx import convert_sklearn
import pandas as pd
import numpy as np

# Veriyi yükle
data = pd.read_excel("Datasets/Yeni_Sentetik_Ogrenci_Veriseti.xlsx")



bolumler = [1,2,3]



input_columns = ["Yas", "AnneMeslek", "BabaMeslek", "SevilenDers_1", "SevilenDers_2",                                  # Giriş değişkenleri
                   "OzelDers", "DersCalisma", "KitapOkuma", "Etkinlik",
                   "TDveEdebiyat", "Matematik", "Fizik", "Kimya", "Biyoloji",
                   "Tarih", "Cografya", "DinKveAhlakB", "BedenEgitimi", "Resim", "Muzik", "Ingilizce"]

output_columns = ["Ders_1", "Ders_2", "Ders_3", "Ders_4", "Ders_5", "Ders_6", "Ders_7", "Ders_8", "Ders_9", "Ders_10"]   # Cikti degerleri



for bolum in bolumler:                                                              # Her bölüm için model eğit

    if(bolum == 1):
        print(f"\n ===> Bilgisayar Mühendisligi Ders Notu Tahmin modeli oluşturuluyor...")
    elif(bolum == 2):
        print(f"\n ===> Hukuk Ders Notu Tahmin modeli oluşturuluyor...")
    elif(bolum == 3):
        print(f"\n ===> Gazetecilik Ders Notu Tahmin modeli oluşturuluyor...")


    df_bolum = data[data["Bolum"] == bolum].reset_index(drop=True)

    # Girişleri bölüm özelinde al
    X = df_bolum[input_columns]
    Y = df_bolum[output_columns]

    # X için scaler
    input_scaler = StandardScaler()
    X_scaled = input_scaler.fit_transform(X)

    # Y için scaler
    output_scaler = StandardScaler()
    Y_scaled = output_scaler.fit_transform(Y)

    base_model = MLPRegressor(activation= 'tanh', alpha = 1, early_stopping = True,           # Bu parametreler Asiri ogrenmeden kacinmak icin. Bu parametreler ayarlanmazsa -25.000 gibi cok kotu R2 degeri veriyor.
    hidden_layer_sizes=(64, 64), learning_rate = 'constant', learning_rate_init = 0.01, max_iter = 3000,      # hidden_layer_sizes = 99 da daha iyi sonuçlar veriyor -> 1.Bölüm: 0,81 / 2.Bölüm: 0,75 / 3.Bölüm: 0.87
    random_state = 42, solver = 'lbfgs', tol = 0.01, verbose = True)
    
    model = MultiOutputRegressor(base_model)
    model.fit(X_scaled, Y_scaled)

    initial_type = [('input', FloatTensorType([None, len(input_columns)]))]
    onnx_model = convert_sklearn(model, initial_types=initial_type)


    # Kayıt
    if(bolum == 1):
        model_path = "Models/LessonModels/BM_Lesson_Model.onnx" 
    elif(bolum == 2):
        model_path = "Models/LessonModels/H_Lesson_Model.onnx" 
    elif(bolum == 3):
        model_path = "Models/LessonModels/G_Lesson_Model.onnx" 


    print("----- INPUT SCALER -----")
    means = np.round(input_scaler.mean_, 4)
    formatted = ', '.join([f"{val:.4f}f" for val in means])
    print(f"\n{model_path} mean:\n{formatted}")

    scales = np.round(input_scaler.scale_,4)
    formatted = ', '.join([f"{val:.4f}f" for val in scales])
    print(f"\n{model_path} scale :\n{formatted}")


    print("----- OUTPUT SCALER -----")
    means = np.round(output_scaler.mean_, 4)
    formatted = ', '.join([f"{val:.4f}f" for val in means])
    print(f"\n{model_path} mean:\n{formatted}")

    scales = np.round(output_scaler.scale_,4)
    formatted = ', '.join([f"{val:.4f}f" for val in scales])
    print(f"\n{model_path} scale :\n{formatted}")

    with open(model_path, "wb") as f:                                                                        # Kaydet
        f.write(onnx_model.SerializeToString())

    print(f"\n{model_path} başarıyla oluşturuldu!")
    print("\n--------------------------------------------------------------------------------------------------------------------------------------")

