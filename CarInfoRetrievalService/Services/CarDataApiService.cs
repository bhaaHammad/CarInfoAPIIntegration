namespace CarInfoRetrievalService.Services
{
    public class CarDataApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear";


        public CarDataApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetCarModelsByMakeAndYear(string makeId, int year)
        {
            string url = $"{ApiBaseUrl}/makeId/{makeId}/modelyear/{year}?format=json";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();
                return jsonString;
            }
        }
    }
}
