using BOOSE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOOSEapp
{
    /// <summary>
    /// Implements the ICanvas interface to provide drawing functionality for the BOOSE application.
    /// Manages a bitmap canvas with drawing operations including shapes, lines, and text.
    /// </summary>
    public class AppCanvas : ICanvas
    {
        Bitmap canvasBitmap;
        Graphics graphics;
        private int xPos, yPos;
        Pen pen;
        private readonly Font textFont = new Font("Arial", 10f);
        private readonly float textPadding = 10f;

        /// <summary>
        /// Initializes a new instance of the BooseCanvas class with the specified dimensions.
        /// </summary>
        /// <param name="xsize">The width of the canvas in pixels.</param>
        /// <param name="ysize">The height of the canvas in pixels.</param>
        public AppCanvas(int xsize, int ysize) 
        {
            canvasBitmap = new Bitmap(xsize, ysize);
            graphics = Graphics.FromImage(canvasBitmap);
            pen = new Pen(Color.Black);
            pen.Width = 7;
        }
        /// <summary>
        /// Gets or sets the X coordinate of the current drawing position.
        /// </summary>
        public int Xpos { get => xPos; set => xPos = value; }
        /// <summary>
        /// Gets or sets the Y coordinate of the current drawing position.
        /// </summary>
        public int Ypos { get => yPos; set => yPos = value; }
        /// <summary>
        /// Gets or sets the pen color. Currently not implemented.
        /// </summary>
        public object PenColour { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Draws a circle at the current position with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle in pixels.</param>
        /// <param name="filled">If true, the circle is filled; otherwise, only the outline is drawn.</param>
        public void Circle(int radius, bool filled)
        {
            int x = Xpos - radius;
            int y = Ypos - radius;
            
            if (filled)
            {
                using var brush = new SolidBrush(Color.Black);
                graphics.FillEllipse(brush, x, y, radius * 2, radius * 2);
            
            }
            else
            {

                graphics.DrawEllipse(pen, x, y, radius * 2, radius * 2);
            
            }
        }
        /// <summary>
        /// Gets the current pen color.
        /// </summary>
        /// <returns>The current color of the pen.</returns>
        public Color GetPenColour()
        {
            return pen.Color;
        }

        /// <summary>
        /// Clears the entire canvas by filling it with white.
        /// </summary>
        public void Clear()
        {
            graphics.Clear(Color.White);
        }

        /// <summary>
        /// Draws a line from the current position to the specified coordinates and updates the position.
        /// </summary>
        /// <param name="x">The target X coordinate.</param>
        /// <param name="y">The target Y coordinate.</param>
        public void DrawTo(int x, int y)
        {
           graphics.DrawLine(pen, xPos, yPos, x, y);
            Xpos = x;
            Ypos = y;
        }

        /// <summary>
        /// Gets the underlying bitmap representation of the canvas.
        /// </summary>
        /// <returns>The bitmap containing the current canvas drawing.</returns>
        public object getBitmap()
        {
            return canvasBitmap;
        }

        /// <summary>
        /// Moves the drawing position to the specified coordinates without drawing.
        /// </summary>
        /// <param name="x">The target X coordinate.</param>
        /// <param name="y">The target Y coordinate.</param>
        public void MoveTo(int x, int y)
        {
            Xpos = x;
            Ypos = y;
        }

        /// <summary>
        /// Draws a rectangle at the current position with the specified dimensions.
        /// </summary>
        /// <param name="width">The width of the rectangle in pixels.</param>
        /// <param name="height">The height of the rectangle in pixels.</param>
        /// <param name="filled">If true, the rectangle is filled; otherwise, only the outline is drawn.</param>
        public void Rect(int width, int height, bool filled)
        {
           graphics.DrawRectangle(pen, xPos, yPos, width, height);
        }

        /// <summary>
        /// Resets the drawing position to the center of the canvas.
        /// </summary>
        public void Reset()
        {
            Xpos = 0;
            Ypos = 0;

        }

        /// <summary>
        /// Sets the canvas size. Currently not implemented.
        /// </summary>
        /// <param name="width">The desired width of the canvas.</param>
        /// <param name="height">The desired height of the canvas.</param>
        public void Set(int width, int height)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the pen color using RGB values.
        /// </summary>
        /// <param name="red">The red component (0-255).</param>
        /// <param name="green">The green component (0-255).</param>
        /// <param name="blue">The blue component (0-255).</param>
        public void SetColour(int red, int green, int blue)
        {
            pen.Color = Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// Draws a triangle at the current position with the specified dimensions.
        /// </summary>
        /// <param name="width">The width of the triangle base in pixels.</param>
        /// <param name="height">The height of the triangle in pixels.</param>
        public void Tri(int width, int height)
        {
            graphics.DrawPolygon(pen, new PointF[]
            {
                new PointF(Xpos, Ypos),
                new PointF(Xpos + width / 2, Ypos + height),
                new PointF(Xpos - width / 2, Ypos + height)
            }); 


        }

        /// <summary>
        /// Writes text to the canvas at the top-left area with padding.
        /// </summary>
        /// <param name="text">The text string to display on the canvas.</param>
        public void WriteText(string text)
        {

            using (var brush = new SolidBrush(GetPenColour()))
            {
                graphics.DrawString(text, textFont, brush,
                new RectangleF(Xpos, Ypos, canvasBitmap.Width - 20, canvasBitmap.Height - 20));

            }
        }

    }
}
