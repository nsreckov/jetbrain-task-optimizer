using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpresionTestProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Build expression with DUPLICATES: sin(7*(2+x)) - 7*(2+x) + cos(x)
            var two = new ConstantExpression(2);
            var seven = new ConstantExpression(7);
            var x = new VariableExpression("x");

            var add = new BinaryExpression(OperatorSign.Plus, two, x);
            var multi = new BinaryExpression(OperatorSign.Multiply, seven, add);

            var sinExpr = new FunctionExpression(FunctionKind.Sin, multi);
            var cosExpr = new FunctionExpression(FunctionKind.Cos, x);

            var subtract = new BinaryExpression(OperatorSign.Minus, sinExpr, multi);
            var finalExpr = new BinaryExpression(OperatorSign.Plus, subtract, cosExpr);

            Console.WriteLine("=== BEFORE OPTIMIZATION ===");
            Console.WriteLine("Expression tree:");
            ExprPrinter.Print(finalExpr);
            Console.WriteLine();

            // Check BEFORE optimization
            var subtractExpr = (BinaryExpression)finalExpr.Left;
            var multiInSin = ((FunctionExpression)subtractExpr.Left).Argument;
            var multiInSubtract = subtractExpr.Right;

            Console.WriteLine($"BEFORE: 7*(2+x) in sin == 7*(2+x) in subtract? {object.ReferenceEquals(multiInSin, multiInSubtract)}");
            Console.WriteLine($"  (Expected: True - you reused the same 'multi' variable)\n");

            // Optimize
            var optimized = Optimizer.Optimize(finalExpr);

            Console.WriteLine("=== AFTER OPTIMIZATION ===");
            Console.WriteLine("Expression tree:");
            ExprPrinter.Print(optimized);
            Console.WriteLine();

            // Navigate the optimized tree
            var optimizedPlus = (BinaryExpression)optimized;
            var optimizedMinus = (BinaryExpression)optimizedPlus.Left;
            var optimizedSin = (FunctionExpression)optimizedMinus.Left;
            var optimizedCos = (FunctionExpression)optimizedPlus.Right;

            // Get 7*(2+x) from both locations
            var sevenTimesInSin = (BinaryExpression)optimizedSin.Argument;
            var sevenTimesInMinus = (BinaryExpression)optimizedMinus.Right;

            Console.WriteLine("=== VERIFICATION ===");
            bool test1 = object.ReferenceEquals(sevenTimesInSin, sevenTimesInMinus);
            Console.WriteLine($"✓ TEST 1: 7*(2+x) subtree reused? {test1} {(test1 ? "PASS ✓" : "FAIL ✗")}");

            // Get x from different locations - handle normalization!
            // After normalization, could be: (2+x)*7 OR 7*(2+x)
            BinaryExpression twoPlusX;
            if (sevenTimesInSin.Left is BinaryExpression leftBinary)
            {
                twoPlusX = leftBinary;  // Structure: (2+x) * 7
            }
            else
            {
                twoPlusX = (BinaryExpression)sevenTimesInSin.Right;  // Structure: 7 * (2+x)
            }

            var xInMulti = (VariableExpression)twoPlusX.Right;
            var xInCos = (VariableExpression)optimizedCos.Argument;

            bool test2 = object.ReferenceEquals(xInMulti, xInCos);
            Console.WriteLine($"✓ TEST 2: 'x' variable reused? {test2} {(test2 ? "PASS ✓" : "FAIL ✗")}");

            // Get constants - also handle normalization
            ConstantExpression sevenConst;
            ConstantExpression twoConst;

            if (sevenTimesInSin.Left is ConstantExpression)
            {
                sevenConst = (ConstantExpression)sevenTimesInSin.Left;
                twoConst = (ConstantExpression)twoPlusX.Left;
            }
            else
            {
                sevenConst = (ConstantExpression)sevenTimesInSin.Right;
                twoConst = (ConstantExpression)twoPlusX.Left;
            }

            Console.WriteLine($"\n✓ TEST 3: Constants normalized?");
            Console.WriteLine($"  - 7 = {sevenConst.Value} {(sevenConst.Value == 7 ? "✓" : "✗")}");
            Console.WriteLine($"  - 2 = {twoConst.Value} {(twoConst.Value == 2 ? "✓" : "✗")}");

            Console.WriteLine($"\n=== SUMMARY ===");
            bool allPass = test1 && test2;
            Console.WriteLine($"All tests: {(allPass ? "PASS ✓✓✓" : "FAIL")}");

            Console.ReadLine();
        }
    }
}
