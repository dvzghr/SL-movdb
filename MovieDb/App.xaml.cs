﻿using System;
using System.ComponentModel.Composition;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using MovieDb.ViewModel;
using MovieDb.Views;

namespace MovieDb
{
    public partial class App : Application
    {
        [Import]
        public ViewModelLocator Locator { get; set; }

        [Import]
        public MainPage MPage
        {
            get { return RootVisual as MainPage; }
            set { RootVisual = value; }
        }

        public App()
        {
            Startup += Application_Startup;
            Exit += Application_Exit;
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CompositionInitializer.SatisfyImports(this);
            MPage.DataContext = Locator;
            //RootVisual = new MainPage();
            DispatcherHelper.Initialize();
        }

        private void Application_Exit(object sender, EventArgs e)
        {
            Locator.Cleanup();
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight 2 Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}