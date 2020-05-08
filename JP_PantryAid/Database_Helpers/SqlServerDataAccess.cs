using PantryAid.Core.Models;
using PantryAid.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Helpers
{
    /// <summary>
    /// Accesses a SQL Database for project
    /// </summary>
    public class SqlServerDataAccess : iSqlServerDataAccess
    {
        private static string _serverAddress = "aura.cset.oit.edu";
        private static string _serverPort = "5433";
        private static string _databaseName = "JBNT";
        private static string _serverUsername = "JBNT";
        private static string _serverPassword = "Hootie123";
        private static int _curuserid;
        private static bool _debugMode = true;

        /// <summary>
        /// Returns The connection string for the database
        /// </summary>
        public static string GetConnectionString()
        {
            return "server=" + ServerAddress + ", " + ServerPort + "; database=" + ServerDatabaseName + "; UID=" + ServerUsername + "; password=" + ServerPassword;
        }

        /// <summary>
        /// Execute a Query on a sql database that has no return type
        /// </summary>
        /// <param name="sql">The query to execute</param>
        /// <returns>0 if passed, 1 if failed</returns>
        public int ExecuteQuery_NoReturnType(string sql)
        {
            using(SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand comm = new SqlCommand(sql, con);

                try
                {
                    con.Open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return FAIL;
                }
            }

            return PASS;
        }

        public List<IngredientItem> ExecuteQuery_GetPantry(string sql)
        {
            throw new NotImplementedException();
        }

        public Ingredient ExecuteQuery_SingleIngredientItem(string sql)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand comm = new SqlCommand(sql, con);

                int ingID = 0;
                string ingName = null;

                //Open Connection
                try { con.Open(); }
                catch (Exception) { return null; }

                //Execute reader
                try
                {
                    SqlDataReader read = comm.ExecuteReader();

                    if (read.Read())
                    {
                        ingID = read.GetInt32(0);
                        ingName = read.GetString(1);
                    }
                    else{ return null; }

                    read.Close();
                }
                catch (Exception){ return null; }

                return new Ingredient(ingID, ingName);
            }
        }

        public User ExecuteQuery_SingleUser(string sql)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand comm = new SqlCommand(sql, con);

                int usrId = 0;
                string email = "";
                string hash = "";

                //Open Connection
                try { con.Open(); }
                catch (Exception) { return null; }

                //Execute reader
                try
                {
                    SqlDataReader read = comm.ExecuteReader();

                    if (read.Read())
                    {
                        usrId = read.GetInt32(0);
                        email = read.GetString(1);
                        hash = read.GetString(2);
                    }
                    else { return null; }

                    read.Close();
                }
                catch (Exception) { return null; }

                return new User() { Id = usrId, Email = email, Hash = hash };
            }
        }

        /// <summary>
        /// The server address of the SQL server
        /// </summary>
        public static string ServerAddress
        {
            get
            { return _serverAddress; }
            set
            { _serverAddress = value; }
        }

        /// <summary>
        /// The port of the server to connect to
        /// </summary>
        public static string ServerPort
        {
            get => _serverPort;
            set => _serverPort = value;
        }

        /// <summary>
        /// The name of the database on the server
        /// </summary>
        public static string ServerDatabaseName
        {
            get
            { return _databaseName; }
            set
            { _databaseName = value; }
        }

        /// <summary>
        /// The username for the SQL server
        /// </summary>
        public static string ServerUsername
        {
            get
            { return _serverUsername; }
            set
            { _serverUsername = value; }
        }

        /// <summary>
        /// The password for the SQL server
        /// </summary>
        public static string ServerPassword
        {
            get
            { return _serverPassword; }
            set
            { _serverPassword = value; }
        }

        /// <summary>
        /// The user ID for the current user
        /// </summary>
        public static int UserID
        {
            get
            { return _curuserid; }
            set
            { _curuserid = value; }
        }

        /// <summary>
        /// Sets Debug mode for application
        /// </summary>
        public static bool DebugMode { get => _debugMode; }

        /// <summary>
        /// Definition of return types
        /// </summary>
        private int FAIL = 0;
        private int PASS = 1;

        public static string Sanitize(string str)
        {
            str.Replace("-", "");
            str.Replace("!", "");
            str.Replace("@", "");
            str.Replace("#", "");
            str.Replace("$", "");
            str.Replace("%", "");
            str.Replace("^", "");
            str.Replace("&", "");
            str.Replace("*", "");
            str.Replace("(", "");
            str.Replace(")", "");
            str.Replace("{", "");
            str.Replace("}", "");
            str.Replace("\\", "");
            str.Replace(",", "");
            str.Replace("/", "");
            str.Replace("|", "");
            return str;
        }

        //Converts one measurement to another
        //Converts all liquid measurements to cups and all mass/weight measurements to grams
        //then converts to the desired measurement from there
        //Returns -1 if not convertable
        public static double ConvertM(string From, string To, double val)
        {
            if (From == To)
                return val;

            if (To == "Serving")
                return -1;

            switch (From)
            {
                case "Tbsp":
                    return ConvertM("Cup", To, val / 16);
                case "Tsp":
                    return ConvertM("Cup", To, val / 48);
                case "Cup":
                    switch (To)
                    {
                        case "Tbsp":
                            return val * 16;
                        case "Tsp":
                            return val * 48;
                        case "Pint":
                            return val / 2;
                        case "Quart":
                            return val / 4;
                        case "Gallon":
                            return val / 16;
                        case "Liter":
                            return val / 4.227;
                        case "Milliliter":
                            return val / 237;
                        case "Fluid Ounce":
                            return val * 8;
                        default:
                            return -1;
                    }
                case "Pint":
                    return ConvertM("Cup", To, val * 2);
                case "Quart":
                    return ConvertM("Cup", To, val * 4);
                case "Gallon":
                    return ConvertM("Cup", To, val * 16);
                case "Liter":
                    return ConvertM("Cup", To, val * 4.227);
                case "Milliliter":
                    return ConvertM("Cup", To, val / 237);
                case "Fluid Ounce":
                    return ConvertM("Cup", To, val / 8);

                case "Ounce":
                    return ConvertM("Gram", To, val * 28.35);
                case "Lb":
                    return ConvertM("Gram", To, val * 454);
                case "Milligram":
                    return ConvertM("Gram", To, val / 1000);
                case "Gram":
                    switch(To)
                    {
                        case "Ounce":
                            return val / 28.35;
                        case "Lb":
                            return val / 454;
                        case "Milligram":
                            return val * 1000;
                        case "Kilogram":
                            return val / 1000;
                        default:
                            return -1;
                    }
                case "Kilogram":
                    return ConvertM("Gram", To, val * 1000);

                case "Serving":
                default:
                    return -1;
            }
        }
    }
}
