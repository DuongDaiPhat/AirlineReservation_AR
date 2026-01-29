using System;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using System;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.popup
{
    public partial class AdjustSearchParamsForm : Form
    {
        private readonly SeatClassController _seatClassController;
        private FlightSearchParams _original;
        private int adult;
        private int child;
        private int infant;

        public FlightSearchParams UpdatedParams { get; private set; }

        public AdjustSearchParamsForm(FlightSearchParams p)
        {
            InitializeComponent();

            _original = p;
            _seatClassController = DIContainer.SeatClassController;

            this.FormBorderStyle = FormBorderStyle.None;
            this.Load += AdjustSearchParamsForm_Load;
        }

        private async void AdjustSearchParamsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Load danh sách hạng ghế
                var seatClasses = await _seatClassController.GetAll();

                cboSeatClass.DataSource = seatClasses;
                cboSeatClass.DisplayMember = "DisplayName";
                cboSeatClass.ValueMember = "SeatClassId";

                // Set giá trị ban đầu từ _original
                if (_original.SeatClassId > 0)
                {
                    cboSeatClass.SelectedValue = _original.SeatClassId;
                }

                // Khởi tạo số lượng hành khách
                adult = _original.Adult > 0 ? _original.Adult : 1;
                child = _original.Child >= 0 ? _original.Child : 0;
                infant = _original.Infant >= 0 ? _original.Infant : 0;

                // Hiển thị giá trị
                UpdatePassengerDisplay();

                // Gắn sự kiện cho các nút
                AttachEventHandlers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void AttachEventHandlers()
        {
            // Nút Close ở header
            btnClose.Click += BtnClose_Click;

            // Người lớn
            btnAdultPlus.Click += (s, ev) =>
            {
                if (adult < 9) // Giới hạn tối đa 9 người lớn
                {
                    adult++;
                    UpdatePassengerDisplay();
                }
            };

            btnAdultMinus.Click += (s, ev) =>
            {
                if (adult > 1) // Tối thiểu 1 người lớn
                {
                    adult--;
                    UpdatePassengerDisplay();
                }
            };

            // Trẻ em
            btnChildPlus.Click += (s, ev) =>
            {
                if (child < 9) // Giới hạn tối đa 9 trẻ em
                {
                    child++;
                    UpdatePassengerDisplay();
                }
            };

            btnChildMinus.Click += (s, ev) =>
            {
                if (child > 0)
                {
                    child--;
                    UpdatePassengerDisplay();
                }
            };

            // Em bé
            btnInfantPlus.Click += (s, ev) =>
            {
                if (infant < adult) // Số em bé không vượt quá số người lớn
                {
                    infant++;
                    UpdatePassengerDisplay();
                }
                else
                {
                    AnnouncementForm form = new AnnouncementForm();
                    form.SetAnnouncement("Invalid Input", "The number of infant must be less than adult", false, null);
                    form.Show();
                    form.BringToFront();
                }
            };

            btnInfantMinus.Click += (s, ev) =>
            {
                if (infant > 0)
                {
                    infant--;
                    UpdatePassengerDisplay();
                }
            };

            // Nút hủy và lưu
            btnCancel.Click += BtnCancel_Click;
            btnDone.Click += BtnDone_Click;
        }

        private void UpdatePassengerDisplay()
        {
            txtAdult.Text = adult.ToString();
            txtChild.Text = child.ToString();
            txtInfant.Text = infant.ToString();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboSeatClass.SelectedValue == null)
                {
                    AnnouncementForm form = new AnnouncementForm();
                    form.SetAnnouncement("Announcement", "Please select seat class", false, null);
                    form.Show();
                    form.BringToFront();
                    return;
                }

                if (adult < 1)
                {
                    AnnouncementForm form = new AnnouncementForm();
                    form.SetAnnouncement("Invalid Input", "Require at least one adult", false, null);
                    form.Show();
                    form.BringToFront();
                    return;
                }

                //  Tổng người (chỉ tính người lớn + trẻ em)
                int totalPeople = adult + child;
                if (totalPeople > 7)
                {
                    AnnouncementForm form = new AnnouncementForm();
                    form.SetAnnouncement("Invalid Input", "The number of passenger must be less than 9", false, null);
                    form.Show();
                    form.BringToFront();
                    return;
                }

                //  Mỗi người lớn chỉ được kèm 1 em bé
                if (infant > adult)
                {
                    AnnouncementForm announcementForm = new AnnouncementForm();
                    announcementForm.SetAnnouncement("Thông báo", "Số em bé không được vượt quá số người lớn!", false, null);
                    announcementForm.Show();
                    return;
                }

                // Tạo object mới
                UpdatedParams = new FlightSearchParams
                {
                    FromCode = _original.FromCode,
                    ToCode = _original.ToCode,
                    StartDate = _original.StartDate,
                    SeatClassId = (int)cboSeatClass.SelectedValue,
                    Adult = adult,
                    Child = child,
                    Infant = infant
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Error", "Save data error", false, null);
                form.Show();
                form.BringToFront();
            }
        }

    }
}