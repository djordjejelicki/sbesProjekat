using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace Manager
{
    public class CertManager
    {
        public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
        {
            return null;
        }

        public static X509Certificate2 GetCertificateFromFile(string fileName)
        {
            X509Certificate2 certificate = null;
            return certificate;
        }
    }
}
