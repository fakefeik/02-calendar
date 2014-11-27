using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class Calendar
    {
        private string[] days = { "#", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        private readonly StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        private DateTime date;
        private int scale = 16;

        public Calendar(string date)
        {
            this.date = DateTime.Parse(date);
        }

        public void SaveToFile(string filename)
        {
            using (var bitmap = new Bitmap(400, 400))
            {
                var graphics = Graphics.FromImage(bitmap);

                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var firstDayOfYear = new DateTime(date.Year, 1, 1);
                var offset = ((int) firstDayOfMonth.DayOfWeek + 6)%7;
                var daysCount = DateTime.DaysInMonth(date.Year, date.Month);
                var count = (daysCount + offset - 1)/7 + 1;
                var weekNumber = ((int) (firstDayOfMonth.DayOfYear - firstDayOfYear.DayOfWeek)/7)%52 + 1;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                DrawMonth(graphics);
                DrawWeeks(graphics);
                DrawWeekNumber(graphics, weekNumber, count);
                DrawDays(graphics, offset, daysCount);
                bitmap.Save(filename);
            }
        }

        private void DrawMonth(Graphics graphics)
        {
            graphics.DrawString(months[date.Month - 1] + " " + date.Year, new Font("Arial", scale), Brushes.Black, 400 / 2, scale,
                centerFormat);
        }

        private void DrawWeeks(Graphics graphics)
        {
            for (int i = 0; i < days.Length; i++)
                graphics.DrawString(days[i], new Font("Arial", scale), Brushes.Black, i * scale * 3 + (float)(scale * 1.5), scale * 3,
                    centerFormat);
        }

        private void DrawWeekNumber(Graphics graphics, int weekNumber, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var number = weekNumber + i;
                if (number > 52)
                    number = number % 53 + 1;
                graphics.DrawString(number.ToString(), new Font("Arial", scale), Brushes.DodgerBlue,
                    (float)(scale * 1.5),
                    i * scale * 3 + (float)(scale * 4.5),
                    centerFormat);
            }
        }

        private void DrawDays(Graphics graphics, int offset, int daysCount)
        {
            for (int i = 0; i < daysCount; i++)
            {
                if (i == date.Day - 1)
                    graphics.FillEllipse(Brushes.CornflowerBlue,
                        ((i + offset) % 7) * scale * 3 + (float)(scale * 4.5) - scale,
                        (i + offset) / 7 * scale * 3 + (float)(scale * 4.5) - scale,
                        scale * 2, scale * 2);
                graphics.DrawString((i + 1).ToString(), new Font("Arial", scale),
                    (i + offset) % 7 == 6 ? Brushes.Red : Brushes.Black,
                    ((i + offset) % 7) * scale * 3 + (float)(scale * 4.5),
                    (i + offset) / 7 * scale * 3 + (float)(scale * 4.5),
                    centerFormat);
            }
        }
    }
}
