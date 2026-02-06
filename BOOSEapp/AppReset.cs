using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOOSEapp
{
    /// <summary>
    /// Command class for resetting the drawing position to the center of the canvas. Extends CanvasCommand to handle position reset operations.
    /// </summary>
    public class AppReset : CanvasCommand, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the BooseReset class with default values.
        /// </summary>
        public AppReset() : base() 
        { }

        /// <summary>
        /// Initializes a new instance of the BooseReset class with a canvas.
        /// </summary>
        /// <param name="c">The canvas on which to reset the drawing position.</param>
        public AppReset(Canvas c) : base(c)
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
        /// Executes the reset command, moving the drawing position to the center of the canvas.
        /// </summary>
        public override void Execute()
        {
            base.Execute();


            Canvas?.Reset();
        }
        /// <summary>
        /// Validates that the command has no parameters (reset command takes no parameters).
        /// </summary>
        /// <param name="parameter">The array of parameter strings to validate.</param>
        public override void CheckParameters(string[] parameter)
        {
           
        }
    }
}
