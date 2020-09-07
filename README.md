# TestCASE-NETLOF-CRUD-JWT-20200905

1)	Basic Login Authentication veya Token Doğrulaması ile giriş

2)	Tüm tablolar için CRUD fonksiyonlarının geliştirilmesi.

3)	Raporlama için gerekli gördüğünüz fonksiyonların geliştirilmesi. Raporlama ekranı için aşağıdaki rapor sayfası tasarımına bakabilirsiniz. 

Üstteki gereklilikler için yetiştirebildiğim kadarıyla eklemeler yaptım. 
-- Authentication, ASPNET Identity ve JWT Token entegrasyonlarını yaptım. Authorization için de rollerin tanımlanması yeterli.
-- Maintenance ve Vehicle tabloları için birkaç metot ekledim. Çok fazla tablo ve dolayısıyla çok metot olacağı için olabildiğince genelleştirilmiş bir yapı kurdum. 
    Bunun üstüne hızlı bir şekilde kontrollerlar ve servisler eklenebilir. Kendini tekrar ettiğini fark edince bıraktım.
-- Middleware yazmış olmak genelde firmalar için önemli oluyor, bu nedenle başlangıçta Jwt'yi middleware olarak yazmıştım ama sonra kaldırıp Core'un hizmetini kullandım.
-- Raporla ilgili servisi de yazmak isterdim ancak zamanım yetmedi. 
-- Birçok yerin de gözden geçirilmesi gerekiyor kodda, ancak güzel bir altyapısı oldu.
-- Olabildiğince kabiliyetleri eklemeye çalıştım, umarım yeterli olur. Bu proje kullanılarak 3-5 gün içinde güzel bir proje çıkmış olur.

4) Projelerde öncelikle happy-path'leri tamamlarım. Yani bağlantısı olmayan en ufak componentlere CRUD yapılmasından başlarım. İlk olarak araç tipi ve daha sonra araç servisini bu sebeple yazdım. 
    -- Birbirine bağlı olan tablolar için gereklilikler gönderilmediğinde doğru hatanın gösterilmesini denerim. 
        Örneğin; Araç, kullanıcı veya aksiyon tipleri doğru girilmeden bakım girilemediğinden emin olurum.
    -- Status tablosu önemli, farklı tablolarda farklı kişiler güncelleme var oluşturma yaptığında doğru güncellenip güncellenmediğini test ederim.
    -- Kullanıcının yalnızca kendi arıza bildirimlerini görüp görmediğini test ederim. (GetMaintenaceForUserID gibi bir servis yazmaya vaktim olmadı)
    -- Maintenance üzerinde yalnızca ResponsibleUser'ın değişiklik yapıp yapmadığını test ederim.
    -- Silinmiş datanın admin için olan fonksiyonlar haricinde get'lerden gelmediğinden emin olurum.
	-- PictureGroup için doğru görsel url'lerinin doğru maintenance için geldiğinden emin olurdum.
	-- Aynı anda CRUD yapıldığında consistency hataları olup olmadığını incelerim.
	-- Genel performans testlerini yapardım. Load test vs.
5) Test için backend tarafında kullandığım bir tool yok. Paneller için Selenium kullanıyoruz. Backend için şu anda hizmet verdiğim şirkette manual load test yazılıp deneniyor. 
	Yavaşlığın miktarı ve hızlı bir kontrol genelde sebepler hakkında fikir veriyor ama performans kaybının sebebini araştırmalara rağmen bulamadığımızı düşünerek aklıma gelenlerin tamamını yazayım:
	-- İlk olarak veritabanı inital size ve growth size'ını bilmiyorsam kontrol ederim. Kendini sürekli olarak büyütmeye çalışması performansı etkiliyor.
	-- LinQ sorgularında kullanılan ilişkilere ait doğru indexler olup olmadığını kontrol ederim.
	-- Bu uygulamada yok ancak sql query'ler olsaydı nolock'sız select atmadığımızdan ve gerekli olmayan kolonları çekmediğimizden emin olurdum. 
	-- Enjeksiyonların scope'larını doğru belirleyip belirlemediğimizi kontrol ederdim.
	-- Caching eklemeyi düşünürdüm. Caching varsa süre ve kapsamlarını gözden geçirirdim.
	-- Veritabanında birden çok instance kullanmayı hiç tercih etmesem de eğer varsa HighAvailability, log shipping ve benzeri ayarlarını gözden geçirirdim. 
	-- Web tarafındaki instance'ların session paylaşma konusunda problem yaşama ihtimalleri var. Bununla birlikte connection pool'ları doğru ayarlandığından emin olurdum. Connection'ların alındıktan sonra sağlıklı olarak bırakıldığından emin olmaya çalışırdım. (bu konuda genelde zorlanılıyor)
	-- SQL execution planları incelerdim. 
	-- Network katmanında NLB varsa onun performansını ayrıca inceletirdim. (kendim inceleyemem)
	-- Requestin geldiği noktalar ile sql'den data alınana kadarki network trace'inde firewall'ların süre kaybına sebep olup olmadığına bakardım.
	-- (Bir bilgi sayılmasa da) belli bir miktar araştırmadan sonra her zaman meslektaşlarımdan yardım alırım. 
