using BOOSE;
using System;

namespace BOOSEapp
{
    /// <summary>
    /// Represents a peek command for reading a value from an array element in the BOOSE language.
    /// Syntax: peek targetVariable = arrayName rowIndex [columnIndex]
    /// </summary>
    class AppPeek : Command, ICommand
    {
        /// <summary>
        /// Compiles the peek command. Currently no compilation logic needed.
        /// </summary>
        public override void Compile()
        { 
            
        }

        /// <summary>
        /// Executes the peek operation by reading a value from the specified array element
        /// and storing it in the target variable.
        /// </summary>
        /// <exception cref="CommandException">Thrown when syntax is invalid or array is not found.</exception>
        public override void Execute()
        {
            var prog = base.Program;
            string raw = base.ParameterList ?? string.Empty;

            var parts = raw.Split('=', 2);
            if (parts.Length != 2) throw new CommandException("Invalid peek syntax; expected: peek target = arr row [col]");

            string targetName = parts[0].Trim();
            var rightTokens = parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (rightTokens.Length < 2) throw new CommandException("Invalid peek target; expected: arr row [col]");

            string arrName = rightTokens[0].Trim();

            Evaluation varObj = prog.GetVariable(arrName);
            if (varObj is AppArray arr)
            {
                arr.ProcessArrayParametersCompile(peekOrPoke: false, parameters: raw);
                arr.ProcessArrayParametersExecute(peekOrPoke: false);
                return;
            }

            throw new CommandException($"Undefined or unsupported array: {arrName}");
        }

        /// <summary>
        /// Validates the peek command parameters.
        /// </summary>
        /// <param name="parameters">The array of parameter strings to validate.</param>
        public override void CheckParameters(string[] parameters)
        {

        }
    }
}
