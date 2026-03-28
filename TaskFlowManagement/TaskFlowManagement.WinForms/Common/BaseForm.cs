// ============================================================
//  BaseForm.cs  —  TaskFlowManagement.WinForms.Common
//  Tất cả MDI-child Form kế thừa class này.
//  Mục đích:
//    1. Bật DoubleBuffered → chống nháy hình
//    2. Set Font + BackColor chung 1 lần duy nhất
//    3. WS_EX_COMPOSITED để tránh flicker trên MDI container
// ============================================================
using System.Drawing;
using System.Windows.Forms;

namespace TaskFlowManagement.WinForms.Common
{
    /// <summary>
    /// Base form cho toàn bộ TaskFlow WinForms application.
    /// Inherit class này thay vì <see cref="Form"/> trực tiếp.
    /// <para>
    /// Cách dùng trong Designer.cs:
    /// <code>partial class frmProjects : BaseForm { ... }</code>
    /// </para>
    /// </summary>
    public class BaseForm : Form
    {
        public BaseForm()
        {
            // Anti-flicker: bật double buffering ngay từ constructor
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint  |
                ControlStyles.UserPaint,
                true);
            this.UpdateStyles();

            // Đặt font & background mặc định nhất quán
            this.Font      = UIHelper.FontBase;
            this.BackColor = UIHelper.ColorSurface;

            // Chuẩn hóa DPI scaling
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = AutoScaleMode.Font;
        }

        /// <summary>
        /// WS_EX_COMPOSITED: giảm flickering cho MDI child forms
        /// khi có nhiều control Dock / Fill.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
