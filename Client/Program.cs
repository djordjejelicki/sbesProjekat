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
            string bankCertCN = "Mika";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 bankCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, bankCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/receiver"),
                new X509CertificateEndpointIdentity(bankCert));

            using(WCFClient proxy = new WCFClient(binding, address))
            {
                proxy.TestCommunication();
                Console.WriteLine("TestCommunication() finished. Press <enter> to continue ...");
                Console.ReadLine();
            }
        }
    }
}
