// ============================================================
//  ComboItem.cs  —  TaskFlowManagement.WinForms.Common
//  Trước đây bị định nghĩa TRONG frmTaskList.cs → không dùng được ở form khác.
//  Chuyển ra đây để tất cả form đều dùng chung.
// ============================================================
namespace TaskFlowManagement.WinForms.Common
{
    /// <summary>
    /// Generic wrapper dùng cho mọi ComboBox cần lưu (Id, Label).
    /// ToString() trả về Label → ComboBox hiển thị đúng tự động.
    /// </summary>
    public record ComboItem(int Id, string Label)
    {
        public override string ToString() => Label;
    }
}
