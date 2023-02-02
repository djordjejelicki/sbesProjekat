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
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                    
                }
                else
                {

                    return false;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
            //throw new NotImplementedException();
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {

                    return false;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }

            //throw new NotImplementedException();
        }

        public bool OtvoriRacun(string username)
        {
            
            List<Racun> racuni = JSON.ReadRacuni();
            foreach(Racun r in racuni)
            {
                if(r.Username == username)
                {
                    return false;
                }
            }
            Random rnd = new Random();
            string cardNumber = string.Empty;

            for (int i = 0; i < 32; i++)
            {
                cardNumber += rnd.Next(0, 9).ToString();
            }
            Racun racun = new Racun((long)Convert.ToDouble(cardNumber), 0, 30000, false, DateTime.Now, username);
            JSON.SaveRacun(racun);
            return true;
            //throw new NotImplementedException();
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
                    return true;
                }
                else
                {
                    iznos = -1;
                    return false;
                }
            }
            catch (Exception e)
            {
                iznos = -1;
                Console.WriteLine(e.Message);
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
                            JSON.SaveRacun(racun);
                        }
                    }
                    return true;
                }
                else
                {
                   
                    return false;
                }
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                return false;
            }
            //throw new NotImplementedException();
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
                    return true;

                }
                else
                {

                    return false;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
            //throw new NotImplementedException();
        }
    }
}
