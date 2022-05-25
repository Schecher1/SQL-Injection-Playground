namespace SQL_Injection_Playground.Class
{
    class EnvironmentVariable
    {
        //the Connection String for the database
        public const string CONNECTION_STRING = "Server=localhost;Database=SqlInjection;User=root;Password=";
        public static bool MySqlData { get; set; }
        public static bool PasswordPlain { get; set; }
        public static bool ShowQuery { get; set; }

        //for the request, if the user can play this game/task
        public static bool IsTask01Completed { get; set; } 
        public static bool IsTask02Completed { get; set; } 
        public static bool IsTask03Completed { get; set; } 
        public static bool IsTask04Completed { get; set; }
    }
}
