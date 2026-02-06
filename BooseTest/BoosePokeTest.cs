using BOOSEapp;
using BOOSE;

namespace BooseTest;

/// <summary>
/// Tests for the BoosePoke command to verify writing values to arrays.
/// </summary>
[TestClass]
public class BoosePokeTest
{
    /// <summary>
    /// Simple test: poke writes an integer value to an array.
    /// </summary>
    [TestMethod]
    public void Poke_SimpleInt_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        parser.ParseCommand("array int nums 10");
        parser.ParseCommand("poke nums 7 = 555");
        
        program.Run();
        
        var array = program.GetVariable("nums") as BooseArray;
        Assert.IsNotNull(array, "Array should exist.");
        Assert.AreEqual(555, array.GetIntValue(7, 0), "Poke should write the correct value to the array.");
    }
}
