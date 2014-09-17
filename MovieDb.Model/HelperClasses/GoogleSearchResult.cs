using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MovieDb.Model.HelperClasses
{
    public class GoogleSearchResult
    {
        const string HtmlTagPattern = "<.*?>";

        public string UnescapedUrl { get; set; }
        public string Url { get; set; }
        public string VisibleUrl { get; set; }
        public string TitleNoFormatting { get; set; }
        string _content;
        public string Content
        {
            get { return StripHtml(_content); }
            set { _content = value; }
        }

        private bool _isDesignTime;
        private string _thumbshotsUrl = "http://img.bitpixels.com/getthumbnail?code=20080&size=200&url=";
        //private string _thumbshotsUrl = "http://images.shrinktheweb.com/xino.php?stwembed=1&stwaccesskeyid=adfdb262f89c62a&stwsize=lg&stwurl=";  //ONLY ROOT
        //private string _thumbshotsUrl = "http://images.thumbshots.com/image.aspx?cid=0W79ApN9SUY%3d&v=1&w=240&url=";  //ONLY ROOT
        //private string _thumbshotsUrl = "http://picoshot.com/t.php?picurl=";
        //private string _thumbshotsUrl = "http://www.thumbshots.de/cgi-bin/show.cgi?url=";
        public string ThumbshotsUrl
        {
            get { return _thumbshotsUrl; }
            set
            {
                _thumbshotsUrl = value;
                _isDesignTime = true;
            }
        }

        public string ThumbnailUrl
        {
            get { return _isDesignTime ? ThumbshotsUrl : ThumbshotsUrl + Url; }
        }

        static string StripHtml(string inputString)
        {
            return Regex.Replace
              (inputString, HtmlTagPattern, string.Empty);
        }
    }
}
