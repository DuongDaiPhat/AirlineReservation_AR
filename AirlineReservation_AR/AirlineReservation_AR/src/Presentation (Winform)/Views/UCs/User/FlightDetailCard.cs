using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class FlightDetailCard : UserControl
    {
        public FlightDetailCard()
        {
            InitializeComponent();

        }
    

    public void setData(FlightResultDTO flight)
        {
            FromHours.Text = flight.DepartureTime.ToString(@"hh\:mm");
            ToHours.Text = flight.ArrivalTime.ToString(@"hh\:mm");
            FromCity.Text = flight.FromAirportName;
            ToCity.Text = flight.ToAirportName;
            ToCity.RightToLeft = RightToLeft.No;
            FromCode.Text = flight.FromAirportCode;

            ToCode.Text = flight.ToAirportCode;
            ToCode.RightToLeft = RightToLeft.No;
            FromDate.Text = flight.FlightDate.ToString("dd/MM/yyyy");
            
            ToDate.Text = flight.FlightDate.AddMinutes(flight.DurationMinutes).ToString("dd/MM/yyyy");
            ToCode.RightToLeft = RightToLeft.No;
            Prices.Text = string.Format("{0:N0} VND", flight.Price);
        }
    }

   }
