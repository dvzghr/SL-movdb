using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace DataContracts
{
    [DataContract]
    public class ImdbMovie : INotifyPropertyChanged
    {
        #region Properties
        private byte _awards;
        [DataMember]
        public byte Awards
        {
            get { return _awards; }
            set
            {
                if (_awards == value) return;
                _awards = value;
                RaisePropertyChanged("Awards");
            }
        }

        private List<Actor> _cast;
        [DataMember]
        public List<Actor> Cast
        {
            get { return _cast; }
            set
            {
                {
                    if (_cast == value) return;
                    _cast = value;
                    RaisePropertyChanged("Cast");
                    RaisePropertyChanged("CastTop5");
                }
            }
        }
        public List<Actor> CastTop15 { get { return _cast.Take(15).ToList(); } }

        private List<string> _countries;
        [DataMember]
        public List<string> Countries
        {
            get { return _countries; }
            set
            {
                if (_countries == value) return;
                _countries = value;
                RaisePropertyChanged("Countries");
            }
        }

        private List<string> _directors;
        [DataMember]
        public List<string> Directors
        {
            get { return _directors; }
            set
            {
                if (_directors == value) return;
                _directors = value;
                RaisePropertyChanged("Directors");
            }
        }

        private List<string> _genres;
        [DataMember]
        public List<string> Genres
        {
            get { return _genres; }
            set
            {
                if (_genres == value) return;
                _genres = value;
                RaisePropertyChanged("Genres");
            }
        }

        private string _id;
        [DataMember]
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        private string _imdbUrl;
        [DataMember]
        public string ImdbUrl
        {
            get { return _imdbUrl; }
            set
            {
                if (_imdbUrl == value) return;
                _imdbUrl = value;
                RaisePropertyChanged("ImdbUrl");
            }
        }

        private List<string> _mediaImages;
        [DataMember]
        public List<string> MediaImages
        {
            get { return _mediaImages; }
            set
            {
                if (_mediaImages == value) return;
                _mediaImages = value;
                RaisePropertyChanged("MediaImages");
            }
        }

        private string _mpaaRating;
        [DataMember]
        public string MpaaRating
        {
            get { return _mpaaRating; }
            set
            {
                if (_mpaaRating == value) return;
                _mpaaRating = value;
                RaisePropertyChanged("MpaaRating");
            }
        }

        private byte _nominations;
        [DataMember]
        public byte Nominations
        {
            get { return _nominations; }
            set
            {
                if (_nominations == value) return;
                _nominations = value;
                RaisePropertyChanged("Nominations");
            }
        }

        private string _originalTitle;
        [DataMember]
        public string OriginalTitle
        {
            get { return _originalTitle; }
            set
            {
                if (_originalTitle == value) return;
                _originalTitle = value;
                RaisePropertyChanged("OriginalTitle");
            }
        }

        private byte _oscars;
        [DataMember]
        public byte Oscars
        {
            get { return _oscars; }
            set
            {
                if (_oscars == value) return;
                _oscars = value;
                RaisePropertyChanged("Oscars");
                RaisePropertyChanged("OscarsArray");
            }
        }

        [DataMember]
        public object[] OscarsArray
        {
            get { return new object[_oscars]; }
        }

        private string _plot;
        [DataMember]
        public string Plot
        {
            get { return _plot; }
            set
            {
                if (_plot == value) return;
                _plot = value;
                RaisePropertyChanged("Plot");
            }
        }

        private string _poster;
        [DataMember]
        public string Poster
        {
            get { return _poster; }
            set
            {
                if (_poster == value) return;
                _poster = value;
                RaisePropertyChanged("Poster");
            }
        }

        private string _posterFull;
        [DataMember]
        public string PosterFull
        {
            get { return _posterFull; }
            set
            {
                if (_posterFull == value) return;
                _posterFull = value;
                RaisePropertyChanged("PosterFull");
            }
        }

        private string _posterLarge;
        [DataMember]
        public string PosterLarge
        {
            get { return _posterLarge; }
            set
            {
                if (_posterLarge == value) return;
                _posterLarge = value;
                RaisePropertyChanged("PosterLarge");
            }
        }

        private double _rating;
        [DataMember]
        public double Rating
        {
            get { return _rating; }
            set
            {
                if (_rating == value) return;
                _rating = value;
                RaisePropertyChanged("Rating");
            }
        }

        private DateTime? _releaseDate;
        [DataMember]
        public DateTime? ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                if (_releaseDate == value) return;
                _releaseDate = value;
                RaisePropertyChanged("ReleaseDate");
            }
        }

        private short _runtime;
        [DataMember]
        public short Runtime
        {
            get { return _runtime; }
            set
            {
                if (_runtime == value) return;
                _runtime = value;
                RaisePropertyChanged("Runtime");
            }
        }

        //[DataMember]
        //public List<Star> Stars { get; set; }

        private string _storyline;
        [DataMember]
        public string Storyline
        {
            get { return _storyline; }
            set
            {
                if (_storyline == value) return;
                _storyline = value;
                RaisePropertyChanged("Storyline");
            }
        }

        private string _tagline;
        [DataMember]
        public string Tagline
        {
            get { return _tagline; }
            set
            {
                if (_tagline == value) return;
                _tagline = value;
                RaisePropertyChanged("Tagline");
            }
        }

        private string _title;
        [DataMember]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value) return;
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private byte _top250;
        [DataMember]
        public byte Top250
        {
            get { return _top250; }
            set
            {
                if (_top250 == value) return;
                _top250 = value;
                RaisePropertyChanged("Top250");
            }
        }

        private uint _votes;
        [DataMember]
        public uint Votes
        {
            get { return _votes; }
            set
            {
                if (_votes == value) return;
                _votes = value;
                RaisePropertyChanged("Votes");
            }
        }

        private List<string> _writers;
        [DataMember]
        public List<string> Writers
        {
            get { return _writers; }
            set
            {
                if (_writers == value) return;
                _writers = value;
                RaisePropertyChanged("Writers");
            }
        }

        private short _year;
        [DataMember]
        public short Year
        {
            get { return _year; }
            set
            {
                if (_year == value) return;
                _year = value;
                RaisePropertyChanged("Year");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [DataContract]
    [KnownType(typeof(Star))]
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
