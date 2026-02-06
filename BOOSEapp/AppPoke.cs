using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOOSEapp
{
    /// <summary>
    /// Represents a poke command for writing a value to an array element in the BOOSE language.
    /// Syntax: poke arrayName rowIndex [columnIndex] = value
    /// </summary>
    class AppPoke : Command, ICommand
    {
        /// <summary>
        /// Compiles the poke command. Currently no compilation logic needed.
        /// </summary>
        public override void Compile()
        {
 
        }

        /// <summary>
        /// Executes the poke operation by writing the specified value to the array element.
        /// </summary>
        /// <exception cref="CommandException">Thrown when syntax is invalid or array is not found.</exception>
        public override void Execute()
        {
            var prog = base.Program;
            string raw = base.ParameterList ?? string.Empty;

            var left = raw.Split('=', 2)[0].Trim();
            var leftTokens = left.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (leftTokens.Length < 2) throw new CommandException("Invalid poke target; expected: arr row [col]");

            string arrName = leftTokens[0].Trim();

            Evaluation varObj = prog.GetVariable(arrName);
            if (varObj is AppArray arr)
            {
                arr.ProcessArrayParametersCompile(peekOrPoke: true, parameters: raw);
                arr.ProcessArrayParametersExecute(peekOrPoke: true);
                return;
            }

            throw new CommandException($"Undefined or unsupported array: {arrName}");
        }

        /// <summary>
        /// Validates the poke command parameters.
        /// </summary>
        /// <param name="parameters">The array of parameter strings to validate.</param>
        public override void CheckParameters(string[] parameters)
        {

        }
    }

}
