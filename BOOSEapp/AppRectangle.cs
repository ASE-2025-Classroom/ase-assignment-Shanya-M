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
    /// Command class for drawing rectangles on the canvas. Extends CommandTwoParameters to handle rectangle drawing operations.
    /// </summary>
    class AppRectangle : CommandTwoParameters, ICommand
    {
        /// <summary>
        /// The width of the rectangle to be drawn.
        /// </summary>
        public int width;
        /// <summary>
        /// The height of the rectangle to be drawn.
        /// </summary>
        public int height;
        /// <summary>
        /// Initializes a new instance of the BooseRectangle class with default values.
        /// </summary>
        public AppRectangle() : base()
        {
            Debug.WriteLine("BooseRectangle empty constructor called");
        }
        /// <summary>
        /// Initializes a new instance of the BooseRectangle class with a canvas, width, and height.
        /// </summary>
        /// <param name="c">The canvas on which to draw the rectangle.</param>
        /// <param name="width">The width of the rectangle in pixels.</param>
        /// <param name="height">The height of the rectangle in pixels.</param>
        public AppRectangle(Canvas c, int width, int height) : base(c)
        {
            this.width = width;
            this.height = height;
            Debug.WriteLine($"BooseRectangle with canvas called, width: {width}, height: {height}");

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
        /// Executes the rectangle drawing command. Validates parameters and draws the rectangle on the canvas.
        /// </summary>
        /// <exception cref="CanvasException">Thrown when double parameters are provided (not supported).</exception>
        /// <exception cref="CommandException">Thrown when width or height parameters are negative.</exception>
        public override void Execute()
        {

            base.Execute();
            if (IsDouble)
            {
                throw new CanvasException("rect command does not support double parameters");
            }

            if (Paramsint[0] < 0 || Paramsint[1] < 0)
            {
                throw new CommandException(Name + " command parameters must be non-negative");

            }

            width = Paramsint[0];
            height = Paramsint[1];

            try
            {
                if (Canvas is AppCanvas booseCanvas)
                {
                    Debug.WriteLine("Executing canvas draw");

                    booseCanvas.Rect(width, height, false);
                }
            }
            catch (ApplicationException ex)
            {
                Debug.WriteLine(ex.Message);

            }
        }

        /// <summary>
        /// Validates that the command has exactly two parameters (width and height).
        /// </summary>
        /// <param name="Parameters">The array of parameter strings to validate.</param>
        /// <exception cref="CommandException">Thrown when the number of parameters is not exactly two.</exception>
        public override void CheckParameters(string[] Parameters)
        {


            if (Parameters == null || Parameters.Length != 2)
            {
                throw new CommandException(Name + " command requires exactly two parameters: width and height");
            }

        }
    }
}
