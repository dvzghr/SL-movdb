using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using DataContracts;
using MovieDb.Model.HelperClasses;
using MovieDb.Model.ImdbScrapperServiceReference;

namespace MovieDb.Model
{
    //[Export(typeof(IServiceAgent))]
    public class MockServiceAgent : IServiceAgent
    {
        public void GetMovie(string name, Action<ImdbMovie, Exception> userCallback)
        {
            var mov = new ImdbMovie
                          {
                              Id = "tt1375666",
                              Title = "Inception",
                              OriginalTitle = "Inception",
                              Year = 2010,
                              Rating = 8.9,
                              Genres = new List<string> { "Action", "Adventure", "Sci-Fi", "Thriller" },
                              Directors = new List<string> { "Christopher Nolan" },
                              Writers = new List<string> { "Christopher Nolan" },
                              Cast = new List<Actor>
                                         {
                                             new Star ("Leonardo DiCaprio"),
                                             new Star("Joseph Gordon-Levitt"),
                                             new Star("Ellen Page"),
                                             new Actor("Tom Hardy"),
                                             new Actor("Ken Watanabe"),
                                             new Actor("Dileep Rao"),
                                             new Actor("Cillian Murphy"),
                                             new Actor("Tom Berenger"),
                                             new Actor("Marion Cotillard"),
                                             new Actor("Pete Postlethwaite"),
                                             new Actor("Michael Caine"),
                                             new Actor("Lukas Haas"),
                                             new Actor("Tai-Li Lee"),
                                             new Actor("Claire Geare"),
                                             new Actor("Magnus Nolan")
                                         },
                              MpaaRating = "PG_13",
                              ReleaseDate = Convert.ToDateTime("16 July 2010"),
                              Plot = "In a world where technology exists to enter the human mind through dream invasion, a highly skilled thief is given a final chance at redemption which involves executing his toughest job to date: Inception.",
                              Poster = "/img/poster.jpg",
                              PosterLarge = "/img/posterlarge.jpg",
                              PosterFull = "/img/posterfull.jpg",
                              //Poster = @"http://ia.media-imdb.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1._SY317_.jpg",
                              //PosterFull = @"http://ia.media-imdb.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1._SY0.jpg",
                              //PosterLarge = @"http://ia.media-imdb.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1._SY500.jpg",
                              //PosterSmall = @"http://ia.media-imdb.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1._SY150.jpg",
                              Runtime = 148,
                              Top250 = 9,
                              Oscars = 4,
                              Awards = 69,
                              Nominations = 97,
                              Storyline = "Dom Cobb is a skilled thief, the absolute best in the dangerous art of extraction, stealing valuable secrets from deep within the subconscious during the dream state, when the mind is at its most vulnerable. Cobb's rare ability has made him a coveted player in this treacherous new world of corporate espionage, but it has also made him an international fugitive and cost him everything he has ever loved. Now Cobb is being offered a chance at redemption. One last job could give him his life back but only if he can accomplish the impossible-inception. Instead of the perfect heist, Cobb and his team of specialists have to pull off the reverse: their task is not to steal an idea but to plant one. If they succeed, it could be the perfect crime. But no amount of careful planning or expertise can prepare the team for the dangerous enemy that seems to predict their every move. An enemy that only Cobb could have seen coming.",
                              Tagline = "Your mind is the scene of the crime",
                              Votes = 393458,
                              ImdbUrl = "http://www.imdb.com/title/tt1375666/",
                              MediaImages = new List<string> { "/img/media01.jpg", "/img/media02.jpg", "/img/media03.jpg", "/img/media04.jpg", "/img/media05.jpg", "/img/media06.jpg", "/img/media07.jpg", "/img/media08.jpg", "/img/media09.jpg" }
                              //MediaImages = new ObservableCollection<string> { "http://ia.media-imdb.com/images/M/MV5BMjIzNjc5NTMwM15BMl5BanBnXkFtZTcwMjQyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMjI0MTg3MzI0M15BMl5BanBnXkFtZTcwMzQyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMjI1NjM2MDMxMF5BMl5BanBnXkFtZTcwNDQyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTg5MTM1NDk4NF5BMl5BanBnXkFtZTcwNTQyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTc3NTYyMTkyMl5BMl5BanBnXkFtZTcwNjQyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTMyODk0MDUyOF5BMl5BanBnXkFtZTcwNzQyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTU0NTMwNzMxNl5BMl5BanBnXkFtZTcwODQyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTExNDg3NjMyMTBeQTJeQWpwZ15BbWU3MDk0Mjg1NjM@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTYyODA1OTI4OV5BMl5BanBnXkFtZTcwMDUyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTQzMTUzNjc4Nl5BMl5BanBnXkFtZTcwMTUyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMjE2MTI1MjA3MF5BMl5BanBnXkFtZTcwMjUyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTI4MDA0OTY5MF5BMl5BanBnXkFtZTcwMzUyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTA2ODkxOTc5NzZeQTJeQWpwZ15BbWU3MDQ1Mjg1NjM@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMjE5NTk4ODM5NV5BMl5BanBnXkFtZTcwNTUyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTc4MTA5OTU1NF5BMl5BanBnXkFtZTcwNjUyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BNTAzNjQ1NTkwNV5BMl5BanBnXkFtZTcwNzUyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTk0NzMzMDY1OF5BMl5BanBnXkFtZTcwODUyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTk4Njg2NjM5OV5BMl5BanBnXkFtZTcwOTUyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTI4MzEyMjEyM15BMl5BanBnXkFtZTcwMDYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTM3NjQzMzI2MF5BMl5BanBnXkFtZTcwMTYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BNDMzNjk4NDUxNV5BMl5BanBnXkFtZTcwMjYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BNTA3MzU0NjE5MV5BMl5BanBnXkFtZTcwMzYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTI4MDQ3Nzc2Ml5BMl5BanBnXkFtZTcwNDYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMjIxMzE0OTY4Ml5BMl5BanBnXkFtZTcwNTYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMjA1NTUxNzcxNV5BMl5BanBnXkFtZTcwNjYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTMxMzYzMjg0MF5BMl5BanBnXkFtZTcwNzYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTM2MTA4OTA0MV5BMl5BanBnXkFtZTcwODYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMjI3NDk1NTUzNV5BMl5BanBnXkFtZTcwOTYyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMjE1MzA3Njg1MF5BMl5BanBnXkFtZTcwMDcyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BNzYwNjQ5MDkxNF5BMl5BanBnXkFtZTcwMTcyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTg4ODA0NjE1MF5BMl5BanBnXkFtZTcwMjcyODU2Mw@@._V1._SY500.jpg", "http://ia.media-imdb.com/images/M/MV5BMTM1NzQyMTE4OF5BMl5BanBnXkFtZTcwMzcyODU2Mw@@._V1._SY500.jpg" }

                          };
            userCallback(mov, null);
        }

        public void GetMovieUrls(string name, Action<List<GoogleSearchResult>, Exception> userCallback)
        {
            userCallback(new List<GoogleSearchResult>
                             {
                                 new GoogleSearchResult{Url = @"http://www.imdb.com/title/tt0462499/",ThumbshotsUrl = "/img/googleresultInception01.jpg",Content = "Directed by Christopher Nolan. With Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe. In a world where technology exists to enter the ...",TitleNoFormatting = "Inception (2010) - IMDb"},
                                 new GoogleSearchResult{Url = @"http://www.imdb.com/title/tt0999999/",ThumbshotsUrl = "/img/googleresultInception02.jpg",Content = "With his breakthrough performance as Eames in Christopher Nolan's science fiction thriller Inception, English actor Tom Hardy has been brought to the attention ...",TitleNoFormatting = "Inception Featurette (Inception: Behind the Scenes) - IMDb"},
                                 new GoogleSearchResult{Url = @"http://www.imdb.com/title/tt0455533/",ThumbshotsUrl = "/img/googleresultempty.jpg",Content = "Was the end a dream or reality? Although no one can prove it one way or another, unless that person is Chris Nolan, it is hard to say. But the more likely.",TitleNoFormatting = "IMDb – My theory of Inception's ending (Spoilers) | Inception Ending"},
                             }, null);
        }

        public void GetMovies(Action<IEnumerable<ImdbMovie>, Exception> userCallback)
        {
            //throw new NotImplementedException();
        }

        public void InsertMovie(ImdbMovie newMovie, Action<OperationStatus, Exception> userCallback)
        {
            //throw new NotImplementedException();
        }

        public void UpdateMovie(ImdbMovie updMovie, Action<OperationStatus, Exception> userCallback)
        {
            //throw new NotImplementedException();
        }

        public void DeleteMovie(ImdbMovie delMovie, Action<OperationStatus, Exception> userCallback)
        {
            //throw new NotImplementedException();
        }
    }
}