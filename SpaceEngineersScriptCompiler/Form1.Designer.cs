namespace SpaceEngineersScriptCompiler
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.solutionDirLabel = new System.Windows.Forms.Label();
            this.solutionDirTextBox = new System.Windows.Forms.TextBox();
            this.solutionDirButton = new System.Windows.Forms.Button();
            this.outputDirLabel = new System.Windows.Forms.Label();
            this.outputDirTextBox = new System.Windows.Forms.TextBox();
            this.outputDirButton = new System.Windows.Forms.Button();
            this.generateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // solutionDirLabel
            // 
            this.solutionDirLabel.AutoSize = true;
            this.solutionDirLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solutionDirLabel.Location = new System.Drawing.Point(12, 22);
            this.solutionDirLabel.Name = "solutionDirLabel";
            this.solutionDirLabel.Size = new System.Drawing.Size(168, 20);
            this.solutionDirLabel.TabIndex = 0;
            this.solutionDirLabel.Text = "Visual Studio Solution:";
            // 
            // solutionDirTextBox
            // 
            this.solutionDirTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.solutionDirTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solutionDirTextBox.Location = new System.Drawing.Point(186, 19);
            this.solutionDirTextBox.Name = "solutionDirTextBox";
            this.solutionDirTextBox.ReadOnly = true;
            this.solutionDirTextBox.Size = new System.Drawing.Size(300, 26);
            this.solutionDirTextBox.TabIndex = 1;
            // 
            // solutionDirButton
            // 
            this.solutionDirButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solutionDirButton.Location = new System.Drawing.Point(492, 19);
            this.solutionDirButton.Name = "solutionDirButton";
            this.solutionDirButton.Size = new System.Drawing.Size(66, 26);
            this.solutionDirButton.TabIndex = 2;
            this.solutionDirButton.Text = "Select";
            this.solutionDirButton.UseVisualStyleBackColor = true;
            this.solutionDirButton.Click += new System.EventHandler(this.solutionDirButton_Click);
            // 
            // outputDirLabel
            // 
            this.outputDirLabel.AutoSize = true;
            this.outputDirLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputDirLabel.Location = new System.Drawing.Point(13, 58);
            this.outputDirLabel.Name = "outputDirLabel";
            this.outputDirLabel.Size = new System.Drawing.Size(129, 20);
            this.outputDirLabel.TabIndex = 3;
            this.outputDirLabel.Text = "Output Directory:";
            // 
            // outputDirTextBox
            // 
            this.outputDirTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputDirTextBox.Location = new System.Drawing.Point(186, 57);
            this.outputDirTextBox.Name = "outputDirTextBox";
            this.outputDirTextBox.ReadOnly = true;
            this.outputDirTextBox.Size = new System.Drawing.Size(300, 26);
            this.outputDirTextBox.TabIndex = 4;
            // 
            // outputDirButton
            // 
            this.outputDirButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputDirButton.Location = new System.Drawing.Point(492, 57);
            this.outputDirButton.Name = "outputDirButton";
            this.outputDirButton.Size = new System.Drawing.Size(66, 26);
            this.outputDirButton.TabIndex = 5;
            this.outputDirButton.Text = "Select";
            this.outputDirButton.UseVisualStyleBackColor = true;
            this.outputDirButton.Click += new System.EventHandler(this.outputDirButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generateButton.Location = new System.Drawing.Point(481, 104);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(86, 26);
            this.generateButton.TabIndex = 6;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 145);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.outputDirButton);
            this.Controls.Add(this.outputDirTextBox);
            this.Controls.Add(this.outputDirLabel);
            this.Controls.Add(this.solutionDirButton);
            this.Controls.Add(this.solutionDirTextBox);
            this.Controls.Add(this.solutionDirLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "Space Engineers Script Compiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label solutionDirLabel;
        private System.Windows.Forms.TextBox solutionDirTextBox;
        private System.Windows.Forms.Button solutionDirButton;
        private System.Windows.Forms.Label outputDirLabel;
        private System.Windows.Forms.TextBox outputDirTextBox;
        private System.Windows.Forms.Button outputDirButton;
        private System.Windows.Forms.Button generateButton;
    }
}

