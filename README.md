# 📁 FTP Dosya Yükleyici (C# WinForms)

Bu proje, C# WinForms kullanılarak geliştirilen bir **FTP dosya yükleyici** uygulamasıdır.  
Uygulama, kullanıcıların yerel bilgisayarlarından seçtikleri dosyaları bir FTP sunucusuna kolayca yüklemelerini sağlar.  
Ayrıca gerçek zamanlı **yükleme ilerlemesi**, **kalan süre tahmini** ve **ayarlanabilir sunucu bilgileri** gibi gelişmiş özellikler içerir.

## 🚀 Özellikler

- ✅ **Sürükle ve bırak destekli dosya yükleme**
- 📦 **Büyük dosyalar için parça parça aktarım** (buffered upload)
- ⏱️ **Gerçek zamanlı kalan süre tahmini**
- 📊 **ProgressBar ile yükleme ilerlemesini görselleştirme**
- 🔒 **FTP bağlantı bilgilerini `ftpconfig.txt` dosyasından okuma**
- 🧠 **300 MB sonrası hız testi ile ortalama hız ve süre tahmini**
- 📝 **Günlük (log) alanı ile bilgilendirme**
- 📛 **Aynı isimde dosya kontrolü**

## ⚙️ Kullanım

1. **Proje derlenip çalıştırıldığında**, kullanıcıdan bir dosya seçmesi istenir.
2. Dosya seçildikten sonra `UploadFileToFTP` fonksiyonu devreye girer.
3. `ftpconfig.txt` dosyasından FTP bilgileri okunur.
4. Eğer FTP’de aynı isimde bir dosya varsa uyarı verilir.
5. Dosya yüklenmeye başladığında:
   - İlerleme çubuğu güncellenir
   - Kalan süre tahmini hesaplanır ve görüntülenir
6. Yükleme tamamlandığında log ekranına başarı mesajı yazılır.

## 🗂 ftpconfig.txt Dosyası Formatı

Aşağıdaki gibi yapılandırılmalıdır:

host: ftp://ftp.ornekadres.com
kullanıcı: ftp_kullanici_adi
şifre: base64_ile_kodlanmis_sifre


💡 Sistem Gereksinimleri
Windows 10 veya üzeri

.NET Framework 4.7.2 veya üzeri

İnternet bağlantısı

Geçerli bir FTP hesabı

🧪 Ekran Görüntüsü

![aaaaaa](https://github.com/user-attachments/assets/76b48249-29dc-459d-abed-1755a0db7286)

📌 Notlar
Uygulama tek bir dosyanın yüklenmesi için optimize edilmiştir.

Şu anda yükleme sırasındaki duraklatma/devam etme özelliği yoktur.

Şifre çözümleme başarısız olursa uygulama uyarı verir ve işlem iptal edilir.

