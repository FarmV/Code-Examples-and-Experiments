using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
    internal class Indesator2
    {
        class TaskHelper<Output>
        {
            async Task<Output> Check(object res)
            {
                if (res is not Task<(bool Result, Output Output, string comment)> task) throw new InvalidOperationException("Объект не является указанным типом");

                (bool Result, Output Output, string comment) resTask = await task;

                if (resTask.Result is not true)
                {
                    Console.WriteLine(resTask.comment); // сделай что то?
                    return resTask.Output;
                }


                throw new NotImplementedException();
            }
            internal async Task<Output?> Checkcer(Task<(Output? Output, string comment)> task, Action<string>? action = null, string? over = null,
                [CallerMemberName] string? callFunc = null, [CallerFilePath] string? document = null, [CallerLineNumber] int? line = null)
            {
                if (task is null) throw new InvalidOperationException("Task is null");

                var resTask = await task;
                if (resTask.Output is null)
                {
                    Console.WriteLine(@$"{DateTime.Now}{Environment.NewLine}В документе {document}.{Environment.NewLine}В строке '{line}' метода '{callFunc}', ожидавшим результат '{typeof(Output)}', была возращенна пустая ссылка. Причина - '{resTask.comment}'"); // типа лог

                    if (action is not null)
                    {
                        if (over is not null) action.Invoke(over);
                        else
                        {
                            action.Invoke(resTask.comment);
                        }
                    }

                }
                else
                {
                    return resTask.Output;
                }
                return default;
            }

        }

        static async Task Main()
        {
            TaskHelper<A1> taskHelper = new TaskHelper<A1>();

            var collection = new CollectionA();

            if (await taskHelper.Checkcer(collection["eденица"], new Action<string>((str) => { Console.WriteLine(str); }),"Всё пошло наперекосяк") is not A1 res) return;

            Console.WriteLine($"{res._name}");

        }
    }

    class CollectionA
    {
        A1[] Functions = { new A1("Еденица"), new A1("Двойка") };

        public Task<(A1? output, string comment)> this[string functionName] => FindFunction(functionName); // если set, то на входе требуется такая же сигнатура пример collection[Task<(A1? output, string comment)>]  


        private Task<(A1? output, string comment)> FindFunction(string function)
        {
            for (int number = 0;number < Functions.Length;number++)
            {
                if (Functions[number] is not A1 res) return Task.FromResult(((A1?, string))(null, $"Фукция {nameof(A1)} с заданным именем не найдена"));

                if (res?.Name is not string name) return Task.FromResult(((A1?, string))(null, $"Имя фукции {nameof(A1.Name)} являлось NULL"));

                if (name == function)
                {
                    return Task.FromResult(((A1?, string))(res, $"Функция успешно выполнена")); ;
                }
            }
            return Task.FromResult(((A1?, string))(null, $"Фукция {nameof(A1)} с заданным именем не найдена"));
        }


        internal Task<(bool result, A1 output, string comment)> Add(A1 function)
        {
            Array.Resize(ref Functions, Functions.Length + 1);

            Functions[Functions.Length - 1] = function;

            if (Functions[Functions.Length - 1] is null) throw new NullReferenceException($"Ошибка в функции {nameof(Add)}");

            return Task.FromResult((true, Functions[Functions.Length - 1], "Функция успешно инициализирована"));
        }
    }

    internal class A1
    {
        internal string _name;

        internal string Name => _name;
        
        public A1(string name)
        {
            _name = name;
        }
    }
}

