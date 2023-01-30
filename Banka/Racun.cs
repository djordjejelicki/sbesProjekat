using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka
{
    class Racun
    {
        private long broj;
        private double iznos;
        private double dozvoljeniMinus;
        private bool blokiran;
        private DateTime poslednjaTransakcija;
        private string username;

        public Racun(long broj, double iznos, double dozvoljeniMinus, bool blokiran, DateTime poslednjaTransakcija, string username)
        {
            this.broj = broj;
            this.iznos = iznos;
            this.dozvoljeniMinus = dozvoljeniMinus;
            this.blokiran = blokiran;
            this.poslednjaTransakcija = poslednjaTransakcija;
            this.username = username;
        }

        public long Broj { get => broj; set => broj = value; }
        public double Iznos { get => iznos; set => iznos = value; }
        public double DozvoljeniMinus { get => dozvoljeniMinus; set => dozvoljeniMinus = value; }
        public bool Blokiran { get => blokiran; set => blokiran = value; }
        public DateTime PoslednjaTransakcija { get => poslednjaTransakcija; set => poslednjaTransakcija = value; }
        public string Username { get => username; set => username = value; }
    }
}
