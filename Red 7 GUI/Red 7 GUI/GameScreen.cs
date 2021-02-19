using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Red_7_GUI
{
    public partial class GameScreen : Form
    {
        private Client client;
        private List<Button> playerHand;
        private List<List<Button>> palettes;
        private int cardHeight = 80;
        private int cardWidth = 60;
        private int top = 300;
        private int players;
        private string[] playerNames;
        private int player;
        private int shift;

        public GameScreen(int players, int player, string[] playerNames, bool advanced, bool actionRule, int seed, bool start, ref TcpClient tcpClient, ref StreamWriter STW, ref StreamReader STR)
        {
            InitializeComponent();
            this.Text = "Game: " + players.ToString() + " players";
            client = new Client(players, advanced, actionRule, seed, ref tcpClient, ref STW, ref STR);
            palettes = new List<List<Button>>();
            playerHand = new List<Button>();
            this.players = players;
            this.playerNames = playerNames;
            this.player = player;
            shift = (4 - player) % 4;
            for (int i = 0; i < players; i++)
            {
                palettes.Add(new List<Button>());
            }

            Setup(start);

            //client.Debug();
        }
        private void Setup(bool start)
        {            
            if (!start)
            {
                client.GameState = -1;
            }

            RedrawHand();
            for (int i = 0; i < players; i++)
            {
                RedrawPalette(i);
            }

            //MessageBox.Show("finished setup");
        }
        private void UpdateLabels()
        {
            canvas.Text = client.Canvas.GetName();
            deckCardsLabel.Text = "Cards Left: " + client.Deck.Size.ToString();
            opponent1HandButton.Text = "Hand: " + client.Hands[1].Size.ToString();
            if (players > 2)
            {
                opponent2HandButton.Text = "Hand: " + client.Hands[2].Size.ToString();
                if (player > 3)
                {
                    opponent3HandButton.Text = "Hand: " + client.Hands[3].Size.ToString();
                }
            }
            playerName.Text = playerNames[player];
            opponent1Name.Text = playerNames[(player + 1) % 4];
            opponent2Name.Text = playerNames[(player + 2) % 4];
            opponent3Name.Text = playerNames[(player + 3) % 4];
            UpdateActionLabel();
        }
        private void UpdateActionLabel()
        {
            switch (client.GameState)
            {
                case (-1):
                    actionLabel.Text = "Waiting for other player(s)...";
                    break;
                case (0):
                    actionLabel.Text = "Play to palette (L click) or discard to canvas (R click) or end turn";
                    break;
                case (1):
                    actionLabel.Text = "Discard to canvas (R click) or end turn";
                    break;
                case (2):
                    actionLabel.Text = "End turn or undo";
                    break;
                case (3):
                    actionLabel.Text = "Discard from other player's palette (L click -> canvas, R click -> deck)";
                    break;
                case (4):
                    actionLabel.Text = "Discard from own palette (L click -> canvas, R click -> deck)";
                    break;
                default:
                    throw new Exception("Invalid game state in action label");
            }
        }
        public void RedrawHand()
        {
            //MessageBox.Show("Redrawing hand");
            foreach (Button b in playerHand)
            {
                b.Dispose();
            }
            playerHand.Clear();

            //MessageBox.Show(client.Hands.Count.ToString());
            for (int i = 0; i < client.Hands[player].Size; i++)
            {
                Card card = client.Hands[player].GetCard(i);
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
            foreach (Button b in palettes[player])
            {
                b.Dispose();
            }
            palettes[player].Clear();

            if (player == this.player)
            {
                for (int i = 0; i < client.Palettes[player].Size; i++)
                {
                    card = client.Palettes[player].GetCard(i);
                    palettes[player].Add(new Button()
                    {
                        Size = new Size(cardWidth, cardHeight),
                        Location = new Point((cardWidth + 15) * i + 60, top),
                        BackColor = Color.LightGray,
                        ForeColor = Color.Black,
                        Text = card.GetName(),
                    });
                    // add to Form's Controls so that they show up
                    palettes[player][i].MouseDown += new MouseEventHandler(paletteCardClick);
                    // 
                    Controls.Add(palettes[player][i]);
                    palettes[player][i].BringToFront();
                }
            }
            else
            {
                int visualIndex = (player + shift) % 4;
                for (int i = 0; i < client.Palettes[player].Size; i++)
                {
                    card = client.Palettes[player].GetCard(i);
                    palettes[player].Add(new Button()
                    {
                        Size = new Size(cardWidth, cardHeight),
                        Location = new Point((cardWidth + 15) * i + 60, (visualIndex * 190) + 285),
                        BackColor = Color.LightGray,
                        ForeColor = Color.Black,
                        Text = card.GetName(),
                    });
                    // add to Form's Controls so that they show up
                    palettes[player][i].MouseDown += new MouseEventHandler(paletteCardClick);
                    // 
                    Controls.Add(palettes[player][i]);
                    palettes[player][i].BringToFront();
                }
            }
            UpdateLabels();
        }
        private void paletteCardClick(object sender, MouseEventArgs e)
        {
            int player = -1;
            int index = -1;

            for (int i = 0; i < palettes.Count; i++)//finds index of card clicked
            {
                for (int x = 0; x < palettes[i].Count; x++)
                {
                    if (sender.Equals(palettes[i][x]))
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
                if (player == this.player)
                {
                    MessageBox.Show("Cannot dicard your own card");
                }
                else if (palettes[player].Count < palettes[this.player].Count)
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
                if (player != this.player)
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
            //MessageBox.Show(client.GameState.ToString());
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
                    client.PlayToPalette(this.player, index);
                    RedrawPalette(this.player);
                    RedrawHand();
                    UpdateLabels();
                }
            }
            else if (e.Button == MouseButtons.Right)//discard to canvas
            {
                if (client.GameState == 0 || client.GameState == 1)
                {
                    client.DiscardToCanvas(this.player, index);
                    canvas.Text = client.Canvas.GetName();
                    RedrawHand();
                    UpdateLabels();
                }
            }
            //MessageBox.Show(client.GameState.ToString());
        }
        private void undoButton_Click(object sender, EventArgs e)
        {
            if (client.CanUndo)
            {
                bool success = client.TryUndo();

                if (!success)
                {
                    MessageBox.Show("Cannot undo a draw action (no cheating)");
                }
            }
            else
            {
                MessageBox.Show("No actions to undo");
            }
        }
        private void endTurnButton_Click(object sender, EventArgs e)
        {
            if (client.CheckWinner(this.player))
            {
                client.EndTurn(true);
            }
            else
            {
                var result = MessageBox.Show("You are not winning - are you sure you want to end your turn", "Confirm end turn", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    client.EndTurn(false);
                }
            }
        }
        public void RemovePlayer(int player)
        {
            foreach (Button b in palettes[player])
            {
                this.Invoke((MethodInvoker)delegate {
                    b.Enabled = false;
                });

            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("open rules/gui overview screen");
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Program.ReturnToLobby();
            Close();
        }
        public void Display(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
