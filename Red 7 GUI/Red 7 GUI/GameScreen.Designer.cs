
namespace Red_7_GUI
{
    partial class GameScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Title = new System.Windows.Forms.Label();
            this.playerPanel = new System.Windows.Forms.Panel();
            this.playerPalettePanel = new System.Windows.Forms.Panel();
            this.playerPaletteLabel = new System.Windows.Forms.Label();
            this.playerHandPanel = new System.Windows.Forms.Panel();
            this.playerHandLabel = new System.Windows.Forms.Label();
            this.playerName = new System.Windows.Forms.Label();
            this.opponent1Panel = new System.Windows.Forms.Panel();
            this.opponent1PalettePanel = new System.Windows.Forms.Panel();
            this.opponent1HandButton = new System.Windows.Forms.Button();
            this.opponent1Name = new System.Windows.Forms.Label();
            this.opponent2Panel = new System.Windows.Forms.Panel();
            this.opponent2PalettePanel = new System.Windows.Forms.Panel();
            this.opponent2HandButton = new System.Windows.Forms.Button();
            this.opponent2Name = new System.Windows.Forms.Label();
            this.opponent3Panel = new System.Windows.Forms.Panel();
            this.opponent3PalettePanel = new System.Windows.Forms.Panel();
            this.opponent3HandButton = new System.Windows.Forms.Button();
            this.opponent3Name = new System.Windows.Forms.Label();
            this.endTurnButton = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.deckCanvasPanel = new System.Windows.Forms.Panel();
            this.deckCardsLabel = new System.Windows.Forms.Label();
            this.canvas = new System.Windows.Forms.Button();
            this.deck = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.playerPanel.SuspendLayout();
            this.playerPalettePanel.SuspendLayout();
            this.playerHandPanel.SuspendLayout();
            this.opponent1Panel.SuspendLayout();
            this.opponent1PalettePanel.SuspendLayout();
            this.opponent2Panel.SuspendLayout();
            this.opponent2PalettePanel.SuspendLayout();
            this.opponent3Panel.SuspendLayout();
            this.opponent3PalettePanel.SuspendLayout();
            this.deckCanvasPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F);
            this.Title.Location = new System.Drawing.Point(12, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(210, 76);
            this.Title.TabIndex = 0;
            this.Title.Text = "Red 7";
            // 
            // playerPanel
            // 
            this.playerPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.playerPanel.Controls.Add(this.playerPalettePanel);
            this.playerPanel.Controls.Add(this.playerHandPanel);
            this.playerPanel.Controls.Add(this.playerName);
            this.playerPanel.Location = new System.Drawing.Point(25, 100);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(750, 300);
            this.playerPanel.TabIndex = 1;
            // 
            // playerPalettePanel
            // 
            this.playerPalettePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.playerPalettePanel.Controls.Add(this.playerPaletteLabel);
            this.playerPalettePanel.Location = new System.Drawing.Point(25, 175);
            this.playerPalettePanel.Name = "playerPalettePanel";
            this.playerPalettePanel.Size = new System.Drawing.Size(700, 110);
            this.playerPalettePanel.TabIndex = 2;
            // 
            // playerPaletteLabel
            // 
            this.playerPaletteLabel.AutoSize = true;
            this.playerPaletteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.playerPaletteLabel.Location = new System.Drawing.Point(3, 3);
            this.playerPaletteLabel.Name = "playerPaletteLabel";
            this.playerPaletteLabel.Size = new System.Drawing.Size(72, 25);
            this.playerPaletteLabel.TabIndex = 1;
            this.playerPaletteLabel.Text = "Palette";
            // 
            // playerHandPanel
            // 
            this.playerHandPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.playerHandPanel.Controls.Add(this.playerHandLabel);
            this.playerHandPanel.Location = new System.Drawing.Point(25, 50);
            this.playerHandPanel.Name = "playerHandPanel";
            this.playerHandPanel.Size = new System.Drawing.Size(700, 110);
            this.playerHandPanel.TabIndex = 1;
            // 
            // playerHandLabel
            // 
            this.playerHandLabel.AutoSize = true;
            this.playerHandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.playerHandLabel.Location = new System.Drawing.Point(3, 3);
            this.playerHandLabel.Name = "playerHandLabel";
            this.playerHandLabel.Size = new System.Drawing.Size(59, 25);
            this.playerHandLabel.TabIndex = 0;
            this.playerHandLabel.Text = "Hand";
            // 
            // playerName
            // 
            this.playerName.AutoSize = true;
            this.playerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.playerName.ForeColor = System.Drawing.Color.Cornsilk;
            this.playerName.Location = new System.Drawing.Point(15, 15);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(124, 25);
            this.playerName.TabIndex = 0;
            this.playerName.Text = "Player Name";
            // 
            // opponent1Panel
            // 
            this.opponent1Panel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.opponent1Panel.Controls.Add(this.opponent1PalettePanel);
            this.opponent1Panel.Controls.Add(this.opponent1Name);
            this.opponent1Panel.Location = new System.Drawing.Point(25, 410);
            this.opponent1Panel.Name = "opponent1Panel";
            this.opponent1Panel.Size = new System.Drawing.Size(750, 180);
            this.opponent1Panel.TabIndex = 2;
            // 
            // opponent1PalettePanel
            // 
            this.opponent1PalettePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.opponent1PalettePanel.Controls.Add(this.opponent1HandButton);
            this.opponent1PalettePanel.Location = new System.Drawing.Point(25, 50);
            this.opponent1PalettePanel.Name = "opponent1PalettePanel";
            this.opponent1PalettePanel.Size = new System.Drawing.Size(700, 110);
            this.opponent1PalettePanel.TabIndex = 2;
            // 
            // opponent1HandButton
            // 
            this.opponent1HandButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.opponent1HandButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.opponent1HandButton.Location = new System.Drawing.Point(625, 15);
            this.opponent1HandButton.Name = "opponent1HandButton";
            this.opponent1HandButton.Size = new System.Drawing.Size(60, 80);
            this.opponent1HandButton.TabIndex = 0;
            this.opponent1HandButton.Text = "Hand: 7";
            this.opponent1HandButton.UseVisualStyleBackColor = false;
            // 
            // opponent1Name
            // 
            this.opponent1Name.AutoSize = true;
            this.opponent1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.opponent1Name.ForeColor = System.Drawing.Color.Cornsilk;
            this.opponent1Name.Location = new System.Drawing.Point(15, 15);
            this.opponent1Name.Name = "opponent1Name";
            this.opponent1Name.Size = new System.Drawing.Size(172, 25);
            this.opponent1Name.TabIndex = 1;
            this.opponent1Name.Text = "Opponent 1 Name";
            // 
            // opponent2Panel
            // 
            this.opponent2Panel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.opponent2Panel.Controls.Add(this.opponent2PalettePanel);
            this.opponent2Panel.Controls.Add(this.opponent2Name);
            this.opponent2Panel.Location = new System.Drawing.Point(25, 600);
            this.opponent2Panel.Name = "opponent2Panel";
            this.opponent2Panel.Size = new System.Drawing.Size(750, 180);
            this.opponent2Panel.TabIndex = 3;
            // 
            // opponent2PalettePanel
            // 
            this.opponent2PalettePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.opponent2PalettePanel.Controls.Add(this.opponent2HandButton);
            this.opponent2PalettePanel.Location = new System.Drawing.Point(25, 50);
            this.opponent2PalettePanel.Name = "opponent2PalettePanel";
            this.opponent2PalettePanel.Size = new System.Drawing.Size(700, 110);
            this.opponent2PalettePanel.TabIndex = 3;
            // 
            // opponent2HandButton
            // 
            this.opponent2HandButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.opponent2HandButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.opponent2HandButton.Location = new System.Drawing.Point(625, 15);
            this.opponent2HandButton.Name = "opponent2HandButton";
            this.opponent2HandButton.Size = new System.Drawing.Size(60, 80);
            this.opponent2HandButton.TabIndex = 1;
            this.opponent2HandButton.Text = "Hand: 7";
            this.opponent2HandButton.UseVisualStyleBackColor = false;
            // 
            // opponent2Name
            // 
            this.opponent2Name.AutoSize = true;
            this.opponent2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.opponent2Name.ForeColor = System.Drawing.Color.Cornsilk;
            this.opponent2Name.Location = new System.Drawing.Point(15, 15);
            this.opponent2Name.Name = "opponent2Name";
            this.opponent2Name.Size = new System.Drawing.Size(172, 25);
            this.opponent2Name.TabIndex = 2;
            this.opponent2Name.Text = "Opponent 2 Name";
            // 
            // opponent3Panel
            // 
            this.opponent3Panel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.opponent3Panel.Controls.Add(this.opponent3PalettePanel);
            this.opponent3Panel.Controls.Add(this.opponent3Name);
            this.opponent3Panel.Location = new System.Drawing.Point(25, 790);
            this.opponent3Panel.Name = "opponent3Panel";
            this.opponent3Panel.Size = new System.Drawing.Size(750, 180);
            this.opponent3Panel.TabIndex = 4;
            // 
            // opponent3PalettePanel
            // 
            this.opponent3PalettePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.opponent3PalettePanel.Controls.Add(this.opponent3HandButton);
            this.opponent3PalettePanel.Location = new System.Drawing.Point(25, 50);
            this.opponent3PalettePanel.Name = "opponent3PalettePanel";
            this.opponent3PalettePanel.Size = new System.Drawing.Size(700, 110);
            this.opponent3PalettePanel.TabIndex = 4;
            // 
            // opponent3HandButton
            // 
            this.opponent3HandButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.opponent3HandButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.opponent3HandButton.Location = new System.Drawing.Point(625, 15);
            this.opponent3HandButton.Name = "opponent3HandButton";
            this.opponent3HandButton.Size = new System.Drawing.Size(60, 80);
            this.opponent3HandButton.TabIndex = 9;
            this.opponent3HandButton.Text = "Hand: 7";
            this.opponent3HandButton.UseVisualStyleBackColor = false;
            // 
            // opponent3Name
            // 
            this.opponent3Name.AutoSize = true;
            this.opponent3Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.opponent3Name.ForeColor = System.Drawing.Color.Cornsilk;
            this.opponent3Name.Location = new System.Drawing.Point(15, 15);
            this.opponent3Name.Name = "opponent3Name";
            this.opponent3Name.Size = new System.Drawing.Size(172, 25);
            this.opponent3Name.TabIndex = 3;
            this.opponent3Name.Text = "Opponent 3 Name";
            // 
            // endTurnButton
            // 
            this.endTurnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.endTurnButton.Location = new System.Drawing.Point(1153, 890);
            this.endTurnButton.Name = "endTurnButton";
            this.endTurnButton.Size = new System.Drawing.Size(150, 70);
            this.endTurnButton.TabIndex = 5;
            this.endTurnButton.Text = "End Turn";
            this.endTurnButton.UseVisualStyleBackColor = true;
            this.endTurnButton.Click += new System.EventHandler(this.endTurnButton_Click);
            // 
            // undoButton
            // 
            this.undoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.undoButton.Location = new System.Drawing.Point(1055, 890);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(75, 70);
            this.undoButton.TabIndex = 6;
            this.undoButton.Text = "Undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // deckCanvasPanel
            // 
            this.deckCanvasPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.deckCanvasPanel.Controls.Add(this.deckCardsLabel);
            this.deckCanvasPanel.Controls.Add(this.canvas);
            this.deckCanvasPanel.Controls.Add(this.deck);
            this.deckCanvasPanel.ForeColor = System.Drawing.Color.Cornsilk;
            this.deckCanvasPanel.Location = new System.Drawing.Point(840, 100);
            this.deckCanvasPanel.Name = "deckCanvasPanel";
            this.deckCanvasPanel.Size = new System.Drawing.Size(450, 300);
            this.deckCanvasPanel.TabIndex = 7;
            // 
            // deckCardsLabel
            // 
            this.deckCardsLabel.AutoSize = true;
            this.deckCardsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.deckCardsLabel.Location = new System.Drawing.Point(65, 25);
            this.deckCardsLabel.Name = "deckCardsLabel";
            this.deckCardsLabel.Size = new System.Drawing.Size(135, 25);
            this.deckCardsLabel.TabIndex = 2;
            this.deckCardsLabel.Text = "Cards Left: 49";
            // 
            // canvas
            // 
            this.canvas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.canvas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.canvas.Location = new System.Drawing.Point(255, 60);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(125, 180);
            this.canvas.TabIndex = 1;
            this.canvas.Text = "Canvas";
            this.canvas.UseVisualStyleBackColor = true;
            // 
            // deck
            // 
            this.deck.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.deck.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deck.Location = new System.Drawing.Point(70, 60);
            this.deck.Name = "deck";
            this.deck.Size = new System.Drawing.Size(125, 180);
            this.deck.TabIndex = 0;
            this.deck.Text = "Deck";
            this.deck.UseVisualStyleBackColor = true;
            // 
            // helpButton
            // 
            this.helpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.helpButton.Location = new System.Drawing.Point(960, 890);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(75, 70);
            this.helpButton.TabIndex = 8;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 987);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.deckCanvasPanel);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.endTurnButton);
            this.Controls.Add(this.opponent3Panel);
            this.Controls.Add(this.opponent2Panel);
            this.Controls.Add(this.opponent1Panel);
            this.Controls.Add(this.playerPanel);
            this.Controls.Add(this.Title);
            this.Name = "GameScreen";
            this.playerPanel.ResumeLayout(false);
            this.playerPanel.PerformLayout();
            this.playerPalettePanel.ResumeLayout(false);
            this.playerPalettePanel.PerformLayout();
            this.playerHandPanel.ResumeLayout(false);
            this.playerHandPanel.PerformLayout();
            this.opponent1Panel.ResumeLayout(false);
            this.opponent1Panel.PerformLayout();
            this.opponent1PalettePanel.ResumeLayout(false);
            this.opponent2Panel.ResumeLayout(false);
            this.opponent2Panel.PerformLayout();
            this.opponent2PalettePanel.ResumeLayout(false);
            this.opponent3Panel.ResumeLayout(false);
            this.opponent3Panel.PerformLayout();
            this.opponent3PalettePanel.ResumeLayout(false);
            this.deckCanvasPanel.ResumeLayout(false);
            this.deckCanvasPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Panel playerPanel;
        private System.Windows.Forms.Label playerName;
        private System.Windows.Forms.Panel opponent1Panel;
        private System.Windows.Forms.Panel opponent2Panel;
        private System.Windows.Forms.Panel opponent3Panel;
        private System.Windows.Forms.Label opponent1Name;
        private System.Windows.Forms.Label opponent2Name;
        private System.Windows.Forms.Label opponent3Name;
        private System.Windows.Forms.Panel playerPalettePanel;
        private System.Windows.Forms.Panel playerHandPanel;
        private System.Windows.Forms.Panel opponent1PalettePanel;
        private System.Windows.Forms.Panel opponent2PalettePanel;
        private System.Windows.Forms.Panel opponent3PalettePanel;
        private System.Windows.Forms.Label playerPaletteLabel;
        private System.Windows.Forms.Label playerHandLabel;
        private System.Windows.Forms.Button endTurnButton;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Panel deckCanvasPanel;
        private System.Windows.Forms.Button canvas;
        private System.Windows.Forms.Button deck;
        private System.Windows.Forms.Label deckCardsLabel;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button opponent1HandButton;
        private System.Windows.Forms.Button opponent2HandButton;
        private System.Windows.Forms.Button opponent3HandButton;
    }
}

