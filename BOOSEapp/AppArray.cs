using BOOSE;
using System;

namespace BOOSEapp
{
    /// <summary>
    /// Represents an array variable in the BOOSE language.
    /// Supports one-dimensional and two-dimensional arrays of integer or real types.
    /// Provides peek (read) and poke (write) operations for array element access.
    /// </summary>
    public class AppArray : Evaluation
    {
        /// <summary>
        /// The element type of the array ("int" or "real").
        /// </summary>
        protected string ElemType = "int";

        /// <summary>
        /// The number of rows in the array.
        /// </summary>
        protected int rows;

        /// <summary>
        /// The number of columns in the array (defaults to 1 for one-dimensional arrays).
        /// </summary>
        protected int columns = 1;

        /// <summary>
        /// Storage for integer array elements.
        /// </summary>
        protected int[,] intArray;

        /// <summary>
        /// Storage for real (double) array elements.
        /// </summary>
        protected double[,] realArray;

        /// <summary>
        /// The variable name to store the peeked value.
        /// </summary>
        public string peekVar;

        /// <summary>
        /// The value expression to poke into the array.
        /// </summary>
        public string pokeValue;

        /// <summary>
        /// The expression for the row index.
        /// </summary>
        public string rowExpr;

        /// <summary>
        /// The expression for the column index.
        /// </summary>
        public string colExpr;

        /// <summary>
        /// The evaluated row index.
        /// </summary>
        protected int row;

        /// <summary>
        /// The evaluated column index.
        /// </summary>
        protected int column;

        /// <summary>
        /// Initializes a new instance of the BooseArray class.
        /// </summary>
        public AppArray() : base() { }

        /// <summary>
        /// Compiles the array declaration by parsing the type, name, and dimensions, then initializing the array storage.
        /// </summary>
        public override void Compile()
        {
            var parts = (ParameterList ?? string.Empty).Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            ElemType = parts[0].ToLower();
            rows = int.Parse(parts[2]);
            if (parts.Length == 4) columns = int.Parse(parts[3]);

            VarName = parts[1];
         
            if (ElemType == "int") intArray = new int[rows, columns];
            else realArray = new double[rows, columns];

            Program.AddVariable(this);
        }

        /// <summary>
        /// Processes array access parameters during compilation for peek or poke operations.
        /// Parses the variable name, row expression, and column expression from the parameter string.
        /// </summary>
        /// <param name="peekOrPoke">True for poke (write) operations, false for peek (read) operations.</param>
        /// <param name="parameters">The parameter string containing array access syntax.</param>
        public void ProcessArrayParametersCompile(bool peekOrPoke, string parameters)
        {
            var parts = parameters.Split('=', 2);
            if (peekOrPoke)
            {

                pokeValue = parts[1].Trim();
                var leftParts = parts[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                VarName = leftParts[0];
                rowExpr = leftParts[1];
                colExpr = leftParts.Length >= 3 ? leftParts[2] : "0";
            }
            else
            {
                peekVar = parts[0].Trim();
                var rightParts = parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                VarName = rightParts[0];
                rowExpr = rightParts[1];
                colExpr = rightParts.Length >= 3 ? rightParts[2] : "0";
            }
        }
        /// <summary>
        /// Executes array access operations by evaluating row and column expressions and performing the peek or poke operation.
        /// </summary>
        /// <param name="peekOrPoke">True for poke (write) operations, false for peek (read) operations.</param>
        public void ProcessArrayParametersExecute(bool peekOrPoke)
        {
            if (Program.IsExpression(rowExpr))
            {
                row = int.Parse(Program.EvaluateExpression(rowExpr));
            }
            else
            {
                row = int.Parse(rowExpr);
            }

            if (string.IsNullOrEmpty(colExpr))
            {
                column = 0;
            }
            else if (Program.IsExpression(colExpr))
            {
                column = int.Parse(Program.EvaluateExpression(colExpr));
            }
            else
            {
                column = int.Parse(colExpr);
            }

            if (peekOrPoke)
            {
                var valExpr = Program.IsExpression(pokeValue) ? Program.EvaluateExpression(pokeValue) : pokeValue;
                if (ElemType == "int")
                {
                    intArray[row, column] = int.Parse(valExpr);
                }
                else
                {
                    realArray[row, column] = double.Parse(valExpr);
                }
            }
            else
            {
                if (ElemType == "int")
                {
                    Program.UpdateVariable(peekVar, intArray[row, column]);
                }
                else
                {
                    Program.UpdateVariable(peekVar, realArray[row, column]);
                }
            }
        }
        /// <summary>
        /// Gets the integer value at the specified row and column.
        /// </summary>
        /// <param name="r">The row index.</param>
        /// <param name="c">The column index.</param>
        /// <returns>The integer value at the specified position.</returns>
        public int GetIntValue(int r, int c) => intArray[r, c];

        /// <summary>
        /// Gets the real (double) value at the specified row and column.
        /// </summary>
        /// <param name="r">The row index.</param>
        /// <param name="c">The column index (defaults to 0).</param>
        /// <returns>The real value at the specified position.</returns>
        public double GetRealValue(int r, int c = 0) => realArray[r, c];

        /// <summary>
        /// Sets the integer value at the specified row and column.
        /// </summary>
        /// <param name="r">The row index.</param>
        /// <param name="c">The column index.</param>
        /// <param name="val">The integer value to set.</param>
        public void SetIntValue(int r, int c, int val) => intArray[r, c] = val;

        /// <summary>
        /// Sets the real (double) value at the specified row and column.
        /// </summary>
        /// <param name="r">The row index.</param>
        /// <param name="c">The column index.</param>
        /// <param name="val">The real value to set.</param>
        public void SetRealValue(int r, int c, double val) => realArray[r, c] = val;
    }
}
