namespace MultithreadingExamples
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
            this.btnAwaitWithOwnTask = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAwaitWithOwnTask
            // 
            this.btnAwaitWithOwnTask.Location = new System.Drawing.Point(31, 22);
            this.btnAwaitWithOwnTask.Name = "btnAwaitWithOwnTask";
            this.btnAwaitWithOwnTask.Size = new System.Drawing.Size(148, 23);
            this.btnAwaitWithOwnTask.TabIndex = 0;
            this.btnAwaitWithOwnTask.Text = "await with own Task";
            this.btnAwaitWithOwnTask.UseVisualStyleBackColor = true;
            this.btnAwaitWithOwnTask.Click += new System.EventHandler(this.btnAwaitWithOwnTask_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 262);
            this.Controls.Add(this.btnAwaitWithOwnTask);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAwaitWithOwnTask;
    }
}

