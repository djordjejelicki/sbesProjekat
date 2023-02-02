using BankManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            string bankCertCN = "Banka";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 bankCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, bankCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/receiver"),
                new X509CertificateEndpointIdentity(bankCert));

            using (WCFClient proxy = new WCFClient(binding, address))
            {
                proxy.TestCommunication();
                Console.WriteLine("TestCommunication() finished. Press <enter> to continue ...");
                Console.ReadLine();
            }
        }


        public static void KorisnikInterface(WCFClient proxy)
        {
            string option;
            do
            {
                Console.WriteLine("Izaberite komandu: ");
                Console.WriteLine("\t1. Provera stanja");
                Console.WriteLine("\t2. Uplata");
                Console.WriteLine("\t3. Isplata");
                Console.WriteLine("\t4. Kraj");
                Console.WriteLine("Vasa opcija: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        {
                            Console.WriteLine("Unesite broj vaseg racuna");
                            string racun = Console.ReadLine();
                            double stanje;
                            if (proxy.ProveriStanje((long)Convert.ToDouble(racun), out stanje))
                            {
                                Console.WriteLine("Stanje na vasem racunu je: " + stanje + " DIN");
                            }
                            else
                            {
                                Console.WriteLine("GRESKA: Doslo je do greske prilikom provere stanja");
                            }
                        }
                        break;

                    case "2":
                        {
                            Console.WriteLine("Unesite broj vaseg racuna: ");
                            string racun = Console.ReadLine();
                            Console.WriteLine("Unesite kolicinu novca koju zelite da uplatite: ");
                            string uplata = Console.ReadLine();
                            if (proxy.Uplata((long)Convert.ToDouble(racun), Convert.ToDouble(uplata)))
                            {
                                Console.WriteLine("Uplata uspesno izvrsena");
                            }
                            else
                            {
                                Console.WriteLine("GRESKA: Uplata nije izvrsena");
                            }

                        }
                        break;

                    case "3":
                        {
                            Console.WriteLine("Unesite broj vaseg racuna: ");
                            string racun = Console.ReadLine();
                            Console.WriteLine("Unesite kolicinu novca koju zelite da isplatite: ");
                            string isplata = Console.ReadLine();
                            if (proxy.Isplata((long)Convert.ToDouble(racun), Convert.ToDouble(isplata)))
                            {
                                Console.WriteLine("Uplata uspesno izvrsena");
                            }
                            else
                            {
                                Console.WriteLine("GRESKA: Uplata nije izvrsena");
                            }
                        }
                        break;

                    default:
                        break;
                }

            } while (option != "4");
        }

        public static void SluzbenikInterface(WCFClient proxy)
        {
            string option;
            do
            {
                Console.WriteLine("Izaberite komandu: ");
                Console.WriteLine("\t1. Otvori racun");
                Console.WriteLine("\t2. Zatvori racun");
                Console.WriteLine("\t3. Provera stanja");
                Console.WriteLine("\t4. Uplata");
                Console.WriteLine("\t5. Isplata");
                Console.WriteLine("\t6. Opomena");
                Console.WriteLine("\t7. Kraj");
                Console.WriteLine("Vasa opcija: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        {
                            Console.WriteLine("Unesite username korisnika kome otvarate racun: ");
                            string username = Console.ReadLine();
                            if (proxy.OtvoriRacun(username))
                            {
                                Console.WriteLine("Uspesno ste otvorili racun korisniku: " + username);
                            }
                            else
                            {
                                Console.WriteLine("GRESKA: korisnik vec ima otvoren racun");
                            }
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine("Unesite broj racuna koji zelite da zatvorite: ");
                            string racun = Console.ReadLine();
                            if (proxy.ZatvoriRacun((long)Convert.ToDouble(racun)))
                            {
                                Console.WriteLine("Uspesno ste zatvorili racun");
                            }
                            else
                            {
                                Console.WriteLine("GRESKA: Doslo je do greske prilikom brisanja racuna");
                            }
                        }
                        break;
                    case "3":
                        {
                            Console.WriteLine("Unesite broj racuna kojem zelite da proverite stanje");
                            string racun = Console.ReadLine();
                            double stanje;
                            if(proxy.ProveriStanje((long)Convert.ToDouble(racun),out stanje))
                            {
                                Console.WriteLine("Stanje na racunu korisnika je: " + stanje + " DIN");
                            }
                            else
                            {
                                Console.WriteLine("GRESKA: doslo je do greske prilikom provere stanja korisnika");
                            }
                        }
                        break;
                    case "4":
                        {
                            Console.WriteLine("Unesite broj racuna korisnika kojem uplacujete novac: ");
                            string racun = Console.ReadLine();
                            Console.WriteLine("Unesite kolicinu novca koju zelite da uplatite: ");
                            string uplata = Console.ReadLine();
                            if (proxy.Uplata((long)Convert.ToDouble(racun), Convert.ToDouble(uplata)))
                            {
                                Console.WriteLine("Uplata uspesno izvrsena");
                            }
                            else
                            {
                                Console.WriteLine("GRESKA: Uplata nije izvrsena");
                            }
                        }
                        break;
                    case "5":
                        {
                            Console.WriteLine("Unesite broj racuna korisnika kojem isplacujete novac: ");
                            string racun = Console.ReadLine();
                            Console.WriteLine("Unesite kolicinu novca koju zelite da isplatite: ");
                            string isplata = Console.ReadLine();
                            if (proxy.Isplata((long)Convert.ToDouble(racun), Convert.ToDouble(isplata)))
                            {
                                Console.WriteLine("Isplata uspesno izvrsena");
                            }
                            else
                            {
                                Console.WriteLine("GRESKA: Isplata nije izvrsena");
                            }
                        }
                        break;
                    case "6":
                        {
                            Console.WriteLine("Unesite broj racuna korisnika: ");
                            string racun = Console.ReadLine();
                            if (proxy.Opomena((long)Convert.ToDouble(racun)))
                            {
                                Console.WriteLine("Racun korisnika je blokiran jer je u minusu");
                            }
                            else
                            {
                                Console.WriteLine("Korisnikov racun nije blokiran jer nije u minusu");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            while (option != "7");
        }
    }   
}
