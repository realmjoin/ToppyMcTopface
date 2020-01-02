using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToppyMcTopface
{
    internal class TickerLabel : Label
    {
        private readonly Timer timer = new Timer();

        private long last;
        private float offset;
        private SolidBrush backBrush;
        private SolidBrush textBrush;
        private int fps;

        public TickerLabel()
        {
            timer.Tick += TimerTick;

            Fps = 60;
            Speed = 40;
            TickerEnabled = true;
        }

        /// <summary>
        /// Speed in px/second.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Render at n frames/second.
        /// </summary>
        public int Fps
        {
            get => fps;
            set
            {
                fps = value;
                timer.Interval = (int)(1000f / value);
            }
        }

        public bool TickerEnabled
        {
            get => timer.Enabled;
            set
            {
                timer.Enabled = value;
                last = 0;
            }
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                backBrush?.Dispose();
                backBrush = new SolidBrush(value);
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                textBrush?.Dispose();
                textBrush = new SolidBrush(value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                timer?.Dispose();
                backBrush?.Dispose();
                textBrush?.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (backBrush != null)
                e.Graphics.FillRectangle(backBrush, e.ClipRectangle);

            var (width, count) = Measure(e.Graphics);

            for (var i = 0; i < count; i++)
            {
                e.Graphics.DrawString(Text, Font, textBrush, i * width + offset, 0);
            }
        }

        private void TimerTick(object sender, EventArgs args)
        {
            float width;

            using (var g = CreateGraphics())
            {
                (width, _) = Measure(g);
            }

            var curr = DateTime.UtcNow.Ticks;

            if (last == 0)
            {
                last = curr - (timer.Interval * TimeSpan.TicksPerMillisecond);
            }

            float diff = curr - last;
            float rtio = diff / TimeSpan.TicksPerSecond;
            float dist = Speed * rtio;

            offset -= dist;

            if (offset < -width)
                offset = 0;

            last = curr;

            Invalidate();
        }

        private (float width, int count) Measure(Graphics g)
        {
            var size = g.MeasureString(Text, Font);
            var count = 1 + (int)Math.Ceiling(ClientSize.Width / size.Width);

            return (size.Width, count);
        }
    }
}
