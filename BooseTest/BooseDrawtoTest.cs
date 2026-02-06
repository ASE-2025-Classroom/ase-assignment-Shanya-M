using BOOSEapp;

namespace BooseTest;
/// <summary>
/// Tests that the DrawTo command correctly draws a line and updates the canvas pen position.
/// </summary>
[TestClass]
public class BooseDrawtoTest
{
    /// <summary>
    /// Tests that the DrawTo command correctly draws a line and updates the canvas pen position.
    /// </summary>
    [TestMethod]
    public void DrawToTest()
    {
        var canvas = new BooseCanvas(200, 200);

        canvas.DrawTo(50, 80);
        Assert.AreEqual(50, canvas.Xpos, "Canvas X position was not updated by DrawTo command.");
        Assert.AreEqual(80, canvas.Ypos, "Canvas Y position was not updated by DrawTo command.");

    }
}
