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
    /// Represents a for loop control structure in the BOOSE language.
    /// Executes a block of code repeatedly with a loop variable iterating from start to end value with an optional step.
    /// Syntax: for variable = start to end [step increment]
    /// </summary>
    class AppFor : ConditionalCommand
    {
        /// <summary>
        /// The name of the loop control variable.
        /// </summary>
        private string loopVariable;

        /// <summary>
        /// The starting value of the loop.
        /// </summary>
        private int startValue;

        /// <summary>
        /// The ending value of the loop.
        /// </summary>
        private int endValue;

        /// <summary>
        /// The increment step for each iteration (defaults to 1).
        /// </summary>
        private int stepValue = 1;

        /// <summary>
        /// Initializes a new instance of the BooseFor class.
        /// </summary>
        public AppFor() : base()
        {
        }

        /// <summary>
        /// Sets the for loop parameters by parsing the loop variable, start value, end value, and optional step value.
        /// </summary>
        /// <param name="Program">The stored program containing variable context.</param>
        /// <param name="Parameter">The parameter string in format: variable = start to end [step increment]</param>
        /// <exception cref="CommandException">Thrown when syntax is invalid or values cannot be parsed.</exception>
        public override void Set(StoredProgram Program, string Parameter)
        {
            base.Set(Program, Parameter);
            
            // Parse our custom "count = 1 to 10 step 2" format
            var parameters = Parameter.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            if (parameters.Length < 5)
            {
                throw new CommandException("Invalid FOR syntax. Expected: for variable = start to end [step increment]");
            }

            loopVariable = parameters[0];
            
            if (!int.TryParse(parameters[2], out startValue))
            {
                throw new CommandException($"Invalid start value in FOR: {parameters[2]}");
            }

            if (!int.TryParse(parameters[4], out endValue))
            {
                throw new CommandException($"Invalid end value in FOR: {parameters[4]}");
            }

            if (parameters.Length >= 7 && parameters[5].ToLowerInvariant() == "step")
            {
                if (!int.TryParse(parameters[6], out stepValue))
                {
                    throw new CommandException($"Invalid step value in FOR: {parameters[6]}");
                }
            }
            
            Debug.WriteLine($"BooseFor.Set: Variable={loopVariable}, Start={startValue}, End={endValue}, Step={stepValue}");
        }

        /// <summary>
        /// Compiles the for loop by recording its line number and creating the loop variable if it doesn't exist.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
            LineNumber = Program.Count;

            // Create loop variable if it doesn't exist
            if (!Program.VariableExists(loopVariable))
            {
                AppInt loopVar = new AppInt();
                loopVar.VarName = loopVariable;
                loopVar.Value = startValue;
                Program.AddVariable(loopVar);
            }

            Debug.WriteLine($"BooseFor.Compile: LineNumber={LineNumber}, Variable={loopVariable}");
        }

        /// <summary>
        /// Executes the for loop condition check. Evaluates whether to continue or exit the loop based on the current value and step direction.
        /// </summary>
        public override void Execute()
        {
            var variable = Program.GetVariable(loopVariable);
            int value = (int)variable.Value;

            Condition = (stepValue > 0) ? value <= endValue : value >= endValue;

            Debug.WriteLine($"BooseFor.Execute: {loopVariable}={value}, Condition={Condition}");

            if (!Condition)
            {
                Program.PC = EndLineNumber - 1;
            }
        }

        /// <summary>
        /// Increments the loop variable by the step value.
        /// Called at the end of each loop iteration.
        /// </summary>
        public void IncrementLoopVariable()
        {
            var variable = Program.GetVariable(loopVariable);
            int value = (int)variable.Value;
            Program.UpdateVariable(loopVariable, value + stepValue);
        }
    }
}
