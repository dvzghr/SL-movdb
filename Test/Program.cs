using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using IMDb_Scraper;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            GetMovieInfoImdbScrapping();
            //GetMovieInfoUrl();
        }

        private static void GetMovieInfoUrl()
        {
            Console.Write("Imdb url:");

            const string key = "bbccdb8024a0ffe87d397d94b083b691";
            var url = "tt1375666";

            var serviceUri =
                new Uri(string.Format("http://api.themoviedb.org/2.1/Movie.imdbLookup/en/xml/{0}/{1}", key, url));

            var client = new WebClient();
            client.OpenReadCompleted += ClientOpenReadCompleted;
            client.OpenReadAsync(serviceUri);
       }

        static void ClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if(e.Error!=null)
            {
                Console.WriteLine(e.Error.Message);
                return;
            }

            var responseStream = e.Result;

            var responseReader = XmlReader.Create(responseStream);
            var mov = XElement.Load(responseReader);

            var name = (string)mov.Element("movies").Element("movie").Element("name");

            Console.WriteLine(name);
            Console.WriteLine("\nPress Enter...");
            Console.ReadLine();


        }

        private static void GetMovieInfoImdbScrapping()
        {
            Console.Write("Movie Title: ");
            var movTitle = Console.ReadLine();

            var mov = new IMDb(movTitle, false);

            Console.WriteLine(mov.ImdbURL);
            Console.WriteLine("\nPress Enter...");
            Console.ReadLine();
        }

        private static void ConvertEncoding(Encoding unicode, Encoding ascii)
        {
            string storyline = "Cobb&#x27;s rare ability";
            var sourceBytes = ascii.GetBytes(storyline);
            var destBytes = Encoding.Convert(ascii, unicode, sourceBytes);

            var destChars = new char[unicode.GetCharCount(destBytes, 0, destBytes.Length)];
            unicode.GetChars(destBytes, 0, destBytes.Length, destChars, 0);
            var newString = new string(destChars);

            Console.WriteLine("\n-------------------------------");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(unicode.EncodingName);
            Console.WriteLine(ascii.EncodingName);
            Console.WriteLine("===============================");
            Console.WriteLine(newString);
        }
    }
}
