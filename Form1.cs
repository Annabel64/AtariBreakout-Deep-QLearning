using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;



namespace Mission4_Atari_BreakOut
{
    public partial class Form1 : Form
    {

        #region ======================================================= Class Brick =====================================================

        public class Brick
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public bool IsAlive { get; set; }
            public Brush Color { get; set; }
            public Pen Contour { get; set; }

            public Brick(int x = 0, int y = 0, int width = 64, int height = 10)
            {
                X = x;
                Y = y;
                Width = width;
                Height = height;
                IsAlive = true;
                Color = Brushes.Pink;
                Contour = Pens.Black;
            }

            public bool CollidesWith(int x_ball, int y_ball, int diameter)
            {
                //vérifie si la balle et la brique sont supperposees
                //if (x_ball >= X - 3 * diameter / 4 && x_ball <= X + Width + 3 * diameter / 4 && y_ball == Y)
                if ((x_ball + diameter == X && y_ball + diameter >= Y && y_ball < Y + Height) || (x_ball == X + Width && y_ball + diameter >= Y && y_ball < Y + Height) || (y_ball + diameter >= Y && y_ball <= Y + Height && x_ball + diameter >= X && x_ball <= X + Width) || (y_ball == Y + Height && x_ball + diameter >= X && x_ball <= X + Width + diameter))
                {
                    return true;
                }
                return false;
            }

        }
        #endregion


        #region ======================================================= Parameters of the game ==========================================

        readonly bool brick = false; //play with the bricks or not
        public bool pauseBool = false;

        readonly int dim = 320;//dimentions of the interface
        float vx, vy; // speed of the ball
        List<Brick> bricks;
        Brick PlateformeUp;
        Brick PlateformeDown;
        Brick Ball;
        List<float> games;

        //rewards and penalties
        readonly int win = 5;
        readonly int breakBrick = 3;
        readonly int bounce = 2;
        readonly int loose = -5;
        readonly float moved = (float)-0; //penalty if moves left / right, so it doesnt move too much
        readonly float time = (float)-0; //penalty for taking too much time, so the plateform goes for the shorter path


        //parameters neural network
        int numInputs = 6; // x_plateforme-x_ball, dim-y_ball, vx_ball, vy_ball, x_autre_plateforme et action
        readonly int numOutputs = 1;  // the reward of the action
        float[,] hiddenWeights;
        float[] hiddenBiases;
        float[,] outputWeights;
        float[] outputBiases;

        //parameters training neural network
        float learningRate = 0.01f;
        int numNeurons = 500;
        int numEpochs = 5;
        int num_games_max = 100000000;
        readonly double percentageLearning = 0.95; // 0, puis 0.2, puis 0.5, puis 0.7, puis 0.9
        readonly int speed = 2;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            InitializeNN(false);

            string jsonFileGames = File.ReadAllText("games.json");
            games = JsonSerializer.Deserialize<List<float>>(jsonFileGames);
            UpdateGraphBounces();
        }

        private void InitializeGame()
        {
            // =================================== Initialization de la balle
            Random rnd = new Random();
            PlateformeUp = new Brick(rnd.Next(dim), 0, 100, 10); //x, y, width, height
            PlateformeDown = new Brick(rnd.Next(dim), dim, 100, 10); //x, y, width, height
            Ball = new Brick(rnd.Next(dim), rnd.Next(2)*(dim-2)+1, 20); // balle part soit d'en haut soit d'en bas
            vx = rnd.Next(3) - 1;
            if (Ball.Y < dim / 2)
            {
                vy = 1;
            }
            else
            {
                vy = -1;
            }


            // =================================== Initialization de la liste des bricks
            this.bricks = new List<Brick>();
            if (brick)
            {
                Brush[] couleurs = new Brush[] { Brushes.OrangeRed, Brushes.LightSalmon, Brushes.LightYellow, Brushes.LightGreen, Brushes.LightBlue, Brushes.Lavender };

                //for (int i = 0; i < couleurs.Length; i++) //nb de lignes
                //{
                //    if (i % 2 == 0) // si i pair alors les lignes pas decalees (pour faire en quinconce)
                //    {
                //        for (int j = 0; j < 5; j++) //nb de briques par ligne, 5 car 320/64 (largeur du cadre/largeur d'une brique)
                //        {
                //            Brick b = new Brick(j * 64, i * 20); //x, y, largeur, hauteur
                //            b.color = couleurs[i];
                //            bricks.Add(b);
                //        }
                //    }
                //    else
                //    {
                //        for (int j = 0; j < 7; j++)
                //        {
                //            Brick b = new Brick(j * 64 - 32, i * 20);
                //            b.color = couleurs[i];
                //            bricks.Add(b);
                //        }
                //    }
                //}


                // 3 petites briques
                Brick b1 = new Brick(0, 0); b1.Color = couleurs[4];
                Brick b2 = new Brick(100, 0); b2.Color = couleurs[4];
                Brick b3 = new Brick(200, 100); b3.Color = couleurs[4];
                this.bricks.Add(b1);
                this.bricks.Add(b2);
                this.bricks.Add(b3);

                numInputs += bricks.Count;
            }
        }

        public void InitializeNN(bool initialize)
        {
            //parameters neural network
            this.hiddenWeights = new float[numInputs, numNeurons];
            this.hiddenBiases = new float[numNeurons];
            this.outputWeights = new float[numNeurons, numOutputs];
            this.outputBiases = new float[numOutputs];

            string JSONhiddenWeights;
            List<List<float>> hiddenWeightsList;
            string JSONhiddenBiases;
            List<float> hiddenBiasesList;
            string JSONoutputWeights;
            List<List<float>> outputWeightsList;
            string JSONoutputBiases;
            List<float> outputBiasesList;

            if (initialize)
            {
                // we assign random values
                Random weightRand = new Random();
                Random biasRand = new Random();
                for (int i = 0; i < numInputs; i++)
                {
                    for (int j = 0; j < numNeurons; j++)
                    {
                        hiddenWeights[i, j] = (float)weightRand.NextDouble() * 2 - 1; //initialiser pas au pif
                        //hiddenWeights[i, j] = (float)(speed*(weightRand.NextDouble() < 0.5 ? -1 : weightRand.NextDouble()) * weightRand.NextDouble() * Math.Sqrt(1.0 / numNeurons));
                    }
                }
                for (int i = 0; i < numNeurons; i++)
                {
                    hiddenBiases[i] = (float)biasRand.NextDouble() * 2 - 1;
                    //hiddenBiases[i] = (float)(speed * (biasRand.NextDouble() < 0.5 ? -1 : biasRand.NextDouble()) * biasRand.NextDouble() * Math.Sqrt(1.0 / numNeurons));
                }
                for (int i = 0; i < numNeurons; i++)
                {
                    for (int j = 0; j < numOutputs; j++)
                    {
                        outputWeights[i, j] = (float)weightRand.NextDouble() * 2 - 1;
                        //outputWeights[i, j] = (float)(speed * (weightRand.NextDouble() < 0.5 ? -1 : weightRand.NextDouble()) * weightRand.NextDouble() * Math.Sqrt(1.0 / numNeurons));
                    }
                }
                for (int i = 0; i < numOutputs; i++)
                {
                    outputBiases[i] = (float)biasRand.NextDouble() * 2 - 1;
                    //outputBiases[i] = (float)(speed * (biasRand.NextDouble() < 0.5 ? -1 : biasRand.NextDouble()) * biasRand.NextDouble() * Math.Sqrt(1.0 / numNeurons));
                }
            }
            else
            {
                JSONhiddenWeights = File.ReadAllText("NN/hiddenWeights.json");
                hiddenWeightsList = JsonSerializer.Deserialize<List<List<float>>>(JSONhiddenWeights);
                for (int i = 0; i < numInputs; i++)
                {
                    for (int j = 0; j < numNeurons; j++)
                    {
                        hiddenWeights[i, j] = hiddenWeightsList[i][j];
                    }
                }

                JSONhiddenBiases = File.ReadAllText("NN/hiddenBiases.json");
                hiddenBiasesList = JsonSerializer.Deserialize<List<float>>(JSONhiddenBiases);
                hiddenBiases = hiddenBiasesList.ToArray();

                JSONoutputWeights = File.ReadAllText("NN/outputWeights.json");
                outputWeightsList = JsonSerializer.Deserialize<List<List<float>>>(JSONoutputWeights);
                for (int i = 0; i < numNeurons; i++)
                {
                    for (int j = 0; j < numOutputs; j++)
                    {
                        outputWeights[i, j] = outputWeightsList[i][j];
                    }
                }

                JSONoutputBiases = File.ReadAllText("NN/outputBiases.json");
                outputBiasesList = JsonSerializer.Deserialize<List<float>>(JSONoutputBiases);
                outputBiases = outputBiasesList.ToArray();
            }

            hiddenWeightsList = new List<List<float>>();
            for (int i = 0; i < numInputs; i++)
            {
                List<float> innerList = new List<float>();
                for (int j = 0; j < numNeurons; j++)
                {
                    innerList.Add(hiddenWeights[i, j]);
                }
                hiddenWeightsList.Add(innerList);
            }
            JSONhiddenWeights = JsonSerializer.Serialize(hiddenWeightsList);
            File.WriteAllText("NN/hiddenWeights.json", JSONhiddenWeights);

            hiddenBiasesList = hiddenBiases.ToList();
            JSONhiddenBiases = JsonSerializer.Serialize(hiddenBiasesList);
            File.WriteAllText("NN/hiddenBiases.json", JSONhiddenBiases);

            outputWeightsList = new List<List<float>>();
            for (int i = 0; i < numNeurons; i++)
            {
                List<float> innerList = new List<float>();
                for (int j = 0; j < numOutputs; j++)
                {
                    innerList.Add(outputWeights[i, j]);
                }
                outputWeightsList.Add(innerList);
            }
            JSONoutputWeights = JsonSerializer.Serialize(outputWeightsList);
            File.WriteAllText("NN/outputWeights.json", JSONoutputWeights);

            outputBiasesList = outputBiases.ToList();
            JSONoutputBiases = JsonSerializer.Serialize(outputBiasesList);
            File.WriteAllText("NN/outputBiases.json", JSONoutputBiases);
        }

        public float PlayGame(Brick plateforme, bool train = false)
        {
            bool hasCollidedWithBrick = false; //pour parer le cas ou la balle touche 2 briques en mm temps
            Ball.X = (int)(Ball.X + speed * vx);
            Ball.Y = (int)(Ball.Y + speed * vy);
            float reward = 0;

            if (plateforme.X > dim)
            {
                plateforme.X = dim;
            }
            else if (plateforme.X < 0)
            {
                plateforme.X = 0;
            }

            //collisions murs + on verifie que la balle sort pas du cadre
            if (Ball.X >= dim) //si touche les bords, on change sa vitesse de signe
            {
                Ball.X = dim;
                vx = -vx;
            }
            else if (Ball.X <= 0)
            {
                Ball.X = 0;
                vx = -vx;
            }

            if (Ball.Y <= 0) //touche le plafond
            {
                Ball.Y = 0;
                if (plateforme == PlateformeUp && PlateformeUp.CollidesWith(Ball.X, Ball.Y, Ball.Width)) // plateforme rattrape balle
                {
                    float dist_au_milieu = (float)(Ball.X + Ball.Width / 2 - (PlateformeUp.X + PlateformeUp.Width / 2)) / 40.0f;
                    if (dist_au_milieu > 1)
                    {
                        vx = 1;
                    }
                    else if (dist_au_milieu < -1)
                    {
                        vx = -1;
                    }
                    else
                    {
                        vx = (float)dist_au_milieu;
                    }
                    vy = -vy;
                    reward += bounce;
                }
                else if (!PlateformeUp.CollidesWith(Ball.X, Ball.Y, Ball.Width))
                {
                    if (plateforme == PlateformeUp)
                    {
                        reward += loose;
                    }
                    else if (plateforme == PlateformeDown)
                    {
                        reward += win;
                    }
                }
            }
            else if (Ball.Y >= dim) //balle tout en bas
            {
                Ball.Y = dim;
                if (plateforme == PlateformeDown && PlateformeDown.CollidesWith(Ball.X, Ball.Y, Ball.Width)) // plateforme rattrape balle
                {
                    float dist_au_milieu = (float)(Ball.X + Ball.Width / 2 - (PlateformeDown.X + PlateformeDown.Width / 2)) / 40.0f;
                    if (dist_au_milieu > 1)
                    {
                        vx = 1;
                    }
                    else if (dist_au_milieu < -1)
                    {
                        vx = -1;
                    }
                    else
                    {
                        vx = (float)dist_au_milieu;
                    }
                    vy = -vy;
                    reward += bounce;
                }
                else if (!PlateformeDown.CollidesWith(Ball.X, Ball.Y, Ball.Width))
                {
                    if (plateforme == PlateformeDown)
                    {
                        reward += loose;
                    }
                    else if (plateforme == PlateformeUp)
                    {
                        reward += win;
                    }
                }
            }

            // collisions avec les bricks
            if (brick)
            {
                foreach (Brick b in bricks)
                {
                    // si la brique est vivante et touchee : elle devient morte, disparait et la balle rebondit
                    if (b.IsAlive && b.CollidesWith(Ball.X, Ball.Y, Ball.Width) && !hasCollidedWithBrick)
                    {
                        hasCollidedWithBrick = true;
                        vy = -vy;
                        b.IsAlive = false;
                        b.Color = Brushes.Transparent;
                        b.Contour = Pens.Transparent;
                        reward = breakBrick;
                    }
                }
            }


            //affichage si c'est pas pour du training
            if (!train)
            {
                //Thread.Sleep(1);
                pictureBoxGame.Refresh();

                arrayCoord1.Text = "X = " + Ball.X.ToString() + "    Y = " + Ball.Y.ToString() + "    VX = " + vx.ToString("0.00") + "    VY = " + vy.ToString("0.00") + "                X_Plateforme_Down = " + PlateformeDown.X.ToString() + "    X_Plateforme_Up = " + PlateformeUp.X.ToString();

                if (reward != 0)
                {
                    cumulRewards.Text = "Last reward = " + reward;
                    cumulRewards.Refresh();
                }


                if (reward <= loose)
                {
                    using (Graphics g = pictureBoxGame.CreateGraphics())
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        g.DrawString("END OF THE MATCH", new Font("Consolas", 35), Brushes.White, pictureBoxGame.ClientRectangle, stringFormat);
                    }
                    timerLoop.Enabled = false;
                }
                else if (Victory())
                {
                    // "VICTORY" au milieu de la pictureBox
                    using (Graphics g = pictureBoxGame.CreateGraphics())
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        g.DrawString("VICTORY", new Font("Consolas", 35), Brushes.White, pictureBoxGame.ClientRectangle, stringFormat);
                    }
                    timerLoop.Enabled = false;
                }
            }
            return reward;
        }

        public bool Victory()
        {
            // en cas de victoire
            bool allBricksDead = true;
            if (brick)
            {
                foreach (Brick brick in bricks)
                {
                    if (brick.IsAlive)
                    {
                        allBricksDead = false;
                        break;
                    }
                }
            }
            else
            {
                allBricksDead = false;
            }
            return allBricksDead;
        }

        private List<float> StateBricks()
        {
            List<float> stateBricks = new List<float>();
            if (brick)
            {
                foreach (Brick brick in bricks)
                {
                    stateBricks.Add(Convert.ToInt32(brick.IsAlive) * 6 - 3); // -3 si mort, 3 si vvt
                }
            }
            return stateBricks;
        }

        private int ChoseMove(Brick Plateforme, bool train) // return -1, 0 ou 1 * speed
        {
            int move;
            List<float> input;
            if (Plateforme == PlateformeDown)
            {
                input = new List<float> { Plateforme.X - Ball.X, dim - Ball.Y, vx, vy, PlateformeUp.X - Ball.X }; // tend vers 0, tend vers 0
            }
            else
            {
                input = new List<float> { Math.Abs(dim- Plateforme.X) - Math.Abs(dim - Ball.X), dim - Math.Abs(dim - Ball.Y), -vx, -vy, Math.Abs(dim - PlateformeDown.X) - Math.Abs(dim - Ball.X) };
            }
            input.AddRange(StateBricks());

            //choix de l'action
            Random rnd = new Random();
            float p = (float)rnd.NextDouble();
            if (!train || (train && p < percentageLearning)) // exploitation ou entrainement et apprentissage dans 70% des cas
            {
                float[] outputPrediction = NeuralNetwork(false, input);
                move = (int)Array.IndexOf(outputPrediction, outputPrediction.Max()) - 1; // index de l'action avec le plus de reward
                if (!train)
                {
                    ValuePredictions.Text = outputPrediction[0].ToString("0.000") + "                          " + outputPrediction[1].ToString("0.000") + "                        " + outputPrediction[2].ToString("0.000");
                    ValuePredictions.Refresh();
                    Updatepanel(outputPrediction, Plateforme);
                }
            }
            else //random
            {
                move = rnd.Next(3) - 1; //left = -1, stationnary = 0, right = 1
            }

            if ((move == -1 && Plateforme.X <= 0) || (move == 1 && Plateforme.X >= dim) || (move != -1 && move != 1)) //on check que la balle n'est pas aux extremites et que move vaut -1 0 ou 1
            {
                move = 0;
            }
            return 2*speed * move;
        }

        #endregion


        #region ======================================================= Neural network ==================================================
        // Réseau de neurones simple a 1 layer : 
        // - prenant en entrée l'etat actuel de l'interface (les coordonnees de la balle, sa vitesse, les coordonnees de la plateforme) ainsi qu'une action que peut faire la plateforme
        // - prédit le reward de l'action en question
        // --> permettra de choisir le reward le plus eleve pour savoir ce que dois faire la plateforme pour rattraper la balle (-1 = left, 0 = stationary, 1 = right)



        static public float Sigmoid(float x)
        {
            // activation function sigmoid (we chose this one because generate a number btw 0 and 1)
            return (float)(1 / (1 + Math.Exp(-x)));
        }

        static public float Deriv(float x, Func<float, float> f)
        {
            // Define the derivative of the activation function (for backpropagation)
            return f(x) * (1 - f(x));
        }

        public float[] NeuralNetwork(bool train, List<float> input, List<float> targetOutput = null)
        {
            // input = x_plateform-x_ball, y_plateform-y_ball, vx_ball, vy_ball, x_plateforme, action
            float[] outputPrediction = new float[3] { 0, 0, 0 }; //outputLeft, outputStationary, outputRight

            // Normalize btw -3 and 3 the inputs
            input[0] = 6 * (input[0] + dim) / (2 * dim) - 3; //x_plateform-x_ball, min -dim max dim
            input[1] = 6 * (input[1] / dim) - 3; //y_plateform-y_ball, min 0 max dim
            input[2] *= 3; //vx, min -1 max 1
            input[3] *= 3; //vy, min -1 max 1
            input[4] = 6 * (input[4] + dim) / (2 * dim) - 3; //x_autre_plateforme, min 0 max dim

            if (train && targetOutput != null)
            {
                // Normalize btw -3 and 3
                input[numInputs - 1] *= 3 / speed; //action, -1 0 1

                // Normalize btw 0 and 1
                float valMin = loose + time + moved;
                float valMax = win;
                for (int i = 0; i < numOutputs; i++)
                {
                    targetOutput[i] = (targetOutput[i] - valMin) / (valMax - valMin); //reward de l'action, normalise entre 0 et 1
                }


                // Train w/ backpropagation
                for (int epoch = 0; epoch < numEpochs; epoch++)
                {
                    // Forward pass
                    float[] hiddenLayer = new float[numNeurons];
                    for (int j = 0; j < numNeurons; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < numInputs; k++)
                        {
                            sum += input[k] * hiddenWeights[k, j];
                        }
                        sum += hiddenBiases[j];
                        hiddenLayer[j] = Sigmoid(sum);
                    }

                    float[] output = new float[numOutputs];
                    for (int j = 0; j < numOutputs; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < numNeurons; k++)
                        {
                            sum += hiddenLayer[k] * outputWeights[k, j];
                        }
                        sum += outputBiases[j];
                        output[j] = Sigmoid(sum);
                    }

                    // Backward pass
                    float[] outputErrors = new float[numOutputs];
                    for (int j = 0; j < numOutputs; j++)
                    {
                        outputErrors[j] = (targetOutput[j] - output[j]) * Deriv(output[j], Sigmoid);
                    }
                    float[] hiddenErrors = new float[numNeurons];
                    for (int j = 0; j < numNeurons; j++)
                    {
                        float weightedErrorSum = 0;
                        for (int k = 0; k < numOutputs; k++)
                        {
                            weightedErrorSum += outputErrors[k] * outputWeights[j, k];
                        }
                        hiddenErrors[j] = weightedErrorSum * Deriv(hiddenLayer[j], Sigmoid);
                    }


                    // Update weights and biases
                    for (int j = 0; j < numNeurons; j++)
                    {
                        for (int k = 0; k < numOutputs; k++)
                        {
                            outputWeights[j, k] += learningRate * outputErrors[k] * hiddenLayer[j];
                            outputBiases[k] += learningRate * outputErrors[k];
                        }
                    }
                    for (int j = 0; j < numInputs; j++)
                    {
                        for (int k = 0; k < numNeurons; k++)
                        {
                            hiddenWeights[j, k] += learningRate * hiddenErrors[k] * input[j];
                            hiddenBiases[k] += learningRate * hiddenErrors[k];
                        }
                    }
                }
            }


            else //train = false, prediction
            {
                //on fait 3 predictions du reward pour les 3 actions pour trouver celle avec le plus haut reward
                List<float> inputLeft = input.ToList(); inputLeft.Add(-3);
                List<float> inputStationary = input.ToList(); inputStationary.Add(0);
                List<float> inputRight = input.ToList(); inputRight.Add(3);

                List<List<float>> inputPrediction = new List<List<float>> { inputLeft, inputStationary, inputRight };

                for (int i = 0; i < inputPrediction.Count; i++)
                {
                    // Forward pass
                    float[] hiddenLayer = new float[numNeurons];
                    for (int j = 0; j < numNeurons; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < numInputs; k++)
                        {
                            sum += inputPrediction[i][k] * hiddenWeights[k, j];
                        }
                        sum += hiddenBiases[j];
                        hiddenLayer[j] = Sigmoid(sum);
                    }

                    // Prediction
                    float predict = 0;
                    for (int j = 0; j < numOutputs; j++)
                    {
                        for (int k = 0; k < numNeurons; k++)
                        {
                            predict += hiddenLayer[k] * outputWeights[k, j];
                        }
                        predict += outputBiases[j];
                    }

                    outputPrediction[i] = Sigmoid(predict);
                }
            }

            return outputPrediction; //retourne les predictions pour les deplacements -1 0 ou 1
        }



        #endregion


        #region ======================================================= Exploration =====================================================


        public void Exploration()
        {
            using (Graphics g = pictureBoxGame.CreateGraphics())
            {
                g.Clear(Color.Black);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                g.DrawString($"TRAINING ...", new Font("Consolas", 30), Brushes.White, pictureBoxGame.ClientRectangle, stringFormat);
            }

            //games = new List<float>();
            float boucePerGame = 0;
            float winPerGame = 0;
            float num_games = 0;
            while (num_games <= num_games_max)
            {
                num_games++;
                InitializeGame();
                float bouce = 0;

                if (num_games % 500 == 0)
                {
                    trainingNB.Text = "Training number :   " + num_games.ToString() + " / " + num_games_max.ToString();
                    trainingNB.Refresh();
                    bouncePerGame.Text = "Moyenne de bounce par game : " + (boucePerGame / num_games).ToString("0.000");
                    bouncePerGame.Refresh();
                }

                while (bouce<10) //max 5 bounces par game sinon peut s'eterniser
                {
                    //choix de l'action, puis on determine le reward
                    List<float> inputDown = new List<float> { PlateformeDown.X - Ball.X, dim - Ball.Y, vx, vy, PlateformeUp.X - Ball.X };
                    inputDown.AddRange(StateBricks());
                    List<float> inputUp = new List<float> { Math.Abs(dim - PlateformeUp.X) - Math.Abs(dim - Ball.X), dim - Math.Abs(dim - Ball.Y), -vx, -vy, Math.Abs(dim - PlateformeDown.X) - Math.Abs(dim - Ball.X) };
                    inputUp.AddRange(StateBricks());

                    int moveUp = ChoseMove(PlateformeUp, true); // train = true
                    int moveDown = ChoseMove(PlateformeDown, true); // train = true
                    PlateformeUp.X -= moveUp;
                    PlateformeDown.X += moveDown;
                    inputUp.Add(moveUp);
                    inputDown.Add(moveDown);
                    float rewardUp = PlayGame(PlateformeUp, true);
                    float rewardDown = PlayGame(PlateformeDown, true);

                    Random rnd = new Random();
                    float p = (float)rnd.NextDouble();
                    if (rewardUp != 0 || p < 0.0005) //on met a jour que si le reward vaut pas 0 ou dans 5% des cas de 0
                    {
                        float ajusted_reward_Up = rewardUp + time;
                        if (moveUp != 0)
                        {
                            ajusted_reward_Up += moved;
                        }
                        List<float> targetOutput_Up = new List<float>() { ajusted_reward_Up };

                        //on met a jour le NN apres avoir joue
                        NeuralNetwork(true, inputUp, targetOutput_Up); // train = true
                    }
                    if (rewardDown != 0 || p < 0.01) //on met a jour que si le reward vaut pas 0 ou dans 5% des cas de 0
                    {
                        float ajusted_reward_Down = rewardDown + time;
                        if (moveDown != 0)
                        {
                            ajusted_reward_Down += moved;
                        }
                        List<float> targetOutput_Down = new List<float>() { ajusted_reward_Down };

                        //on met a jour le NN apres avoir joue
                        NeuralNetwork(true, inputDown, targetOutput_Down); // train = true
                    }

                    if (rewardDown == bounce || rewardUp == bounce) { boucePerGame++; bouce++; }
                    if (rewardUp == win || rewardDown == win) { winPerGame++; }
                    if (rewardUp == loose || rewardDown == loose || Victory()) { break; }
                }

                if (num_games % 10000 == 0 || num_games == num_games_max) // save the weights etc in the JSON files
                {

                    List<List<float>> hiddenWeightsList = new List<List<float>>();
                    for (int i = 0; i < numInputs; i++)
                    {
                        List<float> innerList = new List<float>();
                        for (int j = 0; j < numNeurons; j++)
                        {
                            innerList.Add(hiddenWeights[i, j]);
                        }
                        hiddenWeightsList.Add(innerList);
                    }
                    string JSONhiddenWeights = JsonSerializer.Serialize(hiddenWeightsList);
                    File.WriteAllText("NN/hiddenWeights.json", JSONhiddenWeights);

                    List<float> hiddenBiasesList = hiddenBiases.ToList();
                    string JSONhiddenBiases = JsonSerializer.Serialize(hiddenBiasesList);
                    File.WriteAllText("NN/hiddenBiases.json", JSONhiddenBiases);

                    List<List<float>> outputWeightsList = new List<List<float>>();
                    for (int i = 0; i < numNeurons; i++)
                    {
                        List<float> innerList = new List<float>();
                        for (int j = 0; j < numOutputs; j++)
                        {
                            innerList.Add(outputWeights[i, j]);
                        }
                        outputWeightsList.Add(innerList);
                    }
                    string JSONoutputWeights = JsonSerializer.Serialize(outputWeightsList);
                    File.WriteAllText("NN/outputWeights.json", JSONoutputWeights);

                    List<float> outputBiasesList = outputBiases.ToList();
                    string JSONoutputBiases = JsonSerializer.Serialize(outputBiasesList);
                    File.WriteAllText("NN/outputBiases.json", JSONoutputBiases);
                }
            }

            UpdateGraphBounces();
            timerLoop.Enabled = false;

            using (Graphics g = pictureBoxGame.CreateGraphics())
            {
                g.Clear(Color.Black);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                g.DrawString("Training complete", new Font("Consolas", 30), Brushes.White, pictureBoxGame.ClientRectangle, stringFormat);
            }
        }


        private void ExplorationButton_Click(object sender, EventArgs e)
        {
            Exploration();
        }


        #endregion


        #region ======================================================= Exploitation ====================================================

        public int Exploitation(bool training = false)
        {
            InitializeGame();
            int limitBounces = 20;
            int comptBounces = 0;

            while (!Victory() && comptBounces < limitBounces)
            {
                List<float> inputDown = new List<float> { PlateformeDown.X - Ball.X, dim - Ball.Y, vx, vy, PlateformeUp.X - Ball.X };
                inputDown.AddRange(StateBricks());
                List<float> inputUp = new List<float> { Math.Abs(dim - PlateformeUp.X) - Math.Abs(dim - Ball.X), dim - Math.Abs(dim - Ball.Y), -vx, -vy, Math.Abs(dim - PlateformeDown.X) - Math.Abs(dim - Ball.X) };
                inputUp.AddRange(StateBricks());

                int moveUp = ChoseMove(PlateformeUp, false); // train = false
                int moveDown = ChoseMove(PlateformeDown, false); // train = false
                PlateformeUp.X -= moveUp;
                PlateformeDown.X += moveDown;
                float rewardUp = PlayGame(PlateformeUp, training);
                float rewardDown = PlayGame(PlateformeDown, training);


                if (rewardUp == loose || rewardDown == loose) // si game over
                {
                    break;
                }
                else if (rewardUp == bounce || rewardDown == bounce) //balle rattrapee
                {
                    comptBounces += 1;
                }
                if (!training)
                {
                    UpdateChart();

                    if (rewardUp != 0 || rewardDown != 0)
                    {
                        cumulRewards.Text = "Last reward up = " + rewardUp + "; Last reward down = " + rewardDown;
                        cumulRewards.Refresh();
                    }

                    labelBounces.Text = "Number of bounces : " + comptBounces.ToString() + " / " + limitBounces.ToString();
                    labelBounces.Refresh();

                    buttonStop.Refresh();
                    buttonGo.Refresh();
                    Application.DoEvents();

                    while (pauseBool)
                    {
                        Application.DoEvents();
                        buttonStop.Refresh();
                        buttonGo.Refresh();
                    }
                }
                //bricksPerGame.Text = comptBounces.ToString();
                //bricksPerGame.Refresh();
            }
            if (!training)
            {
                games.Add(comptBounces);
                string jsonG = JsonSerializer.Serialize(this.games);
                File.WriteAllText("games.json", jsonG);
                UpdateGraphBounces();
            }

            return comptBounces;
        }

        private void ExploitationButton_Click(object sender, EventArgs e)
        {
            Exploitation();
        }

        #endregion


        #region ======================================================= Test parameters =================================================


        private void FindBestParams()
        {
            // ========================== test pour trouver les meilleurs parametres =================================
            List<int> listNbNeurons = new List<int>() { 110, 120, 130, 140, 150 };
            List<float> listLR = new List<float>() { 0.1f };
            List<int> listNbEpochs = new List<int>() { 5 };
            List<int> listNbGames = new List<int>() { 100000 };
            int nbEntrainements = 1;

            //on initialise la liste games
            games = new List<float>();

            foreach (int nbNeurons in listNbNeurons)
            {
                this.numNeurons = nbNeurons;
                foreach (float lr in listLR)
                {
                    this.learningRate = lr;
                    foreach (int epochs in listNbEpochs)
                    {
                        this.numEpochs = epochs;
                        foreach (int nbGames in listNbGames)
                        {
                            this.num_games_max = nbGames;
                            float moy = 0;
                            for (int i = 0; i < nbEntrainements; i++) // faire avec nbEntrainements entrainements differents
                            {
                                InitializeNN(true);
                                textBox1.Text = "nbNeurons = " + nbNeurons + ",                       lr = " + lr + ",                        epochs = " + epochs + ",                       entrainement n'" + (i + 1) + "/" + nbEntrainements;
                                textBox1.Refresh();
                                Exploration();
                                for (int j = 0; j < 20; j++) // 20 parties
                                {
                                    moy += Exploitation(true); // on additionne le nombre de bounces fait par partie
                                }
                            }
                            games.Add(moy / (nbEntrainements * 20)); //nombre de bounces en moyenne par game
                            string jsonG = JsonSerializer.Serialize(this.games);
                            File.WriteAllText("games.json", jsonG);
                            UpdateGraphBounces();
                        }
                    }
                }
            }
        }


        private void buttonBestParam_Click(object sender, EventArgs e)
        {
            FindBestParams();
            UpdateGraphBounces();
        }

        #endregion


        #region ======================================================= Parametres FORM =================================================

        //======================================================= timers
        private void timerLoop_Tick(object sender, EventArgs e)
        {
            if (vy > 0)
            {
                PlayGame(PlateformeDown, false);
            }
            else
            {
                PlayGame(PlateformeUp, false);
            }
        }


        //======================================================= buttons
        private void Play_Click(object sender, EventArgs e)
        {
            InitializeGame();
            timerLoop.Enabled = true;

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            pauseBool = true;
        }


        private void buttonGo_Click(object sender, EventArgs e)
        {
            pauseBool = false;
        }

        private void buttonNextState_Click(object sender, EventArgs e)
        {
            if (vy > 0)
            {
                PlayGame(PlateformeDown, false);
                UpdateChart();
            }
            else
            {
                PlayGame(PlateformeUp, false);
                UpdateChart();
            }

        }

        private void buttonReinitGame_Click(object sender, EventArgs e)
        {
            games = new List<float>();
            string jsonG = JsonSerializer.Serialize(this.games);
            File.WriteAllText("games.json", jsonG);
            UpdateGraphBounces();
            chartLastGames.Refresh();
        }

        //======================================================= Déplacer la plateforme avec la souris

        private void pictureBoxGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (vy > 0)
            {
                PlateformeDown.X = e.X;
            }
            else
            {
                PlateformeUp.X = e.X;
            }
            cumulRewards.Refresh();
        }

        //======================================================= affichage
        private void pictureBoxGame_Paint(object sender, PaintEventArgs e)
        {
            // Dessiner cadrillage
            Pen whitePen = new Pen(Color.White, 1);
            //e.Graphics.DrawLine(whitePen, Plateforme.X + Plateforme.Width / 7, 0, Plateforme.X + Plateforme.Width / 7, pictureBoxGame.Height);
            //e.Graphics.DrawLine(whitePen, Plateforme.X + 2 * Plateforme.Width / 7, 0, Plateforme.X + 2 * Plateforme.Width / 7, pictureBoxGame.Height);
            //e.Graphics.DrawLine(whitePen, Plateforme.X + 3 * Plateforme.Width / 7, 0, Plateforme.X + 3 * Plateforme.Width / 7, pictureBoxGame.Height);
            //e.Graphics.DrawLine(whitePen, Plateforme.X + 4 * Plateforme.Width / 7, 0, Plateforme.X + 4 * Plateforme.Width / 7, pictureBoxGame.Height);

            //e.Graphics.DrawLine(whitePen, Math.Abs(Ball.X - (Plateforme.X + Plateforme.Width / 2)), 0, Math.Abs(Ball.X - (Plateforme.X + Plateforme.Width / 2)), pictureBoxGame.Height);
            //e.Graphics.DrawLine(whitePen, Plateforme.X + Plateforme.Width / 2, 0, Plateforme.X + Plateforme.Width / 2, pictureBoxGame.Height);

            //for (int i = 0; i < 320; i += 35)
            //{
            //    e.Graphics.DrawLine(whitePen, i, 0, i, pictureBoxGame.Height);
            //    e.Graphics.DrawLine(whitePen, 0, i, pictureBoxGame.Width, i);
            //}

            // Dessin bricks
            foreach (Brick brick in bricks)
            {
                e.Graphics.FillRectangle(brick.Color, (float)brick.X, (float)brick.Y, brick.Width, brick.Height);
                e.Graphics.DrawRectangle(brick.Contour, (float)brick.X, (float)brick.Y, brick.Width, brick.Height);
            }

            // Dessin balle
            e.Graphics.FillEllipse(Ball.Color, (float)Ball.X, (float)Ball.Y, Ball.Width, Ball.Width);

            // dessin plateforme
            e.Graphics.FillRectangle(PlateformeDown.Color, (float)(PlateformeDown.X), (float)PlateformeDown.Y + 15, PlateformeDown.Width, PlateformeDown.Height);
            e.Graphics.FillRectangle(PlateformeUp.Color, (float)(PlateformeUp.X), (float)PlateformeUp.Y, PlateformeUp.Width, PlateformeUp.Height);
        }


        //======================================================= updates

        private void UpdateGraphBounces()
        {
            if (games.Count() > 40)
            {
                List<float> valeurs = games.Skip(Math.Max(0, games.Count() - 40)).Take(40).ToList();
                chartLastGames.Series["Bounces"].Points.DataBindY(valeurs);
            }
            else if (games.Count() > 0)
            {
                chartLastGames.Series["Bounces"].Points.DataBindY(games);
            }
            chartLastGames.Refresh();
        }

        private void UpdateChart()
        {
            List<float> gradientLeftDown = new List<float>();
            List<float> gradientStationnaryDown = new List<float>();
            List<float> gradientRightDown = new List<float>();

            for (int x_plateform = 0; x_plateform <= dim; x_plateform += 20) //on parcourt les abscisses de la plateforme
            {
                List<float> inputDown = new List<float> { x_plateform - Ball.X, dim - Ball.Y, vx, vy, PlateformeUp.X - Ball.X };
                inputDown.AddRange(StateBricks());
                float[] outputPredictionDown = NeuralNetwork(false, inputDown);

                gradientLeftDown.Add((float)outputPredictionDown[0]);
                gradientStationnaryDown.Add((float)outputPredictionDown[1]);
                gradientRightDown.Add((float)outputPredictionDown[2]);
            }

            List<float> gradientLeftUp = new List<float>();
            List<float> gradientStationnaryUp = new List<float>();
            List<float> gradientRightUp = new List<float>();

            for (int x_plateform = 0; x_plateform <= dim; x_plateform += 20) //on parcourt les abscisses de la plateforme
            {
                List<float> inputUp = new List<float> { Math.Abs(dim - x_plateform) - Math.Abs(dim - Ball.X), dim - Math.Abs(dim - Ball.Y), -vx, -vy, Math.Abs(dim - PlateformeDown.X) - Math.Abs(dim - Ball.X) };
                inputUp.AddRange(StateBricks());
                float[] outputPredictionUp = NeuralNetwork(false, inputUp);

                gradientLeftUp.Add((float)outputPredictionUp[0]);
                gradientStationnaryUp.Add((float)outputPredictionUp[1]);
                gradientRightUp.Add((float)outputPredictionUp[2]);
            }


            chart1.Series.Clear();
            // Ajouter les trois séries de données au graphique
            chart1.Series.Add("Left");
            chart1.Series["Left"].ChartType = SeriesChartType.Spline;
            chart1.Series["Left"].Color = Color.Green;

            chart1.Series.Add("Stationnary");
            chart1.Series["Stationnary"].ChartType = SeriesChartType.Spline;
            chart1.Series["Stationnary"].Color = Color.Blue;

            chart1.Series.Add("Right");
            chart1.Series["Right"].ChartType = SeriesChartType.Spline;
            chart1.Series["Right"].Color = Color.Red;

            for (int i = 0; i <= dim / 20; i++)
            {
                chart1.Series["Stationnary"].Points.AddXY(i * 20, gradientStationnaryDown[i]);
                chart1.Series["Left"].Points.AddXY(i * 20, gradientLeftDown[i]);
                chart1.Series["Right"].Points.AddXY(i * 20, gradientRightDown[i]);
            }

            // Masquer les lignes de grille principales de l'axe des abscisses
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            // Masquer les lignes de grille principales de l'axe des ordonnées
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            // Masquer les graduations de l'axe des abscisses
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            // Masquer les graduations de l'axe des ordonnées
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;




            chart2.Series.Clear();
            // Ajouter les trois séries de données au graphique
            chart2.Series.Add("Left");
            chart2.Series["Left"].ChartType = SeriesChartType.Spline;
            chart2.Series["Left"].Color = Color.Green;

            chart2.Series.Add("Stationnary");
            chart2.Series["Stationnary"].ChartType = SeriesChartType.Spline;
            chart2.Series["Stationnary"].Color = Color.Blue;

            chart2.Series.Add("Right");
            chart2.Series["Right"].ChartType = SeriesChartType.Spline;
            chart2.Series["Right"].Color = Color.Red;

            for (int i = 0; i <= dim / 20; i++)
            {
                chart2.Series["Stationnary"].Points.AddXY(i * 20, gradientStationnaryUp[i]);
                chart2.Series["Left"].Points.AddXY(i * 20, gradientLeftUp[i]);
                chart2.Series["Right"].Points.AddXY(i * 20, gradientRightUp[i]);
            }

            // Masquer les lignes de grille principales de l'axe des abscisses
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            // Masquer les lignes de grille principales de l'axe des ordonnées
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            // Masquer les graduations de l'axe des abscisses
            chart2.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            // Masquer les graduations de l'axe des ordonnées
            chart2.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
        }

        private void Updatepanel(float[] outputPrediction, Brick Plateforme )
        {
            // Personnaliser l'apparence des panel
            if(Plateforme == PlateformeDown)
            {
                int colorValue0;
                int colorValue1;
                int colorValue2;
                if (outputPrediction[0] == outputPrediction.Max())
                {
                    colorValue0 = 255;
                }
                else if (outputPrediction[0] == outputPrediction.Min())
                {
                    colorValue0 = 0;
                }
                else
                {
                    colorValue0 = 120;
                }

                if (outputPrediction[1] == outputPrediction.Max())
                {
                    colorValue1 = 255;
                }
                else if (outputPrediction[1] == outputPrediction.Min())
                {
                    colorValue1 = 0;
                }
                else
                {
                    colorValue1 = 120;
                }

                if (outputPrediction[2] == outputPrediction.Max())
                {
                    colorValue2 = 255;
                }
                else if (outputPrediction[2] == outputPrediction.Min())
                {
                    colorValue2 = 0;
                }
                else
                {
                    colorValue2 = 120;
                }

                // Affecter les valeurs du vecteur "actions" aux panel
                panel0.BackColor = Color.FromArgb(255 - colorValue0, 255 - colorValue0, 255);
                panel1.BackColor = Color.FromArgb(255 - colorValue1, 255 - colorValue1, 255);
                panel2.BackColor = Color.FromArgb(255 - colorValue2, 255 - colorValue2, 255);

                panel0.Refresh();
                panel1.Refresh();
                panel2.Refresh();
            }
            else
            {
                // Personnaliser l'apparence des panel
                int colorValue3;
                int colorValue4;
                int colorValue5;
                if (outputPrediction[0] == outputPrediction.Max())
                {
                    colorValue3 = 255;
                }
                else if (outputPrediction[0] == outputPrediction.Min())
                {
                    colorValue3 = 0;
                }
                else
                {
                    colorValue3 = 120;
                }

                if (outputPrediction[1] == outputPrediction.Max())
                {
                    colorValue4 = 255;
                }
                else if (outputPrediction[1] == outputPrediction.Min())
                {
                    colorValue4 = 0;
                }
                else
                {
                    colorValue4 = 120;
                }

                if (outputPrediction[2] == outputPrediction.Max())
                {
                    colorValue5 = 255;
                }
                else if (outputPrediction[2] == outputPrediction.Min())
                {
                    colorValue5 = 0;
                }
                else
                {
                    colorValue5 = 120;
                }

                // Affecter les valeurs du vecteur "actions" aux panel
                panel3.BackColor = Color.FromArgb(255 - colorValue3, 255 - colorValue3, 255);
                panel4.BackColor = Color.FromArgb(255 - colorValue4, 255 - colorValue4, 255);
                panel5.BackColor = Color.FromArgb(255 - colorValue5, 255 - colorValue5, 255);

                panel3.Refresh();
                panel4.Refresh();
                panel5.Refresh();
            }


            
        }



        #endregion

    }
}


