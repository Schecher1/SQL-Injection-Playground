namespace SQL_Injection_Playground.View
{
    public partial class Task05Page : Page
    {
        MainWindow mw = (MainWindow)Application.Current.MainWindow;

        public Task05Page()
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
                Id = 1889,
                Username = "Lea",
                Password = "K0OUFdPawzB5a6XzszKy0NI4EzvpYPHDWRvtzgRsui9uZTVRFH1HPtJ9Y2lrInXJ"
            };


            if (DbContextIntance.DbContext.User.FirstOrDefault(p => p.Id == newUser.Id) != null) 
                return;

            DbContextIntance.DbContext.User.Add(newUser);
            DbContextIntance.DbContext.SaveChanges();
        }

        private void CreateMessageWindow()
        {
            //this is hardcode, but it's ok for now
            string title = $"Aufgabe Nummer 5 {Environment.NewLine}" +
                                 $" (Schwiergikeits Level: ★ ★ ★ ★ ★)";

            string message = $"Deine Aufgabe ist: {Environment.NewLine}" +
                                       $"Melde dich mit dem Account von \"Lea\" an, MIT das Passwort \"Lea123\"! {Environment.NewLine}" +
                                       $"Tipp: Du muss das Passwort in der Datenbank verändern, weiß du überhaupt, wie man ein Datensatz bearbeitet? Nein? Finde es erst heraus! " +
                                       $"Tipp: Die ID von \"Lea\" ist ID: 1889 {Environment.NewLine}" +
                                       $"Keine Sorge dieser Account existiert in der Datenbank, du musst ihn nicht erstellen. {Environment.NewLine}" +
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
                    SendLog($"Die angegebenen Benutzer Daten sind fehlerhaft! Du bist nicht Angemeldet!");
                else if (Tb_Password.Text == "Lea123" && Tb_Username.Text == "Lea")
                {
                    //The Task is finished
                    mw.Frame_PageMirror.Content = new FinishTaskPage(5);
                }
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
                mw.Msg_SqlQuery.Text = $"SELECT * FROM sqlinjection.user WHERE(Username= '{Tb_Username.Text}' AND Password = '{Tb_Password.Text}');";
            }
        }
    }
}
