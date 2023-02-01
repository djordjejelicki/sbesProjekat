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

        public void OtvoriRacun()
        {
            throw new NotImplementedException();
            
        }

        public void ZatvoriRacun()
        {
            throw new NotImplementedException();
        }

        public void ProveriStanje()
        {
            throw new NotImplementedException();
        }

        public void Uplata()
        {
            throw new NotImplementedException();
        }

        public void Isplata()
        {
            throw new NotImplementedException();
        }

        public void Opomena()
        {
            throw new NotImplementedException();
        }
    }
}
