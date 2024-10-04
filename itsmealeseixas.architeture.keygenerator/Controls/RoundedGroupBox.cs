using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.keygenerator.Controls
{
    public class RoundedGroupBox : GroupBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Definir retângulo e borda arredondada
            Rectangle rect = new Rectangle(0, 10, this.Width - 1, this.Height - 10);
            int cornerRadius = 20; // Ajuste conforme necessário

            using (GraphicsPath path = new GraphicsPath())
            {
                // Desenha os cantos arredondados
                path.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                path.CloseAllFigures();

                // Preenche o fundo e desenha a borda
                e.Graphics.FillPath(new SolidBrush(this.BackColor), path);
                e.Graphics.DrawPath(Pens.Gray, path);
            }

            // Desenhar o texto do GroupBox na parte superior
            SizeF stringSize = e.Graphics.MeasureString(this.Text, this.Font);
            Rectangle textRect = new Rectangle(10, 0, (int)stringSize.Width, (int)stringSize.Height);
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), textRect);
            e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, textRect);
        }
    }
}
