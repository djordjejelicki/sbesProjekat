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
        void OtvoriRacun();
        [OperationContract]
        void ZatvoriRacun();
        [OperationContract]
        void ProveriStanje();
        [OperationContract]
        void Uplata();
        [OperationContract]
        void Isplata();
        [OperationContract]
        void Opomena();
    }
}
