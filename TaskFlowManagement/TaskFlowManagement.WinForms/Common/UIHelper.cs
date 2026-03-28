// ============================================================
//  UIHelper.cs  —  TaskFlowManagement.WinForms.Common
//  Tất cả màu sắc, font, style được định nghĩa TẠI ĐÂY.
//  Không được hard-code Color/Font trong bất kỳ Form nào nữa.
// ============================================================
using System.Drawing;
using System.Windows.Forms;

namespace TaskFlowManagement.WinForms.Common
{
    /// <summary>
    /// Centralized UI theme, palette, and helper methods.
    /// Design language: Flat / Modern Dark-Header, Tailwind-inspired palette.
    /// </summary>
    public static class UIHelper
    {
        // ── Color Palette ─────────────────────────────────────────────────────
        // Naming follows Tailwind CSS convention for easy cross-reference
        public static readonly Color ColorSurface       = Color.White;
        public static readonly Color ColorBackground    = Color.FromArgb(241, 245, 249); // slate-100
        public static readonly Color ColorHeaderBg      = Color.FromArgb(15, 23, 42);    // slate-900
        public static readonly Color ColorHeaderFg      = Color.White;
        public static readonly Color ColorSubtitle      = Color.FromArgb(148, 163, 184); // slate-400
        public static readonly Color ColorMuted         = Color.FromArgb(100, 116, 139); // slate-500
        public static readonly Color ColorBorderLight   = Color.FromArgb(226, 232, 240); // slate-200
        public static readonly Color ColorDark          = Color.FromArgb(30, 41, 59);    // slate-800

        // Button / Accent colors (bg / hover pairs)
        public static readonly Color ColorPrimary       = Color.FromArgb(37, 99, 235);   // blue-600
        public static readonly Color ColorPrimaryHover  = Color.FromArgb(29, 78, 216);   // blue-700
        public static readonly Color ColorSuccess       = Color.FromArgb(5, 150, 105);   // emerald-600
        public static readonly Color ColorSuccessHover  = Color.FromArgb(4, 120, 87);    // emerald-700
        public static readonly Color ColorDanger        = Color.FromArgb(220, 38, 38);   // red-600
        public static readonly Color ColorDangerHover   = Color.FromArgb(185, 28, 28);   // red-700
        public static readonly Color ColorWarning       = Color.FromArgb(245, 158, 11);  // amber-500
        public static readonly Color ColorWarningHover  = Color.FromArgb(217, 119, 6);   // amber-600
        public static readonly Color ColorInfo          = Color.FromArgb(14, 116, 144);  // cyan-700
        public static readonly Color ColorInfoHover     = Color.FromArgb(12, 94, 116);
        public static readonly Color ColorPurple        = Color.FromArgb(124, 58, 237);  // violet-600
        public static readonly Color ColorPurpleHover   = Color.FromArgb(109, 40, 217);  // violet-700
        public static readonly Color ColorSlate         = Color.FromArgb(71, 85, 105);   // slate-600

        // Semantic row colors for task / project status
        public static readonly Color ColorRowOverdue    = Color.FromArgb(185, 28, 28);
        public static readonly Color ColorRowCompleted  = Color.FromArgb(5, 150, 105);
        public static readonly Color ColorRowCancelled  = Color.FromArgb(148, 163, 184);
        public static readonly Color ColorRowInProgress = Color.FromArgb(37, 99, 235);
        public static readonly Color ColorRowReview     = Color.FromArgb(180, 83, 9);
        public static readonly Color ColorRowInTest     = Color.FromArgb(109, 40, 217);

        // ── Fonts ─────────────────────────────────────────────────────────────
        public static readonly Font FontBase        = new("Segoe UI", 9.5F);
        public static readonly Font FontSmall       = new("Segoe UI", 9F);
        public static readonly Font FontBold        = new("Segoe UI", 9F, FontStyle.Bold);
        public static readonly Font FontLabel       = new("Segoe UI", 8.5F, FontStyle.Bold);
        public static readonly Font FontHeaderLarge = new("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font FontGridHeader  = new("Segoe UI", 9F, FontStyle.Bold);

        // ── Button Variants ───────────────────────────────────────────────────
        public enum ButtonVariant
        {
            Primary,    // blue
            Success,    // emerald
            Danger,     // red
            Warning,    // amber
            Info,       // cyan
            Purple,     // violet
            Slate,      // slate-600 (neutral dark)
            Secondary   // light grey with border
        }

        /// <summary>
        /// Applies flat modern style to a button.
        /// Call this once per button — typically in Designer or in a Setup method.
        /// </summary>
        public static void StyleButton(Button btn, ButtonVariant variant = ButtonVariant.Primary)
        {
            var (bg, hover, fg) = variant switch
            {
                ButtonVariant.Success   => (ColorSuccess,   ColorSuccessHover,  Color.White),
                ButtonVariant.Danger    => (ColorDanger,    ColorDangerHover,   Color.White),
                ButtonVariant.Warning   => (ColorWarning,   ColorWarningHover,  Color.White),
                ButtonVariant.Info      => (ColorInfo,      ColorInfoHover,     Color.White),
                ButtonVariant.Purple    => (ColorPurple,    ColorPurpleHover,   Color.White),
                ButtonVariant.Slate     => (ColorSlate,     Color.FromArgb(51, 65, 85), Color.White),
                ButtonVariant.Secondary => (Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), ColorSlate),
                _                      => (ColorPrimary,   ColorPrimaryHover,  Color.White)
            };

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = hover;
            btn.BackColor = bg;
            btn.ForeColor = fg;
            btn.Font      = FontBold;
            btn.Cursor    = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        /// <summary>
        /// Shorthand: style + position + size a toolbar button in one call.
        /// Replaces the private SetToolButton() helpers scattered across Designer files.
        /// </summary>
        public static void StyleToolButton(
            Button btn, string text, ButtonVariant variant,
            int x, int y, int w, int h)
        {
            StyleButton(btn, variant);
            btn.Text     = text;
            btn.Location = new Point(x, y);
            btn.Size     = new Size(w, h);
        }

        // ── DataGridView ──────────────────────────────────────────────────────

        /// <summary>
        /// Applies the standard TaskFlow DataGridView style.
        /// Dark column headers, no row headers, full-row selection, single horizontal grid.
        /// </summary>
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns   = false;
            dgv.AllowUserToAddRows    = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.BackgroundColor       = ColorSurface;
            dgv.BorderStyle           = BorderStyle.None;
            dgv.CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Font                  = FontBase;
            dgv.GridColor             = ColorBackground;
            dgv.MultiSelect           = false;
            dgv.ReadOnly              = true;
            dgv.RowHeadersVisible     = false;
            dgv.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowTemplate.Height    = 36;
            dgv.DefaultCellStyle.Padding = new Padding(4, 0, 0, 0);

            // Dark header
            var hs = dgv.ColumnHeadersDefaultCellStyle;
            hs.BackColor = ColorHeaderBg;
            hs.ForeColor = ColorHeaderFg;
            hs.Font      = FontGridHeader;
            hs.Padding   = new Padding(4, 0, 0, 0);
        }

        /// <summary>
        /// Applies zebra-stripe to alternating rows.
        /// Call after StyleDataGridView if desired.
        /// </summary>
        public static void ApplyAlternateRowColors(DataGridView dgv)
        {
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }

        // ── Project Row Color ─────────────────────────────────────────────────

        /// <summary>
        /// Tô màu row dự án theo trạng thái.
        /// Tách ra khỏi BindGrid() để không bị lặp giữa nhiều form.
        /// </summary>
        public static void ApplyProjectRowStyle(
            DataGridViewRow row,
            string? status,
            DateOnly? plannedEndDate)
        {
            if (status == "Completed")
            {
                row.DefaultCellStyle.ForeColor = ColorSuccess;
            }
            else if (status == "Cancelled")
            {
                row.DefaultCellStyle.ForeColor = ColorRowCancelled;
                row.DefaultCellStyle.Font      = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            }
            else if (plannedEndDate.HasValue
                && plannedEndDate.Value < DateOnly.FromDateTime(DateTime.Now))
            {
                // Quá deadline mà vẫn chưa xong
                row.DefaultCellStyle.ForeColor = ColorDanger;
            }
            // else: default color, nothing to do
        }

        // ── Task Row Color ────────────────────────────────────────────────────

        /// <summary>
        /// Tô màu row task theo trạng thái.
        /// SINGLE SOURCE OF TRUTH — thay thế ApplyRowColor() đang bị duplicate
        /// ở cả frmTaskList.cs VÀ frmMyTasks.cs.
        /// </summary>
        public static void ApplyTaskRowStyle(
            DataGridViewRow row,
            string? statusName,
            bool isCompleted,
            DateTime? dueDate)
        {
            // Ưu tiên: quá hạn (kiểm tra trước)
            if (dueDate.HasValue && dueDate.Value < DateTime.UtcNow && !isCompleted)
            {
                row.DefaultCellStyle.ForeColor = ColorRowOverdue;
                return;
            }

            row.DefaultCellStyle.ForeColor = statusName switch
            {
                "CLOSED"                  => ColorRowCompleted,
                "RESOLVED"                => Color.FromArgb(22, 163, 74), // green-600
                "FAILED"                  => ColorDanger,
                "IN-PROGRESS"             => ColorRowInProgress,
                "REVIEW-1" or "REVIEW-2"  => ColorRowReview,
                "IN-TEST"                 => ColorRowInTest,
                _                         => ColorDark,
            };
        }

        // ── Project Status Display Text ───────────────────────────────────────

        /// <summary>
        /// Chuyển status code DB → chuỗi hiển thị có icon.
        /// Single source of truth — thay thế switch inline trong frmProjects.BindGrid().
        /// </summary>
        public static string FormatProjectStatus(string? status) => status switch
        {
            "NotStarted" => "📋 Chưa bắt đầu",
            "InProgress" => "🔄 Đang thực hiện",
            "OnHold"     => "⏸ Tạm dừng",
            "Completed"  => "✅ Hoàn thành",
            "Cancelled"  => "❌ Đã hủy",
            _            => status ?? "—"
        };

        // ── Panel Factory Methods ─────────────────────────────────────────────

        /// <summary>
        /// Tạo panel header đen với title + subtitle chuẩn.
        /// Dùng cho tất cả form dialog (frmProjectEdit, frmUserEdit, frmChangePassword…)
        /// </summary>
        public static Panel CreateHeaderPanel(string title, string subtitle)
        {
            var panel = new Panel
            {
                BackColor = ColorHeaderBg,
                Dock      = DockStyle.Top,
                Height    = 68
            };

            var lblTitle = new Label
            {
                AutoSize  = false,
                Font      = FontHeaderLarge,
                ForeColor = ColorHeaderFg,
                Location  = new Point(20, 10),
                Size      = new Size(800, 30),
                Text      = title
            };

            var lblSub = new Label
            {
                AutoSize  = false,
                Font      = FontSmall,
                ForeColor = ColorSubtitle,
                Location  = new Point(22, 44),
                Size      = new Size(800, 18),
                Text      = subtitle
            };

            panel.Controls.AddRange(new Control[] { lblTitle, lblSub });
            return panel;
        }

        /// <summary>
        /// Tạo panel thanh công cụ (toolbar) chuẩn.
        /// BackColor slate nhạt, Dock Top.
        /// </summary>
        public static Panel CreateToolbarPanel(int height = 52)
            => new Panel
            {
                BackColor = Color.FromArgb(248, 250, 252),
                Dock      = DockStyle.Top,
                Height    = height
            };

        /// <summary>
        /// Tạo status bar phía dưới với Label sẵn sàng ghi trạng thái.
        /// Returns (panel, label) để form gán lblStatus.
        /// </summary>
        public static (Panel Panel, Label Label) CreateStatusBar()
        {
            var panel = new Panel
            {
                BackColor = ColorHeaderBg,
                Dock      = DockStyle.Bottom,
                Height    = 28
            };
            var lbl = new Label
            {
                AutoSize  = false,
                Dock      = DockStyle.Fill,
                Font      = FontSmall,
                ForeColor = ColorSubtitle,
                Padding   = new Padding(12, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(lbl);
            return (panel, lbl);
        }

        // ── TextBox Search ────────────────────────────────────────────────────

        /// <summary>
        /// Style chuẩn cho ô tìm kiếm.
        /// </summary>
        public static void StyleSearchBox(TextBox txt, string placeholder = "🔍  Tìm kiếm...")
        {
            txt.Font            = new Font("Segoe UI", 10F);
            txt.PlaceholderText = placeholder;
            txt.BorderStyle     = BorderStyle.FixedSingle;
        }

        // ── ComboBox Filter ───────────────────────────────────────────────────

        /// <summary>
        /// Style chuẩn cho các ComboBox filter.
        /// </summary>
        public static void StyleFilterCombo(ComboBox cbo)
        {
            cbo.Font          = new Font("Segoe UI", 9.5F);
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.FlatStyle     = FlatStyle.Flat;
        }
    }
}
