using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace BOOSEapp
{
    /// <summary>
    /// Represents a boolean variable in the BOOSE language.
    /// Handles declaration, assignment, and evaluation of boolean expressions (true/false).
    /// </summary>
    class AppBoolean : Evaluation
    {
        /// <summary>
        /// The internal storage for the boolean value.
        /// </summary>
        public bool boolValue;

        /// <summary>
        /// Initializes a new instance of the BooseBoolean class.
        /// </summary>
        public AppBoolean()
        {
        }

        /// <summary>
        /// Gets or sets the boolean value of this variable.
        /// </summary>
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        /// <summary>
        /// Compiles the boolean variable declaration and adds it to the program's variable table.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
            base.Program.AddVariable(this);
        }

        /// <summary>
        /// Executes the boolean variable assignment by evaluating the expression as true or false.
        /// </summary>
        /// <exception cref="CommandException">Thrown when the expression is not 'true' or 'false'.</exception>
        public override void Execute()
        {
            base.Execute();
            if (evaluatedExpression.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                value = 1;
                boolValue = true;
                return;
            }
            if (evaluatedExpression.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                value = 0;
                boolValue = false;
                return;
            }
            throw new CommandException("Type mismatch, expected a boolean value (true/false)");
        }
    }
}
