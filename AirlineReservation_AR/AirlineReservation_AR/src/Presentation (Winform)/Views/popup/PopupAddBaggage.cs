using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.popup
{
    public partial class PopupAddBaggage : Form
    {
        private FlightResultDTO _flight;
        private int _passengerCount;

        public List<PassengerBaggageDTO> SelectedBaggage { get; private set; }
            = new List<PassengerBaggageDTO>();

        private List<BaggageDTO> _baggageOptions = new List<BaggageDTO>()
        {
            new BaggageDTO { Weight="0kg",  Price=0 },
            new BaggageDTO { Weight="20kg", Price=845000 },
            new BaggageDTO { Weight="30kg", Price=1124000 },
            new BaggageDTO { Weight="40kg", Price=1399000 }
        };

        public PopupAddBaggage(FlightResultDTO flight, int passengerCount)
        {
            _flight = flight;
            _passengerCount = passengerCount;

            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            lblFlightRoute.Text =
                $"{_flight.FromAirportCode} → {_flight.ToAirportCode} ({_flight.AirlineName})";

            LoadFlightSection();
            LoadPassengerSection();
            UpdateTotalPrice();
        }

        // LEFT SIDE
        private void LoadFlightSection()
        {
            flowFlightList.Controls.Clear();

            var panel = new Panel()
            {
                Height = 80,
                Width = 330,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            var lbl = new Label()
            {
                Text = $"{_flight.FromAirportCode} → {_flight.ToAirportCode}\n" +
                       $"{_flight.FlightDate:dd/MM/yyyy}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                AutoSize = true
            };

            panel.Controls.Add(lbl);
            flowFlightList.Controls.Add(panel);
        }

        // RIGHT SIDE
        private void LoadPassengerSection()
        {
            flowPassengerList.Controls.Clear();

            for (int i = 1; i <= _passengerCount; i++)
            {
                var panel = CreatePassengerBaggagePanel(i);
                flowPassengerList.Controls.Add(panel);
            }
        }

        private Panel CreatePassengerBaggagePanel(int index)
        {
            var panel = new Panel()
            {
                Width = 520,
                Height = 180,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 20)
            };

            var lblTitle = new Label()
            {
                Text = $"Hành khách {index}",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true
            };
            panel.Controls.Add(lblTitle);

            int y = 50;
            int optionIndex = 0;

            foreach (var opt in _baggageOptions)
            {
                var radio = new RadioButton()
                {
                    Text = $"{opt.Weight} – {opt.Price:N0} VND",
                    Tag = new { PassengerIndex = index, Baggage = opt },
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(10, y)
                };
                radio.CheckedChanged += radio_CheckedChanged;

                panel.Controls.Add(radio);

                y += 35;
                optionIndex++;
            }

            return panel;
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            decimal total = 0;

            SelectedBaggage.Clear();

            foreach (Panel panel in flowPassengerList.Controls)
            {
                foreach (Control ctrl in panel.Controls)
                {
                    if (ctrl is RadioButton radio && radio.Checked)
                    {
                        var data = (dynamic)radio.Tag;

                        var bg = data.Baggage as BaggageDTO;

                        SelectedBaggage.Add(new PassengerBaggageDTO
                        {
                            PassengerIndex = data.PassengerIndex,
                            Weight = bg.Weight,
                            Price = bg.Price
                        });

                        total += bg.Price;
                    }
                }
            }

            lblTotalPrice.Text = $"Tổng phụ: {total:N0} VND";
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
