using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace BOOSEapp
{
    /// <summary>
    /// Command class for drawing a line from the current position to specified coordinates. Extends CommandTwoParameters to handle line drawing.
    /// </summary>
    public class AppDrawTo : CommandTwoParameters, ICommand
    {
        /// <summary>
        /// The target X coordinate for the draw operation.
        /// </summary>
        public int xPos;
        /// <summary>
        /// The target Y coordinate for the draw operation.
        /// </summary>
        public int yPos;
        /// <summary>
        /// Initializes a new instance of the BooseDrawTo class with default values.
        /// </summary>
        public AppDrawTo() : base() 
        {
            Debug.WriteLine("BooseDrawTo empty constructor called");
        }

        /// <summary>
        /// Initializes a new instance of the BooseDrawTo class with a canvas and target coordinates.
        /// </summary>
        /// <param name="c">The canvas on which to draw the line.</param>
        /// <param name="x">The target X coordinate.</param>
        /// <param name="y">The target Y coordinate.</param>
        public AppDrawTo(Canvas c, int x, int y) : base(c) 
        {
            this.xPos = x;
            this.yPos = y;
            Debug.WriteLine($"BooseDrawTo with canvas called, x-postion: {x}, y-posstion: {y}");
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
        /// Executes the draw-to command. Validates parameters and draws a line from the current position to the target coordinates.
        /// </summary>
        /// <exception cref="CanvasException">Thrown when double parameters are provided (not supported).</exception>
        /// <exception cref="CommandException">Thrown when X or Y coordinates are negative.</exception>
        public override void Execute()
        {
            base.Execute();
            if (IsDouble) {   
                throw new CanvasException("drawto command does not support double parameters");
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
                    booseCanvas.DrawTo(xPos, yPos);
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
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
