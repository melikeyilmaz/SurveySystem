# SurveySystem
Kullanım Senaryoları
Üye Olma
Site açıldığında yer alan Üye Ol linki üzerinden açılan sayfada ad, soyad, mail adresi, şifre ve şifre tekrar
alanları bulunur. Bu alanların hepsi zorunludur. Şifre en az 8 karakter olmalı ve büyük harf, küçük harf ve
rakam içermelidir. Kullanıcı bilgileri girdikten sonra üyeliği tamamla diyerek kaydını gerçekleştirir.

Üye Girişi
Anasayfada yer alan Giriş Yap linki üzerinden yönlenilen ekrandan sisteme giriş yapılır. Üyeler kayıt
esnasında verdikleri mail ve şifre bilgileriyle giriş sağlarlar. Admin kullanıcısı ise sistem ayağa kaldırılırken
oluşturulan mail ve şifre bilgileriyle sisteme giriş sağlar.
Her mail adresine sadece bir kullanıcı hesabı tanımlıdır.

Anket Oluşturma
Anasayfada yer alan anket oluşturma linki üzerinden yönlenilen ekranda anket oluşturabilir.
Üye olmayan kullanıcılar 4 soruluk bir anket oluşturabilir ve bu anketi de sistemde tanımlı 10 soru
üzerinden seçim yaparak oluşturabilir. Oluşturduğu anketi en fazla 5 kişiye gönderebilir. Eğer daha fazla
kişi ankete erişmek isterse “Anketin cevaplanma kontenjanı dolmuştur” uyarısının aldığı bir sayfayla
karşılaşır.
Üye olan kullanıcılar 10 soruluk bir anket oluşturabilir. Bu anketi oluştururken sistemde tanımlı tüm
sorular üzerinden seçim yaparak oluşturabilir. Oluşturduğu anketi dilediği kadar kişiye gönderebilir.
Üye olmayan kullanıcılar anket oluşturmaya başladıklarında ilk olarak isimlerini girerler. İsim girmeyen
kullanıcılar anket oluşturmaya devam edemezler. Üye olan kullanıcılar içinse üyelik formunda verdikleri
ad soyad bilgisi kullanılır.
Kullanıcılar anket sorularını sırayla seçerler. Kullanıcılar anket sorularını seçerken o soruya tanımlı
seçeneklerden birini doğru cevap olarak belirler. Anketi başarıyla oluşturduktan sonra arkadaşlarıyla
paylaşabilmesi için anket erişim linki kullanıcıya gösterilir.

Anket Cevaplama
Bir anketin linkine sahip herkes o anketi görüntüleyebilir. Anketi Başlat butonuna tıklandığında öncelikle
cevaplayan kişiden isim bilgisi istenir. İsim bilgisi girmeyenler anketi cevaplayamazlar. Ardından sırayla
sorular ve seçenekleri gösterilir. Doğru olduğunu düşündüğü seçeneği işaretler. Doğruysa ilgili seçenek
yeşil bir çizgi ile çerçevelenir. Eğer yanlışsa kırmızı bir çizgi ile çerçevelenir ve doğru olan seçenek yeşil bir
çizgi ile çerçevelenir.
Seçeneği işaretleyip yeşil ve kırmızı çizgiler göründükten sonra sıradaki soru kişinin önüne gelir. Anketi
cevaplayan kişi sorular arasında ileri geri gidemez. Son soruyu da cevapladıktan sonra kaç doğru cevap
verdiği gösterilir.
Anketi cevaplamak için üye olma zorunluluğu yoktur.

Soru Havuzu
Sistemde tanımlı sorular bulunur. Admin kullanıcısı giriş yaptıktan sonra yüklenen sayfada kendisine
sunulan menüler aracılığıyla sistemde tanımlı soruları görüntüleyebilir. Kaldırmak istediği soruları
sistemden kaldırabilir. Yeni sorular ekleyebilir.
Soru eklerken soru metnini ve o soru için geçerli olan 5 seçeneğin ifadelerini girer. Kaydet butonuna
tıklayarak soruyu sisteme ekler.
Üye olan kullanıcılar sisteme giriş yaptıktan sonra kendilerine sunulan menü üzerinden soru oluşturabilir.
Aynı şekilde soru metnini ve 5 seçeneğin ifadelerini girer. Kaydet butonuna tıklandığında ilgili soru
onaylanmak üzere admin kullanıcısına iletilir. Üye olan kullanıcı bu sorunun onaylanıp onaylanmadığını
kendisine sunulan ekran üzerinden takip edebilir.
Admin kullanıcısı, menüsü üzerinden erişebildiği Kullanıcı Soruları ekranından kullanıcıların eklediği
soruları görüntüleyebilir. İsterse soruya onay vererek havuza eklenmesini sağlar. Eğer reddederse ilgili
soru bu ekranda bir daha görüntülenmez.

Skor Tablosu
Kullanıcılar oluşturdukları anketlere arkadaşlarının verdiği cevapları Skor Tablosu ekranında
görüntüleyebilir.
Üye olan kullanıcılar giriş yaptıktan sonra menülerinden Skor Tablosu sayfasını açarak oluşturdukları tüm
anketleri listeleyebilirler. Listeden seçip tıkladıkları ankete cevap veren arkadaşlarının isim ve doğru
cevap sayılarını bir tablo halinde görüntüleyebilirler.
Üye olmayan kullanıcılar ise anketi oluşturdukları tarayıcı üzerinden anket linkini girerek Skor Tablosunu
görüntüleyebilirler.
