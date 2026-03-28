namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmDashboard
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

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.cboProject = new System.Windows.Forms.ComboBox();
            this.lblProjectFilter = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tabControlDashboard = new System.Windows.Forms.TabControl();
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.pnlCharts = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPieChart = new System.Windows.Forms.Panel();
            this.pnlCards = new System.Windows.Forms.FlowLayoutPanel();
            this.tabProgress = new System.Windows.Forms.TabPage();
            this.pnlProgressChart = new System.Windows.Forms.Panel();
            this.tabBudget = new System.Windows.Forms.TabPage();
            this.pnlBudgetChart = new System.Windows.Forms.Panel();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tabControlDashboard.SuspendLayout();
            this.tabOverview.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            this.tabProgress.SuspendLayout();
            this.tabBudget.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 68);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.lblProjectFilter);
            this.pnlToolbar.Controls.Add(this.cboProject);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 68);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1200, 52);
            this.pnlToolbar.TabIndex = 1;
            // 
            // lblProjectFilter
            // 
            this.lblProjectFilter.AutoSize = true;
            this.lblProjectFilter.Location = new System.Drawing.Point(20, 18);
            this.lblProjectFilter.Name = "lblProjectFilter";
            this.lblProjectFilter.Size = new System.Drawing.Size(60, 15);
            this.lblProjectFilter.TabIndex = 1;
            this.lblProjectFilter.Text = "Lọc dự án:";
            // 
            // cboProject
            // 
            this.cboProject.FormattingEnabled = true;
            this.cboProject.Location = new System.Drawing.Point(90, 14);
            this.cboProject.Name = "cboProject";
            this.cboProject.Size = new System.Drawing.Size(300, 23);
            this.cboProject.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.tabControlDashboard);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 120);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(1200, 680);
            this.pnlContent.TabIndex = 2;
            // 
            // tabControlDashboard
            // 
            this.tabControlDashboard.Controls.Add(this.tabOverview);
            this.tabControlDashboard.Controls.Add(this.tabProgress);
            this.tabControlDashboard.Controls.Add(this.tabBudget);
            this.tabControlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlDashboard.Location = new System.Drawing.Point(20, 20);
            this.tabControlDashboard.Name = "tabControlDashboard";
            this.tabControlDashboard.SelectedIndex = 0;
            this.tabControlDashboard.Size = new System.Drawing.Size(1160, 640);
            this.tabControlDashboard.TabIndex = 0;
            // 
            // tabOverview
            // 
            this.tabOverview.Controls.Add(this.pnlCharts);
            this.tabOverview.Controls.Add(this.pnlCards);
            this.tabOverview.Location = new System.Drawing.Point(4, 26);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Padding = new System.Windows.Forms.Padding(20);
            this.tabOverview.Size = new System.Drawing.Size(1152, 610);
            this.tabOverview.TabIndex = 0;
            this.tabOverview.Text = "Tổng Quan";
            this.tabOverview.UseVisualStyleBackColor = true;
            // 
            // pnlCharts
            // 
            this.pnlCharts.ColumnCount = 1;
            this.pnlCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCharts.Controls.Add(this.pnlPieChart, 0, 0);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(20, 140);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.RowCount = 1;
            this.pnlCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCharts.Size = new System.Drawing.Size(1112, 450);
            this.pnlCharts.TabIndex = 1;
            // 
            // pnlPieChart
            // 
            this.pnlPieChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPieChart.Location = new System.Drawing.Point(0, 20);
            this.pnlPieChart.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.pnlPieChart.Name = "pnlPieChart";
            this.pnlPieChart.Size = new System.Drawing.Size(1112, 430);
            this.pnlPieChart.TabIndex = 0;
            this.pnlPieChart.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlPieChart_Paint);
            // 
            // pnlCards
            // 
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Location = new System.Drawing.Point(20, 20);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(1112, 120);
            this.pnlCards.TabIndex = 0;
            this.pnlCards.WrapContents = false;
            // 
            // tabProgress
            // 
            this.tabProgress.Controls.Add(this.pnlProgressChart);
            this.tabProgress.Location = new System.Drawing.Point(4, 26);
            this.tabProgress.Name = "tabProgress";
            this.tabProgress.Padding = new System.Windows.Forms.Padding(20);
            this.tabProgress.Size = new System.Drawing.Size(1152, 610);
            this.tabProgress.TabIndex = 1;
            this.tabProgress.Text = "Báo cáo tiến độ";
            this.tabProgress.UseVisualStyleBackColor = true;
            // 
            // pnlProgressChart
            // 
            this.pnlProgressChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProgressChart.Location = new System.Drawing.Point(20, 20);
            this.pnlProgressChart.Name = "pnlProgressChart";
            this.pnlProgressChart.Size = new System.Drawing.Size(1112, 570);
            this.pnlProgressChart.TabIndex = 0;
            this.pnlProgressChart.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlProgressChart_Paint);
            // 
            // tabBudget
            // 
            this.tabBudget.Controls.Add(this.pnlBudgetChart);
            this.tabBudget.Location = new System.Drawing.Point(4, 26);
            this.tabBudget.Name = "tabBudget";
            this.tabBudget.Padding = new System.Windows.Forms.Padding(20);
            this.tabBudget.Size = new System.Drawing.Size(1152, 610);
            this.tabBudget.TabIndex = 2;
            this.tabBudget.Text = "Ngân sách & Chi phí";
            this.tabBudget.UseVisualStyleBackColor = true;
            // 
            // pnlBudgetChart
            // 
            this.pnlBudgetChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBudgetChart.Location = new System.Drawing.Point(20, 20);
            this.pnlBudgetChart.Name = "pnlBudgetChart";
            this.pnlBudgetChart.Size = new System.Drawing.Size(1112, 570);
            this.pnlBudgetChart.TabIndex = 0;
            this.pnlBudgetChart.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlBudgetChart_Paint);
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmDashboard";
            this.Text = "Dashboard Thống Kê";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.tabControlDashboard.ResumeLayout(false);
            this.tabOverview.ResumeLayout(false);
            this.pnlCharts.ResumeLayout(false);
            this.tabProgress.ResumeLayout(false);
            this.tabBudget.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.ComboBox cboProject;
        private System.Windows.Forms.Label lblProjectFilter;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TabControl tabControlDashboard;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.TabPage tabProgress;
        private System.Windows.Forms.TabPage tabBudget;
        private System.Windows.Forms.FlowLayoutPanel pnlCards;
        private System.Windows.Forms.TableLayoutPanel pnlCharts;
        private System.Windows.Forms.Panel pnlPieChart;
        private System.Windows.Forms.Panel pnlProgressChart;
        private System.Windows.Forms.Panel pnlBudgetChart;
    }
}
