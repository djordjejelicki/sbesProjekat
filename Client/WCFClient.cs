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

            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName
                .My, StoreLocation.LocalMachine, cltCertCN);


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

        public bool OtvoriRacun(string username)
        {
            throw new NotImplementedException();
            
        }

        public bool ZatvoriRacun(long broj)
        {
            throw new NotImplementedException();
        }

        public bool ProveriStanje(long broj, out double iznos)
        {
            throw new NotImplementedException();
        }

        public bool Uplata(long broj, double uplata)
        {
            throw new NotImplementedException();
        }

        public bool Isplata(long broj, double isplata)
        {
            throw new NotImplementedException();
        }

        public bool Opomena(long broj)
        {
            throw new NotImplementedException();
        }
    }
}
