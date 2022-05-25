namespace SQL_Injection_Playground.Window
{
    public partial class MessageWindow
    {
        public MessageWindow(string Title, string Message)
        {
            InitializeComponent();
            SetText(Title, Message);
        }

        private void SetText(string Title, string Message)
        {
            Msg_Title.Text = Title;
            Tb_Message.Text = Message;
        }
    }
}
