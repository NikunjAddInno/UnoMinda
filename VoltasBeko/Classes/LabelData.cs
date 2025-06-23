using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltasBeko
{
    public class LabelData
    {
        public string EnergyConsumption { get; set; }
        public int StarRating { get; set; }
        public string Volume { get; set; }

        // Constructor
        public LabelData(string energyConsumption, int starRating, string volume)
        {
            EnergyConsumption = energyConsumption;
            StarRating = starRating;
            Volume = volume;
        }

        // Default constructor required for deserialization
        public LabelData() { }

        // Method to display appliance information
        public void DisplayInformation()
        {
            Console.WriteLine($"Energy Consumption: {EnergyConsumption}");
            Console.WriteLine($"Star Rating: {StarRating}");
            Console.WriteLine($"Volume: {Volume}");
        }
    }
}
