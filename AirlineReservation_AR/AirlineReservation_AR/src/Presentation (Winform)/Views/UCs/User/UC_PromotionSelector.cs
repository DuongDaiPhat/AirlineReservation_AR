using AirlineReservation_AR.src.Domain.DTOs;
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
    public partial class UC_PromotionSelector : UserControl
    {
        private string _promotionType; // loại của selector hiện tại ("Special Campaigns", "Flights", ...)
        public event Action<string> SeeMoreClicked; // event bắn ra cho màn Promotion
        private List<PromotionDTO> _allPromos = new List<PromotionDTO>(); // lưu list hiện tại

        public UC_PromotionSelector()
        {
            InitializeComponent();
        }

        // Set dữ liệu cho selector
        public void Bind(string promotionType, List<PromotionDTO> promos, bool showFilter, bool showSeeMore)
        {
            _promotionType = promotionType; // lưu lại loại để bắn event
            typePromotionLbl.Text = promotionType; // "Special Campaigns" hoặc "Flights"

            // Đổi icon theo loại
            proTypePic.Image = GetTypeIcon(promotionType);

            // Hiển thị button theo loại
            filterBtn.Visible = showFilter;
            seeMoreBtn.Visible = showSeeMore;

            _allPromos = promos.ToList(); // lưu lại danh sách đầy đủ
            RenderPromos(_allPromos);

            // Hiển thị các mục khuyến mãi
            promoItemsFlowPnl.SuspendLayout();
            promoItemsFlowPnl.Controls.Clear();

            foreach (var promo in promos)
            {
                var item = new UC_PromotionItem();
                item.Margin = new Padding(10, 50, 10, 10);
                item.Width = 350;         // chỉnh cho vừa 3 card/1 hàng
                item.Height = 240;
                item.Bind(promo);

                promoItemsFlowPnl.Controls.Add(item);
            }

            promoItemsFlowPnl.ResumeLayout();
        }

        private void RenderPromos(List<PromotionDTO> promos)
        {
            promoItemsFlowPnl.SuspendLayout();
            promoItemsFlowPnl.Controls.Clear();

            foreach (var promo in promos)
            {
                var item = new UC_PromotionItem();
                item.Margin = new Padding(10, 50, 10, 10);
                item.Width = 350;         // chỉnh cho vừa 3 card/1 hàng
                item.Height = 240;
                item.Bind(promo);

                promoItemsFlowPnl.Controls.Add(item);
            }

            promoItemsFlowPnl.ResumeLayout();
        }

        private Image GetTypeIcon(string type)
        {
            switch (type)
            {
                case "Special Campaigns":
                    return Properties.Resources.megaphone;

                case "Flights":
                    return Properties.Resources.airplane;

                default:
                    return Properties.Resources.menu_dots;
            }
        }

        private void seeMoreBtn_Click(object sender, EventArgs e)
        {
            SeeMoreClicked?.Invoke(_promotionType);
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            filterMenu.Show(filterBtn, new Point(100, filterBtn.Height));
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenderPromos(_allPromos); // hiển thị lại tất cả
        }

        private void promoEndingSoonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Promo ending soon: sort ValidTo tăng dần
            var sorted = _allPromos
                .OrderBy(p => p.ValidTo)
                .ToList();

            RenderPromos(sorted);
        }

        private void promoEndingLaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Promo ending later: sort ValidTo giảm dần
            var sorted = _allPromos
                .OrderByDescending(p => p.ValidTo)
                .ToList();

            RenderPromos(sorted);
        }
    }
}
