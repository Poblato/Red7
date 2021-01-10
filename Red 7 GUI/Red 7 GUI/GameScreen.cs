using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Red_7_GUI
{
    public partial class GameScreen : Form
    {
        private Client client;
        private List<Button> playerHand;
        private List<List<Button>> cards;
        private int cardHeight = 80;
        private int cardWidth = 60;
        private int top = 300;
        private int players;

        public GameScreen(int players, bool advanced, bool actionRule)
        {
            InitializeComponent();
            client = new Client(players, advanced, actionRule);
            cards = new List<List<Button>>();
            playerHand = new List<Button>();
            this.players = players;
            for (int i = 0; i < players; i++)
            {
                cards.Add(new List<Button>());
            }
            //client.Debug();

            Setup();
        }
        private void Setup()
        {
            RedrawHand();
            for (int i = 0; i < players; i++)
            {
                RedrawPalette(i);
            }
        }
        private void UpdateLabels()
        {
            canvas.Text = client.Canvas.GetName();
            deckCardsLabel.Text = "Cards Left: " + client.Deck.Size.ToString();
            opponent1HandButton.Text = "Hand: " + client.Hands[1].Size.ToString();
            opponent2HandButton.Text = "Hand: " + client.Hands[2].Size.ToString();
            opponent3HandButton.Text = "Hand: " + client.Hands[3].Size.ToString();
        }
        public void RedrawHand()
        {
            //MessageBox.Show("Redrawing hand");
            foreach (Button b in playerHand)
            {
                b.Dispose();
            }
            playerHand.Clear();

            for (int i = 0; i < client.Hands[0].Size; i++)
            {
                Card card = client.Hands[0].GetCard(i);
                playerHand.Add(new Button()
                {
                    Size = new Size(cardWidth, cardHeight),
                    Location = new Point((cardWidth + 15) * i + 60, 175),
                    BackColor = Color.LightGray,
                    ForeColor = Color.Black,
                    Text = card.GetName(),
                });
                // add to Form's Controls so that they show up
                playerHand[i].MouseDown += new MouseEventHandler(handCardClick);
                // 
                Controls.Add(playerHand[i]);
                playerHand[i].BringToFront();
            }
            UpdateLabels();
        }
        public void RedrawPalette(int player)
        {
            //MessageBox.Show("Redrawing palette " + player.ToString());
            Card card;
            foreach (Button b in cards[player])
            {
                b.Dispose();
            }
            cards[player].Clear();

            if (player == 0)
            {
                for (int i = 0; i < client.Palettes[player].Size; i++)
                {
                    card = client.Palettes[player].GetCard(i);
                    cards[player].Add(new Button()
                    {
                        Size = new Size(cardWidth, cardHeight),
                        Location = new Point((cardWidth + 15) * i + 60, top),
                        BackColor = Color.LightGray,
                        ForeColor = Color.Black,
                        Text = card.GetName(),
                    });
                    // add to Form's Controls so that they show up
                    cards[player][i].MouseDown += new MouseEventHandler(paletteCardClick);
                    // 
                    Controls.Add(cards[player][i]);
                    cards[player][i].BringToFront();
                }
            }
            else
            {
                for (int i = 0; i < client.Palettes[player].Size; i++)
                {
                    card = client.Palettes[player].GetCard(i);
                    cards[player].Add(new Button()
                    {
                        Size = new Size(cardWidth, cardHeight),
                        Location = new Point((cardWidth + 15) * i + 60, (player * 190) + 285),
                        BackColor = Color.LightGray,
                        ForeColor = Color.Black,
                        Text = card.GetName(),
                    });
                    // add to Form's Controls so that they show up
                    cards[player][i].MouseDown += new MouseEventHandler(paletteCardClick);
                    // 
                    Controls.Add(cards[player][i]);
                    cards[player][i].BringToFront();
                }
            }
            UpdateLabels();
        }
        private void paletteCardClick(object sender, MouseEventArgs e)
        {
            int player = -1;
            int index = -1;

            for (int i = 0; i < cards.Count; i++)//finds index of card clicked
            {
                for (int x = 0; x < cards[i].Count; x++)
                {
                    if (sender.Equals(cards[i][x]))
                    {
                        player = i;
                        index = x;
                        break;
                    }
                }
            }

            if (index == -1 || player == -1)
            {
                throw new Exception("Clicked card not found");
            }

            if (client.GameState == 3)//if discarding from other palette
            {
                if (player == 0)
                {
                    MessageBox.Show("Cannot dicard your own card");
                }
                else if (cards[player].Count < cards[0].Count)
                {
                    MessageBox.Show("Cannot discard from a player with less cards than you");
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        client.DiscardPaletteCard(player, index, -1);
                        RedrawPalette(player);
                        UpdateLabels();
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        client.DiscardPaletteCard(player, index, -2);
                        RedrawPalette(player);
                        UpdateLabels();
                    }
                }
            }
            else if (client.GameState == 4)//if discarding from own palette
            {
                if (player != 0)
                {
                    MessageBox.Show("Cannot dicard another player's card");
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        client.DiscardPaletteCard(player, index, -1);
                        RedrawPalette(player);
                        UpdateLabels();
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        client.DiscardPaletteCard(player, index, -2);
                        RedrawPalette(player);
                        UpdateLabels();
                    }
                }
            }
        }
        private void handCardClick(object sender, MouseEventArgs e)
        {
            int index = -1;

            for (int i = 0; i < playerHand.Count; i++)//finds index of card clicked
            {
                if (sender.Equals(playerHand[i]))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new Exception("Clicked card not found");
            }

            if (e.Button == MouseButtons.Left)//play to palette
            {
                if (client.GameState == 0)
                {
                    client.PlayToPalette(0, index);
                    RedrawPalette(0);
                    RedrawHand();
                    Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)//discard to canvas
            {
                if (client.GameState == 0 || client.GameState == 1)
                {
                    client.DiscardToCanvas(0, index);
                    canvas.Text = client.Canvas.GetName();
                    RedrawHand();
                    Invalidate();
                }
            }
            MessageBox.Show(client.GameState.ToString());
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void undoButton_Click(object sender, EventArgs e)
        {
            client.Undo();
        }
    }
}
