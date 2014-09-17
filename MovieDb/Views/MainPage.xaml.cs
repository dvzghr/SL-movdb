using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;

namespace MovieDb.Views
{
    [Export]
    public partial class MainPage : UserControl
    {

        public MainPage()
        {
            InitializeComponent();
            Messenger.Default.Register<Exception>(this, ex =>
                                                            {
                                                                ChildWindow errorWin = new ErrorWindow(ex);
                                                                errorWin.Show();
                                                            });
        }

        // After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
        private void ContentFrameNavigated(object sender, NavigationEventArgs e)
        {

        }

        // If an error occurs during navigation, show an error window
        private void ContentFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }
    }
}
