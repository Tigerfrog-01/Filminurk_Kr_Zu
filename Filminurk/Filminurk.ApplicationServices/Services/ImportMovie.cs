using Filminurk.Core.dto;
using Filminurk.Core.Domain;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data; 
using System.Text.Json;

namespace Filminurk.ApplicationServices.Services
{
    public class ImportMovie : IImportServices
    {
       
        private readonly FilminurkTARpe24Context _context;

        public ImportMovie(FilminurkTARpe24Context context)
        {
            _context = context;

        }

        public async Task<ImportDTO> ImportMovieService(ImportDTO dto)
        {
            string apikey = Filminurk.Data.Enviroment.Importkey;

            var url = $"https://www.omdbapi.com/?t={dto.Title}&apikey={apikey}";

            using (var httpclient = new HttpClient())
            {
                var response = await httpclient.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var movieData = JsonSerializer.Deserialize<ImportDTO>(jsonResponse,

                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (movieData != null && movieData.Response != "False")
                {
             
                    var domainMovie = new Movie
                    {
                        ID = Guid.NewGuid(),
                        Title = movieData.Title,
                        Description = movieData.Plot,

                        Director = movieData.Director,
                  
                        FirstPublished = DateOnly.TryParse(movieData.Year.Substring(0, 4), out var date) ? date : new DateOnly(2000, 1, 1),
                        IMDBrating = (int)(double.TryParse(movieData.imdbRating, out var r) ? r : 0),
                        EntryCreatedAt = DateTime.Now,

                        EntryModifedAt = DateTime.Now
                    };

                   
                    if (!string.IsNullOrEmpty(movieData.Poster) && movieData.Poster != "N/A")
                    {
                        var poster = new FileToApi
                        {
                            ImageID = Guid.NewGuid(),

                            ExistingFilePath = movieData.Poster,

                            MovieID = domainMovie.ID,

                            IsPoster = true
                        };
                        _context.FilesToApi.Add(poster);
                    }

                    _context.Movies.Add(domainMovie);

                    await _context.SaveChangesAsync();

                    return movieData;
                }
            }
            return null;
        }
    }
}