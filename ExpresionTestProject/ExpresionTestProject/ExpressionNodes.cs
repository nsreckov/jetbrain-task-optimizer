using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpresionTestProject
{
    public interface IExpression { }
    interface IConstantExpression { int Value { get; } }

    interface IVariableExpression { string Name { get; } }
    interface IBinaryExpression { IExpression Left { get; } IExpression Right { get; } OperatorSign Sign { get; } }

    interface IFunction { FunctionKind Kind { get; } IExpression Argument { get; } }

    public enum FunctionKind { Sin, Cos, Max }
    public enum OperatorSign { Plus, Minus, Multiply, Divide }

    public class ConstantExpression : IExpression, IConstantExpression
    {
        public int Value { get; }
        public ConstantExpression(int value) => Value = value;
        public override string ToString() => Value.ToString();
    }

    public class VariableExpression : IExpression, IVariableExpression
    {
        public string Name { get; }
        public VariableExpression(string name) => Name = name;
        public override string ToString() => Name;
    }

    public class BinaryExpression : IExpression, IBinaryExpression
    {
        public IExpression Left { get; }
        public IExpression Right { get; }
        public OperatorSign Sign { get; }

        public BinaryExpression(OperatorSign sign, IExpression left, IExpression right)
        {
            Sign = sign;
            Left = left;
            Right = right;
        }

        public override string ToString() => $"({Left} {Sign} {Right})";
    }

    public class FunctionExpression : IExpression, IFunction
    {
        public FunctionKind Kind { get; }
        public IExpression Argument { get; }

        public FunctionExpression(FunctionKind kind, IExpression argument)
        {
            Kind = kind;
            Argument = argument;
        }

        public override string ToString() => $"{Kind}({Argument})";
    }
}
