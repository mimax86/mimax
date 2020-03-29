using System.Collections.Generic;

namespace Mimax.Susquehanna
{
    public class StackMachine
    {
        public static int ErrorValue = -1;

        public static int Solution(string input)
        {
            var stack = new Stack<MachineValue>();
            var commands = input.Split();

            foreach (var command in commands)
            {
                if (MachineValue.TryParse(command, out var value))
                {
                    if (value == MachineValue.Invalid)
                        return ErrorValue;

                    stack.Push(value);
                    continue;
                }

                switch (command)
                {
                    case "DUP":
                        if (stack.Count < 1)
                            return ErrorValue;
                        stack.Push(stack.Peek());
                        continue;
                    case "POP":
                        if (stack.Count < 1)
                            return ErrorValue;
                        stack.Pop();
                        continue;
                    case "+":
                        if (stack.Count < 2)
                            return ErrorValue;
                        var addResult = stack.Pop().Add(stack.Pop());
                        if (addResult == MachineValue.Invalid)
                            return ErrorValue;
                        stack.Push(addResult);
                        continue;
                    case "-":
                        if (stack.Count < 2)
                            return ErrorValue;
                        var subtractResult = stack.Pop().Subtract(stack.Pop());
                        if (subtractResult == MachineValue.Invalid)
                            return ErrorValue;
                        stack.Push(subtractResult);
                        continue;
                }

                return ErrorValue;
            }

            if (stack.Count < 1)
                return ErrorValue;

            return stack.Pop().Value;
        }
    }
}