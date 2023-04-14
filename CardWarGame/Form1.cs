using CardLib;
using GraphicsInfrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardWarGame
{
    public partial class Form1 : Form
    {
        GameLogic game;
        List<GraphicsCardSet> sets = new();
        List<Player> players = new();
        GraphicsStore store;
        GraphicsCardSet table;
        Card activeCard;
        public Form1()
        {
            InitializeComponent();
            players.Add(new Player("Artem "));
            for (int i = 1; i < 4; i++)
            {
                players.Add(new Player($"Noname{i} "));
            }
            game = new GameLogic(players, showState, showInfo);
            store = new(game.Deck, this);


            sets.Add(new GraphicsCardSet(game.Deck, new Rectangle(pDeck.Location, pDeck.Size), store));
            sets.Add(new GraphicsCardSet(game.players[0].Hand, new Rectangle(pPl1.Location, pPl1.Size), store));
            sets.Add(new GraphicsCardSet(game.players[1].Hand, new Rectangle(pPl2.Location, pPl2.Size), store));
            sets.Add(new GraphicsCardSet(game.players[2].Hand, new Rectangle(pPl3.Location, pPl3.Size), store));
            sets.Add(new GraphicsCardSet(game.players[3].Hand, new Rectangle(pPl4.Location, pPl4.Size), store));
            table = new GraphicsCardSet(game.TableDeck, new Rectangle(pTable.Location, pTable.Size), store);

            sets.Add(table);
            BindEvents();
            game.Start();
            Update();
        }

        private void showInfo()
        {
            MessageBox.Show(game.Winner.Name + "win!");
        }

        private void BindEvents()
        {
            foreach (var card in game.Deck)
            {
                var pb = store.GetPictureBox(card);
                pb.MouseDown += SelectCard;
                pb.MouseMove += CardMoving;
                pb.MouseUp += Turn;
            }
        }

        private void Turn(object sender, MouseEventArgs e)
        {
            if (activeCard == null) return;
            if (e.Button != MouseButtons.Left) return;

            Rectangle r1 = new(pActive.Location, pActive.Size);
            Rectangle r2 = table.Frame;

            if (r1.IntersectsWith(r2))
                game.Turn(activeCard);

            activeCard = null;
            pActive.Hide();
        }

        private void CardMoving(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (activeCard == null) return;

            pActive.Location = new Point(Cursor.Position.X - this.Location.X - pActive.Width,
                Cursor.Position.Y - this.Location.Y - pActive.Height);
            pActive.BringToFront();
        }

        private void SelectCard(object sender, MouseEventArgs e)
        {
            var pb = sender as PictureBox;
            if (pb == null) return;

            pb.BringToFront();
            activeCard = store.GetCard(pb);

            pActive.Location = new Point(Cursor.Position.X - this.Location.X - pActive.Width,
                Cursor.Position.Y - this.Location.Y - pActive.Height);

            pActive.Image = pb.Image;
            pActive.Show();
        }

        private void showState()
        {
            foreach (var set in sets)
            {
                set.Draw(game.TableDeck == set.CardSet);
            }
        }

        private void showInfo(string obj)
        {
            throw new NotImplementedException();
        }
    }
}
