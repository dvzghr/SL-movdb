using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MovieDb.Model;
using MovieDb.Model.HelperClasses;

namespace MovieDb.ViewModel
{
    [Export]
    public class ViewModelLocator : ViewModelBase, IPartImportsSatisfiedNotification
    {
        public RelayCommand UpdateCommand { get; private set; }

        public ViewModelLocator()
        {
            if (IsInDesignMode)
            {
                MViewModel = new MoviesViewModel(new MockServiceAgent());
                MViewModel.SelMovieTitle = "Inception";

                MViewModel.GetMovieCommand.Execute(new SelectionChangedEventArgs(new List<GoogleSearchResult>(),
                                                                                 new List<GoogleSearchResult>
                                                                                     {
                                                                                         new GoogleSearchResult
                                                                                             {Url = "testurl"}
                                                                                     }));
                MViewModel.GetMovieUrlsCommand.Execute(null);
            }
        }

        [Import]
        public MoviesViewModel MViewModel { get; set; }

        public bool IsBusy
        {
            get
            {
                return MViewModel != null && MViewModel.IsBusy;
            }
        }

        private bool _dragDropStatus;
        public bool DragDropStatus
        {
            get { return _dragDropStatus; }
            set
            {
                if (_dragDropStatus == value) return;
                _dragDropStatus = value;
                RaisePropertyChanged("DragDropStatus");
            }
        }

        public void OnImportsSatisfied()
        {
            MViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsBusy")
                    RaisePropertyChanged("IsBusy");
            };
        }

        public override void Cleanup()
        {
            MViewModel.Cleanup();
            MViewModel = null;
        }
    }
}
