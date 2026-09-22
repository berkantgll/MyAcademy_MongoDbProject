<div align="center">

<img width="1672" height="941" alt="ChatGPT Image 22 Eyl 2026 13_35_21" src="https://github.com/user-attachments/assets/9a846ab0-c083-4497-a391-69abde7437d1" />

<br><br>

# 🌍 Travelio

### ASP.NET Core MVC & MongoDB Travel and Reservation Platform

Travelio; tur keşfi, rezervasyon yönetimi, kullanıcı etkileşimi ve yönetim süreçlerini tek bir platformda bir araya getiren kapsamlı bir seyahat uygulamasıdır.

<br>

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![MongoDB](https://img.shields.io/badge/MongoDB-47A248?style=for-the-badge&logo=mongodb&logoColor=white)
![AutoMapper](https://img.shields.io/badge/AutoMapper-DD0031?style=for-the-badge)
![FluentValidation](https://img.shields.io/badge/FluentValidation-2C3E50?style=for-the-badge)

<br>

![Chart.js](https://img.shields.io/badge/Chart.js-FF6384?style=flat-square&logo=chartdotjs&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=flat-square&logo=bootstrap&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=flat-square&logo=javascript&logoColor=black)
![ClosedXML](https://img.shields.io/badge/ClosedXML-Excel-217346?style=flat-square)
![QuestPDF](https://img.shields.io/badge/QuestPDF-PDF-B30B00?style=flat-square)

</div>

---

## ✨ Proje Hakkında

**Travelio**, ASP.NET Core MVC ve MongoDB kullanılarak geliştirilen kapsamlı bir tur ve rezervasyon yönetim uygulamasıdır.

Kullanıcılar platform üzerinden turları ve destinasyonları inceleyebilir, gelişmiş filtreleme seçeneklerini kullanabilir, uygun tur tarihleri üzerinden rezervasyon oluşturabilir, favori turlarını kaydedebilir, yorum ve puanlama yapabilir ve turlar hakkında soru sorabilir.

Yönetim tarafında ise turlar, rezervasyonlar, kullanıcı yorumları ve sorular admin paneli üzerinden yönetilebilir. Dashboard üzerinde MongoDB Aggregation ile oluşturulan veriler Chart.js kullanılarak görselleştirilmektedir.

---

## 🏠 Ana Sayfa

Travelio'nun kullanıcı tarafında; öne çıkan turlar, destinasyon arama, tarih ve kişi sayısı seçimi ile tur arama işlemleri tek ekran üzerinden gerçekleştirilebilmektedir.

<img width="1873" height="919" alt="3" src="https://github.com/user-attachments/assets/3b40f742-1c49-4ca0-bf56-1bc2cca52148" />

---

## 🌍 Çoklu Dil Desteği

Travelio, Türkçe ve İngilizce olmak üzere iki farklı dil desteğine sahiptir.

Sabit arayüz metinlerinin yanında tur adı ve açıklaması gibi dinamik içerikler de seçilen dile göre görüntülenmektedir.

<table>
<tr>
<td width="50%">

### 🇹🇷 Türkçe

<img width="1873" height="919" alt="3" src="https://github.com/user-attachments/assets/ccb3a51e-6c34-4e47-a357-c10ec4d8ec2c" />


</td>

<td width="50%">

### 🇬🇧 English

<img width="1874" height="920" alt="4" src="https://github.com/user-attachments/assets/86a5d05a-e4a8-4d9b-9d01-f3cbaef34a25" />


</td>
</tr>
</table>

---

## 🗺️ Tur Listeleme ve Filtreleme

Kullanıcılar mevcut turları tek bir ekran üzerinden inceleyebilir ve ihtiyaçlarına göre filtreleyebilir.

Filtreleme seçenekleri arasında;

- Destinasyon
- Kategori
- Tur tarihi
- Kişi sayısı
- Minimum / maksimum fiyat
- Sıralama

gibi seçenekler bulunmaktadır.

<img width="1870" height="919" alt="5" src="https://github.com/user-attachments/assets/3f9af5d2-11a2-4f7b-a9e6-142ee60c2597" />

---

## 🎟️ Tur Detayı ve Rezervasyon Sistemi

Kullanıcılar seçtikleri turun detay sayfasında tur açıklamasını, galeri görsellerini, tur süresini, destinasyon bilgisini, fiyatı, puanlamayı ve tur programını görüntüleyebilir.

Tur programı gün gün accordion yapısında gösterilmektedir. Rezervasyon alanında ise kullanıcı tur tarihini, yetişkin ve çocuk sayısını seçerek toplam tutarı görüntüleyebilir ve rezervasyon oluşturabilir.

Rezervasyon sürecinde;

- Seçilen tur tarihi kontrol edilir
- Kalan kontenjan doğrulanır
- Yetişkin ve çocuk sayısına göre toplam ücret hesaplanır
- Rezervasyon backend tarafında oluşturulur
- Tur kontenjanı otomatik olarak azaltılır
- Rezervasyon iptal edildiğinde kontenjan geri yüklenir

Bu yapı sayesinde fiyat ve kapasite işlemleri kullanıcı tarafına bırakılmadan uygulamanın backend tarafında güvenli şekilde yönetilmektedir.

### 🗺️ Tur Detay Sayfası

<img width="1919" height="1079" alt="6" src="https://github.com/user-attachments/assets/b646a3b0-698f-4c68-b7b1-c0cea7dca370" />

<br>

### 📅 Tur Programı ve Rezervasyon

<img width="1919" height="1079" alt="7" src="https://github.com/user-attachments/assets/d32f34ec-2ce7-497b-bd97-fd06c418d43e" />

<br>

### 💬 Yorum, Puanlama ve Soru / Cevap

Kullanıcılar tur deneyimlerini puanlayıp yorumlayabilir ve tur hakkında merak ettikleri soruları sistem üzerinden gönderebilir.

Onaylanan yorumlar ve admin tarafından cevaplanan sorular tur detay sayfasında diğer kullanıcılar tarafından görüntülenebilir.

<img width="1919" height="1079" alt="8" src="https://github.com/user-attachments/assets/149a8985-40bb-4c85-a202-bfc148483ffe" />


---

## 👤 Kullanıcı Profil Alanı

Travelio, kullanıcıların tüm seyahat işlemlerini tek bir yerden yönetebileceği kişisel bir profil alanına sahiptir.

Kullanıcılar profil ekranında yaklaşan ve tamamlanan seyahatlerini görüntüleyebilir, favori turlarını inceleyebilir, yorumlarını ve tur hakkında sordukları soruları takip edebilir.

### 📊 Profil ve Genel Bakış

Profilin ana ekranında yaklaşan ve tamamlanan tur sayıları, favoriler, yorumlar, yaklaşan seyahat bilgileri ve son aktiviteler gösterilmektedir.

<img width="1919" height="1079" alt="12 1" src="https://github.com/user-attachments/assets/0b2196e6-1275-45f2-a898-e56c8e645052" />

### ❤️ Favori Turlarım

Kullanıcılar ilgilendikleri turları favorilerine ekleyebilir ve daha sonra profil alanından bu turlara kolayca ulaşabilir.

<img width="1919" height="1079" alt="12 2" src="https://github.com/user-attachments/assets/d7f4f50e-7cd4-49bc-9846-3103f3c37446" />


### 💬 Sorularım

Kullanıcıların turlar hakkında gönderdiği sorular ve yönetici tarafından verilen cevaplar, kişisel profil alanında görüntülenebilir.

<img width="1919" height="1079" alt="12 3" src="https://github.com/user-attachments/assets/f4e5e755-25c9-4df1-9e54-8dbd1414a49d" />


---

## 📊 Admin Paneli ve Dashboard

Travelio'nun yönetim paneli üzerinden turlar, rezervasyonlar, kullanıcılar, yorumlar ve sorular yönetilebilmektedir.

Dashboard ekranında MongoDB Aggregation kullanılarak elde edilen veriler, Chart.js ile görselleştirilmektedir.

### 📈 Dashboard ve İstatistikler

Dashboard üzerinden aşağıdaki bilgiler takip edilebilmektedir:

- Toplam tur, rezervasyon ve kullanıcı sayıları
- Aktif ve pasif turlar
- Bekleyen sorular
- En çok rezervasyon alan turlar
- Son 6 aylık rezervasyon istatistikleri
- Son rezervasyonlar

<img width="1919" height="1079" alt="13" src="https://github.com/user-attachments/assets/86a07e93-82dd-4713-8779-4004536ba79c" />


<br>

### 🗺️ Tur Yönetimi

Yöneticiler turları oluşturabilir, güncelleyebilir, silebilir ve aktif/pasif durumlarını değiştirebilir. Tur adı, destinasyon, kategori ve durum kriterlerine göre arama ve filtreleme yapılabilmektedir.

<img width="1919" height="1079" alt="14" src="https://github.com/user-attachments/assets/a369b7d7-9ecc-49f8-8d00-530eae425bfe" />


<br>

### 🎟️ Rezervasyon Yönetimi

Rezervasyonlar; tur, kullanıcı, tarih ve durum kriterlerine göre filtrelenebilir. Yöneticiler rezervasyonları onaylayabilir veya iptal edebilir.

<img width="1919" height="1077" alt="15" src="https://github.com/user-attachments/assets/b755a0e7-72d5-48dd-a767-743c2f94dae9" />


---

## 💬 Yorum ve Soru Yönetimi

Admin paneli üzerinden kullanıcı yorumları ve turlar hakkında gönderilen sorular yönetilebilmektedir.

### ⭐ Yorum Yönetimi

Kullanıcıların yaptığı yorumlar, yönetici onayından geçtikten sonra ilgili turun detay sayfasında yayınlanmaktadır.

Yöneticiler yorumları inceleyebilir, onaylayabilir ve silebilir.

<img width="1919" height="1079" alt="16" src="https://github.com/user-attachments/assets/646573bd-5207-4d48-a0d8-37abad2b1f54" />

<br>

### ❓ Soru ve Cevap Yönetimi

Kullanıcıların turlar hakkında gönderdiği sorular yönetim panelinde listelenmektedir.

Yöneticiler bu soruları cevaplayabilir veya silebilir. Cevaplanan sorular ilgili tur sayfasında görüntülenmektedir.

<img width="1919" height="1079" alt="18" src="https://github.com/user-attachments/assets/8805f495-1440-4573-97f1-b9387a8d07e4" />


---

## 📄 Excel ve PDF Raporlama

Travelio'da rezervasyon ve katılımcı bilgileri Excel ve PDF formatlarında dışa aktarılabilmektedir.

- **ClosedXML:** Excel dosyalarının oluşturulması
- **QuestPDF:** PDF raporlarının oluşturulması
- 
Raporlarda katılımcı bilgileri, tur adı, seyahat tarihi, kişi sayısı, toplam ücret ve rezervasyon durumu gibi bilgiler yer almaktadır.

<img width="1917" height="955" alt="Ekran görüntüsü 2026-09-22 135315" src="https://github.com/user-attachments/assets/8352f0d1-73c5-42c4-82dd-bbc919bc0ec9" />
<img width="1869" height="919" alt="Ekran görüntüsü 2026-09-22 135241" src="https://github.com/user-attachments/assets/7ef4e5cd-3a20-47ac-8c27-c81bdb5403cd" />


