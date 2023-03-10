using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankManager
{
    public class BankCertValidator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {

            X509Certificate2 bankCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine,
                "Banka");

            if (!certificate.Issuer.Equals(bankCert.Issuer))
            {
                throw new Exception("Certificate is not from the valid issuer.");
            }

        }
    }
}
