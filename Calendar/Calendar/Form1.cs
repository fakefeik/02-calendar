using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Calendar
{
    public partial class Form1 : Form
    {
        private string[] days = { "#", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        private DateTime date;
        private int scale = 16;

        public Form1(string date)
        {
            this.date = DateTime.Parse(date);
            this.date = DateTime.Now;
            Width = 400;
            Height = 340;
            InitializeComponent();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var firstDayOfYear = new DateTime(date.Year, 1, 1);
            var offset = ((int)firstDayOfMonth.DayOfWeek + 6) % 7;
            var daysCount = DateTime.DaysInMonth(date.Year, date.Month);
            var count = (daysCount + offset - 1) / 7 + 1;
            var weekNumber = ((int)(firstDayOfMonth.DayOfYear - firstDayOfYear.DayOfWeek) / 7) % 52 + 1;

            DrawMonth(graphics);
            DrawWeeks(graphics);
            DrawWeekNumber(graphics, weekNumber, count);
            DrawDays(graphics, offset, daysCount);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            scale = Math.Min(Width, Height) * 16 / 400;
            Invalidate();
        }

        private void DrawMonth(Graphics graphics)
        {
            graphics.DrawString(months[date.Month - 1] + " " + date.Year, new Font("Arial", scale), Brushes.Black, Width / 2, scale, 
                new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private void DrawWeeks(Graphics graphics)
        {
            for (int i = 0; i < days.Length; i++)
                graphics.DrawString(days[i], new Font("Arial", scale), Brushes.Black, i * scale * 3 + (float)(scale * 1.5), scale * 3, 
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private void DrawWeekNumber(Graphics graphics, int weekNumber, int count)
        {
            

            for (int i = 0; i < count; i++)
                graphics.DrawString((weekNumber + i).ToString(), new Font("Arial", scale), Brushes.DodgerBlue, (float)(scale * 1.5), i * scale * 3 + (float)(scale*4.5),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private void DrawDays(Graphics graphics, int offset, int daysCount)
        {

            Console.WriteLine(offset);
            for (int i = 0; i < daysCount; i++)
            {
                graphics.DrawString((i + 1).ToString(), new Font("Arial", scale),
                    (i + offset)%7 == 6 ? Brushes.Red : Brushes.Black, ((i + offset)%7)*scale*3 + (float) (scale*4.5),
                    (i + offset)/7*scale*3 + (float) (scale*4.5),
                    new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center});
                Console.WriteLine((i + offset) / 7);
            }
        }
    }
}
