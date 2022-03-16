using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_poject
{
    internal class EnumFalgs
    {
        [Flags]
        enum MyEnum :ulong
        {
            // None = 0L, // отсутвие нуля - считается присутвсием такового, даже если его нету.
            a2 = 1,
            a3 = 2,
            a4 = 4,
            a5 = 8,
            a6 = 16,
            a7 = 32,
            a8 = 64,
            a9 = 128,
            a10 = 256,
            a11 = 512,
            a12 = 1024,
            a13 = 2048,
            a14 = 4096,
            a15 = 8192,
            a16 = 16384,
            a17 = 32768,
            a18 = 65536,
            a19 = 131072,
            a20 = 262144,
        }
        static async Task Main()
        {
            //string[] m = Enum.GetNames<MyEnum>();
            MyEnum[] v = Enum.GetValues<MyEnum>();
            MyEnum IAm = default;
            for (int i = 1;i < v.Length -1;i++)
            {
                IAm = IAm | v[i];
            }
            MyEnum test1 = MyEnum.a10 | MyEnum.a18 | MyEnum.a4|MyEnum.a20; // сравниваем набор
            Console.WriteLine($"{IAm.HasFlag(test1)}");

            
        }
    }
}
