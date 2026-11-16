# Test Sonuçları

Visual Studio üzerinde aynı bilgisayarda iki ayrı pencere açılarak yapılan localhost testleri:

- **Derleme:** Başarılı (0 Hata, 0 Uyarı)
- **Sunucu Başlatma:** 1. pencerede 'Ali' kullanıcı adıyla sunucu 5000 portunda başarıyla başlatıldı.
- **İstemci Bağlantısı:** 2. pencerede 'Veli' kullanıcı adıyla localhost üzerinden sunucuya bağlanıldı.
- **Karşılıklı Mesajlaşma:**
  - 'Veli' tarafından gönderilen "selam" mesajı her iki ekranda görüntülendi.
  - 'Ali' tarafından gönderilen "aleyküm selam" mesajı her iki ekranda görüntülendi.
- **Arayüz ve Durum:** Bağlantı kurulduğunda durum yazıları ("Sunucuya bağlandı!", "İstemci bağlandı!") güncellendi ve Gönder butonları aktifleşti.
