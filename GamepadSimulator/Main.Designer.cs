namespace GamepadSimulator
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RunSimulator = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectedDevice = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.启用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.禁用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(156, 196);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "版本:1.0.0(测试)";
            // 
            // RunSimulator
            // 
            this.RunSimulator.Location = new System.Drawing.Point(286, 185);
            this.RunSimulator.Name = "RunSimulator";
            this.RunSimulator.Size = new System.Drawing.Size(136, 33);
            this.RunSimulator.TabIndex = 7;
            this.RunSimulator.Text = "启用";
            this.RunSimulator.UseVisualStyleBackColor = true;
            this.RunSimulator.Click += new System.EventHandler(this.RunSimulator_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "连接设备：";
            // 
            // SelectedDevice
            // 
            this.SelectedDevice.AutoSize = true;
            this.SelectedDevice.Location = new System.Drawing.Point(283, 28);
            this.SelectedDevice.Name = "SelectedDevice";
            this.SelectedDevice.Size = new System.Drawing.Size(52, 15);
            this.SelectedDevice.TabIndex = 9;
            this.SelectedDevice.Text = "无连接";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "游戏手柄模拟器";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启用ToolStripMenuItem,
            this.禁用ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 52);
            // 
            // 启用ToolStripMenuItem
            // 
            this.启用ToolStripMenuItem.Name = "启用ToolStripMenuItem";
            this.启用ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.启用ToolStripMenuItem.Text = "启用";
            this.启用ToolStripMenuItem.Click += new System.EventHandler(this.启用ToolStripMenuItem_Click);
            // 
            // 禁用ToolStripMenuItem
            // 
            this.禁用ToolStripMenuItem.Enabled = false;
            this.禁用ToolStripMenuItem.Name = "禁用ToolStripMenuItem";
            this.禁用ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.禁用ToolStripMenuItem.Text = "禁用";
            this.禁用ToolStripMenuItem.Click += new System.EventHandler(this.禁用ToolStripMenuItem_Click);
            // 
            // RunLog
            // 
            this.RunLog.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.RunLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RunLog.Location = new System.Drawing.Point(177, 233);
            this.RunLog.Name = "RunLog";
            this.RunLog.ReadOnly = true;
            this.RunLog.Size = new System.Drawing.Size(287, 18);
            this.RunLog.TabIndex = 11;
            this.RunLog.TabStop = false;
            this.RunLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 267);
            this.Controls.Add(this.RunLog);
            this.Controls.Add(this.SelectedDevice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RunSimulator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "游戏手柄模拟器";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RunSimulator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SelectedDevice;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox RunLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 启用ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 禁用ToolStripMenuItem;
    }
}