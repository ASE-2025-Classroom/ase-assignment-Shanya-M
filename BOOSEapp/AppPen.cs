using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOOSEapp
{
    /// <summary>
    /// Represents a pen command for setting the drawing color in the BOOSE language.
    /// Sets the RGB color values for subsequent drawing operations.
    /// Syntax: pen red green blue (values 0-255)
    /// </summary>
    class AppPen : CommandThreeParameters, ICommand
    {
        /// <summary>
        /// The red component of the color (0-255).
        /// </summary>
        public int red;

        /// <summary>
        /// The green component of the color (0-255).
        /// </summary>
        public int green;

        /// <summary>
        /// The blue component of the color (0-255).
        /// </summary>
        public int blue;

        /// <summary>
        /// Initializes a new instance of the BoosePen class.
        /// </summary>
        public AppPen(): base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BoosePen class with a canvas and RGB values.
        /// </summary>
        /// <param name="c">The canvas on which to set the pen color.</param>
        /// <param name="r">The red component (0-255).</param>
        /// <param name="g">The green component (0-255).</param>
        /// <param name="b">The blue component (0-255).</param>
        public AppPen(Canvas c, int r, int g, int b) : base(c)
        {
            this.red = r;
            this.green = g;
            this.blue = b;
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
        /// Compiles the pen command parameters. Must be called before Execute.
        /// </summary>
        public override void Compile()
        {
            // To be called before command run
            base.Compile();
        }

        /// <summary>
        /// Validates that the pen command has exactly three parameters (red, green, blue).
        /// </summary>
        /// <param name="parameterList">The array of parameter strings to validate.</param>
        /// <exception cref="CommandException">Thrown when the number of parameters is not exactly three.</exception>
        public override void CheckParameters(string[] parameterList)
        {
            if (Parameters == null || Parameters.Length != 3)
            {
                throw new CommandException(Name + " command requires exactly three parameters: red, green and blue ");
            }
        }

        /// <summary>
        /// Executes the pen command by validating RGB values and setting the canvas pen color.
        /// </summary>
        /// <exception cref="CanvasException">Thrown when double parameters are provided or execution fails.</exception>
        /// <exception cref="CommandException">Thrown when color values are not in the range 0-255.</exception>
        public override void Execute()
        {
            base.Execute();
            if (IsDouble)
            {
                throw new CanvasException("pen command does not support double parameters");
            }
            if (Paramsint[0] < 0 || Paramsint[0] > 255 ||
                Paramsint[1] < 0 || Paramsint[1] > 255 ||
                Paramsint[2] < 0 || Paramsint[2] > 255)
            {
                throw new CommandException(Name + " command parameters must be in the range 0-255");
            }

            red = Paramsint[0];
            green = Paramsint[1];
            blue = Paramsint[2];

            try
            {
                if (Canvas is AppCanvas booseCanvas)
                {
                    booseCanvas.SetColour(red, green, blue);
                }
            }
            catch (Exception ex)
            {
                throw new CanvasException("Error executing pen command: " + ex.Message);
            }
        }
    }
}
