using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_Promotion : UserControl
    {
        private readonly PromotionController _promotionController;

        // cache theo loại để dùng cho filter
        private Dictionary<string, List<PromotionDTO>> _groupedPromos = new Dictionary<string, List<PromotionDTO>>();

        // cache toàn bộ promo để search
        private List<PromotionDTO> _allPromosCache = new List<PromotionDTO>();

        public UC_Promotion()
        {
            InitializeComponent();

            // Lấy controller từ DI container
            _promotionController = DIContainer.PromotionController;
        }

        private void UC_Promotion_Load(object sender, EventArgs e)
        {
            LoadAllPromotions();
        }

        private void LoadAllPromotions()
        {
            // LẤY DỮ LIỆU QUA CONTROLLER, KHÔNG DÙNG _promotionService NỮA
            var allPromos = _promotionController.GetActivePromotions();

            if (allPromos == null || allPromos.Count == 0)
            {
                promotionContentPnl.Controls.Clear();
                return;
            }

            // lưu cache cho search
            _allPromosCache = allPromos;

            _groupedPromos = allPromos
                .GroupBy(p => p.PromotionType) // Special Campaigns, Flights, Others
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderByDescending(x => x.ValidFrom).ToList()
                );

            ShowAllPreview();   // mặc định là All, chỉ preview 3 cái / loại
        }

        private void ShowAllPreview()
        {
            promotionContentPnl.SuspendLayout();
            promotionContentPnl.Controls.Clear();

            string[] types = { "Special Campaigns", "Flights" };
            int top = 0, margin = 15;

            foreach (var type in types)
            {
                if (!_groupedPromos.TryGetValue(type, out var promos) || promos.Count == 0)
                    continue;

                var selector = new UC_PromotionSelector
                {
                    Width = promotionContentPnl.ClientSize.Width - 20,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Location = new Point(10, top)
                };

                selector.Bind(
                    type,
                    promos.Take(3).ToList(),      // chỉ 3 cái khi All
                    showFilter: false,
                    showSeeMore: true
                );

                selector.SeeMoreClicked += Selector_SeeMoreClicked; // đăng ký event See More

                promotionContentPnl.Controls.Add(selector);
                top += selector.Height + margin;
            }

            promotionContentPnl.ResumeLayout();
        }

        private void ShowSingleType(string type)
        {
            promotionContentPnl.SuspendLayout();
            promotionContentPnl.Controls.Clear();

            if (_groupedPromos.TryGetValue(type, out var promos) && promos.Count > 0)
            {
                var selector = new UC_PromotionSelector
                {

                    Dock = DockStyle.Fill
                };

                selector.Bind(
                    type,
                    promos,                 // TOÀN BỘ list
                    showFilter: true,
                    showSeeMore: false
                );

                promotionContentPnl.Controls.Add(selector);
            }

            promotionContentPnl.ResumeLayout();
        }

        private void Selector_SeeMoreClicked(string type)
        {
            ShowSingleType(type);
        }


        private void btnAll_Click(object sender, EventArgs e)
        {
            ShowAllPreview();
        }

        private void btnSpecialCampaigns_Click(object sender, EventArgs e)
        {
            ShowSingleType("Special Campaigns");
        }

        private void btnFlights_Click(object sender, EventArgs e)
        {
            ShowSingleType("Flights");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearch(txtSearchPromo.Text);
        }

        // Tìm theo PromoName (proNameLbl), không gọi DB lại
        private void ApplySearch(string keyword)
        {
            keyword = (keyword ?? string.Empty).Trim();

            // Nếu rỗng -> quay lại chế độ All mặc định
            if (string.IsNullOrWhiteSpace(keyword))
            {
                ShowAllPreview();
                return;
            }

            // Lọc trên cache, so khớp không phân biệt hoa/thường
            var filtered = _allPromosCache
                .Where(p => p.PromoName != null &&
                            p.PromoName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            promotionContentPnl.SuspendLayout();
            promotionContentPnl.Controls.Clear();

            if (filtered.Count > 0)
            {
                // Dùng lại UC_PromotionSelector để hiển thị kết quả tìm kiếm
                var selector = new UC_PromotionSelector
                {
                    Dock = DockStyle.Fill
                };

                selector.Bind(
                    "Search results",   // tiêu đề
                    filtered,           // full kết quả
                    showFilter: false,  // kết quả search không cần filter/see more
                    showSeeMore: false
                );

                promotionContentPnl.Controls.Add(selector);
            }

            promotionContentPnl.ResumeLayout();
        }
    }
}
