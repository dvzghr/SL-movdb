using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ImdbScrapperService.HelperClasses
{
    [DataContract]
    public class ImdbMovie
    {
        [DataMember]
        public byte Awards { get; set; }
        [DataMember]
        public List<Actor> Cast { get; set; }
        [DataMember]
        public List<string> Directors { get; set; }
        [DataMember]
        public List<string> Genres { get; set; }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string ImdbUrl { get; set; }
        [DataMember]
        public List<string> MediaImages { get; set; }
        [DataMember]
        public string MpaaRating { get; set; }
        [DataMember]
        public byte Nominations { get; set; }
        [DataMember]
        public string OriginalTitle { get; set; }
        [DataMember]
        public byte Oscars { get; set; }
        [DataMember]
        public string Plot { get; set; }
        [DataMember]
        public string Poster { get; set; }
        [DataMember]
        public string PosterFull { get; set; }
        [DataMember]
        public string PosterLarge { get; set; }
        [DataMember]
        public string PosterSmall { get; set; }
        [DataMember]
        public double Rating { get; set; }
        [DataMember]
        public DateTime? ReleaseDate { get; set; }
        [DataMember]
        public short Runtime { get; set; }
        [DataMember]
        public List<Star> Stars { get; set; }
        [DataMember]
        public string Storyline { get; set; }
        [DataMember]
        public string Tagline { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public byte Top250 { get; set; }
        [DataMember]
        public uint Votes { get; set; }
        [DataMember]
        public List<string> Writers { get; set; }
        [DataMember]
        public short Year { get; set; }

        public static explicit operator ImdbMovie(ImdbMovScraped imDb)
        {
            var mov = new ImdbMovie
                       {
                           Awards = string.IsNullOrWhiteSpace(imDb.Awards) ? (byte)0 : Convert.ToByte(imDb.Awards),
                           Directors = imDb.Directors.Cast<string>().ToList(),
                           Genres = imDb.Genres.Cast<string>().ToList(),
                           Id = imDb.Id,
                           ImdbUrl = imDb.ImdbUrl,
                           MediaImages = imDb.MediaImages.Cast<string>().ToList(),
                           MpaaRating = imDb.MpaaRating,
                           Nominations = string.IsNullOrWhiteSpace(imDb.Nominations) ? (byte)0 : Convert.ToByte(imDb.Nominations),
                           OriginalTitle = imDb.OriginalTitle,
                           Oscars = string.IsNullOrWhiteSpace(imDb.Oscars) ? (byte)0 : Convert.ToByte(imDb.Oscars),
                           Plot = imDb.Plot,
                           Poster = imDb.Poster,
                           PosterFull = imDb.PosterFull,
                           PosterLarge = imDb.PosterLarge,
                           PosterSmall = imDb.PosterSmall,
                           Rating = Double.Parse(imDb.Rating, NumberFormatInfo.InvariantInfo),
                           ReleaseDate = string.IsNullOrWhiteSpace(imDb.ReleaseDate) ? (DateTime?)null : Convert.ToDateTime(imDb.ReleaseDate),
                           Runtime = Convert.ToInt16(imDb.Runtime),
                           Storyline = imDb.Storyline,
                           Tagline = imDb.Tagline,
                           Title = string.IsNullOrEmpty(imDb.OriginalTitle) ? imDb.Title : imDb.OriginalTitle,
                           Top250 = string.IsNullOrWhiteSpace(imDb.Top250) ? (byte)0 : Convert.ToByte(imDb.Top250),
                           Votes = Convert.ToUInt32(imDb.Votes.Replace(",", "")),
                           Writers = imDb.Writers.Cast<string>().ToList(),
                           Year = Convert.ToInt16(imDb.Year)
                       };

            var actors = (from string star in imDb.Stars
                         select new Star(star)).Cast<Actor>().ToList();

            //actors.AddRange(from object cast in imDb.Cast
            //               where !imDb.Stars.Contains(cast)
            //               select new Actor(cast.ToString()));
            mov.Cast = actors;
            return mov;
        }
    }

    [DataContract]
    public class Actor
    {
        public Actor(string name)
        {
            FullName = name;
        }

        [DataMember]
        public string FullName { get; set; }
    }

    [DataContract]
    public class Star : Actor
    {
        public Star(string name) : base(name) { }
    }
}
