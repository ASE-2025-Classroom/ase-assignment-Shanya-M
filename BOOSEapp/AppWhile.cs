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
    /// Represents a while loop control structure in the BOOSE language.
    /// Executes a block of code repeatedly while a condition remains true.
    /// </summary>
    class AppWhile : ConditionalCommand

    {
        /// <summary>
        /// Initializes a new instance of the BooseWhile class.
        /// </summary>
        public AppWhile() : base() { }

        /// <summary>
        /// Compiles the while loop by recording its line number in the program.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
            LineNumber = Program.Count;
            Debug.WriteLine($"BooseWhile.Compile: LineNumber={LineNumber}");

        }

        /// <summary>
        /// Executes the while loop condition check. If the condition is false, jumps to the end of the loop.
        /// </summary>
        public override void Execute()
        {
            base.Execute();
            Debug.WriteLine($"BooseWhile.Execute: Condition={Condition}, EndLineNumber={EndLineNumber}, PC={Program.PC}");

            if (!Condition)
            {
                Debug.WriteLine($"BooseWhile.Execute: Exiting loop, jumping to {EndLineNumber - 1}");
                Program.PC = EndLineNumber - 1;
            }
        }

    }
}
