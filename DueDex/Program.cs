using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DueDex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
            //while (true)
            //{
            //    var key = Console.ReadKey();
            //    if(key.Modifiers.HasFlag(ConsoleModifiers.Control) && key.Key == ConsoleKey.C)
            //    {
            //        Console.WriteLine("Shutting down application.");
            //        break;
            //    }
            //}
            //Console.ReadLine();

            var fisrt = (index: -1, value: int.MaxValue);


            var r = solution("13 DUP");

            var b = 1 << 20;

        }

        public static int ErrorValue = -1;
  
        public static int solution(string S)
        {

            var stack = new Stack<MachineValue>();
            var args = S.Split(new char[] { ' ' });

            for (int i = 0; i < args.Length; i++)
            {
                MachineValue value;
                if (MachineValue.TryParse(args[i], out value))
                {
                    stack.Push(value);
                    continue;
                }

                if (value.Equals(MachineValue.Invalid))
                    return ErrorValue;

                switch (args[i])
                {
                    case "DUP":
                        stack.Push(stack.Peek());
                        continue;
                    case "POP":
                        stack.Pop();
                        continue;
                    case "+":
                        if (stack.Count < 2)
                            return ErrorValue;
                        var addResult = stack.Pop().Add(stack.Pop());
                        if (addResult.Equals(MachineValue.Invalid))
                            return ErrorValue;
                        stack.Push(addResult);
                        continue;
                    case "-":
                        if (stack.Count < 2)
                            return ErrorValue;
                        var substractResult = stack.Pop().Substract(stack.Pop());
                        if (substractResult.Equals(MachineValue.Invalid))
                            return ErrorValue;
                        stack.Push(substractResult);
                        continue;
                }

                return ErrorValue;
            }

            return stack.Pop().Value;
        }

        public struct MachineValue
        {
            public static MachineValue Invalid = new MachineValue(int.MinValue);
            public static MachineValue Max = new MachineValue(1 << 20 - 1);
            public static MachineValue Min = new MachineValue(0);

            public int Value { get; }

            private MachineValue(int value)
            {
                Value = value;
            }

            public static bool TryParse(string s, out MachineValue value)
            {
                int temp;
                if (!int.TryParse(s, out temp))
                {
                    value = Invalid;
                    return false;
                }

                if (temp < Min.Value || temp > Max.Value)
                {
                    value = Invalid;
                    return false;
                }

                value = new MachineValue(temp);
                return true;
            }

            public MachineValue Add(MachineValue value)
            {
                var temp = Value + value.Value;
                if (temp < Min.Value || temp > Max.Value)
                    return Invalid;
                return new MachineValue(temp);
            }

            public MachineValue Substract(MachineValue value)
            {
                var temp = Value - value.Value;
                if (temp < Min.Value || temp > Max.Value)
                    return Invalid;
                return new MachineValue(temp);
            }
        }


        //Naive implementation
        //public int solution(string S)
        //{

        //    var stack = new Stack<int>();
        //    var args = S.Split(new char[] { ' ' });

        //    for (int i = 0; i < args.Length; i++)
        //    {
        //        int value;
        //        if (int.TryParse(args[i], out value))
        //        {
        //            stack.Push(value);
        //            continue;
        //        }

        //        switch (args[i])
        //        {
        //            case "DUP":
        //                stack.Push(stack.Peek());
        //                continue;
        //            case "POP":
        //                stack.Pop();
        //                continue;
        //            case "+":
        //                if (stack.Count < 2)
        //                    return ErrorValue;
        //                stack.Push(stack.Pop() + stack.Pop());
        //                continue;
        //            case "-":
        //                if (stack.Count < 2)
        //                    return ErrorValue;
        //                stack.Push(stack.Pop() - stack.Pop());
        //                continue;
        //        }

        //        return ErrorValue;
        //    }

        //    return stack.Pop();
        //}

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Shutting down application.");
        }

        public int solution(int[] A)
        {
            var first = (index: -1, value: int.MaxValue);
            var second = (index: -1, value: int.MaxValue);
            for (var i = 0; i < A.Length; i++)
            {
                if (A[i] < first.value)
                {
                    second = first;
                    first = (i, A[i]);
                }
                else if (A[i] < second.value && A[i] != first.value)
                    second = (i, A[i]);
            }
            return first.value + second.value;
        }
    }
}
