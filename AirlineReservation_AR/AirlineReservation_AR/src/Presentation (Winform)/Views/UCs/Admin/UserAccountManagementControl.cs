using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.UCs.Admin
{
    public partial class UserAccountManagementControl : UserControl
    {
        private List<User> users = new List<User>();
        private List<User> filteredUsers = new List<User>();
        private List<User> pagedUsers = new List<User>();
        private readonly UserContrller _userController = DIContainer.UserContrller;
        private const int AVATAR_SIZE = 40;
        private const int BADGE_HEIGHT = 28;
        private const int BUTTON_SIZE = 32;

        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalPages = 0;

        public UserAccountManagementControl()
        {
            InitializeComponent();
            InitializeDataGridView();
            this.Load += UserAccountManagementControl_Load;
        }

        private async void UserAccountManagementControl_Load(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                dgvUsers.Enabled = false;

                await LoadRolesAsync();

                var allUsers = await _userController.GetAllUsersAsync();
                users = allUsers.ToList();

                InitializeFilters();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvUsers.Enabled = true;
            }
        }

        private async Task LoadRolesAsync() 
        {
             try {
                 var roles = await DIContainer.LookupService.GetRolesAsync();
                 cboRole.Items.Clear();
                 cboRole.Items.Add(new RoleSelectDto { RoleId = 0, RoleName = "All", DisplayName = "All" });
                 foreach(var r in roles) cboRole.Items.Add(r);
                 cboRole.SelectedIndex = 0;
             } catch { /* Ignore */ }
        }

        private void InitializeFilters()
        {
            // Status
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new object[] { "All", "Active", "Locked" });
            cboStatus.SelectedIndex = 0;

            // Verified
            cboXacThuc.Items.Clear();
            cboXacThuc.Items.AddRange(new object[] { "All", "Verified", "Unverified" });
            cboXacThuc.SelectedIndex = 0;

            // Sort
            cboSort.Items.Clear();
            cboSort.Items.AddRange(new object[] { "Date Created (Newest)", "Date Created (Oldest)", "Name (A-Z)", "Name (Z-A)" });
            cboSort.SelectedIndex = 0;

            // Clear old events
            cboRole.SelectedIndexChanged -= (s, e) => ApplyFilters();
            cboRole.SelectedIndexChanged += (s, e) => ApplyFilters();
            
            cboStatus.SelectedIndexChanged -= (s, e) => ApplyFilters();
            cboStatus.SelectedIndexChanged += (s, e) => ApplyFilters();
            
            cboXacThuc.SelectedIndexChanged -= (s, e) => ApplyFilters();
            cboXacThuc.SelectedIndexChanged += (s, e) => ApplyFilters();
            
            cboSort.SelectedIndexChanged -= (s, e) => ApplyFilters();
            cboSort.SelectedIndexChanged += (s, e) => ApplyFilters();
            
            txtFindUsers.TextChanged -= (s, e) => ApplyFilters();
            txtFindUsers.TextChanged += (s, e) => ApplyFilters();

            // Pagination
            paginationControl.PageChanged += (s, page) => {
                _currentPage = page;
                LoadPage();
            };

            // Action Buttons
            btnActionView.Click += (s, e) => ExecuteView();
            btnActionEdit.Click += (s, e) => ExecuteEdit();
            btnActionDisable.Click += (s, e) => ExecuteDisable();

            btnActionRole.Click += (s, e) => ExecuteRole();
            btnActionRefresh.Click += (s, e) => { LoadUsersAsync(); };
        }

        private void InitializeDataGridView()
        {
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersHeight = 50;
            dgvUsers.RowTemplate.Height = 80;
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUsers.DefaultCellStyle.ForeColor = Color.Black;
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvUsers.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            dgvUsers.CellPainting += DgvUsers_CellPainting;
            dgvUsers.CellClick += DgvUsers_CellClick;
            dgvUsers.CellMouseMove += DgvUsers_CellMouseMove;
        }

        private void DgvUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= pagedUsers.Count) return;

            var user = pagedUsers[e.RowIndex];
            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground);

            switch (dgvUsers.Columns[e.ColumnIndex].Name)
            {
                case "colID":
                    DrawID(e, user);
                    break;
                case "colUser":
                    DrawUserInfo(e, user);
                    break;
                case "colPhone":
                    DrawCenteredText(e, user.Phone ?? "N/A");
                    break;
                case "colRole":
                    DrawRoleBadge(e, user);
                    break;
                case "colStatus":
                    DrawStatusBadge(e, user);
                    break;
                case "colVerified":
                    DrawVerifiedBadge(e, user);
                    break;
                case "colLastLogin":
                    DrawCenteredText(e, "N/A"); // Placeholder for Last Login
                    break;
                case "colCreatedDate":
                    DrawCreateDate(e, user);
                    break;

            }
            e.Handled = true;
        }

        // === CÁC HÀM VẼ GIỐNG BẠN ĐÃ LÀM, MÌNH CHỈ SỬA NHỎ CHO HOẠT ĐỘNG ===
        private void DrawID(DataGridViewCellPaintingEventArgs e, User user)
        {
            string idText = user.UserId.ToString("N").Substring(0, 8).ToUpper();
            using var brush = new SolidBrush(Color.FromArgb(80, 80, 80));
            using var font = new Font("Consolas", 10F, FontStyle.Bold);
            e.Graphics.DrawString(idText, font, brush, e.CellBounds, GetCenterFormat());
        }

        private void DrawUserInfo(DataGridViewCellPaintingEventArgs e, User user)
        {
            int avatarX = e.CellBounds.X + 15;
            int avatarY = e.CellBounds.Y + (e.CellBounds.Height - AVATAR_SIZE) / 2;

            string role = user.UserRoles.FirstOrDefault()?.Role?.RoleName ?? "Khách hàng";
            using (var avatarBrush = new SolidBrush(GetAvatarColor(role)))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(avatarBrush, avatarX, avatarY, AVATAR_SIZE, AVATAR_SIZE);

                using var font = new Font("Segoe UI", 16F, FontStyle.Bold);
                using var textBrush = new SolidBrush(Color.White);
                var rect = new RectangleF(avatarX, avatarY, AVATAR_SIZE, AVATAR_SIZE);
                e.Graphics.DrawString(GetInitials(user.FullName), font, textBrush, rect, GetCenterFormat());
            }

            int textX = avatarX + AVATAR_SIZE + 15;
            int nameY = e.CellBounds.Y + 20;
            int emailY = nameY + 25;

            using var nameFont = new Font("Segoe UI", 11F, FontStyle.Bold);
            using var emailFont = new Font("Segoe UI", 9F);
            using var nameBrush = new SolidBrush(Color.Black);
            using var emailBrush = new SolidBrush(Color.Gray);

            e.Graphics.DrawString(user.FullName, nameFont, nameBrush, textX, nameY);
            e.Graphics.DrawString(user.Email, emailFont, emailBrush, textX, emailY);
        }

        private void DrawRoleBadge(DataGridViewCellPaintingEventArgs e, User user)
        {
            string roleName = user.UserRoles?.FirstOrDefault()?.Role?.RoleName ?? "Customer";
            var (bg, text) = roleName switch
            {
                "Admin" => (Color.FromArgb(136, 78, 160), "Admin"),
                "Manager" => (Color.FromArgb(230, 74, 25), "Manager"),
                "Staff" => (Color.FromArgb(25, 118, 210), "Staff"),
                _ => (Color.FromArgb(97, 97, 97), "Customer")
            };
            DrawBadge(e, text, bg, Color.White);
        }

        private void DrawStatusBadge(DataGridViewCellPaintingEventArgs e, User user)
        {
            var (bg, textColor, text) = user.IsActive
                ? (Color.FromArgb(76, 175, 80), Color.White, "Active")
                : (Color.FromArgb(244, 67, 54), Color.White, "Locked");
            DrawBadge(e, text, bg, textColor);
        }

        private void DrawVerifiedBadge(DataGridViewCellPaintingEventArgs e, User user)
        {
            if (user.IsVerified)
                DrawBadge(e, "Verified", Color.FromArgb(76, 175, 80), Color.White);
            else
                DrawBadge(e, "Unverified", Color.FromArgb(255, 152, 0), Color.White);
        }

        private void DrawCreateDate(DataGridViewCellPaintingEventArgs e, User user)
        {
            string text = user.CreatedAt.ToString("dd/MM/yyyy");
            DrawCenteredText(e, text);
        }

        private void DrawCenteredText(DataGridViewCellPaintingEventArgs e, string text)
        {
            using var brush = new SolidBrush(Color.Black); // Fixed color to Black
            using var font = new Font("Segoe UI", 9.5F);
            e.Graphics.DrawString(text, font, brush, e.CellBounds, GetCenterFormat());
        }

        private void DrawBadge(DataGridViewCellPaintingEventArgs e, string text, Color bg, Color fg)
        {
            int w = 100;
            int h = BADGE_HEIGHT;
            int x = e.CellBounds.X + (e.CellBounds.Width - w) / 2;
            int y = e.CellBounds.Y + (e.CellBounds.Height - h) / 2;
            var rect = new Rectangle(x, y, w, h);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var brush = new SolidBrush(bg);
            e.Graphics.FillEllipse(brush, rect); // Using Ellipse/Rounded feel

            using var font = new Font("Segoe UI", 9F, FontStyle.Bold);
            using var textBrush = new SolidBrush(fg);
            e.Graphics.DrawString(text, font, textBrush, rect, GetCenterFormat());
        }



        private void DgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // No action column anymore
        }

        private void EditUser(User user)
        {
            using var form = new EditUserForm(user, _userController);
            if (form.ShowDialog() == DialogResult.OK) LoadPage();
        }

        private async void ToggleUserStatus(User user)
        {
            var newStatus = !user.IsActive;
            var action = newStatus ? "unlock" : "lock";
            var result = MessageBox.Show($"Are you sure you want to {action} account:\n{user.FullName}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bool success = newStatus ? await _userController.ActivateUserAsync(user.UserId) : await _userController.DeactivateUserAsync(user.UserId);
                if (success) { user.IsActive = newStatus; LoadPage(); MessageBox.Show($"Successfully {action}ed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else MessageBox.Show("Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeUserRole(User user) { using var form = new ChangeRoleForm(user, _userController); if (form.ShowDialog() == DialogResult.OK) LoadPage(); }
        
        private void ViewUserDetail(User user)
        {
            var role = user.UserRoles?.FirstOrDefault()?.Role?.RoleName ?? "Customer";
            if (role == "Khách hàng") role = "Customer";
            MessageBox.Show($"ID: {user.UserId}\nName: {user.FullName}\nEmail: {user.Email}\nPhone: {user.Phone ?? "N/A"}\nRole: {role}\nStatus: {(user.IsActive ? "Active" : "Locked")}\nVerified: {user.IsVerified}\nCreated: {user.CreatedAt:dd/MM/yyyy}", "User Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private User? GetSelectedUser() {
             if (dgvUsers.CurrentRow != null && dgvUsers.CurrentRow.Index >= 0 && dgvUsers.CurrentRow.Index < pagedUsers.Count) return pagedUsers[dgvUsers.CurrentRow.Index];
             return null;
        }
        private void ExecuteView() { var u = GetSelectedUser(); if(u!=null) ViewUserDetail(u); else MessageBox.Show("Select a user row first!", "Info"); }
        private void ExecuteEdit() { var u = GetSelectedUser(); if(u!=null) EditUser(u); else MessageBox.Show("Select a user row first!", "Info"); }
        private void ExecuteDisable() { var u = GetSelectedUser(); if(u!=null) ToggleUserStatus(u); else MessageBox.Show("Select a user row first!", "Info"); }
        private void ExecuteRole() { var u = GetSelectedUser(); if(u!=null) ChangeUserRole(u); else MessageBox.Show("Select a user row first!", "Info"); }


        private void DgvUsers_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
             dgvUsers.Cursor = Cursors.Default;
        }

        private StringFormat GetCenterFormat() => new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        private Color GetAvatarColor(string role) => role switch
        {
            "Admin" => Color.FromArgb(136, 78, 160),
            "Manager" => Color.FromArgb(230, 74, 25),
            "Staff" => Color.FromArgb(25, 118, 210),
            _ => Color.FromArgb(117, 117, 117)
        };

        private string GetInitials(string name)
        {
            var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length >= 2
                ? $"{parts[0][0]}{parts[^1][0]}".ToUpper()
                : name.Length >= 2 ? name.Substring(0, 2).ToUpper() : name.ToUpper();
        }

        // Tìm kiếm
        private void ApplyFilters()
        {
            var query = users.AsEnumerable();

            // 1. Search
            string keyword = txtFindUsers.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(u =>
                    (u.FullName?.ToLower().Contains(keyword) ?? false) ||
                    (u.Email?.ToLower().Contains(keyword) ?? false) ||
                    (u.Phone?.Contains(keyword) ?? false));
            }

            // 2. Role
            if (cboRole.SelectedIndex > 0)
            {
                if (cboRole.SelectedItem is RoleSelectDto roleDto)
                {
                     query = query.Where(u => (u.UserRoles?.FirstOrDefault()?.Role?.RoleName ?? "Customer") == roleDto.RoleName);
                }
                else
                {
                    string selectedRole = cboRole.SelectedItem.ToString();
                    query = query.Where(u => (u.UserRoles?.FirstOrDefault()?.Role?.RoleName ?? "Customer") == selectedRole);
                }
            }

            // 3. Status
            // Items: All, Active, Locked
            if (cboStatus.SelectedIndex > 0)
            {
                // Index 1 = Active, Index 2 = Locked
                bool isActive = cboStatus.SelectedIndex == 1; 
                query = query.Where(u => u.IsActive == isActive);
            }

            // 4. Verified
            // Items: All, Verified, Unverified
            if (cboXacThuc.SelectedIndex > 0)
            {
                bool isVerified = cboXacThuc.SelectedIndex == 1;
                query = query.Where(u => u.IsVerified == isVerified);
            }

            // 5. Sort
            query = cboSort.SelectedIndex switch
            {
                0 => query.OrderByDescending(u => u.CreatedAt),
                1 => query.OrderBy(u => u.CreatedAt),
                2 => query.OrderBy(u => u.FullName),
                3 => query.OrderByDescending(u => u.FullName),
                _ => query.OrderByDescending(u => u.CreatedAt)
            };

            filteredUsers = query.ToList();

            // Pagination Setup
            int totalRecords = filteredUsers.Count;
            _totalPages = (int)Math.Ceiling((double)totalRecords / _pageSize);
            if (_totalPages < 1) _totalPages = 1;

            paginationControl.TotalPages = _totalPages;
            // Only reset to 1 if ApplyFilters called explicitly (filter change), not page change.
            // But PageChange only calls LoadPage, not ApplyFilters. So safe to set 1.
            paginationControl.CurrentPage = 1; 
            _currentPage = 1;

            LoadPage();
        }

        private void LoadPage()
        {
            int skip = (_currentPage - 1) * _pageSize;
            pagedUsers = filteredUsers.Skip(skip).Take(_pageSize).ToList();

            dgvUsers.Rows.Clear();
            foreach (var user in pagedUsers) 
            {
                // Just add empty rows, CellPainting does the rest based on pagedUsers index
                dgvUsers.Rows.Add(); 
            }
            dgvUsers.Invalidate();
        }

        private void ResetFilters()
        {
             cboRole.SelectedIndex = 0;
             cboStatus.SelectedIndex = 0;
             cboXacThuc.SelectedIndex = 0;
             cboSort.SelectedIndex = 0;
             txtFindUsers.Text = "";
        }
    }

    // Form đổi role (có lưu thật)
    public class ChangeRoleForm : Form
    {
        private readonly User _user;
        private readonly UserContrller _controller;
        private ComboBox cboRole;

        public ChangeRoleForm(User user, UserContrller controller)
        {
            _user = user;
            _controller = controller;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Change User Role";
            this.Size = new Size(420, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = this.MinimizeBox = false;

            var lbl = new Label { Text = "Select New Role:", Location = new Point(20, 20), AutoSize = true };
            cboRole = new ComboBox
            {
                Location = new Point(20, 50),
                Width = 360,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboRole.Items.AddRange(new[] { "Admin", "Manager", "Staff", "Customer" });

            var current = _user.UserRoles?.FirstOrDefault()?.Role?.RoleName ?? "Customer";
            if (current == "Khách hàng") current = "Customer";
            cboRole.SelectedItem = current;
            if (cboRole.SelectedIndex < 0) cboRole.SelectedIndex = 3;

            var btnOk = new Button
            {
                Text = "Save",
                DialogResult = DialogResult.OK,
                Location = new Point(230, 110),
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White
            };
            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(320, 110),
                Size = new Size(80, 35)
            };

            btnOk.Click += async (s, e) =>
            {
                if (cboRole.SelectedItem is string role)
                {
                    int roleId = role switch { "Admin" => 1, "Manager" => 2, "Staff" => 3, _ => 4 };
                    var (success, msg) = await _controller.AddUserRoleAsync(_user.Email, roleId);
                    MessageBox.Show(msg, success ? "Success" : "Error",
                        MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
            };

            this.Controls.AddRange(new Control[] { lbl, cboRole, btnOk, btnCancel });
        }
    }

    public class EditUserForm : Form
    {
        private readonly User _user;
        private readonly UserContrller _controller;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private ComboBox cboGender;

        public EditUserForm(User user, UserContrller controller)
        {
            _user = user;
            _controller = controller;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Edit User Profile";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var lblPhone = new Label { Text = "Phone:", Location = new Point(20, 20), AutoSize = true };
            txtPhone = new TextBox { Text = _user.Phone, Location = new Point(100, 17), Width = 250 };

            var lblGender = new Label { Text = "Gender:", Location = new Point(20, 60), AutoSize = true };
            cboGender = new ComboBox { Location = new Point(100, 57), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cboGender.Items.AddRange(new[] { "Male", "Female", "Other" });
            
            var currentGender = _user.Gender?.ToUpper() ?? "OTHER";
            if (currentGender.StartsWith("M")) cboGender.SelectedItem = "Male";
            else if (currentGender.StartsWith("F")) cboGender.SelectedItem = "Female";
            else cboGender.SelectedItem = "Other";

            var lblAddress = new Label { Text = "Address:", Location = new Point(20, 100), AutoSize = true };
            txtAddress = new TextBox { Text = _user.Address, Location = new Point(100, 97), Width = 250 };

            var btnSave = new Button { Text = "Save", Location = new Point(190, 200), Size = new Size(75, 30), BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White };
            var btnCancel = new Button { Text = "Cancel", Location = new Point(275, 200), Size = new Size(75, 30), DialogResult = DialogResult.Cancel };

            btnSave.Click += async (s, e) =>
            {
                string phone = txtPhone.Text;
                string address = txtAddress.Text;
                string selectedGender = cboGender.SelectedItem?.ToString() ?? "Other";
                string genderCode = selectedGender == "Male" ? "M" : (selectedGender == "Female" ? "F" : "O");

                btnSave.Enabled = false;
                bool success = await _controller.UpdateProfileAsync(_user.UserId, phone, genderCode, address);
                if (!success)
                {
                    MessageBox.Show("Update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSave.Enabled = true;
                }
                else
                {
                   _user.Phone = phone;
                   _user.Gender = genderCode;
                   _user.Address = address;
                   this.DialogResult = DialogResult.OK;
                   this.Close();
                }
            };

            this.Controls.AddRange(new Control[] { lblPhone, txtPhone, lblGender, cboGender, lblAddress, txtAddress, btnSave, btnCancel });
        }
    }
}