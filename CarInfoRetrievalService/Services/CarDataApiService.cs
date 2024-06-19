using System.Diagnostics;

namespace CarInfoRetrievalService.Services
{
    public class CarDataApiService
    {
        private readonly HttpClient _httpClient;

        public CarDataApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public void GetModelsForMakeIdWithYear(string makeId, int year)
        {
            try
            {
                string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                string url = $"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = chromePath,
                    Arguments = url
                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
