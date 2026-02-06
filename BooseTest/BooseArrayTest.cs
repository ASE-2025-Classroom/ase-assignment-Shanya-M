using BOOSEapp;
using BOOSE;

namespace BooseTest;

/// <summary>
/// Tests for the BooseArray class to verify array declaration, compilation, and element access.
/// </summary>
[TestClass]
public class BooseArrayTest
{
    /// <summary>
    /// Tests that a one-dimensional integer array is correctly created and compiled.
    /// </summary>
    [TestMethod]
    public void IntArray1D_CreationTest()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("array int numbers 10");
        program.Run();
        
        Assert.IsTrue(program.VariableExists("numbers"), "Array variable should exist in program.");
    }

    /// <summary>
    /// Tests that a one-dimensional real array is correctly created and compiled.
    /// </summary>
    [TestMethod]
    public void RealArray1D_CreationTest()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("array real prices 5");
        program.Run();
        
        Assert.IsTrue(program.VariableExists("prices"), "Array variable should exist in program.");
    }

    /// <summary>
    /// Tests that integer values can be set and retrieved from an array.
    /// </summary>
    [TestMethod]
    public void IntArray_SetAndGetValue_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("array int nums 10");
        program.Run();
        
        var array = program.GetVariable("nums") as BooseArray;
        Assert.IsNotNull(array, "Array should exist.");
        
        array.SetIntValue(5, 0, 99);
        int result = array.GetIntValue(5, 0);
        
        Assert.AreEqual(99, result, "Array should store and retrieve the correct integer value.");
    }

    /// <summary>
    /// Tests that real values can be set and retrieved from an array.
    /// </summary>
    [TestMethod]
    public void RealArray_SetAndGetValue_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("array real values 10");
        program.Run();
        
        var array = program.GetVariable("values") as BooseArray;
        Assert.IsNotNull(array, "Array should exist.");
        
        array.SetRealValue(3, 0, 3.14);
        double result = array.GetRealValue(3, 0);
        
        Assert.AreEqual(3.14, result, 0.001, "Array should store and retrieve the correct real value.");
    }

    /// <summary>
    /// Tests that a two-dimensional integer array is correctly created.
    /// </summary>
    [TestMethod]
    public void IntArray2D_CreationTest()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("array int matrix 5 5");
        program.Run();
        
        Assert.IsTrue(program.VariableExists("matrix"), "2D array variable should exist in program.");
    }

    /// <summary>
    /// Tests that values can be set and retrieved from a 2D array.
    /// </summary>
    [TestMethod]
    public void IntArray2D_SetAndGetValue_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("array int grid 3 3");
        program.Run();
        
        var array = program.GetVariable("grid") as BooseArray;
        Assert.IsNotNull(array, "Array should exist.");
        
        array.SetIntValue(1, 2, 42);
        int result = array.GetIntValue(1, 2);
        
        Assert.AreEqual(42, result, "2D array should store and retrieve the correct value at row 1, col 2.");
    }
}
