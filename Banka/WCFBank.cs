using BankManager;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka
{
    public class WCFBank : IBANKContract
    {

        public bool Isplata(long broj, double isplata)
        {
            List<Racun> racuni = JSON.ReadRacuni();
            try
            {
                Racun racun = racuni.Find(r => r.Broj == broj);

                if (racun != null)
                {
                    if (!racun.Blokiran)
                    {
                        if ((racun.Iznos + racun.DozvoljeniMinus) >= isplata)
                        {
                            racun.Iznos -= isplata;
                            racun.PoslednjaTransakcija = DateTime.Now;
                            JSON.SaveRacun(racun);
                            try
                            {
                                Audit.IsplataSuccess(broj);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("AUDIT GRESKA: " + ex.Message);
                            }
                            return true;
                        }
                        else
                        {
                            throw new Exception("Iznos prekoracuje dozvoljeni minus");
                        }
                    }
                    else
                    {
                        throw new Exception("Racun je blokiran");
                    }

                    
                }
                else
                {

                    throw new Exception("Nepostojuci racun");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                try
                {
                    Audit.IsplataFailure(broj,e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("AUDIT GRESKA: " + ex.Message);
                }
                return false;
            }
            
        }

        public bool Opomena(long broj)
        {
            List<Racun> racuni = JSON.ReadRacuni();
            try
            {
                Racun racun = racuni.Find(r => r.Broj == broj);

                if (racun != null)
                {
                    if(racun.Iznos < 0)
                    {
                        racun.Blokiran = true;
                        JSON.SaveRacun(racun);
                        try
                        {
                            Audit.OpomenaSuccess(broj);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("AUDIT GRESKA: " + ex.Message);
                        }
                        return true;
                    }
                    else
                    {
                        throw new Exception("Racun ima vece stanje od 0");
                    }

                }
                else
                {

                    throw new Exception("Nepostojeci racun");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                try
                {
                    Audit.OpomenaFailure(broj,e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("AUDIT GRESKA: " + ex.Message);
                }
                return false;
            }

            
        }

        public bool OtvoriRacun(string username, out long broj)
        {
             broj = 0; 
            List<Racun> racuni = JSON.ReadRacuni();
            if (racuni != null)
            {
                foreach (Racun r in racuni)
                {
                    if (r.Username == username)
                    {
                        try
                        {
                            Audit.OtvoriRacunFailure(username, "Racun vec postoji");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("AUDIT GRESKA: " + ex.Message);
                        }
                        return false;
                    }
                }
            }
            Random rnd = new Random();
            string cardNumber = string.Empty;
            cardNumber += rnd.Next(1, 9).ToString();
            for (int i = 0; i < 15; i++)
            {
                cardNumber += rnd.Next(0, 9).ToString();
            }
            broj = (long)Convert.ToDouble(cardNumber);
            Racun racun = new Racun((long)Convert.ToDouble(cardNumber), 0, 30000, false, DateTime.Now, username);
            JSON.SaveRacun(racun);
            try
            {
                Audit.OtvoriRacunSuccess(username);
            }
            catch (Exception ex)
            {
                Console.WriteLine("AUDIT GRESKA: " + ex.Message);
            }
            return true;
            
        }

        public bool ProveriStanje(long broj, out double iznos)
        {
            List<Racun> racuni = JSON.ReadRacuni();
            try
            {
                Racun racun = racuni.Find(r => r.Broj == broj);

                if (racun != null)
                {
                    iznos = racun.Iznos;
                    try
                    {
                        Audit.ProveriStanjeSuccess(broj);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("AUDIT GRESKA: " + ex.Message);
                    }
                    return true;
                }
                else
                {

                    throw new Exception("Pokusaj provera stanja nepostojeceg racuna");
                }
            }
            catch (Exception e)
            {
                iznos = -1;
                Console.WriteLine(e.Message);
                try
                {
                    Audit.ProveriStanjeFailure(broj,e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("AUDIT GRESKA: " + ex.Message);
                }
                return false;
            }
            //throw new NotImplementedException();
        }

        public void TestCommunication()
        {
            Console.WriteLine("Communication established.");
        }

        public bool Uplata(long broj, double uplata)
        {
            List<Racun> racuni = JSON.ReadRacuni();
            try
            {
                Racun racun = racuni.Find(r => r.Broj == broj);

                if (racun != null)
                {
                    racun.Iznos += uplata;
                    racun.PoslednjaTransakcija = DateTime.Now;
                    if(racun.Iznos >= 0)
                    {
                        if(racun.Blokiran == true)
                        {
                            racun.Blokiran = false;
                            
                        }
                        
                    }
                    JSON.SaveRacun(racun);
                    try
                    {
                        Audit.UplataSuccess(broj);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("AUDIT GRESKA:  " + ex.Message);
                    }
                    return true;
                }
                else
                {

                    throw new Exception("Pokusaj uplate na nepostojeci racun");
                }
            }
            catch (Exception e)
            {

                
                Console.WriteLine(e.Message);
                try
                {
                    Audit.UplataFailure(broj,e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("AUDIT GRESKA:  " + ex.Message);
                }

                return false;
            }
            
        }

        public bool ZatvoriRacun(long broj)
        {
            List<Racun> racuni = JSON.ReadRacuni();
            try
            {
                Racun racun = racuni.Find(r => r.Broj == broj);

                if (racun != null)
                {
                    JSON.DeleteRacun(racun.Username);
                    try
                    {
                        Audit.ZatvoriRacunSuccess(broj);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("AUDIT GRESKA: " + ex.Message);
                    }
                    return true;

                }
                else
                {

                    throw new Exception("Greska pokusaj brisanja nepostojeceg racuna");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                try
                {
                    Audit.ZatvoriRacunFailure(broj,e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("AUDIT GRESKA: " + ex.Message);
                }
                return false;
            }
            //throw new NotImplementedException();
        }
    }
}
