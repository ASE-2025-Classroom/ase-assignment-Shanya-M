using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BOOSEapp
{
    /// <summary>
    /// Command class for moving the drawing position without drawing. Extends CommandTwoParameters to handle position movement.
    /// </summary>
    public class AppMoveto : CommandTwoParameters, ICommand
    {
        /// <summary>
        /// The target X coordinate for the move operation.
        /// </summary>
        public int xPos;
        /// <summary>
        /// The target Y coordinate for the move operation.
        /// </summary>
        public int yPos;
        /// <summary>
        /// Initializes a new instance of the BooseMoveto class with default values.
        /// </summary>
        public AppMoveto() : base() 
        {
            Debug.WriteLine("BooseMoveto empty constructor called");
     
        }
        /// <summary>
        /// Initializes a new instance of the BooseMoveto class with a canvas and target coordinates.
        /// </summary>
        /// <param name="c">The canvas on which to move the drawing position.</param>
        /// <param name="x">The target X coordinate.</param>
        /// <param name="y">The target Y coordinate.</param>
        public AppMoveto(Canvas c, int x, int y): base(c) 
        {
            this.xPos = x;
            this.yPos = y;
            Debug.WriteLine($"BooseMoveto with canvas called, x-postion: {x}, y-posstion: {y}");


        }
        /// <summary>
        /// Sets the command parameters from the stored program.
        /// </summary>
        /// <param name="Program">The stored program containing variable context.</param>
        /// <param name="Params">The parameter string to parse.</param>
        public override void Set(StoredProgram Program, string Params)
        {
            base.Set(Program, Params);
            Debug.WriteLine($"Set called with params: {Params}");

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
        /// Executes the move command. Validates parameters and moves the drawing position on the canvas.
        /// </summary>
        /// <exception cref="CanvasException">Thrown when double parameters are provided (not supported).</exception>
        /// <exception cref="CommandException">Thrown when X or Y coordinates are negative.</exception>
        public override void Execute()
        {

            base.Execute();
            if (IsDouble)
            {
                throw new CanvasException("moveto command does not support double parameters");
            }
            if (Paramsint[0] < 0 || Paramsint[1] < 0)
            {
                throw new CommandException(Name + " command parameters must be non-negative");
            }


            xPos = Paramsint[0];
            yPos = Paramsint[1];
            Debug.WriteLine($"Xpos: {xPos}, Ypos: {yPos}");
            try
            {
                if (Canvas is AppCanvas booseCanvas)
                {
                    Debug.WriteLine("Executing canvas draw");

                    booseCanvas.MoveTo(xPos, yPos);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Validates that the command has exactly two parameters (X and Y coordinates).
        /// </summary>
        /// <param name="Parameters">The array of parameter strings to validate.</param>
        /// <exception cref="CommandException">Thrown when the number of parameters is not exactly two.</exception>
        public override void CheckParameters(string[] Parameters)
        {
            if (Parameters.Length != 2)
            {
                throw new CommandException(Name + " command requires exactly two parameters: Xpos, Ypos");

            }

        }

    }
}
