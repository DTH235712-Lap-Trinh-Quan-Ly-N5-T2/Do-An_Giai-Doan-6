using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmCustomerEdit : BaseForm
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly Customer? _editCustomer;
        private readonly bool _isEdit;

        public frmCustomerEdit(ICustomerRepository customerRepo, Customer? editCustomer)
        {
            _customerRepo = customerRepo;
            _editCustomer = editCustomer;
            _isEdit       = editCustomer != null;
            InitializeComponent();
            LoadForm();
        }

        private void LoadForm()
        {
            if (_isEdit)
            {
                this.Text         = "Sửa thông tin khách hàng";
                lblTitleForm.Text = "✏️  Sửa thông tin";
                txtCompany.Text   = _editCustomer!.CompanyName;
                txtContact.Text   = _editCustomer.ContactName ?? "";
                txtEmail.Text     = _editCustomer.Email       ?? "";
                txtPhone.Text     = _editCustomer.Phone       ?? "";
                txtAddress.Text   = _editCustomer.Address     ?? "";
            }
            else
            {
                this.Text         = "Thêm khách hàng mới";
                lblTitleForm.Text = "➕  Thêm khách hàng mới";
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            // Validate
            if (string.IsNullOrWhiteSpace(txtCompany.Text))
            {
                lblError.Text = "⚠  Tên công ty không được để trống.";
                txtCompany.Focus();
                return;
            }

            // BUG FIX: Validate email nếu có nhập
            var emailInput = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(emailInput) && !emailInput.Contains('@'))
            {
                lblError.Text = "⚠  Email không hợp lệ.";
                txtEmail.Focus();
                return;
            }

            SetLoading(true);
            try
            {
                if (_isEdit)
                {
                    _editCustomer!.CompanyName = txtCompany.Text.Trim();
                    _editCustomer.ContactName  = NullIfEmpty(txtContact.Text);
                    _editCustomer.Email        = NullIfEmpty(emailInput);
                    _editCustomer.Phone        = NullIfEmpty(txtPhone.Text);
                    _editCustomer.Address      = NullIfEmpty(txtAddress.Text);
                    await _customerRepo.UpdateAsync(_editCustomer);
                }
                else
                {
                    var newCustomer = new Customer
                    {
                        CompanyName = txtCompany.Text.Trim(),
                        ContactName = NullIfEmpty(txtContact.Text),
                        Email       = NullIfEmpty(emailInput),
                        Phone       = NullIfEmpty(txtPhone.Text),
                        Address     = NullIfEmpty(txtAddress.Text),
                    };
                    await _customerRepo.AddAsync(newCustomer);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // BUG FIX: Bắt exception từ DB (ràng buộc unique, connection lỗi...)
                // Trước đây không có try/catch → crash không xử lý được
                lblError.Text = "⚠  Lỗi khi lưu: " + (ex.InnerException?.Message ?? ex.Message);
            }
            finally
            {
                // BUG FIX: Luôn reset loading kể cả khi lỗi
                if (!this.IsDisposed) SetLoading(false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetLoading(bool loading)
        {
            btnSave.Enabled = !loading;
            btnSave.Text    = loading ? "Đang lưu..." : "💾  Lưu";
        }

        // Helper: trả null nếu chuỗi rỗng/whitespace
        private static string? NullIfEmpty(string? s)
            => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    }
}
