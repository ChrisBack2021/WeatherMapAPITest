using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherMapAPITest
{
    [TestClass]
    public class WeatherApiTests
    {
        public HttpClient _httpClient;

        [TestInitialize]
        public void SetUpTest()
        {
            // Initialize HttpClient with the base URL of the OpenWeatherMap API
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
        }

        [TestMethod]
        public async Task GetWeatherAPI()
        {

            string apiKey = "6cbfd8ad172f3a00e0593c7272b2193d";
            string cityName = "Sydney";
            string endPoint = $"weather?q={cityName}&appid={apiKey}";


            HttpResponseMessage res = await _httpClient.GetAsync(endPoint);
            Assert.IsNotNull(res.StatusCode, "Response status code can be found.");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, res.StatusCode, "Status code 200 could not be reached");


            string resBody = await res.Content.ReadAsStringAsync();
            Assert.IsNotNull(resBody);
            Assert.IsTrue(resBody.Contains("Sydney"), "response body should include Sydney.");
            Assert.IsTrue(resBody.Contains("AU"), "response body should include AU as country.");
        }

        [TestMethod]
        public async Task GetIncorrectCityWeatherAPI()
        {
            string apiKey = "6cbfd8ad172f3a00e0593c7272b2193d";
            string cityName = "Sydnamy";
            string endPoint = $"weather?q={cityName}&appid={apiKey}";


            HttpResponseMessage res = await _httpClient.GetAsync(endPoint);
            Assert.IsNotNull(res.StatusCode, "Response status code can be found.");
            Assert.AreNotEqual(System.Net.HttpStatusCode.OK, res.StatusCode, "Status code 200 should not be reached");
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, res.StatusCode, "Status code 404 is found due to incorrect city.");
            

            string resBody = await res.Content.ReadAsStringAsync();
            Assert.IsNotNull(resBody);
            Console.WriteLine(resBody);
            Assert.IsTrue(resBody.Contains("city not found"), "City not found as incorrect city name");
        }

        [TestMethod]
        public async Task GetIncorrectAPIKey()
        {
            string apiKey = "6cbfd8ad172f3a00e0593c7272b2193dasd";
            string cityName = "Sydney";
            string endPoint = $"weather?q={cityName}&appid={apiKey}";


            HttpResponseMessage res = await _httpClient.GetAsync(endPoint);
            Assert.IsNotNull(res.StatusCode, "Response status code can be found.");
            Assert.AreEqual(System.Net.HttpStatusCode.Unauthorized, res.StatusCode, "Status code 401 should be reached as incorrect API key.");


            string resBody = await res.Content.ReadAsStringAsync();
            Assert.IsNotNull(resBody);
            Console.WriteLine(resBody);
            Assert.IsTrue(resBody.Contains("Invalid API key"), "Incorrect API key is given.");
        }


        [TestCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}




