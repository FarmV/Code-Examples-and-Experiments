using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Test_poject
{
    internal class Cancel
    {

        static async Task Main()
        {

            var cTS = new CancellationTokenSource();
            var cencel = cTS.Token;

            A2 a2 = new A2(cencel);

            var cla = new A1(cTS, cencel);




            int count = default;


            //taski.AsParallel().ForAll<Task>( x=> x.Start());
            //  Parallel.ForEach(taski, x => x.Start());


            while (cencel.IsCancellationRequested is not true)
            {
                await Task.Delay(500);
                count++;
                Console.WriteLine(count);
                if (count >= 20)
                {
                    cTS.Cancel();
                }
            }
            while (true)
            {
                await Task.Delay(100);
            }


        }
    }
    internal class A2
    {
        private static int count = default;

        CancellationToken Ct = default;
        public A2(CancellationToken cencel)
        {
            Ct = cencel;
            Str(cencel);
        }
        private void Str(CancellationToken cencel)
        {
            Task[] tasks = new Task[]
            {
              new Task(async () =>
              {
                while (true)
                {
                    await Task.Delay(400);
                    Console.WriteLine(await Tt());
                      if(cencel.IsCancellationRequested is true)
                      {
                         return;
                      }
                }
              }, cencel)
            };


            tasks[0].Start();

        }
        private static async Task<string> Tt()
        {
            var cla = new A1(new CancellationTokenSource(), new CancellationToken());
            Task<string?> result = await cla.myTaskChek().ContinueWith<Task<string?>>(new Func<Task<string?>, Task<string?>>(async (a) =>
            {
                string res123 = await a + " Это победа";
                return res123;
            }));

            if (await result is not string res) throw new Exception();

            return res;
        }


    }



}
internal class A1
{
    public A1(CancellationTokenSource? cts, CancellationToken ct)
    {
        Cts = cts;
        this.ct = ct;
    }

    internal CancellationTokenSource? Cts;
    private CancellationToken ct;


    internal async Task<string?> myTaskChek(CancellationToken? token = default)
    {
        string abc = "Это тест - Good job";
        await Task.Factory.StartNew(new Action<object?>((input) => { Console.WriteLine(input); }), abc);

        return "Тест завершён";
    }

}

