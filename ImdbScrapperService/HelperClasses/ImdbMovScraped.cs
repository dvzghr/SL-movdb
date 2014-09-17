using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


/*******************************************************************************
 * Free ASP.net IMDb Scraper API for the new IMDb Template.
 * Author: Abhinay Rathore
 * Website: http://www.AbhinayRathore.com
 * Blog: http://web3o.blogspot.com
 * More Info: http://web3o.blogspot.com/2010/11/aspnetc-imdb-scraping-api.html
 * Last Updated: Feb 20, 2013
 *******************************************************************************/

namespace ImdbScrapperService.HelperClasses
{
    public class ImdbMovScraped
    {
        public bool Status { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Year { get; set; }
        public string Rating { get; set; }
        public ArrayList Genres { get; set; }
        public ArrayList Directors { get; set; }
        public ArrayList Writers { get; set; }
        public ArrayList Stars { get; set; }
        public ArrayList Cast { get; set; }
        public ArrayList Producers { get; set; }
        public ArrayList Musicians { get; set; }
        public ArrayList Cinematographers { get; set; }
        public ArrayList Editors { get; set; }
        public string MpaaRating { get; set; }
        public string ReleaseDate { get; set; }
        public string Plot { get; set; }
        public ArrayList PlotKeywords { get; set; }
        public string Poster { get; set; }
        public string PosterLarge { get; set; }
        public string PosterFull { get; set; }
        public string Runtime { get; set; }
        public string Top250 { get; set; }
        public string Oscars { get; set; }
        public string Awards { get; set; }
        public string Nominations { get; set; }
        public string Storyline { get; set; }
        public string Tagline { get; set; }
        public string Votes { get; set; }
        public ArrayList Languages { get; set; }
        public ArrayList Countries { get; set; }
        //public ArrayList ReleaseDates { get; set; }
        public ArrayList MediaImages { get; set; }
        //public ArrayList RecommendedTitles { get; set; }
        public string ImdbUrl { get; set; }

        //Search Engine URLs
        //private static string GoogleSearch = "http://www.google.com/search?q=imdb+";
        //private static string BingSearch = "http://www.bing.com/search?q=imdb+";
        //private static string AskSearch = "http://www.ask.com/web?q=imdb+";

        //Constructor
        //public IMDb(string MovieName, bool GetExtraInfo = true)
        //{
        //    var imdbUrl = getIMDbUrl(System.Uri.EscapeUriString(MovieName));
        //    status = false;
        //    if (!string.IsNullOrEmpty(imdbUrl))
        //    {
        //        string html = getUrlData(imdbUrl);
        //        parseIMDbPage(html, GetExtraInfo);
        //    }
        //}

        public ImdbMovScraped(string imdbUrl, bool getExtraInfo = true)
        {
            var html = GetUrlData(imdbUrl + "combined");
            ParseImdbPage(html, getExtraInfo);
        }

        //Get IMDb URL from search results
        //static public ArrayList getIMDbUrl(string MovieName, string searchEngine = "google")
        //{
        //    string url = GoogleSearch + MovieName; //default to Google search
        //    if (searchEngine.ToLower().Equals("bing")) url = BingSearch + MovieName;
        //    if (searchEngine.ToLower().Equals("ask")) url = AskSearch + MovieName;
        //    string html = getUrlData(url);
        //    ArrayList imdbUrls = matchAll(@"<a href=""(http://www.imdb.com/title/tt\d{7}/)"".*?>.*?</a>", html);
        //    if (imdbUrls.Count > 0)
        //        return imdbUrls; //return first IMDb result
        //    else if (searchEngine.ToLower().Equals("google")) //if Google search fails
        //        return getIMDbUrl(MovieName, "bing"); //search using Bing
        //    else if (searchEngine.ToLower().Equals("bing")) //if Bing search fails
        //        return getIMDbUrl(MovieName, "ask"); //search using Ask
        //    else //search fails
        //        return new ArrayList {string.Empty};
        //}

        //Parse IMDb page data
        private void ParseImdbPage(string html, bool getExtraInfo)
        {
            Id = match(@"<link rel=""canonical"" href=""http://www.imdb.com/title/(tt\d{7})/combined"" />", html);
            if (!string.IsNullOrEmpty(Id))
            {
                Status = true;
                Title = match(@"<title>(IMDb \- )*(.*?) \(.*?</title>", html, 2);
                OriginalTitle = match(@"title-extra"">(.*?)<", html);
                Year = match(@"<title>.*?\(.*?(\d{4}).*?\).*?</title>", html);
                Rating = match(@"<b>(\d.\d)/10</b>", html);
                Genres = matchAll(@"<a.*?>(.*?)</a>", match(@"Genre.?:(.*?)(</div>|See more)", html));
                Directors = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Directed by</a></h5>(.*?)</table>", html));
                Writers = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Writing credits</a></h5>(.*?)</table>", html));
                Producers = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Produced by</a></h5>(.*?)</table>", html));
                Musicians = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Original Music by</a></h5>(.*?)</table>", html));
                Cinematographers = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Cinematography by</a></h5>(.*?)</table>", html));
                Editors = matchAll(@"<td valign=""top""><a.*?href=""/name/.*?/"">(.*?)</a>", match(@"Film Editing by</a></h5>(.*?)</table>", html));
                Cast = matchAll(@"<td class=""nm""><a.*?href=""/name/.*?/"".*?>(.*?)</a>", match(@"<h3>Cast</h3>(.*?)</table>", html));
                Plot = match(@"Plot:</h5>.*?<div class=""info-content"">(.*?)(<a|</div)", html);
                PlotKeywords = matchAll(@"<a.*?>(.*?)</a>", match(@"Plot Keywords:</h5>.*?<div class=""info-content"">(.*?)</div", html));
                ReleaseDate = match(@"Release Date:</h5>.*?<div class=""info-content"">.*?(\d{1,2} (January|February|March|April|May|June|July|August|September|October|November|December) (19|20)\d{2})", html);
                Runtime = match(@"Runtime:</h5><div class=""info-content"">(\d{1,4}) min[\s]*.*?</div>", html);
                Top250 = match(@"Top 250: #(\d{1,3})<", html);
                Oscars = match(@"Won (\d+) Oscars?\.", html);
                if (string.IsNullOrEmpty(Oscars) && "Won Oscar.".Equals(match(@"(Won Oscar\.)", html))) Oscars = "1";
                Awards = match(@"(\d{1,4}) wins", html);
                Nominations = match(@"(\d{1,4}) nominations", html);
                Tagline = match(@"Tagline:</h5>.*?<div class=""info-content"">(.*?)(<a|</div)", html);
                MpaaRating = match(@"MPAA</a>:</h5><div class=""info-content"">Rated (G|PG|PG-13|PG-14|R|NC-17|X) ", html);
                Votes = match(@">(\d+,?\d*) votes<", html);
                Languages = matchAll(@"<a.*?>(.*?)</a>", match(@"Language.?:(.*?)(</div>|>.?and )", html));
                Countries = matchAll(@"<a.*?>(.*?)</a>", match(@"Country:(.*?)(</div>|>.?and )", html));
                Poster = match(@"<div class=""photo"">.*?<a name=""poster"".*?><img.*?src=""(.*?)"".*?</div>", html);
                if (!string.IsNullOrEmpty(Poster) && Poster.IndexOf("media-imdb.com") > 0)
                {
                    Poster = Regex.Replace(Poster, @"_V1.*?.jpg", "_V1._SY200.jpg");
                    PosterLarge = Regex.Replace(Poster, @"_V1.*?.jpg", "_V1._SY500.jpg");
                    PosterFull = Regex.Replace(Poster, @"_V1.*?.jpg", "_V1._SY0.jpg");
                }
                else
                {
                    Poster = string.Empty;
                    PosterLarge = string.Empty;
                    PosterFull = string.Empty;
                }
                ImdbUrl = "http://www.imdb.com/title/" + Id + "/";

                //Get Stars
                var html2 = GetUrlData(ImdbUrl);
                Stars = matchAll(@"<a.*?><span class=""itemprop"" itemprop=""name"">(.*?)</span></a>", match(@"Stars?:(.*?)</div>", html2));

                if (getExtraInfo)
                {
                    string plotHtml = GetUrlData(ImdbUrl + "plotsummary");
                    Storyline = StripHtml(match(@"<p class=""plotSummary"">(.*?)(<i>|</p>)", plotHtml));
                    //ReleaseDates = GetReleaseDates();
                    MediaImages = GetMediaImages();
                    //RecommendedTitles = GetRecommendedTitles();
                }
            }
        }

        //Get all media images
        private ArrayList GetMediaImages()
        {
            ArrayList list = new ArrayList();
            string mediaurl = "http://www.imdb.com/title/" + Id + "/mediaindex";
            string mediahtml = GetUrlData(mediaurl);
            //int pagecount = matchAll(@"<a href=""\?page=(.*?)"">", match(@"<span style=""padding: 0 1em;"">(.*?)</span>", mediahtml)).Count;
            //for (int p = 1; p <= pagecount + 1; p++)
            {
                //mediahtml = GetUrlData(mediaurl + "?page=" + p);
                foreach (Match m in new Regex(@"src=""(.*?)""", RegexOptions.Multiline).Matches(match(@"<div class=""media_index_thumb_list"" id=""media_index_thumbnail_grid"">(.*?)</div>", mediahtml)))
                {
                    String image = m.Groups[1].Value;
                    list.Add(Regex.Replace(image, @"_V1_.*?.jpg", "_V1__SX640_SY720_.jpg"));
                }
            }
            return list;
        }

        /*******************************[ Helper Methods ]********************************/

        //Match single instance
        private string match(string regex, string html, int i = 1)
        {
            return new Regex(regex, RegexOptions.Multiline).Match(html).Groups[i].Value.Trim();
        }

        //Match all instances and return as ArrayList
        private static ArrayList matchAll(string regex, string html, int i = 1)
        {
            var list = new ArrayList();
            foreach (Match m in new Regex(regex, RegexOptions.Multiline).Matches(html))
                list.Add(m.Groups[i].Value.Trim());
            return list;
        }

        //Strip HTML Tags
        static string StripHtml(string inputString)
        {
            return Regex.Replace(inputString, @"<.*?>", string.Empty);
        }

        //Get URL Data
        private static string GetUrlData(string url)
        {
            var client = new WebClient();
            client.Proxy = new WebProxy("http://192.168.255.5:8080", true);
            var r = new Random();
            //Random IP Address
            client.Headers["X-Forwarded-For"] = r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255);
            //Random User-Agent
            client.Headers["User-Agent"] = "Mozilla/" + r.Next(3, 5) + ".0 (Windows NT " + r.Next(3, 5) + "." + r.Next(0, 2) + "; rv:2.0.1) Gecko/20100101 Firefox/" + r.Next(3, 5) + "." + r.Next(0, 5) + "." + r.Next(0, 5);
            var datastream = client.OpenRead(url);
            var reader = new StreamReader(datastream);
            var sb = new StringBuilder();
            while (!reader.EndOfStream)
                sb.Append(reader.ReadLine());
            return sb.ToString();
        }
    }
}