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
        private Button[] playerHand;
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
            playerHand = new Button[7];
            this.players = players;
            for(int i = 0; i < players; i++)
            {
                cards.Add(new List<Button>());
            }

            Setup();
        }
        private void Setup()
        {
            for (int x = 0; x < players; x++)
            {
                CreatePaletteCard(client.Palettes[x].GetCard(0), x);
            }
            for (int i = 0; i < 7; i++)
            {
                CreateHandCard(i);
            }
            
            UpdateLabels();
            Invalidate();
        }
        private void UpdateLabels()
        {
            canvas.Text = client.Canvas.GetName();
            deckCardsLabel.Text = "Cards Left: " + client.Deck.Size.ToString();
            opponent1HandButton.Text = "Hand: " + client.Hands[1].Size.ToString();
            opponent2HandButton.Text = "Hand: " + client.Hands[2].Size.ToString();
            opponent3HandButton.Text = "Hand: " + client.Hands[3].Size.ToString();
        }
        private void CreateHandCard(int pos)
        {
            Card card = client.Hands[0].GetCard(pos);
            playerHand[pos] = new Button()
            {
                Size = new Size(cardWidth, cardHeight),
                Location = new Point((cardWidth + 15) * pos + 60, 175),
            };
            playerHand[pos].ForeColor = Color.Black;
            playerHand[pos].BackColor = Color.LightGray;
            playerHand[pos].Text = card.GetName();
            // add to Form's Controls so that they show up
            playerHand[pos].MouseDown += new MouseEventHandler(handCardClick);
            // 
            Controls.Add(playerHand[pos]);
            playerHand[pos].BringToFront();
        }
        private void CreatePaletteCard(Card card, int player)
        {
            int pos = client.Palettes[player].Size - 1;
            if (player == 0)
            {
                cards[player].Add(new Button()
                {
                    Size = new Size(cardWidth, cardHeight),
                    Location = new Point((cardWidth + 15) * pos + 60, top),
                });
            }
            else
            {
                cards[player].Add(new Button()
                {
                    Size = new Size(cardWidth, cardHeight),
                    Location = new Point((cardWidth + 15) * pos + 60, (player * 190) + 285),
                });
            }

            cards[player][pos].ForeColor = Color.Black;
            cards[player][pos].BackColor = Color.LightGray;
            cards[player][pos].Text = card.GetName();
            // add to Form's Controls so that they show up

            cards[player][pos].MouseDown += new MouseEventHandler(paletteCardClick);
            Controls.Add(cards[player][pos]);
            cards[player][pos].BringToFront();
        }
        private void MoveCard(int[] pos, int target)//pos is in the form [hand/palette, player, index] (appends to target
        {
            int index;
            if (pos[0] == 0)//move from hand
            {
                switch (target)
                {
                    case (-1)://move to canvas
                        playerHand[pos[2]].Dispose();
                        break;
                    case (-2)://move to deck
                        playerHand[pos[2]].Dispose();
                        break;
                    default://move to player palette
                        index = client.Palettes[target].Size;
                        cards[target].Add(new Button()
                        {
                            Size = new Size(cardWidth, cardHeight),
                            Location = new Point((cardWidth + 15) * index + 60, top),
                            Text = playerHand[pos[2]].Text,
                            BackColor = Color.LightGray,
                            ForeColor = Color.Black,
                        });
                        cards[target][index].MouseDown += new MouseEventHandler(paletteCardClick);
                        Controls.Add(cards[target][index]);
                        cards[target][index].BringToFront();
                        playerHand[pos[2]].Dispose();
                        break;
                }
            }
            else//move from palette
            {
                switch (target)
                {
                    case (-1)://move to canvas
                        cards[pos[1]][pos[2]].Dispose();
                        break;
                    case (-2)://move to deck
                        cards[pos[1]][pos[2]].Dispose();
                        break;
                    default://move to player palette
                        index = client.Palettes[target].Size - 1;
                        cards[target].Add(new Button()
                        {
                            Size = new Size(cardWidth, cardHeight),
                            Text = playerHand[pos[2]].Text,
                            BackColor = Color.LightGray,
                            ForeColor = Color.Black,
                        });
                        if (target == 0)
                        {
                            cards[target][index].Location = new Point((cardWidth + 15) * index + 60, top);
                        }
                        else
                        {
                            cards[target][index].Location = new Point((cardWidth + 15) * index + 60, (target * 190) + 285);
                        }
                        cards[target][index].MouseDown += new MouseEventHandler(paletteCardClick);
                        Controls.Add(cards[target][index]);
                        cards[target][index].BringToFront();
                        cards[pos[1]][pos[2]].Dispose();

                        cards[target][index].BringToFront();
                        break;
                }
            }
        }
        private void MoveCard(int[] startPos, int[] endPos)//start/endPos is in the form [hand/palette, player, index] (specified enpoint version)
        {

        }
        private void paletteCardClick(object sender, MouseEventArgs e)
        {
            if (client.GameState == 3)//if discarding from other palette
            {
                MessageBox.Show("discard from other palette");
            }
            else if (client.GameState == 4)//if discarding from own palette
            {
                MessageBox.Show("discard from own palette");
            }
        }
        private void handCardClick(object sender, MouseEventArgs e)
        {
            int index = -1;

            for (int i = 0; i < playerHand.Count(); i++)//finds index of card clicked
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
                    MoveCard(new int[] { 0, 0, index }, 0);
                    client.PlayToPalette(0, index);
                    RedrawHand();
                    Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)//discard to canvas
            {
                if (client.GameState == 0 || client.GameState == 1)
                {
                    MoveCard(new int[] { 0, 0, index }, -1);
                    client.DiscardToCanvas(0, index);
                    canvas.Text = client.Canvas.GetName();
                    RedrawHand();
                    Invalidate();
                }
            }
            MessageBox.Show(client.GameState.ToString());
        }
        public void RedrawHand()
        {
            foreach (Button b in playerHand)
            {
                b.Dispose();
            }

            for (int i = 0; i < client.Hands[0].Size; i++)
            {
                Card card = client.Hands[0].GetCard(i);
                playerHand[i] = new Button()
                {
                    Size = new Size(cardWidth, cardHeight),
                    Location = new Point((cardWidth + 15) * i + 60, 175),
                    BackColor = Color.LightGray,
                    ForeColor = Color.Black,
                    Text = card.GetName(),
                };
                // add to Form's Controls so that they show up
                playerHand[i].MouseDown += new MouseEventHandler(handCardClick);
                // 
                Controls.Add(playerHand[i]);
                playerHand[i].BringToFront();
            }
        }
        public void RedrawPalette(int player)
        {
            Card card;
            foreach (Button b in cards[player])
            {
                b.Dispose();
            }

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
                    playerHand[i].MouseDown += new MouseEventHandler(handCardClick);
                    // 
                    Controls.Add(playerHand[i]);
                    playerHand[i].BringToFront();
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
                    playerHand[i].MouseDown += new MouseEventHandler(handCardClick);
                    // 
                    Controls.Add(playerHand[i]);
                    playerHand[i].BringToFront();
                }
            }
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
