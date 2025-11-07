using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpresionTestProject
{
    internal class ExpressionComparer : IEqualityComparer<IExpression>
    {
        public bool Equals(IExpression a, IExpression b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (a == null || b == null)
            {
                return false;
            }
            if (a.GetType() != b.GetType())
            {
                return false;
            }
            switch (a)
            {
                case ConstantExpression ca:
                    var cb = (ConstantExpression)b;
                    return ca.Value == cb.Value;
                case VariableExpression va:
                    var vb = (VariableExpression)b;
                    return va.Name == vb.Name;
                case BinaryExpression ba:
                    var bb = (BinaryExpression)b;
                    return ba.Sign == bb.Sign && Equals(ba.Left, bb.Left) && Equals(ba.Right, bb.Right);
                case FunctionExpression fa:
                    var fb = (FunctionExpression)b;
                    return fa.Kind == fb.Kind && Equals(fa.Argument, fb.Argument);
                default:
                    return false;
            }
        }

        public int GetHashCode(IExpression expr)
        {
            if (expr == null)
            {
                return 0;
            }

            unchecked 
            {
                switch (expr)
                {
                    case ConstantExpression c:
                        return c.Value.GetHashCode();
                    
                    case VariableExpression v:
                        return v.Name.GetHashCode() * 397;
                    
                    case BinaryExpression b:
                        int hash = (int)b.Sign;
                        hash = (hash * 397) ^ GetHashCode(b.Left);
                        hash = (hash * 397) ^ GetHashCode(b.Right);
                        return hash;
                    
                    case FunctionExpression f:
                        return ((int)f.Kind * 397) ^ GetHashCode(f.Argument);
                    
                    default:
                        return expr.GetHashCode();
                }
            }
        }
    }
}
