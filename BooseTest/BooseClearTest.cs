using BOOSEapp;
using System.Drawing;

namespace BooseTest;
/// <summary>
/// Tests that the Clear command resets the entire canvas to white, removing all previous drawings.
/// </summary>
[TestClass]
public class BooseClearTest
{
    /// <summary>
    /// Tests that the Clear command resets the entire canvas to white, removing all previous drawings.
    /// </summary>
    [TestMethod]
    public void ClearTest()
    {
        var canvas = new BooseCanvas(200, 200);


        canvas.SetColour(255, 0, 0);
        canvas.Rect(200, 200, true);
        canvas.Clear();

        var bitmap = (Bitmap)canvas.getBitmap();
        var clearedColor = bitmap.GetPixel(50, 50);


        Assert.AreEqual(Color.White.ToArgb(), clearedColor.ToArgb(), "Canvas was not cleared correctly.");
    }
}
