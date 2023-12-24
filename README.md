
# PROJE ADI

**RİSETECH TELEFON REHBERİM.**


# PROJE AÇIKLAMASI

**Proje iki mikroservisten oluşan telefon rehberi servisidir.**
1. Mikroservis kişi yönetimi ve iletişim bilgilerinin yönetildiği servistir.
2. Mikroservis raporların yönetildiği ve sunulduğu servistir.
3. Servislere HTTP üzerinden iletişim sağlanır.
4. Servisler arası iletişim MQTT ile sağlanmıştır.

   
# KURULUM

1. Projeyi bilgisayarınıza klonlayın.
    git clone https://github.com/egemencpcgl/risetech.git
2. Veritabanı backup dosyasını PostgreSQL içinde restore işlemini gerçekleştirin.

# AYARLAR

Klonlama işleminden sonra MQTTBroker projesini çalıştırın, ardından servisler çalıştırıldığında Broker'a otomatik bağlancak ve ilgili portlarda API'lar açılacaktır.
Endpointlere yapılacak isteklerle kullanılabilir. Broker çalışmadığında da servisler kullanılabilir ancak bir birileri ile haberleşme sağlanamaz..
