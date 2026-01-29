using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin;

namespace AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.UCs.Admin
{
    partial class UserAccountManagementControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelFilters = new Guna.UI2.WinForms.Guna2Panel();
            btnActionRefresh = new Guna.UI2.WinForms.Guna2Button();
            btnActionRole = new Guna.UI2.WinForms.Guna2Button();

            btnActionDisable = new Guna.UI2.WinForms.Guna2Button();
            btnActionEdit = new Guna.UI2.WinForms.Guna2Button();
            btnActionView = new Guna.UI2.WinForms.Guna2Button();
            cboSort = new Guna.UI2.WinForms.Guna2ComboBox();
            cboXacThuc = new Guna.UI2.WinForms.Guna2ComboBox();
            cboStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            cboRole = new Guna.UI2.WinForms.Guna2ComboBox();
            txtFindUsers = new Guna.UI2.WinForms.Guna2TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            colCreatedDate = new DataGridViewTextBoxColumn();
            colLastLogin = new DataGridViewTextBoxColumn();
            colVerified = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colRole = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colUser = new DataGridViewTextBoxColumn();
            colID = new DataGridViewTextBoxColumn();
            dgvUsers = new DataGridView();
            panelBottom = new Panel();
            paginationControl = new PaginationControl();
            panelFilters.SuspendLayout();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // panelFilters
            // 
            panelFilters.BackColor = Color.WhiteSmoke;
            panelFilters.BorderColor = Color.Black;
            panelFilters.Controls.Add(btnActionRefresh);
            panelFilters.Controls.Add(btnActionRole);

            panelFilters.Controls.Add(btnActionDisable);
            panelFilters.Controls.Add(btnActionEdit);
            panelFilters.Controls.Add(btnActionView);
            panelFilters.Controls.Add(cboSort);
            panelFilters.Controls.Add(cboXacThuc);
            panelFilters.Controls.Add(cboStatus);
            panelFilters.Controls.Add(cboRole);
            panelFilters.Controls.Add(txtFindUsers);
            panelFilters.Controls.Add(label5);
            panelFilters.Controls.Add(label4);
            panelFilters.Controls.Add(label3);
            panelFilters.Controls.Add(label2);
            panelFilters.Controls.Add(label1);
            panelFilters.CustomizableEdges = customizableEdges23;
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Location = new Point(0, 0);
            panelFilters.Margin = new Padding(0);
            panelFilters.Name = "panelFilters";
            panelFilters.Padding = new Padding(15);
            panelFilters.ShadowDecoration.BorderRadius = 0;
            panelFilters.ShadowDecoration.CustomizableEdges = customizableEdges24;
            panelFilters.Size = new Size(1040, 150);
            panelFilters.TabIndex = 0;
            // 
            // btnActionRefresh
            // 
            btnActionRefresh.BorderRadius = 15;
            btnActionRefresh.CustomizableEdges = customizableEdges1;
            btnActionRefresh.FillColor = Color.Gray;
            btnActionRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnActionRefresh.ForeColor = Color.White;
            btnActionRefresh.Location = new Point(620, 97);
            btnActionRefresh.Name = "btnActionRefresh";
            btnActionRefresh.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnActionRefresh.Size = new Size(130, 35);
            btnActionRefresh.TabIndex = 11;
            btnActionRefresh.Text = "Refresh";
            // 
            // btnActionRole
            // 
            btnActionRole.BorderRadius = 15;
            btnActionRole.CustomizableEdges = customizableEdges3;
            btnActionRole.FillColor = Color.FromArgb(156, 39, 176);
            btnActionRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnActionRole.ForeColor = Color.White;
            btnActionRole.Location = new Point(465, 97);
            btnActionRole.Name = "btnActionRole";
            btnActionRole.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnActionRole.Size = new Size(130, 35);
            btnActionRole.TabIndex = 2;
            btnActionRole.Text = "Role";

            // 
            // btnActionDisable
            // 
            btnActionDisable.BorderRadius = 15;
            btnActionDisable.CustomizableEdges = customizableEdges7;
            btnActionDisable.FillColor = Color.FromArgb(255, 193, 7);
            btnActionDisable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnActionDisable.ForeColor = Color.White;
            btnActionDisable.Location = new Point(310, 97);
            btnActionDisable.Name = "btnActionDisable";
            btnActionDisable.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnActionDisable.Size = new Size(130, 35);
            btnActionDisable.TabIndex = 10;
            btnActionDisable.Text = "Disable";
            // 
            // btnActionEdit
            // 
            btnActionEdit.BorderRadius = 15;
            btnActionEdit.CustomizableEdges = customizableEdges9;
            btnActionEdit.FillColor = Color.FromArgb(40, 167, 69);
            btnActionEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnActionEdit.ForeColor = Color.White;
            btnActionEdit.Location = new Point(160, 97);
            btnActionEdit.Name = "btnActionEdit";
            btnActionEdit.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnActionEdit.Size = new Size(130, 35);
            btnActionEdit.TabIndex = 9;
            btnActionEdit.Text = "Edit";
            // 
            // btnActionView
            // 
            btnActionView.BorderRadius = 15;
            btnActionView.CustomizableEdges = customizableEdges11;
            btnActionView.FillColor = Color.FromArgb(0, 123, 255);
            btnActionView.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnActionView.ForeColor = Color.White;
            btnActionView.Location = new Point(15, 97);
            btnActionView.Name = "btnActionView";
            btnActionView.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnActionView.Size = new Size(130, 35);
            btnActionView.TabIndex = 1;
            btnActionView.Text = "View";
            // 
            // cboSort
            // 
            cboSort.BackColor = Color.Transparent;
            cboSort.BorderRadius = 10;
            cboSort.CustomizableEdges = customizableEdges13;
            cboSort.DrawMode = DrawMode.OwnerDrawFixed;
            cboSort.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSort.FocusedColor = Color.FromArgb(94, 148, 255);
            cboSort.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboSort.Font = new Font("Segoe UI", 10F);
            cboSort.ForeColor = Color.FromArgb(68, 88, 112);
            cboSort.ItemHeight = 30;
            cboSort.Location = new Point(850, 40);
            cboSort.Margin = new Padding(0);
            cboSort.Name = "cboSort";
            cboSort.ShadowDecoration.CustomizableEdges = customizableEdges14;
            cboSort.Size = new Size(150, 36);
            cboSort.TabIndex = 8;
            // 
            // cboXacThuc
            // 
            cboXacThuc.BackColor = Color.Transparent;
            cboXacThuc.BorderRadius = 10;
            cboXacThuc.CustomizableEdges = customizableEdges15;
            cboXacThuc.DrawMode = DrawMode.OwnerDrawFixed;
            cboXacThuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboXacThuc.FocusedColor = Color.FromArgb(94, 148, 255);
            cboXacThuc.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboXacThuc.Font = new Font("Segoe UI", 10F);
            cboXacThuc.ForeColor = Color.FromArgb(68, 88, 112);
            cboXacThuc.ItemHeight = 30;
            cboXacThuc.Location = new Point(650, 40);
            cboXacThuc.Margin = new Padding(0);
            cboXacThuc.Name = "cboXacThuc";
            cboXacThuc.ShadowDecoration.CustomizableEdges = customizableEdges16;
            cboXacThuc.Size = new Size(150, 36);
            cboXacThuc.TabIndex = 7;
            // 
            // cboStatus
            // 
            cboStatus.BackColor = Color.Transparent;
            cboStatus.BorderRadius = 10;
            cboStatus.CustomizableEdges = customizableEdges17;
            cboStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.FocusedColor = Color.FromArgb(94, 148, 255);
            cboStatus.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboStatus.Font = new Font("Segoe UI", 10F);
            cboStatus.ForeColor = Color.FromArgb(68, 88, 112);
            cboStatus.ItemHeight = 30;
            cboStatus.Location = new Point(445, 40);
            cboStatus.Margin = new Padding(0);
            cboStatus.Name = "cboStatus";
            cboStatus.ShadowDecoration.CustomizableEdges = customizableEdges18;
            cboStatus.Size = new Size(150, 36);
            cboStatus.TabIndex = 6;
            // 
            // cboRole
            // 
            cboRole.BackColor = Color.Transparent;
            cboRole.BorderRadius = 10;
            cboRole.CustomizableEdges = customizableEdges19;
            cboRole.DrawMode = DrawMode.OwnerDrawFixed;
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.FocusedColor = Color.FromArgb(94, 148, 255);
            cboRole.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboRole.Font = new Font("Segoe UI", 10F);
            cboRole.ForeColor = Color.FromArgb(68, 88, 112);
            cboRole.ItemHeight = 30;
            cboRole.Location = new Point(250, 40);
            cboRole.Margin = new Padding(0);
            cboRole.Name = "cboRole";
            cboRole.ShadowDecoration.CustomizableEdges = customizableEdges20;
            cboRole.Size = new Size(150, 36);
            cboRole.TabIndex = 5;
            // 
            // txtFindUsers
            // 
            txtFindUsers.BorderRadius = 10;
            txtFindUsers.CustomizableEdges = customizableEdges21;
            txtFindUsers.DefaultText = "";
            txtFindUsers.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtFindUsers.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtFindUsers.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtFindUsers.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtFindUsers.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtFindUsers.Font = new Font("Segoe UI", 9F);
            txtFindUsers.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtFindUsers.Location = new Point(15, 40);
            txtFindUsers.Margin = new Padding(0);
            txtFindUsers.Name = "txtFindUsers";
            txtFindUsers.PlaceholderText = "";
            txtFindUsers.SelectedText = "";
            txtFindUsers.ShadowDecoration.CustomizableEdges = customizableEdges22;
            txtFindUsers.Size = new Size(200, 36);
            txtFindUsers.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(850, 15);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(56, 19);
            label5.TabIndex = 4;
            label5.Text = "Sort By";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(650, 15);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(61, 19);
            label4.TabIndex = 3;
            label4.Text = "Verified";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(445, 15);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(70, 19);
            label3.TabIndex = 2;
            label3.Text = "Status";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(250, 15);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(49, 19);
            label2.TabIndex = 1;
            label2.Text = "Role";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 15);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(64, 19);
            label1.TabIndex = 0;
            label1.Text = "Search";
            // 
            // colActions
            // 
            // 
            // colCreatedDate
            // 
            colCreatedDate.HeaderText = "CREATED DATE";
            colCreatedDate.MinimumWidth = 100;
            colCreatedDate.Name = "colCreatedDate";
            colCreatedDate.ReadOnly = true;
            colCreatedDate.Width = 100;
            // 
            // colLastLogin
            // 
            colLastLogin.HeaderText = "LAST LOGIN";
            colLastLogin.MinimumWidth = 100;
            colLastLogin.Name = "colLastLogin";
            colLastLogin.ReadOnly = true;
            colLastLogin.Visible = false;
            // 
            // colVerified
            // 
            colVerified.HeaderText = "VERIFIED";
            colVerified.MinimumWidth = 100;
            colVerified.Name = "colVerified";
            colVerified.ReadOnly = true;
            colVerified.Width = 100;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "STATUS";
            colStatus.MinimumWidth = 100;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Width = 100;
            // 
            // colRole
            // 
            colRole.HeaderText = "ROLE";
            colRole.MinimumWidth = 100;
            colRole.Name = "colRole";
            colRole.ReadOnly = true;
            colRole.Width = 100;
            // 
            // colPhone
            // 
            colPhone.HeaderText = "PHONE";
            colPhone.MinimumWidth = 120;
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
            colPhone.Width = 150;
            // 
            // colUser
            // 
            colUser.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colUser.HeaderText = "USER";
            colUser.Name = "colUser";
            colUser.ReadOnly = true;
            colUser.Width = 300;
            // 
            // colID
            // 
            colID.HeaderText = "ID";
            colID.MinimumWidth = 60;
            colID.Name = "colID";
            colID.ReadOnly = true;
            colID.Width = 80;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Columns.AddRange(new DataGridViewColumn[] { colID, colUser, colPhone, colRole, colStatus, colVerified, colLastLogin, colCreatedDate });
            dgvUsers.Location = new Point(0, 200);
            dgvUsers.Margin = new Padding(0);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowTemplate.Height = 80;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(1040, 500);
            dgvUsers.TabIndex = 1;
            dgvUsers.Dock = DockStyle.Fill;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(paginationControl);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 600);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1040, 50);
            panelBottom.TabIndex = 2;
            // 
            // paginationControl
            // 
            paginationControl.BackColor = Color.White;
            paginationControl.Dock = DockStyle.Fill;
            paginationControl.Location = new Point(0, 0);
            paginationControl.Name = "paginationControl";
            paginationControl.Size = new Size(1040, 50);
            paginationControl.TabIndex = 0;
            // 
            // UserAccountManagementControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(dgvUsers);
            Controls.Add(panelBottom);
            Controls.Add(panelFilters);
            Margin = new Padding(0);
            Name = "UserAccountManagementControl";
            Size = new Size(1040, 650);
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2Panel panelFilters;
        private Guna.UI2.WinForms.Guna2ComboBox cboRole;
        private Guna.UI2.WinForms.Guna2TextBox txtFindUsers;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cboSort;
        private Guna.UI2.WinForms.Guna2ComboBox cboXacThuc;
        private Guna.UI2.WinForms.Guna2ComboBox cboStatus;
        private Guna.UI2.WinForms.Guna2Button btnActionRefresh;
        private Guna.UI2.WinForms.Guna2Button btnActionRole;

        private Guna.UI2.WinForms.Guna2Button btnActionDisable;
        private Guna.UI2.WinForms.Guna2Button btnActionEdit;
        private Guna.UI2.WinForms.Guna2Button btnActionView;
        private DataGridViewTextBoxColumn colCreatedDate;
        private DataGridViewTextBoxColumn colLastLogin;
        private DataGridViewTextBoxColumn colVerified;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colRole;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colUser;
        private DataGridViewTextBoxColumn colID;
        private DataGridView dgvUsers;
        private Panel panelBottom;
        private PaginationControl paginationControl;
    }
}
