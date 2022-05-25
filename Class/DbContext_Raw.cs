using MySql.Data.MySqlClient;

namespace SQL_Injection_Playground.Class
{
    public class DbContext_Raw
    {
        public static MySqlConnection con = new MySqlConnection();
        public static MySqlCommand cmd = new MySqlCommand();

        public static bool DbConnect()
        {
            if (con.State.ToString() == "Open")
                return true;
            
            try
            {
                con.ConnectionString = EnvironmentVariable.CONNECTION_STRING;
                cmd.Connection = con;
                con.Open();
                if (con.State.ToString() != "Open")
                    return false;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
