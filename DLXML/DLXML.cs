using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    class DLXML
    {
        #region Singleton
        private static readonly DLXML instance = new DLXML();
        static DLXML() { }
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        
    }
}
