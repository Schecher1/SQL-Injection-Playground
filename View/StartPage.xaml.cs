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
                    MessageBox.Show("The database could not be created!");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("The database is unreachable, please make sure it is started in XAMPP!");
                return;
            }

            //try to connect the Database via EF
            if (!DatabaseEF.Database.CanConnect())
            {
                MessageBox.Show("Login failed from EF");
                return;
            }

            //try to connect the Database via MySqlData
            if (!DbContext_Raw.DbConnect())
            {
                MessageBox.Show("Login failed from MySqlData");
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
