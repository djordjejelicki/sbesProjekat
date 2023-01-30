using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka
{
    public class WCFBank : IBANKContract
    {
        public void TestCommunication()
        {
            Console.WriteLine("Communication established.");
        }
    }
}
