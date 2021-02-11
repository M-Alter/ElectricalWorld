using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    public static class BLFactory
    {
        public static IBL GetBL() => new BLImp();
    }
}
