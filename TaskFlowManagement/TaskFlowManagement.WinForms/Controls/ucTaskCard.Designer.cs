namespace TaskFlowManagement.WinForms.Controls
{
    partial class ucTaskCard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblExactStatus = new System.Windows.Forms.Label();
            this.lblAssignee = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmsTaskActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCreated = new System.Windows.Forms.ToolStripMenuItem();
            this.miAssigned = new System.Windows.Forms.ToolStripMenuItem();
            this.miInProgress = new System.Windows.Forms.ToolStripMenuItem();
            this.miFailed = new System.Windows.Forms.ToolStripMenuItem();
            this.miReview1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miReview2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miApproved = new System.Windows.Forms.ToolStripMenuItem();
            this.miInTest = new System.Windows.Forms.ToolStripMenuItem();
            this.miResolved = new System.Windows.Forms.ToolStripMenuItem();
            this.miClosed = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContainer.SuspendLayout();
            this.cmsTaskActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContainer.Controls.Add(this.lblPriority);
            this.pnlContainer.Controls.Add(this.lblExactStatus);
            this.pnlContainer.Controls.Add(this.lblAssignee);
            this.pnlContainer.Controls.Add(this.lblTitle);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlContainer.Size = new System.Drawing.Size(250, 110);
            this.pnlContainer.TabIndex = 0;
            // 
            // lblPriority
            // 
            this.lblPriority.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPriority.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPriority.Location = new System.Drawing.Point(10, 82);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(228, 18);
            this.lblPriority.TabIndex = 3;
            this.lblPriority.Text = "Priority: -";
            this.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExactStatus
            // 
            this.lblExactStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExactStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblExactStatus.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblExactStatus.Location = new System.Drawing.Point(10, 64);
            this.lblExactStatus.Name = "lblExactStatus";
            this.lblExactStatus.Size = new System.Drawing.Size(228, 18);
            this.lblExactStatus.TabIndex = 2;
            this.lblExactStatus.Text = "Status: -";
            this.lblExactStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAssignee
            // 
            this.lblAssignee.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAssignee.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAssignee.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.lblAssignee.Location = new System.Drawing.Point(10, 42);
            this.lblAssignee.Name = "lblAssignee";
            this.lblAssignee.Size = new System.Drawing.Size(228, 22);
            this.lblAssignee.TabIndex = 1;
            this.lblAssignee.Text = "Assign: -";
            this.lblAssignee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Location = new System.Drawing.Point(10, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(228, 34);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Task Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // cmsTaskActions
            // 
            this.cmsTaskActions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsTaskActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCreated,
            this.miAssigned,
            this.miInProgress,
            this.miFailed,
            this.miReview1,
            this.miReview2,
            this.miApproved,
            this.miInTest,
            this.miResolved,
            this.miClosed});
            this.cmsTaskActions.Name = "cmsTaskActions";
            this.cmsTaskActions.Size = new System.Drawing.Size(251, 264);
            // 
            // miCreated
            // 
            this.miCreated.Name = "miCreated";
            this.miCreated.Size = new System.Drawing.Size(250, 26);
            this.miCreated.Text = "Chuyển sang Created";
            // 
            // miAssigned
            // 
            this.miAssigned.Name = "miAssigned";
            this.miAssigned.Size = new System.Drawing.Size(250, 26);
            this.miAssigned.Text = "Chuyển sang Assigned";
            // 
            // miInProgress
            // 
            this.miInProgress.Name = "miInProgress";
            this.miInProgress.Size = new System.Drawing.Size(250, 26);
            this.miInProgress.Text = "Chuyển sang In-Progress";
            // 
            // miFailed
            // 
            this.miFailed.Name = "miFailed";
            this.miFailed.Size = new System.Drawing.Size(250, 26);
            this.miFailed.Text = "Chuyển sang Failed";
            // 
            // miReview1
            // 
            this.miReview1.Name = "miReview1";
            this.miReview1.Size = new System.Drawing.Size(250, 26);
            this.miReview1.Text = "Chuyển sang Review-1";
            // 
            // miReview2
            // 
            this.miReview2.Name = "miReview2";
            this.miReview2.Size = new System.Drawing.Size(250, 26);
            this.miReview2.Text = "Chuyển sang Review-2";
            // 
            // miApproved
            // 
            this.miApproved.Name = "miApproved";
            this.miApproved.Size = new System.Drawing.Size(250, 26);
            this.miApproved.Text = "Chuyển sang Approved";
            // 
            // miInTest
            // 
            this.miInTest.Name = "miInTest";
            this.miInTest.Size = new System.Drawing.Size(250, 26);
            this.miInTest.Text = "Chuyển sang In-Test";
            // 
            // miResolved
            // 
            this.miResolved.Name = "miResolved";
            this.miResolved.Size = new System.Drawing.Size(250, 26);
            this.miResolved.Text = "Chuyển sang Resolved";
            // 
            // miClosed
            // 
            this.miClosed.Name = "miClosed";
            this.miClosed.Size = new System.Drawing.Size(250, 26);
            this.miClosed.Text = "Chuyển sang Closed";
            // 
            // ucTaskCard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.cmsTaskActions;
            this.Controls.Add(this.pnlContainer);
            this.Name = "ucTaskCard";
            this.Size = new System.Drawing.Size(250, 110);
            this.pnlContainer.ResumeLayout(false);
            this.cmsTaskActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAssignee;
        private System.Windows.Forms.Label lblExactStatus;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.ContextMenuStrip cmsTaskActions;
        private System.Windows.Forms.ToolStripMenuItem miCreated;
        private System.Windows.Forms.ToolStripMenuItem miAssigned;
        private System.Windows.Forms.ToolStripMenuItem miInProgress;
        private System.Windows.Forms.ToolStripMenuItem miFailed;
        private System.Windows.Forms.ToolStripMenuItem miReview1;
        private System.Windows.Forms.ToolStripMenuItem miReview2;
        private System.Windows.Forms.ToolStripMenuItem miApproved;
        private System.Windows.Forms.ToolStripMenuItem miInTest;
        private System.Windows.Forms.ToolStripMenuItem miResolved;
        private System.Windows.Forms.ToolStripMenuItem miClosed;
    }
}
