

namespace BalanceResponse.Models
{
    public class StatementModel
    {
        public string? Period { get; set; }
        public double InkSaldo { get; set; } //Входящее сальдо на начало периода
        public double Accrued { get; set; } //Начислено за период
        public double Paid { get; set; } //Оплачено за период
        public double OutBalance { get; set; } //Исходящий баланс на конец периода


        public StatementModel(string? period, double inkSaldo, double accrued, double paid)
        {
            Period = period;
            InkSaldo = inkSaldo;
            Accrued = accrued;
            Paid = paid;
            OutBalance = CountBalance(inkSaldo, accrued, paid);
        }

        private double CountBalance(double inc, double accured, double paid)
        {
            double balance = inc + accured - paid;
            return balance;
        }
    }
}
