using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpresionTestProject
{
    public static class ExprPrinter
    {
        public static void Print(IExpression expr, int indent = 0)
        {
            string pad = new string(' ', indent);

            switch (expr)
            {
                case ConstantExpression c:
                    Console.WriteLine($"{pad}Constant: {c.Value}");
                    break;

                case VariableExpression v:
                    Console.WriteLine($"{pad}Variable: {v.Name}");
                    break;

                case BinaryExpression b:
                    Console.WriteLine($"{pad}Binary: {b.Sign}");
                    Print(b.Left, indent + 2);
                    Print(b.Right, indent + 2);
                    break;

                case FunctionExpression f:
                    Console.WriteLine($"{pad}Function: {f.Kind}");
                    Print(f.Argument, indent + 2);
                    break;

                default:
                    Console.WriteLine($"{pad}Unknown expression");
                    break;
            }
        }
    }
}
