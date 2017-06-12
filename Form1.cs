using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
     /////////////////////////
     // V a r i a b l e s   //
     // //////////////////////
     
        static Boolean firstRun = true;

    //  Variables to Create Grid Height and Width
        const int GRID_WIDTH= 10;
        const int GRID_HEIGHT = 10;
        Label[] lblSpots = new Label[GRID_WIDTH * GRID_HEIGHT];
        int[] grid = new int[GRID_WIDTH * GRID_HEIGHT];
       
        int ShotsFired;
        Boolean SpotTaken;        
        Random rnd = new Random();

    //  Ship Arrays 
        int[] DisplayedNumbers = { 0, 1, 2, 3, 4, 5 };//0-Empty, 1-AirCraft Carrier, 2-Battleship, 3-Cruiser, 4-Submarine, 5-Destroyer.
        int[] ShipSizes = { 0, 5, 4, 3, 3, 2 };// Empty, 1-AirCraft Carrier, 2-Battleship, 3-Cruiser, 4-Submarine, 5-Destroyer
        string[] ShipName = { "", "Air Craft Carrier", "Battleship", "Cruiser", "Submarine", "Destroyer" }; 

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (firstRun)
	        {
                firstRun = false;
                
                for (int i = 0; i < lblSpots.Length; i++)
                {
                 
                    lblSpots[i] = new Label();

                //  Properties For All The Other Labels Are Equal To The Main Label
                    lblSpots[i].Font = lblSpot.Font;
                    lblSpots[i].Size = lblSpot.Size;                    
                    lblSpots[i].Tag = i;
                    lblSpots[i].Text = lblSpot.Text;                    
                    lblSpots[i].TextAlign = lblSpot.TextAlign;

                    lblSpots[i].BackColor = Color.Gray;
                    
                //  Create 10 x 10 Board
                    int x_offset = i % GRID_WIDTH; // Over
                    int y_offset = i / GRID_WIDTH; // Down

                    lblSpots[i].Left = lblSpot.Left + x_offset * (lblSpot.Width + 10);
                    lblSpots[i].Top = lblSpot.Top + y_offset * (lblSpot.Height + 10);
                    

                //  Makes The Click Event Work For All Labels
                    lblSpots[i].Click += new EventHandler(lblSpot_Click);

                    Form.ActiveForm.Controls.AddRange(lblSpots);

                //  Disables Labels Until They Click New Game
                    lblSpot.Enabled = false;
                    lblSpots[i].Enabled = false;

                }// if
             }
    }

        /////////////////////////////////////////////////////////
        //
        //                  E V E N T S
        //
        ////////////////////////////////////////////////////////

        
        private void lblSpot_Click(object sender, EventArgs e)
        {
            Label me = new Label();
            me = (Label)sender;

        //  Displays Number Of Shots Fired
            ShotsFired++;
            lblShotsFired.Text = "Shots Fired: " + Convert.ToString(ShotsFired);

        //  Makes It So That You Can't Click The Labels Again After You Already Clicked It
            me.Enabled = false;
                    
        //  Tells If Player Hits Or Misses The Ships
            int index = Convert.ToInt32(me.Tag);

        //  If Player Hits A Ship The Label Will Turn Yellow
            if (grid[index] != 0)
            {
                me.BackColor = Color.Yellow;
                MessageBox.Show("You Hit Me!");

            }// if
        
        //  If Player Doesn't Hit A Ship The Label Will Turn Green
            else
            {
                me.BackColor = Color.Green;

            }// else
        }

        private void btnDisplayBoard_Click(object sender, EventArgs e)
        {
            if (lblSpots[0].Text == "")
            
            //  Displays Board
                for (int i = 0; i < grid.Length; i++)
                {
                    lblSpot.Text = grid[0].ToString();
                    lblSpots[i].Text = grid[i].ToString();
                    btnDisplayBoard.Text = "Hide";
                }// if
                
            else
            
            //  Hides Board
                for (int i = 0; i < grid.Length; i++)
                {
                    lblSpot.Text = "";
                    lblSpots[i].Text = "";
                    btnDisplayBoard.Text = "Display Board";
                }// else
            
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
            PlaceShips();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();

        }


        /////////////////////////////////////////////////////////
        //
        //                M E T H O D S
        //
        ////////////////////////////////////////////////////////
        
        private void NewGame()
        {
            
        //  Resets Shots Fired Back To 0
            ShotsFired = 0;
            lblShotsFired.Text = "Shots Fired: 0";

            for (int i = 0; i < grid.Length; i++)
            {
            //  Enables The Buttons When Player Clicks NewGame 
                lblSpot.Enabled = true;
                lblSpots[i].Enabled = true;

            //  Clears The Ships
                grid[i] = 0;

            //  Sets The Label Colors Back To Gray
                lblSpots[i].BackColor = Color.Gray;
                lblSpot.BackColor = Color.Gray;
            }// for



            lblStart.Text = "";
        }

        private void PlaceShips()
        {

        // For Loop That Creates All The Ships

            for (int ship = 1; ship < ShipSizes.Length; ship++)
            {

            //  Horizontal   
                if (rnd.Next(2) == 0)
                {
                    int num;
                    int x, y;

                //  num = WIDTH - Shiplength
                    num = GRID_WIDTH - ShipSizes[ship] + 1;

                    do
                    {
                        x = rnd.Next(num);
                        y = rnd.Next(GRID_HEIGHT);
                        SpotTaken = false;

                        for (int i = 0; i < ShipSizes[ship]; i++)
                        {
                            int index = y * GRID_WIDTH + x + i;

                            if (grid[index] != 0)
                            {
                                SpotTaken = true;

                            }// if 

                        }// for 

                    } while (SpotTaken); // do

                //  Filling Using A For Loop
                    for (int i = 0; i < ShipSizes[ship]; i++)
                    {
                        int index = y * GRID_WIDTH + x + i;
                        grid[index] = DisplayedNumbers[ship];
                    }// for

                }// if 


            //  Vertical
                else
                {
                    int num;
                    int x, y;

                //  num = HEIGHT - Shiplength
                    num = GRID_HEIGHT - ShipSizes[ship] + 1;

                    do
                    {
                        x = rnd.Next(GRID_WIDTH);
                        y = rnd.Next(num);
                        SpotTaken = false;

                        for (int i = 0; i < ShipSizes[ship]; i++)
                        {
                            int index = y * GRID_WIDTH + x + GRID_HEIGHT * i;

                            if (grid[index] != 0)
                            {
                                SpotTaken = true;

                            }// if 

                        }// for 

                    } while (SpotTaken); // do

                //  Filling Using A For Loop
                    for (int i = 0; i < ShipSizes[ship]; i++)
                    {
                        int index = y * GRID_WIDTH + x + GRID_HEIGHT * i;
                        grid[index] = DisplayedNumbers[ship];

                    }// for 

                }// if 
           
            }// for 

        }// private void PlaceShips()


    }// public partial class Form1 : Form

}// namespace WindowsFormsApplication1