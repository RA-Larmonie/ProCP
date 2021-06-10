using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.model_v2;
using System.Reflection;
using System.Drawing.Drawing2D;
using DocumentFormat.OpenXml.Spreadsheet;
//using System.Windows.Input;

namespace WindowsFormsApp3
{
    [Serializable]
    public partial class FormSim : Form
    {
        private static int width = 33;
        private static int height = 19;

        Node[,] grid = new Node[width, height];
        Node[,] tempGrid = new Node[width, height];

        public Airport airport;

        List<Node> Check_In = new List<Node>();
        List<Node> Check_Out = new List<Node>();
        List<Node> Obstacles = new List<Node>();

        List<List<Node>> Optimum_Paths = new List<List<Node>>();
        List<List<Mover>> movers = new List<List<Mover>>();


        Random rand = new Random();
        //Timer timer = new Timer();

        private Node.NodeType nodeType;
        private List<Thread> threads;

        public FormSim(Airport airport)
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.pictureBox1.Dock = DockStyle.Fill;
            populateGrid();
            threads = new List<Thread>();
            this.airport = airport;
            this.buttonStop.BackColor = System.Drawing.Color.IndianRed;
        }

        public FormSim()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            populateGrid();
            updateGrid();
            this.nodeType = Node.NodeType.free;
            threads = new List<Thread>();
            this.buttonStop.BackColor = System.Drawing.Color.IndianRed;
        }

        public void initAnimation()
        {
            this.DoubleBuffered = true;
            timer.Enabled = true;
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            timerFlow.Enabled = true;
            this.button3.Text = "Start Simulation";
            this.button3.BackColor = System.Drawing.Color.GreenYellow;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {        
            pictureBox1.Invalidate();
        }

        private void populateGrid()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {                                        
                    grid[i, j] = new Node(new Coordinate(i, j), Node.NodeType.free, 1);
                   
                }
            }
        }
        private void updateGrid()
        {
            pictureBox1.Invalidate();
        }       

        //start simulation
        private void button2_Click(object sender, EventArgs e)
        {
            if((Check_In.Count == Check_Out.Count) && Check_In.Count!=0 && Check_Out.Count!=0 && (Check_In.Count == airport.flights.Count && Check_Out.Count == airport.flights.Count)) 
            {
                btnStart.Enabled = false;
                btnEnd.Enabled = false;
                button2.Enabled = false;
                btnReset.Enabled = true;
                btnDelete.Enabled = false;
                buttonObstacle.Enabled = false;

                this.nodeType = Node.NodeType.free;

                this.OptimumPath();
            }
            else 
            {
                MessageBox.Show("Please add equal amount of check-in and check-out \n and make sure to have the same number of checks the same as flights");
                btnReset.Enabled = true;
            }            
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            // OG code goes here
            stopSim();
            // enable the buttons
        }

        private void stopSim()
        {
            foreach (Thread t in threads)
            {
                t.Interrupt();
            }
            // stop the luggauges
            timer.Stop();
            timerFlow.Stop();
            // enable the buttons
            btnStart.Enabled = false;
            btnEnd.Enabled = false;
            button2.Enabled = false;
            btnDelete.Enabled = false;
            buttonObstacle.Enabled = false;
            // puase the start button
            this.button3.Text = "Resume Simulation";
            this.button3.BackColor = System.Drawing.Color.Orange;
        }
       
        private void resetSim()
        {
            btnReset.Enabled = false;
            btnStart.Enabled = true;
            button2.Enabled = true;
            buttonObstacle.Enabled = true;
            btnEnd.Enabled = true;
            btnDelete.Enabled = true;
            pictureBox1.Invalidate();
            label2.Text = "";
            //stopSim();
            this.WindowState = FormWindowState.Maximized;
            populateGrid();
            updateGrid();
            this.nodeType = Node.NodeType.free;
            this.movers = new List<List<Mover>>();
            Optimum_Paths = new List<List<Node>>();
            Check_In = new List<Node>();
            Check_Out = new List<Node>();
            timerFlow.Stop();
            timer.Stop();
        }

        private void btnReset_Click(object sender, EventArgs e)
        { 
            resetSim();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            initAnimation();            
            timer.Start();
            timerFlow.Start();
        }

        #region PictureBox1 Events + Releated Methods
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bool check = false;
            int pbW = 50;
            int pbH = 50;
            if (this.nodeType == Node.NodeType.start)
            {
                int countA = 1;
                int countB = 1;
                for (int i = 0; i < width; i++)
                {                   
                    for (int j = 0; j < height; j++)
                    {                                                
                        Node n = this.grid[i, j];
                        int posX = countA * pbW;
                        int posY = countB * pbH;
               
                        if ((e.X <= posX && e.X >= (posX - pbW)) &&( e.Y >= (posY - pbW) && e.Y <= posY))
                        {                           
                            if (this.grid[i, j].Type == Node.NodeType.free)
                            {
                                this.grid[i, j].Type = Node.NodeType.start;
                                Check_In.Add(this.grid[i, j]);
                                check = true;
                                break;
                            }
                        }
                        countB++;
                    }
                    if (check==true)
                    {
                        break;
                    }
                    countA++;
                    countB = 1;
                }
                pictureBox1.Invalidate();
            }
            else if(this.nodeType==Node.NodeType.end)
            {
                int countA = 1;
                int countB = 1;
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Node n = this.grid[i, j];
                        int posX = countA * pbW;
                        int posY = countB * pbH;

                        if ((e.X <= posX && e.X >= (posX - pbW)) && (e.Y >= (posY - pbW) && e.Y <= posY))
                        {
                            if (this.grid[i,j].Type == Node.NodeType.free)
                            {
                                this.grid[i, j].Type = Node.NodeType.end;
                                Check_Out.Add(this.grid[i, j]);
                                check = true;
                                break;
                            }
                        }
                        countB++;
                    }
                    if (check == true)
                    {
                        break;
                    }
                    countA++;
                    countB = 1;
                }
                pictureBox1.Invalidate();
            }

            else if (this.nodeType == Node.NodeType.obs)
            {
                if (this.grid[e.X / pbW, e.Y / pbH].Type == Node.NodeType.free)
                {
                    this.grid[e.X / pbW, e.Y / pbH].Type = Node.NodeType.obs;
                    Obstacles.Add(this.grid[e.X / pbW, e.Y / pbH]);
                    pictureBox1.Invalidate();
                }
            }

            else if (this.nodeType == Node.NodeType.free)
            {                
                if (this.grid[e.X / pbW, e.Y / pbH].Type != Node.NodeType.free)
                {
                    if (this.grid[e.X / pbW, e.Y / pbH].Type == Node.NodeType.start)
                    {
                        Check_In.Remove(this.grid[e.X / pbW, e.Y / pbH]);
                    }
                    else if (this.grid[e.X / pbW, e.Y / pbH].Type == Node.NodeType.end)
                    {
                        Check_Out.Remove(this.grid[e.X / pbW, e.Y / pbH]);
                    }
                    else if (this.grid[e.X / pbW, e.Y / pbH].Type == Node.NodeType.obs)
                    {
                        Obstacles.Remove(this.grid[e.X / pbW, e.Y / pbH]);
                    }
                    this.grid[e.X / pbW, e.Y / pbH].Type = Node.NodeType.free;
                    pictureBox1.Invalidate();
                }
            }
        }

        public void loopPath(object index) 
        {
            int i = (int)index;
            Random random = new Random();
            if (Check_In[i] != null)
            {
                Thread.Sleep(random.Next(1000, 3000));
            }
        }

        /// <summary>
        /// This method would pick the most optimal path, out of a list of possible paths.
        /// It also creates the list of possible paths.
        /// </summary>
        /// 
        private void OptimumPath()
        {

            NewDijkstra nDijkstra = new NewDijkstra(grid); ;
            

            for (int i = 0; i < Check_In.Count; i++)
            {

                Optimum_Paths.Add(nDijkstra.FindPath(Check_In[i], Check_Out[i]));
                foreach (var GG in Optimum_Paths[i])
                {
                    if (GG.Type == Node.NodeType.free)
                    {
                        grid[GG.Position.X, GG.Position.Y].Type = Node.NodeType.path;

                    }
                    if(GG.Type == Node.NodeType.start || GG.Type == Node.NodeType.end)
                    {

                    }
                    
                }
                //OG code goes here
                //create thread
                Thread threadPath = new Thread(loopPath);
                threadPath.Start(i);
                threads.Add(threadPath);
                Thread.Sleep(500);
                //OG code ends here
                //Path_Options.Add
            }

            if (!(Optimum_Paths.Contains(null)))
            {
                foreach (List<Node> paths in this.Optimum_Paths)
                {
                    foreach (var GG in paths)
                    {
                        if (GG.Type == Node.NodeType.free)
                        {
                            grid[GG.Position.X, GG.Position.Y].Type = Node.NodeType.path;
                        }
                    }
                    Node tempNodeStart = paths[0];
                    Node tempNodeEnd = paths[paths.Count - 1];
                    paths[0] = tempNodeEnd;
                    paths[paths.Count - 1] = tempNodeStart;
                    paths.Reverse();
                }

                for (int i = 0; i < Optimum_Paths.Count; i++)
                {
                    List<Mover> tempMovers = new List<Mover>();                    
                    movers.Add(tempMovers);
                }
                pictureBox1.Invalidate();
            }
            else 
            {
                btnReset.Enabled = true;
                MessageBox.Show("Please dont block the way");

            }
        }

        private void populateTempGrid()
        {           
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tempGrid[i, j] = new Node(new Coordinate(i, j), Node.NodeType.free, 1);

                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int size = 50;
            int size1 = 30;
            Graphics g = e.Graphics;
            Rectangle r;
            Rectangle r1;
            SolidBrush b;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)

                {
                    r = new Rectangle(this.grid[i,j].Position.X * size, this.grid[i, j].Position.Y * size, size, size);
                    r1 = new Rectangle(this.grid[i, j].Position.X * size + 10, this.grid[i, j].Position.Y * size + 10, size1, size1);

                    if (grid[i, j].Type == Node.NodeType.start)
                    {
                        b = new SolidBrush(System.Drawing.Color.Green);
                        g.FillRectangle(b, this.grid[i, j].Position.X * size, this.grid[i, j].Position.Y * size, size, size);
                    }
                    else if (grid[i, j].Type == Node.NodeType.end)
                    {
                        b = new SolidBrush(System.Drawing.Color.Red);
                        g.FillRectangle(b, this.grid[i, j].Position.X * size, this.grid[i, j].Position.Y * size, size, size);
                    }
                    else if (grid[i, j].Type == Node.NodeType.path)
                    {
                        b = new SolidBrush(System.Drawing.Color.Gray);
                        g.FillRectangle(b, this.grid[i, j].Position.X * size, this.grid[i, j].Position.Y * size, size, size);
                    }
                    else if (grid[i, j].Type == Node.NodeType.free)
                    {
                        //b = new SolidBrush(System.Drawing.Color.DimGray);
                        g.DrawRectangle(new Pen(System.Drawing.Color.Silver), this.grid[i, j].Position.X * size, this.grid[i, j].Position.Y * size, size, size);
                    }
                    else if (grid[i,j].Type == Node.NodeType.move)
                    {

                    }
                    else if (grid[i, j].Type == Node.NodeType.obs)
                    {
                        b = new SolidBrush(System.Drawing.Color.Black);
                        g.FillRectangle(b, this.grid[i, j].Position.X * size, this.grid[i, j].Position.Y * size, size, size);
                    }                          
                }
            }
            if(Optimum_Paths.Count != 0)
            {
                foreach(List<Mover> mList in movers)
                {
                    foreach(Mover m in mList)
                    {
                        m.Update();
                        m.Display(e.Graphics);
                    }
                }
            }
        }

        /// <summary>
        /// to draw our on path.
        /// 1st check if we select path button.
        /// 2nd while left mouse button is pressed.
        /// 3rd draw the specific grid posistion gray.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                int pbW = 50;
                int pbH = 50;

                if (System.Windows.Forms.Control.MouseButtons == MouseButtons.Left && this.grid[e.X / pbW, e.Y / pbH].Type == Node.NodeType.free)
                {
                    if (this.nodeType == Node.NodeType.path)
                    {
                        this.grid[e.X / pbW, e.Y / pbH].Type = Node.NodeType.path;
                        pictureBox1.Invalidate();
                    }
                }
            }
            catch (System.IndexOutOfRangeException ex) { MessageBox.Show("You have left your drawing Board. Go Back! "+ ex.Message); }
        }
#endregion

        #region Buttons For Creating Path, Check-in, Check-out
        /// <summary>
        /// Button Start, is used for assigning the field nodeType to Node.NodeType.path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            nodeType = Node.NodeType.start;            
        }

        /// <summary>
        /// Button End, is used for assigning the field nodeType to Node.NodeType.path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            nodeType = Node.NodeType.end;
        }

        //
        /// <summary>
        /// Button Delete, is used for assigning the field nodeType to Node.NodeType.free
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.nodeType = Node.NodeType.free;
        }

        

        /// <summary>
        /// Used for putting obstacles on the path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonObstacle_Click(object sender, EventArgs e)
        {
            this.nodeType = Node.NodeType.obs;
        }
        #endregion

        #region Button For emport & Export + related Methods
        private void btnExport_Click(object sender, EventArgs e)
        {
            int _end = 0;           int _start = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (this.grid[i, j].Type == Node.NodeType.start)
                    {
                        _start++;
                    }
                    if (this.grid[i, j].Type == Node.NodeType.end)
                    {
                        _end++;
                    }
                }
            }
            if (tbSimulationName.Text == "")
            {
                MessageBox.Show("You need to put a name for the simulation you want to save in the textbox!");
            }
            else if (_start != _end || _start == 0 || _end == 0)
            {
                MessageBox.Show("you must add equal amount of check_in and check_out to be able to export your simulation!");
            }
            else
            {
                FileStream fs = null;
                BinaryFormatter bf = null;
                List<Node> temp = new List<Node>();

                try
                {
                    fs = new FileStream("../../../../SavedSim/" + tbSimulationName.Text, FileMode.Create, FileAccess.Write);
                    bf = new BinaryFormatter();
                    temp = List_end_start();
                    bf.Serialize(fs, temp);
                    MessageBox.Show("Saving done!");                    
                }
                catch (IOException exx)
                {
                    MessageBox.Show("Error creating file " + exx.Message);
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show("Error creating file " + ex.Message);
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }        
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Check_In.Clear();
                this.Check_Out.Clear();
                this.Obstacles.Clear();
                this.Optimum_Paths.Clear();
                this.lblFilePosition.Text = this.openFileDialog1.FileName;

                FileStream fs = null;
                BinaryFormatter bf = null;

                try
                {
                    fs = new FileStream(this.lblFilePosition.Text, FileMode.Open, FileAccess.Read);
                    bf = new BinaryFormatter();
                    List<Node> listNode = new List<Node>();
                    listNode = (bf.Deserialize(fs)) as List<Node>;
                    Node t;

                    foreach (Node item in listNode)
                    {
                        t = item;
                        if (item.Type == Node.NodeType.start)
                        {
                            Check_In.Add(item);
                        }
                        else if (item.Type == Node.NodeType.end)
                        {
                            Check_Out.Add(item);
                        }
                        else if (item.Type == Node.NodeType.obs)
                        {
                            Obstacles.Add(item);
                        }
                    }

                    this.AfterImportUpdate(listNode);
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show("Somethings wrong with Serialization " + ex.Message);
                }
                catch (IOException exx)
                {
                    MessageBox.Show("Something is wrong with IO " + exx.Message);
                }
            }
            else
            {
                MessageBox.Show("You decided to cancel");
            }
        }

        /// <summary>
        /// Adds the check-in (start) nodes and the check-out (end) Nodes to their position in the grid object.
        /// </summary>
        private void AfterImportUpdate(List<Node> start_end)
        {
            foreach (Node n in start_end)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (this.grid[i, j].Position.X == n.Position.X && this.grid[i, j].Position.Y == n.Position.Y)
                        {
                            this.grid[i, j] = n;
                        }
                    }
                }
            }

            pictureBox1.Invalidate();
        }
        
        /// <summary>
        /// here we return a list of both the check-in and check-out.
        /// </summary>
        /// <returns></returns>
        public List<Node> List_end_start()
        {
            List<Node> start_end = new List<Node>();

            foreach (Node item in Check_In)
            {
                start_end.Add(item);
            }

            foreach (Node item in Check_Out)
            {
                start_end.Add(item);
            }
            foreach (Node item in Obstacles)
            {
                start_end.Add(item);
            }
            return start_end;
        }

        private void timerFlow_Tick(object sender, EventArgs e)
        {
            timerFlow.Interval = rand.Next(1000, 3000);
            for (int i = 0; i < movers.Count; i++)
            {
                if(airport.flights[i].NrOffBaggages == 0)
                {

                }                
                else
                {
                    movers[i].Add(new Mover(Optimum_Paths[i], airport.flights[i].Color));
                    airport.flights[i].NrOffBaggages--; //or the nr on start

                }
                airport.flights[i].NrOnEnd = movers[i].Count(item => item.killIt);
                airport.flights[i].NrOnPath = movers[i].Count(item => item.onPath);
            }
            updateLabel2();
        }

        public void updateLabel2()
        {
            label2.Text = "";
            for (int i = 0; i < airport.flights.Count; i++)
            {
                label2.Text += "Flight NR : " + airport.flights[i].FlightNr + ", Color: :" + airport.flights[i].Color + "\n" +
                                "Nr of Baggages Waiting: " + airport.flights[i].NrOffBaggages + "\n" +
                                "Nr of Baggages on Belt: " + airport.flights[i].NrOnPath + "\n" +
                                "Nr of Baggages on CheckOut: " + airport.flights[i].NrOnEnd + "\n" + 
                                "----------------------------------------------\n";
            }
        }

        private void buttonObstacle_Click_1(object sender, EventArgs e)
        {
            this.nodeType = Node.NodeType.obs;
        }
        #endregion


    }
}