using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace restorant_projesi_proje1_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region bilgiler
            //değer ve referans tiplerini de araştır.(araştırma ödevi olacak her hangi bir ödev teslimi yok)
            //diziler dahil öncesine kadar olan kısımlar olacak. sonrasını eklemek yasak.
            //hangi tuşun hangi koda denk geleceğini incele.
            //diziler kullanılabilir.
            //masanın günlük kazancı ve restorantın günlük kazancını da ekle.adisyonun içindeki ürünlere gerek yok.
            //masa aç ve masa işlem farklı şeyler. 
            //masa aç kısmında masa eklenip çıkarılabilir.
            //kasa işlemde günün getirisi vs. olucak.
            #endregion

            int menuSecim, menuSecimAdet, masaId, i, gunlukCiro = 0, acikMasaSayisi = 0, doluMasaSayisi = 0;

            bool menuSecimDevam = true;

            string[] etYemekleri = { "Köfte", "Pirzola", "İskender" };
            string[] pizzalar = { "Margarita", "Karışık", "Vejeteryan" };
            string[] sicakIcecekler = { "Çay", "Latte", "Türk Kahvesi" };
            string[] sogukIcecekler = { "Kola", "İce Tea", "Ayran" }; //menüdeki elemanları tek dizide de toplayabilirdim ancak kafa karıştırmaması için bu şekilde yaptım.
            string[] masalar = { "1.Masa", "2.Masa", "3.Masa", "4.Masa", "5.Masa", "6.Masa", "7.Masa" };
            string[] masaAcma = { "[kapalı]", "[kapalı]", "[kapalı]", "[kapalı]", "[kapalı]", "[kapalı]", "[kapalı]" };
            string[] masaDurumu = { "[boş]", "[boş]", "[boş]", "[boş]", "[boş]", "[boş]", "[boş]" };

            int[] etYemekleriFiyat = { 80, 120, 100 };
            int[] pizzalarFiyat = { 70, 75, 60 };
            int[] sicakIceceklerFiyat = { 10, 25, 20 };
            int[] sogukIceceklerFiyat = { 20, 20, 15 };
            int[] masaHesap = { 0, 0, 0, 0, 0, 0, 0 }; //her masanın güncel hesabı için
            int[] masaGunlukCiro = { 0, 0, 0, 0, 0, 0, 0 }; //masanın gün içindeki toplam cirosu için

        anaMenu:
            Console.Write("              ANA MENÜ\n--------------------------------------\nMasa Aç          [1]\nMasa İşlem       [2]\nMasa Hesap       [3]\nKasa İşlemleri   [4]\n--------------------------------------\nÇIKIŞ YAP        [0]\n--------------------------------------\nSeçiminiz:");

        tekrarSecim:
            try
            {

                int secim = int.Parse(Console.ReadLine());

                switch (secim)
                {
                    case 1:

                        Console.Clear();
                        masaAcBasi:
                        Console.WriteLine("MASA AÇ\n---------------------------------------------------------------");

                        for (i = 0; i < masalar.Length; i++)
                        {
                            Console.WriteLine($"{masalar[i]}       {masaAcma[i]}");
                        }
                        Console.WriteLine("---------------------------------------------------------------");

                        Console.WriteLine("ANA MENÜ için [ESC]/Masa ekleme işlemine başlamak için herhangi bir tuşa basınız\n---------------------------------------------------------------");

                        var anaMenuTusu = Console.ReadKey(true).Key;
                        if (anaMenuTusu == ConsoleKey.Escape) //esc girilip girilmediğini kontrol edecek.
                        {
                            Console.Clear();
                            goto anaMenu;
                        }

                        if (acikMasaSayisi == 7) //her masa açılmışsa daha fazla masa açılamaz
                        {
                            Console.WriteLine("Zaten her masayı açtınız.Daha fazla masa açamazsınız.\n[ENTER]'a basınız.");
                            Console.ReadLine();
                            Console.Clear();
                            goto anaMenu;
                        }

                        Console.Write("Aktif hale getirmek istediğiniz masanın ID numarasını giriniz:");
                        tekrarMasaAcSecim:

                        try
                        {
                            while (true) //sonsuz döngü olarak atadım,gotolar ile sonsuzdan çıkılacak şekilde
                            {
                                masaId = int.Parse(Console.ReadLine());

                                if (masaId < 1 || masaId > masalar.Length)
                                    throw new IndexOutOfRangeException();

                                if (masaAcma[masaId - 1] == "[açık]")
                                {
                                    Console.WriteLine($"{masalar[masaId - 1]} zaten açık.");
                                    Console.Write($"Lütfen seçiminizi tekrar yapın:");
                                    goto tekrarMasaAcSecim;
                                }
                                else if (masaAcma[masaId - 1] == "[kapalı]")
                                {
                                    Console.WriteLine($"{masalar[masaId - 1]} açılıyor.\n");
                                    masaAcma[masaId - 1] = "[açık]"; //masanın kapalıdan açığa alınması için
                                    acikMasaSayisi++;

                                    if (acikMasaSayisi == 7)
                                    {
                                        Console.WriteLine("Zaten her masayı açtınız.Daha fazla masa açamazsınız.\n[ENTER]'a basınız.");
                                        Console.ReadLine();
                                        Console.Clear();
                                        goto masaAcBasi;
                                    }
                                }
                                Console.Write("Masa açmaya devam etmek ister misiniz?(Evet için [1]/Hayır için [ESC] tuşuna basınız)");
                                tekrarMasaAcTamamDevam:
                                var masaAcTamamDevam = Console.ReadKey(true).Key;

                                if (masaAcTamamDevam == ConsoleKey.D1)
                                {
                                    Console.Clear();
                                    Console.WriteLine("MASA AÇ\n---------------------------------------------------------------");
                                    for (i = 0; i < masalar.Length; i++)
                                    {
                                        Console.WriteLine($"{masalar[i]}       {masaAcma[i]}");
                                    }
                                    Console.WriteLine("---------------------------------------------------------------");
                                    Console.Write("\nAçmak istediğiniz masa idsini giriniz:");
                                    goto tekrarMasaAcSecim;
                                }
                                else if (masaAcTamamDevam == ConsoleKey.Escape)
                                {
                                    Console.Clear();
                                    goto masaAcBasi;
                                }
                                else
                                {
                                    Console.Write("\nLütfen doğru tuşlara basınız.");
                                    goto tekrarMasaAcTamamDevam;
                                }

                            }
                        }

                        catch (FormatException)
                        {
                            Console.Write("Hatalı giriş! Lütfen yalnızca rakam giriniz:");
                            goto tekrarMasaAcSecim;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.Write("Geçersiz masa numarası! 1 ile 7 arasında bir sayı giriniz:");
                            goto tekrarMasaAcSecim;
                        }



                    case 2:
                        Console.Clear();
                        masaİslemBasi:
                        Console.WriteLine("MASA İŞLEM\n---------------------------------------------------------------");

                        for (i = 0; i < masalar.Length; i++) //açık olan masaları göstermesi için bir for döngüsü
                        {
                            if (masaAcma[i] == "[açık]")
                            {
                                Console.WriteLine($"{masalar[i]}       {masaDurumu[i]}");
                            }
                        }

                        if (acikMasaSayisi == 0) //hiç açık masa yoksa uyarı mesajı ve ana menüye yönlendirme
                        {
                            Console.WriteLine("\nLütfen önce masaları açın.Ana menüye yönlendiriliyorsunuz.Lütfen [ENTER] tuşuna basın");
                            Console.ReadLine();
                            Console.Clear();
                            goto anaMenu;
                        }
                        else if (doluMasaSayisi == acikMasaSayisi) //açık olan her masa doluysa daha fazla alınanaması için
                        {
                            Console.WriteLine("Açık olan her masa dolu.Daha fazla masa dolamaz.\n[ENTER]'a basınız.");
                            Console.ReadLine();
                            Console.Clear();
                            goto anaMenu;
                        }

                        Console.Write("\nANA MENÜ için [ESC]/Masa ekleme işlemine başlamak için herhangi bir tuşa basınız\n---------------------------------------------------------------");

                        anaMenuTusu = Console.ReadKey(true).Key;
                        if (anaMenuTusu == ConsoleKey.Escape) //esc girilip girilmediğini kontrol edecek
                        {
                            Console.Clear();
                            goto anaMenu;
                        }

                        Console.Write("\nİşlem yapmak istediğiniz masanın ID numarasını giriniz:");
                        tekrarMasaSecim:
                        try
                        {
                            masaId = int.Parse(Console.ReadLine());

                            if (masaId < 1 || masaId > masalar.Length)
                                throw new IndexOutOfRangeException();

                            if (masaAcma[masaId - 1] != "[açık]")
                            {
                                Console.WriteLine("Bu masa şu anda kapalı!Lütfen açık olan masalardan birini seçiniz.");
                                goto tekrarMasaSecim;
                            }

                            if (masaDurumu[masaId - 1] == "[boş]")
                            {
                                Console.Clear();
                                Console.WriteLine($"Masa {masaId} açılıyor...");

                                Console.WriteLine("-----------------------------------------------------------");

                                Console.WriteLine("YEMEKLER\n\nEt Yemekleri");
                                for (i = 0; i < etYemekleri.Length; i++)
                                    Console.WriteLine($"{i + 1}.{etYemekleri[i]} - {etYemekleriFiyat[i]} TL");

                                Console.WriteLine("\nPizzalar");
                                for (i = 0; i < pizzalar.Length; i++)
                                    Console.WriteLine($"{i + 4}.{pizzalar[i]} - {pizzalarFiyat[i]} TL");

                                Console.WriteLine("-----------------------------------------------------------\nİÇECEKLER");

                                Console.WriteLine("\nSıcak İçecekler");
                                for (i = 0; i < sicakIcecekler.Length; i++)
                                    Console.WriteLine($"{i + 7}.{sicakIcecekler[i]} - {sicakIceceklerFiyat[i]} TL");

                                Console.WriteLine("\nSoğuk İçecekler");
                                for (i = 0; i < sogukIcecekler.Length; i++)
                                    Console.WriteLine($"{i + 10}.{sogukIcecekler[i]} - {sogukIceceklerFiyat[i]} TL");

                                Console.WriteLine("-----------------------------------------------------------");

                                do //Masanın dolu konumuna alınabilmesi için en az bir sipariş verilmesi gerekiyor.bundan dolayı do while kullandım
                                {
                                    Console.Write("\nEklemek istediğiniz ürünün ID numarasını giriniz:");
                                tekrarMenuSecim:
                                    menuSecim = int.Parse(Console.ReadLine());

                                    Console.Write("Kaç adet istersiniz:");
                                    menuSecimAdet = int.Parse(Console.ReadLine());

                                    if (menuSecim >= 1 && menuSecim <= 3) //Et yemekleri
                                    {
                                        Console.WriteLine($"{menuSecimAdet} adet {etYemekleri[menuSecim - 1]} hazırlanıyor.");
                                        masaHesap[masaId - 1] += menuSecimAdet * etYemekleriFiyat[menuSecim - 1];
                                    }
                                    else if (menuSecim >= 4 && menuSecim <= 6) //Pizzalar
                                    {
                                        Console.WriteLine($"{menuSecimAdet} adet {pizzalar[menuSecim - 4]} pizza hazırlanıyor.");
                                        masaHesap[masaId - 1] += menuSecimAdet * pizzalarFiyat[menuSecim - 4];
                                    }
                                    else if (menuSecim >= 7 && menuSecim <= 9) //Sıcak içecekler
                                    {
                                        Console.WriteLine($"{menuSecimAdet} adet {sicakIcecekler[menuSecim - 7]} hazırlanıyor.");
                                        masaHesap[masaId - 1] += menuSecimAdet * sicakIceceklerFiyat[menuSecim - 7];
                                    }
                                    else if (menuSecim >= 10 && menuSecim <= 12) //Soğuk içecekler
                                    {
                                        Console.WriteLine($"{menuSecimAdet} adet {sogukIcecekler[menuSecim - 10]} hazırlanıyor.");
                                        masaHesap[masaId - 1] += menuSecimAdet * sogukIceceklerFiyat[menuSecim - 10];
                                    }
                                    else
                                    {
                                        Console.WriteLine("Geçersiz seçim. Lütfen menüden bir ürün seçiniz.");
                                        goto tekrarMenuSecim;
                                    }

                                    Console.Write("\nMenü seçimine devam etmek ister misiniz?(Evet için [1]/Hayır için [ESC] tuşuna basınız)");
                                tekrarMenuSecimTamamDevam:
                                    var menuSecimTamamDevam = Console.ReadKey(true).Key;

                                    if (menuSecimTamamDevam == ConsoleKey.Escape) //Sipariş tamamlandığında menuSecimDevam false olur ve ana menüye gider.
                                    {
                                        menuSecimDevam = false;
                                    }
                                    else if (menuSecimTamamDevam == ConsoleKey.D1)
                                    {
                                        menuSecimDevam = true;
                                    }
                                    else
                                    {
                                        Console.Write("Lütfen geçerli bir değer giriniz:");
                                        goto tekrarMenuSecimTamamDevam;
                                    }

                                }
                                while (menuSecimDevam == true); //True olduğu sürece sipariş alınmaya devam edilecek

                                masaDurumu[masaId - 1] = "[dolu]";
                                doluMasaSayisi++;

                                Console.Clear();
                                goto masaİslemBasi;
                            }
                            else if (masaDurumu[masaId - 1] == "[dolu]")
                            {
                                Console.Write($"{masalar[masaId - 1]} şu anda dolu. Lütfen boş olan bir masa seçiniz:");
                                goto tekrarMasaSecim;
                            }
                            else
                            {
                                Console.Write("Geçersiz masa numarası girdiniz. Lütfen tekrar giriniz:");
                                goto tekrarMasaSecim;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Hatalı giriş!Lütfen yalnızca rakam giriniz:");
                            goto tekrarMasaSecim;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Geçersiz masa numarası!Lütfen ekranda gözüken masalardan birisini giriniz:");
                            goto tekrarMasaSecim;
                        }


                    case 3:

                        try
                        {
                            Console.Clear();
                            Console.WriteLine("MASA HESAP\n---------------------------------------------------------------");

                            for (i = 0; i < masalar.Length; i++) //dolu olan masaları göstermesi için bir for döngüsü
                            {
                                if (masaDurumu[i] == "[dolu]")
                                {
                                    Console.WriteLine($"{masalar[i]}       {masaDurumu[i]}");
                                }
                            }
                            Console.WriteLine("---------------------------------------------------------------");

                            if (acikMasaSayisi == 0)
                            {
                                Console.WriteLine("Şu anda açık masa bulunmamakta hesabı görmek için masayı açmalı ve sipariş almalısınız.");
                                Console.WriteLine("Ana menüye yönlendiriliyorsunuz.Lütfen [ENTER] tuşuna basınız.");
                                Console.ReadLine();
                                Console.Clear();
                                goto anaMenu;
                            }

                            Console.Write("ANA MENÜ için [ESC]/Hesap alma işlemine başlamak için herhangi bir tuşa basınız\n---------------------------------------------------------------");

                            anaMenuTusu = Console.ReadKey(true).Key;
                            if (anaMenuTusu == ConsoleKey.Escape) //esc girilip girilmediğini kontrol edecek.
                            {
                                Console.Clear();
                                goto anaMenu;
                            }

                            else
                            {
                                if (doluMasaSayisi == 0)
                                {
                                    Console.WriteLine("Şu anda dolu masa bulunmamakta hesabı görmek için masanın dolu olması gerekmektedir.");
                                    Console.WriteLine("Ana menüye yönlendiriliyorsunuz.Lütfen [ENTER] tuşuna basınız.");
                                    Console.ReadLine();
                                    Console.Clear();
                                    goto anaMenu;
                                }
                                else
                                {
                                    Console.Write("\nHangi masanın hesabını almak istiyorsunuz?:");
                                tekrarMasaHesapSecim:
                                    masaId = int.Parse(Console.ReadLine());

                                    if (masaAcma[masaId - 1] == "[açık]")
                                    {
                                        if (masaDurumu[masaId - 1] == "[dolu]")
                                        {
                                            masaGunlukCiro[masaId - 1] += masaHesap[masaId - 1];
                                            Console.WriteLine($"{masalar[masaId - 1]}nın hesabı:{masaHesap[masaId - 1]}");
                                            masaHesap[masaId - 1] = 0;
                                            doluMasaSayisi -= 1;
                                            masaDurumu[masaId - 1] = "[boş]";

                                            Console.WriteLine($"\nŞu anda dolu olan masa sayısı:{doluMasaSayisi}");

                                            if (doluMasaSayisi == 0)
                                            {
                                                Console.WriteLine("Şu anda her masa boş.Hesabını görebileceğiniz bir masa yok.");
                                                Console.WriteLine("[ENTER] tuşuna basarak ana menüye gidebilirsiniz.");
                                                Console.ReadLine();
                                                Console.Clear();
                                                goto anaMenu;
                                            }

                                            else
                                            {
                                                Console.Write("\nHesap almaya devam etmek ister misiniz?(Evet için [1]/Hayır için [ESC] tuşuna basınız)");
                                            tekrarHesapTamamDevam:
                                                var masaHesapTamamDevam = Console.ReadKey(true).Key;

                                                if (masaHesapTamamDevam == ConsoleKey.D1)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("MASA HESAP\n---------------------------------------------------------------");
                                                    for (i = 0; i < masalar.Length; i++) //dolu olan masaları göstermesi için bir for döngüsü
                                                    {
                                                        if (masaDurumu[i] == "[dolu]")
                                                        {
                                                            Console.WriteLine($"{masalar[i]}       {masaDurumu[i]}");
                                                        }
                                                    }
                                                    Console.WriteLine("---------------------------------------------------------------");
                                                    Console.Write("\nHesabını almak istediğiniz masanın idsini giriniz:");
                                                    goto tekrarMasaHesapSecim;
                                                }
                                                else if (masaHesapTamamDevam == ConsoleKey.Escape)
                                                {
                                                    Console.Clear();
                                                    goto anaMenu;
                                                }
                                                else
                                                {
                                                    Console.Write("\nLütfen doğru tuşlara basınız.");
                                                    goto tekrarHesapTamamDevam;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{masalar[masaId - 1]} şu anda boş.Dolu olan masaların hesabını görebilirsiniz.");
                                            Console.Write("Lütfen dolu olan masalardan birini seçiniz:");
                                            goto tekrarMasaHesapSecim;

                                        }
                                    }
                                    else
                                    {
                                        Console.Write("Hesabını görmek istediğiniz masa şu an kapalı.Lütfen tekrar seçiniz:");
                                        goto tekrarMasaHesapSecim;
                                    }
                                }
                            }
                        }
                        catch (FormatException f)
                        {
                            Console.WriteLine($"Hata: {f.Message}");
                            Console.Write("\nLütfen geçerli bir tamsayı giriniz:"); ;
                            goto tekrarSecim;
                        }

                        catch (IndexOutOfRangeException)
                        {

                            Console.Write("\nLütfen dolu olan masalardan birini seçiniz:"); ;
                            goto tekrarSecim;
                        }


                    case 4:

                        try
                        {
                            Console.Clear();
                            kasaIslemleriBasi:
                            Console.WriteLine("KASA İŞLEMLERİ\n---------------------------------------------------------------\n");

                            acikMasaSayisi = 0; //masa işlem kısmında da kullanıldığı için burda sıfırlıyorum.
                            for (i = 0; i < masaAcma.Length; i++)
                            {
                                if (masaAcma[i] == "[açık]")
                                {
                                    acikMasaSayisi++;
                                }
                            }

                            if (acikMasaSayisi == 0)
                            {
                                Console.WriteLine("Hiçbir masa açılmamış.Masalar açılmadan toplam ciroyu göremezsiniz.");
                                Console.WriteLine("Ana menüye yönlendiriliyorsunuz.Lütfen [ENTER] tuşuna basın.");
                                Console.ReadLine();
                                Console.Clear();
                                goto anaMenu;
                            }

                            Console.Write("ANA MENÜ için [ESC]/Ciroları görme işlemine başlamak için herhangi bir tuşa basınız\n---------------------------------------------------------------");

                            anaMenuTusu = Console.ReadKey(true).Key;
                            if (anaMenuTusu == ConsoleKey.Escape) //esc girilip girilmediğini kontrol edecek.
                            {
                                Console.Clear();
                                goto anaMenu;
                            }

                            else
                            {
                                Console.Write("\nMasanın günlük cirosunu mu görmek istersiniz yoksa dükkanın günlük cirosunu mu?(1.Masa/2.Dükkan):");
                            tekrarKasaIslemleriSecim:
                                int kasaIslemleriSecim = int.Parse(Console.ReadLine());

                                if (kasaIslemleriSecim == 1)
                                {

                                    Console.Write("Hangi masanın günlük cirosunu görmek istersiniz:");
                                tekrarKasaIslemleriSecimMasa:
                                    masaId = int.Parse(Console.ReadLine());

                                    if (masaId - 1 < masalar.Length)
                                    {
                                        Console.WriteLine($"{masalar[masaId - 1]}'in günlük cirosu:{masaGunlukCiro[masaId - 1]}");

                                        Console.Write("\nMasaların günlük cirosunu görmeye devam etmek ister misiniz?(Evet için [1]/Hayır için [ESC] tuşuna basınız)");
                                        tekrarMasaCiroTamamDevam:
                                        var masaCiroTamamDevam = Console.ReadKey(true).Key;

                                        if (masaCiroTamamDevam == ConsoleKey.D1)
                                        {
                                            Console.Write("\nCirosunu görmek istediğiniz masanın idsini giriniz:");
                                            goto tekrarKasaIslemleriSecimMasa;
                                        }
                                        else if (masaCiroTamamDevam == ConsoleKey.Escape)
                                        {
                                            Console.Clear();
                                            goto kasaIslemleriBasi;
                                        }
                                        else
                                        {
                                            Console.Write("\nLütfen doğru tuşlara basınız.");
                                            goto tekrarMasaCiroTamamDevam;
                                        }
                                    }
                                    else
                                    {
                                        Console.Write("Lütfen geçerli bir masa numarası giriniz:");
                                        goto tekrarKasaIslemleriSecimMasa;
                                    }

                                }
                                else if (kasaIslemleriSecim == 2)
                                {
                                    for (i = 0; i < masaGunlukCiro.Length; i++)
                                    {
                                        gunlukCiro += masaGunlukCiro[i];
                                    }
                                    Console.WriteLine($"Dükkanın günlük cirosu:{gunlukCiro}\n\n[ENTER] tuşuna basarak Kasa İşlemleri menüsüne dönebilirsiniz");
                                    Console.ReadLine();
                                    Console.Clear();
                                    goto kasaIslemleriBasi;

                                }
                                else
                                {
                                    Console.Write("Lütfen geçerli bir değer giriniz:");
                                    goto tekrarKasaIslemleriSecim;
                                }
                            }
                        }
                        catch (FormatException f)
                        {
                            Console.WriteLine($"Hata: {f.Message}");
                            Console.Write("\nLütfen geçerli bir tamsayı giriniz:"); ;
                            goto tekrarSecim;
                        }
                        catch (IndexOutOfRangeException)
                        {

                            Console.Write("\nLütfen dolu olan masalardan birini seçiniz:"); ;
                            goto tekrarSecim;
                        }


                    case 0:
                        Console.WriteLine("Bizi kullandığınız için teşekkür ederiz.");
                        break;

                    default:
                        Console.Write("\nLütfen geçerli bir değer giriniz:");
                        goto tekrarSecim;

                }
            }
            catch (FormatException f)
            {
                Console.WriteLine($"Hata: {f.Message}");
                Console.Write("\nLütfen geçerli bir tamsayı giriniz: "); ;
                goto tekrarSecim;
            }

            catch (IndexOutOfRangeException)
            {

                Console.Write("\nLütfen seçim sınırları içinden bir tamsayı giriniz: "); ;
                goto tekrarSecim;
            }

            catch (Exception e)
            {
                Console.WriteLine($"Hata: {e.Message}");
                Console.Write("\nLütfen geçerli bir değer giriniz:");
                goto tekrarSecim;
            }


            Console.ReadLine();

        }
    }
}
