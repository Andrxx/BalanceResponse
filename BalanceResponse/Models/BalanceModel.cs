using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;

namespace BalanceResponse.Models
{
    public class BalanceModel
    {
        public int account_id { get; set; }
        public int period { get; set; }
        public double in_balance { get; set; }
        public double calculation { get; set; }

        /// <summary>
        /// парсим данные
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BalanceModel CreateBM(JObject data)
        {
            BalanceModel _balanceModel = new BalanceModel();

            _balanceModel.account_id = (int)data.GetValue("account_id");
            _balanceModel.period = (int)data.GetValue("period");
            _balanceModel.in_balance = (int)data.GetValue("in_balance");
            _balanceModel.calculation = (int)data.GetValue("calculation");

            return _balanceModel;
        }

        /// <summary>
        /// Создаем массив балансов
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<BalanceModel> CreateBalances(string data)
        {
            BalanceModel _balanceModel = new BalanceModel();
            List<BalanceModel> _balanceModels = new List<BalanceModel>();

            JArray jBalances = (JArray)JsonConvert.DeserializeObject(data);
            foreach (JObject o in jBalances)
            {
                _balanceModels.Add(CreateBM(o));
            }
            return _balanceModels;
        }

    }
}
