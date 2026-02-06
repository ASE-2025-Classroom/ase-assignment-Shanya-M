using BOOSE;
using System.Diagnostics;
using System.Drawing;

namespace BOOSEapp
{
    /// <summary>
    /// Main form class for the BOOSE application. Handles user interface interactions,
    /// program parsing, execution, and canvas rendering.
    /// </summary>
    public partial class BooseApp : Form
    {
        AppCanvas myCanvas;
        AppCommandFactory factory;
        StoredProgram program;
        AppParser parser;

        /// <summary>
        /// Initializes a new instance of the BooseApp form and sets up the canvas, command factory, and parser.
        /// </summary>
        public BooseApp()
        {
            InitializeComponent();
            Debug.WriteLine("BooseApp initialized");
            Debug.WriteLine(AboutBOOSE.about());
            myCanvas = new AppCanvas(761, 514);
            factory = new AppCommandFactory();

            program = new StoredProgram(myCanvas);
            parser = new AppParser(factory, program);
            myCanvas.WriteText(AboutBOOSE.about());
        }

        /// <summary>
        /// Gets or sets the user input text from the text box.
        /// </summary>
        public string UserInput
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }
        /// <summary>
        /// Handles the click event for the Run button. Parses and executes the BOOSE program from the user input.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void RunButton_Click(object sender, EventArgs e)
        {
            string input = UserInput;
            myCanvas?.Clear();

            program = new StoredProgram(myCanvas);
            parser = new AppParser(factory, program);


            try
            {
                parser.ParseProgram(UserInput);
                program.Run();
            }
            catch (ParserException ex)
            {
                myCanvas?.Clear();
                myCanvas?.WriteText($"Error: {ex.Message}");

            }
            catch (CommandException ex)
            {
                myCanvas?.Clear();
                myCanvas?.WriteText($"Error: {ex.Message}");

            }
            catch (CanvasException ex)
            {
                myCanvas?.Clear();
                myCanvas?.WriteText($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                myCanvas?.Clear();
                myCanvas?.WriteText($"Error: {ex.Message}");
                Debug.WriteLine(ex);

            }
            finally
            {
                pictureBox1?.Invalidate();
                pictureBox1?.Refresh();
            }
        }

        /// <summary>
        /// Handles the paint event for the panel, rendering the canvas bitmap to the display.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The paint event arguments containing the graphics object.</param>
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap bitmap = (Bitmap)myCanvas.getBitmap();
            g.DrawImage(bitmap, 0, 0);
        }

        /// <summary>
        /// Handles the click event for label1. Currently no action is performed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the click event for the Clear button. Clears the canvas and resets the user input text box.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Clear button clicked");

            myCanvas?.Clear();
            myCanvas?.WriteText(AboutBOOSE.about());
            UserInput = string.Empty;
            textBox1.Text = string.Empty;

            pictureBox1?.Invalidate();
            pictureBox1?.Refresh();

        }


    }
}
