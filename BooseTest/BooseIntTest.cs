using BOOSEapp;
using BOOSE;

namespace BooseTest;

/// <summary>
/// Tests for the BooseInt class to verify integer variable declaration, assignment, and evaluation.
/// </summary>
[TestClass]
public class BooseIntTest
{
    /// <summary>
    /// Tests that an integer variable can be declared and initialized.
    /// </summary>
    [TestMethod]
    public void Int_DeclarationAndInitialization_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("int x = 42");
        program.Run();
        
        var x = program.GetVariable("x");
        Assert.IsNotNull(x, "Variable x should exist.");
        Assert.AreEqual(42, x.Value, "Variable x should have the value 42.");
    }

    /// <summary>
    /// Tests that an integer variable can be declared without initialization.
    /// </summary>
    [TestMethod]
    public void Int_DeclarationOnly_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("int y");
        program.Run();
        
        Assert.IsTrue(program.VariableExists("y"), "Variable y should exist after declaration.");
    }

    /// <summary>
    /// Tests that an integer variable can be assigned a new value.
    /// </summary>
    [TestMethod]
    public void Int_Reassignment_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("int count = 10");
        parser.ParseCommand("count = 20");
        program.Run();
        
        var count = program.GetVariable("count");
        Assert.AreEqual(20, count.Value, "Variable count should be updated to 20.");
    }

    /// <summary>
    /// Tests that integer expressions are evaluated correctly.
    /// </summary>
    [TestMethod]
    public void Int_ExpressionEvaluation_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("int a = 5");
        parser.ParseCommand("int b = 10");
        parser.ParseCommand("int result = a + b");
        program.Run();
        
        var result = program.GetVariable("result");
        Assert.AreEqual(15, result.Value, "Expression 5 + 10 should equal 15.");
    }

    /// <summary>
    /// Tests that negative integers are handled correctly.
    /// </summary>
    [TestMethod]
    public void Int_NegativeValue_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("int negative = -50");
        program.Run();
        
        var negative = program.GetVariable("negative");
        Assert.AreEqual(-50, negative.Value, "Variable should store negative value -50.");
    }
}
