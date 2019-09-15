namespace Middleware1
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
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sentBox = new System.Windows.Forms.ListBox();
            this.recBox = new System.Windows.Forms.ListBox();
            this.readyBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(55, 25);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sent Messages";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(348, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Received Messages";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(611, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ready Messages";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // sentBox
            // 
            this.sentBox.FormattingEnabled = true;
            this.sentBox.Location = new System.Drawing.Point(43, 118);
            this.sentBox.Name = "sentBox";
            this.sentBox.Size = new System.Drawing.Size(203, 303);
            this.sentBox.TabIndex = 5;
            this.sentBox.SelectedIndexChanged += new System.EventHandler(this.sentBox_SelectedIndexChanged);
            // 
            // recBox
            // 
            this.recBox.FormattingEnabled = true;
            this.recBox.Location = new System.Drawing.Point(297, 118);
            this.recBox.Name = "recBox";
            this.recBox.Size = new System.Drawing.Size(203, 303);
            this.recBox.TabIndex = 6;
            this.recBox.SelectedIndexChanged += new System.EventHandler(this.recBox_SelectedIndexChanged);
            // 
            // readyBox
            // 
            this.readyBox.FormattingEnabled = true;
            this.readyBox.Location = new System.Drawing.Point(551, 118);
            this.readyBox.Name = "readyBox";
            this.readyBox.Size = new System.Drawing.Size(203, 303);
            this.readyBox.TabIndex = 7;
            this.readyBox.SelectedIndexChanged += new System.EventHandler(this.readyBox_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.readyBox);
            this.Controls.Add(this.recBox);
            this.Controls.Add(this.sentBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSend);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox sentBox;
        private System.Windows.Forms.ListBox recBox;
        private System.Windows.Forms.ListBox readyBox;
    }
}

