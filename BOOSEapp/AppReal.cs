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
    /// Represents a real (double) variable in the BOOSE language.
    /// Handles declaration, assignment, and evaluation of real number expressions.
    /// </summary>
    public class AppReal : Evaluation
    {
        /// <summary>
        /// The internal storage for the real number value.
        /// </summary>
        public double realValue;

        /// <summary>
        /// Initializes a new instance of the BooseReal class.
        /// </summary>
        public AppReal()
        {

        }

        /// <summary>
        /// Gets or sets the real value of this variable.
        /// </summary>
        public double RealValue
        {
            get { return realValue; }
            set { realValue = value; }
        }

        /// <summary>
        /// Compiles the real variable declaration and adds it to the program's variable table.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
            base.Program.AddVariable(this);
            
        }

        /// <summary>
        /// Executes the real variable assignment by parsing the evaluated expression and updating the program variable.
        /// </summary>
        /// <exception cref="StoredProgramException">Thrown when the expression cannot be parsed as a real number.</exception>
        public override void Execute()
        {
            base.Execute();           
            if (!double.TryParse(evaluatedExpression, out realValue))
            {
                throw new StoredProgramException("Type mismatch, expected a real value");
            }
            base.Program.UpdateVariable(VarName, realValue);
        }
    }

}
