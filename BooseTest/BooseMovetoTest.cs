using BOOSEapp;

namespace BooseTest;
/// <summary>
/// Tests that the MoveTo command correctly updates the canvas pen position without drawing.
/// </summary>
[TestClass]
public class BooseMovetoTest
{
    /// <summary>
    /// Tests that the MoveTo command correctly updates the canvas pen position without drawing.
    /// </summary>
    [TestMethod]
    public void MoveToTest()
    {

        var canvas = new BooseCanvas(200, 200);

        canvas.MoveTo(10, 20);

        Assert.AreEqual(10, canvas.Xpos, "Canvas X position was not updated by MoveTo command.");
        Assert.AreEqual(20, canvas.Ypos, "Canvas Y position was not updated by MoveTo command.");

    }
}
