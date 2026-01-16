namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class TitleMealCard
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleMealCard));
            noteBaggage = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            noteTextTile = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pictureBox5 = new PictureBox();
            noteBaggage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // noteBaggage
            // 
            noteBaggage.BackColor = Color.FromArgb(254, 252, 232);
            noteBaggage.BorderColor = Color.FromArgb(191, 219, 254);
            noteBaggage.BorderRadius = 8;
            noteBaggage.BorderThickness = 2;
            noteBaggage.Controls.Add(guna2HtmlLabel4);
            noteBaggage.Controls.Add(noteTextTile);
            noteBaggage.Controls.Add(pictureBox5);
            noteBaggage.CustomizableEdges = customizableEdges1;
            noteBaggage.ForeColor = Color.FromArgb(254, 252, 232);
            noteBaggage.Location = new Point(0, 3);
            noteBaggage.Margin = new Padding(15, 3, 3, 3);
            noteBaggage.Name = "noteBaggage";
            noteBaggage.ShadowDecoration.CustomizableEdges = customizableEdges2;
            noteBaggage.Size = new Size(595, 72);
            noteBaggage.TabIndex = 2;
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.BackColor = Color.Transparent;
            guna2HtmlLabel4.Font = new Font("Segoe UI Historic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guna2HtmlLabel4.ForeColor = Color.FromArgb(164, 77, 49);
            guna2HtmlLabel4.Location = new Point(79, 39);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(177, 19);
            guna2HtmlLabel4.TabIndex = 2;
            guna2HtmlLabel4.Text = "Do not  include the ticket price";
            // 
            // noteTextTile
            // 
            noteTextTile.BackColor = Color.Transparent;
            noteTextTile.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            noteTextTile.ForeColor = Color.FromArgb(164, 77, 14);
            noteTextTile.Location = new Point(79, 12);
            noteTextTile.Name = "noteTextTile";
            noteTextTile.Size = new Size(102, 23);
            noteTextTile.TabIndex = 1;
            noteTextTile.Text = "Update Meal";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(22, 12);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(30, 32);
            pictureBox5.TabIndex = 0;
            pictureBox5.TabStop = false;
            // 
            // TitlePriorityCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(noteBaggage);
            Name = "TitlePriorityCard";
            Size = new Size(597, 75);
            noteBaggage.ResumeLayout(false);
            noteBaggage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel noteBaggage;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel noteTextTile;
        private PictureBox pictureBox5;
    }
}
