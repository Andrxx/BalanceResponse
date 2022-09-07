using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BalanceResponse.Models
{
    public class PaymentModel
    {
        public int account_id { get; set; }
        public DateTime date { get; set; }
        public double sum { get; set; }
        public string payment_guid { get; set; }




        /// <summary>
        /// парсим данные
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PaymentModel CreatePM(JObject data)
        {
            PaymentModel _paymentModel = new PaymentModel();

            _paymentModel.account_id = (int)data.GetValue("account_id");
            _paymentModel.date = (DateTime)data.GetValue("date");
            _paymentModel.sum = (double)data.GetValue("sum");
            _paymentModel.payment_guid = (string)data.GetValue("payment_guid");


            return _paymentModel;
        }

        /// <summary>
        /// Создаем массив платежей
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<PaymentModel> CreatePayments(string data)
        {
            PaymentModel _paymentModel = new PaymentModel();
            List<PaymentModel> _paymentModels = new List<PaymentModel>();

            JArray jPayments = (JArray)JsonConvert.DeserializeObject(data);
            foreach (JObject o in jPayments)
            {
                _paymentModels.Add(CreatePM(o));
            }
            return _paymentModels;
        }
    }
}
