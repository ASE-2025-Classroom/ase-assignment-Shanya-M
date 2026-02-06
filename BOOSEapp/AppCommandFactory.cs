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
    /// Factory class for creating BOOSE command instances based on command names.
    /// Extends the base CommandFactory to  provide application-specific command creation.
    /// </summary>
    public class AppCommandFactory: CommandFactory
    {
        /// <summary>
        /// Creates a command instance based on the provided command name.
        /// </summary>
        /// <param name="commandName">The name of the command to create (case-insensitive). Supported commands: circle, rect, moveto, drawto, clear, tri, reset.</param>
        /// <returns>An instance of the corresponding command class, or delegates to the base factory if the command is not recognized.</returns>
        public override ICommand MakeCommand(string commandName)
        {
          
                commandName = commandName.ToLower().Trim();
                if (commandName.Equals("circle")) return new AppCircle();
                if (commandName.Equals("rect")) return new AppRectangle();
                if (commandName.Equals("moveto")) return new AppMoveto();
                if (commandName.Equals("drawto")) return new AppDrawTo();
                if (commandName.Equals("clear")) return new AppClear();
                if (commandName.Equals("tri")) return new AppTriangle();
                if (commandName.Equals("reset")) return new AppReset();
                if (commandName.Equals("pen")) return new AppPen();
                if (commandName.Equals("if")) return new AppIf();
                if (commandName.Equals("else")) return new AppElse();
                if (commandName.Equals("for")) return new AppFor();
                if (commandName.Equals("while")) return new AppWhile();
                if (commandName.Equals("write")) return new AppWrite();
                if (commandName.Equals("method")) return new AppMethod();
                if (commandName.Equals("call")) return new AppCall();

                if (commandName.Equals("end if")) return new AppEnd();
                if (commandName.Equals("end while")) return new AppEnd();
                if (commandName.Equals("end for")) return new AppEnd();
                if (commandName.Equals("end method")) return new AppEnd();

                if (commandName.Equals("int")) return new AppInt();
                if (commandName.Equals("real")) return new BOOSE.Real();
                if (commandName.Equals("array")) return new AppArray();
                if (commandName.Equals("boolean")) return new AppBoolean();
                
                if (commandName.Equals("poke")) return new AppPoke();
                if (commandName.Equals("peek")) return new AppPeek();
            return (base.MakeCommand(commandName));
        }

    }
}
