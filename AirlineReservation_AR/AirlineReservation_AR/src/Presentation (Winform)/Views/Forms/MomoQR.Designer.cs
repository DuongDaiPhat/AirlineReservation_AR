using System.Drawing;
using System.Windows.Forms;

namespace MomoQR
{
    partial class MomoQR
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitle = new Label();
            btnClose = new Button();
            panelMain = new Panel();
            txtAmount = new TextBox();
            lblAmount = new Label();
            lblMessage = new Label();
            txtMessage = new TextBox();
            lblPaymentMethod = new Label();
            panelWallet = new Panel();
            lblWalletBalance = new Label();
            lblWalletName = new Label();
            picWallet = new PictureBox();
            payButton = new Button();
            panelFooter = new Panel();
            lblFooterNote = new Label();
            panelHeader.SuspendLayout();
            panelMain.SuspendLayout();
            panelWallet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picWallet).BeginInit();
            panelFooter.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(165, 0, 100);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(btnClose);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(400, 60);
            panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(15, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(197, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "MOMO PAYMENT";
            // 
            // btnClose
            // 
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(355, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(40, 40);
            btnClose.TabIndex = 1;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(txtAmount);
            panelMain.Controls.Add(lblAmount);
            panelMain.Controls.Add(lblMessage);
            panelMain.Controls.Add(txtMessage);
            panelMain.Controls.Add(lblPaymentMethod);
            panelMain.Controls.Add(panelWallet);
            panelMain.Controls.Add(payButton);
            panelMain.Location = new Point(0, 60);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(400, 444);
            panelMain.TabIndex = 1;
            // 
            // txtAmount
            // 
            txtAmount.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAmount.ForeColor = Color.FromArgb(165, 0, 100);
            txtAmount.Location = new Point(20, 40);
            txtAmount.Name = "txtAmount";
            txtAmount.ReadOnly = true;
            txtAmount.Size = new Size(360, 43);
            txtAmount.TabIndex = 9;
            txtAmount.Text = "5000";
            txtAmount.KeyPress += txtAmount_KeyPress;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAmount.ForeColor = Color.Gray;
            lblAmount.Location = new Point(20, 16);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(69, 21);
            lblAmount.TabIndex = 0;
            lblAmount.Text = "Amount:";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMessage.ForeColor = Color.Gray;
            lblMessage.Location = new Point(20, 108);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(74, 21);
            lblMessage.TabIndex = 4;
            lblMessage.Text = "Message:";
            // 
            // txtMessage
            // 
            txtMessage.BorderStyle = BorderStyle.FixedSingle;
            txtMessage.Font = new Font("Segoe UI", 11F);
            txtMessage.Location = new Point(20, 132);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(360, 84);
            txtMessage.TabIndex = 5;
            txtMessage.Text = "Transfer money";
            // 
            // lblPaymentMethod
            // 
            lblPaymentMethod.AutoSize = true;
            lblPaymentMethod.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPaymentMethod.ForeColor = Color.Gray;
            lblPaymentMethod.Location = new Point(20, 237);
            lblPaymentMethod.Name = "lblPaymentMethod";
            lblPaymentMethod.Size = new Size(131, 21);
            lblPaymentMethod.TabIndex = 6;
            lblPaymentMethod.Text = "Payment method:";
            // 
            // panelWallet
            // 
            panelWallet.BackColor = Color.FromArgb(250, 240, 245);
            panelWallet.Controls.Add(lblWalletBalance);
            panelWallet.Controls.Add(lblWalletName);
            panelWallet.Controls.Add(picWallet);
            panelWallet.Location = new Point(20, 261);
            panelWallet.Name = "panelWallet";
            panelWallet.Size = new Size(360, 80);
            panelWallet.TabIndex = 7;
            // 
            // lblWalletBalance
            // 
            lblWalletBalance.AutoSize = true;
            lblWalletBalance.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWalletBalance.ForeColor = Color.Gray;
            lblWalletBalance.Location = new Point(70, 43);
            lblWalletBalance.Name = "lblWalletBalance";
            lblWalletBalance.Size = new Size(193, 20);
            lblWalletBalance.TabIndex = 2;
            lblWalletBalance.Text = "Total amounts: 10,500,000 đ";
            // 
            // lblWalletName
            // 
            lblWalletName.AutoSize = true;
            lblWalletName.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWalletName.ForeColor = Color.FromArgb(165, 0, 100);
            lblWalletName.Location = new Point(70, 18);
            lblWalletName.Name = "lblWalletName";
            lblWalletName.Size = new Size(132, 25);
            lblWalletName.TabIndex = 1;
            lblWalletName.Text = "MoMo Wallet";
            // 
            // picWallet
            // 
            picWallet.BackColor = Color.FromArgb(165, 0, 100);
            picWallet.Location = new Point(15, 18);
            picWallet.Name = "picWallet";
            picWallet.Size = new Size(45, 45);
            picWallet.TabIndex = 0;
            picWallet.TabStop = false;
            // 
            // payButton
            // 
            payButton.BackColor = Color.FromArgb(165, 0, 100);
            payButton.FlatAppearance.BorderSize = 0;
            payButton.FlatStyle = FlatStyle.Flat;
            payButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            payButton.ForeColor = Color.White;
            payButton.Location = new Point(20, 371);
            payButton.Name = "payButton";
            payButton.Size = new Size(360, 50);
            payButton.TabIndex = 8;
            payButton.Text = "CONFIRM PAYMENT";
            payButton.UseVisualStyleBackColor = false;
            payButton.Click += payButton_Click;
            payButton.MouseEnter += payButton_MouseEnter;
            payButton.MouseLeave += payButton_MouseLeave;
            // 
            // panelFooter
            // 
            panelFooter.BackColor = Color.FromArgb(245, 245, 245);
            panelFooter.Controls.Add(lblFooterNote);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Location = new Point(0, 505);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(400, 50);
            panelFooter.TabIndex = 2;
            // 
            // lblFooterNote
            // 
            lblFooterNote.Dock = DockStyle.Fill;
            lblFooterNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFooterNote.ForeColor = Color.Gray;
            lblFooterNote.Location = new Point(0, 0);
            lblFooterNote.Name = "lblFooterNote";
            lblFooterNote.Size = new Size(400, 50);
            lblFooterNote.TabIndex = 1;
            lblFooterNote.Text = "Transaction secured by MoMo";
            lblFooterNote.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MomoQR
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 555);
            Controls.Add(panelFooter);
            Controls.Add(panelMain);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MomoQR";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MoMo Payment";
            Load += MomoQR_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            panelWallet.ResumeLayout(false);
            panelWallet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picWallet).EndInit();
            panelFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblAmountValue;
        private System.Windows.Forms.Label lblRecipient;
        private System.Windows.Forms.TextBox txtRecipient;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.Panel panelWallet;
        private System.Windows.Forms.Label lblWalletName;
        private System.Windows.Forms.PictureBox picWallet;
        private System.Windows.Forms.Button payButton;
        private System.Windows.Forms.Panel panelFooter;
        private TextBox txtAmount;
        private Label lblWalletBalance;
        private Label lblFooterNote;
    }
}