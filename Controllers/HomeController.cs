using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using KSingletonWeather.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace KSingletonWeather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Weather()
        {

            return View();
        }

        /// <summary>
        /// Gets the weather forecast for Belfast
        /// </summary>
        /// <returns>Returns deserialized content of call to API - then put into ResultViewModel</returns>
        private async Task<ResultViewModel> GetWeatherForecasts()
        {
            // Get an instance of HttpClient from the factory that was registered in Startup.cs otherwise will be null and fail
            var client = _httpClientFactory.CreateClient("MetaWeather");

            //Make a call to the API and await a response. 
            var result = await client.GetAsync("/api/location/44544/"); //44544 is the WOEID for Belfast (future development use /api/location/search/?query=Belfast to get WOEID)

            if (result.IsSuccessStatusCode)
            {
                //Read the reponse and use JSON to deserialise it into ResultViewModel class
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultViewModel>(content);
            }
            return null;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = await GetWeatherForecasts();
            // Pass the data into the View
            return View(model);
        }
    }
}
