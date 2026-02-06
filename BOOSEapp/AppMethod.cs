using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BOOSEapp
{
    /// <summary>
    /// Represents a method (function) definition in the BOOSE language.
    /// Supports local variables, parameters, and return values.
    /// Syntax: method returnType methodName [type param1, type param2, ...]
    /// </summary>
    class AppMethod : CompoundCommand
    {
        /// <summary>
        /// The name of the method.
        /// </summary>
        private string methodName;

        /// <summary>
        /// The return type of the method ("int" or "real").
        /// </summary>
        private string returnType;

        /// <summary>
        /// Array of local variable declarations for the method parameters.
        /// </summary>
        private string[] localVariables;

        /// <summary>
        /// The line number to return to after method execution completes.
        /// </summary>
        private int returnLineNumber;

        /// <summary>
        /// Static storage for all defined methods in the program.
        /// </summary>
        private static readonly Dictionary<string, AppMethod> methods = new();

        /// <summary>
        /// Gets a method by name from the method registry.
        /// </summary>
        /// <param name="name">The name of the method to retrieve.</param>
        /// <returns>The BooseMethod instance, or null if not found.</returns>
        public static AppMethod GetMethod(string name) => methods.GetValueOrDefault(name);

        /// <summary>
        /// Clears all registered methods from the method registry.
        /// </summary>
        public static void ClearMethods() => methods.Clear();

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string MethodName => methodName;

        /// <summary>
        /// Gets the array of local variable declarations.
        /// </summary>
        public string[] LocalVariables => localVariables;

        /// <summary>
        /// Gets or sets the line number to return to after method execution.
        /// </summary>
        public int ReturnLineNumber
        {
            get => returnLineNumber;
            set => returnLineNumber = value;
        }

        /// <summary>
        /// Initializes a new instance of the BooseMethod class.
        /// </summary>
        public AppMethod() : base() { }

        /// <summary>
        /// Compiles the method definition by parsing the return type, method name, and parameters.
        /// Creates local variables and registers the method in the method registry.
        /// </summary>
        /// <exception cref="CommandException">Thrown when method syntax is invalid.</exception>
        public override void Compile()
        {
            base.Compile();

            var parts = ParameterList.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
                throw new CommandException("Invalid method syntax");

            returnType = parts[0];
            methodName = parts[1];

            if (parts.Length > 2)
            {
                string paramString = string.Join(" ", parts.Skip(2));
                localVariables = paramString.Split(',').Select(p => p.Trim()).ToArray();
            }
            else
            {
                localVariables = [];
            }
            foreach (var param in localVariables)
            {
                var paramParts = param.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (paramParts.Length >= 2)
                {
                    string paramType = paramParts[0].ToLowerInvariant();
                    string paramName = paramParts[1];

                    if (!Program.VariableExists(paramName))
                    {
                        if (paramType == "int")
                        {
                            AppInt intVar = new AppInt();
                            intVar.VarName = paramName;
                            intVar.Value = 0;
                            Program.AddVariable(intVar);
                        }
                        else if (paramType == "real")
                        {
                            AppReal realVar = new AppReal();
                            realVar.VarName = paramName;
                            realVar.RealValue = 0.0;
                            Program.AddVariable(realVar);
                        }
                    }
                }
            }
            if (!Program.VariableExists(methodName))
            {
                if (returnType.ToLowerInvariant() == "int")
                {
                    AppInt retVar = new AppInt();
                    retVar.VarName = methodName;
                    retVar.Value = 0;
                    Program.AddVariable(retVar);
                }
                else if (returnType.ToLowerInvariant() == "real")
                {
                    AppReal realVar = new AppReal();
                    realVar.VarName = methodName;
                    realVar.RealValue = 0.0;
                    Program.AddVariable(realVar);
                }
            }
            methods[methodName] = this;
            Debug.WriteLine($"BooseMethod.Compile: {methodName} at line {LineNumber}");
        }

        /// <summary>
        /// Executes the method definition during normal program flow.
        /// Skips the method body by jumping to the end line.
        /// </summary>
        public override void Execute()
        {
            // Skip method body during normal flow
            Debug.WriteLine($"BooseMethod.Execute: jumping to {EndLineNumber}");
            Program.PC = EndLineNumber;
        }
    }
}
