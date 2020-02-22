using Gs.DB.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gs.DB.Oracle
{
    public class OracleHelper : IDBHelper
    {
        public OracleHelper()
        {
            Console.WriteLine("{0}被构造", this.GetType().Name);
        }

        public void Query()
        {
            Console.WriteLine("{0}.Query", this.GetType().Name);
        }
    }
}
