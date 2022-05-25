namespace SQL_Injection_Playground.View
{
    public partial class LoginPage : Page
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        
        public LoginPage()
        {
            InitializeComponent();
            Bttn_Login.IsEnabled = false;
        }

        private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonCheck();
            ClearLog();
            ShowQuery();
        }
        
        private void ButtonCheck()
        {
            if (String.IsNullOrWhiteSpace(Tb_Username.Text) || String.IsNullOrWhiteSpace(Tb_Password.Text))
                Bttn_Login.IsEnabled = false;
            else
                Bttn_Login.IsEnabled = true;
        }

        private void Bttn_Registration_Click(object sender, RoutedEventArgs e)
        {
            if (EnvironmentVariable.MySqlData)
                LoginUserWithMySqlData();
            else
                LoginUserWithEF();
        }

        private void LoginUserWithEF()
        {
            try
            {
                //get user from database with username and password
                string password = EnvironmentVariable.PasswordPlain ? Tb_Password.Text : HashSystem.GetHash_MD5(Tb_Password.Text);
                var user = DbContextIntance.DbContext.User.FirstOrDefault(u => u.Username == Tb_Username.Text && u.Password == password);

                if (user == null)
                    SendLog($"Die angegebenen Benutzer Daten sind fehlerhaft! Du bist nicht Angemeldet!");
                else
                    SendLog($"Der User {Tb_Username.Text} wurde gefunden, du bist jetzt Angemeldet!");

            }
            catch
            {
                SendLog($"Die angegebenen Benutzer Daten sind fehlerhaft! Du bist nicht Angemeldet!");
            }
            finally
            {
                if (Cb_InputClear.IsChecked == false)
                    ClearInputsWithRestoreLog();
            }
        }

        private void LoginUserWithMySqlData()
        {
            try
            {
                string password = EnvironmentVariable.PasswordPlain ? Tb_Password.Text : HashSystem.GetHash_MD5(Tb_Password.Text);
                DbContext_Raw.cmd.CommandText = $"SELECT * FROM sqlinjection.user WHERE(Username= '{Tb_Username.Text}' AND Password = '{password}');";
                object user = DbContext_Raw.cmd.ExecuteScalar();

                if (user == null)
                    SendLog($"Die angegebenen Benutzer Daten sind fehlerhaft! Du bist nicht Angemeldet!");
                else
                    SendLog($"Der User {Tb_Username.Text} wurde gefunden, du bist jetzt Angemeldet!");


            }
            catch
            {
                SendLog($"Die angegebenen Benutzer Daten sind fehlerhaft! Du bist nicht Angemeldet!");
            }
            finally
            {
                if (Cb_InputClear.IsChecked == false)
                    ClearInputsWithRestoreLog();
            }
        }

        private void ClearInputs()
        {
            Tb_Password.Text = "";
            Tb_Username.Text = "";
        }

        private void ClearLog()
        {
            Msg_Log.Text = "";
        }

        private void ClearInputsWithRestoreLog()
        {
            string cache = Msg_Log.Text;
            Tb_Password.Text = "";
            Tb_Username.Text = "";
            Msg_Log.Text = cache;
        }

        private void SendLog(string log)
        {
            Msg_Log.Text = log;
        }

        private void ShowQuery()
        {
            if (EnvironmentVariable.ShowQuery && EnvironmentVariable.MySqlData)
            {
                string password = EnvironmentVariable.PasswordPlain ? Tb_Password.Text : HashSystem.GetHash_MD5(Tb_Password.Text);
                mw.Msg_SqlQuery.Text = $"SELECT * FROM sqlinjection.user WHERE(Username= '{Tb_Username.Text}' AND Password = '{password}');";
            }
        }
    }
}
