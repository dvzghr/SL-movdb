using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataContracts;
using ImdbScrapperService.HelperClasses;
using Web.Core;

namespace ImdbScrapperService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [WcfErrorBehavior]
    [WcfSilverlightFaultBehavior]
    public class Service1 : IService1
    {
        public ImdbMovie GetMovie(string url)
        {
            var imdb = new ImdbMovScraped(url);
            if (!imdb.Status)
                throw new Exception("Data could not be scraped!");
            //return (ImdbMovie)imdb;

            var mov = new ImdbMovie
            {
                Awards = string.IsNullOrWhiteSpace(imdb.Awards) ? (byte)0 : Convert.ToByte(imdb.Awards),
                Countries = imdb.Countries.Cast<string>().ToList(),
                Directors = imdb.Directors.Cast<string>().ToList(),
                Genres = imdb.Genres.Cast<string>().ToList(),
                Id = imdb.Id,
                ImdbUrl = imdb.ImdbUrl,
                MediaImages = imdb.MediaImages.Cast<string>().ToList(),
                MpaaRating = imdb.MpaaRating,
                Nominations = string.IsNullOrWhiteSpace(imdb.Nominations) ? (byte)0 : Convert.ToByte(imdb.Nominations),
                OriginalTitle = imdb.OriginalTitle,
                Oscars = string.IsNullOrWhiteSpace(imdb.Oscars) ? (byte)0 : Convert.ToByte(imdb.Oscars),
                Plot = imdb.Plot,
                Poster = imdb.Poster,
                PosterFull = imdb.PosterFull,
                PosterLarge = imdb.PosterLarge,
                Rating = string.IsNullOrWhiteSpace(imdb.Rating) ? 0 : Double.Parse(imdb.Rating, NumberFormatInfo.InvariantInfo),
                ReleaseDate = string.IsNullOrWhiteSpace(imdb.ReleaseDate) ? (DateTime?)null : Convert.ToDateTime(imdb.ReleaseDate),
                Runtime = string.IsNullOrWhiteSpace(imdb.Runtime) ? (short)0 : Convert.ToInt16(imdb.Runtime),
                Storyline = imdb.Storyline,
                Tagline = imdb.Tagline,
                Title = string.IsNullOrEmpty(imdb.OriginalTitle) ? imdb.Title : imdb.OriginalTitle,
                Top250 = string.IsNullOrWhiteSpace(imdb.Top250) ? (byte)0 : Convert.ToByte(imdb.Top250),
                Votes = string.IsNullOrWhiteSpace(imdb.Votes) ? 0 : Convert.ToUInt32(imdb.Votes.Replace(",", "")),
                Writers = imdb.Writers.Cast<string>().ToList(),
                Year = string.IsNullOrWhiteSpace(imdb.Year) ? (short)0 : Convert.ToInt16(imdb.Year)
            };

            var actors = (from string star in imdb.Stars
                          select new Star(star)).Cast<Actor>().ToList();

            actors.AddRange(from object cast in imdb.Cast
                            where !imdb.Stars.Contains(cast)
                            select new Actor(cast.ToString()));
            mov.Cast = actors;
            //mov.Cast=(from string cast in imdb.Cast
            //            select new Actor(cast)).ToList();
            return mov;
        }

        //public List<string> GetMovieUrls(string name)
        //{
        //    var listUrls = ImdbMovScraped.getIMDbUrl(name);
        //    var result = listUrls.Cast<string>().ToList();
        //    return result;
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }
}

