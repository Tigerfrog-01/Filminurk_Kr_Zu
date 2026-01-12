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
            var cityurl = "https://dataservice.accuweather.com/locations/v1/cities/search";

            /*get city*/
            using (var HttpClient = new HttpClient())
            {
                HttpClient.BaseAddress = new Uri(cityurl);
                HttpClient.DefaultRequestHeaders.Accept.Clear();
                HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await HttpClient.GetAsync($"? apikey = { apikey}&={ dto.CityName}");
                var JsonResponse = await response.Content.ReadAsStringAsync();
                List<AccuCityCodeRootDTO> weatherData = JsonSerializer.Deserialize<List<AccuCityCodeRootDTO>>(JsonResponse);

                dto.CityCode = weatherData[0].Key;
                dto.CityName = weatherData[0].LocalizedName;

                
            }
            string weatheresponse = baseurl + $"{dto.CityCode}?apikey={apikey}";

            using (var clientLocation = new HttpClient())
            {
                var httpResponseLocation = await clientLocation.GetAsync(LocationResponse);
                string jsonLocation = await httpResponseLocation.Content.ReadAsStringAsync();
                AccuCityCodeRootDTO cityRootDto.Key;


            }
            


            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(baseurl);
                httpclient.DefaultRequestHeaders.Accept.Clear();
                httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpclient.GetAsync($"{dto.CityCode}?apikey{apikey}&details=true");
            var jsonResponse =  response.Content.ReadAsStringAsync();
            List<AccuCityCodeRootDTO> weatherData = JsonSerializer.Deserialize<List<AccuCityCodeRootDTO>>(jsonResponse);
            dto.CityName = weatherData[0].LocalizedName;
            dto.CityCode = weatherData[0].Key;
            
            }
            string weatherResponse = baseurl + $" {dto.CityCode}?apiKey={apikey}&metric=true";

            using (var clientWeather = new HttpClient()) {
                var httpResponseWeather = await clientWeather.GetAsync(weatherResponse);
                string jsonweather = await httpResponseWeather.Content.ReadAsStringAsync();

                AccuLocationRootDTO weatherRootDTO = JsonSerializer.Deserialize<AccuLocationRootDTO>(jsonweather);

                dto.EffectiveDate = weatherRootDTO.Headline.EffectiveDate;
                dto.EffectiveEpochDate = weatherRootDTO.Headline.EffectiveEpochDate;
                dto.Severity = weatherRootDTO.Headline.Severity;
                dto.Text = weatherRootDTO.Headline.Text;
                dto.Category = weatherRootDTO.Headline.Category;
                dto.EndDate = weatherRootDTO.Headline.EndDate;
                dto.EndEpochDate = weatherRootDTO.Headline.EndEpochDate;

                dto.MobileLink = weatherRootDTO.Headline.MobileLink;
                dto.Link = weatherRootDTO.Headline.Link;


                dto.DailyForeCastsDate = weatherRootDTO.DailyForecasts[0].Date;
                dto.DailyForecastsEpochDate = weatherRootDTO.DailyForecasts[0].EpochDate;

                dto.TempMinValue = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Value;
                dto.TempMinUnit = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Unit;
                dto.TempMinUnitType = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.UnitType;

                dto.TempMaxValue = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.Value;
                dto.TempMaxUnit = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.Unit;
                dto.TempMaxUnitType = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.UnitType;

                dto.DayIcon = weatherRootDTO.DailyForecasts[0].Day.Icon;
                dto.DayIconPhrase = weatherRootDTO.DailyForecasts[0].Day.IconPhrase;
                dto.DayHasPrecipitation = weatherRootDTO.DailyForecasts[0].Day.HasPrecipitation;
                dto.DayHasPrecipitationType = weatherRootDTO.DailyForecasts[0].Day.PrecipitationType;
                dto.DayHasPrecipitationIntensity = weatherRootDTO.DailyForecasts[0].Day.PrecipitationIntensity;

                dto.NightIcon = weatherRootDTO.DailyForecasts[0].Night.Icon;
                dto.NightIconPhrase = weatherRootDTO.DailyForecasts[0].Night.IconPhrase;
                dto.NightHasPrecipitation = weatherRootDTO.DailyForecasts[0].Night.HasPrecipitation;
                dto.NightHasPrecipitationType = weatherRootDTO.DailyForecasts[0].Night.PrecipitationType;
                dto.NightHasPrecipitationIntensity = weatherRootDTO.DailyForecasts[0].Night.PrecipitationIntensity;



            }

            return dto;


        }
        
    }
}
