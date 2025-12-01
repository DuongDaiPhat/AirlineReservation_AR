using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AR_Winform.Presentation.UControls.User
{
    public partial class UC_FlightDate : UserControl
    {
        private DateTime currentMonth;
        private DateTime today = DateTime.Today;
        private List<Guna.UI2.WinForms.Guna2Button> dayButtons;
        public event EventHandler<DateTime> DaySelected;

        public UC_FlightDate()
        {
            InitializeComponent();
            layout.Dock = DockStyle.Fill;
            dayButtons = new List<Guna.UI2.WinForms.Guna2Button>
            {
                guna2Button1, guna2Button2, guna2Button3, guna2Button4, guna2Button5,
                guna2Button6, guna2Button7, guna2Button8, guna2Button9, guna2Button10,
                guna2Button11, guna2Button12, guna2Button13, guna2Button14, guna2Button15,
                guna2Button16, guna2Button17, guna2Button18, guna2Button19, guna2Button20,
                guna2Button21, guna2Button22, guna2Button23, guna2Button24, guna2Button25,
                guna2Button26, guna2Button27, guna2Button28, guna2Button29, guna2Button30,
                guna2Button31, guna2Button32, guna2Button33, guna2Button34, guna2Button35
            };

            currentMonth = DateTime.Today;
            RenderCalendar();
        }

        private void RenderCalendar()
        {
            dateLB.Text = currentMonth.ToString("MMMM yyyy");

            // Reset tất cả button
            foreach (var btn in dayButtons)
            {
                btn.Visible = false;
                btn.Enabled = false;
                btn.Text = "";
                btn.Tag = null;
                btn.Click -= Day_Click;
            }

            DateTime firstDay = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);

            // Xác định vị trí bắt đầu
            // DayOfWeek: Sunday=0, Monday=1, Tuesday=2, ..., Saturday=6
            // Layout: Mon(0), Tue(1), Wed(2), Thu(3), Fri(4), Sat(5), Sun(6)
            int dayOfWeek = (int)firstDay.DayOfWeek;
            int startColumn;

            if (dayOfWeek == 0) // Chủ nhật
                startColumn = 6; // Cột cuối cùng
            else
                startColumn = dayOfWeek - 1; // Monday=0, Tuesday=1, ...

            int index = startColumn;

            for (int day = 1; day <= daysInMonth; day++)
            {
                if (index >= 0 && index < dayButtons.Count)
                {
                    var btn = dayButtons[index];
                    DateTime date = new DateTime(currentMonth.Year, currentMonth.Month, day);

                    btn.Visible = true;
                    btn.Text = day.ToString();
                    btn.Tag = date;

                    // Disable ngày quá khứ
                    if (date < today)
                    {
                        btn.Enabled = false;
                        btn.FillColor = Color.LightGray;
                        btn.ForeColor = Color.DarkGray;
                    }
                    else
                    {
                        btn.Enabled = true;
                        btn.FillColor = Color.RoyalBlue;
                        btn.ForeColor = Color.White;
                    }

                    btn.Click += Day_Click;
                }
                index++;
            }
        }

        public void RefreshCalendar()
        {
            RenderCalendar();
        }

        private void Day_Click(object sender, EventArgs e)
        {
            var btn = sender as Guna.UI2.WinForms.Guna2Button;
            if (btn?.Tag is DateTime date)
            {
                DaySelected?.Invoke(this, date);
            }
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            // Không cho về quá khứ (tháng trước tháng hiện tại)
            DateTime previousMonth = currentMonth.AddMonths(-1);
            DateTime firstDayOfPreviousMonth = new DateTime(previousMonth.Year, previousMonth.Month, 1);

            if (firstDayOfPreviousMonth >= new DateTime(today.Year, today.Month, 1))
            {
                currentMonth = previousMonth;
                RenderCalendar();
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            currentMonth = currentMonth.AddMonths(1);
            RenderCalendar();
        }
    }
}