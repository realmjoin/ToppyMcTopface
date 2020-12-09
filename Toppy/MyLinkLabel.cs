using System.Drawing;
using System.Windows.Forms;

namespace ToppyMcTopface
{
    public class MyLinkLabel : LinkLabel
    {
        public Link ScreenPointInLink(int x, int y)
        {
            var point = PointToClient(new Point(x, y));
            return PointInLink(point.X, point.Y);
        }
    }
}
