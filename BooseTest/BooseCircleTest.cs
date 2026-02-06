using BOOSEapp;

namespace BooseTest;
/// <summary>
/// Tests that drawing a circle does not change the canvas pen position.
/// </summary>
[TestClass]
public sealed class BooseCircleTest

{

    /// <summary>
    /// Tests that drawing a circle does not change the canvas pen position.
    /// </summary>
    [TestMethod]
    public void CircleTest()
    {
        var canvas = new BooseCanvas(200, 200);
        canvas.Circle(30, false);

        Assert.AreEqual(0, canvas.Xpos, "Canvas X position should remain unchanged after drawing a circle.");

    }
}
