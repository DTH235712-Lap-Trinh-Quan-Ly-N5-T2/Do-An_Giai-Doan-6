// ============================================================
//  frmTaskList.Designer.cs  (REFACTORED)
//  TaskFlowManagement.WinForms.Forms
//
//  THAY ĐỔI SO VỚI PHIÊN BẢN CŨ:
//  ─────────────────────────────────────────────────────────
//  [Bug Fix - UI Inconsistency]
//   • panelTop KHÔNG có dark header → ĐÃ THÊM panelHeader riêng
//   • Tất cả button (btnAddNew, btnEdit, btnDelete, btnRefresh)
//     KHÔNG có FlatStyle/BackColor → ĐÃ ÁP DỤNG UIHelper.StyleButton()
//   • Status bar (panelBottom) KHÔNG có dark bg → ĐÃ SỬA
//
//  [Styling]
//   • DataGridView → UIHelper.StyleDataGridView()
//   • Status bar  → UIHelper.CreateStatusBar()
// ============================================================
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmTaskList  // BaseForm declared in frmTaskList.cs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Control instantiation ─────────────────────────────
            panelHeader     = new Panel();
            lblHeader       = new Label();
            panelTop        = new Panel();
            panelBottom     = new Panel();
            panelPaging     = new Panel();
            dgvTasks        = new DataGridView();
            txtSearch       = new TextBox();
            cboStatusFilter  = new ComboBox();
            cboProjectFilter = new ComboBox();
            btnAddNew       = new Button();
            btnEdit         = new Button();
            btnDelete       = new Button();
            btnRefresh      = new Button();
            lblCount        = new Label();
            lblStatus       = new Label();
            btnPrev         = new Button();
            lblPage         = new Label();
            btnNext         = new Button();

            colId       = new DataGridViewTextBoxColumn();
            colTitle    = new DataGridViewTextBoxColumn();
            colProject  = new DataGridViewTextBoxColumn();
            colAssignee = new DataGridViewTextBoxColumn();
            colPriority = new DataGridViewTextBoxColumn();
            colStatus   = new DataGridViewTextBoxColumn();
            colProgress = new DataGridViewTextBoxColumn();
            colDueDate  = new DataGridViewTextBoxColumn();

            panelHeader.SuspendLayout();
            panelTop.SuspendLayout();
            panelBottom.SuspendLayout();
            panelPaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelHeader — Dark banner (BUG FIX: thiếu ở version cũ)
            // ════════════════════════════════════════════════════
            panelHeader.BackColor = UIHelper.ColorHeaderBg;
            panelHeader.Dock      = DockStyle.Top;
            panelHeader.Height    = 58;
            panelHeader.Controls.Add(lblHeader);

            lblHeader.AutoSize  = false;
            lblHeader.Font      = UIHelper.FontHeaderLarge;
            lblHeader.ForeColor = UIHelper.ColorHeaderFg;
            lblHeader.Location  = new Point(20, 14);
            lblHeader.Size      = new Size(700, 30);
            lblHeader.Text      = "📋  Quản lý Công việc";

            // ════════════════════════════════════════════════════
            // panelTop — Toolbar: Filter + CRUD buttons
            // (BUG FIX: buttons không có styling ở version cũ)
            // ════════════════════════════════════════════════════
            panelTop.BackColor = UIHelper.ColorBackground;
            panelTop.Dock      = DockStyle.Top;
            panelTop.Height    = 52;
            panelTop.Controls.AddRange(new Control[]
            {
                txtSearch, cboProjectFilter, cboStatusFilter,
                btnAddNew, btnEdit, btnDelete, btnRefresh, lblCount
            });

            // txtSearch
            txtSearch.Location         = new Point(14, 13);
            txtSearch.Size             = new Size(200, 27);
            txtSearch.Font             = UIHelper.FontSmall;
            txtSearch.PlaceholderText  = "🔍  Tìm kiếm...";
            txtSearch.TextChanged     += txtSearch_TextChanged;

            // cboProjectFilter
            cboProjectFilter.DropDownStyle       = ComboBoxStyle.DropDownList;
            cboProjectFilter.Font                = UIHelper.FontSmall;
            cboProjectFilter.Location            = new Point(224, 13);
            cboProjectFilter.Size                = new Size(175, 27);
            cboProjectFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            // cboStatusFilter
            cboStatusFilter.DropDownStyle        = ComboBoxStyle.DropDownList;
            cboStatusFilter.Font                 = UIHelper.FontSmall;
            cboStatusFilter.Location             = new Point(409, 13);
            cboStatusFilter.Size                 = new Size(165, 27);
            cboStatusFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            //  ┌── Button layout (x positions) ─────────────────┐
            int bx = 584, by = 11, bh = 30, bg = 6;
            //  └────────────────────────────────────────────────┘

            // btnAddNew — Primary (BUG FIX: no style in old version)
            UIHelper.StyleToolButton(btnAddNew, "➕ Thêm mới", UIHelper.ButtonVariant.Primary, bx, by, 100, bh);
            btnAddNew.Click += btnAddNew_Click;
            bx += 100 + bg;

            // btnEdit — Success
            UIHelper.StyleToolButton(btnEdit, "✏️  Sửa", UIHelper.ButtonVariant.Success, bx, by, 80, bh);
            btnEdit.Enabled  = false;
            btnEdit.Click   += btnEdit_Click;
            bx += 80 + bg;

            // btnDelete — Danger
            UIHelper.StyleToolButton(btnDelete, "🗑️  Xóa", UIHelper.ButtonVariant.Danger, bx, by, 80, bh);
            btnDelete.Enabled  = false;
            btnDelete.Click   += btnDelete_Click;
            bx += 80 + bg;

            // btnRefresh — Secondary
            UIHelper.StyleToolButton(btnRefresh, "🔄 Làm mới", UIHelper.ButtonVariant.Secondary, bx, by, 90, bh);
            btnRefresh.Click += btnRefresh_Click;
            bx += 90 + bg;

            // lblCount
            lblCount.Location  = new Point(bx, by + 4);
            lblCount.Size      = new Size(130, 22);
            lblCount.Text      = "0 công việc";
            lblCount.Font      = UIHelper.FontSmall;
            lblCount.ForeColor = UIHelper.ColorMuted;
            lblCount.TextAlign = ContentAlignment.MiddleRight;

            // ════════════════════════════════════════════════════
            // dgvTasks — DataGridView
            // [REFACTOR] All DGV styling → UIHelper.StyleDataGridView()
            // ════════════════════════════════════════════════════
            UIHelper.StyleDataGridView(dgvTasks);
            UIHelper.ApplyAlternateRowColors(dgvTasks);
            dgvTasks.Dock           = DockStyle.Fill;
            dgvTasks.RowTemplate.Height = 30;   // Tasks dùng row ngắn hơn Projects

            dgvTasks.SelectionChanged += dgvTasks_SelectionChanged;
            dgvTasks.CellDoubleClick  += dgvTasks_CellDoubleClick;

            // Columns
            colId.Name       = "colId";       colId.HeaderText       = "ID";                 colId.Width = 50;      colId.Visible = false;
            colTitle.Name    = "colTitle";    colTitle.HeaderText    = "Tiêu đề công việc";  colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; colTitle.MinimumWidth = 200;
            colProject.Name  = "colProject";  colProject.HeaderText  = "Dự án";              colProject.Width = 150;
            colAssignee.Name = "colAssignee"; colAssignee.HeaderText = "Người thực hiện";    colAssignee.Width = 140;
            colPriority.Name = "colPriority"; colPriority.HeaderText = "Ưu tiên";            colPriority.Width = 85;
            colStatus.Name   = "colStatus";   colStatus.HeaderText   = "Trạng thái";         colStatus.Width = 110;
            colProgress.Name = "colProgress"; colProgress.HeaderText = "%";                  colProgress.Width = 55;
            colProgress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDueDate.Name  = "colDueDate";  colDueDate.HeaderText  = "Hạn chót";           colDueDate.Width = 95;

            dgvTasks.Columns.AddRange(new DataGridViewColumn[]
            { colId, colTitle, colProject, colAssignee, colPriority, colStatus, colProgress, colDueDate });

            // ════════════════════════════════════════════════════
            // panelBottom — Status bar + Paging
            // (BUG FIX: không có dark styling ở version cũ)
            // ════════════════════════════════════════════════════
            panelBottom.BackColor = UIHelper.ColorHeaderBg;
            panelBottom.Dock      = DockStyle.Bottom;
            panelBottom.Height    = 32;
            panelBottom.Controls.AddRange(new Control[] { lblStatus, panelPaging });

            lblStatus.AutoSize  = false;
            lblStatus.Location  = new Point(12, 7);
            lblStatus.Size      = new Size(550, 18);
            lblStatus.Text      = "Sẵn sàng";
            lblStatus.Font      = UIHelper.FontSmall;
            lblStatus.ForeColor = UIHelper.ColorSubtitle;

            // panelPaging
            panelPaging.BackColor = UIHelper.ColorHeaderBg;
            panelPaging.Dock      = DockStyle.Right;
            panelPaging.Width     = 225;
            panelPaging.Controls.AddRange(new Control[] { btnPrev, lblPage, btnNext });

            btnPrev.Location = new Point(5, 5);
            btnPrev.Size     = new Size(65, 24);
            UIHelper.StyleButton(btnPrev, UIHelper.ButtonVariant.Secondary);
            btnPrev.Text    = "◀ Trước";
            btnPrev.Font    = UIHelper.FontSmall;
            btnPrev.Enabled = false;
            btnPrev.Click  += btnPrev_Click;

            lblPage.Location  = new Point(75, 8);
            lblPage.Size      = new Size(90, 18);
            lblPage.Text      = "Trang 1 / 1";
            lblPage.Font      = UIHelper.FontSmall;
            lblPage.ForeColor = UIHelper.ColorSubtitle;
            lblPage.TextAlign = ContentAlignment.MiddleCenter;

            btnNext.Location = new Point(170, 5);
            btnNext.Size     = new Size(50, 24);
            UIHelper.StyleButton(btnNext, UIHelper.ButtonVariant.Secondary);
            btnNext.Text    = "Sau ▶";
            btnNext.Font    = UIHelper.FontSmall;
            btnNext.Enabled = false;
            btnNext.Click  += btnNext_Click;

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.Text          = "📋  Quản lý Công việc";
            this.Size          = new Size(1100, 650);
            this.MinimumSize   = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Dock order: Fill FIRST, then Top/Bottom panels
            this.Controls.Add(dgvTasks);     // Fill
            this.Controls.Add(panelBottom);  // Bottom
            this.Controls.Add(panelTop);     // Top (second)
            this.Controls.Add(panelHeader);  // Top (first/topmost)

            panelHeader.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            panelPaging.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            this.ResumeLayout(false);
        }

        // ── Field declarations ───────────────────────────────────────────────
        private Panel                      panelHeader;
        private Label                      lblHeader;
        private Panel                      panelTop;
        private Panel                      panelBottom;
        private Panel                      panelPaging;
        private DataGridView               dgvTasks;
        private TextBox                    txtSearch;
        private ComboBox                   cboStatusFilter;
        private ComboBox                   cboProjectFilter;
        private Button                     btnAddNew;
        private Button                     btnEdit;
        private Button                     btnDelete;
        private Button                     btnRefresh;
        private Label                      lblCount;
        private Label                      lblStatus;
        private Button                     btnPrev;
        private Label                      lblPage;
        private Button                     btnNext;
        private DataGridViewTextBoxColumn  colId;
        private DataGridViewTextBoxColumn  colTitle;
        private DataGridViewTextBoxColumn  colProject;
        private DataGridViewTextBoxColumn  colAssignee;
        private DataGridViewTextBoxColumn  colPriority;
        private DataGridViewTextBoxColumn  colStatus;
        private DataGridViewTextBoxColumn  colProgress;
        private DataGridViewTextBoxColumn  colDueDate;
    }
}
