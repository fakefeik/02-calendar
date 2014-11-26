using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar
{
    public partial class Form1 : Form
    {
        private string[] days = { "#", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        private DateTime date;

        public Form1(string date)
        {
            this.date = DateTime.Parse(date);
            InitializeComponent();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var firstDayOfYear = new DateTime(date.Year, 1, 1);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DrawMonth(graphics);
            DrawWeeks(graphics);
            DrawWeekNumber(graphics);
            DrawDays(graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        private void DrawMonth(Graphics graphics)
        {
            graphics.DrawString(months[date.Month - 1] + " " + date.Year, new Font("Arial", 16), Brushes.Black, Width / 2, 16, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private void DrawWeeks(Graphics graphics)
        {
            for (int i = 0; i < days.Length; i++)
                graphics.DrawString(days[i], new Font("Arial", 16), Brushes.Black, i * 48 + 24, 48, 
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private void DrawWeekNumber(Graphics graphics)
        {
            for (int i = 0; i < 7; i++)
                graphics.DrawString(i.ToString(), new Font("Arial", 16), Brushes.Black, 24, i * 48 + 72,
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private void DrawDays(Graphics graphics)
        {
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    graphics.DrawString("0", new Font("Arial", 16), Brushes.Black, i * 48 + 72, j * 48 + 72, 
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
        }
    }
}
