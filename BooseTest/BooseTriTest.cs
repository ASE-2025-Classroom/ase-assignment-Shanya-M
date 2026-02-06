using BOOSEapp;

namespace BooseTest;
/// <summary>
/// Tests that drawing a triangle does not change the canvas pen position.
/// </summary>
[TestClass]
public class BooseTriTest
{
    /// <summary>
    /// Tests that drawing a triangle does not change the canvas pen position.
    /// </summary>
    [TestMethod]
    public void TriTest()
    {
        var canvas = new BooseCanvas(200, 200);
        canvas.Tri(50, 70);
        Assert.AreEqual(0, canvas.Xpos, "Canvas X position should remain unchanged after drawing a triangle.");
    }
}
