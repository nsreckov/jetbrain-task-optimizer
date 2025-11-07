# Expression Tree Optimizer

A C# console application that demonstrates expression tree optimization through subtree deduplication and normalization.

## 🎯 Overview

This project implements an optimizer for mathematical expressions represented as abstract syntax trees (ASTs). The optimizer performs two key operations:

1. **Subtree Deduplication**: Identifies and reuses identical expression subtrees to save memory
2. **Normalization**: Standardizes commutative operations (addition and multiplication) by ordering operands

## 🔧 Features

- **Expression Node Types**:
  - Constants (`ConstantExpression`)
  - Variables (`VariableExpression`) 
  - Binary operations (`BinaryExpression`) - Plus, Minus, Multiply, Divide
  - Functions (`FunctionExpression`) - Sin, Cos, Max

- **Optimization Techniques**:
  - Reference-based subtree sharing for identical expressions
  - Commutative operation normalization (ensures `a + b` and `b + a` are treated as equivalent)
  - Custom equality comparer for structural expression comparison

- **Verification System**: 
  - Built-in test cases to verify optimization correctness
  - Reference equality checks to confirm subtree reuse
  - Visual tree printing for debugging

## 🚀 Getting Started

### Prerequisites

- .NET Framework 4.7.2 or later
- Visual Studio 2017 or later (recommended)

### Building and Running

1. Clone the repository:
   ```bash
   git clone https://github.com/nsreckov/jetbrain-task-optimizer.git
   cd jetbrain-task-optimizer
   ```

2. Open the solution:
   ```bash
   cd ExpresionTestProject
   # Open ExpresionTestProject.sln in Visual Studio
   ```

3. Build and run:
   - Press `F5` in Visual Studio, or
   - Use the command line:
     ```bash
     dotnet build
     dotnet run
     ```

## 💡 Example Usage

The program demonstrates optimization on the expression: `sin(7*(2+x)) - 7*(2+x) + cos(x)`

**Before Optimization:**
- Contains duplicate subtree `7*(2+x)` 
- Multiple separate instances of variables and constants

**After Optimization:**
- Subtree `7*(2+x)` is deduplicated (same object reference used in both locations)
- Variables `x` are unified across the expression
- Constants are normalized and reused

## 🏗️ Architecture

### Core Components

- **`IExpression`**: Base interface for all expression nodes
- **`Optimizer`**: Main optimization engine with recursive traversal
- **`ExpressionComparer`**: Custom equality comparer for structural comparison
- **`ExprPrinter`**: Utility for visualizing expression trees

### Optimization Algorithm

1. **Recursive Traversal**: Post-order traversal of the expression tree
2. **Caching**: Dictionary-based cache using structural equality
3. **Normalization**: Automatic reordering of commutative operations
4. **Deduplication**: Return cached instances for structurally identical expressions

## 🧪 Testing

The application includes built-in verification:

- ✅ **TEST 1**: Verifies subtree `7*(2+x)` is reused (reference equality)
- ✅ **TEST 2**: Confirms variable `x` instances are unified  
- ✅ **TEST 3**: Validates constant normalization

## 📁 Project Structure

```
ExpresionTestProject/
├── ExpresionTestProject.sln          # Visual Studio solution
├── ExpresionTestProject/
│   ├── Program.cs                     # Main entry point and demo
│   ├── Optimizer.cs                   # Core optimization logic
│   ├── ExpressionNodes.cs             # Expression AST node definitions
│   ├── ExpressionComparer.cs          # Structural equality comparer
│   ├── ExprPrinter.cs                 # Tree visualization utility
│   └── ExpresionTestProject.csproj    # Project configuration
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 🔮 Future Enhancements

- [ ] Add more mathematical functions (tan, log, exp, etc.)
- [ ] Implement constant folding optimization
- [ ] Add support for algebraic simplification rules
- [ ] Performance benchmarking suite
- [ ] Expression parsing from string notation
- [ ] Visualization of optimization steps

---

*Built with ❤️ for expression tree optimization and algorithm demonstration*