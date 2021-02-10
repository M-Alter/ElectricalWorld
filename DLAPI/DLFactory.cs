using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DLAPI
{
    public static class DLFactory
    {
        public static IDL GetDL()
        {
            string dlPackageName = "DLXML";
            string dlNameSpace = "DL";
            string dlClass = "DLXML";

            Assembly.Load(dlPackageName);

            Type type;

            type = Type.GetType($"{dlNameSpace}.{dlClass}, {dlPackageName}", true);


            IDL dal = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as IDL;
            return dal;

        }
    }
}
