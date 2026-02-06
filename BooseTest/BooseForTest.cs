using BOOSEapp;
using BOOSE;

namespace BooseTest;

/// <summary>
/// Tests for the BooseFor class to verify for loop execution.
/// </summary>
[TestClass]
public class BooseForTest
{
    /// <summary>
    /// Simple test: for loop iterates correctly.
    /// </summary>
    [TestMethod]
    public void For_SimpleLoop_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        string code = @"
int sum = 0
for i = 1 to 5
sum = sum + i
end for
";
        
        parser.ParseProgram(code);
        program.Run();
        
        var sum = program.GetVariable("sum");
        Assert.AreEqual(15, sum.Value, "For loop should sum 1+2+3+4+5 = 15.");
    }
}
