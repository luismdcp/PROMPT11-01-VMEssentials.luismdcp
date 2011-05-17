namespace Sessao2
{
    partial class Form1
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
            this.prompt = new System.Windows.Forms.Button();
            this.isel = new System.Windows.Forms.Button();
            this.textbox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // prompt
            // 
            this.prompt.Location = new System.Drawing.Point(136, 72);
            this.prompt.Margin = new System.Windows.Forms.Padding(2);
            this.prompt.Name = "prompt";
            this.prompt.Size = new System.Drawing.Size(56, 28);
            this.prompt.TabIndex = 1;
            this.prompt.Text = "Prompt";
            this.prompt.UseVisualStyleBackColor = true;
            this.prompt.Click += new System.EventHandler(this.prompt_Click);
            // 
            // isel
            // 
            this.isel.Location = new System.Drawing.Point(16, 72);
            this.isel.Margin = new System.Windows.Forms.Padding(2);
            this.isel.Name = "isel";
            this.isel.Size = new System.Drawing.Size(56, 28);
            this.isel.TabIndex = 2;
            this.isel.Text = "Isel";
            this.isel.UseVisualStyleBackColor = true;
            this.isel.Click += new System.EventHandler(this.isel_Click);
            // 
            // textbox
            // 
            this.textbox.Location = new System.Drawing.Point(16, 36);
            this.textbox.Margin = new System.Windows.Forms.Padding(2);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(177, 20);
            this.textbox.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.saveEventsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(526, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(66, 20);
            this.toolStripMenuItem1.Text = "Recorder";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // saveEventsToolStripMenuItem
            // 
            this.saveEventsToolStripMenuItem.Name = "saveEventsToolStripMenuItem";
            this.saveEventsToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.saveEventsToolStripMenuItem.Text = "Save Events";
            this.saveEventsToolStripMenuItem.Click += new System.EventHandler(this.saveEventsToolStripMenuItem_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(242, 36);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(272, 212);
            this.listBox1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 257);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textbox);
            this.Controls.Add(this.isel);
            this.Controls.Add(this.prompt);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "WinForm Sessão 3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button prompt;
        private System.Windows.Forms.Button isel;
        private System.Windows.Forms.TextBox textbox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEventsToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
    }
}

