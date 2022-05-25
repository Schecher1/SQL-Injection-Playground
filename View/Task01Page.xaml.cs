namespace SQL_Injection_Playground.View
{
    public partial class Task01Page : Page
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public Task01Page()
        {
            InitializeComponent();
            CreateMessageWindow();
            mw.DeactivateRadioButtonsForTheTask();
            Bttn_Registration.IsEnabled = false;
        }

        private void CreateMessageWindow()
        {
            //this is hardcode, but it's ok for now
            string title = $"Aufgabe Nummer 1 {Environment.NewLine}" +
                                 $" (Schwiergikeits Level: ★ ☆ ☆ ☆ ☆)";

            string message = $"Deine Aufgabe ist: {Environment.NewLine}" +
                                       $"Erstelle dir ein Account, dir ist es überlassen " +
                                       $"was für ein Benutzername und Passwort der " +
                                       $"haben soll. Die Aufgabe dient dafür damit du " +
                                       $"das Programm kennenlernst! {Environment.NewLine}" +
                                       $"Merke dir die Benutzerdaten! " +
                                       $"Die gebrauchst du für die Zweite Aufgabe {Environment.NewLine}" +
                                       $"Viel Erfolg!";
            
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
                Bttn_Registration.IsEnabled = false;
            else
                Bttn_Registration.IsEnabled = true;
        }

        private void Bttn_Registration_Click(object sender, RoutedEventArgs e)
            => CreateUserWithMySqlData();
        
        private void CreateUserWithMySqlData()
        {
            try
            {
                if (DbContextIntance.DbContext.User.FirstOrDefault(p => p.Username == Tb_Username.Text) != null)
                {
                    SendLog($"Der User {Tb_Username.Text} existiert bereits!");
                    return;
                }
                
                DbContext_Raw.cmd.CommandText = $"INSERT INTO sqlinjection.user (`Username`, `Password`) VALUES ('{Tb_Username.Text}', '{Tb_Password.Text}');";
                DbContext_Raw.cmd.ExecuteNonQuery();

                //The Task is finished
                EnvironmentVariable.IsTask01Completed = true;
                mw.Frame_PageMirror.Content = new FinishTaskPage(1);

            }
            catch
            {
                SendLog($"Der User {Tb_Username.Text} wurde nicht erstellt!");
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
                mw.Msg_SqlQuery.Text = $"INSERT INTO sqlinjection.user (`Username`, `Password`) VALUES ('{Tb_Username.Text}', '{Tb_Password.Text}');";
            }
        }
    }
}
