using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Windows.Browser;
using System.Xml;
using DataContracts;
using MovieDb.Model.HelperClasses;
using MovieDb.Model.ImdbScrapperServiceReference;

namespace MovieDb.Model
{


    [Export(typeof(IServiceAgent))]
    public class ServiceAgent : IServiceAgent
    {
        private Service1Client _imdbScrapperClient = new Service1Client();
        private WebClient _webClient = new WebClient();


        #region GetMovie
        public void GetMovie(string name, Action<ImdbMovie, Exception> userCallback)
        {
            _imdbScrapperClient.GetMovieCompleted += GetMovieCompleted;
            _imdbScrapperClient.GetMovieAsync(name, userCallback);
        }
        private void GetMovieCompleted(object sender, GetMovieCompletedEventArgs e)
        {
            _imdbScrapperClient.GetMovieCompleted -= GetMovieCompleted;
            var userCallback = e.UserState as Action<ImdbMovie, Exception>;

            if (userCallback == null) return;

            if (e.Error != null)
            {
                userCallback(null, e.Error);
                return;
            }
            userCallback(e.Result, null);
        }
        #endregion

        #region GetMovieUrls
        private const string GoogleSearchUrl = "http://ajax.googleapis.com/ajax/services/search/web?v={0}&q={1}&rsz={2}&start={3}&as_sitesearch=www.imdb.com";
        private const string GoogleSerachApiVersion = "1.0";
        private const string ResultSize = "large";
        private const int PageSize = 8;
        private const int StartIndex = 0;

        public void GetMovieUrls(string name, Action<List<GoogleSearchResult>, Exception> userCallback)
        {

            _webClient.OpenReadCompleted += GetMovieUrlsCompleted;
            _webClient.OpenReadAsync(new Uri(string.Format(GoogleSearchUrl, GoogleSerachApiVersion, HttpUtility.UrlEncode(name), ResultSize, StartIndex)), userCallback);
        }
        private void GetMovieUrlsCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            _webClient.OpenReadCompleted -= GetMovieUrlsCompleted;
            var userCallback = e.UserState as Action<List<GoogleSearchResult>, Exception>;

            if (userCallback == null) return;

            if (e.Error != null)
            {
                userCallback(null, e.Error);
                return;
            }

            var resultList = new List<GoogleSearchResult>();

            XmlReader reader = JsonReaderWriterFactory.CreateJsonReader(e.Result, XmlDictionaryReaderQuotas.Max);

            while (reader.Read())
            {
                //string xml = reader.ReadOuterXml();
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "cursor") break;
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "item")
                {
                    var resultItem = new GoogleSearchResult();
                    reader.ReadToDescendant("unescapedUrl");
                    resultItem.UnescapedUrl = reader.ReadElementContentAsString();
                    reader.MoveToContent();
                    resultItem.Url = reader.ReadElementContentAsString();
                    reader.MoveToContent();
                    resultItem.VisibleUrl = reader.ReadElementContentAsString();
                    reader.ReadToFollowing("titleNoFormatting");
                    reader.MoveToContent();
                    resultItem.TitleNoFormatting = reader.ReadElementContentAsString();
                    reader.MoveToContent();
                    resultItem.Content = reader.ReadElementContentAsString();

                    var match = new Regex(@"/title/tt\d+/$");

                    if (match.IsMatch(resultItem.Url))
                        resultList.Add(resultItem);
                }
            }

            userCallback(resultList, null);
        }
        #endregion

        public void GetMovies(Action<IEnumerable<ImdbMovie>, Exception> userCallback)
        {

        }

        public void InsertMovie(ImdbMovie newMovie, Action<OperationStatus, Exception> userCallback)
        {

        }

        public void UpdateMovie(ImdbMovie updMovie, Action<OperationStatus, Exception> userCallback)
        {

        }

        public void DeleteMovie(ImdbMovie delMovie, Action<OperationStatus, Exception> userCallback)
        {

        }


        //#region GetHeadNodes
        //public void GetHeadNodes(Action<IEnumerable<HeadNode>, Exception> userCallback)
        //{
        //    //WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
        //    _dataClient.GetHeadNodesCompleted += GetHeadNodesCompleted;
        //    _dataClient.GetHeadNodesAsync(userCallback);
        //}
        //void GetHeadNodesCompleted(object sender, GetHeadNodesCompletedEventArgs e)
        //{
        //    _dataClient.GetHeadNodesCompleted -= GetHeadNodesCompleted;
        //    var userCallback = e.UserState as Action<IEnumerable<HeadNode>, Exception>;

        //    if (userCallback == null) return;

        //    if (e.Error != null)
        //    {
        //        userCallback(null, e.Error);
        //        return;
        //    }
        //    userCallback(e.Result, null);
        //}
        //#endregion

        //#region InsertHeadNode
        //public void InsertHeadNode(HeadNode newHeadNode, Action<OperationStatus, Exception> userCallback)
        //{
        //    //WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
        //    _dataClient.InsertHeadNodeCompleted += InsertHeadNodeCompleted;
        //    _dataClient.InsertHeadNodeAsync(newHeadNode, userCallback);
        //}
        //private void InsertHeadNodeCompleted(object sender, InsertHeadNodeCompletedEventArgs e)
        //{
        //    _dataClient.InsertHeadNodeCompleted -= InsertHeadNodeCompleted;
        //    var userCallback = e.UserState as Action<OperationStatus, Exception>;

        //    if (userCallback == null) return;

        //    if (e.Error != null)
        //    {
        //        userCallback(null, e.Error);
        //        return;
        //    }
        //    userCallback(e.Result, null);
        //}
        //#endregion

        //#region UpdateHeadNode
        //public void UpdateHeadNode(HeadNode updHeadNode, Action<OperationStatus, Exception> userCallback)
        //{
        //    //WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
        //    _dataClient.UpdateHeadNodeCompleted += UpdateHeadNodeCompleted;
        //    _dataClient.UpdateHeadNodeAsync(updHeadNode, userCallback);
        //}
        //private void UpdateHeadNodeCompleted(object sender, UpdateHeadNodeCompletedEventArgs e)
        //{
        //    _dataClient.UpdateHeadNodeCompleted -= UpdateHeadNodeCompleted;
        //    var userCallback = e.UserState as Action<OperationStatus, Exception>;

        //    if (userCallback == null) return;

        //    if (e.Error != null)
        //    {
        //        userCallback(null, e.Error);
        //        return;
        //    }
        //    userCallback(e.Result, null);
        //}
        //#endregion

        //#region DeleteHeadNode
        //public void DeleteHeadNode(HeadNode delHeadNode, Action<OperationStatus, Exception> userCallback)
        //{
        //    //WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
        //    _dataClient.DeleteHeadNodeCompleted += DeleteHeadNodeCompleted;
        //    _dataClient.DeleteHeadNodeAsync(delHeadNode, userCallback);
        //}

        //private void DeleteHeadNodeCompleted(object sender, DeleteHeadNodeCompletedEventArgs e)
        //{
        //    _dataClient.DeleteHeadNodeCompleted -= DeleteHeadNodeCompleted;
        //    var userCallback = e.UserState as Action<OperationStatus, Exception>;

        //    if (userCallback == null) return;

        //    if (e.Error != null)
        //    {
        //        userCallback(null, e.Error);
        //        return;
        //    }
        //    userCallback(e.Result, null);
        //}
        //#endregion

    }
}
