namespace ToppyMcTopface
{
    partial class Toppy
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
            this.header = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.body = new ToppyMcTopface.TickerLabel();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.header.AutoSize = true;
            this.header.BackColor = System.Drawing.Color.Navy;
            this.header.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header.ForeColor = System.Drawing.Color.White;
            this.header.Location = new System.Drawing.Point(12, 9);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(92, 21);
            this.header.TabIndex = 0;
            this.header.Text = "Hello World";
            this.header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.AutoSize = true;
            this.close.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.close.BackColor = System.Drawing.Color.White;
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Location = new System.Drawing.Point(361, 7);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(47, 25);
            this.close.TabIndex = 1;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = false;
            this.close.Click += new System.EventHandler(this.CloseClick);
            // 
            // body
            // 
            this.body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.body.BackColor = System.Drawing.Color.DarkRed;
            this.body.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.body.ForeColor = System.Drawing.Color.White;
            this.body.Fps = 60;
            this.body.Location = new System.Drawing.Point(12, 37);
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(396, 21);
            this.body.Speed = 40;
            this.body.TabIndex = 0;
            this.body.Text = "Omnis quis laboriosam atque est. Explicabo omnis et dolor totam accusamus est vol" +
    "uptatem. Sint et possimus repellat dolorem. +++";
            this.body.TickerEnabled = true;
            this.body.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            // 
            // Toppy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(420, 70);
            this.ControlBox = false;
            this.Controls.Add(this.close);
            this.Controls.Add(this.body);
            this.Controls.Add(this.header);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Toppy";
            this.Opacity = 0.85D;
            this.ShowIcon = false;
            this.Text = "Toppy McTopface";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label header;
        private TickerLabel body;
        private System.Windows.Forms.Button close;
    }
}

