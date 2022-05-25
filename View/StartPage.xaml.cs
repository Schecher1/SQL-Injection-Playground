namespace SQL_Injection_Playground.View
{
    public partial class StartPage : Page
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        
        public StartPage()
            => InitializeComponent();

        private void Bttn_DatabaseLogin_Click(object sender, RoutedEventArgs e)
        {
            DbContext_EF DatabaseEF = new DbContext_EF();
            
            try
            {
                //Delete The Database, to reset the values
                DatabaseEF.Database.EnsureDeleted();

                //Create the Database, if it not exist
                if (!DatabaseEF.Database.EnsureCreated() && !DatabaseEF.Database.CanConnect())
                {
                    MessageBox.Show("Die Datenbank konnte nicht erstellt werden!");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Die Datenbank ist nicht erreichbar, bitte stelle sicher, dass sie gestartet ist in XAMPP!");
                return;
            }

            //try to connect the Database via EF
            if (!DatabaseEF.Database.CanConnect())
            {
                MessageBox.Show("Login fehlgeschlagen von EF");
                return;
            }

            //try to connect the Database via MySqlData
            if (!DbContext_Raw.DbConnect())
            {
                MessageBox.Show("Login fehlgeschlagen von MySqlData");
                return;
            }


            if (DbContext_Raw.DbConnect() && DatabaseEF.Database.CanConnect())
            {
                mw.ActivateAllButtons();
                mw.Rb_View_Registration.IsChecked = true;
                //saves the db intance in a variable for later use
                DbContextIntance.DbContext = DatabaseEF;
                mw.Frame_PageMirror.Content = new RegistrationPage();
            }
        }
    }
}
