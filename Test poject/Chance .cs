using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_poject
{
    internal class Chance
    {
        //Предположим есть некая коллекция предметов, и мы хотим, чтобы они доставались из лутбокса с определённым шансом.
        internal static List<string> LutBox = new List<string>()
        { "Предмет1", "Предмет2", "Предмет3", "Предмет4", "Предмет5", "Предмет6", "Предмет7", "Предмет8", "Предмет9", "Предмет10", };

        static async Task Main()
        {
            //Добавим для предметов опредедённые шансы выпадения
            IEnumerable<(string Item, decimal chance)>? res = LutBox.Select(x => // Select - Преобразованиее каждого элемента последовательности во что угодно. Возрат IEnumerable<>
            {
               if(x is "Предмет1") return ((string Item,decimal chance))(x,10);
               if (x is "Предмет2") return ((string Item, decimal chance))(x, 20);
               if (x is "Предмет3") return ((string Item, decimal chance))(x, 30);
               if (x is "Предмет4") return ((string Item, decimal chance))(x, 40);
               if (x is "Предмет5") return ((string Item, decimal chance))(x, 50);
               if (x is "Предмет6") return ((string Item, decimal chance))(x, 1);
               if (x is "Предмет7") return ((string Item, decimal chance))(x, 2);
               if (x is "Предмет8") return ((string Item, decimal chance))(x, 0.5);
               if (x is "Предмет9") return ((string Item, decimal chance))(x, 0.0005);
               if (x is "Предмет10") return ((string Item, decimal chance))(x, 0.00000000001);


                throw new InvalidOperationException("Присутсвует предмет, для которого не указан шанс выпадения");
            });

            // int a = await GetRandom();

            
            decimal b = (decimal)1 / (int.MaxValue - 1);

            decimal c = (decimal)b * 100;

        }
        private static Random rand = new Random();
        internal static Task<int> GetRandom()
        {
           

            return Task.FromResult(rand.Next());
        }
        //Добавим для предметов опредедённые шансы выпадения
        internal static Task SetLutBoxChance<T>(IEnumerable<(T item, decimal chance)>collection) where T: notnull
        {
            throw new NotImplementedException();
        }








        
    }
    
}
