using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DataContracts;
using MovieDb.Model.ImdbScrapperServiceReference;

namespace MovieDb.Model.HelperClasses
{
    public interface IServiceAgent
    {
        void GetMovie(string name, Action<ImdbMovie, Exception> userCallback);
        void GetMovieUrls(string name, Action<List<GoogleSearchResult>, Exception> userCallback);
        void GetMovies(Action<IEnumerable<ImdbMovie>, Exception> userCallback);
        void InsertMovie(ImdbMovie newMovie, Action<OperationStatus, Exception> userCallback);
        void UpdateMovie(ImdbMovie updMovie, Action<OperationStatus, Exception> userCallback);
        void DeleteMovie(ImdbMovie delMovie, Action<OperationStatus, Exception> userCallback);
    }
}
