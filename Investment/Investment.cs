using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Investment
    {
        public Investment(double amount, double rate, int years, DateTime initialDate, DateTime espirationDate)
        {
            this.amount = amount;
            this.years = years;
            this.Rate = rate;           
            this.AgreementDate = initialDate;
            this.CalculateDate = espirationDate;
        }

        public double FixedPayment
        {
            get => amount / (years * 365 / 30);
        }

        public double TotalMonthes
        {
            get => (int)((CalculateDate - AgreementDate).TotalDays / 30);
        }

        //Sum of investment - X
        private readonly double amount;

        //Duration - N
        private readonly int years;

        //private DateTime agreementDate;
        public DateTime AgreementDate { get; }

        public DateTime CalculateDate { get; set; }

        //Interest rate
        public double Rate { get; private set; }

        private double MonthlyPayment(double amount)
        {
            return FixedPayment + amount * (Rate / 100) / 12;
        }

        public double CalculateTotalPayment()
        {
            double total = 0;
            double interestAmount = amount;          
            for (int i = 0; i < TotalMonthes; i++)
            {
                if (FixedPayment > interestAmount)
                {
                    double mounthly = amount * (Rate / 100) / 12;
                    total += interestAmount + mounthly;
                    break;
                }
                else
                {
                    total += MonthlyPayment(interestAmount);
                    interestAmount -= FixedPayment;
                }
            }
            return Math.Round(total, 2);
        }
    }

    public class DataInput
    {

        public static Investment CreateInvestment()
        {
            string agreementDate = string.Empty;
            string calculationDate = string.Empty;
            string rate = string.Empty;
            string amount = string.Empty;
            string years = string.Empty;

            double cleanRate = 0.0;       
            for (; ; )
            {
                Console.WriteLine("Please enter a rate in % per year.");
                rate = Console.ReadLine();
                if (!double.TryParse(rate, out cleanRate))
                    Console.WriteLine("Wrong input. Examle: 5.65");
                else if (cleanRate <= 0)
                    Console.WriteLine("Rate per year must be greter than 0.");
                else
                    break;
            }

            double cleanAmount = 0.0;
            for (; ; )
            {
                Console.WriteLine("Please enter an initial principal amount.");
                amount = Console.ReadLine();
                if(!double.TryParse(amount, out cleanAmount))
                    Console.WriteLine("Wrong input. Examle: 50000.00");
                else if (cleanAmount <= 0 || cleanAmount > 1000000000000)
                    Console.WriteLine("Initial principal must be greater than 0 and less than 1000000000000.");
                else
                    break;
            }

            int cleanYears = 0;
            for (; ; )
            {
                Console.WriteLine("Please enter Investment duration in years.");
                years = Console.ReadLine();
                if(!int.TryParse(years, out cleanYears))
                    Console.WriteLine("Wrong input. Examle: 8");
                else if (cleanYears <= 0 || cleanYears > 100)
                    Console.WriteLine("Duration should be minimum 1 year and maximum 100 years.");
                else
                    break;
            }

            DateTime cleanAgreementDate = DateTime.Now;        
            for (; ; )
            {
                Console.WriteLine("Please enter Agreement date in next format - 5/28/2012.");
                agreementDate = Console.ReadLine();
                if (!DateTime.TryParse(agreementDate, out cleanAgreementDate))
                    Console.WriteLine("Wrong input.");
                else if (cleanAgreementDate < DateTime.Now && cleanAgreementDate.Date != DateTime.Now.Date)
                    Console.WriteLine("Agreement date couldn't be in the past.");
                else
                    break;
            }

            DateTime cleanCalculationDate = DateTime.Now;           
            for (; ; )
            {
                Console.WriteLine("Please enter Calculation date in next format - 5/28/2012.");
                calculationDate = Console.ReadLine();
                if(!DateTime.TryParse(calculationDate, out cleanCalculationDate))
                    Console.WriteLine("Wrong input.");
                else if (cleanCalculationDate <= cleanAgreementDate)
                    Console.WriteLine("Calculation date can't be less than Agreement date.");
                else if (cleanCalculationDate > cleanAgreementDate.AddYears(cleanYears))
                    Console.WriteLine("Calculation date must be between Agreement date and date of last refund payment)");
                else
                    break;
            }

            return new Investment(cleanAmount, cleanRate, cleanYears, cleanAgreementDate, cleanCalculationDate);
        }

    }
}
