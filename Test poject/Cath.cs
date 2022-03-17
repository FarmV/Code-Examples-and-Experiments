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
            catch (Exception ex)
            {
                if (ex.Data.Count is 0 || ex.Data.Count > 1) throw;
                if (await ExptHelper(ABS.VsFail.isNullargument, ex)) Console.WriteLine("Ок");
                else
                {
                    Console.WriteLine("Not Ok");
                    return;
                }
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
            internal static Task<Exception> Exept(DictionaryEntry entry)
            {
                Exception res = new Exception();
                res.Data[entry.Key] = entry.Value;
                return Task.FromResult(res);
            }
            internal enum VsFail
            {
                isNullargument = 1, fols2 = 2, fols3 = 3, fols4 = 4, fols5 = 5,
            }

            internal static async Task<string> Vs(string main, int pars)
            {
                DictionaryEntry[] vsFail = new DictionaryEntry[]
                {
                    new DictionaryEntry(VsFail.isNullargument,"не хочу"),        //0
                    new DictionaryEntry(VsFail.fols2,"не буду"),                 //1
                    new DictionaryEntry(VsFail.fols3,"не могу"),                 //2
                    new DictionaryEntry(VsFail.fols4,"отстань"),                 //3
                    new DictionaryEntry(VsFail.fols5,"Могу, но надо подумать!"), //4
                };

                throw await Exept(vsFail[3]);

                return "OK";
            }
        }
    }
}
