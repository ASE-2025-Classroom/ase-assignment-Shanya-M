using BOOSEapp;
using BOOSE;

namespace BooseTest;

/// <summary>
/// Tests for the BooseReal class to verify real (double) variable declaration and assignment.
/// </summary>
[TestClass]
public class BooseRealTest
{
    /// <summary>
    /// Simple test: real variable declaration and initialization.
    /// </summary>
    [TestMethod]
    public void Real_SimpleDeclaration_Test()
        {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("real pi = 3.14");
        program.Run();
        
        var pi = program.GetVariable("pi") as Real;
        Assert.IsNotNull(pi, "Variable pi should exist.");
        Assert.AreEqual(3.14, pi.Value, 0.01, "Variable pi should have the value 3.14.");
    }
}
