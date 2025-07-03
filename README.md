Proje Hakkında
Bu proje, Python ve Unity teknolojilerinin gücünü birleştirerek oluşturulmuştur.
Amaç; Unity ile geliştirilen bir uygulama aracılığıyla kullanıcıdan alınan verileri, Python ile eğitilmiş sinir ağı modellerine gönderip, elde edilen çıktıyı kullanıcıya sunmaktır.

🚀 Unity (Uygulama Arayüzü)
Unity tarafı, kullanıcı etkileşimini yöneten arayüzü sağlar.
Kullanıcıdan veri girişi alınır ve bu veriler Python tarafından oluşturulan makine öğrenmesi modellerine gönderilir.
Modelin verdiği sonuç, Unity arayüzü üzerinden kullanıcıya sunulur.

🧠 Python (Model Eğitimi ve Tahmin)
Projede üç temel Python betiği yer almaktadır:

📘 GradeAndSectionPrediction.ipynb
Bu Jupyter defteri, modelleme öncesi veri hazırlık ve analiz sürecini kapsamaktadır:

Veri Ön İşleme (Preprocessing)

Keşifsel Veri Analizi (EDA)

Regresyon Modelleri ve R² Karşılaştırması

(Yalnızca sunum amacıyla:) İkili Sınıflandırma

F1 Score, Precision, Recall, Accuracy gibi metriklerin karşılaştırması

Confusion Matrix ve ROC Eğrisi görselleştirmeleri

Açıklanabilir Yapay Zeka (XAI) - SHAP

Not: Bu dosyada yapılan sınıflandırma çalışmaları, sadece derste sunum yapmak amacıyla eklenmiştir. Projenin ana odak noktası regresyon modelleridir.

📘 GanoModelCreator.py
Bu script, veri setindeki her bir bölüm (toplam 3 bölüm) için GANO (Genel Ağırlıklı Not Ortalaması) tahminleyen regresyon modelleri oluşturur.
Not: Her ne kadar bölüm sayısı sınırlı olsa da, temel modelleme süreci açısından yeterlidir.

📘 LessonsModelCreator.py
Bu dosyada, kullanıcının seçtiği bölüme ait derslerin puanlarını tahmin eden regresyon modelleri oluşturulmuştur.
Not: Her bölüm için 10 farklı ders bulunmaktadır (örnekleme amaçlı sınırlı tutulmuştur).

