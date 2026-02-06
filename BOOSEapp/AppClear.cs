using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOOSEapp
{
    /// <summary>
    /// Command class for clearing the canvas. Extends CanvasCommand to handle canvas clearing operations.
    /// </summary>
    public class AppClear : CanvasCommand, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the BooseClear class with default values.
        /// </summary>
        public AppClear(): base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BooseClear class with a canvas.
        /// </summary>
        /// <param name="c">The canvas to clear.</param>
        public AppClear(Canvas c) : base(c)
        {
        }
        /// <summary>
        /// Sets the command parameters from the stored program.
        /// </summary>
        /// <param name="Program">The stored program containing variable context.</param>
        /// <param name="Params">The parameter string to parse.</param>
        public override void Set(StoredProgram Program, string Params)
        {
            base.Set(Program, Params);
        }
        /// <summary>
        /// Compiles the command parameters. Must be called before Execute.
        /// </summary>
        public override void Compile()
        {
            // To be called before command run
            base.Compile();
        }
        /// <summary>
        /// Executes the clear command, clearing the entire canvas.
        /// </summary>
        public override void Execute()
        {
            base.Execute();


            Canvas?.Clear();
        }
        /// <summary>
        /// Validates that the command has no parameters (clear command takes no parameters).
        /// </summary>
        /// <param name="parameter">The array of parameter strings to validate.</param>
        public override void CheckParameters(string[] parameter)
        {

        }



    }
}
