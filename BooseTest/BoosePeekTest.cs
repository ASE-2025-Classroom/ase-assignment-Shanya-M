using BOOSEapp;
using BOOSE;

namespace BooseTest;

/// <summary>
/// Tests for the BoosePeek command to verify reading values from arrays.
/// </summary>
[TestClass]
public class BoosePeekTest
{
    /// <summary>
    /// Simple test: peek reads an integer value from an array.
    /// </summary>
    [TestMethod]
    public void Peek_SimpleInt_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("array int nums 10");
        parser.ParseCommand("int x = 0");
        parser.ParseCommand("poke nums 5 = 99");
        parser.ParseCommand("peek x = nums 5");
        
        program.Run();
        
        var x = program.GetVariable("x");
        Assert.AreEqual(99, x.Value, "Peek should read the correct value from the array.");
    }
}
