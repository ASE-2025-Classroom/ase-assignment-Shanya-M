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
    /// Represents the end marker for compound control structures (if, while, for, method) in the BOOSE language.
    /// Handles the logic for loop continuation and method returns.
    /// </summary>
    class AppEnd : CompoundCommand
    {
        /// <summary>
        /// Initializes a new instance of the BooseEnd class.
        /// </summary>
        public AppEnd() : base()
        {
        }

        /// <summary>
        /// Compiles the end statement by popping the corresponding compound command from the stack and linking them.
        /// </summary>
        public override void Compile()
        {
            base.CorrespondingCommand = base.Program.Pop();
            base.LineNumber = base.Program.Count;
            base.CorrespondingCommand.EndLineNumber = base.LineNumber;
        }

        /// <summary>
        /// Executes the end statement logic based on the type of compound command.
        /// For while/for loops: jumps back to the start if condition is still true.
        /// For methods: returns to the calling location.
        /// For if statements: simply continues execution.
        /// </summary>
        public override void Execute()
        {
            if (CorrespondingCommand is AppWhile)
            {
                if (CorrespondingCommand.Condition)
                {
                    Debug.WriteLine($"BooseEnd.Execute: While condition TRUE, jumping back to {CorrespondingCommand.LineNumber - 1}");
                    Program.PC = CorrespondingCommand.LineNumber - 1;
                }
            }
            else if (CorrespondingCommand is AppFor booseFor)
            {
                booseFor.IncrementLoopVariable();

                if (CorrespondingCommand.Condition)
                {
                    Debug.WriteLine($"BooseEnd.Execute: For condition TRUE, jumping back to {CorrespondingCommand.LineNumber - 1}");
                    Program.PC = CorrespondingCommand.LineNumber - 1;
                }
            }
            else if (CorrespondingCommand is AppMethod booseMethod)
            {
                Program.PC = booseMethod.ReturnLineNumber;
            }
            else { }
        }
        /// <summary>
        /// Validates that the end statement matches the corresponding compound command type.
        /// </summary>
        /// <param name="parameter">The array of parameter strings to validate.</param>
        /// <exception cref="CommandException">Thrown when the end type doesn't match the compound command type.</exception>
            public override void CheckParameters(string[] parameter)
            {
            if (CorrespondingCommand is AppIf && !parameter.Contains("if"))
            {
                throw new CommandException("Expected 'end if'");
            }
            if (CorrespondingCommand is AppWhile && !parameter.Contains("while"))
            {
                throw new CommandException("Expected 'end while'");
            }
            if (CorrespondingCommand is AppFor && !parameter.Contains("for"))
            {
                throw new CommandException("Expected 'end for'");
            }
        }
    }
}
