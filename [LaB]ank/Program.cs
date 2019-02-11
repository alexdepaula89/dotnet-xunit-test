using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaBank
{
    class Program
    {
        public static void Main()
        {
            ContaCorrente cc = new ContaCorrente("Sr. Alex de Paula", 11.99);

            cc.Credito(5.77);
            cc.Debito(11.22);

            Console.WriteLine("Saldo em conta R${0}", cc.Saldo);
        }
    }
}
