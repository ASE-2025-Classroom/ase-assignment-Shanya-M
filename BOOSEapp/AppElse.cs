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
    /// Represents the else clause of an if-else control structure in the BOOSE language.
    /// Executes a block of code when the corresponding if condition is false.
    /// </summary>
    class AppElse : CompoundCommand
    {
        /// <summary>
        /// Initializes a new instance of the BooseElse class.
        /// </summary>
        public AppElse() { }

        /// <summary>
        /// Compiles the else statement by linking it to the corresponding if statement.
        /// Pops the if from the stack, links to it, and pushes the else onto the stack.
        /// </summary>
        public override void Compile()
        {
            // Pop the If, link to it, then push ourselves
            CorrespondingCommand = Program.Pop();
            LineNumber = Program.Count;
            CorrespondingCommand.EndLineNumber = LineNumber;  // If jumps here when false
            Program.Push(this);
            Debug.WriteLine($"BooseElse.Compile: linked to If at line {CorrespondingCommand.LineNumber}, else at line {LineNumber}");
        }

        /// <summary>
        /// Executes the else clause logic. If the if condition was true, skips the else block.
        /// If the if condition was false, continues execution into the else block.
        /// </summary>
        public override void Execute()
        {
            Debug.WriteLine($"BooseElse.Execute: If condition was {CorrespondingCommand.Condition}, jumping to {EndLineNumber}");

            // If we reach else during execution, the if-block was executed (condition was true)
            // So we need to skip the else block
            if (CorrespondingCommand.Condition)
            {
                Program.PC = EndLineNumber - 1;
            }
            // If condition was false, we came here from the if, continue into else block
        }

        /// <summary>
        /// Validates that the else statement has no parameters.
        /// </summary>
        /// <param name="parameter">The array of parameter strings to validate.</param>
        public override void CheckParameters(string[] parameter)
        {
            // else has no parameters
        }
    }
}
