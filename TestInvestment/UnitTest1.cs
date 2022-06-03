using Business;

namespace TestInvestment
{
    [TestClass]
    public class UnitTest
    {

        [TestMethod]
        public void PaymentManualCheck()
        {
            double amount = 12000;
            double rate = 12;
            int years = 1;
            double fixedPayment = amount / 12; //1000
            //First3Month pay:
            double firstMonthPayment = (amount) * rate/100/12 + fixedPayment; //1120
            double secondMonthPayment = (amount - fixedPayment) * rate/100/12 + fixedPayment; //110000
            double thirdMonthPayment = (amount - 2*fixedPayment) * rate/100/12 + fixedPayment;
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddMonths(3);
            Investment testInvestment = new Investment(amount, rate, years, start, end);
            double realSum = testInvestment.CalculateTotalPayment();
            double checkSum = firstMonthPayment + secondMonthPayment + thirdMonthPayment;
            Assert.AreEqual(realSum, checkSum, 0.1);
        }


        [TestMethod]
        public void PaymentShouldGreaterThenAmountAfterLastRefund()
        {
            double amount = 1000000;
            double rate = 5.25;
            int years = 1;
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddYears(1);
            Investment testInvestment = new Investment(amount, rate, years, start, end);
            Assert.IsTrue(amount < testInvestment.CalculateTotalPayment());
        }

        [TestMethod]
        public void PaymentShouldBeLessThenAmountMultipliedByRateAndYears()
        {
            double amount = 1000000;
            double rate = 7.5;
            int years = 100;
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddYears(years);
            Investment testInvestment = new Investment(amount, rate, years, start, end);
            double calculatedAmount = testInvestment.CalculateTotalPayment();
            double amountRatedByYears = (amount + amount * years * rate / 100);
            Assert.IsTrue(calculatedAmount < amountRatedByYears);
        }

        [TestMethod]
        public void PaymentShouldBeGreaterWithLongerDuration()
        {
            double amount = 100000;
            double rate = 7.5;
            int years = 3;
            DateTime start = DateTime.Now;
            DateTime end1 = DateTime.Now.AddYears(years - 1).AddMonths(2);
            DateTime end2 = DateTime.Now.AddYears(years - 1).AddMonths(3);
            Investment testInvestment1 = new Investment(amount, rate, years, start, end1);
            Investment testInvestment2 = new Investment(amount, rate, years, start, end2);
            var a = testInvestment1.CalculateTotalPayment();
            var b = testInvestment2.CalculateTotalPayment();
            Assert.IsTrue(testInvestment1.CalculateTotalPayment() < testInvestment2.CalculateTotalPayment());
        }
    }
}