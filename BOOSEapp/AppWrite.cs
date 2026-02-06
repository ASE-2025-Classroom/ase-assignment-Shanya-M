using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOOSEapp
{
    /// <summary>
    /// Represents a write command for displaying text on the canvas in the BOOSE language.
    /// Supports expressions, variables, and string concatenation.
    /// Syntax: write "text" or write expression
    /// </summary>
    class AppWrite : CommandOneParameter, ICommand
    {
        /// <summary>
        /// The string to output to the canvas.
        /// </summary>
        public string outputString;

        /// <summary>
        /// Initializes a new instance of the BooseWrite class.
        /// </summary>
        public AppWrite() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BooseWrite class with a canvas and text parameter.
        /// </summary>
        /// <param name="c">The canvas on which to write text.</param>
        /// <param name="Params">The text or expression to write.</param>
        public AppWrite(Canvas c, string Params) : base(c)
        {
            this.outputString = Params;
        }

        /// <summary>
        /// Validates that the write command has exactly one parameter (the text or expression to write).
        /// </summary>
        /// <param name="parameter">The array of parameter strings to validate.</param>
        /// <exception cref="CommandException">Thrown when the number of parameters is not exactly one.</exception>
        public override void CheckParameters(string[] parameter)
        {
            if (Parameters == null || Parameters.Length != 1)
            {
                throw new CommandException($"{Name} command requires exactly one parameter: text");
            }
        }
        /// <summary>
        /// Executes the write command by evaluating the expression or string and displaying it on the canvas.
        /// Handles numeric formatting and string concatenation.
        /// </summary>
        public override void Execute()
        {

            string param = Parameters[0].Trim();
            string evaluated;
            if (Program.IsExpression(param))
            {
                evaluated = Program.EvaluateExpression(param)?.Trim() ?? string.Empty;
            }
            else
            {
                evaluated = Program.EvaluateExpressionWithString(param)?.Trim() ?? string.Empty;
            }

            if (double.TryParse(evaluated, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double d) ||
                double.TryParse(evaluated, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out d))
            {
                evaluated = d.ToString(CultureInfo.InvariantCulture);
            }

            try
            {
                if (Canvas is AppCanvas booseCanvas)
                {
                    Debug.WriteLine("Executing canvas draw");

                    booseCanvas.WriteText(evaluated);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
