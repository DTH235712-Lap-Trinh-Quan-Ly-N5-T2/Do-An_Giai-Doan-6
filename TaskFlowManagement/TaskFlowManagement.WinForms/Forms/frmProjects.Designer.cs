// ============================================================
//  frmProjects.Designer.cs  (REFACTORED)
//  TaskFlowManagement.WinForms.Forms
//
//  THAY ĐỔI SO VỚI PHIÊN BẢN CŨ:
//  ─────────────────────────────────────────────────────────
//  • partial class kế thừa BaseForm (thay vì Form)
//  • SetToolButton() private helper → ĐÃ XÓA
//    Thay bằng UIHelper.StyleToolButton() — single source of truth
//  • Tất cả Color/Font hard-code → UIHelper.*
//  • DataGridView styling → UIHelper.StyleDataGridView()
//  • Status bar → UIHelper.CreateStatusBar()
// ============================================================
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjects  // BaseForm declared in frmProjects.cs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelTop        = new Panel();
            lblHeader       = new Label();
            panelFilter     = new Panel();
            txtSearch       = new TextBox();
            cboFilterStatus = new ComboBox();
            btnRefresh      = new Button();
            panelToolbar    = new Panel();
            btnAdd          = new Button();
            btnEdit         = new Button();
            btnDelete       = new Button();
            btnStatus       = new Button();
            btnMembers      = new Button();
            btnDetail       = new Button();
            btnKanban       = new Button();
            lblCount        = new Label();
            dgvProjects     = new DataGridView();
            panelStatus     = new Panel();
            lblStatus       = new Label();

            colId         = new DataGridViewTextBoxColumn();
            colName       = new DataGridViewTextBoxColumn();
            colCustomer   = new DataGridViewTextBoxColumn();
            colOwner      = new DataGridViewTextBoxColumn();
            colProjStatus = new DataGridViewTextBoxColumn();
            colMembers    = new DataGridViewTextBoxColumn();
            colDeadline   = new DataGridViewTextBoxColumn();
            colBudget     = new DataGridViewTextBoxColumn();
            colStartDate  = new DataGridViewTextBoxColumn();

            panelTop.SuspendLayout();
            panelFilter.SuspendLayout();
            panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProjects).BeginInit();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelTop — Header banner
            // ════════════════════════════════════════════════════
            panelTop.BackColor = UIHelper.ColorHeaderBg;
            panelTop.Dock      = DockStyle.Top;
            panelTop.Height    = 58;
            panelTop.Controls.Add(lblHeader);

            lblHeader.AutoSize  = false;
            lblHeader.Font      = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;
            lblHeader.Location  = new Point(20, 14);
            lblHeader.Size      = new Size(700, 30);
            lblHeader.Text      = "📁  Quản lý Dự án";

            // ════════════════════════════════════════════════════
            // panelFilter — Search + Status filter + Refresh
            // ════════════════════════════════════════════════════
            panelFilter.BackColor = UIHelper.ColorBackground;
            panelFilter.Dock      = DockStyle.Top;
            panelFilter.Height    = 46;
            panelFilter.Controls.AddRange(new Control[]
            { txtSearch, cboFilterStatus, btnRefresh });

            // txtSearch
            txtSearch.Location         = new Point(14, 10);
            txtSearch.Size             = new Size(220, 26);
            txtSearch.PlaceholderText  = "🔍  Tìm theo tên, khách hàng, PM...";
            txtSearch.Font             = UIHelper.FontSmall;
            txtSearch.TextChanged     += txtSearch_TextChanged;

            // cboFilterStatus
            cboFilterStatus.DropDownStyle    = ComboBoxStyle.DropDownList;
            cboFilterStatus.Font             = UIHelper.FontSmall;
            cboFilterStatus.Location         = new Point(244, 10);
            cboFilterStatus.Size             = new Size(160, 26);
            cboFilterStatus.Items.AddRange(new object[]
            {
                "— Tất cả trạng thái —",
                "NotStarted", "InProgress", "OnHold", "Completed", "Cancelled"
            });
            cboFilterStatus.SelectedIndex        = 0;
            cboFilterStatus.SelectedIndexChanged += cboFilterStatus_SelectedIndexChanged;

            // btnRefresh
            btnRefresh.Location = new Point(414, 10);
            btnRefresh.Size     = new Size(95, 26);
            UIHelper.StyleButton(btnRefresh, UIHelper.ButtonVariant.Secondary);
            btnRefresh.Text   = "🔄 Làm mới";
            btnRefresh.Click += btnRefresh_Click;

            // ════════════════════════════════════════════════════
            // panelToolbar — CRUD buttons
            // ════════════════════════════════════════════════════
            panelToolbar.BackColor = UIHelper.ColorSurface;
            panelToolbar.Dock      = DockStyle.Top;
            panelToolbar.Height    = 52;
            panelToolbar.Controls.AddRange(new Control[]
            { btnAdd, btnEdit, btnDelete, btnStatus, btnMembers, btnDetail, btnKanban, lblCount });

            //  ┌── Button layout constants ──────────────────────┐
            int bx = 14, by = 9, bg = 6, bh = 34;
            //  └────────────────────────────────────────────────┘

            UIHelper.StyleToolButton(btnAdd,     "➕ Thêm mới",   UIHelper.ButtonVariant.Primary,  bx, by, 110, bh); bx += 110 + bg;
            UIHelper.StyleToolButton(btnEdit,    "✏️  Sửa",        UIHelper.ButtonVariant.Success,  bx, by,  80, bh); bx +=  80 + bg;
            UIHelper.StyleToolButton(btnDelete,  "🗑️  Xóa",        UIHelper.ButtonVariant.Danger,   bx, by,  80, bh); bx +=  80 + bg;
            UIHelper.StyleToolButton(btnStatus,  "🔄 Trạng thái", UIHelper.ButtonVariant.Warning,  bx, by, 120, bh); bx += 120 + bg;
            UIHelper.StyleToolButton(btnMembers, "👥 Thành viên", UIHelper.ButtonVariant.Purple,   bx, by, 120, bh); bx += 120 + bg;
            UIHelper.StyleToolButton(btnDetail,  "📋 Chi tiết",   UIHelper.ButtonVariant.Slate,    bx, by, 100, bh); bx += 100 + bg;
            UIHelper.StyleToolButton(btnKanban,  "🗂 Kanban",     UIHelper.ButtonVariant.Info,     bx, by, 110, bh); bx += 110 + bg;

            // Disable buttons until a row is selected
            btnEdit.Enabled = btnDelete.Enabled = btnStatus.Enabled =
                btnMembers.Enabled = btnDetail.Enabled = btnKanban.Enabled = false;

            btnAdd.Click     += btnAdd_Click;
            btnEdit.Click    += btnEdit_Click;
            btnDelete.Click  += btnDelete_Click;
            btnStatus.Click  += btnStatus_Click;
            btnMembers.Click += btnMembers_Click;
            btnDetail.Click  += btnDetail_Click;
            btnKanban.Click  += btnKanban_Click;

            // lblCount
            lblCount.AutoSize  = false;
            lblCount.Font      = UIHelper.FontSmall;
            lblCount.ForeColor = UIHelper.ColorMuted;
            lblCount.Location  = new Point(bx, by);
            lblCount.Size      = new Size(140, bh);
            lblCount.TextAlign = ContentAlignment.MiddleLeft;

            // ════════════════════════════════════════════════════
            // dgvProjects — DataGridView
            // [REFACTOR] All DGV styling → UIHelper.StyleDataGridView()
            // ════════════════════════════════════════════════════
            UIHelper.StyleDataGridView(dgvProjects);
            UIHelper.ApplyAlternateRowColors(dgvProjects);
            dgvProjects.Dock = DockStyle.Fill;

            dgvProjects.SelectionChanged += dgvProjects_SelectionChanged;
            dgvProjects.CellDoubleClick  += dgvProjects_CellDoubleClick;

            // Columns
            colId.Name        = "colId";        colId.HeaderText        = "ID";          colId.Visible = false;
            colName.Name      = "colName";      colName.HeaderText      = "Tên dự án";   colName.Width = 200;
            colCustomer.Name  = "colCustomer";  colCustomer.HeaderText  = "Khách hàng";  colCustomer.Width = 140;
            colOwner.Name     = "colOwner";     colOwner.HeaderText     = "PM";          colOwner.Width = 120;
            colProjStatus.Name = "colProjStatus"; colProjStatus.HeaderText = "Trạng thái"; colProjStatus.Width = 145;
            colMembers.Name   = "colMembers";   colMembers.HeaderText   = "Thành viên";  colMembers.Width = 90;
            colDeadline.Name  = "colDeadline";  colDeadline.HeaderText  = "Deadline";    colDeadline.Width = 95;
            colBudget.Name    = "colBudget";    colBudget.HeaderText    = "Ngân sách";   colBudget.Width = 115;
            colStartDate.Name = "colStartDate"; colStartDate.HeaderText = "Bắt đầu";    colStartDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvProjects.Columns.AddRange(new DataGridViewColumn[]
            { colId, colName, colCustomer, colOwner, colProjStatus, colMembers, colDeadline, colBudget, colStartDate });

            // ════════════════════════════════════════════════════
            // Status Bar
            // [REFACTOR] → UIHelper.CreateStatusBar()
            // ════════════════════════════════════════════════════
            (panelStatus, lblStatus) = UIHelper.CreateStatusBar();

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            // NOTE: Font, BackColor, AutoScaleMode are set by BaseForm constructor.
            //       Only form-specific properties go here.
            this.ClientSize    = new Size(1000, 600);
            this.Name          = "frmProjects";
            this.Text          = "Quản lý Dự án";
            this.StartPosition = FormStartPosition.Manual;

            // Dock order: Fill must be added FIRST, Dock.Top panels added after
            this.Controls.Add(dgvProjects);    // Fill
            this.Controls.Add(panelStatus);    // Bottom
            this.Controls.Add(panelToolbar);   // Top (last Top added = topmost)
            this.Controls.Add(panelFilter);    // Top
            this.Controls.Add(panelTop);       // Top

            panelTop.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProjects).EndInit();
            this.ResumeLayout(false);
        }

        // ── Field declarations ───────────────────────────────────────────────
        private Panel   panelTop, panelFilter, panelToolbar, panelStatus;
        private Label   lblHeader, lblCount, lblStatus;
        private TextBox txtSearch;
        private ComboBox cboFilterStatus;
        private Button  btnRefresh, btnAdd, btnEdit, btnDelete, btnStatus, btnMembers, btnDetail, btnKanban;
        private DataGridView dgvProjects;
        private DataGridViewTextBoxColumn colId, colName, colCustomer, colOwner;
        private DataGridViewTextBoxColumn colProjStatus, colMembers, colDeadline, colBudget, colStartDate;
    }
}
