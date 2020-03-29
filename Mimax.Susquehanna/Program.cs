using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Mimax.Susquehanna
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    //public int solution1(int[] A)
    //{
    //var first = (index: -1, value: int.MaxValue);
    //var second = (index: -1, value: int.MaxValue);
    //    for (var i = 0; i < A.Length; i++)
    //{
    //    if (A[i] < first.value)
    //    {
    //        second = first;
    //        first = (i, A[i]);
    //    }
    //    else if (A[i] < second.value && A[i] != first.value)
    //        second = (i, A[i]);
    //}
    //return first.value + second.value;
    //}

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
}