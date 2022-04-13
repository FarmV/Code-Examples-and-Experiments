using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_poject
{
    internal class MyThread
    {
        static async Task Main()
        {
            string[] asdasd = new string[] { "a", "b", "c" };

            string[] asd = asdasd.Where(x => x == "n").ToArray();

            Task.Run(() =>
            {
                while (true)
                {
                };
            }).Start();





                Console.WriteLine(asd.Count());
            Thread.CurrentThread.Name = "Мой оновной поток приложения";
            AAA a1 = new AAA();
            Console.WriteLine("Hello");

            AutoResetEvent waitHandle = new AutoResetEvent(false);

            // var res1 = await a1.GetStr().ConfigureAwait(true);

            Task<string> abc = a1.GetStr();
        
            Thread.Sleep(1000);
            Console.WriteLine("a1-ok");
            Thread.Sleep(1000);
            Console.WriteLine("a2-ok");
            Thread.Sleep(1000);
            Console.WriteLine("a3-ok");
            Thread.Sleep(1000);
  
            Console.WriteLine(await abc.ConfigureAwait(false));
        }



        internal class AAA
        {
            internal async Task<string> GetStr(AutoResetEvent? resetEvent = null) => await Task.Run(() =>
            {
                Thread.Sleep(5000);
                if(resetEvent is not null) resetEvent.Set();
                //Thread.CurrentThread.Name = "GetStr";
                return "It's ok";
                
            });
           
        }






    }


}
