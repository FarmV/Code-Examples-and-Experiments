using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_poject
{
    internal class Extensions1
    {
        static async Task Main()
        {

            test4 test = new test4(567562151, 1, "Z");

            (int ID, object MessageCollection, string Name) = test.Deconstruct();






            //dynamic test
            object log = test;

            dynamic logDynamic = log;

            (int id, object col, string name) = (logDynamic.id, logDynamic.messageCollection, logDynamic.name);
            

           



        }

    }
    public static class MyExtensions
    {
        public static (int ID , object MessageCollection, string Name) Deconstruct<T>(this T test) where T : test4
        {
            return (test.id, test.messageCollection, test.name);
        }
    }

    public class test4
    {
        private int _id;

        public int id
        {
            get
            {
                return _id;
            }
        }

        private object _messageCollection;

        public object messageCollection
        {
            get
            {
                return _messageCollection;
            }
        }

        private string _name;

        public test4(int id, object messageCollection, string nameA)
        {
            _id = id;
            _messageCollection = messageCollection;
            _name = nameA;
        }

        public string name
        {
            get
            {
                return _name;
            }
        }


    }
}
