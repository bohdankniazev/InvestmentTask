using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Investment myInvest = DataInput.CreateInvestment();
            double farmedGold = myInvest.CalculateTotalPayment();
            Console.WriteLine($"Sum of payments for requested period is {farmedGold}");
            Console.ReadLine();
        }



    }
}
