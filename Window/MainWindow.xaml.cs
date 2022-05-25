namespace SQL_Injection_Playground
{
    public partial class MainWindow
    {
        public MainWindow()
            => InitializeComponent();

        private void Window_Loaded(object sender, RoutedEventArgs e)
            => Frame_PageMirror.Content = new StartPage();

        private void Rb_DatabaseType_Checked(object sender, RoutedEventArgs e)
        {
            if (((RadioButton)sender).Name == "Rb_NonEF")
                EnvironmentVariable.MySqlData = true;
            else
                EnvironmentVariable.MySqlData = false;

            ClearSqlQuery();
        }

        private void Rb_PasswordType_Checked(object sender, RoutedEventArgs e)
        {
            if (((RadioButton)sender).Name == "Rb_Password_Plain")
                EnvironmentVariable.PasswordPlain = true;
            else
                EnvironmentVariable.PasswordPlain = false;

            ClearSqlQuery();
        }

        private void Rb_View_Checked(object sender, RoutedEventArgs e)
        {
            //is only the first time NULL, when the page is loaded
            if (Frame_PageMirror == null)
                return;

            //if the user switch in a task between a view
            ActivateAllButtons();
                
            switch (((RadioButton)sender).Name)
            {
                case "Rb_View_Start":
                    Frame_PageMirror.Content = new StartPage();
                    break;
                case "Rb_View_Registration":
                    Frame_PageMirror.Content = new RegistrationPage();
                    break;
                case "Rb_View_Login":
                    Frame_PageMirror.Content = new LoginPage();
                    break;
            }

            ClearSqlQuery();
        }
        
        private void Rb_Query_Checked(object sender, RoutedEventArgs e)
        {
            if (((RadioButton)sender).Name == "Rb_Query_Show")
                EnvironmentVariable.ShowQuery = true;
            else
                EnvironmentVariable.ShowQuery = false;

            ClearSqlQuery();
        }

        private void ClearSqlQuery()
        {
            //is only the first time NULL, when the page is loaded
            if (Msg_SqlQuery == null)
                return;
            Msg_SqlQuery.Text = "";
        }

        public void ActivateAllButtons()
        {
            Rb_View_Registration.IsEnabled = true;
            Rb_View_Login.IsEnabled = true;
            Rb_Password_Plain.IsEnabled = true;
            Rb_Password_Hash.IsEnabled = true;
            Rb_NonEF.IsEnabled = true;
            Rb_EF.IsEnabled = true;
            Rb_Query_Show.IsEnabled = true;
            Rb_Query_Hidden.IsEnabled = true;
            Rb_View_Start.IsEnabled = true;
            Bttn_Task01.IsEnabled = true;
            Bttn_Task02.IsEnabled = true;
            Bttn_Task03.IsEnabled = true;
            Bttn_Task04.IsEnabled = true;
            Bttn_Task05.IsEnabled = true;
        }

        public void DeactivateRadioButtonsForTheTask()
        {
            Rb_Password_Plain.IsEnabled = false;
            Rb_Password_Hash.IsEnabled = false;
            Rb_NonEF.IsEnabled = false;
            Rb_EF.IsEnabled = false;
            
            Rb_View_Login.IsChecked = false;
            Rb_View_Registration.IsChecked = false;
            Rb_View_Start.IsChecked = false;
        }

        private void Bttn_Task_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "Bttn_Task01":
                    Frame_PageMirror.Content = new Task01Page();
                    break;
                    
                case "Bttn_Task02":
                    if (EnvironmentVariable.IsTask01Completed)
                        Frame_PageMirror.Content = new Task02Page();
                    else
                        MessageBox.Show("Aufgabe 1 ist noch nicht abgeschlossen!");
                    break;
                    
                case "Bttn_Task03":
                    if (EnvironmentVariable.IsTask02Completed)
                        Frame_PageMirror.Content = new Task03Page();
                    else
                        MessageBox.Show("Aufgabe 2 ist noch nicht abgeschlossen!");
                    break;
                    
                case "Bttn_Task04":
                    if (EnvironmentVariable.IsTask03Completed)
                        Frame_PageMirror.Content = new Task04Page();
                    else
                        MessageBox.Show("Aufgabe 3 ist noch nicht abgeschlossen!");
                    break;
                    
                case "Bttn_Task05":
                    if (EnvironmentVariable.IsTask04Completed)
                        Frame_PageMirror.Content = new Task05Page();
                    else
                        MessageBox.Show("Aufgabe 4 ist noch nicht abgeschlossen!");
                    break;
            }
            ClearSqlQuery();
        }
    }
}
