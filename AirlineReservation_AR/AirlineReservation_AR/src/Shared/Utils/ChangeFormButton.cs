using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Shared.Utils
{
    internal class ChangeFormButton
    {
        public static void ChangeForm(System.Windows.Forms.Form currentForm, System.Windows.Forms.Form newForm)
        {
            currentForm.Hide();
            newForm.Closed += (s, args) => currentForm.Close();
            newForm.Show();
        }
    }
}
