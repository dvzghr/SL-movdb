using System;
using System.Windows.Media.Imaging;
using DataContracts;
using GalaSoft.MvvmLight;
using MovieDb.Model.HelperClasses;
using MovieDb.Model.ImdbScrapperServiceReference;

namespace MovieDb.ViewModel
{
    public class SingleMovieViewModel : ViewModelBase
    {
        public SingleMovieViewModel(ImdbMovie singleMovie)
        {
            SingleMovie = singleMovie;
            SingleMovie.PropertyChanged += (s, e) => { IsDirty = true; };
        }

        public ImdbMovie SingleMovie { get; private set; }

        private bool _isDirty;
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    RaisePropertyChanged("IsDirty");
                }
            }
        }

        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                if (_isNew != value)
                {
                    _isNew = value;
                    RaisePropertyChanged("IsNew");
                }
            }
        }

        private bool _isDel;
        public bool IsDel
        {
            get { return _isDel; }
            set
            {
                if (_isDel != value)
                {
                    _isDel = value;
                    RaisePropertyChanged("IsDel");
                }
            }
        }
    }
}
