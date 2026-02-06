using System;
using System.Diagnostics;
using BOOSE;

namespace BOOSEapp
{
    /// <summary>
    /// Represents a method call command in the BOOSE language.
    /// Invokes a previously defined method with specified arguments.
    /// Syntax: call methodName [arg1 arg2 ...]
    /// </summary>
    class AppCall : Command
    {
        /// <summary>
        /// The name of the method to call.
        /// </summary>
        private string methodName;

        /// <summary>
        /// Array of argument values to pass to the method.
        /// </summary>
        private string[] args;

        /// <summary>
        /// Initializes a new instance of the BooseCall class.
        /// </summary>
        public AppCall() : base() { }

        /// <summary>
        /// Compiles the method call by parsing the method name and arguments.
        /// </summary>
        public override void Compile()
        {
            var parts = ParameterList.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            methodName = parts[0];
            args = parts.Length > 1 ? parts[1..] : [];
            Debug.WriteLine($"BooseCall.Compile: {methodName}");
        }

        /// <summary>
        /// Executes the method call by setting parameter values and jumping to the method's start line.
        /// Stores the return address for when the method completes.
        /// </summary>
        /// <exception cref="CommandException">Thrown when the method is not found.</exception>
        public override void Execute()
        {
            var method = AppMethod.GetMethod(methodName);
            if (method == null)
                throw new CommandException($"Method not found: {methodName}");

            // Set parameter values
            for (int i = 0; i < method.LocalVariables.Length && i < args.Length; i++)
            {
                var paramParts = method.LocalVariables[i].Split(' ');
                if (paramParts.Length >= 2)
                {
                    string paramName = paramParts[1];
                    if (int.TryParse(args[i], out int val))
                        Program.UpdateVariable(paramName, val);
                }
            }

            method.ReturnLineNumber = Program.PC;
            Program.PC = method.LineNumber;
            Debug.WriteLine($"BooseCall.Execute: jumping to {method.LineNumber}");
        }

        /// <summary>
        /// Validates the call command parameters.
        /// </summary>
        /// <param name="parameter">The array of parameter strings to validate.</param>
        public override void CheckParameters(string[] parameter) { }
    }
}
