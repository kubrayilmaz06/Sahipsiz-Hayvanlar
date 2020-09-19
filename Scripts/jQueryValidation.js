//Boş Geçilemez Alanların Uyarıları
$(document).ready(function () {
    $("#HalkFormu").validate(
        {
            rules:
            {
               ProtokolNo: { required: true },
            
            },
            messages:
            {
                ProtokolNo: { required: 'Protokol No Zorunlu Alandır!' },
            }
        });


    $("#PersonelFormu").validate(
        {
            rules:
            {
                PersonelAdi: { required: true },
                PersonelSoyadi: { required: true },
                PersonelTel: { required: true },
                KulAdi: { required: true },
                Sifre: { required: true },
                Yetki: { required: true },
            },
            messages:
            {
                PersonelAdi: { required: 'Personel Adı Zorunlu Alandır!' },
                PersonelSoyadi: { required: 'Personel Soyadı Zorunlu Alandır!' },
                PersonelTel: { required: 'Telefon No Zorunlu Alandır!' },
                KulAdi: { required: 'Kullanıcı Adı Zorunlu Alandır!' },
                Sifre: { required: 'Şifre Zorunlu Alandır!' },
                Yetki: { required: 'Yetki Zorunlu Alandır!' },
            }
        });





    $("#IhbarFormu").validate(
        {
            rules:
            {
                'ihbar.ProtokolNo': { required: true },
                "ihbar.AdSoyad": { required: true },
                'ihbar.IhbarTarih': { required: true },
                'ihbar.IhbarIletisim': { required: true },
                'ihbar.Esgal': { required: true },
                'ihbar.Teshis': { required: true }

            },
            messages:
            {
                'ihbar.ProtokolNo': { required: 'Protokol No Zorunlu Alandır!' },
                'ihbar.AdSoyad': { required: 'Personel Adı Soyadı Zorunlu Alandır!' },
                'ihbar.IhbarIletisim': { required: 'İletişim Zorunlu Alandır!' },
                'ihbar.Esgal': { required: 'Eşgal Zorunlu Alandır!' },
                'ihbar.Teshis': { required: 'Teşhis Zorunlu Alandır!' }

            }
            //submitHandler: function () {
            //    alert("alert");
            //}
        });


    $("#DurumFormu").validate(
        {
            rules:
            {
                'durum.DurumTarih': { required: true },
                'durum.Aciklama': { required: true },
            },
            messages:
            {
                'durum.DurumTarih': { required: 'Tarih Zorunlu Alandır!' },
                'durum.Aciklama': { required: 'Durum Zorunlu Alandır!' },
            }
        });

    $("#DurumEdit").validate(
        {
            rules:
            {
                DurumTarih: { required: true },
                Acıklama: { required: true },
                deneme: { required: true },
            },
            messages:
            {
                DurumTarih: { required: 'Tarih Zorunlu Alandır!' },
                Acıklama: { required: 'Durum Zorunlu Alandır!' },
                deneme: { required: 'zorunlu alan' }
            }
        });

    $("#IhbarCreate").validate(
        {
            rules:
            {
                ProtokolNo: { required: true },
                AdSoyad: { required: true },
                IhbarTarih: { required: true },
                IhbarIletisim: { required: true },
                Esgal: { required: true },
                Teshis: { required: true },
            },
            messages:
            {
                ProtokolNo: { required: 'Protokol No Zorunlu Alandır!' },
                AdSoyad: { required: 'Personel Adı Soyadı Zorunlu Alandır!' },
                IhbarIletisim: { required: 'İletişim Zorunlu Alandır!' },
                Esgal: { required: 'Eşgal Zorunlu Alandır!' },
                Teshis: { required: 'Teşhis Zorunlu Alandır!' },
            }
        });
});
