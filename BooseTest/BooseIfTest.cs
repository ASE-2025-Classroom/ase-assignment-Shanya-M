using BOOSEapp;
using BOOSE;

namespace BooseTest;

/// <summary>
/// Tests for the BooseIf class to verify conditional execution of code blocks.
/// </summary>
[TestClass]
public class BooseIfTest
{
    /// <summary>
    /// Simple test: if block executes when condition is true.
    /// </summary>
    [TestMethod]
    public void If_SimpleCondition_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        string code = @"
int x = 10
int result = 0
if x > 5
result = 100
end if
";
        
        parser.ParseProgram(code);
        program.Run();
        
        var result = program.GetVariable("result");
        Assert.AreEqual(100, result.Value, "If block should execute when condition is true.");
    }
}

