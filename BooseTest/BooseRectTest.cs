using BOOSEapp;

namespace BooseTest;
/// <summary>
/// Tests that drawing a rectangle does not change the canvas pen position.
/// </summary>
[TestClass]
public class BooseRectTest
{
    /// <summary>
    /// Tests that drawing a rectangle does not change the canvas pen position.
    /// </summary>
    [TestMethod]
    public void RectTest()
    {
        var canvas = new BooseCanvas(200, 200);
        canvas.Rect(40, 60, true);

        Assert.AreEqual(0, canvas.Xpos, "Canvas X position should remain unchanged after drawing a rectangle.");
    }
}
