using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_poject
{
    internal class BitOperations
    {



        //-- & Битовое «И» [полученное число] & [битовая маска].
        static Task Main()
        {
            //Создаем объект 
            StateController tmpCtrl = new StateController(129);

            Console.WriteLine(tmpCtrl.Unit1State == true ? "Первый блок работает" : "Первый блок не работает");

            return Task.CompletedTask;
        }
    }
    class StateController
    {
        //Конструктор, принимает общее состояние в виде байтового чила
        public StateController(byte aComplexState)
        {
            state = aComplexState;
        }

        //Возвращает состояние первого блока
        public bool Unit1State
        {
            get => ( state & 1 ) is 1;

               // //Получить промежуточный результат, наложив маску битовую маску 00000001 на общий статус
               //int result = state & 1; //0x01(в hex) это 00000001 (в bin)

               // //Если результат равен 0
               // if (result == 0)
               //     return false; //Блок не работает
               // else
              
        }

        //Возвращает состояние второго блока
        public bool Unit2State
        {
            get
            {
                //Получить промежуточный результат, наложив маску битовую маску 00000010 на общий статус
                int result = state & 2; //0x02(в hex) это 00000010 (в bin)

                //Если результат равен 0
                if (result == 0)
                    return false; //Блок не работает
                else
                    return true; //Блок работает (результат не равен нулю)
            }
        }

        //Возвращает состояние третьего блока
        public bool Unit3State
        {
            get
            {
                //Получить промежуточный результат, наложив маску битовую маску 00000100 на общий статус
                int result = state & 4; //0x04(в hex) это 00000100 (в bin)

                //Если результат равен 0
                if (result == 0)
                    return false; //Блок не работает
                else
                    return true; //Блок работает (результат не равен нулю)
            }
        }

        /*Свойства Unit4State - Unit7State не показаны для сокращения кода*/

        //Возвращает состояние восьмого блока
        public bool Unit8State
        {
            get
            {
                //Получить промежуточный результат, наложив маску битовую маску 00000100 на общий статус
                int result = state & 128; //0x80(в hex) это 10000000 (в bin)

                //Если результат равен 0
                if (result == 0)
                    return false; //Блок не работает
                else
                    return true; //Блок работает (результат не равен нулю)
            }
        }

        //Общий статус (в нем закодировано состояние всех блоков)
        private byte state;
    }
    class StateBuilder
    {
        //Конструктор, принимает восемь булевых значений (состояний восьми блоков)
        public StateBuilder(bool Unit1State, bool Unit2State,
            bool Unit3State, bool Unit4State, bool Unit5State,
            bool Unit6State, bool Unit7State, bool Unit8State)
        {
            unit1State = Unit1State;
            unit2State = Unit2State;
            unit3State = Unit3State;
            unit4State = Unit4State;
            unit5State = Unit5State;
            unit6State = Unit6State;
            unit7State = Unit7State;
            unit8State = Unit8State;
        }

        //Формирует результирующее число
        public byte Build()
        {
            //Сбрасываем все биты числа в "0"
            int result = 0;

            //Если нужно включить первый блок
            if (unit1State == true)
                result = result | 1; //0x01 (hex) = 00000001 (bin)

            //Если нужно включить второй блок
            if (unit2State == true)
                result = result | 2; //0x02 (hex) = 00000010 (bin)

            //Если нужно включить третий блок
            if (unit3State == true)
                result = result | 4; //0x04 (hex) = 00000100 (bin)

            //Если нужно включить четвертый блок
            if (unit4State == true)
                result = result | 8; //0x08 (hex) = 00001000 (bin)

            //Если нужно включить пятый блок
            if (unit5State == true)
                result = result | 16; //0x10 (hex) = 00010000 (bin)

            //Если нужно включить шестой блок
            if (unit6State == true)
                result = result | 32; //0x20 (hex) = 00100000 (bin)

            //Если нужно включить седьмой блок
            if (unit7State == true)
                result = result | 64; //0x40 (hex) = 01000000 (bin)

            //Если нужно включить восьмой блок
            if (unit8State == true)
                result = result | 128; //0x80 (hex) = 10000000 (bin)

            //Обрезаем результат до байта
            return (byte)result;
        }

        //Состояния блоков (от первого до 8-го)
        private bool unit1State;
        private bool unit2State;
        private bool unit3State;
        private bool unit4State;
        private bool unit5State;
        private bool unit6State;
        private bool unit7State;
        private bool unit8State;
    }
}
