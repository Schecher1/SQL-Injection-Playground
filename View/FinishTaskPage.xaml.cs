namespace SQL_Injection_Playground.View
{
    public partial class FinishTaskPage : Page
    {
        public FinishTaskPage(int TaskNumber)
        {
            InitializeComponent();
            WriteMessage(TaskNumber);
        }
        
        private void WriteMessage(int taskNumber)
        {
            //this is hardcode, but it's ok for now
            if (taskNumber < 5)
                Tb_Message.Text = $"Du hast erfolgreich, das Level {taskNumber} absolviert! {Environment.NewLine}" +
                                $"Level {taskNumber + 1} ist jetzt Freigeschaltet!";
            else
                Tb_Message.Text = $"Du hast erfolgreich, das Level {taskNumber} absolviert! {Environment.NewLine}" +
                                $"Herzlichen Glückwunsch, du hast alle Level absolviert! {Environment.NewLine}" +
                                $"Du kannst dich jetzt Sql-Injection Profi nennen! :D";
        }
    }
}
