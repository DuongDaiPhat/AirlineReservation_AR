using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin
{
    [DesignerCategory("UserControl")]
    [DefaultEvent("PageChanged")]
    public partial class PaginationControl : UserControl
    {
        #region Private Fields

        private int _currentPage = 1;
        private int _totalPages = 1;
        private int _maxVisiblePages = 5;
        private List<Button> _pageButtons = new List<Button>();

        #endregion

        #region Public Properties - Hiển thị trong Designer Properties
        [Category("Pagination")]
        [Description("Số trang hiện tại")]
        [DefaultValue(1)]
        [Browsable(true)]
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (value >= 1 && value <= _totalPages && value != _currentPage)
                {
                    _currentPage = value;
                    RenderPageButtons();
                    OnCurrentPageChanged();
                }
            }
        }
        [Category("Pagination")]
        [Description("Tổng số trang")]
        [DefaultValue(1)]
        [Browsable(true)]
        public int TotalPages
        {
            get => _totalPages;
            set
            {
                if (value >= 1 && value != _totalPages)
                {
                    _totalPages = value;

                    // Đảm bảo CurrentPage không vượt quá TotalPages
                    if (_currentPage > _totalPages)
                    {
                        _currentPage = _totalPages;
                    }

                    RenderPageButtons();
                }
            }
        }
        [Category("Pagination")]
        [Description("Số button trang tối đa hiển thị (3-10)")]
        [DefaultValue(5)]
        [Browsable(true)]
        public int MaxVisiblePages
        {
            get => _maxVisiblePages;
            set
            {
                if (value >= 3 && value <= 10 && value != _maxVisiblePages)
                {
                    _maxVisiblePages = value;
                    RenderPageButtons();
                }
            }
        }
        [Category("Pagination Colors")]
        [Description("Màu nền của button trang đang active")]
        [Browsable(true)]

        public Color ActiveButtonColor { get; set; } = Color.FromArgb(255, 105, 180);
        [Category("Pagination Colors")]
        [Description("Màu nền của button trang không active")]
        [Browsable(true)]
        public Color InactiveButtonColor { get; set; } = Color.White;
        [Category("Pagination Colors")]
        [Description("Màu chữ của button trang đang active")]
        [Browsable(true)]
        public Color ActiveTextColor { get; set; } = Color.White;
        [Category("Pagination Colors")]
        [Description("Màu chữ của button trang không active")]
        [Browsable(true)]
        public Color InactiveTextColor { get; set; } = Color.FromArgb(120, 120, 120);
        [Category("Pagination Colors")]
        [Description("Màu viền của button trang đang active")]
        [Browsable(true)]
        public Color ActiveBorderColor { get; set; } = Color.FromArgb(255, 105, 180);
        [Category("Pagination Colors")]
        [Description("Màu viền của button trang không active")]
        [Browsable(true)]
        public Color InactiveBorderColor { get; set; } = Color.FromArgb(220, 220, 220);
        [Category("Pagination Colors")]
        [Description("Màu nền khi hover button")]
        [Browsable(true)]
        public Color HoverButtonColor { get; set; } = Color.FromArgb(255, 200, 220);
        #endregion
        #region Events
        [Category("Action")]
        [Description("Sự kiện xảy ra khi người dùng chuyển trang")]
        public event EventHandler<int> PageChanged;
        protected virtual void OnPageChanged(int pageNumber)
        {
            PageChanged?.Invoke(this, pageNumber);
        }
        [Category("Property Changed")]
        [Description("Sự kiện xảy ra khi CurrentPage thay đổi")]
        public event EventHandler CurrentPageChanged;

        protected virtual void OnCurrentPageChanged()
        {
            CurrentPageChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Constructor

        public PaginationControl()
        {
            InitializeComponent();
            InitializeCustomControls();
        }

        #endregion

        #region Initialization
        private void InitializeCustomControls()
        {
            if (btnPrevious != null)
            {
                btnPrevious.Click += BtnPrevious_Click;
            }

            if (btnNext != null)
            {
                btnNext.Click += BtnNext_Click;
            }

            // Render lần đầu
            RenderPageButtons();
        }
        #endregion
        #region Event Handlers

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                CurrentPage = _currentPage - 1;
                OnPageChanged(_currentPage);
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                CurrentPage = _currentPage + 1;
                OnPageChanged(_currentPage);
            }
        }
        private void PageButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int pageNumber)
            {
                if (pageNumber != _currentPage)
                {
                    CurrentPage = pageNumber;
                    OnPageChanged(pageNumber);
                }
            }
        }
        #endregion
        #region Rendering Methods
        private void RenderPageButtons()
        {
            if (panelContainer == null || _totalPages < 1)
                return;

            foreach (var btn in _pageButtons)
            {
                panelContainer.Controls.Remove(btn);
                btn.Click -= PageButton_Click;
                btn.Dispose();
            }
            _pageButtons.Clear();

            List<int?> pagesToShow = CalculatePagesToShow();

            int totalButtonsWidth = pagesToShow.Count * 60;
            int centerX = this.Width / 2;
            int startX = centerX - (totalButtonsWidth / 2);

            int index = 0;
            foreach (var pageNum in pagesToShow)
            {
                int xPos = startX + (index * 60);

                if (pageNum.HasValue)
                {
                    // Tạo button số trang
                    var btnPage = CreatePageButton(pageNum.Value, xPos);
                    _pageButtons.Add(btnPage);
                    panelContainer.Controls.Add(btnPage);
                }
                else
                {
                    var lblEllipsis = CreateEllipsisLabel(xPos);
                    _pageButtons.Add(lblEllipsis);
                    panelContainer.Controls.Add(lblEllipsis);
                }

                index++;
            }

            UpdateNavigationButtons();
        }

        private List<int?> CalculatePagesToShow()
        {
            var pages = new List<int?>();

            if (_totalPages <= _maxVisiblePages)
            {
                for (int i = 1; i <= _totalPages; i++)
                {
                    pages.Add(i);
                }
                return pages;
            }

            int leftSide = (_maxVisiblePages - 3) / 2; // Số trang bên trái current
            int rightSide = _maxVisiblePages - 3 - leftSide; // Số trang bên phải current

            pages.Add(1);

            int startPage = Math.Max(2, _currentPage - leftSide);
            int endPage = Math.Min(_totalPages - 1, _currentPage + rightSide);

            if (_currentPage <= leftSide + 2)
            {
                endPage = Math.Min(_totalPages - 1, _maxVisiblePages - 1);
                startPage = 2;
            }
            else if (_currentPage >= _totalPages - rightSide - 1)
            {
                startPage = Math.Max(2, _totalPages - _maxVisiblePages + 2);
                endPage = _totalPages - 1;
            }
            if (startPage > 2)
            {
                pages.Add(null); // ellipsis
            }

            for (int i = startPage; i <= endPage; i++)
            {
                pages.Add(i);
            }

            if (endPage < _totalPages - 1)
            {
                pages.Add(null); // ellipsis
            }

            if (_totalPages > 1)
            {
                pages.Add(_totalPages);
            }

            return pages;
        }

        private Button CreateEllipsisLabel(int xPos)
        {
            var btn = new Button
            {
                Text = "...",
                Size = new Size(55, 55),
                Location = new Point(xPos, 7),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 180, 180),
                BackColor = Color.White,
                Enabled = false, // Không thể click
                TabStop = false
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.White;

            return btn;
        }
        private Button CreatePageButton(int pageNumber, int xPos)
        {
            bool isActive = pageNumber == _currentPage;

            var btn = new Button
            {
                Text = pageNumber.ToString(),
                Size = new Size(55, 55),
                Location = new Point(xPos, 7),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = isActive ? ActiveTextColor : InactiveTextColor,
                BackColor = isActive ? ActiveButtonColor : InactiveButtonColor,
                Cursor = Cursors.Hand,
                Tag = pageNumber,
                TabStop = false
            };

            btn.FlatAppearance.BorderColor = isActive ? ActiveBorderColor : InactiveBorderColor;
            btn.FlatAppearance.BorderSize = 2;

            if (!isActive)
            {
                btn.MouseEnter += (s, e) =>
                {
                    btn.BackColor = HoverButtonColor;
                    btn.ForeColor = ActiveTextColor;
                };

                btn.MouseLeave += (s, e) =>
                {
                    btn.BackColor = InactiveButtonColor;
                    btn.ForeColor = InactiveTextColor;
                };
            }

            btn.Click += PageButton_Click;

            return btn;
        }
        private void UpdateNavigationButtons()
        {
            if (btnPrevious != null)
            {
                btnPrevious.Enabled = _currentPage > 1;
                btnPrevious.ForeColor = btnPrevious.Enabled
                    ? Color.FromArgb(100, 100, 100)
                    : Color.FromArgb(220, 220, 220);
            }

            if (btnNext != null)
            {
                btnNext.Enabled = _currentPage < _totalPages;
                btnNext.ForeColor = btnNext.Enabled
                    ? Color.FromArgb(100, 100, 100)
                    : Color.FromArgb(220, 220, 220);
            }
        }
        #endregion
        #region Override Methods
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RenderPageButtons();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Render lại khi load (cho trường hợp design time)
            if (!DesignMode)
            {
                RenderPageButtons();
            }
        }
        #endregion
        #region Public Methods
        public void GoToFirstPage()
        {
            if (_currentPage != 1)
            {
                CurrentPage = 1;
                OnPageChanged(1);
            }
        }
        public void GoToLastPage()
        {
            if (_currentPage != _totalPages)
            {
                CurrentPage = _totalPages;
                OnPageChanged(_totalPages);
            }
        }
        public void RefreshDisplay()
        {
            RenderPageButtons();
        }

        #endregion
    }
}
