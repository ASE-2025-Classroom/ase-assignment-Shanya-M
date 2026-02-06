using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BOOSEapp
{
    /// <summary>
    /// Command class for drawing circles on the canvas. Extends CommandOneParameter to handle circle drawing operations.
    /// </summary>
    public class AppCircle : CommandOneParameter, ICommand
    {
        /// <summary>
        /// The radius of the circle to be drawn.
        /// </summary>
        public int radius;

        /// <summary>
        /// Initializes a new instance of the BooseCircle class with default values.
        /// </summary>
        public AppCircle() : base()
        {
            Debug.WriteLine("BooseCircle empty constructor called");
        }

        /// <summary>
        /// Initializes a new instance of the BooseCircle class with a canvas and radius.
        /// </summary>
        /// <param name="c">The canvas on which to draw the circle.</param>
        /// <param name="radius">The radius of the circle in pixels.</param>
        public AppCircle(Canvas c, int radius) : base(c)
        {
           
            this.radius = radius;
            Debug.WriteLine($"BooseCircle with canvas called, radius: {radius}");
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
        /// Executes the circle drawing command. Validates parameters and draws the circle on the canvas.
        /// </summary>
        /// <exception cref="CanvasException">Thrown when double parameters are provided (not supported).</exception>
        /// <exception cref="CommandException">Thrown when the radius parameter is negative.</exception>
        public override void Execute() 
        {
 
            base.Execute();
            if (IsDouble)
            {
                throw new CanvasException("circle command does not support double parameters");
            }
            if (Paramsint[0] < 0)
            {
                throw new CommandException(Name + " command parameter must be non-negative");
            }


            radius = Paramsint[0];
            Debug.WriteLine($"Radius: "+ radius);
            try
            {
                if (Canvas is AppCanvas booseCanvas)
                {
                    Debug.WriteLine("Executing canvas draw");
                   
                    booseCanvas.Circle(radius, false);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Validates that the command has exactly one parameter (radius).
        /// </summary>
        /// <param name="Parameters">The array of parameter strings to validate.</param>
        /// <exception cref="CommandException">Thrown when the number of parameters is not exactly one.</exception>
        public override void CheckParameters(string[] Parameters) 
        {    
                if (Parameters == null || Parameters.Length != 1)
                {
                    throw new CommandException($"{Name} command requires exactly one parameter: radius");
                }
            


        }





    }
}
