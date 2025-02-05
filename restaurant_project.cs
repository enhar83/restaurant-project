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
            //değer ve referans tiplerini de araştır.(araştırma ödevi olacak herhangi bir ödev teslimi yok)
            //diziler dahil öncesine kadar olan kısımlar olacak. sonrasını eklemek yasak.
            //hangi tuşun hangi koda denk geleceğini incele.
            //diziler kullanılabilir.
            //masanın günlük kazancı ve restorantın günlük kazancını da ekle.adisyonun içindeki ürünlere gerek yok.
            //masa aç ve masa işlem farklı şeyler. 
            //masa aç kısmında masa eklenip çıkarılabilir.
            //kasa işlemde günün getirisi vs. olucak.
            #endregion

            int menuSecim, menuSecimAdet, masaId, i, j, gunlukCiro = 0, acikMasaSayisi = 0, doluMasaSayisi = 0;

            bool menuSecimDevam = true, masaAc = true;

            string[] etYemekleri = { "Köfte", "Pirzola", "İskender"};
            string[] pizzalar = { "Margarita", "Karışık" , "Vejeteryan" };
            string[] sicakIcecekler = { "Çay", "Latte", "Türk Kahvesi" };
            string[] sogukIcecekler = { "Kola", "İce Tea", "Ayran" }; //menüdeki elemanları tek dizide de toplayabilirdim ancak kafa karıştırmaması için bu şekilde yaptım.
            string[] masalar = {"1.Masa","2.Masa","3.Masa","4.Masa","5.Masa","6.Masa","7.Masa"};
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
                        Console.WriteLine("MASA AÇ\n--------------------------------------");

                        for (i = 0; i < masalar.Length; i++)
                        {
                            Console.WriteLine($"{masalar[i]}       {masaAcma[i]}");
                        }
                        Console.WriteLine("--------------------------------------");
                        Console.Write("Aktif hale getirmek istediğiniz masanın ID numarasını giriniz:");
                    tekrarMasaAcSecim:
                        try
                        {
                            while (masaAc)
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
                                }
                                Console.Write("Masa açmaya devam etmek ister misiniz?(1.Evet/2.Hayır):");
                                tekrarMasaAcTamamDevam:
                                int masaAcTamamDevam = int.Parse(Console.ReadLine());

                                if (masaAcTamamDevam == 1)
                                {
                                    Console.Write("\nAçmak istediğiniz masa idsini giriniz:");
                                    goto tekrarMasaAcSecim;
                                }
                                else if (masaAcTamamDevam == 2)
                                {
                                    Console.WriteLine("Ana menüye yönlendiriliyorsunuz...");
                                    Console.ReadLine();
                                    Console.Clear();
                                    goto anaMenu;
                                }
                                else
                                {
                                    Console.Write("Lütfen geçerli bir değer giriniz:");
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
                        break;
                    


                    case 2:
                        Console.Clear();
                        Console.WriteLine("MASA İŞLEM\n--------------------------------------");

                        // Açık olan masa sayısını kontrol et
                        for (i = 0; i < masalar.Length; i++)
                        {
                            if (masaAcma[i] == "[açık]")
                            {
                                Console.WriteLine($"{masalar[i]}       {masaDurumu[i]}");
                                acikMasaSayisi++;
                            }
                        }

                        // Eğer hiç açık masa yoksa uyarı ver ve ana menüye yönlendir
                        if (acikMasaSayisi == 0)
                        {
                            Console.WriteLine("\nLütfen önce masaları açın. Ana menüye yönlendiriliyorsunuz...");
                            Console.ReadLine();
                            Console.Clear();
                            goto anaMenu;
                        }

                        Console.Write("\nİşlem yapmak istediğiniz masanın ID numarasını giriniz: ");
                        tekrarMasaSecim:
                        try
                        {
                            masaId = int.Parse(Console.ReadLine());

                            if (masaId < 1 || masaId > masalar.Length)
                                throw new IndexOutOfRangeException();

                            if (masaAcma[masaId - 1] != "[açık]")
                            {
                                Console.WriteLine("Bu masa şu anda kapalı! Lütfen açık olan masalardan birini seçiniz.");
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

                                do // Masanın dolu konumuna alınabilmesi için en az bir sipariş verilmesi gerekiyor.
                                {
                                    Console.Write("Eklemek istediğiniz ürünün ID numarasını giriniz: ");
                                tekrarMenuSecim:
                                    menuSecim = int.Parse(Console.ReadLine());

                                    Console.Write("Kaç adet istersiniz: ");
                                    menuSecimAdet = int.Parse(Console.ReadLine());

                                    if (menuSecim >= 1 && menuSecim <= 3) // Et yemekleri
                                    {
                                        Console.WriteLine($"{menuSecimAdet} adet {etYemekleri[menuSecim - 1]} hazırlanıyor.");
                                        masaHesap[masaId - 1] += menuSecimAdet * etYemekleriFiyat[menuSecim - 1];
                                    }
                                    else if (menuSecim >= 4 && menuSecim <= 6) // Pizzalar
                                    {
                                        Console.WriteLine($"{menuSecimAdet} adet {pizzalar[menuSecim - 4]} pizza hazırlanıyor.");
                                        masaHesap[masaId - 1] += menuSecimAdet * pizzalarFiyat[menuSecim - 4];
                                    }
                                    else if (menuSecim >= 7 && menuSecim <= 9) // Sıcak içecekler
                                    {
                                        Console.WriteLine($"{menuSecimAdet} adet {sicakIcecekler[menuSecim - 7]} hazırlanıyor.");
                                        masaHesap[masaId - 1] += menuSecimAdet * sicakIceceklerFiyat[menuSecim - 7];
                                    }
                                    else if (menuSecim >= 10 && menuSecim <= 12) // Soğuk içecekler
                                    {
                                        Console.WriteLine($"{menuSecimAdet} adet {sogukIcecekler[menuSecim - 10]} hazırlanıyor.");
                                        masaHesap[masaId - 1] += menuSecimAdet * sogukIceceklerFiyat[menuSecim - 10];
                                    }
                                    else
                                    {
                                        Console.WriteLine("Geçersiz seçim. Lütfen menüden bir ürün seçiniz.");
                                        goto tekrarMenuSecim;
                                    }

                                    Console.Write("\nSeçime devam etmek ister misiniz? (1.Evet/2.Hayır): ");
                                    tekrarMenuSecimTamamDevam:
                                    int menuSecimTamamDevam = int.Parse(Console.ReadLine());

                                    if (menuSecimTamamDevam == 2) // Sipariş tamamlandığında
                                    {
                                        menuSecimDevam = false;
                                    }
                                    else if (menuSecimTamamDevam == 1)
                                    {
                                        menuSecimDevam = true;
                                    }
                                    else
                                    {
                                        Console.Write("Lütfen geçerli bir değer giriniz: ");
                                        goto tekrarMenuSecimTamamDevam;
                                    }

                                }
                                while (menuSecimDevam == true); // True olduğu sürece sipariş alınmaya devam edilecek.

                                masaDurumu[masaId - 1] = "[dolu]";

                                Console.Clear();
                                goto anaMenu;
                            }
                            else if (masaDurumu[masaId - 1] == "[dolu]")
                            {
                                Console.Write($"{masalar[masaId - 1]} şu anda dolu. Lütfen boş olan bir masa seçiniz: ");
                                goto tekrarMasaSecim;
                            }
                            else
                            {
                                Console.Write("Geçersiz masa numarası girdiniz. Lütfen tekrar giriniz: ");
                                goto tekrarMasaSecim;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Hatalı giriş! Lütfen yalnızca rakam giriniz.");
                            goto tekrarMasaSecim;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Geçersiz masa numarası! 1 ile 7 arasında bir sayı giriniz.");
                            goto tekrarMasaSecim;
                        }


                    case 3:
                        
                        Console.Clear();
                        Console.WriteLine("MASA HESAP\n--------------------------------------------------\n");

                        acikMasaSayisi = 0; //masa işlem kısmında da kullanıldığı için burda sıfırlıyorum.
                        for (i = 0; i < masaAcma.Length ; i++)
                        {
                            if (masaAcma[i] == "[açık]")
                            {
                                acikMasaSayisi++;
                                for (j = 0; j < masaDurumu.Length; j++)
                                {
                                    if (masaDurumu[i] == "[dolu]")
                                    {
                                        doluMasaSayisi++;
                                    }
                                }
                            }
                        }

                        if (acikMasaSayisi == 0)
                        {
                            Console.WriteLine("Şu anda açık masa bulunmamakta hesabı görmek için masayı açmalı ve sipariş almalısınız.");
                            Console.WriteLine("Ana menüye yönlendiriliyorsunuz...");
                            Console.ReadLine();
                            Console.Clear();
                            goto anaMenu;
                        }

                        else 
                        {
                            if (doluMasaSayisi == 0)
                            {
                                Console.WriteLine("Şu anda dolu masa bulunmamakta hesabı görmek için masanın dolu olması gerekmektedir.");
                                Console.WriteLine("Ana menüye yönlendiriliyorsunuz...");
                                Console.ReadLine();
                                Console.Clear();
                                goto anaMenu;
                            }
                            else
                            {
                                Console.Write("Hangi masanın hesabını görmek istiyorsunuz?:");
                            tekrarMasaHesapSecim:
                                masaId = int.Parse(Console.ReadLine());

                                if (masaAcma[masaId - 1] == "[açık]")
                                {
                                    if (masaDurumu[masaId - 1] == "[dolu]")
                                    {
                                        masaGunlukCiro[masaId - 1] += masaHesap[masaId - 1];
                                        Console.WriteLine($"{masalar[masaId - 1]}nın hesabı:{masaHesap[masaId - 1]}");
                                        masaHesap[masaId - 1] = 0;
                                        masaDurumu[masaId - 1] = "[boş]";
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

                                Console.ReadLine(); //konsolu temizlemeden önce entera basmak gerekir.yoksa hesabı göremeden ana menüye gider
                                Console.Clear();
                                goto anaMenu;
                            }
                        }
                    
                    
                    case 4:

                        Console.Clear();
                        Console.WriteLine("KASA İŞLEMLERİ\n--------------------------------------------------\n");

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
                            Console.WriteLine("Ana menüye yönlendiriliyorsunuz...");
                            Console.ReadLine();
                            Console.Clear();
                            goto anaMenu;
                        }

                        else
                        {
                            Console.Write("Masanın günlük cirosunu mu görmek istersiniz yoksa dükkanın günlük cirosunu mu?(1.Masa/2.Dükkan):");
                            tekrarKasaIslemleriSecim:
                            int kasaIslemleriSecim = int.Parse(Console.ReadLine());

                            if (kasaIslemleriSecim == 1)
                            {
                                Console.Write("Hangi masanın günlük cirosunu görmek istersiniz:");
                            tekrarKasaIslemleriSecimMasa:
                                masaId = int.Parse(Console.ReadLine());

                                if (masaId - 1 < masalar.Length)
                                {
                                    if (masaDurumu[masaId - 1] == "[dolu]")
                                    {
                                        Console.WriteLine($"{masalar[masaId - 1]} şu an dolu.İlk olarak masanın hesabını alınız ve masa boşalsın.");
                                        goto tekrarKasaIslemleriSecimMasa;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{masalar[masaId - 1]}'in günlük cirosu:{masaGunlukCiro[masaId - 1]}");
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
                                Console.WriteLine($"Dükkanın günlük cirosu:{gunlukCiro}");
                            }
                            else
                            {
                                Console.Write("Lütfen geçerli bir değer giriniz:");
                                goto tekrarKasaIslemleriSecim;
                            }
                            Console.ReadLine();
                            Console.Clear();
                            goto anaMenu;
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
                
                Console.Write("\nLütfen şu an açık olan masalardan birini giriniz: "); ;
                goto tekrarSecim;
            }

            catch (Exception e)
            {
                Console.WriteLine($"Hata: {e.Message}");
                Console.Write("\nLütfen geçerli bir değer giriniz: ");
                goto tekrarSecim;
            }
            

            Console.ReadLine();

        }
    }
}
