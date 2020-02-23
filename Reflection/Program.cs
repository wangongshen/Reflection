using Gs.DB.Interface;
using Gs.DB.SqlServer;
using Gs.Model;
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
                    Console.WriteLine("==========创建连接=============");
                    //IDBHelper iDBHelper = new MySqlHelper();
                    IDBHelper iDBHelper = new SqlServerHelper();

                    //当MySqlHelper换成SqlServerHelper 就必学修改现有的代码，重新编译然后发布，为了提高效率我们可以利用反射来解决这一问题。
                }
                #endregion

                #region 加载DLL
                {
                    Console.WriteLine("==========加载DLL==========");
                    //根据名称加载程序集
                    //Assembly assembly = Assembly.Load("Gs.DB.MySql");
                    //根据完整路径加载程序集
                    //Assembly assembly1 = Assembly.LoadFile(@"‪C:\Users\wango\source\repos\Reflection\Gs.DB.MySql\bin\Debug\Gs.DB.MySql.dll");
                    //根据名称加载程序集
                    Assembly assembly2 = Assembly.LoadFrom("Gs.DB.SqlServer.dll");
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
                        Console.WriteLine("类:" + type.Name);
                        foreach (var method in type.GetMethods())
                        {
                            Console.WriteLine(type.Name + "类中的方法:" + method.Name);
                        }
                    }

                }
                #endregion

                #region 加载DLL并创建对象
                {
                    Console.WriteLine("*****************加载DLL并创建对象、调用方法********************");
                    Assembly assembly = Assembly.Load("Gs.DB.SqlServer"); //1 动态加载
                    Type type = assembly.GetType("Gs.DB.SqlServer.SqlServerHelper"); //2 获取程序集下的指定类
                    foreach (var member in type.GetMembers())
                    {
                        //遍历类中的方法
                        Console.WriteLine("方法名：" + member.Name);
                    }
                    //object oDBHelper = Activator.CreateInstance(type); //创建对象
                    //oDBHelper.Say(); //调用方法，实际上oDBHelper是有SqlServerHelper()这个方法，只不过编译器不通过，解决这个问题可以用dynamic来收
                    dynamic dDBHelper = Activator.CreateInstance(type);
                    dDBHelper.Say(); //dynamic编译器不检查，方法拼写一定要写对

                    //不使用dynamic，转换类型也可以
                    object oDBHelper = Activator.CreateInstance(type); //创建对象
                    IDBHelper SqlServerHelper = oDBHelper as IDBHelper;
                    SqlServerHelper.Say(); //调用方法，实际上oDBHelper是有SqlServerHelper()这个方法，只不过编译器不通过，解决这个问题可以用dynamic来收

                }
                #endregion 封装之后
                {
                    Console.WriteLine("*****************封装之后********************");
                    IDBHelper iDBHelper = SimpleFactory.CreateInstance();
                    iDBHelper.Say();
                    //程序的可配置，通过修改配置文件就可以自动切换,实现类必须是事先已有的，而且在目录下面,没有写死类型，而是通过配置文件执行，反射创建的
                    //可扩展:完全不修改原有代码，只是增加新的实现，copy，修改配置文件，就可以支持新功能
                    //反射的动态加载和动态创建对象  以及配置文件结合
                }

                #region 调用带参方法
                {

                    //Assembly assembly = Assembly.Load("Ruanmou.DB.SqlServer");
                    //Type type = assembly.GetType("Ruanmou.DB.SqlServer.ReflectionTest");
                    //foreach (ConstructorInfo ctor in type.GetConstructors())
                    //{
                    //    Console.WriteLine(ctor.Name);
                    //    foreach (var parameter in ctor.GetParameters())
                    //    {
                    //        Console.WriteLine(parameter.ParameterType);
                    //    }
                    //}
                    //object oTest1 = Activator.CreateInstance(type);
                    //object oTest2 = Activator.CreateInstance(type, new object[] { 123 });
                    //object oTest3 = Activator.CreateInstance(type, new object[] { "陌殇" });

                    Console.WriteLine("*****************调用带参方法********************");
                    Assembly assembly = Assembly.Load("Gs.DB.SqlServer");
                    Type type = assembly.GetType("Gs.DB.SqlServer.ReflectionTest");
                    foreach (ConstructorInfo ctor in type.GetConstructors())
                    {
                        //获取类中的构造方法
                        Console.WriteLine("111:"+ctor.Name);
                        foreach (var parameter in ctor.GetParameters())
                        {
                            //返回构造方法的参数
                            Console.WriteLine("222:" + parameter.ParameterType);
                        }
                    }
                    object oTest1 = Activator.CreateInstance(type);
                    object oTest2 = Activator.CreateInstance(type, new object[] { 960927 });
                    object oTest3 = Activator.CreateInstance(type, new object[] { "调用带参方法" });
                }
                #endregion

                #region 反射破坏单例
                {
                    Console.WriteLine("********************反射破坏单例********************");
                    Singleton singleton1 = Singleton.GetInstance(); //new Singleton();
                    Singleton singleton2 = Singleton.GetInstance();
                    Singleton singleton3 = Singleton.GetInstance();
                    Singleton singleton4 = Singleton.GetInstance();
                    Singleton singleton5 = Singleton.GetInstance();
                    Console.WriteLine($"{object.ReferenceEquals(singleton1, singleton5)}");

                    //反射破坏单例---因为构造方法是用private修饰的，防止外部调用，但是反射不受限制
                    Assembly assembly = Assembly.Load("Gs.DB.SqlServer");
                    Type type = assembly.GetType("Gs.DB.SqlServer.Singleton");
                    Singleton singletonA = (Singleton)Activator.CreateInstance(type, true);
                    Singleton singletonB = (Singleton)Activator.CreateInstance(type, true);
                    Singleton singletonC = (Singleton)Activator.CreateInstance(type, true);
                    Singleton singletonD = (Singleton)Activator.CreateInstance(type, true);
                    Console.WriteLine($"{object.ReferenceEquals(singletonA, singletonD)}");
                }
                 
                #endregion 反射调用泛型
                {
                    Console.WriteLine("********************反射调用泛型********************");
                    Assembly assembly = Assembly.Load("Gs.DB.SqlServer");
                    Type type = assembly.GetType("Gs.DB.SqlServer.GenericClass`3"); //3表示GenericClass有3个参数

                    Type typeMake = type.MakeGenericType(new Type[] { typeof(string), typeof(int), typeof(DateTime) });
                    dynamic oGeneric = Activator.CreateInstance(typeMake);
                    oGeneric.ShowA();
                    //MethodInfo method = type.GetMethod("Show").MakeGenericMethod(typeof(string), typeof(string), typeof(string));
                    //method.Invoke(oGeneric, new object[] { "123","456","789" });
                }
                #region  不做类型转换调用
                {
                    //如果反射创建对象之后，知道方法名称，怎么样不做类型转换，直接调用方法？
                    //反射创建了对象实例---有方法的名称--反射调用方法
                    //dll名称---类型名称---方法名称---我们就能调用方法
                    //MVC就是靠的这一招---调用Action
                    //http://localhost:9099/home/index  经过路由解析---会调用--HomeController--Index方法  
                    //浏览器输入时只告诉了服务器类型名称和方法名称，肯定是反射的
                    //MVC在启动时会先加载--扫描全部的dll--找到全部的Controller--存起来--请求来的时候，用Controller来匹配的---dll+类型名称
                    //1 MVC局限性的--Action重载--反射是无法区分---只能通过HttpMethod+特性httpget/httppost
                    //2 AOP--反射调用方法，可以在前后插入逻辑
                    Console.WriteLine("********************不做类型转换调用********************");
                    Assembly assembly = Assembly.Load("Gs.DB.SqlServer");
                    Type type = assembly.GetType("Gs.DB.SqlServer.ReflectionTest");
                    object oTest = Activator.CreateInstance(type);
                   
                    foreach (var method in type.GetMethods())
                    {
                        //获取类中的方法名
                        Console.WriteLine("方法名:"+method.Name);
                        foreach (var parameter in method.GetParameters())
                        {
                            //获取方法中的参数
                            Console.WriteLine(method.Name+"方法中的参数："+parameter.Name+"==="+parameter.ParameterType);
                        }
                    }
                    {
                        ReflectionTest reflection = new ReflectionTest();
                        Console.WriteLine("普通调用");
                        reflection.Show1();
                    }
                    {
                        MethodInfo method = type.GetMethod("Show1");
                        Console.WriteLine("反射调用");
                        method.Invoke(oTest, null);
                    }
                    {
                        MethodInfo method = type.GetMethod("Show2");
                        Console.WriteLine("反射调用有参方法");
                        method.Invoke(oTest, new object[] { 123 });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { });
                        Console.WriteLine("反射调用重载无参方法Show3");
                        method.Invoke(oTest, null);
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(int) });
                        method.Invoke(oTest, new object[] { 1700 });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(string) });
                        method.Invoke(oTest, new object[] { "2020年2月23日17:02:00" });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(int), typeof(string) });
                        method.Invoke(oTest, new object[] { 1702, "2020年2月23日17:02:38" });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(string), typeof(int) });
                        method.Invoke(oTest, new object[] { "2020年2月23日17:04:02", 1704 });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show5");
                        method.Invoke(oTest, new object[] { "2020年2月23日17:04:32" });
                    }
                }
                #endregion 调用私有方法
                {
                    //调用私有方法
                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&私有方法&&&&&&&&&&&&&&&&&&&");
                    Assembly assembly = Assembly.Load("Gs.DB.SqlServer");
                    Type type = assembly.GetType("Gs.DB.SqlServer.ReflectionTest");
                    object oTest = Activator.CreateInstance(type);
                    var method = type.GetMethod("Show4", BindingFlags.Instance | BindingFlags.NonPublic);
                    //var method = type.GetMethod("Show4");
                    method.Invoke(oTest, new object[] { "2020年2月23日17:13:08" });
                }
                #region 
                {
                    Console.WriteLine("********************调用泛型方法********************");
                    Assembly assembly = Assembly.Load("Gs.DB.SqlServer");
                    Type type = assembly.GetType("Gs.DB.SqlServer.GenericMethod");
                    object oGeneric = Activator.CreateInstance(type);
                    foreach (var item in type.GetMethods())
                    {
                        //获取类中的方法
                        Console.WriteLine("类中的方法："+item.Name);
                    }
                    MethodInfo method = type.GetMethod("Show"); //获取方法
                    var methodNew = method.MakeGenericMethod(new Type[] { typeof(int), typeof(string), typeof(DateTime) });
                    object oReturn = methodNew.Invoke(oGeneric, new object[] { 1920, "2020年2月23日19:20:25", DateTime.Now });
                }
                #endregion
                {
                    Console.WriteLine("********************调用泛型类中的泛型方法********************");
                    Assembly assembly = Assembly.Load("Gs.DB.SqlServer");
                    Type type = assembly.GetType("Gs.DB.SqlServer.GenericDouble`1").MakeGenericType(typeof(int));
                    object oObject = Activator.CreateInstance(type);
                    MethodInfo method = type.GetMethod("Show").MakeGenericMethod(typeof(string), typeof(DateTime));
                    method.Invoke(oObject, new object[] { 1927, "2020年2月23日19:42:22", DateTime.Now });

                }
                #region 
                {
                    Console.WriteLine("*****************Common*******************");
                    People people = new People();
                    people.Id = 001;
                    people.Name = "孙悟空";
                    people.Description = "我是齐天大圣";
                    Console.WriteLine($"people.Id={people.Id}");
                    Console.WriteLine($"people.Name={people.Name}");
                    Console.WriteLine($"people.Description={people.Description}");
                }
                #endregion 反射赋值
                {
                    //1 get 反射展示是有意义的--反射遍历，可以不用改代码
                    //2 set 感觉好像没啥用
                    Console.WriteLine("*****************反射赋值*******************");
                    Type type = typeof(People);
                    object oPeople = Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        Console.WriteLine("类："+type.Name+";属性："+prop.Name+";值："+prop.GetValue(oPeople));
                        Console.WriteLine($"{type.Name}.{prop.Name}={prop.GetValue(oPeople)}");
                        if (prop.Name.Equals("Id"))
                        {
                            prop.SetValue(oPeople, 234);
                        }
                        else if (prop.Name.Equals("Name"))
                        {
                            prop.SetValue(oPeople, "猪八戒");
                        }
                        Console.WriteLine($"{type.Name}.{prop.Name}={prop.GetValue(oPeople)}");
                    }
                    foreach (var field in type.GetFields())
                    {
                        Console.WriteLine($"{type.Name}.{field.Name}={field.GetValue(oPeople)}");
                        if (field.Name.Equals("Description"))
                        {
                            field.SetValue(oPeople, "我是天蓬元帅");
                        }
                        Console.WriteLine($"{type.Name}.{field.Name}={field.GetValue(oPeople)}");
                    }

                }
                #region 动态查询数据库
                {
                    Console.WriteLine("******************动态查询数据库*********************");
                    SqlServerHelper helper = new SqlServerHelper();
                    Company company = helper.Find<Company>(1);
                    User user = helper.Find<User>(1);
                }
                #endregion 测试性能
                {
                    Console.WriteLine("*******************测试性能*******************");
                    Monitor.Show();
                }
                #region 

                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine("错误信息：" + e.Message);
            }
            Console.WriteLine("运行完毕");
            Console.ReadLine();
        }
    }
}
