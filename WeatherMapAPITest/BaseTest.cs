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
        public async Task GetWeatherApi()
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

        [TestCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}




