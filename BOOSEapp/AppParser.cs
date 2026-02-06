using BOOSE;    
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BOOSEapp
{
    /// <summary>
    /// Parser class for analyzing and converting BOOSE program text into executable commands.
    /// Handles variable declarations, control structures (if, while, for), arrays, methods, and drawing commands.
    /// Implements the IParser interface from the BOOSE library.
    /// </summary>
    public class AppParser : IParser
       {
        /// <summary>
        /// The command factory used to create command instances.
        /// </summary>
        protected AppCommandFactory commandFactory;

        /// <summary>
        /// The stored program that will hold the parsed commands.
        /// </summary>
        protected StoredProgram StoredProgram;

        /// <summary>
        /// Initializes a new instance of the BooseParser class with a command factory and program context.
        /// </summary>
        /// <param name="commandFactory">The factory used to create command instances.</param>
        /// <param name="Program">The stored program that will hold the parsed commands.</param>
        public AppParser(AppCommandFactory commandFactory, StoredProgram Program) : base() 
        { 
            this.commandFactory = commandFactory;
            this.StoredProgram = Program;
        }

        /// <summary>
        /// Normalizes a line of BOOSE code by trimming whitespace and adding spacing around operators.
        /// This ensures consistent parsing of expressions and commands.
        /// </summary>
        /// <param name="Line">The line of code to normalize.</param>
        /// <returns>The normalized line with consistent spacing around operators.</returns>
        private string Normalize(string Line)
        { 
            Line = Line.Trim();
            Line = Line.TrimStart('\uFEFF').Trim();
            Line = Regex.Replace(Line, @"([+\-*/%()=])", " $1 ");
            Line = Regex.Replace(Line, @"\s+", " ");
            return Line;
        }

        /// <summary>
        /// Parses a single line of BOOSE code and converts it into a command object.
        /// Handles comments (lines starting with *), variable declarations, assignments, control structures, arrays, and methods.
        /// Special handling for 'for' loops to preserve negative step values.
        /// </summary>
        /// <param name="Line">The line of code to parse.</param>
        /// <returns>The parsed command object, or null if the line is empty or a comment.</returns>
        /// <exception cref="ParserException">Thrown when syntax errors are detected, invalid types are specified, or undefined variables are used.</exception>
        public virtual ICommand ParseCommand(string Line)
        {
            string typeToken;
            Line = Line.Trim();

            if (Line.Length == 0) return null;             
            if (Line[0] == '*')
            {
                return null;
            }

            string[] rawTokens = Line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string firstToken = rawTokens[0].ToLowerInvariant();

            // Handle FOR separately - don't normalize to preserve negative step
            if (firstToken == "for")
            {
                Debug.WriteLine($"Parsing line: '{Line}'");

                // Just clean up whitespace, don't add spaces around operators
                string cleanedLine = Regex.Replace(Line, @"\s+", " ").Trim();
                string param = cleanedLine.Substring(3).Trim(); // Remove "for"

                ICommand forCmd = commandFactory.MakeCommand("for");
                forCmd.Set(StoredProgram, param);
                forCmd.Compile();
                return forCmd;
            }
            Line = Normalize(Line);
    
            Debug.WriteLine($"Parsing line: '{Line}'");

            string[] tokens = Line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            firstToken = tokens[0];
            string parameters = tokens.Length > 1 ? string.Join(" ", tokens.Skip(1)).Trim() : string.Empty;
            
            if (firstToken == "end" && tokens.Length > 1)
            {
                string endType = tokens[1].ToLowerInvariant();
                string combinedCommand = "end " + endType;  // "end if", "end while", "end for"
                ICommand endCmd = commandFactory.MakeCommand(combinedCommand);
                endCmd.Set(StoredProgram, endType);
                endCmd.Compile();
                return endCmd;
            }

            if (firstToken.Equals("array", StringComparison.OrdinalIgnoreCase))
            {
                var paramTokens = parameters.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (paramTokens.Length < 3)
                {
                    throw new ParserException("Invalid array declaration. Expected: array <type> <name> <size>");
                }

                string elemType = paramTokens[0].ToLowerInvariant();
                if (!(elemType == "int" || elemType == "real" || elemType == "boolean" || elemType == "bool"))
                {
                    throw new ParserException($"Invalid array element type: {paramTokens[0]}");
                }

                // proceed to create array command (the command's Set/Compile should handle details)
                ICommand parsedCommand = commandFactory.MakeCommand(firstToken);
                parsedCommand.Set(StoredProgram, parameters);
                parsedCommand.Compile();
                return parsedCommand;
            }

            if (tokens.Length > 1 && tokens[1].Trim() == "=") {

                if (!StoredProgram.VariableExists(firstToken))
                {
                    throw new ParserException($"Undefined variable: {firstToken}");
                }

                string rest = string.Join(" ", tokens.Skip(1)).Trim(); 
                string fullParam = firstToken + " " + rest; 

                Evaluation existingVar = StoredProgram.GetVariable(firstToken);
                if (existingVar == null)
                {
                    throw new ParserException($"Variable lookup failed: {firstToken}");
                }


                var name = existingVar.GetType().Name.ToLowerInvariant();
                if (name.Contains("int")) typeToken = "int";
                else if (name.Contains("real")) typeToken = "real";
                else if (name.Contains("bool") || name.Contains("boolean")) typeToken = "boolean";
                else throw new ParserException($"Unsupported variable type: {existingVar.GetType().Name}");
                
                ICommand parsedAssign = commandFactory.MakeCommand(typeToken);
                parsedAssign.Set(StoredProgram, fullParam);
                parsedAssign.Compile();

                return parsedAssign;
            }
            switch (firstToken.ToLower())
            {
                case "int":
                    typeToken = "int";
                    break;

                case "real":
                    typeToken = "real";
                    break;

                case "bool":
                case "boolean":
                    typeToken = "boolean";
                    break;

                default:
                    typeToken = firstToken;
                    break;
            }

            ICommand parsed = commandFactory.MakeCommand(firstToken);
            parsed.Set(StoredProgram, parameters);
            parsed.Compile();
            return parsed;
        }


        /// <summary>
        /// Parses an entire BOOSE program by splitting it into lines and parsing each line individually.
        /// Empty lines and whitespace-only lines are skipped.
        /// </summary>
        /// <param name="program">The complete program text to parse, with commands separated by newlines.</param>
        public virtual void ParseProgram(string program)
        {
            string[] lines = program.Split('\n');
            for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                string line = lines[lineNumber];

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                ParseCommand(line);

            }


        }
    }
}
