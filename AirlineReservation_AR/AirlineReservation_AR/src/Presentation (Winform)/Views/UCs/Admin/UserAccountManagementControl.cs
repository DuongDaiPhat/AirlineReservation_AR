using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.Forms.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.UCs.Admin
{
    public partial class UserAccountManagementControl : UserControl
    {
        private List<User> users = new List<User>();
        private List<User> filteredUsers = new List<User>();
        private const int AVATAR_SIZE = 40;
        private const int BADGE_HEIGHT = 28;
        private const int BUTTON_SIZE = 30;
        public UserAccountManagementControl()
        {
            InitializeComponent();
            //InitializeDataGridView();
            //LoadSampleData();
            //LoadUsers();
            if (this.ParentForm is MenuAdminDashboard main)
            {
                main.SidebarStateChanged += Main_SidebarStateChanged;
            }
        }
        private void Main_SidebarStateChanged(bool isExpanded)
        {
            if (isExpanded)
            {
                //dgvUsers.Columns["colID"].Width += 10;
                //dgvUsers.Columns["colUser"].Width += 20;
                //dgvUsers.Columns["colPhone"].Width += 20;
                //dgvUsers.Columns["colRole"].Width += 20;
                //dgvUsers.Columns["colStatus"].Width += 20;
                //dgvUsers.Columns["colVerified"].Width += 20;
                //dgvUsers.Columns["colLastLogin"].Width += 30;
                //dgvUsers.Columns["colCreateDate"].Width += 20;
                //dgvUsers.Columns["colActions"].Width += 20;
            }
            else
            {

            }
        }
        private void InitializeDataGridView()
        {
            // Style cho header
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersHeight = 50;

            // Style cho rows
            dgvUsers.RowTemplate.Height = 80;
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            // Bắt sự kiện vẽ cell
            dgvUsers.CellPainting += DgvUsers_CellPainting;
            dgvUsers.CellClick += DgvUsers_CellClick;
            dgvUsers.CellMouseMove += DgvUsers_CellMouseMove;
        }
        private void LoadSampleData()
        {
            users = new List<User>
            {
                new User
                {
                    FullName = "Nguyễn Văn Thành",
                    Email = "thanh@email.com",
                    Phone = "0901234567",
                    IsVerified = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2023, 3, 15),
                    //LastLogin = new DateTime(2024, 12, 15, 10, 30, 0),
                    UserRoles = new List<UserRole>
                    {
                        new UserRole { Role = new Role { RoleName = "Admin" } }
                    }
                },
                new User
                {
                    FullName = "Lê Thị Hương",
                    Email = "huong@email.com",
                    Phone = "0909876543",
                    IsVerified = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2023, 5, 22),
                    //LastLogin = new DateTime(2024, 12, 15, 9, 15, 0),
                    UserRoles = new List<UserRole>
                    {
                        new UserRole { Role = new Role { RoleName = "Manager" } }
                    }
                },
                new User
                {
                    FullName = "Phạm Minh Đức",
                    Email = "duc@email.com",
                    Phone = "0912345678",
                    IsVerified = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2023, 7, 8),
                    //LastLogin = new DateTime(2024, 12, 14, 16, 20, 0),
                    UserRoles = new List<UserRole>
                    {
                        new UserRole { Role = new Role { RoleName = "Staff" } }
                    }
                },
                new User
                {
                    FullName = "Trần Thị Lan",
                    Email = "lan@email.com",
                    Phone = "0923456789",
                    IsVerified = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2023, 9, 12),
                    //LastLogin = new DateTime(2024, 12, 13, 14, 45, 0),
                    UserRoles = new List<UserRole>
                    {
                        new UserRole { Role = new Role { RoleName = "Khách hàng" } }
                    }
                }
            };
        }
        private void DgvUsers_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var user = users[e.RowIndex];

            // Vẽ background
            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground);

            // Vẽ theo từng column
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
                    DrawLastLogin(e, user);
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

        private void DrawID(DataGridViewCellPaintingEventArgs e, User user)
        {
            if (e.Graphics == null) return;

            string idText = user.UserId.ToString().PadLeft(3, '0');
            using var brush = new SolidBrush(Color.Black);
            using var boldFont = new Font("Segoe UI", 11, FontStyle.Bold);
            e.Graphics.DrawString(idText, boldFont, brush, e.CellBounds, GetCenterFormat());
        }

        private void DrawUserInfo(DataGridViewCellPaintingEventArgs e, User user)
        {
            if (e.Graphics == null) return;

            int avatarX = e.CellBounds.X + 15;
            int avatarY = e.CellBounds.Y + (e.CellBounds.Height - AVATAR_SIZE) / 2;

            // Vẽ avatar tròn
            string role = user.UserRoles.FirstOrDefault()?.Role.RoleName ?? "Khách hàng";
            using (var avatarBrush = new SolidBrush(GetAvatarColor(role)))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(avatarBrush, avatarX, avatarY, AVATAR_SIZE, AVATAR_SIZE);

                // Vẽ chữ cái trong avatar
                using var avatarFont = new Font("Segoe UI", 18, FontStyle.Bold);
                using var textBrush = new SolidBrush(Color.White);
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                var avatarRect = new Rectangle(avatarX, avatarY, AVATAR_SIZE, AVATAR_SIZE);
                e.Graphics.DrawString(GetInitials(user.FullName), avatarFont, textBrush, avatarRect, sf);
            }

            // Vẽ text thông tin
            int textX = avatarX + AVATAR_SIZE + 15;
            int nameY = e.CellBounds.Y + 22;
            int emailY = nameY + 28;

            using var nameFont = new Font("Segoe UI", 10, FontStyle.Bold);
            using var emailFont = new Font("Segoe UI", 9);
            using var nameBrush = new SolidBrush(Color.Black);
            using var emailBrush = new SolidBrush(Color.FromArgb(100, 100, 100));

            e.Graphics.DrawString(user.FullName, nameFont, nameBrush, textX, nameY);
            e.Graphics.DrawString(user.Email, emailFont, emailBrush, textX, emailY);
        }

        private void DrawCenteredText(DataGridViewCellPaintingEventArgs e, string text)
        {
            if (e.Graphics == null) return;

            using var brush = new SolidBrush(Color.Black);
            using var font = new Font("Segoe UI", 9);
            e.Graphics.DrawString(text, font, brush, e.CellBounds, GetCenterFormat());
        }

        private void DrawRoleBadge(DataGridViewCellPaintingEventArgs e, User user)
        {
            string roleName = user.UserRoles?.FirstOrDefault()?.Role.RoleName ?? "Khách hàng";
            var (badgeColor, roleText) = roleName switch
            {
                "Admin" => (Color.FromArgb(255, 193, 7), "Admin"),
                "Manager" => (Color.FromArgb(233, 30, 99), "Manager"),
                "Staff" => (Color.FromArgb(103, 58, 183), "Staff"),
                _ => (Color.FromArgb(158, 158, 158), "Khách hàng")
            };

            DrawBadge(e, roleText, badgeColor, Color.White);
        }

        private static void DrawStatusBadge(DataGridViewCellPaintingEventArgs e, User user)
        {
            string userStatus = user.IsActive ? "Hoạt động" : "Không hoạt động";

            var (badgeColor, textColor) = userStatus switch
            {
                "Hoạt động" => (Color.FromArgb(129, 199, 132), Color.White),
                "Không hoạt động" => (Color.FromArgb(239, 83, 80), Color.White),
                _ => (Color.FromArgb(189, 189, 189), Color.White)
            };

            DrawBadge(e, userStatus, badgeColor, textColor);
        }

        private static void DrawVerifiedBadge(DataGridViewCellPaintingEventArgs e, User user)
        {
            if (user.IsVerified)
            {
                DrawBadge(e, "✓ Đã xác thực", Color.FromArgb(129, 199, 132), Color.White);
            }
            else
            {
                DrawBadge(e, "⏳ Chưa xác thực", Color.FromArgb(255, 213, 79), Color.FromArgb(51, 51, 51));
            }
        }

        private void DrawLastLogin(DataGridViewCellPaintingEventArgs e, User user)
        {
            //|| user.LastLogin == null
            if (e.Graphics == null ) return;

            int textX = e.CellBounds.X + 10;
            int dateY = e.CellBounds.Y + 27;
            int timeY = dateY + 23;

            using var dateFont = new Font("Segoe UI", 9, FontStyle.Bold);
            using var timeFont = new Font("Segoe UI", 8);
            using var dateBrush = new SolidBrush(Color.Black);
            using var timeBrush = new SolidBrush(Color.FromArgb(117, 117, 117));

            //e.Graphics.DrawString(user.LastLogin.Value.ToString("dd/MM/yyyy"), dateFont, dateBrush, textX, dateY);
            //e.Graphics.DrawString(user.LastLogin.Value.ToString("HH:mm"), timeFont, timeBrush, textX, timeY);
        }

        private void DrawCreateDate(DataGridViewCellPaintingEventArgs e, User user)
        {
            if (e.Graphics == null) return;

            string dateText = user.CreatedAt.ToString("dd/MM/yyyy");
            using var brush = new SolidBrush(Color.Black);
            using var font = new Font("Segoe UI", 9);
            e.Graphics.DrawString(dateText, font, brush, e.CellBounds, GetCenterFormat());
        }

        private static void DrawActionButtons(DataGridViewCellPaintingEventArgs e)
        {
            if (e.Graphics == null) return;

            int centerX = e.CellBounds.X + e.CellBounds.Width / 2;
            int buttonY = e.CellBounds.Y + (e.CellBounds.Height - BUTTON_SIZE) / 2;
            int spacing = 8;
            int totalWidth = (BUTTON_SIZE * 3) + (spacing * 2);
            int startX = centerX - (totalWidth / 2);

            // Nút xem (màu xanh dương nhạt)
            var viewRect = new Rectangle(startX, buttonY, BUTTON_SIZE, BUTTON_SIZE);
            DrawIconButton(e.Graphics, viewRect, "👁", Color.FromArgb(38, 166, 154));

            // Nút sửa vai trò (màu vàng)
            var roleRect = new Rectangle(startX + BUTTON_SIZE + spacing, buttonY, BUTTON_SIZE, BUTTON_SIZE);
            DrawIconButton(e.Graphics, roleRect, "🔑", Color.FromArgb(255, 193, 7));

            // Nút thay đổi trạng thái (màu xanh lá)
            var statusRect = new Rectangle(startX + (BUTTON_SIZE + spacing) * 2, buttonY, BUTTON_SIZE, BUTTON_SIZE);
            DrawIconButton(e.Graphics, statusRect, "⚡", Color.FromArgb(102, 187, 106));

            // Lưu vị trí buttons để xử lý click
            e.CellStyle.Tag = new List<Rectangle> { viewRect, roleRect, statusRect };
        }

        private static void DrawIconButton(Graphics g, Rectangle rect, string icon, Color bgColor)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Vẽ nền bo tròn
            //using (var brush = new SolidBrush(bgColor))
            //{
            //    g.FillRoundedRectangle(brush, rect, 8);
            //}

            // Vẽ icon
            using var iconFont = new Font("Segoe UI", 16);
            using var textBrush = new SolidBrush(Color.White);
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(icon, iconFont, textBrush, rect, sf);
        }

        private static void DrawBadge(DataGridViewCellPaintingEventArgs e, string text, Color bgColor, Color textColor)
        {
            if (e.Graphics == null) return;

            int badgeWidth = 110;
            int badgeX = e.CellBounds.X + (e.CellBounds.Width - badgeWidth) / 2;
            int badgeY = e.CellBounds.Y + (e.CellBounds.Height - BADGE_HEIGHT) / 2;
            var badgeRect = new Rectangle(badgeX, badgeY, badgeWidth, BADGE_HEIGHT);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Vẽ nền badge bo tròn
            //using (var brush = new SolidBrush(bgColor))
            //{
            //    e.Graphics.FillRoundedRectangle(brush, badgeRect, 16);
            //}

            // Vẽ text
            using var font = new Font("Segoe UI", 9, FontStyle.Bold);
            using var textBrush = new SolidBrush(textColor);
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            e.Graphics.DrawString(text, font, textBrush, badgeRect, sf);
        }

        private static Color GetAvatarColor(string role)
        {
            return role switch
            {
                "Admin" => Color.FromArgb(103, 58, 183),
                "Manager" => Color.FromArgb(233, 30, 99),
                "Staff" => Color.FromArgb(103, 58, 183),
                _ => Color.FromArgb(158, 158, 158)
            };
        }

        private static string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "?";

            var parts = fullName.Trim().Split(' ');
            if (parts.Length == 1)
                return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();

            return (parts[0][0].ToString() + parts[^1][0].ToString()).ToUpper();
        }

        private static StringFormat GetCenterFormat()
        {
            return new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        private void LoadUsers()
        {
            dgvUsers.Rows.Clear();
            foreach (var user in users)
            {
                dgvUsers.Rows.Add();
            }
        }

        private void DgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvUsers.Columns[e.ColumnIndex].Name == "colActions")
            {
                var cell = dgvUsers.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Style.Tag is not List<Rectangle> buttons) return;

                var clickPoint = dgvUsers.PointToClient(Cursor.Position);
                var cellRect = dgvUsers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var relativeClick = new Point(clickPoint.X - cellRect.X, clickPoint.Y - cellRect.Y);

                var user = users[e.RowIndex];

                if (buttons[0].Contains(relativeClick))
                {
                    ViewUserDetail(user);
                }
                else if (buttons[1].Contains(relativeClick))
                {
                    ChangeUserRole(user);
                }
                else if (buttons[2].Contains(relativeClick))
                {
                    ChangeUserStatus(user);
                }
            }
        }

        private void DgvUsers_CellMouseMove(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvUsers.Cursor = dgvUsers.Columns[e.ColumnIndex].Name == "colActions"
                    ? Cursors.Hand
                    : Cursors.Default;
            }
        }

        private void ViewUserDetail(User user)
        {
            string role = user.UserRoles?.FirstOrDefault()?.Role.RoleName ?? "Khách hàng";
            string status = user.IsActive ? "Hoạt động" : "Không hoạt động";

            MessageBox.Show($"ID: #{user.UserId.ToString("N").Substring(0,8).ToUpper()}\n" +
                           $"Tên: {user.FullName}\n" +
                           $"Email: {user.Email}\n" +
                           $"SĐT: {user.Phone}\n" +
                           $"Vai trò: {role}\n" +
                           $"Trạng thái: {status}\n" +
                           $"Xác thực: {(user.IsVerified ? "Đã xác thực" : "Chưa xác thực")}\n" +
                           $"Ngày tạo: {user.CreatedAt:dd/MM/yyyy}",
                           "Thông tin người dùng",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
        }

        private void ChangeUserRole(User user)
        {
            using var form = new ChangeRoleForm(user);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgvUsers.Invalidate();
            }
        }

        private void ChangeUserStatus(User user)
        {
            string newStatus = user.IsActive ? "Không hoạt động" : "Hoạt động";
            var result = MessageBox.Show(
                $"Bạn có muốn thay đổi trạng thái người dùng '{user.FullName}' thành '{newStatus}'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                user.IsActive = !user.IsActive;
                dgvUsers.Invalidate();
                MessageBox.Show("Đã cập nhật trạng thái!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public class ChangeRoleForm : Form
    {
        private ComboBox cboRole;
        private Button btnSave;
        private Button btnCancel;
        private User user;

        public ChangeRoleForm(User user)
        {
            this.user = user;
            InitializeComponent();
            LoadCurrentRole();
        }

        private void InitializeComponent()
        {
            this.Text = "Thay đổi vai trò";
            this.Size = new Size(450, 220);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblRole = new Label
            {
                Text = "Chọn vai trò mới:",
                Location = new Point(30, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            cboRole = new ComboBox
            {
                Location = new Point(30, 60),
                Size = new Size(380, 35),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cboRole.Items.AddRange(new object[] { "Admin", "Manager", "Staff", "Khách hàng" });

            btnSave = new Button
            {
                Text = "Lưu thay đổi",
                Location = new Point(210, 130),
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(320, 130),
                Size = new Size(90, 40),
                BackColor = Color.FromArgb(189, 189, 189),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            this.Controls.Add(lblRole);
            this.Controls.Add(cboRole);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void LoadCurrentRole()
        {
            string currentRole = user.UserRoles?.FirstOrDefault()?.Role.RoleName ?? "Khách hàng";
            cboRole.SelectedItem = currentRole;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (cboRole.SelectedItem != null)
            {
                string newRole = cboRole.SelectedItem.ToString()!;
                // TODO: Cập nhật vào database khi kết nối
                MessageBox.Show($"Đã cập nhật vai trò thành '{newRole}'!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

namespace AirlineReservation_AR.src.Presentation__WinForms_.Views.UserControls
{
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            ArgumentNullException.ThrowIfNull(graphics);
            ArgumentNullException.ThrowIfNull(brush);

            using var path = RoundedRect(bounds, cornerRadius);
            graphics.FillPath(brush, path);
        }

        private static System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var size = new Size(diameter, diameter);
            var arc = new Rectangle(bounds.Location, size);
            var path = new System.Drawing.Drawing2D.GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
