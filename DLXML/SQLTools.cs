using System;

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
            //return "Data Source = .\\ElectricalWorld.db;Version = 3;";
            //string dir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\ElectricalWorld\";
            return "Data Source = " + dir + @"\ElectricalWorld.db;Version = 3;";
        }

    }
}
