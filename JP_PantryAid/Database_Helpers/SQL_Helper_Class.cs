using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Helpers
{
    
    /// <summary>
    /// This class exists to help centralize our SQL related functions and info
    /// </summary>
    public static class SqlHelper
    {
        //ToDO / Notes
        // Add in config file loading for the connection string values
        // We may need a more robust version of this depending on how user accounts are handled or if we end up using multiple database servers
        //
        //


        //I broke these apart so that individual parts of the connection string could easily be changed without worrying about formatting
        //private string ConnectionString = "server=aura.cset.oit.edu, 5433; database=iUngerTime; UID=iUngerTime; password=iUngerTime";
        private static string _serverAddress = "aura.cset.oit.edu";
        private static string _serverPort = "5433";
        private static string _databaseName = "JBNT";
        private static string _serverUsername = "JBNT";
        private static string _serverPassword = "Hootie123";

        private static int _curuserid;

        /// <summary>
        /// Returns The connection string for the database
        /// </summary>
        public static string GetConnectionString()
        {
            return "server=" + ServerAddress + ", " + ServerPort + "; database=" + ServerDatabaseName +"; UID=" + ServerUsername + "; password=" + ServerPassword;
        }


        /// <summary>
        /// The server address of the SQL server
        /// </summary>
        public static string ServerAddress 
        {
            get
            { return  _serverAddress; }
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

    }
}
