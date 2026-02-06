using BOOSEapp;

namespace BooseTest;
/// <summary>
/// Tests that the SetColour command correctly updates the pen color using RGB values.
/// </summary>
[TestClass]
public class BooseSetColTest
{
    /// <summary>
    /// Tests that the SetColour command correctly updates the pen color using RGB values.
    /// </summary>
    [TestMethod]
    public void SetColourTest()
    {
        var canvas = new BooseCanvas(200, 200);
        canvas.SetColour(255, 0, 0); // Set to red
        var penColor = canvas.GetPenColour();
        Assert.AreEqual(255, penColor.R, "Pen red component was not set correctly.");
        Assert.AreEqual(0, penColor.G, "Pen green component was not set correctly.");
        Assert.AreEqual(0, penColor.B, "Pen blue component was not set correctly.");
    }
}
