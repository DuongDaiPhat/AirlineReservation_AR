using System.Drawing;
using AirlineReservation_AR.Properties;

namespace AR_Winform.Presentation.UControls.UserCntl
{
    partial class Searching
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.searchBox = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.seachBtn = new Guna.UI2.WinForms.Guna2Button();
            this.closeBtn = new Guna.UI2.WinForms.Guna2ImageButton();
            this.swapBtn = new Guna.UI2.WinForms.Guna2ImageButton();
            this.seatClassCB = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.typeTicketBtn = new Guna.UI2.WinForms.Guna2Button();
            this.reDateChB = new Guna.UI2.WinForms.Guna2CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.deDateBtn = new Guna.UI2.WinForms.Guna2Button();
            this.reDateBtn = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fromAirportBtn = new Guna.UI2.WinForms.Guna2Button();
            this.roundtripRBtn = new Guna.UI2.WinForms.Guna2RadioButton();
            this.onewayRBtn = new Guna.UI2.WinForms.Guna2RadioButton();
            this.toAirportBtn = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.searchBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.BackColor = System.Drawing.Color.Transparent;
            this.searchBox.Controls.Add(this.seachBtn);
            this.searchBox.Controls.Add(this.closeBtn);
            this.searchBox.Controls.Add(this.swapBtn);
            this.searchBox.Controls.Add(this.seatClassCB);
            this.searchBox.Controls.Add(this.guna2PictureBox3);
            this.searchBox.Controls.Add(this.typeTicketBtn);
            this.searchBox.Controls.Add(this.reDateChB);
            this.searchBox.Controls.Add(this.label4);
            this.searchBox.Controls.Add(this.deDateBtn);
            this.searchBox.Controls.Add(this.reDateBtn);
            this.searchBox.Controls.Add(this.label2);
            this.searchBox.Controls.Add(this.label1);
            this.searchBox.Controls.Add(this.fromAirportBtn);
            this.searchBox.Controls.Add(this.roundtripRBtn);
            this.searchBox.Controls.Add(this.onewayRBtn);
            this.searchBox.Controls.Add(this.toAirportBtn);
            this.searchBox.FillColor = System.Drawing.Color.WhiteSmoke;
            this.searchBox.Location = new System.Drawing.Point(0, 0);
            this.searchBox.Name = "searchBox";
            this.searchBox.Radius = 10;
            this.searchBox.ShadowColor = System.Drawing.Color.Black;
            this.searchBox.ShadowDepth = 0;
            this.searchBox.ShadowShift = 0;
            this.searchBox.Size = new System.Drawing.Size(710, 250);
            this.searchBox.TabIndex = 2;
            // 
            // seachBtn
            // 
            this.seachBtn.BorderRadius = 10;
            this.seachBtn.CheckedState.Parent = this.seachBtn;
            this.seachBtn.CustomImages.Parent = this.seachBtn;
            this.seachBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(94)))), ((int)(((byte)(31)))));
            this.seachBtn.Font = new System.Drawing.Font("Fz Poppins SemBd", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seachBtn.ForeColor = System.Drawing.Color.White;
            this.seachBtn.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(71)))), ((int)(((byte)(23)))));
            this.seachBtn.HoverState.Parent = this.seachBtn;
            this.seachBtn.Image = Resources.search_white;
            this.seachBtn.ImageSize = new System.Drawing.Size(15, 15);
            this.seachBtn.Location = new System.Drawing.Point(432, 187);
            this.seachBtn.Name = "seachBtn";
            this.seachBtn.ShadowDecoration.Parent = this.seachBtn;
            this.seachBtn.Size = new System.Drawing.Size(268, 45);
            this.seachBtn.TabIndex = 17;
            this.seachBtn.Text = "Search Flights";
            // 
            // closeBtn
            // 
            this.closeBtn.CheckedState.Parent = this.closeBtn;
            this.closeBtn.HoverState.ImageSize = new System.Drawing.Size(35, 35);
            this.closeBtn.HoverState.Parent = this.closeBtn;
            this.closeBtn.Image = Resources.close;
            this.closeBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.closeBtn.Location = new System.Drawing.Point(658, 15);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.PressedState.ImageSize = new System.Drawing.Size(35, 35);
            this.closeBtn.PressedState.Parent = this.closeBtn;
            this.closeBtn.Size = new System.Drawing.Size(35, 35);
            this.closeBtn.TabIndex = 16;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // swapBtn
            // 
            this.swapBtn.BackColor = System.Drawing.Color.White;
            this.swapBtn.CheckedState.Parent = this.swapBtn;
            this.swapBtn.HoverState.ImageSize = new System.Drawing.Size(35, 35);
            this.swapBtn.HoverState.Parent = this.swapBtn;
            this.swapBtn.Image = Resources.loading_arrow;
            this.swapBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.swapBtn.Location = new System.Drawing.Point(198, 91);
            this.swapBtn.Name = "swapBtn";
            this.swapBtn.PressedState.ImageSize = new System.Drawing.Size(35, 35);
            this.swapBtn.PressedState.Parent = this.swapBtn;
            this.swapBtn.Size = new System.Drawing.Size(35, 35);
            this.swapBtn.TabIndex = 15;
            // 
            // seatClassCB
            // 
            this.seatClassCB.BackColor = System.Drawing.Color.Transparent;
            this.seatClassCB.BorderColor = System.Drawing.Color.Black;
            this.seatClassCB.BorderRadius = 10;
            this.seatClassCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.seatClassCB.DropDownHeight = 110;
            this.seatClassCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seatClassCB.DropDownWidth = 180;
            this.seatClassCB.FocusedColor = System.Drawing.Color.Empty;
            this.seatClassCB.FocusedState.Parent = this.seatClassCB;
            this.seatClassCB.Font = new System.Drawing.Font("Fz Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seatClassCB.ForeColor = System.Drawing.Color.Black;
            this.seatClassCB.FormattingEnabled = true;
            this.seatClassCB.HoverState.Parent = this.seatClassCB;
            this.seatClassCB.IntegralHeight = false;
            this.seatClassCB.ItemHeight = 30;
            this.seatClassCB.ItemsAppearance.Parent = this.seatClassCB;
            this.seatClassCB.Location = new System.Drawing.Point(432, 137);
            this.seatClassCB.Name = "seatClassCB";
            this.seatClassCB.ShadowDecoration.Parent = this.seatClassCB;
            this.seatClassCB.Size = new System.Drawing.Size(268, 36);
            this.seatClassCB.TabIndex = 13;
            // 
            // guna2PictureBox3
            // 
            this.guna2PictureBox3.Image = Resources.right_arrow_tra;
            this.guna2PictureBox3.Location = new System.Drawing.Point(673, 98);
            this.guna2PictureBox3.Name = "guna2PictureBox3";
            this.guna2PictureBox3.ShadowDecoration.Parent = this.guna2PictureBox3;
            this.guna2PictureBox3.Size = new System.Drawing.Size(20, 20);
            this.guna2PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox3.TabIndex = 12;
            this.guna2PictureBox3.TabStop = false;
            this.guna2PictureBox3.UseTransparentBackground = true;
            // 
            // typeTicketBtn
            // 
            this.typeTicketBtn.BorderRadius = 10;
            this.typeTicketBtn.BorderThickness = 1;
            this.typeTicketBtn.CheckedState.Parent = this.typeTicketBtn;
            this.typeTicketBtn.CustomImages.Parent = this.typeTicketBtn;
            this.typeTicketBtn.FillColor = System.Drawing.Color.White;
            this.typeTicketBtn.Font = new System.Drawing.Font("Fz Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeTicketBtn.ForeColor = System.Drawing.Color.Black;
            this.typeTicketBtn.HoverState.Parent = this.typeTicketBtn;
            this.typeTicketBtn.Image = Resources.users;
            this.typeTicketBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.typeTicketBtn.ImageSize = new System.Drawing.Size(30, 30);
            this.typeTicketBtn.Location = new System.Drawing.Point(432, 86);
            this.typeTicketBtn.Name = "typeTicketBtn";
            this.typeTicketBtn.ShadowDecoration.Parent = this.typeTicketBtn;
            this.typeTicketBtn.Size = new System.Drawing.Size(268, 45);
            this.typeTicketBtn.TabIndex = 11;
            this.typeTicketBtn.Text = "3 Adult, 0 Child, 0 Infant (on lap)";
            this.typeTicketBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // reDateChB
            // 
            this.reDateChB.AutoSize = true;
            this.reDateChB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.reDateChB.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.reDateChB.CheckedState.BorderRadius = 2;
            this.reDateChB.CheckedState.BorderThickness = 1;
            this.reDateChB.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.reDateChB.Enabled = false;
            this.reDateChB.Font = new System.Drawing.Font("Fz Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reDateChB.Location = new System.Drawing.Point(229, 157);
            this.reDateChB.Name = "reDateChB";
            this.reDateChB.Size = new System.Drawing.Size(118, 30);
            this.reDateChB.TabIndex = 2;
            this.reDateChB.Text = "Return date";
            this.reDateChB.UncheckedState.BorderColor = System.Drawing.Color.DarkGray;
            this.reDateChB.UncheckedState.BorderRadius = 2;
            this.reDateChB.UncheckedState.BorderThickness = 1;
            this.reDateChB.UncheckedState.FillColor = System.Drawing.Color.White;
            this.reDateChB.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Fz Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 26);
            this.label4.TabIndex = 10;
            this.label4.Text = "Departure date";
            // 
            // deDateBtn
            // 
            this.deDateBtn.BorderRadius = 10;
            this.deDateBtn.BorderThickness = 1;
            this.deDateBtn.CheckedState.Parent = this.deDateBtn;
            this.deDateBtn.CustomImages.Parent = this.deDateBtn;
            this.deDateBtn.FillColor = System.Drawing.Color.White;
            this.deDateBtn.Font = new System.Drawing.Font("Fz Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deDateBtn.ForeColor = System.Drawing.Color.Black;
            this.deDateBtn.HoverState.Parent = this.deDateBtn;
            this.deDateBtn.Image = Resources.calendar;
            this.deDateBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.deDateBtn.ImageSize = new System.Drawing.Size(30, 30);
            this.deDateBtn.Location = new System.Drawing.Point(9, 187);
            this.deDateBtn.Name = "deDateBtn";
            this.deDateBtn.ShadowDecoration.Parent = this.deDateBtn;
            this.deDateBtn.Size = new System.Drawing.Size(202, 45);
            this.deDateBtn.TabIndex = 7;
            this.deDateBtn.Text = "27 Oct 2025";
            this.deDateBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // reDateBtn
            // 
            this.reDateBtn.BorderRadius = 10;
            this.reDateBtn.BorderThickness = 1;
            this.reDateBtn.CheckedState.Parent = this.reDateBtn;
            this.reDateBtn.CustomImages.Parent = this.reDateBtn;
            this.reDateBtn.Enabled = false;
            this.reDateBtn.FillColor = System.Drawing.Color.White;
            this.reDateBtn.Font = new System.Drawing.Font("Fz Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reDateBtn.ForeColor = System.Drawing.Color.Black;
            this.reDateBtn.HoverState.Parent = this.reDateBtn;
            this.reDateBtn.Image = Resources.calendar_unchecked;
            this.reDateBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.reDateBtn.ImageSize = new System.Drawing.Size(30, 30);
            this.reDateBtn.Location = new System.Drawing.Point(217, 187);
            this.reDateBtn.Name = "reDateBtn";
            this.reDateBtn.ShadowDecoration.Parent = this.reDateBtn;
            this.reDateBtn.Size = new System.Drawing.Size(200, 45);
            this.reDateBtn.TabIndex = 8;
            this.reDateBtn.Text = "29 Oct 2025";
            this.reDateBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Fz Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(246, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Fz Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "From";
            // 
            // fromAirportBtn
            // 
            this.fromAirportBtn.BorderRadius = 10;
            this.fromAirportBtn.BorderThickness = 1;
            this.fromAirportBtn.CheckedState.Parent = this.fromAirportBtn;
            this.fromAirportBtn.CustomImages.Parent = this.fromAirportBtn;
            this.fromAirportBtn.FillColor = System.Drawing.Color.White;
            this.fromAirportBtn.Font = new System.Drawing.Font("Fz Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromAirportBtn.ForeColor = System.Drawing.Color.Black;
            this.fromAirportBtn.HoverState.Parent = this.fromAirportBtn;
            this.fromAirportBtn.Image = Resources.take_off;
            this.fromAirportBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.fromAirportBtn.ImageSize = new System.Drawing.Size(30, 30);
            this.fromAirportBtn.Location = new System.Drawing.Point(11, 86);
            this.fromAirportBtn.Name = "fromAirportBtn";
            this.fromAirportBtn.ShadowDecoration.Parent = this.fromAirportBtn;
            this.fromAirportBtn.Size = new System.Drawing.Size(200, 45);
            this.fromAirportBtn.TabIndex = 2;
            this.fromAirportBtn.Text = "... (...)";
            this.fromAirportBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // roundtripRBtn
            // 
            this.roundtripRBtn.AutoSize = true;
            this.roundtripRBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.roundtripRBtn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.roundtripRBtn.CheckedState.BorderThickness = 0;
            this.roundtripRBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.roundtripRBtn.CheckedState.InnerColor = System.Drawing.Color.White;
            this.roundtripRBtn.CheckedState.InnerOffset = -4;
            this.roundtripRBtn.Font = new System.Drawing.Font("Fz Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundtripRBtn.ForeColor = System.Drawing.Color.Black;
            this.roundtripRBtn.Location = new System.Drawing.Point(125, 15);
            this.roundtripRBtn.Name = "roundtripRBtn";
            this.roundtripRBtn.Size = new System.Drawing.Size(112, 30);
            this.roundtripRBtn.TabIndex = 1;
            this.roundtripRBtn.TabStop = true;
            this.roundtripRBtn.Text = "Round-trip";
            this.roundtripRBtn.UncheckedState.BorderColor = System.Drawing.Color.Black;
            this.roundtripRBtn.UncheckedState.BorderThickness = 2;
            this.roundtripRBtn.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.roundtripRBtn.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.roundtripRBtn.UseVisualStyleBackColor = false;
            // 
            // onewayRBtn
            // 
            this.onewayRBtn.AutoSize = true;
            this.onewayRBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.onewayRBtn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.onewayRBtn.CheckedState.BorderThickness = 0;
            this.onewayRBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.onewayRBtn.CheckedState.InnerColor = System.Drawing.Color.White;
            this.onewayRBtn.CheckedState.InnerOffset = -4;
            this.onewayRBtn.Font = new System.Drawing.Font("Fz Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onewayRBtn.Location = new System.Drawing.Point(20, 15);
            this.onewayRBtn.Name = "onewayRBtn";
            this.onewayRBtn.Size = new System.Drawing.Size(99, 30);
            this.onewayRBtn.TabIndex = 0;
            this.onewayRBtn.TabStop = true;
            this.onewayRBtn.Text = "One-way";
            this.onewayRBtn.UncheckedState.BorderColor = System.Drawing.Color.Black;
            this.onewayRBtn.UncheckedState.BorderThickness = 2;
            this.onewayRBtn.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.onewayRBtn.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.onewayRBtn.UseVisualStyleBackColor = false;
            // 
            // toAirportBtn
            // 
            this.toAirportBtn.BorderRadius = 10;
            this.toAirportBtn.BorderThickness = 1;
            this.toAirportBtn.CheckedState.Parent = this.toAirportBtn;
            this.toAirportBtn.CustomImages.Parent = this.toAirportBtn;
            this.toAirportBtn.FillColor = System.Drawing.Color.White;
            this.toAirportBtn.Font = new System.Drawing.Font("Fz Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toAirportBtn.ForeColor = System.Drawing.Color.Black;
            this.toAirportBtn.HoverState.Parent = this.toAirportBtn;
            this.toAirportBtn.Image = Resources.landing;
            this.toAirportBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.toAirportBtn.ImageOffset = new System.Drawing.Point(20, 0);
            this.toAirportBtn.ImageSize = new System.Drawing.Size(30, 30);
            this.toAirportBtn.Location = new System.Drawing.Point(217, 86);
            this.toAirportBtn.Name = "toAirportBtn";
            this.toAirportBtn.ShadowDecoration.Parent = this.toAirportBtn;
            this.toAirportBtn.Size = new System.Drawing.Size(200, 45);
            this.toAirportBtn.TabIndex = 3;
            this.toAirportBtn.Text = "... (...)";
            this.toAirportBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.toAirportBtn.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            this.guna2Elipse1.TargetControl = this;
            // 
            // Searching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.searchBox);
            this.Name = "Searching";
            this.Size = new System.Drawing.Size(710, 250);
            this.searchBox.ResumeLayout(false);
            this.searchBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel searchBox;
        private Guna.UI2.WinForms.Guna2ImageButton swapBtn;
        private Guna.UI2.WinForms.Guna2ComboBox seatClassCB;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private Guna.UI2.WinForms.Guna2Button typeTicketBtn;
        private Guna.UI2.WinForms.Guna2CheckBox reDateChB;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Button deDateBtn;
        private Guna.UI2.WinForms.Guna2Button reDateBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button fromAirportBtn;
        private Guna.UI2.WinForms.Guna2RadioButton roundtripRBtn;
        private Guna.UI2.WinForms.Guna2RadioButton onewayRBtn;
        private Guna.UI2.WinForms.Guna2Button toAirportBtn;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ImageButton closeBtn;
        private Guna.UI2.WinForms.Guna2Button seachBtn;
    }
}
