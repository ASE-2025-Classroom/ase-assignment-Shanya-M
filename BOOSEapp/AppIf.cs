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
    /// Represents an if conditional control structure in the BOOSE language.
    /// Executes a block of code only when a specified condition is true.
    /// </summary>
    class AppIf : ConditionalCommand
    {
        /// <summary>
        /// Initializes a new instance of the BooseIf class.
        /// </summary>
        public AppIf() :base()
        { 


        }

        /// <summary>
        /// Compiles the if statement by recording its line number and pushing it onto the program stack.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
            LineNumber = Program.Count;
            Debug.WriteLine($"BooseIf.Compile: pushed at line {LineNumber}");
        }

        /// <summary>
        /// Executes the if condition check. If the condition is false, jumps to else or end of if block.
        /// </summary>
        public override void Execute()
        {
            base.Execute();  
            Debug.WriteLine($"BooseIf.Execute: condition = {Condition}, EndLine = {EndLineNumber}");

            if (!Condition)
            {
                // Condition false - jump to else or end
                Program.PC = EndLineNumber - 1;
            }
            // If true, just continue to next line (inside if block)
        }
        
    }
}
