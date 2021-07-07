using System;
using System.Reflection;

namespace DLAPI
{
    public static class DLFactory
    {
        public static IDL GetDL()
        {
            string dlPackageName = "DLXML";
            string dlNameSpace = "DL";
            string dlClass = "DLSQL";

            Assembly.Load(dlPackageName);

            Type type;

            type = Type.GetType($"{dlNameSpace}.{dlClass}, {dlPackageName}", true);


            IDL dal = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as IDL;
            return dal;

        }
    }
}
