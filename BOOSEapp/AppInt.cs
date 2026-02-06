using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BOOSEapp
{
    /// <summary>
    /// Represents an integer variable in the BOOSE language.
    /// Handles declaration, assignment, and evaluation of integer expressions.
    /// </summary>
    class AppInt : Evaluation, ICommand
    {
        /// <summary>
        /// The internal storage for the integer value.
        /// </summary>
        private int intValue;

        /// <summary>
        /// Initializes a new instance of the BooseInt class.
        /// </summary>
        public AppInt() : base() 
        {
            Debug.WriteLine("BooseInt empty constructor called");
        }

        /// <summary>
        /// Compiles the integer variable declaration and adds it to the program's variable table.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
            base.Program.AddVariable(this);           
        }

        /// <summary>
        /// Executes the integer variable assignment by parsing the evaluated expression and updating the program variable.
        /// </summary>
        /// <exception cref="StoredProgramException">Thrown when the expression cannot be parsed as an integer.</exception>
        public override void Execute()
        {

            base.Execute();

            if (!int.TryParse(evaluatedExpression, out intValue))
            {
                throw new StoredProgramException("Type mismatch, expected a int value");
            }

            base.Program.UpdateVariable(VarName, intValue);
        }
    }
}
