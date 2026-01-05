using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Filminurk.Core.dto.AccuWeatherDTOs;
using Filminurk.Core.ServiceInterface;
using MimeKit;

namespace Filminurk.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDTO> AccuWeatherResult(AccuLocationWeatherResultDTO dto)
        {
            string apikey = Filminurk.Data.Enviroment.accuweatherkey; //key tuleb environmentist, ega pole hardcodedud
            var baseurl = "https://dataservice.accuweather.com/forecasts/v1/daily/1day";

            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(baseurl);
                httpclient.DefaultRequestHeaders.Accept.Clear();
                httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
            var response = await httpClient.GetAsync($"{dto.CityCode}?apikey{apikey}&details=true");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            List<AccuCityCodeRootDTO> weatherData = JsonSerializer.Deserialize<list<AccuCityCodeRootDTO>>(jsonResponse);
            dto.CityName = weatherData[0].LocalizedName;
            dto.CityCode = weatherData[0].Key;
            
            }
            string weatherResponse = baseUrl + $" {dto.CityCode}?apiKey={apikey}&metric=true";

            using (var clientWeather = new HttpClient()) {
                var httpResponseWeather = await clientWeather.GetAsync(weatherResponse);
                string jsonweather = await httpResponseWeather.Content.ReadAsStringAsync();

                AccuLocationRootDTO weatherRootDTO = JsonSerializer.Deserialize<AccuLocationRootDTO>(jsonweather);

                dto.EffectiveDate = weatherRootDTO.Headline.EffectiveDate;
                dto.EffectiveEpochDate = weatherRootDTO.Headline.EffectiveEpochDate;
                dto.Severity = weatherRootDTO.Headline.Severity;
                dto.Test = weatherRootDTO.Headline.Text;
                dto.Category = weatherRootDTO.Headline.Category;
                dto.EndDate = weatherRootDTO.Headline.EndDate;
                dto.EndEpochDate = weatherRootDTO.Headline.EndEpochDate;

            }




        }
        
    }
}
