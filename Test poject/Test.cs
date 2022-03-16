using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FunctionsFV;



namespace Test_poject
{
    internal class Test
    {
       
        internal static Dictionary<string, BaseFunc> MyFunctionList = new Dictionary<string, BaseFunc>()
        {
            {"Кот", new A1("Кот") }
        };

        static async Task Main()
        {
            if (MyFunctionList["Кот"] is not A1 res) throw new NullReferenceException();

            Enum? a = res.getE();
            var cc = Enum.Format(typeof(A1.Reason), a, "G");


            (string? Result, BaseFunc.BadResonTaskM enumM, Exception? ex) = await BaseFunc.taskM();


            //if (enumM.HasFlag(BaseFunc.BadResonTaskM.Ok))
            //{
            //    Console.WriteLine("IS OK");
            //}
            //else
            //{
            //    if (enumM.HasFlag(BaseFunc.BadResonTaskM.Throw)) { Console.WriteLine($"{ex.Message}"); return; }

            //    Console.WriteLine(Enum.Format(typeof(BaseFunc.BadResonTaskM), enumM, "G"));

            //}




            // var cbvb = await BaseFunc.taskM().GetT();

            Task<(string? Result, BaseFunc.BadResonTaskM enumM, Exception? ex)>? ccasd = BaseFunc.taskM();

            Console.WriteLine(GetRandom());
            Console.WriteLine(GetRandom());
            Console.WriteLine(GetRandom());

            //    (string car, BaseFunc.BadResonTaskM mm) cccas = await sta.GetT<Task<(string, BaseFunc.BadResonTaskM, Exception?)>, BaseFunc.BadResonTaskM>(new object(), BaseFunc.taskM());
        }

        //Этот метод должен возвращать случайное значение
        static int GetRandom()
        {
            //Создание объекта для генерации чисел (с указанием начального значения)
            Random rnd = new Random();

            //Получить случайное число 
            int value = rnd.Next(1,6);

            //Вернуть полученное значение
            return value;
        }

    }

    internal static class sta
    {

        enum Result
        {
            Ok = 1,
            Throw =2
        }
        internal static async Task<(T?, Enum)> GetTT<T, T2>(T? res) where T : Task<(T? result, T2 reson, Exception? ex)> where T2 : Enum
        {
            if (res is null) throw new NullReferenceException();

            (T? result, Enum reson, Exception? ex) = await res;

            if (reson.HasFlag(Result.Ok)) return (result, reson);

            if (reson.HasFlag(Result.Throw)) Console.WriteLine($"{ex.Message}");

            return (result, reson);
        }

        internal static async Task<(T?,Enum)> GetT<T,T2>(this object obj,T? res) where T: Task<(T? result,T2 reson, Exception? ex)> where T2: Enum
        {
            if (res is null) throw new NullReferenceException();

            (T? result, Enum reson, Exception? ex) = await res;

            if (reson.HasFlag(Result.Ok)) return (result, reson);

            if (reson.HasFlag(Result.Throw)) Console.WriteLine($"{ex.Message}");

            return (result, reson);
        }
    }
    //class CollectionA
    //{
    //    A1[] Functions = { new A1("Еденица"), new A1("Двойка") };

    //    public Task<(A1? output, string comment)> this[string functionName] => FindFunction(functionName);

    //    private Task<(A1? output, string comment)> FindFunction(string function)
    //    {
    //        for (int number = 0;number < Functions.Length;number++)
    //        {
    //            if (Functions[number] is not A1 res) return Task.FromResult(((A1?, string))(null, $"Фукция {nameof(A1)} с заданным именем не найдена"));

    //            if (res?.Name is not string name) return Task.FromResult(((A1?, string))(null, $"Имя фукции {nameof(A1.Name)} являлось NULL"));

    //            if (name == function)
    //            {
    //                return Task.FromResult(((A1?, string))(res, $"Функция успешно выполнена")); ;
    //            }
    //        }
    //        return Task.FromResult(((A1?, string))(null, $"Фукция {nameof(A1)} с заданным именем не найдена"));
    //    }
    //    internal Task<(bool result, A1 output, string comment)> Add(A1 function)
    //    {
    //        Array.Resize(ref Functions, Functions.Length + 1);

    //        Functions[Functions.Length - 1] = function;

    //        if (Functions[Functions.Length - 1] is null) throw new NullReferenceException($"Ошибка в функции {nameof(Add)}");

    //        return Task.FromResult((true, Functions[Functions.Length - 1], "Функция успешно инициализирована"));
    //    }
    //}
}

namespace FunctionsFV
{

    internal abstract class BaseFunc
    {
        protected BaseFunc()
        {

        }

        protected internal virtual event EventHandler<EventArgs>? Complite;
        protected internal bool IsCheceked
        {
            get; private set;
        }
        protected string? Name
        {
            get;
        }

        /// <summary>
        /// Это базовый метод
        /// </summary>
        /// <returns></returns>
        protected internal virtual Task<(object? result, string comment)> ChecStart()
        {
            return Task.FromResult(((object?, string))(null, "Реализация по умолчаню"));
        }
        protected internal abstract Task<(object? result, string comment)> Start();

        protected internal virtual Task<(bool result, string comment)> Cancel() => Task.FromResult(((bool, string))(false, "Функция отмены не реализованна"));

        private enum Reason
        {
            None,
            BadFuntcion
        }
        protected internal virtual Enum getE()
        {
            return Reason.None;
        }



        internal enum BadResonTaskM
        {
            Ok = 1, Throw = 2, crash = 4
        }
        internal static Task<(string? Result, BadResonTaskM enumM, Exception? ex)> taskM()
        {

            try
            {
                throw new NullReferenceException("Хуй");
                return Task.FromResult(((string?, BadResonTaskM, Exception?))("Good job", BadResonTaskM.Ok, null));
            }
            catch (Exception exception)
            {
                return Task.FromResult(((string?, BadResonTaskM, Exception?))(null, BadResonTaskM.Throw, exception));
            }


        }



    }
    internal class A1: BaseFunc
    {
        [Flags]
        internal enum Reason
        {
            None = 1,
            Bad = 2,
            Locked = 4,
            Trash = 8
        }

        protected internal override Enum getE()
        {
            return Reason.Trash | Reason.Locked | Reason.Bad;
        }

        internal A1 Gets
        {
            get;
        }
        internal string Name
        {
            get;
        }


        public A1(string name)
        {
            Gets = MyCheck(this);
            Name = name;
        }

        protected internal override event EventHandler<EventArgs>? Complite;

        public T MyCheck<T>(T func)
        {
            return func;
        }

        protected internal override Task<(object? result, string comment)> Start()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Это потомок
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected internal override Task<(object? result, string comment)> ChecStart()
        {
            return Task.FromResult(((object?, string))(null, "Реализация не по умолчаню"));
        }

        protected internal override Task<(bool result, string comment)> Cancel()
        {
            return base.Cancel();
        }
    }

    internal class A2
    {

    }
}