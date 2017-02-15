using System.ComponentModel;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private BackgroundWorker backgroundWorker1;

    public Form1()
    {
        InitializeComponent();
        //
        backgroundWorker1 = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };
        // 事件绑定
        backgroundWorker1.DoWork += backgroundWorker1_DoWork;
        backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
        backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
    }

    #region ---   按钮点击

    private void startAsyncButton_Click(System.Object sender,
        System.EventArgs e)
    {
        if (backgroundWorker1.IsBusy != true)
        {
            //如果在前面的线程还没有结束之前，再次调用“ backgroundWorker1 . RunWorkerAsync(args) ”，则会出现如下报错："此 BackgroundWorker 当前正忙，无法同时运行多个任务。"
            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync();
        }
    }
    private void cancelAsyncButton_Click(System.Object sender,
        System.EventArgs e)
    {
        if (backgroundWorker1.WorkerSupportsCancellation == true)
        {
            // Cancel the asynchronous operation.
            backgroundWorker1.CancelAsync();
            // 此方法只是请求取消后台的异步操作，而实际的取消后台线程的操作是通过DoWork事件中的Exit Sub来实现的。
        }
    }

    #endregion

    #region ---   BackgroundWorker 事件

    // This event handler is where the time-consuming work is done.
    private void backgroundWorker1_DoWork(System.Object sender,
        DoWorkEventArgs e)
    {
        BackgroundWorker worker = (BackgroundWorker)sender;
        int i = 0;

        for (i = 1; i <= 10; i++)
        {
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                // Cancel属性只是设置一个状态，用于在RunWorkerCompleted事件中判断是否是手动退出线程，真正的退出线程是用下面的Exit For来实现的。
                break;
            }
            else
            {
                // 注意这里异步关闭线程的思路：在线程外部请求取消本线程，然后在本线程内判断外部是否有取消线程的请求，如果有，则在线程内部通过Exit Sub（这里是Exit For，因为整个操作内容都在For代码块里面）将线程关闭。
                // Perform a time consuming operation and report progress.
                System.Threading.Thread.Sleep(500);
                worker.ReportProgress(i * 10, "Working...");
            }
        }
    }

    // This event handler updates the progress.
    private void backgroundWorker1_ProgressChanged(System.Object sender,
        ProgressChangedEventArgs e)
    {
        resultLabel.Text = e.ProgressPercentage.ToString() + "% " + e.UserState;
    }

    // This event handler deals with the results of the background operation.
    private void backgroundWorker1_RunWorkerCompleted(System.Object sender,
        RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled == true)
        {
            resultLabel.Text = "Canceled!";
        }
        else if (e.Error != null)
        {
            resultLabel.Text = "Error: " + e.Error.Message;
        }
        else
        {
            resultLabel.Text = "Done!";
        }
    }
    #endregion

    #region ---   InitializeComponent

    private System.Windows.Forms.Label resultLabel;
    private System.Windows.Forms.Button startAsync;
    private System.Windows.Forms.Button cancelAsync;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.resultLabel = new System.Windows.Forms.Label();
        this.startAsync = new System.Windows.Forms.Button();
        this.cancelAsync = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // resultLabel
        // 
        this.resultLabel.AutoSize = true;
        this.resultLabel.Location = new System.Drawing.Point(10, 9);
        this.resultLabel.Name = "resultLabel";
        this.resultLabel.Size = new System.Drawing.Size(41, 12);
        this.resultLabel.TabIndex = 0;
        this.resultLabel.Text = "label1";
        // 
        // startAsync
        // 
        this.startAsync.Location = new System.Drawing.Point(12, 41);
        this.startAsync.Name = "startAsync";
        this.startAsync.Size = new System.Drawing.Size(75, 23);
        this.startAsync.TabIndex = 1;
        this.startAsync.Text = "Start";
        this.startAsync.UseVisualStyleBackColor = true;
        this.startAsync.Click += new System.EventHandler(this.startAsyncButton_Click);
        // 
        // cancelAsync
        // 
        this.cancelAsync.Location = new System.Drawing.Point(93, 41);
        this.cancelAsync.Name = "cancelAsync";
        this.cancelAsync.Size = new System.Drawing.Size(75, 23);
        this.cancelAsync.TabIndex = 2;
        this.cancelAsync.Text = "Cancel";
        this.cancelAsync.UseVisualStyleBackColor = true;
        this.cancelAsync.Click += new System.EventHandler(this.cancelAsyncButton_Click);
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(220, 74);
        this.Controls.Add(this.cancelAsync);
        this.Controls.Add(this.startAsync);
        this.Controls.Add(this.resultLabel);
        this.Name = "Form1";
        this.Text = "Form1";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

}