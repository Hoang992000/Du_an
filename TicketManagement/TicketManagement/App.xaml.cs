using RFIDApp.Helper;
using System;
using System.Threading;
using System.Windows;
using TicketManagement.Models;
using TicketManagement.View;

namespace TicketManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        AuthenticatorModel authen;
        MainWindow mainView;
        LoginView loginView;
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            ApiHelper.InitializeClient();
            authen = AuthenticatorModel.Ins;
            authen.StateLoggedInChanged += Authen_StateLoggedInChanged; ;
            loginView = new LoginView(authen);

            loginView.Show();

        }

        private void Authen_StateLoggedInChanged(object sender, EventArgs e)
        {
            if (authen.IsLoggedIn)
            {

                mainView = new MainWindow(authen);
                loginView.Close();
                mainView.Show();
            }
            else
            {
                loginView = new LoginView(authen);
                mainView.Close();
                loginView.Show();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            Mutex mutex = new Mutex(true, "NhatMinhApp", out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Another instance of the application is already running.", "Nhat Minh New Technology .,JSC");
                Current.Shutdown();
                return;
            }
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Mutex mutex = new Mutex(true, "NhatMinhApp");
            mutex.ReleaseMutex();

            base.OnExit(e);
        }
    }
}
