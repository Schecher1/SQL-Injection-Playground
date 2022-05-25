namespace SQL_Injection_Playground.View
{
    public partial class Task02Page : Page
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public Task02Page()
        {
            InitializeComponent();
            CreateMessageWindow();
            mw.DeactivateRadioButtonsForTheTask();
            Bttn_Login.IsEnabled = false;
        }

        private void CreateMessageWindow()
        {
            //this is hardcode, but it's ok for now
            string title = $"Task number 2 {Environment.NewLine}" +
                                 $" (Difficulty level: ★ ☆ ☆ ☆ ☆)";

            string message = $"Your task is: {Environment.NewLine}" +
                                       $"Log in with your account that you created earlier. " +
                                       $"The task is for you to " +
                                       $"get to know the program! {Environment.NewLine}" +
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

                if (user == null)
                    SendLog($"The specified user data is incorrect! You are not logged in!");
                else
                {
                    //The Task is finished
                    EnvironmentVariable.IsTask02Completed = true;
                    mw.Frame_PageMirror.Content = new FinishTaskPage(2);
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
