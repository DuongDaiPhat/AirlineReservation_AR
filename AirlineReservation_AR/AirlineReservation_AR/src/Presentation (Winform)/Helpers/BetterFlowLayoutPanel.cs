using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Helpers
{
    public class BetterFlowLayoutPanel : FlowLayoutPanel
    {
        public BetterFlowLayoutPanel()
        {
            // Kích hoạt double buffering qua Reflection
            typeof(FlowLayoutPanel)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(this, true);

            this.ResizeRedraw = true;
        }
    }
}
