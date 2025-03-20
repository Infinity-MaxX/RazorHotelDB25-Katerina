using System;

namespace RazorHotelDB25_Katerina.Helpers
{
    public class ConnectionManager
    {
        #region Instances
        private static string _connectionString = "";
        #endregion

        #region Properties
        public static string Connection { get { return _connectionString; } }
        #endregion
    }
}
