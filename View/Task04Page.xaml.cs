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
            string title = $"Aufgabe Nummer 4 {Environment.NewLine}" +
                                 $" (Schwiergikeits Level: ★ ★ ★ ★ ☆)";

            string message = $"Deine Aufgabe ist: {Environment.NewLine}" +
                                       $"Versuche die Datenbank \"sqlinjection\" zu löschen! {Environment.NewLine}" +
                                       $"Tipp: Weißt du überhaupt wie man eine Datenbank löscht? Nein? Finde es erst heraus! {Environment.NewLine}" +
                                       $"Keine Sorge diese Datenbank existiert schon, du musst die nicht erstellen. {Environment.NewLine}" +
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
                else
                    SendLog($"Der User {Tb_Username.Text} wurde gefunden, du bist jetzt Angemeldet!");
            }
            catch
            {
                SendLog($"Die angegebenen Benutzer Daten sind fehlerhaft! Du bist nicht Angemeldet!");

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
