
# PROJE ADI

**RİSETECH TELEFON REHBERİM.**


# PROJE AÇIKLAMASI

**Proje iki mikroservisten oluşan telefon rehberi servisidir.**
1. PersoneManagementService kişi yönetimi ve iletişim bilgilerinin yönetildiği servistir.
2. ReportManagementService raporların yönetildiği ve sunulduğu servistir.
3. Servislere HTTP üzerinden iletişim sağlanır.
4. Servisler arası iletişim MQTT ile sağlanmıştır.

   
# KURULUM

1. Projeyi bilgisayarınıza klonlayın.
   
       git clone https://github.com/egemencpcgl/risetech.git
   
2. Docker compose ile tüm servisleri çalıştırın.

        docker-compose up

3. Web servisler 5011 ve 5012 numaralı portları kullanmakta ve swagger mevcut.
 - http://localhost:5011/swagger/index.html
 - http://localhost:5012/swagger/index.html
adreslerine giderek endpointlere erişebilirsiniz.


