//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
namespace WindowsFormsApp1
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
            this.initializeOven = new System.Windows.Forms.Button();
            this.OvenClientOutput = new System.Windows.Forms.TextBox();
            this.bakeBread = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // initializeOven
            // 
            this.initializeOven.Location = new System.Drawing.Point(12, 12);
            this.initializeOven.Name = "initializeOven";
            this.initializeOven.Size = new System.Drawing.Size(121, 56);
            this.initializeOven.TabIndex = 0;
            this.initializeOven.Text = "Start Client2";
            this.initializeOven.UseVisualStyleBackColor = true;
            this.initializeOven.Click += new System.EventHandler(this.button1_Click);
            // 
            // OvenClientOutput
            // 
            this.OvenClientOutput.Location = new System.Drawing.Point(139, 12);
            this.OvenClientOutput.Multiline = true;
            this.OvenClientOutput.Name = "OvenClientOutput";
            this.OvenClientOutput.Size = new System.Drawing.Size(207, 164);
            this.OvenClientOutput.TabIndex = 1;
            // 
            // bakeBread
            // 
            this.bakeBread.Location = new System.Drawing.Point(12, 74);
            this.bakeBread.Name = "bakeBread";
            this.bakeBread.Size = new System.Drawing.Size(121, 56);
            this.bakeBread.TabIndex = 2;
            this.bakeBread.Text = "Bake Bread";
            this.bakeBread.UseVisualStyleBackColor = true;
            this.bakeBread.Click += new System.EventHandler(this.bakeBread_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 206);
            this.Controls.Add(this.bakeBread);
            this.Controls.Add(this.OvenClientOutput);
            this.Controls.Add(this.initializeOven);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button initializeOven;
        private System.Windows.Forms.TextBox OvenClientOutput;
        private System.Windows.Forms.Button bakeBread;
    }
}


