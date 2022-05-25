namespace SQL_Injection_Playground.View
{
    public partial class RegistrationPage : Page
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        
        public RegistrationPage()
        {
            InitializeComponent();
            Bttn_Registration.IsEnabled = false;
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
                Bttn_Registration.IsEnabled = false;
            else
                Bttn_Registration.IsEnabled = true;
        }

        private void Bttn_Registration_Click(object sender, RoutedEventArgs e)
        {
            if (EnvironmentVariable.MySqlData)
                CreateUserWithMySqlData();
            else
                CreateUserWithEF();
        }

        private void CreateUserWithEF()
        {
            try
            {
                if (DbContextIntance.DbContext.User.FirstOrDefault(p => p.Username == Tb_Username.Text) != null)
                {
                    SendLog($"The user {Tb_Username.Text} already exists!");
                    return;
                }

                DbContextIntance.DbContext.User.Add(new UserModel()
                {
                    Username = Tb_Username.Text,
                    Password = EnvironmentVariable.PasswordPlain ? Tb_Password.Text : HashSystem.GetHash_MD5(Tb_Password.Text)
                });

                DbContextIntance.DbContext.SaveChanges();
                SendLog($"The user {Tb_Username.Text} has been created!");
            }
            catch
            {
                SendLog($"The user {Tb_Username.Text} was not created!");
            }
            finally
            {
                if (Cb_InputClear.IsChecked == false)
                    ClearInputsWithRestoreLog();
            }
        }

        private void CreateUserWithMySqlData()
        {
            try
            {
                if (DbContextIntance.DbContext.User.FirstOrDefault(p => p.Username == Tb_Username.Text) != null)
                {
                    SendLog($"The user {Tb_Username.Text} already exists!");
                    return;
                }

                
                string password = EnvironmentVariable.PasswordPlain ? Tb_Password.Text : HashSystem.GetHash_MD5(Tb_Password.Text);
                DbContext_Raw.cmd.CommandText = $"INSERT INTO sqlinjection.user (`Username`, `Password`) VALUES ('{Tb_Username.Text}', '{password}');";
                DbContext_Raw.cmd.ExecuteNonQuery();

                SendLog($"The user {Tb_Username.Text} has been created!");
            }
            catch
            {
                SendLog($"The user {Tb_Username.Text} was not created!");
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
                mw.Msg_SqlQuery.Text = $"INSERT INTO sqlinjection.user (`Username`, `Password`) VALUES ('{Tb_Username.Text}', '{password}');";
            }
        }        
    }
}
