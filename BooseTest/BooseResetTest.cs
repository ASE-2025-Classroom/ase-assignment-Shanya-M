using BOOSEapp;

namespace BooseTest;
/// <summary>
/// Tests that the Reset command moves the pen position to the center of the canvas.
/// </summary>
[TestClass]
public class BooseResetTest
{
    /// <summary>
    /// Tests that the Reset command moves the pen position to the center of the canvas.
    /// </summary>
    [TestMethod]
    public void ResetTest()
    {
        var canvas = new BooseCanvas(200, 200);
        canvas.MoveTo(150, 150);
        canvas.Reset();
        Assert.AreEqual(0, canvas.Xpos, "Canvas X position was not reset correctly.");
        Assert.AreEqual(0, canvas.Ypos, "Canvas Y position was not reset correctly.");
    }
}
