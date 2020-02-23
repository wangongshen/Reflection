using System;
using System.Configuration;
using System.Linq;

using System.Data.SqlClient;
using Gs.DB.Interface;

namespace Gs.DB.SqlServer
{
    /// <summary>
    /// SqlServer实现
    /// </summary>
    public class SqlServerHelper : IDBHelper
    {
        private static string ConnectionStringCustomers = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;

        public SqlServerHelper()
        {
            //Console.WriteLine("{0}被构造", this.GetType().Name);
        }

        public void Query()
        {
            //Console.WriteLine("{0}.Query", this.GetType().Name);
        }

        /// <summary>
        /// Find Company  Find User 还有N多个表。。。。
        /// 如果希望一个方法  能返回不同的类型，那就是泛型方法
        /// 
        /// 如果还有一个人是钟于阿森纳，那就是我
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id)
        {
            //string sql = @"SELECT [Id]
            //                     ,[Name]
            //                     ,[CreateTime]
            //                     ,[CreatorId]
            //                     ,[LastModifierId]
            //                     ,[LastModifyTime]
            //                 FROM [Company]
            //                 WHERE ID=" + id;
            Type type = typeof(T);
            string sql = $"SELECT {string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"))} FROM [{type.Name}] WHERE ID={id}";
            object oObject = Activator.CreateInstance(type);
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    //Console.WriteLine(reader[1]);
                    foreach (var prop in type.GetProperties())
                    {
                        prop.SetValue(oObject, reader[prop.Name]);
                        //if (prop.Name.Equals("Id"))
                        //{
                        //    //prop.SetValue(oObject, reader["Id"]);
                        //    prop.SetValue(oObject, reader[prop.Name]);
                        //}
                        //else if (prop.Name.Equals("Name"))
                        //{
                        //    //prop.SetValue(oObject, reader["Name"]);
                        //    prop.SetValue(oObject, reader[prop.Name]);
                        //}
                        //....
                    }
                }
            }
            return (T)oObject;
        }


        public void Say()
        {
            Console.WriteLine("调用ReflectionTest中的Say方法");
        }
    }
}
