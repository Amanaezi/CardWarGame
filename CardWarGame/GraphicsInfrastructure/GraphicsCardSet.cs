using CardLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsInfrastructure
{
    public class GraphicsCardSet
    {
        public CardSet CardSet { get; }
        public Rectangle Frame { get; set; }

        private GraphicsStore cardStore;

        public GraphicsCardSet(CardSet cardSet, Rectangle frame, GraphicsStore cardStore)
        {
            Frame = frame;
            CardSet = cardSet;
            this.cardStore = cardStore;
        }

        public void Draw(Predicate<Card> opened)
        {
            if (CardSet.Count == 0) return;
            int h = Frame.Height;
            Image Sample = GraphicsStore.FaceDownImage;

            int w = h * Sample.Width / Sample.Height;
            int d = (Frame.Width - w) / CardSet.Count;

            int x0 = Frame.X;
            int y0 = Frame.Y;

            for(int i = 0; i < CardSet.Count; i++)
            {
                var card = CardSet[i];
                var pb = cardStore.GetPictureBox(card, opened(card));

                pb.Size = new Size(w, h);
                pb.Location = new Point(x0 + i * d, y0);
                pb.BringToFront();
            }
        }

        public void Draw(bool opened)
        {
            Draw(c => opened);
        }
    }
}
