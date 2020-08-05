using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigureAwait
{
    public partial class Form1 : Form
    {
        private AsyncAwaitVoid v = new AsyncAwaitVoid();
        private ConfigureAwaitExample cae = new ConfigureAwaitExample();
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnAwaitWithOwnTask_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId);
            //v.Go();
            // v.Start().Wait();
            await v.WaitWithRun().ConfigureAwait(continueOnCapturedContext: false);
            // await v.WaitWithRun();
            //v.WaitWithRun().Wait();
            var result = await cae.DoCurlAsync();
            // var result = cae.DoCurlAsync().Result;

            //btnAwaitWithOwnTask.Text = "DONE";
            System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId);
        }

        private void btnTaskDelay_Click(object sender, EventArgs e)
        {
            new TaskDelay().Run();
        }

        private async void btn_TaskWithReturn_NoDeadlock_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId); // thread 1 (main thread)
            var result = await cae.DoCurlAsync();
            System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId); // thread 1 (main thread)
            btn_TaskWithReturn_NoDeadlock.Text = "DONE";
        }

        private void btn_TaskWithReturn_Deadlock_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId); // thread 1 (main thread)
            var result = cae.DoCurlAsync().Result;
            System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId); // never will be executed (deadlock)
            btn_TaskWithReturn_Deadlock.Text = "DONE";
        }

        private async void btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId);
            // thread 1 (main thread)

            var result = await cae.DoCurlAsync().ConfigureAwait(continueOnCapturedContext: false);          

            System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId);


            btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Text = "DONE";

        }
        private void btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId);
            // thread 1 (main thread)

            var result = cae.DoCurlAsyncDoNotForceToContinueOnCallingThread();

            System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId);
            // thread 1 (main thread)

            btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Text = "DONE";
        }


        #region stub
        private async void btn_TaskWithReturnStub_NoDeadlock_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId);
            var result = await cae.DoCurlStubAsync();
            btn_TaskWithReturnStub_NoDeadlock.Text = "DONE";
            System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId);
        }

        private void btn_TaskWithReturnStub_Deadlock_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId);
            var result = cae.DoCurlStubAsync().Result;
            btn_TaskWithReturnStub_Deadlock.Text = "DONE";
            System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId);
        }

        private async void btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId);
            var result = await cae.DoCurlStubAsync().ConfigureAwait(continueOnCapturedContext: false);
            System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId);
            btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Text = "DONE";
        }


        #endregion

        private void btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId);
            var result = cae.DoCurlStubAsyncDoNotForceToContinueOnCallingThread();
            System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId);
            btn_TaskWithReturnStub_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Text = "DONE";
        }
    }
}
