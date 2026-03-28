using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.WinForms.Common;
using TaskFlowManagement.Core.Constants;

namespace TaskFlowManagement.WinForms.Controls
{
    public sealed class StatusChangedEventArgs : EventArgs
    {
        public int TaskId { get; }
        public int NewStatusId { get; }

        public StatusChangedEventArgs(int taskId, int newStatusId)
        {
            TaskId = taskId;
            NewStatusId = newStatusId;
        }
    }

    public partial class ucTaskCard : UserControl
    {
        private int _taskId;
        private int _currentStatusId;
        public int TaskId => _taskId;

        public event EventHandler<StatusChangedEventArgs>? StatusChanged;
        public event EventHandler<int>? CardDoubleClicked;

        public ucTaskCard()
        {
            InitializeComponent();
            WireEvents();
            WireUpDoubleClick();

            this.MouseMove += UcTaskCard_MouseMove;
        }

        private bool _isDragging;
        private Point _dragStartPoint;

        private void UcTaskCard_MouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (_isDragging) return;

            // Chỉ bắt đầu drag khi di chuyển đủ xa (tránh click nhầm)
            var delta = new Point(
                Math.Abs(e.X - _dragStartPoint.X),
                Math.Abs(e.Y - _dragStartPoint.Y));

            if (delta.X < SystemInformation.DragSize.Width &&
                delta.Y < SystemInformation.DragSize.Height) return;

            _isDragging = true;
            DoDragDrop(_taskId, DragDropEffects.Move);
            _isDragging = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
                _dragStartPoint = e.Location;
        }

        public void Bind(TaskItem task)
        {
            ArgumentNullException.ThrowIfNull(task);

            _taskId = task.Id;
            _currentStatusId = task.StatusId;

            lblTitle.Text = string.IsNullOrWhiteSpace(task.Title)
                ? "(Không có tiêu đề)"
                : task.Title.Trim();

            string? assigneeText = task.AssignedTo?.FullName;
            if (string.IsNullOrWhiteSpace(assigneeText))
                assigneeText = task.AssignedToId.HasValue
                    ? $"User #{task.AssignedToId.Value}"
                    : "Chưa phân công";
            lblAssignee.Text = $"Assign: {assigneeText}";

            string priorityName = task.Priority?.Name
                ?? WorkflowConstants.GetPriorityName(task.PriorityId);
            lblPriority.Text = $"Priority: {priorityName}";
            lblPriority.ForeColor = GetPriorityColor(priorityName);

            lblExactStatus.Text = $"Status: {WorkflowConstants.GetStatusName(_currentStatusId)}"; // ← Chỉ giữ dòng này

            UpdateMenuItemState();
        }

        private void WireEvents()
        {
            miCreated.Click += (_, _) => RaiseStatusChanged(1);
            miAssigned.Click += (_, _) => RaiseStatusChanged(2);
            miInProgress.Click += (_, _) => RaiseStatusChanged(3);
            miFailed.Click += (_, _) => RaiseStatusChanged(4);
            miReview1.Click += (_, _) => RaiseStatusChanged(5);
            miReview2.Click += (_, _) => RaiseStatusChanged(6);
            miApproved.Click += (_, _) => RaiseStatusChanged(7);
            miInTest.Click += (_, _) => RaiseStatusChanged(8);
            miResolved.Click += (_, _) => RaiseStatusChanged(9);
            miClosed.Click += (_, _) => RaiseStatusChanged(10);
        }

        private void WireUpDoubleClick()
        {
            // Gắn sự kiện cho chính UserControl
            this.DoubleClick += InvokeDoubleClick;

            // Gắn sự kiện cho các thành phần con bên trong (như Label, Panel...)
            foreach (Control control in this.Controls)
            {
                control.DoubleClick += InvokeDoubleClick;
                // Lặp thêm 1 tầng nếu có Panel chứa Label bên trong
                foreach (Control child in control.Controls)
                {
                    child.DoubleClick += InvokeDoubleClick;
                }
            }
        }

        private void InvokeDoubleClick(object? sender, EventArgs e)
        {
            if (_taskId > 0)
            {
                CardDoubleClicked?.Invoke(this, _taskId);
            }
        }

        private void RaiseStatusChanged(int newStatusId)
        {
            if (_taskId <= 0 || newStatusId <= 0 || newStatusId == _currentStatusId)
            {
                return;
            }

            StatusChanged?.Invoke(this, new StatusChangedEventArgs(_taskId, newStatusId));
        }

        private void UpdateMenuItemState()
        {
            miCreated.Enabled = _currentStatusId != 1;
            miAssigned.Enabled = _currentStatusId != 2;
            miInProgress.Enabled = _currentStatusId != 3;
            miFailed.Enabled = _currentStatusId != 4;
            miReview1.Enabled = _currentStatusId != 5;
            miReview2.Enabled = _currentStatusId != 6;
            miApproved.Enabled = _currentStatusId != 7;
            miInTest.Enabled = _currentStatusId != 8;
            miResolved.Enabled = _currentStatusId != 9;
            miClosed.Enabled = _currentStatusId != 10;
        }

        private static Color GetPriorityColor(string priorityName) =>
            priorityName.Trim().ToUpperInvariant() switch
            {
                "CRITICAL" => UIHelper.ColorDanger,    // đỏ đậm
                "HIGH" => UIHelper.ColorDanger,
                "MEDIUM" => UIHelper.ColorWarning,
                "LOW" => UIHelper.ColorSuccess,
                _ => UIHelper.ColorMuted
            };

        public void UpdateBoundStatus(int newStatusId)
        {
            _currentStatusId = newStatusId;
            lblExactStatus.Text = $"Status: {WorkflowConstants.GetStatusName(newStatusId)}";
            UpdateMenuItemState();
        }
    }
}
