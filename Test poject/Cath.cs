using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_poject
{
    internal class Cath
    {


        static async Task Main()
        {
            try
            {
                await ABS.Vs("1", 1);
            }
            catch(ExReport ex)
            {
                if (ex.Data.Count is 0 || ex.Data.Count > 1) throw;
                if (await ExptHelper(ABS.VsFail.Input_Argument_Main_is_Null, ex)) Console.WriteLine("Ок");
                else
                {
                    Console.WriteLine("Not Ok");
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static Task<bool> ExptHelper<T>(T res, Exception ex) where T : Enum
        {
            foreach (DictionaryEntry Dictionary in ex.Data)
            {
                if (Equals((Enum)Dictionary.Key, (Enum)res)) return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        internal static class ABS
        {
            //internal static Task<Exception> Exept(DictionaryEntry entry)
            //{
            //    Exception res = new Exception();
            //    res.Data[entry.Key] = entry.Value;
            //    return Task.FromResult(res);
            //}
            internal enum VsFail
            {
                Input_Argument_Main_is_Null = 1, 
                Input_Argument_Pars_is_Out_Of_Range = 2, 
                Invalid_Operation_2 = 3, 
                Invalid_Operation_3 = 4, 
                Invalid_Operation_4 = 5,
            }

            internal static async Task<string> Vs(string main, int pars)
            {
                DictionaryEntry[] vsFail = new DictionaryEntry[]
                {
                    new DictionaryEntry(VsFail.Input_Argument_Main_is_Null,"не хочу"),         //0
                    new DictionaryEntry(VsFail.Input_Argument_Pars_is_Out_Of_Range,"не буду"), //1
                    new DictionaryEntry(VsFail.Invalid_Operation_2,"не могу"),                 //2
                    new DictionaryEntry(VsFail.Invalid_Operation_3,"отстань"),                 //3
                    new DictionaryEntry(VsFail.Invalid_Operation_4,"Могу, но надо подумать!"), //4
                };

                throw new ExReport(vsFail[0]);
                throw new Exception().MyRport(vsFail[0]);
                return "OK";
            }
        }
    }
    internal static class ExS
    {
        internal static Exception MyRport(this Exception ex, DictionaryEntry report)
        {
            ex.Data[report.Key] = report.Value;
            return ex;
        }
    }
    internal class ExReport: Exception
    {     
        public ExReport(DictionaryEntry report)
        {
            base.Data[report.Key] = report.Value;
        }
    }
}
