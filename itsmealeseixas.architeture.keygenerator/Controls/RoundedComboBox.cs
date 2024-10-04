using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.keygenerator.Controls
{
    public class RoundedComboBox:ComboBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, 10, 10, 180, 90);
                path.AddArc(Width - 10, 0, 10, 10, 270, 90);
                path.AddArc(Width - 10, Height - 10, 10, 10, 0, 90);
                path.AddArc(0, Height - 10, 10, 10, 90, 90);
                path.CloseFigure();
                this.Region = new Region(path);
            }
        }
    }
}
