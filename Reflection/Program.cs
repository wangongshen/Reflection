using Gs.DB.Interface;
using Gs.DB.SqlServer;
using System;
using System.Reflection;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
			try
			{
                #region 创建连接
                {
                    Console.WriteLine("=======================");
                    //IDBHelper iDBHelper = new MySqlHelper();
                    IDBHelper iDBHelper = new SqlServerHelper();

                    //当MySqlHelper换成SqlServerHelper 就必学修改现有的代码，重新编译然后发布，为了提高效率我们可以利用反射来解决这一问题。
                }
                #endregion

                #region 加载DLL
                {
                    Console.WriteLine("==========反射==========");
                    //根据名称加载程序集
                    //Assembly assembly = Assembly.Load("Gs.DB.MySql");
                    //根据完整路径加载程序集
                    //Assembly assembly1 = Assembly.LoadFile(@"‪C:\Users\wango\source\repos\Reflection\Gs.DB.MySql\bin\Debug\Gs.DB.MySql.dll");
                    //根据名称加载程序集
                    Assembly assembly2 = Assembly.LoadFrom("SimpleFtp.dll");
                    //根据完整路径加载程序集
                    //Assembly assembly3 = Assembly.LoadFrom(@"C:\Users\wango\source\repos\Reflection\Gs.DB.MySql\bin\Debug\Gs.DB.MySql.dll");
                    ///**
                    ///以上四个assembly获取的结果都是一样的
                    ///看起来LoadFile和LoadFrom作用是一样，他们两个有什么区别呢？
                    ///LoadFile加载dll文件时只会加载指定的dll文件，不会加载与指定dll文件相关的dll文件，而LoadFrom则会加载与指定dll文件相关的dll文件
                    ///LoadFrom加载dll文件时会先检查前面是否已经加载过相同名称的dll，如果载入过相同的dll那就不能再载入，而LoadFile不检查，会直接覆盖之前相同名称的dll文件
                    /// 

                    ///

                    foreach (var type in assembly2.GetTypes())
                    {
                        Console.WriteLine("类:"+type.Name);
                        foreach (var method in type.GetMethods())
                        {
                            Console.WriteLine(type.Name+"类中的方法:" +method.Name);
                        }
                    }
                    
                }
                #endregion

                #region 加载DLL并创建

                #endregion
            }
            catch (Exception e)
			{
                Console.WriteLine("错误信息："+e.Message);
			}

            Console.ReadLine();
        }
    }
}
