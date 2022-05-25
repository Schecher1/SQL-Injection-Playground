namespace SQL_Injection_Playground.View
{
    public partial class Task04Page : Page
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public Task04Page()
        {
            InitializeComponent();
            CreateMessageWindow();
            mw.DeactivateRadioButtonsForTheTask();
            Bttn_Login.IsEnabled = false;
        }

        private void CreateMessageWindow()
        {
            //this is hardcode, but it's ok for now
            string title = $"Task number 4 {Environment.NewLine}" +
                                 $" (Difficulty level: ★ ★ ★ ★ ☆)";

            string message = $"Your task is: {Environment.NewLine}" +
                                       $"Try to delete the database \"sqlinjection\"! {Environment.NewLine}" +
                                       $"Hint: Do you even know how to delete a database? No? Find out first! {Environment.NewLine}" +
                                       $"Don't worry this database already exists, you don't need to create it. {Environment.NewLine}" +
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
                    SendLog($"The user {Tb_Username.Text} has been found, you are now Logged in!");
            }
            catch
            {
                SendLog($"The specified user data is incorrect! You are not logged in!");

                DbContext_Raw.cmd.CommandText = $"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'sqlinjection';";
                object table = DbContext_Raw.cmd.ExecuteScalar();
                
                if (table == null)
                {
                    //The Task is finished
                    DbContextIntance.DbContext.Database.EnsureCreated();
                    EnvironmentVariable.IsTask04Completed = true;
                    mw.Frame_PageMirror.Content = new FinishTaskPage(4);
                }
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
