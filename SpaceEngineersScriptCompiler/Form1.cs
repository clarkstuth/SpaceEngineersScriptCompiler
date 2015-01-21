using System;
using System.IO;
using System.Windows.Forms;

namespace SpaceEngineersScriptCompiler
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void solutionDirButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;

            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var file = openFileDialog.SafeFileName;

                solutionDirTextBox.Text = file;
            }
        }

        private void outputDirButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;

            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var dir = Path.GetDirectoryName(openFileDialog.FileName);
                
                outputDirTextBox.Text = dir;
            }
        }

    }
}
