
using MySql.Data.MySqlClient;

namespace Kadelle_Liburd_C__Cumulative.Models
{
    public class SchoolDbContext
    {
        //This is the information needed to login into my Database. Specifically using Xampp.
        private static string User { get { return "root"; } }

        private static string Password { get { return ""; } }

        private static string Database { get { return "schoolc"; } }

        private static string Server { get { return "localhost"; } }

        private static string Port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server= " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";

            }
        }

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
