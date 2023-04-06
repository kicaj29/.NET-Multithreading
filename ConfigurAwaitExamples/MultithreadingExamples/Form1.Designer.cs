namespace ConfigureAwait
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
            this.btnTaskDelay = new System.Windows.Forms.Button();
            this.btn_TaskWithReturn_NoDeadlock = new System.Windows.Forms.Button();
            this.btn_TaskWithReturn_Deadlock = new System.Windows.Forms.Button();
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception = new System.Windows.Forms.Button();
            this.btn_TaskWithReturnStub_NoDeadlock = new System.Windows.Forms.Button();
            this.btn_TaskWithReturnStub_Deadlock = new System.Windows.Forms.Button();
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception = new System.Windows.Forms.Button();
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception = new System.Windows.Forms.Button();
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAwaitWithOwnTask
            // 
            this.btnAwaitWithOwnTask.Location = new System.Drawing.Point(265, 288);
            this.btnAwaitWithOwnTask.Name = "btnAwaitWithOwnTask";
            this.btnAwaitWithOwnTask.Size = new System.Drawing.Size(159, 23);
            this.btnAwaitWithOwnTask.TabIndex = 0;
            this.btnAwaitWithOwnTask.Text = "await with own Task - void";
            this.btnAwaitWithOwnTask.UseVisualStyleBackColor = true;
            this.btnAwaitWithOwnTask.Click += new System.EventHandler(this.btnAwaitWithOwnTask_Click);
            // 
            // btnTaskDelay
            // 
            this.btnTaskDelay.Location = new System.Drawing.Point(12, 288);
            this.btnTaskDelay.Name = "btnTaskDelay";
            this.btnTaskDelay.Size = new System.Drawing.Size(148, 23);
            this.btnTaskDelay.TabIndex = 1;
            this.btnTaskDelay.Text = "Task.Delay";
            this.btnTaskDelay.UseVisualStyleBackColor = true;
            this.btnTaskDelay.Click += new System.EventHandler(this.btnTaskDelay_Click);
            // 
            // btn_TaskWithReturn_NoDeadlock
            // 
            this.btn_TaskWithReturn_NoDeadlock.Location = new System.Drawing.Point(31, 28);
            this.btn_TaskWithReturn_NoDeadlock.Name = "btn_TaskWithReturn_NoDeadlock";
            this.btn_TaskWithReturn_NoDeadlock.Size = new System.Drawing.Size(403, 23);
            this.btn_TaskWithReturn_NoDeadlock.TabIndex = 2;
            this.btn_TaskWithReturn_NoDeadlock.Text = "TaskWithReturn_NoDeadlock";
            this.btn_TaskWithReturn_NoDeadlock.UseVisualStyleBackColor = true;
            this.btn_TaskWithReturn_NoDeadlock.Click += new System.EventHandler(this.btn_TaskWithReturn_NoDeadlock_Click);
            // 
            // btn_TaskWithReturn_Deadlock
            // 
            this.btn_TaskWithReturn_Deadlock.Location = new System.Drawing.Point(31, 72);
            this.btn_TaskWithReturn_Deadlock.Name = "btn_TaskWithReturn_Deadlock";
            this.btn_TaskWithReturn_Deadlock.Size = new System.Drawing.Size(403, 23);
            this.btn_TaskWithReturn_Deadlock.TabIndex = 3;
            this.btn_TaskWithReturn_Deadlock.Text = "TaskWithReturn_Deadlock";
            this.btn_TaskWithReturn_Deadlock.UseVisualStyleBackColor = true;
            this.btn_TaskWithReturn_Deadlock.Click += new System.EventHandler(this.btn_TaskWithReturn_Deadlock_Click);
            // 
            // btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception
            // 
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Location = new System.Drawing.Point(31, 161);
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Name = "btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception";
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Size = new System.Drawing.Size(403, 23);
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception.TabIndex = 5;
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Text = "TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception";
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception.UseVisualStyleBackColor = true;
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Click += new System.EventHandler(this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception_Click);
            // 
            // btn_TaskWithReturnStub_NoDeadlock
            // 
            this.btn_TaskWithReturnStub_NoDeadlock.Location = new System.Drawing.Point(520, 28);
            this.btn_TaskWithReturnStub_NoDeadlock.Name = "btn_TaskWithReturnStub_NoDeadlock";
            this.btn_TaskWithReturnStub_NoDeadlock.Size = new System.Drawing.Size(432, 23);
            this.btn_TaskWithReturnStub_NoDeadlock.TabIndex = 6;
            this.btn_TaskWithReturnStub_NoDeadlock.Text = "TaskWithReturnStub_NoDeadlock";
            this.btn_TaskWithReturnStub_NoDeadlock.UseVisualStyleBackColor = true;
            this.btn_TaskWithReturnStub_NoDeadlock.Click += new System.EventHandler(this.btn_TaskWithReturnStub_NoDeadlock_Click);
            // 
            // btn_TaskWithReturnStub_Deadlock
            // 
            this.btn_TaskWithReturnStub_Deadlock.Location = new System.Drawing.Point(520, 72);
            this.btn_TaskWithReturnStub_Deadlock.Name = "btn_TaskWithReturnStub_Deadlock";
            this.btn_TaskWithReturnStub_Deadlock.Size = new System.Drawing.Size(432, 23);
            this.btn_TaskWithReturnStub_Deadlock.TabIndex = 7;
            this.btn_TaskWithReturnStub_Deadlock.Text = "TaskWithReturnStub_Deadlock";
            this.btn_TaskWithReturnStub_Deadlock.UseVisualStyleBackColor = true;
            this.btn_TaskWithReturnStub_Deadlock.Click += new System.EventHandler(this.btn_TaskWithReturnStub_Deadlock_Click);
            // 
            // btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception
            // 
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Location = new System.Drawing.Point(520, 161);
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Name = "btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception";
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Size = new System.Drawing.Size(432, 23);
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception.TabIndex = 9;
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Text = "TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception";
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception.UseVisualStyleBackColor = true;
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Click += new System.EventHandler(this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception_Click);
            // 
            // btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception
            // 
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Location = new System.Drawing.Point(31, 117);
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Name = "btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception";
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Size = new System.Drawing.Size(403, 23);
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.TabIndex = 10;
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Text = "TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception";
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.UseVisualStyleBackColor = true;
            this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Click += new System.EventHandler(this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception_Click);
            // 
            // btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception
            // 
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Location = new System.Drawing.Point(520, 117);
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Name = "btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception";
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Size = new System.Drawing.Size(432, 23);
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.TabIndex = 11;
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Text = "TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception";
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.UseVisualStyleBackColor = true;
            this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Click += new System.EventHandler(this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 370);
            this.Controls.Add(this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception);
            this.Controls.Add(this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception);
            this.Controls.Add(this.btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception);
            this.Controls.Add(this.btn_TaskWithReturnStub_Deadlock);
            this.Controls.Add(this.btn_TaskWithReturnStub_NoDeadlock);
            this.Controls.Add(this.btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception);
            this.Controls.Add(this.btn_TaskWithReturn_Deadlock);
            this.Controls.Add(this.btn_TaskWithReturn_NoDeadlock);
            this.Controls.Add(this.btnTaskDelay);
            this.Controls.Add(this.btnAwaitWithOwnTask);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAwaitWithOwnTask;
        private System.Windows.Forms.Button btnTaskDelay;
        private System.Windows.Forms.Button btn_TaskWithReturn_NoDeadlock;
        private System.Windows.Forms.Button btn_TaskWithReturn_Deadlock;
        private System.Windows.Forms.Button btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception;
        private System.Windows.Forms.Button btn_TaskWithReturnStub_NoDeadlock;
        private System.Windows.Forms.Button btn_TaskWithReturnStub_Deadlock;
        private System.Windows.Forms.Button btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception;
        private System.Windows.Forms.Button btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception;
        private System.Windows.Forms.Button btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception;
    }
}

