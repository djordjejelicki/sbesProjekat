using BankManager;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class WCFClient : ChannelFactory<IBANKContract>, IBANKContract, IDisposable
    {
        IBANKContract factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            
            factory = this.CreateChannel();
        }

        public void TestCommunication()
        {
            try
            {
                factory.TestCommunication();
            }
            catch(Exception e)
            {
                Console.WriteLine("[TestCommunication] ERROR = {0}", e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void Dispose()
        {
            if(factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public bool OtvoriRacun(string username, out long broj)
        {
           return factory.OtvoriRacun(username, out broj);
            
        }

        public bool ZatvoriRacun(long broj)
        {
            return factory.ZatvoriRacun(broj);
        }

        public bool ProveriStanje(long broj, out double iznos)
        {
            return factory.ProveriStanje(broj,out iznos);
        }

        public bool Uplata(long broj, double uplata)
        {
            return factory.Uplata(broj,uplata);
        }

        public bool Isplata(long broj, double isplata)
        {
            return factory.Isplata(broj,isplata);
        }

        public bool Opomena(long broj)
        {
            return factory.Opomena(broj);
        }
    }
}
