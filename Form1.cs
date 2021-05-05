using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace FrameEditor
{
    public partial class Form1 : Form
    {
        // Define variable mesurements units
        private int X = Screen.PrimaryScreen.Bounds.Size.Width / 100;
        private int Y = Screen.PrimaryScreen.Bounds.Size.Height / 100;

        // Create the app panel
        private Panel applicationsPanel = new Panel();

        // Create the text editor button
        private Button textEditorButton = new Button();

        // Create the web browser button
        private Button webBrowserButton = new Button();

        public Form1()
        {
            InitializeComponent();
        }

        // Execute when the form is loaded
        private void Form1_Load(object sender, EventArgs e)
        {
            // Sets the form to fullscreen
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //// APPLICATION PANEL
            // Set the size and position of the application panel
            applicationsPanel.Location = new Point(5 * X, 20 * Y);
            applicationsPanel.Size = new Size(90 * X, 80 * Y);
            // Set the Borderstyle for the application panel to three-dimensional.
            applicationsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            // Add the application panel to the form.
            this.Controls.Add(applicationsPanel);

            //// TEXT EDITOR BUTTON
            // Set the size and position of the text editor button
            textEditorButton.Location = new Point(15 * X, 8 * Y);
            textEditorButton.Size = new Size(20 * X, 8 * Y);
            // Set the display text and font
            textEditorButton.Text = "Text Editor";
            textEditorButton.Font = new Font("Georgia", 12);
            // Add a Button Click Event handler
            textEditorButton.Click += new EventHandler(TextEditorButton_Click);
            // Add the application panel to the form.
            this.Controls.Add(textEditorButton);

            //// WEB BROWSER BUTTON
            // Set the size and position of the web browser button
            webBrowserButton.Location = new Point(65 * X, 8 * Y);
            webBrowserButton.Size = new Size(20 * X, 8 * Y);
            // Set the display text and font
            webBrowserButton.Text = "Web Browser";
            webBrowserButton.Font = new Font("Georgia", 12);
            // Add a Button Click Event handler
            webBrowserButton.Click += new EventHandler(WebBrowserButton_Click);
            // Add the application panel to the form.
            this.Controls.Add(webBrowserButton);



        }


        private void TextEditorButton_Click(object sender, EventArgs e)
        {
            StartApplication("wordpad");
        }


        private void WebBrowserButton_Click(object sender, EventArgs e)
        {
            StartApplication("chrome");
        }

        private void StartApplication(string app)
        {
            // Initiate the process
            ProcessStartInfo psi = new ProcessStartInfo(app + ".exe");

            // Tells the process to run minimized
            psi.WindowStyle = ProcessWindowStyle.Minimized;

            // Start the process
            Process p = Process.Start(psi);

            // Wait for the process to start
            p.WaitForInputIdle();
            Thread.Sleep(2000);

            List<Process> applications = new List<Process>();
            foreach (Process p1 in Process.GetProcesses())
            {
                string procName = Convert.ToString(p1.ProcessName);
                if (procName == app)
                {
                    SetParent(p1.MainWindowHandle, applicationsPanel.Handle);
                    //Set the app to fullscreen
                    ShowWindow(p1.MainWindowHandle, 3);
                }
            }
        }

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
