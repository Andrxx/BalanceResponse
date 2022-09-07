using BalanceResponse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace BalanceResponse.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Balance()
        {
            DateTime stDate = new DateTime(2019, 01, 01);
            DateTime endDate = new DateTime(2020, 01, 01);
            StatementModel statement = GetBalances(stDate, endDate);
            ViewBag.statement = statement;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private StatementModel GetBalances(DateTime startDate, DateTime endDate)
        {
            StatementModel _statementModel;
            // создаем список баланса
            List<BalanceModel> _balances = new List<BalanceModel>();
            string path = "wwwroot\\lib\\balance.json";
            string jBalances = System.IO.File.ReadAllText(path, Encoding.UTF8);
            BalanceModel balanceModel = new BalanceModel();
            _balances = balanceModel.CreateBalances(jBalances);
            _balances.Sort((x, y) => x.period.CompareTo(y.period));
            //return _balances;

            //создаем список платежей
            List<PaymentModel> _payments = new List<PaymentModel>();
            string pathPayments = "wwwroot\\lib\\payment.json";
            string jPayments = System.IO.File.ReadAllText(pathPayments, Encoding.UTF8);
            PaymentModel paymentModel = new PaymentModel();
            _payments = paymentModel.CreatePayments(jPayments);


            //выборка платежей по дате

            int formattedStartDate = int.Parse(startDate.ToString("yyyyMM"));
            int formattedEndDate = int.Parse(endDate.ToString("yyyyMM"));
           
            List<BalanceModel> _balancesInPeriod = new List<BalanceModel>();
            foreach(BalanceModel b in _balances)
            {
                if(b.period > formattedStartDate & b.period < formattedEndDate)
                    _balancesInPeriod.Add(b);
            }

            List<PaymentModel> _paymentsInPeriod = new List<PaymentModel>();
            foreach (PaymentModel p in _payments)
            {
                if (p.date > startDate & p.date < endDate)
                    _paymentsInPeriod.Add(p);
            }

            double _accuredInPeriod = 0; 
            foreach (BalanceModel b in _balancesInPeriod)
            {
                _accuredInPeriod += b.calculation;
            }

            double _sumInPeriod = 0;
            foreach (PaymentModel p in _paymentsInPeriod)
            {
                _sumInPeriod += p.sum;
            }

            _statementModel = new StatementModel($"Период с { startDate } по { endDate } ", _balances[0].in_balance, _accuredInPeriod, _sumInPeriod);

            return _statementModel;
            
        }
       
    } 
}