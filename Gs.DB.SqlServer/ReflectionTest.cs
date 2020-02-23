using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gs.DB.SqlServer
{
    /// <summary>
    /// 反射测试类
    /// </summary>
    public class ReflectionTest
    {
        #region Identity
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ReflectionTest()
        {
            Console.WriteLine("这里是{0}无参数构造函数", this.GetType());
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="name"></param>
        public ReflectionTest(string name)
        {
            Console.WriteLine("这里是{0} 有参数构造函数", name);
        }

        public ReflectionTest(int id)
        {
            Console.WriteLine("这里是{0} 有参数构造函数", id);
        }
        #endregion

        #region Method
        /// <summary>
        /// 无参方法
        /// </summary>
        public void Show1()
        {
            Console.WriteLine("这里是{0}的Show1", this.GetType());
        }
        /// <summary>
        /// 有参数方法
        /// </summary>
        /// <param name="id"></param>
        public void Show2(int id)
        {

            Console.WriteLine("这里Show2，参数是{0}", id);
        }
        /// <summary>
        /// 重载方法之一
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Show3(int id, string name)
        {
            Console.WriteLine("调用参数为id、name的show3方法，参数为：{0}、{1}", id, name);
        }
        /// <summary>
        /// 重载方法之二
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public void Show3(string name, int id)
        {
            Console.WriteLine("调用参数为id、name的show3方法，参数为：{0}、{1}", id,name);
        }
        /// <summary>
        /// 重载方法之三
        /// </summary>
        /// <param name="id"></param>
        public void Show3(int id)
        {
            Console.WriteLine("调用参数为id的show3方法，参数为：{0}",id);
        }
        /// <summary>
        /// 重载方法之四
        /// </summary>
        /// <param name="name"></param>
        public void Show3(string name)
        {
            Console.WriteLine("调用参数为name的show3方法，参数为：{0}", name);
        }
        /// <summary>
        /// 重载方法之五
        /// </summary>
        public void Show3()
        {

            Console.WriteLine("这是无参方法Show3()");
        }
        /// <summary>
        /// 私有方法
        /// </summary>
        /// <param name="name"></param>
        private void Show4(string name)
        {
            Console.WriteLine("调用参数为name的Show4方法，参数为：{0}", name);
        }
        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="name"></param>
        public static void Show5(string name)
        {
            Console.WriteLine("调用参数为name的Show5方法，参数为：{0}", name);
        }
        #endregion
    }
}
