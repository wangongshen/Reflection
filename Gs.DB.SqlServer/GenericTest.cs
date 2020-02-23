using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gs.DB.SqlServer
{
    public class GenericClass<T, W, X>
    {
        public void Show(T t, W w, X x)
        {

            Console.WriteLine("方法1");
            Console.WriteLine("t.type={0},w.type={1},x.type={2}", t.GetType().Name, w.GetType().Name, x.GetType().Name);
        }

        public void ShowA()
        {
            Console.WriteLine("调用ShowA");
        }


    }

    public class GenericMethod
    {
        public void Show<T, W, X>(T t, W w, X x)
        {

            Console.WriteLine("方法2");
            Console.WriteLine("t.type={0},w.type={1},x.type={2}", t.GetType().Name, w.GetType().Name, x.GetType().Name);
        }
    }

    public class GenericDouble<T>
    {
        public void Show<W, X>(T t, W w, X x)
        {

            Console.WriteLine("方法3");
            Console.WriteLine("t.type={0},w.type={1},x.type={2}", t.GetType().Name, w.GetType().Name, x.GetType().Name);
        }
    }
}
