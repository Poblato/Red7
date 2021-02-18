
namespace Red_7_GUI
{
    partial class LobbyScreen
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
            this.player0Panel = new System.Windows.Forms.Panel();
            this.player0Label = new System.Windows.Forms.Label();
            this.player1Panel = new System.Windows.Forms.Panel();
            this.player1Label = new System.Windows.Forms.Label();
            this.player2Panel = new System.Windows.Forms.Panel();
            this.player2Label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.player3Label = new System.Windows.Forms.Label();
            this.advancedCheckBox = new System.Windows.Forms.CheckBox();
            this.actionCheckBox = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.startServerButton = new System.Windows.Forms.Button();
            this.receiver = new System.ComponentModel.BackgroundWorker();
            this.listener = new System.ComponentModel.BackgroundWorker();
            this.serverReceiver = new System.ComponentModel.BackgroundWorker();
            this.player0Panel.SuspendLayout();
            this.player1Panel.SuspendLayout();
            this.player2Panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // player0Panel
            // 
            this.player0Panel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.player0Panel.Controls.Add(this.player0Label);
            this.player0Panel.Location = new System.Drawing.Point(20, 20);
            this.player0Panel.Name = "player0Panel";
            this.player0Panel.Size = new System.Drawing.Size(500, 75);
            this.player0Panel.TabIndex = 0;
            // 
            // player0Label
            // 
            this.player0Label.AutoSize = true;
            this.player0Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.player0Label.ForeColor = System.Drawing.Color.Cornsilk;
            this.player0Label.Location = new System.Drawing.Point(15, 15);
            this.player0Label.Name = "player0Label";
            this.player0Label.Size = new System.Drawing.Size(230, 39);
            this.player0Label.TabIndex = 0;
            this.player0Label.Text = "player0 (Host)";
            // 
            // player1Panel
            // 
            this.player1Panel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.player1Panel.Controls.Add(this.player1Label);
            this.player1Panel.Location = new System.Drawing.Point(20, 115);
            this.player1Panel.Name = "player1Panel";
            this.player1Panel.Size = new System.Drawing.Size(500, 75);
            this.player1Panel.TabIndex = 1;
            // 
            // player1Label
            // 
            this.player1Label.AutoSize = true;
            this.player1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.player1Label.ForeColor = System.Drawing.Color.Cornsilk;
            this.player1Label.Location = new System.Drawing.Point(15, 18);
            this.player1Label.Name = "player1Label";
            this.player1Label.Size = new System.Drawing.Size(129, 39);
            this.player1Label.TabIndex = 1;
            this.player1Label.Text = "player1";
            // 
            // player2Panel
            // 
            this.player2Panel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.player2Panel.Controls.Add(this.player2Label);
            this.player2Panel.Location = new System.Drawing.Point(20, 210);
            this.player2Panel.Name = "player2Panel";
            this.player2Panel.Size = new System.Drawing.Size(500, 75);
            this.player2Panel.TabIndex = 2;
            // 
            // player2Label
            // 
            this.player2Label.AutoSize = true;
            this.player2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.player2Label.ForeColor = System.Drawing.Color.Cornsilk;
            this.player2Label.Location = new System.Drawing.Point(15, 16);
            this.player2Label.Name = "player2Label";
            this.player2Label.Size = new System.Drawing.Size(129, 39);
            this.player2Label.TabIndex = 2;
            this.player2Label.Text = "player2";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.player3Label);
            this.panel1.Location = new System.Drawing.Point(20, 305);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 75);
            this.panel1.TabIndex = 3;
            // 
            // player3Label
            // 
            this.player3Label.AutoSize = true;
            this.player3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.player3Label.ForeColor = System.Drawing.Color.Cornsilk;
            this.player3Label.Location = new System.Drawing.Point(15, 16);
            this.player3Label.Name = "player3Label";
            this.player3Label.Size = new System.Drawing.Size(129, 39);
            this.player3Label.TabIndex = 3;
            this.player3Label.Text = "player3";
            // 
            // advancedCheckBox
            // 
            this.advancedCheckBox.AutoSize = true;
            this.advancedCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.advancedCheckBox.Location = new System.Drawing.Point(578, 98);
            this.advancedCheckBox.Name = "advancedCheckBox";
            this.advancedCheckBox.Size = new System.Drawing.Size(120, 29);
            this.advancedCheckBox.TabIndex = 4;
            this.advancedCheckBox.Text = "Advanced";
            this.advancedCheckBox.UseVisualStyleBackColor = true;
            this.advancedCheckBox.CheckedChanged += new System.EventHandler(this.advancedCheckBox_CheckedChanged);
            // 
            // actionCheckBox
            // 
            this.actionCheckBox.AutoSize = true;
            this.actionCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.actionCheckBox.Location = new System.Drawing.Point(578, 32);
            this.actionCheckBox.Name = "actionCheckBox";
            this.actionCheckBox.Size = new System.Drawing.Size(130, 29);
            this.actionCheckBox.TabIndex = 5;
            this.actionCheckBox.Text = "Action Rule";
            this.actionCheckBox.UseVisualStyleBackColor = true;
            this.actionCheckBox.CheckedChanged += new System.EventHandler(this.actionCheckBox_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.startButton.Location = new System.Drawing.Point(555, 308);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(200, 72);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.quitButton.Location = new System.Drawing.Point(578, 213);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(177, 72);
            this.quitButton.TabIndex = 7;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // helpButton
            // 
            this.helpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.helpButton.Location = new System.Drawing.Point(720, 155);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(35, 35);
            this.helpButton.TabIndex = 8;
            this.helpButton.Text = "?";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // startServerButton
            // 
            this.startServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.startServerButton.Location = new System.Drawing.Point(590, 143);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(107, 46);
            this.startServerButton.TabIndex = 9;
            this.startServerButton.Text = "Start server";
            this.startServerButton.UseVisualStyleBackColor = true;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
            // 
            // receiver
            // 
            this.receiver.DoWork += new System.ComponentModel.DoWorkEventHandler(this.receiver_DoWork);
            // 
            // listener
            // 
            this.listener.WorkerSupportsCancellation = true;
            this.listener.DoWork += new System.ComponentModel.DoWorkEventHandler(this.listener_DoWork);
            // 
            // serverReceiver
            // 
            this.serverReceiver.WorkerSupportsCancellation = true;
            this.serverReceiver.DoWork += new System.ComponentModel.DoWorkEventHandler(this.serverReceiver_DoWork);
            // 
            // LobbyScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 400);
            this.Controls.Add(this.startServerButton);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.actionCheckBox);
            this.Controls.Add(this.advancedCheckBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.player2Panel);
            this.Controls.Add(this.player1Panel);
            this.Controls.Add(this.player0Panel);
            this.Name = "LobbyScreen";
            this.Text = "LobbyScreen";
            this.player0Panel.ResumeLayout(false);
            this.player0Panel.PerformLayout();
            this.player1Panel.ResumeLayout(false);
            this.player1Panel.PerformLayout();
            this.player2Panel.ResumeLayout(false);
            this.player2Panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel player0Panel;
        private System.Windows.Forms.Panel player1Panel;
        private System.Windows.Forms.Panel player2Panel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox advancedCheckBox;
        private System.Windows.Forms.CheckBox actionCheckBox;
        private System.Windows.Forms.Label player0Label;
        private System.Windows.Forms.Label player1Label;
        private System.Windows.Forms.Label player2Label;
        private System.Windows.Forms.Label player3Label;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button startServerButton;
        private System.ComponentModel.BackgroundWorker receiver;
        private System.ComponentModel.BackgroundWorker listener;
        private System.ComponentModel.BackgroundWorker serverReceiver;
    }
}