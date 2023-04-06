namespace _2048
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 600);
            MaximumSize = new Size(600, 600);
            MinimumSize = new Size(600, 600);
            Name = "Form1";
            Text = "2048";
            KeyPreview = true;
            Load += Game;
            Load += (s, e) =>
            {
                MessageBox.Show("Use WASD to move tiles");
            };
            KeyDown += MoveOnKey;
            KeyDown += (s, e) =>
            {
                if (this.Controls.Cast<Button>().ToList().Any(x => x.Text.Equals("2048")))
                {
                    MessageBox.Show("You Won! Congratulations!");
                    Application.Restart();
                    Environment.Exit(0);
                }
                else if (this.Controls.Count >= _fieldSize*_fieldSize) 
                {
                    MessageBox.Show("Game Over!");
                    Application.Restart();
                    Environment.Exit(0);
                }
            };
            ResumeLayout(false);
        }
        #endregion
    }
}