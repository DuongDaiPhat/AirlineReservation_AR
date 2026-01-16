using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class RescheduleQuoteResultDto
    {
        // True nếu tính toán thành công, False nếu có lỗi logic (vd: hết ghế, rule sai)
        public bool IsSuccess { get; set; }

        // Thông báo lỗi nếu IsSuccess = false
        public string? ErrorMessage { get; set; }

        // Giá vé gốc
        public decimal OriginalBasePrice { get; set; }

        // Giá vé chuyến mới
        public decimal NewBasePrice { get; set; }

        // Chênh lệch giá (New - Original). Nếu âm có thể set về 0 tùy nghiệp vụ.
        public decimal FareDifference { get; set; }

        // Phí phạt đổi vé (Lấy từ FareRule.ChangeFee)
        public decimal PenaltyFee { get; set; }

        // Tổng tiền phải thanh toán thêm = FareDifference + PenaltyFee
        public decimal TotalAmount { get; set; }

        // Đơn vị tiền tệ (VND/USD)
        public string Currency { get; set; } = "VND";

        // Mã quy định giá vé (hiển thị để user biết vé mới thuộc hạng vé nào)
        public string? FareCode { get; set; }
    }
}
