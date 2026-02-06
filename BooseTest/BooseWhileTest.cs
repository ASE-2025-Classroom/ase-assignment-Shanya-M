using BOOSEapp;
using BOOSE;

namespace BooseTest;

/// <summary>
/// Tests for the BooseWhile class to verify while loop execution.
/// </summary>
[TestClass]
public class BooseWhileTest
{
    /// <summary>
    /// Simple test: while loop iterates correctly.
    /// </summary>
    [TestMethod]
    public void While_SimpleLoop_Test()
    {
        var canvas = new BooseCanvas(200, 200);
        var program = new StoredProgram(canvas);
        var factory = new AppCommandFactory();
        var parser = new BooseParser(factory, program);
        
        string code = @"
int counter = 0
int sum = 0
while counter < 5
counter = counter + 1
sum = sum + counter
end while
";
        
        parser.ParseProgram(code);
        program.Run();
        
        var sum = program.GetVariable("sum");
        Assert.AreEqual(15, sum.Value, "While loop should sum 1+2+3+4+5 = 15.");
    }
}
