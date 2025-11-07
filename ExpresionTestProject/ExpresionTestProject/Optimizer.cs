using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpresionTestProject
{
    internal class Optimizer
    {
        public static IExpression Optimize(IExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var cache = new Dictionary<IExpression, IExpression>(new ExpressionComparer());
            return OptimizeRecursion(expression, cache);

        }

        public static IExpression OptimizeRecursion(IExpression expression, Dictionary<IExpression, IExpression> cache)
        {

            switch (expression)
            {
                case ConstantExpression c:
                    if(cache.TryGetValue(c, out var cachedConst))
                    {
                        return cachedConst;
                    }
                    cache[c] = c;
                    return expression;
                case VariableExpression v:
                    {
                        if(cache.TryGetValue(v, out var cachedVar))
                        {
                            return cachedVar;
                        }
                        cache[v] = v;
                        return expression;
                    }
                case BinaryExpression b:
                    {
                        var leftOptimized = OptimizeRecursion(b.Left, cache);
                        var rightOptimized = OptimizeRecursion(b.Right, cache);
                        if (b.Sign == OperatorSign.Plus || b.Sign == OperatorSign.Multiply)
                        {
                            if (leftOptimized.ToString().CompareTo(rightOptimized.ToString()) > 0)
                            {
                                var tmp = leftOptimized;
                                leftOptimized = rightOptimized;
                                rightOptimized = tmp;
                            }
                        }
                        var optimizedExpr = new BinaryExpression(b.Sign, leftOptimized, rightOptimized);
                        if(cache.TryGetValue(optimizedExpr,out var reusedExp))
                        {
                            return reusedExp;
                        }
                        cache[optimizedExpr] = optimizedExpr;
                        return optimizedExpr;
                    }
                case FunctionExpression f:
                    {
                        var argOptimized = OptimizeRecursion(f.Argument, cache);
                        var optimizedExpr = new FunctionExpression(f.Kind, argOptimized);
                        var key = optimizedExpr.ToString();
                        if(cache.TryGetValue(optimizedExpr,out var reusedFunc))
                        {
                            return reusedFunc;
                        }
                        cache[optimizedExpr] = optimizedExpr;
                        return optimizedExpr;
                    }
                default:
                    throw new NotSupportedException("Unknown expression type");


            }

        }

    }
}
