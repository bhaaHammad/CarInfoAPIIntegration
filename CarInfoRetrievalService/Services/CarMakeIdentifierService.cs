using System.Globalization;
using CsvHelper;


namespace CarInfoRetrievalService.Services
{
    public class CarMakeIdentifierService
    {
        private const string FilePath = "CarMake.csv";
        public string? FindMakeIdByMakeName(string makeName)
        {
            using (var streamReader = new StreamReader(FilePath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    while (csvReader.Read())
                    {
                        var carMake = csvReader.GetRecord<dynamic>();
                        var carMakeName = carMake != null ? carMake.make_name : null;
                        if (carMakeName != null && carMakeName == makeName.ToUpper())
                        {
                            return carMake != null ? carMake.make_id : null;
                        }
                    }
                }
            }
            return null;
        }
    }
}
