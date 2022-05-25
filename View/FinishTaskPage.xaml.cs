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
                Tb_Message.Text = $"You have successfully completed the {taskNumber} level! {Environment.NewLine}" +
                                $"Level {taskNumber + 1} is now unlocked!";
            else
                Tb_Message.Text = $"You have successfully completed the {taskNumber} level! {Environment.NewLine}" +
                                $"Congratulations, you have completed all levels! {Environment.NewLine}" +
                                $"You can call yourself Sql-Injection pro now! :D";
        }
    }
}
