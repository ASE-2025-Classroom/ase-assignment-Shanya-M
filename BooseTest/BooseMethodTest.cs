using BOOSEapp;
using BOOSE;

namespace BooseTest;

/// <summary>
/// Tests for the BooseMethod class to verify method definition and calling.
/// </summary>
[TestClass]
public class BooseMethodTest
{
    /// <summary>
    /// Simple test: method can be defined and called.
    /// </summary>
    [TestMethod]
    public void Method_SimpleCall_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        string code = @"
method int result int one, int two
result = one * two
end method
call result 10 5
write result
";
        
        parser.ParseProgram(code);
        program.Run();
        
        var result = program.GetVariable("result");
        Assert.AreEqual(50, result.Value, "Method should set result to 50 (10 * 5) when called.");
    }
}
