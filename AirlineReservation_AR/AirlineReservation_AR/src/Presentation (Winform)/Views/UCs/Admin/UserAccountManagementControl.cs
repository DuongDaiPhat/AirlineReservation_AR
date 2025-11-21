using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Infrastructure.DI;
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
        private readonly UserContrller _userController = DIContainer.UserContrller; // ĐÃ SỬA TÊN
        private const int AVATAR_SIZE = 40;
        private const int BADGE_HEIGHT = 28;
        private const int BUTTON_SIZE = 32;

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

                var allUsers = await _userController.GetAllUsersAsync();
                users = allUsers.ToList();
                filteredUsers = new List<User>(users);

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvUsers.Enabled = true;
            }
        }

        private void RefreshGrid()
        {
            dgvUsers.Rows.Clear();
            foreach (var user in filteredUsers)
            {
                dgvUsers.Rows.Add();
            }
            dgvUsers.Invalidate(); // Kích hoạt vẽ lại
            //lblTotal.Text = $"Tổng: {filteredUsers.Count} người dùng";
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
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            dgvUsers.CellPainting += DgvUsers_CellPainting;
            dgvUsers.CellClick += DgvUsers_CellClick;
            dgvUsers.CellMouseMove += DgvUsers_CellMouseMove;
        }

        private void DgvUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= filteredUsers.Count) return;

            var user = filteredUsers[e.RowIndex];
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
                    DrawCenteredText(e, user.Phone ?? "Chưa có");
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
                case "colCreateDate":
                    DrawCreateDate(e, user);
                    break;
                case "colActions":
                    DrawActionButtons(e);
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
            string roleName = user.UserRoles?.FirstOrDefault()?.Role?.RoleName ?? "Khách hàng";
            var (bg, text) = roleName switch
            {
                "Admin" => (Color.FromArgb(136, 78, 160), "Admin"),
                "Manager" => (Color.FromArgb(230, 74, 25), "Manager"),
                "Staff" => (Color.FromArgb(25, 118, 210), "Staff"),
                _ => (Color.FromArgb(97, 97, 97), "Khách hàng")
            };
            DrawBadge(e, text, bg, Color.White);
        }

        private void DrawStatusBadge(DataGridViewCellPaintingEventArgs e, User user)
        {
            var (bg, textColor, text) = user.IsActive
                ? (Color.FromArgb(76, 175, 80), Color.White, "Hoạt động")
                : (Color.FromArgb(244, 67, 54), Color.White, "Bị khóa");
            DrawBadge(e, text, bg, textColor);
        }

        private void DrawVerifiedBadge(DataGridViewCellPaintingEventArgs e, User user)
        {
            if (user.IsVerified)
                DrawBadge(e, "Đã xác thực", Color.FromArgb(76, 175, 80), Color.White);
            else
                DrawBadge(e, "Chưa xác thực", Color.FromArgb(255, 152, 0), Color.White);
        }

        private void DrawCreateDate(DataGridViewCellPaintingEventArgs e, User user)
        {
            string text = user.CreatedAt.ToString("dd/MM/yyyy");
            DrawCenteredText(e, text);
        }

        private void DrawCenteredText(DataGridViewCellPaintingEventArgs e, string text)
        {
            using var brush = new SolidBrush(Color.FromArgb(64, 64, 64));
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
            //e.Graphics.FillRoundedRectangle(brush, rect, 14);

            using var font = new Font("Segoe UI", 9F, FontStyle.Bold);
            using var textBrush = new SolidBrush(fg);
            e.Graphics.DrawString(text, font, textBrush, rect, GetCenterFormat());
        }

        private void DrawActionButtons(DataGridViewCellPaintingEventArgs e)
        {
            int centerX = e.CellBounds.X + e.CellBounds.Width / 2;
            int y = e.CellBounds.Y + (e.CellBounds.Height - BUTTON_SIZE) / 2;
            int spacing = 10;
            int startX = centerX - (BUTTON_SIZE * 3 + spacing * 2) / 2;

            var btn1 = new Rectangle(startX, y, BUTTON_SIZE, BUTTON_SIZE);
            var btn2 = new Rectangle(startX + BUTTON_SIZE + spacing, y, BUTTON_SIZE, BUTTON_SIZE);
            var btn3 = new Rectangle(startX + (BUTTON_SIZE + spacing) * 2, y, BUTTON_SIZE, BUTTON_SIZE);

            DrawIconButton(e.Graphics, btn1, "Xem", Color.FromArgb(0, 122, 204));
            DrawIconButton(e.Graphics, btn2, "Vai trò", Color.FromArgb(255, 152, 0));
            DrawIconButton(e.Graphics, btn3, "Trạng thái", Color.FromArgb(76, 175, 80));

            e.CellStyle.Tag = new[] { btn1, btn2, btn3 };
        }

        private void DrawIconButton(Graphics g, Rectangle r, string text, Color bg)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var brush = new SolidBrush(bg);
            //g.FillRoundedRectangle(brush, r, 8);
            using var font = new Font("Segoe UI", 9F, FontStyle.Bold);
            using var tb = new SolidBrush(Color.White);
            g.DrawString(text, font, tb, r, GetCenterFormat());
        }

        private void DgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= filteredUsers.Count) return;
            if (dgvUsers.Columns[e.ColumnIndex].Name != "colActions") return;

            var cell = dgvUsers[e.ColumnIndex, e.RowIndex];
            if (cell.Style.Tag is not Rectangle[] buttons) return;

            var clickPoint = dgvUsers.PointToClient(Cursor.Position);
            var cellRect = dgvUsers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            var rel = new Point(clickPoint.X - cellRect.X, clickPoint.Y - cellRect.Y);

            var user = filteredUsers[e.RowIndex];

            if (buttons[0].Contains(rel))
                ViewUserDetail(user);
            else if (buttons[1].Contains(rel))
                ChangeUserRole(user);
            else if (buttons[2].Contains(rel))
                ToggleUserStatus(user);
        }

        private async void ToggleUserStatus(User user)
        {
            var newStatus = !user.IsActive;
            var action = newStatus ? "kích hoạt" : "khóa";
            var result = MessageBox.Show(
                $"Bạn có chắc muốn {action} tài khoản:\n{user.FullName} ({user.Email})?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool success = newStatus
                    ? await _userController.ActivateUserAsync(user.UserId)
                    : await _userController.DeactivateUserAsync(user.UserId);

                if (success)
                {
                    user.IsActive = newStatus;
                    dgvUsers.InvalidateCell(dgvUsers.Columns["colStatus"].Index, dgvUsers.Rows.IndexOf(dgvUsers.Rows.Cast<DataGridViewRow>().First(r => filteredUsers[dgvUsers.Rows.IndexOf(r)] == user)));
                    MessageBox.Show($"Đã {action} tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thao tác thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ChangeUserRole(User user)
        {
            using var form = new ChangeRoleForm(user, _userController);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgvUsers.Invalidate();
            }
        }

        private void ViewUserDetail(User user)
        {
            var role = user.UserRoles?.FirstOrDefault()?.Role?.RoleName ?? "Khách hàng";
            MessageBox.Show(
                $"ID: {user.UserId}\n" +
                $"Họ tên: {user.FullName}\n" +
                $"Email: {user.Email}\n" +
                $"SĐT: {user.Phone ?? "Chưa có"}\n" +
                $"Vai trò: {role}\n" +
                $"Trạng thái: {(user.IsActive ? "Hoạt động" : "Bị khóa")}\n" +
                $"Xác thực: {(user.IsVerified ? "Đã xác thực" : "Chưa xác thực")}\n" +
                $"Ngày tạo: {user.CreatedAt:dd/MM/yyyy HH:mm}",
                "Chi tiết người dùng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DgvUsers_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvUsers.Cursor = (e.ColumnIndex >= 0 && dgvUsers.Columns[e.ColumnIndex].Name == "colActions")
                ? Cursors.Hand : Cursors.Default;
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
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtFindUsers.Text.Trim().ToLower();
            filteredUsers = string.IsNullOrEmpty(keyword)
                ? new List<User>(users)
                : users.Where(u =>
                    u.FullName.ToLower().Contains(keyword) ||
                    u.Email.ToLower().Contains(keyword) ||
                    u.Phone?.Contains(keyword) == true).ToList();

            RefreshGrid();
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
            this.Text = "Thay đổi vai trò người dùng";
            this.Size = new Size(420, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = this.MinimizeBox = false;

            var lbl = new Label { Text = "Chọn vai trò mới:", Location = new Point(20, 20), AutoSize = true };
            cboRole = new ComboBox
            {
                Location = new Point(20, 50),
                Width = 360,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboRole.Items.AddRange(new[] { "Admin", "Manager", "Staff", "Khách hàng" });

            var current = _user.UserRoles?.FirstOrDefault()?.Role?.RoleName ?? "Khách hàng";
            cboRole.SelectedItem = current;

            var btnOk = new Button
            {
                Text = "Lưu",
                DialogResult = DialogResult.OK,
                Location = new Point(230, 110),
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White
            };
            var btnCancel = new Button
            {
                Text = "Hủy",
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
                    MessageBox.Show(msg, success ? "Thành công" : "Lỗi",
                        MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
            };

            this.Controls.AddRange(new Control[] { lbl, cboRole, btnOk, btnCancel });
        }
    }
}