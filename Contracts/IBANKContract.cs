using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface IBANKContract
    {
        [OperationContract]
        void TestCommunication();
        [OperationContract]
        bool OtvoriRacun(string username, out long broj);
        [OperationContract]
        bool ZatvoriRacun(long broj);
        [OperationContract]
        bool ProveriStanje(long broj, out double iznos);
        [OperationContract]
        bool Uplata(long broj, double uplata);
        [OperationContract]
        bool Isplata(long broj, double isplata);
        [OperationContract]
        bool Opomena(long broj);
    }
}
