using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_poject
{
    internal class UnivarsalType
    {
        static async Task Main()
        {



            int aasd = default;

            AddF<Task>("Еденица", new Task(async () =>
            {
                Task<int>? TaskW = await Task.Factory.StartNew<int>(new Func<int>(() => TestType.A())).              

                ContinueWith<Task<int>>(new Func<Task<int>, Task<int>>(async (TaskIntA) =>
                {
                
                    int abcd = await await Task.Factory.StartNew<Task<int>>(new Func<Task<int>>(() => { return TaskIntA; }));
                
                    int result = TestType.B(abcd);
                
                    var res1 = TestType.C(result);
                
                    var res = TestType.D(res1);
                
                    aasd = res;
                
                    return res;
                
                }));
                Console.WriteLine(await TaskW);                                          
            }));                                                                          // Первый уровень Task, остальные только через Task.Factory.StartNew<>()  - предположительно.
                                                                                         
                                                                                          //Task.Factory.StartNew = работает            
                                                                                         
                                                                                         
                                                                                           //int c = await new Task<int>(() => 1); не работает
                     
            AddF("Test1", new Task<int>(() => { Console.WriteLine("ОК-OK"); return 1; }));


            Call<Task>("Еденица");          

            Call<Task<int>>("Test1"); // Если при вызове указывать тип не соотвествубщий сигнатуре например Call<Task> вместо Call<Task<int>> то будет исключние

            // У удного и того же статического класса параметризированного T. Будет свой отдельный статический класс. Он создаеётся в момент помещеёния новго типа? 
            while (true)
            {
                //await Task.Delay(200);

                await Task.Yield();
            }
        }
        internal static class F<T> where T : Task
        {
            internal static Dictionary<string, T> FunctionsCallback = new Dictionary<string, T>();

            internal static string[] TestStr = new string[]
            {
                $"{typeof(T)}"
            };             
        }
        internal static void AddF<T>(string key, T callback) where T : Task
        {
            
            F<T>.FunctionsCallback.Add(key, callback);
        }
        internal static void Call<T>(string key) where T : Task
        {
            Debug.WriteLine($"{F<Task>.TestStr[0]}");
            Debug.WriteLine($"{F<Task<int>>.TestStr[0]}");
            Debug.WriteLine($"{F<Task<long>>.TestStr[0]}");

            F<T>.FunctionsCallback[key].Start();
        }
    }


    internal static class TestType
    {
        internal static int A() => 12345;

        internal static int B(int nunber) => nunber + 5;

        internal static int C(int nunber) => nunber + 9;
               
        internal static int D(int nunber) => nunber + -11;
               
    }
}
