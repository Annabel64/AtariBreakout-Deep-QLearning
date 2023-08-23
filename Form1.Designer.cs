namespace Mission4_Atari_BreakOut
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.timerLoop = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxGame = new System.Windows.Forms.PictureBox();
            this.Play = new System.Windows.Forms.Button();
            this.ExplorationButton = new System.Windows.Forms.Button();
            this.ExploitationButton = new System.Windows.Forms.Button();
            this.trainingNB = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bouncePerGame = new System.Windows.Forms.TextBox();
            this.buttonBestParam = new System.Windows.Forms.Button();
            this.labelBounces = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonReinitGame = new System.Windows.Forms.Button();
            this.cumulRewards = new System.Windows.Forms.TextBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.chartLastGames = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.arrayCoord1 = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.buttonNextState = new System.Windows.Forms.Button();
            this.panel0 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ValuePredictions = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.LegendLeft = new System.Windows.Forms.TextBox();
            this.LegendStationnary = new System.Windows.Forms.TextBox();
            this.LegendRight = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGame)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLastGames)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // timerLoop
            // 
            this.timerLoop.Enabled = true;
            this.timerLoop.Interval = 1;
            this.timerLoop.Tick += new System.EventHandler(this.timerLoop_Tick);
            // 
            // pictureBoxGame
            // 
            this.pictureBoxGame.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxGame.Location = new System.Drawing.Point(88, 216);
            this.pictureBoxGame.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxGame.Name = "pictureBoxGame";
            this.pictureBoxGame.Size = new System.Drawing.Size(451, 433);
            this.pictureBoxGame.TabIndex = 0;
            this.pictureBoxGame.TabStop = false;
            this.pictureBoxGame.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGame_Paint);
            this.pictureBoxGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGame_MouseMove);
            // 
            // Play
            // 
            this.Play.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Play.Location = new System.Drawing.Point(656, 694);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(102, 72);
            this.Play.TabIndex = 3;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // ExplorationButton
            // 
            this.ExplorationButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ExplorationButton.Location = new System.Drawing.Point(776, 694);
            this.ExplorationButton.Name = "ExplorationButton";
            this.ExplorationButton.Size = new System.Drawing.Size(124, 72);
            this.ExplorationButton.TabIndex = 4;
            this.ExplorationButton.Text = "Exploration";
            this.ExplorationButton.UseVisualStyleBackColor = true;
            this.ExplorationButton.Click += new System.EventHandler(this.ExplorationButton_Click);
            // 
            // ExploitationButton
            // 
            this.ExploitationButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ExploitationButton.Location = new System.Drawing.Point(913, 694);
            this.ExploitationButton.Name = "ExploitationButton";
            this.ExploitationButton.Size = new System.Drawing.Size(133, 72);
            this.ExploitationButton.TabIndex = 5;
            this.ExploitationButton.Text = "Exploitation";
            this.ExploitationButton.UseVisualStyleBackColor = true;
            this.ExploitationButton.Click += new System.EventHandler(this.ExploitationButton_Click);
            // 
            // trainingNB
            // 
            this.trainingNB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.trainingNB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trainingNB.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.trainingNB.Location = new System.Drawing.Point(15, 28);
            this.trainingNB.Name = "trainingNB";
            this.trainingNB.Size = new System.Drawing.Size(480, 20);
            this.trainingNB.TabIndex = 9;
            this.trainingNB.Text = "Training number";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.bouncePerGame);
            this.groupBox1.Controls.Add(this.trainingNB);
            this.groupBox1.Location = new System.Drawing.Point(656, 232);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 98);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exploration";
            // 
            // bouncePerGame
            // 
            this.bouncePerGame.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bouncePerGame.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bouncePerGame.Font = new System.Drawing.Font("MS UI Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bouncePerGame.Location = new System.Drawing.Point(15, 65);
            this.bouncePerGame.Name = "bouncePerGame";
            this.bouncePerGame.Size = new System.Drawing.Size(436, 17);
            this.bouncePerGame.TabIndex = 10;
            this.bouncePerGame.Text = "bouncePerGame";
            // 
            // buttonBestParam
            // 
            this.buttonBestParam.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonBestParam.Location = new System.Drawing.Point(1231, 130);
            this.buttonBestParam.Name = "buttonBestParam";
            this.buttonBestParam.Size = new System.Drawing.Size(121, 72);
            this.buttonBestParam.TabIndex = 52;
            this.buttonBestParam.Text = "Best Param";
            this.buttonBestParam.UseVisualStyleBackColor = true;
            this.buttonBestParam.Click += new System.EventHandler(this.buttonBestParam_Click);
            // 
            // labelBounces
            // 
            this.labelBounces.BackColor = System.Drawing.SystemColors.Info;
            this.labelBounces.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelBounces.Location = new System.Drawing.Point(15, 64);
            this.labelBounces.Name = "labelBounces";
            this.labelBounces.Size = new System.Drawing.Size(377, 15);
            this.labelBounces.TabIndex = 29;
            this.labelBounces.Text = "Number of bounces";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Controls.Add(this.buttonReinitGame);
            this.groupBox2.Controls.Add(this.cumulRewards);
            this.groupBox2.Controls.Add(this.textBox);
            this.groupBox2.Controls.Add(this.labelBounces);
            this.groupBox2.Controls.Add(this.chartLastGames);
            this.groupBox2.Location = new System.Drawing.Point(656, 345);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(768, 343);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Exploitation";
            // 
            // buttonReinitGame
            // 
            this.buttonReinitGame.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonReinitGame.Location = new System.Drawing.Point(588, 32);
            this.buttonReinitGame.Name = "buttonReinitGame";
            this.buttonReinitGame.Size = new System.Drawing.Size(121, 72);
            this.buttonReinitGame.TabIndex = 53;
            this.buttonReinitGame.Text = "Reinit grid";
            this.buttonReinitGame.UseVisualStyleBackColor = true;
            this.buttonReinitGame.Click += new System.EventHandler(this.buttonReinitGame_Click);
            // 
            // cumulRewards
            // 
            this.cumulRewards.BackColor = System.Drawing.SystemColors.Info;
            this.cumulRewards.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cumulRewards.Location = new System.Drawing.Point(15, 34);
            this.cumulRewards.Name = "cumulRewards";
            this.cumulRewards.Size = new System.Drawing.Size(356, 15);
            this.cumulRewards.TabIndex = 29;
            this.cumulRewards.Text = "Last reward";
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.SystemColors.Info;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox.Location = new System.Drawing.Point(15, 99);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(330, 15);
            this.textBox.TabIndex = 32;
            this.textBox.Text = "Results of the 30 last games played by the AI :";
            // 
            // chartLastGames
            // 
            this.chartLastGames.BackColor = System.Drawing.Color.PeachPuff;
            this.chartLastGames.BorderlineColor = System.Drawing.SystemColors.Window;
            chartArea4.Name = "ChartArea1";
            this.chartLastGames.ChartAreas.Add(chartArea4);
            this.chartLastGames.Location = new System.Drawing.Point(11, 120);
            this.chartLastGames.Name = "chartLastGames";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Name = "Bounces";
            this.chartLastGames.Series.Add(series2);
            this.chartLastGames.Size = new System.Drawing.Size(751, 213);
            this.chartLastGames.TabIndex = 11;
            this.chartLastGames.Text = "30 last games";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox3.Controls.Add(this.arrayCoord1);
            this.groupBox3.Location = new System.Drawing.Point(656, 130);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(501, 82);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Positions";
            // 
            // arrayCoord1
            // 
            this.arrayCoord1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.arrayCoord1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arrayCoord1.Location = new System.Drawing.Point(13, 25);
            this.arrayCoord1.Multiline = true;
            this.arrayCoord1.Name = "arrayCoord1";
            this.arrayCoord1.Size = new System.Drawing.Size(468, 51);
            this.arrayCoord1.TabIndex = 28;
            this.arrayCoord1.Text = "Array";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTitle.Font = new System.Drawing.Font("Forte", 25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxTitle.Location = new System.Drawing.Point(31, 40);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(666, 48);
            this.textBoxTitle.TabIndex = 32;
            this.textBoxTitle.Text = "AtariBreakout w/ DeepQLearning";
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonStop.Location = new System.Drawing.Point(1082, 694);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(57, 36);
            this.buttonStop.TabIndex = 35;
            this.buttonStop.Text = "| |";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonGo.Location = new System.Drawing.Point(1082, 730);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(57, 36);
            this.buttonGo.TabIndex = 39;
            this.buttonGo.Text = "|>";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // textBox13
            // 
            this.textBox13.BackColor = System.Drawing.SystemColors.Control;
            this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox13.Font = new System.Drawing.Font("Forte", 16.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox13.Location = new System.Drawing.Point(759, 52);
            this.textBox13.Multiline = true;
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(298, 48);
            this.textBox13.TabIndex = 49;
            this.textBox13.Text = "Variables and results";
            // 
            // buttonNextState
            // 
            this.buttonNextState.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonNextState.Location = new System.Drawing.Point(1159, 694);
            this.buttonNextState.Name = "buttonNextState";
            this.buttonNextState.Size = new System.Drawing.Size(87, 72);
            this.buttonNextState.TabIndex = 51;
            this.buttonNextState.Text = "Next state";
            this.buttonNextState.UseVisualStyleBackColor = true;
            this.buttonNextState.Click += new System.EventHandler(this.buttonNextState_Click);
            // 
            // panel0
            // 
            this.panel0.Location = new System.Drawing.Point(88, 658);
            this.panel0.Name = "panel0";
            this.panel0.Size = new System.Drawing.Size(142, 10);
            this.panel0.TabIndex = 52;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(397, 658);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(142, 10);
            this.panel2.TabIndex = 53;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(242, 658);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(142, 10);
            this.panel1.TabIndex = 53;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(1194, 232);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(230, 98);
            this.textBox1.TabIndex = 30;
            this.textBox1.Text = "Array";
            // 
            // ValuePredictions
            // 
            this.ValuePredictions.BackColor = System.Drawing.SystemColors.Menu;
            this.ValuePredictions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ValuePredictions.Location = new System.Drawing.Point(88, 669);
            this.ValuePredictions.Multiline = true;
            this.ValuePredictions.Name = "ValuePredictions";
            this.ValuePredictions.Size = new System.Drawing.Size(458, 14);
            this.ValuePredictions.TabIndex = 30;
            this.ValuePredictions.Text = "ValuePredictions";
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chart1.BorderlineColor = System.Drawing.SystemColors.ButtonFace;
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            this.chart1.Location = new System.Drawing.Point(67, 689);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(491, 75);
            this.chart1.TabIndex = 54;
            this.chart1.Text = "chart1";
            // 
            // LegendLeft
            // 
            this.LegendLeft.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LegendLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LegendLeft.ForeColor = System.Drawing.Color.Green;
            this.LegendLeft.Location = new System.Drawing.Point(555, 695);
            this.LegendLeft.Multiline = true;
            this.LegendLeft.Name = "LegendLeft";
            this.LegendLeft.Size = new System.Drawing.Size(71, 24);
            this.LegendLeft.TabIndex = 30;
            this.LegendLeft.Text = "Left";
            // 
            // LegendStationnary
            // 
            this.LegendStationnary.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LegendStationnary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LegendStationnary.ForeColor = System.Drawing.Color.Blue;
            this.LegendStationnary.Location = new System.Drawing.Point(555, 718);
            this.LegendStationnary.Multiline = true;
            this.LegendStationnary.Name = "LegendStationnary";
            this.LegendStationnary.Size = new System.Drawing.Size(90, 16);
            this.LegendStationnary.TabIndex = 55;
            this.LegendStationnary.Text = "Stationnary";
            // 
            // LegendRight
            // 
            this.LegendRight.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LegendRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LegendRight.ForeColor = System.Drawing.Color.Red;
            this.LegendRight.Location = new System.Drawing.Point(555, 740);
            this.LegendRight.Multiline = true;
            this.LegendRight.Name = "LegendRight";
            this.LegendRight.Size = new System.Drawing.Size(71, 24);
            this.LegendRight.TabIndex = 56;
            this.LegendRight.Text = "Right";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.Color.Red;
            this.textBox2.Location = new System.Drawing.Point(555, 178);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(71, 24);
            this.textBox2.TabIndex = 60;
            this.textBox2.Text = "Right";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.Blue;
            this.textBox3.Location = new System.Drawing.Point(555, 156);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(90, 16);
            this.textBox3.TabIndex = 59;
            this.textBox3.Text = "Stationnary";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.Green;
            this.textBox4.Location = new System.Drawing.Point(555, 133);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(71, 24);
            this.textBox4.TabIndex = 57;
            this.textBox4.Text = "Left";
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chart2.BorderlineColor = System.Drawing.SystemColors.ButtonFace;
            chartArea6.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea6);
            this.chart2.Location = new System.Drawing.Point(67, 127);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(491, 75);
            this.chart2.TabIndex = 58;
            this.chart2.Text = "chart2";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(397, 202);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(142, 10);
            this.panel3.TabIndex = 56;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(243, 203);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(142, 10);
            this.panel4.TabIndex = 55;
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(88, 203);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(142, 10);
            this.panel5.TabIndex = 54;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1436, 824);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.LegendRight);
            this.Controls.Add(this.LegendStationnary);
            this.Controls.Add(this.LegendLeft);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.ValuePredictions);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonBestParam);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel0);
            this.Controls.Add(this.buttonNextState);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ExploitationButton);
            this.Controls.Add(this.ExplorationButton);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.pictureBoxGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGame)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLastGames)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerLoop;
        private System.Windows.Forms.PictureBox pictureBoxGame;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button ExplorationButton;
        private System.Windows.Forms.Button ExploitationButton;
        private System.Windows.Forms.TextBox trainingNB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLastGames;
        private System.Windows.Forms.TextBox labelBounces;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox arrayCoord1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.TextBox cumulRewards;
        private System.Windows.Forms.TextBox bouncePerGame;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Button buttonNextState;
        private System.Windows.Forms.Button buttonBestParam;
        private System.Windows.Forms.Button buttonReinitGame;
        private System.Windows.Forms.Panel panel0;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox ValuePredictions;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox LegendLeft;
        private System.Windows.Forms.TextBox LegendStationnary;
        private System.Windows.Forms.TextBox LegendRight;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
    }
}

