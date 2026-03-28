using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmCustomers : BaseForm
    {
        private readonly ICustomerRepository _customerRepo;
        private List<Customer> _allCustomers = new();
        private List<Customer> _displayedCustomers = new();
        private Customer? _selectedCustomer;
        private readonly System.Windows.Forms.Timer _searchTimer;

        public frmCustomers(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
            InitializeComponent();

            _searchTimer = new System.Windows.Forms.Timer { Interval = 350 };
            _searchTimer.Tick += async (s, e) =>
            {
                _searchTimer.Stop();
                await SearchAsync();
            };
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadAllAsync();
        }

        private async Task LoadAllAsync()
        {
            SetStatus("⏳  Đang tải...");
            _allCustomers = await _customerRepo.GetAllAsync();
            BindGrid(_allCustomers);
            SetStatus($"Tổng: {_allCustomers.Count} khách hàng");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private async Task SearchAsync()
        {
            var kw = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(kw)) { BindGrid(_allCustomers); return; }
            var results = await _customerRepo.SearchAsync(kw);
            BindGrid(results);
            SetStatus($"Tìm thấy: {results.Count} kết quả");
        }

        private void BindGrid(List<Customer> list)
        {
            _displayedCustomers = list;
            _selectedCustomer = null;
            dgvCustomers.Rows.Clear();
            foreach (var c in list)
            {
                dgvCustomers.Rows.Add(
                    c.Id, c.CompanyName,
                    c.ContactName ?? "", c.Email ?? "",
                    c.Phone ?? "", c.Address ?? "",
                    c.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy"));
            }
            lblCount.Text = $"{list.Count} khách hàng";
            UpdateButtons();
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            { _selectedCustomer = null; UpdateButtons(); return; }
            int id = (int)dgvCustomers.SelectedRows[0].Cells["colCustId"].Value;
            _selectedCustomer = _displayedCustomers.FirstOrDefault(c => c.Id == id);
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool sel = _selectedCustomer != null;
            btnEdit.Enabled = sel;
            btnDelete.Enabled = sel;
            btnDetail.Enabled = sel;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new frmCustomerEdit(_customerRepo, null);
            if (dlg.ShowDialog(this) == DialogResult.OK) _ = LoadAllAsync();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null) return;
            using var dlg = new frmCustomerEdit(_customerRepo, _selectedCustomer);
            if (dlg.ShowDialog(this) == DialogResult.OK) _ = LoadAllAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null) return;
            var confirm = MessageBox.Show(
                $"Xóa khách hàng \"{_selectedCustomer.CompanyName}\"?\n\n" +
                "⚠️  Lưu ý: Chỉ xóa được nếu khách hàng chưa có dự án nào.",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _customerRepo.DeleteAsync(_selectedCustomer.Id);
                txtSearch.Clear();
                await LoadAllAsync();
                MessageBox.Show("Xóa khách hàng thành công.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa vì khách hàng còn dự án liên quan.\n\n" +
                    "Chi tiết: " + (ex.InnerException?.Message ?? ex.Message),
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDetail_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null) return;
            var customer = await _customerRepo.GetWithProjectsAsync(_selectedCustomer.Id);
            if (customer == null) { MessageBox.Show("Không tìm thấy khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            var count = customer.Projects?.Count ?? 0;
            if (count == 0)
            { MessageBox.Show($"\"{customer.CompanyName}\" chưa có dự án nào.", "Chi tiết khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            var lines = customer.Projects!
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => $"  📁  {p.Name}  —  {p.Status}  (PM: {p.Owner?.FullName ?? "?"})");

            MessageBox.Show(
                $"Khách hàng: {customer.CompanyName}\nTổng dự án: {count}\n\n" + string.Join("\n", lines),
                "Danh sách dự án", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { if (e.RowIndex >= 0) btnEdit_Click(sender, e); }

        private async void btnRefresh_Click(object sender, EventArgs e)
        { txtSearch.Clear(); await LoadAllAsync(); }

        protected override void OnFormClosed(FormClosedEventArgs e)
        { _searchTimer.Stop(); _searchTimer.Dispose(); base.OnFormClosed(e); }

        private void SetStatus(string msg) => lblStatus.Text = msg;
    }
}