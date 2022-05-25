using System.Security.Cryptography;
using System.Text;

namespace SQL_Injection_Playground.Class
{
    public class HashSystem
    {
        //Create a MD5 Hash from a string,
        //return a "" if a Exeption is thrown
        //return a "" if the string is null
        public static string GetHash_MD5(string input)
        {
            try
            {
                if (input == null)
                    return "";

                StringBuilder Sb = new StringBuilder();
                
                using (var hash = MD5.Create())
                {
                    Encoding enc = Encoding.UTF8;
                    Byte[] result = hash.ComputeHash(enc.GetBytes(input));

                    foreach (Byte b in result)
                        Sb.Append(b.ToString("x2"));
                }
                return Sb.ToString();
            }
            catch { return ""; }
        }
    }
}
