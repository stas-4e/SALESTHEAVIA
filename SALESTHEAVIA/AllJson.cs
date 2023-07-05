using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace SALESTHEAVIA
{
    internal class JsonModel
    {
        public string Departure_city { get; set; }
        public string Departure_city_code { get; set; }
        public string City_of_arrival { get; set; }
        public string City_of_arrival_code { get; set; }
        public DateTime Arrival_time { get; set; }
        public DateTime Dparture_time { get; set; }
        public int Price { get; set; }
    }
    internal class AviaPereleti
    {
        public string StartCity { get; set; }
        public string StartCityCode { get; set; }
        public string EndCity { get; set; }
        public string EndCityCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
        public string SearchToken { get; set; }
    }
    internal class AllJson
    {

        public List<AviaPereleti> jason = null;
        public void Load_data_from_Json()
        {
            string file_name = "C:\\Users\\стас\\Desktop\\SALESTHEAVIA\\SALESTHEAVIA\\Json\\avia1.json";

            if (File.Exists(file_name) == true)
            {
                var list = JsonConvert.DeserializeObject<List<AviaPereleti>>(File.ReadAllText(file_name));
                if (list != null)
                    jason = list;
                else
                    MessageBox.Show("Файл пустой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public List<JsonModel> Get_all_display_data()
        {
            Load_data_from_Json();
            if (jason != null)
            {
                List<JsonModel> all = new List<JsonModel>();
                for (int i = 0; i < jason.Count; i++)
                {
                    JsonModel record = new JsonModel
                    {
                        Departure_city = jason[i].StartCity,
                        Departure_city_code = jason[i].StartCityCode,
                        City_of_arrival = jason[i].EndCity,
                        City_of_arrival_code = jason[i].EndCityCode,
                        Arrival_time = jason[i].EndDate.ToLocalTime(),
                        Dparture_time = jason[i].StartDate.ToLocalTime(),
                        Price = jason[i].Price,

                    };
                    all.Add(record);
                }
                return all;
            }
            return null;
        }

    }
}
