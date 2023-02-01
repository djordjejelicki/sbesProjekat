using Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManager
{
    public class JSON
    {
        public static List<Racun> ReadRacuni()
        {
            string path = "..\\..\\racuni.json";

            List<Racun> racuni;

            if (File.Exists(path))
            {
                using(StreamReader sr = new StreamReader(path))
                {
                    string file = sr.ReadToEnd();
                    racuni = JsonConvert.DeserializeObject<List<Racun>>(file);
                }
            }
            else
            {
                racuni = null;
            }

            return racuni;
        }

        public static void SaveRacun(Racun racun)
        {
            string file;
            List<Racun> racuni;

            string path = "..\\..\\racuni.json";

            if (File.Exists(path))
            {
                using(StreamReader sr = new StreamReader(path))
                {
                    string fileTemp = sr.ReadToEnd();
                    racuni = JsonConvert.DeserializeObject<List<Racun>>(fileTemp);
                    if(racuni != null)
                    {
                        foreach(Racun r in racuni)
                        {
                            if(r.Username == racun.Username)
                            {
                                racuni.Remove(r);
                                break;
                            }
                        }
                    }
                    else
                    {
                        racuni = new List<Racun>();
                    }
                    racuni.Add(racun);
                    file = JsonConvert.SerializeObject(racuni);
                }
            }
            else
            {
                racuni = new List<Racun>();
                racuni.Add(racun);
                file = JsonConvert.SerializeObject(racuni);
            }

            File.WriteAllText(path, file);
            
        }

        public static void DeleteRacun(string username)
        {
            string file;
            List<Racun> racuni;

            string path = "..\\..\\racuni.json";

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string fileTemp = sr.ReadToEnd();
                    racuni = JsonConvert.DeserializeObject<List<Racun>>(fileTemp);
                    if (racuni != null)
                    {
                        foreach(Racun r in racuni)
                        {
                            if(r.Username == username)
                            {
                                racuni.Remove(r);
                                break;
                            }
                        }
                    }
                    file = JsonConvert.SerializeObject(racuni);
                    File.WriteAllText(path, file);
                }

            }


        }


    }
}
