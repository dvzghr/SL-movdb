using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using DataContracts;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MovieDb.HelperClasses;
using MovieDb.Model;
using MovieDb.Model.HelperClasses;
using MovieDb.Model.ImdbScrapperServiceReference;

namespace MovieDb.ViewModel
{
    [Export]
    public class MoviesViewModel : ViewModelBase
    {
        private readonly IServiceAgent _serviceAgent;
        public IDialogService DialogService { get; internal set; }

        public RelayCommand<SelectionChangedEventArgs> GetMovieCommand { get; private set; }
        public RelayCommand GetMovieUrlsCommand { get; private set; }
        public RelayCommand NewMovieCommand { get; private set; }
        public RelayCommand UpdMovieCommand { get; private set; }
        public RelayCommand DelMovieCommand { get; private set; }
        public RelayCommand RefreshCommand { get; private set; }

        //public MoviesViewModel() : this(new MockServiceAgent()) { }

        [ImportingConstructor]
        public MoviesViewModel([Import] IServiceAgent serviceAgent)
        {
            //_serviceAgent = !IsInDesignMode ? serviceAgent : new MockServiceAgent();
            _serviceAgent = serviceAgent;

            GetMovieCommand = new RelayCommand<SelectionChangedEventArgs>(GetMovie);
                
            GetMovieUrlsCommand = new RelayCommand(GetMovieUrls);
            NewMovieCommand = new RelayCommand(NewMovie, () => SelMovie == null);
            UpdMovieCommand = new RelayCommand(UpdMovie, () => SelMovie != null);
            DelMovieCommand = new RelayCommand(DelMovie, () => SelMovie != null);

            RefreshCommand = new RelayCommand(Refresh);
        }

        #region GetData
        private void GetMovie(SelectionChangedEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(SelMovieTitle)) return;
            if (IsBusy) return;
            IsBusy = true;

            var selMovie = eventArgs.AddedItems[0] as GoogleSearchResult;

            _serviceAgent.GetMovie(selMovie.Url, OnGetMovieCallback);
        }

        private void OnGetMovieCallback(ImdbMovie mov, Exception ex)
        {
            IsBusy = false;
            if (ex != null)
            {
                Messenger.Default.Send(ex);
                SelMovie = null;
                return;
            }
            SelMovie = new SingleMovieViewModel(mov);
        }


        private void GetMovieUrls()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            _serviceAgent.GetMovieUrls(SelMovieTitle, OnGetMovieUrlsCallback);
        }
        private void OnGetMovieUrlsCallback(List<GoogleSearchResult> result, Exception exception)
        {
            if (exception != null)
            {
                Messenger.Default.Send(exception);
                return;
            }

            MovieUrls = new ObservableCollection<GoogleSearchResult>(result);

            //var list = new ObservableCollection<SingleMovieViewModel>();
            //foreach (var movie in result)
            //{
            //    list.Add(new SingleMovieViewModel(movie));
            //}

            //Movies = list;

            //NewMovieCommand.RaiseCanExecuteChanged();
            IsBusy = false;
        }
        #endregion

        #region CRUD
        private void NewMovie()
        {
            if (IsBusy)
                return;

            SelMovie = new SingleMovieViewModel(new ImdbMovie())
            {
                IsDirty = true,
                IsNew = true,
            };
            Movies.Add(SelMovie);
        }

        private void UpdMovie()
        {
            if (IsBusy)
                return;

            if (!SelMovie.IsDirty)
                return;

            IsBusy = true;
            if (SelMovie.IsNew)
                _serviceAgent.InsertMovie(SelMovie.SingleMovie, OnUserCallback);
            else
                _serviceAgent.UpdateMovie(SelMovie.SingleMovie, OnUserCallback);
        }
        private void DelMovie()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            _serviceAgent.DeleteMovie(SelMovie.SingleMovie, OnUserCallback);
        }
        private void OnUserCallback(OperationStatus result, Exception exception)
        {
            IsBusy = false;
            if (exception != null)
                Messenger.Default.Send(exception);
            if (!result.IsSuccess)
                Messenger.Default.Send(new Exception("SERVER ERROR: " + result.ExMessage));
            else if (DialogService != null)
                DialogService.ShowMessage(result.IsSuccess ? "Success!" : "Failed!", "Notification"); //ToDo Success notification is never reached
            Refresh();
        }
        #endregion

        #region Properties
        private ObservableCollection<SingleMovieViewModel> _movies;
        public ObservableCollection<SingleMovieViewModel> Movies
        {
            get { return _movies; }
            set
            {
                if (_movies == value) return;
                _movies = value;
                RaisePropertyChanged("Movies");
            }
        }

        private string _selMovieTitle;
        public string SelMovieTitle { get; set; }

        private ObservableCollection<GoogleSearchResult> _movieUrls;
        public ObservableCollection<GoogleSearchResult> MovieUrls
        {
            get { return _movieUrls; }
            set
            {
                if (_movieUrls == value) return;
                _movieUrls = value;
                RaisePropertyChanged("MovieUrls");
            }
        }

        private SingleMovieViewModel _selMovie;
        public SingleMovieViewModel SelMovie
        {
            get { return _selMovie; }
            set
            {
                CurrentState = value == null ? "NoMEdit" : "MEdit";
                if (_selMovie != value)
                {
                    var oldValue = _selMovie == null ? "-1" : _selMovie.SingleMovie.Id;
                    _selMovie = value;
                    var newValue = _selMovie == null ? "-1" : _selMovie.SingleMovie.Id;
                    RaisePropertyChanged("SelMovie", oldValue, newValue, true);

                    NewMovieCommand.RaiseCanExecuteChanged();
                    UpdMovieCommand.RaiseCanExecuteChanged();
                    DelMovieCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _currentState;
        public string CurrentState
        {
            get { return _currentState; }
            set
            {
                if (_currentState == value) return;
                _currentState = value;
                RaisePropertyChanged("CurrentState");
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }
        #endregion

        public void Reset()
        {
            SelMovie = null;
            Movies = null;
        }

        public void Refresh()
        {
            //Reset();
            SelMovie = null;
            GetMovieUrls();
        }
    }
}
