namespace DLSQL
{

    public class SQLTools
    {
        /// <summary>
        /// load a connection to the SQLite 
        /// </summary>
        /// <returns></returns>
        public static string LoadConnection()
        {
            return "Data Source = .\\ElectricalWorld.db;Version = 3;";
        }

    }
}
