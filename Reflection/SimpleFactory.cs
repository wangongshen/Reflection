using Gs.DB.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class SimpleFactory
    {
        private static string IDBHelperConfig = ConfigurationManager.AppSettings["IDBHelperConfig"];
        private static string DllName = IDBHelperConfig.Split(',')[1];
        private static string TypeName = IDBHelperConfig.Split(',')[0];
        public static IDBHelper CreateInstance()
        {
            Assembly assembly = Assembly.Load(DllName);
            Type type = assembly.GetType(TypeName);
            object oDBHelper = Activator.CreateInstance(type);
            IDBHelper iDBHelper = oDBHelper as IDBHelper;
            return iDBHelper;
        }
    }
}
