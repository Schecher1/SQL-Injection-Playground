namespace SQL_Injection_Playground.View
{
    public partial class Task03Page : Page
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public Task03Page()
        {
            InitializeComponent();
            CreateMessageWindow();
            mw.DeactivateRadioButtonsForTheTask();
            CreateUserForThisTask();
            Bttn_Login.IsEnabled = false;
        }

        private void CreateUserForThisTask()
        {
            UserModel newUser = new UserModel()
            {
                Username = "Tom",
                Password = "s2OGh02TCWPxNHMSoQ1iA3DbMcQ1A1ND5F2jQ6yvgoNm8bMuFeXThHvp9hWoo9Y8"
            };

            DbContextIntance.DbContext.User.Add(newUser);
            DbContextIntance.DbContext.SaveChanges();
        }

        private void CreateMessageWindow()
        {
            //this is hardcode, but it's ok for now
            string title = $"Task number 3 {Environment.NewLine}" +
                                 $" (Difficulty level: ★ ★ ☆ ☆ ☆)";

            string message = $"Your task is: {Environment.NewLine}" +
                                       $"Log in with the account of \"Tom\" without knowing the password! {Environment.NewLine}" +
                                       $"Don't worry this account exists in the database, you don't have to create it. {Environment.NewLine}" +
                                       $"Good luck!";

            MessageWindow MsgWindow = new MessageWindow(title, message);
            MsgWindow.Show();
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
            => LoginUserWithMySqlData();

        
        private void LoginUserWithMySqlData()
        {
            try
            {
                DbContext_Raw.cmd.CommandText = $"SELECT * FROM sqlinjection.user WHERE(Username= '{Tb_Username.Text}' AND Password = '{Tb_Password.Text}');";
                object user = DbContext_Raw.cmd.ExecuteScalar();
                
                if (!Tb_Username.Text.ToLower().Contains("tom"))
                {
                    SendLog($"This is not the user \"Tom\"!");
                }
                else
                {
                    if (user == null)
                        SendLog($"The specified user data is incorrect! You are not logged in!");
                    else
                    {
                        //The Task is finished
                        EnvironmentVariable.IsTask03Completed = true;
                        mw.Frame_PageMirror.Content = new FinishTaskPage(3);
                    }
                }
                  
            }
            catch
            {
                SendLog($"The specified user data is incorrect! You are not logged in!");
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
                mw.Msg_SqlQuery.Text = $"SELECT * FROM sqlinjection.user WHERE(Username= '{Tb_Username.Text}' AND Password = '{Tb_Password.Text}');";
            }
        }
    }
}
