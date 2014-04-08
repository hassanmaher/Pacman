using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace Chomp
{
	
	public class frmScreen : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label lblPlayer1Points;
		private System.Windows.Forms.Label lblHighScore;
		private System.Windows.Forms.Label lblPlayer2Points;
		private System.Windows.Forms.Label lbl1UP;
		private System.Windows.Forms.Label lblHScore;
		private System.Windows.Forms.Label lbl2UP;
		private System.Windows.Forms.Panel pnlSplash;
		private System.Windows.Forms.Label lblSplash1;
		private System.Windows.Forms.Label lblSplash2;
		private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox picGameBoard;

		private System.Windows.Forms.Timer tmrMove;
        private System.Windows.Forms.Timer tmrBlink;
		private System.Windows.Forms.Timer tmrDie;
		private System.Windows.Forms.Timer tmrPowerMode;

        private bool _debug;
		private const int START_LIVES = 3;
		private const int MOVE_INTERVAL = 5;
		
		private GameBoard _board;
		private Bitmap _bmpPacman;
				
		public frmScreen()
		{
			InitializeComponent();
			
			_debug = false;
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			InitializeGameBoard();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tmrMove = new System.Windows.Forms.Timer(this.components);
			this.lblPlayer1Points = new System.Windows.Forms.Label();
			this.lblHighScore = new System.Windows.Forms.Label();
			this.lblPlayer2Points = new System.Windows.Forms.Label();
			this.lbl1UP = new System.Windows.Forms.Label();
			this.lblHScore = new System.Windows.Forms.Label();
			this.lbl2UP = new System.Windows.Forms.Label();
			this.tmrPowerMode = new System.Windows.Forms.Timer(this.components);
			this.tmrBlink = new System.Windows.Forms.Timer(this.components);
			this.tmrDie = new System.Windows.Forms.Timer(this.components);
			this.lblStatus = new System.Windows.Forms.Label();
			this.picGameBoard = new System.Windows.Forms.PictureBox();
			this.pnlSplash = new System.Windows.Forms.Panel();
			this.lblSplash2 = new System.Windows.Forms.Label();
			this.lblSplash1 = new System.Windows.Forms.Label();
			this.pnlSplash.SuspendLayout();
			this.SuspendLayout();
			// 
			// tmrMove
			// 
			this.tmrMove.Interval = 20;
			this.tmrMove.Tick += new System.EventHandler(this.tmrMove_Tick);
			// 
			// lblPlayer1Points
			// 
			this.lblPlayer1Points.BackColor = System.Drawing.Color.Black;
			this.lblPlayer1Points.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPlayer1Points.ForeColor = System.Drawing.Color.White;
			this.lblPlayer1Points.Location = new System.Drawing.Point(30, 30);
			this.lblPlayer1Points.Name = "lblPlayer1Points";
			this.lblPlayer1Points.Size = new System.Drawing.Size(110, 30);
			this.lblPlayer1Points.TabIndex = 9;
			this.lblPlayer1Points.Text = "0";
			this.lblPlayer1Points.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblHighScore
			// 
			this.lblHighScore.BackColor = System.Drawing.Color.Black;
			this.lblHighScore.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblHighScore.ForeColor = System.Drawing.Color.White;
			this.lblHighScore.Location = new System.Drawing.Point(185, 30);
			this.lblHighScore.Name = "lblHighScore";
			this.lblHighScore.Size = new System.Drawing.Size(110, 30);
			this.lblHighScore.TabIndex = 10;
			this.lblHighScore.Text = "0";
			this.lblHighScore.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblPlayer2Points
			// 
			this.lblPlayer2Points.BackColor = System.Drawing.Color.Black;
			this.lblPlayer2Points.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPlayer2Points.ForeColor = System.Drawing.Color.White;
			this.lblPlayer2Points.Location = new System.Drawing.Point(335, 30);
			this.lblPlayer2Points.Name = "lblPlayer2Points";
			this.lblPlayer2Points.Size = new System.Drawing.Size(110, 30);
			this.lblPlayer2Points.TabIndex = 11;
			this.lblPlayer2Points.Text = "0";
			this.lblPlayer2Points.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbl1UP
			// 
			this.lbl1UP.BackColor = System.Drawing.Color.Black;
			this.lbl1UP.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl1UP.ForeColor = System.Drawing.Color.White;
			this.lbl1UP.Location = new System.Drawing.Point(65, 5);
			this.lbl1UP.Name = "lbl1UP";
			this.lbl1UP.Size = new System.Drawing.Size(57, 30);
			this.lbl1UP.TabIndex = 12;
			this.lbl1UP.Text = "1UP";
			this.lbl1UP.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblHScore
			// 
			this.lblHScore.BackColor = System.Drawing.Color.Black;
			this.lblHScore.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblHScore.ForeColor = System.Drawing.Color.White;
			this.lblHScore.Location = new System.Drawing.Point(145, 5);
			this.lblHScore.Name = "lblHScore";
			this.lblHScore.Size = new System.Drawing.Size(180, 30);
			this.lblHScore.TabIndex = 13;
			this.lblHScore.Text = "HIGH  SCORE";
			this.lblHScore.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbl2UP
			// 
			this.lbl2UP.BackColor = System.Drawing.Color.Black;
			this.lbl2UP.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl2UP.ForeColor = System.Drawing.Color.White;
			this.lbl2UP.Location = new System.Drawing.Point(365, 5);
			this.lbl2UP.Name = "lbl2UP";
			this.lbl2UP.Size = new System.Drawing.Size(60, 30);
			this.lbl2UP.TabIndex = 14;
			this.lbl2UP.Text = "2UP";
			this.lbl2UP.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tmrPowerMode
			// 
			this.tmrPowerMode.Interval = 10000;
			this.tmrPowerMode.Tick += new System.EventHandler(this.tmrPowerMode_Tick);
			// 
			// tmrBlink
			// 
			this.tmrBlink.Interval = 250;
			this.tmrBlink.Tick += new System.EventHandler(this.tmrBlink_Tick);
			// 
			// lblStatus
			// 
			this.lblStatus.BackColor = System.Drawing.Color.Black;
			this.lblStatus.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.Yellow;
			this.lblStatus.Location = new System.Drawing.Point(170, 368);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(165, 25);
			this.lblStatus.TabIndex = 15;
			this.lblStatus.Text = "READY!";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lblStatus.Visible = false;
			// 
			// picGameBoard
			// 
			this.picGameBoard.BackColor = System.Drawing.Color.Black;
			this.picGameBoard.Location = new System.Drawing.Point(30, 70);
			this.picGameBoard.Name = "picGameBoard";
			this.picGameBoard.Size = new System.Drawing.Size(448, 550);
			this.picGameBoard.TabIndex = 3;
			this.picGameBoard.TabStop = false;
			this.picGameBoard.Visible = false;
			this.picGameBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.picGameBoard_Paint);
			// 
			// pnlSplash
			// 
			this.pnlSplash.Controls.Add(this.lblSplash2);
			this.pnlSplash.Controls.Add(this.lblSplash1);
			this.pnlSplash.Location = new System.Drawing.Point(30, 70);
			this.pnlSplash.Name = "pnlSplash";
			this.pnlSplash.Size = new System.Drawing.Size(450, 550);
			this.pnlSplash.TabIndex = 16;
			// 
			// lblSplash2
			// 
			this.lblSplash2.BackColor = System.Drawing.Color.Black;
			this.lblSplash2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSplash2.ForeColor = System.Drawing.Color.White;
			this.lblSplash2.Location = new System.Drawing.Point(50, 305);
			this.lblSplash2.Name = "lblSplash2";
			this.lblSplash2.Size = new System.Drawing.Size(335, 30);
			this.lblSplash2.TabIndex = 15;
			this.lblSplash2.Text = "PRESS F2 FOR 2 PLAYERS";
			this.lblSplash2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblSplash1
			// 
			this.lblSplash1.BackColor = System.Drawing.Color.Black;
			this.lblSplash1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSplash1.ForeColor = System.Drawing.Color.White;
			this.lblSplash1.Location = new System.Drawing.Point(35, 260);
			this.lblSplash1.Name = "lblSplash1";
			this.lblSplash1.Size = new System.Drawing.Size(335, 30);
			this.lblSplash1.TabIndex = 14;
			this.lblSplash1.Text = "PRESS F1 FOR 1 PLAYER";
			this.lblSplash1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// frmScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(507, 661);
			this.Controls.Add(this.pnlSplash);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.lbl2UP);
			this.Controls.Add(this.lblHScore);
			this.Controls.Add(this.lbl1UP);
			this.Controls.Add(this.lblPlayer2Points);
			this.Controls.Add(this.lblHighScore);
			this.Controls.Add(this.lblPlayer1Points);
			this.Controls.Add(this.picGameBoard);
			this.Name = "frmScreen";
			this.Text = "Chomp";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmScreen_KeyDown);
			this.pnlSplash.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new frmScreen());
		}

		private void InitializeGameBoard()
		{
			this.BackColor = Color.Black;
			
			// Set up Game Board properties
			_board = new GameBoard(this.Width, this.Height, picGameBoard);
			_board.OnBoardCleared += new GameBoard.GameBoardClearedEventHandler(GameBoardCleared);
			_board.onEndPlayerTurn += new GameBoard.EndPlayerTurnEventHandler(EndPlayerTurn);
			_board.onScoreChanged += new GameBoard.ScoreChangedEventHandler(ScoreChanged);
			_board.OnPowerMode += new GameBoard.PowerModeEventHandler(PowerModeInitiated);
			_board.GeneratePathPoints();
			_board.Pellets.GeneratePellets();            
			_board.CurrentEatScore = 200;
			_board.InitializeCharacters();

			// Generate Lives Image
			_bmpPacman = new Bitmap(31, 31);
			SolidBrush CoverBrush = new SolidBrush(Color.Black);
			SolidBrush b = new SolidBrush(Color.FromArgb(255, 255, 0));
			Graphics g = Graphics.FromImage(_bmpPacman);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillEllipse(b, 0, 0, 30, 30);
			g.FillPie(CoverBrush, -2, 0, 41, 30, 155, 50);
			g.FillRectangle(CoverBrush, 0, 5, 4, 6);
			g.FillRectangle(CoverBrush, 0, 20, 4, 6);
		}
		
		private void picGameBoard_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			_board.PaintGameBoard(e.Graphics);
			_board.Pellets.PaintPellets(e.Graphics);
			_board.PacMan.PaintCharacter(e.Graphics);
			_board.Blinky.PaintCharacter(e.Graphics);
			_board.Pinky.PaintCharacter(e.Graphics);
			_board.Inky.PaintCharacter(e.Graphics);
			_board.Clyde.PaintCharacter(e.Graphics);
			if (_debug)	_board.PaintGamePath(e.Graphics);            
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			UpdateLives(e.Graphics);
		}

		private void frmScreen_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
			{
				_board.PacMan.AttemptedDirection = CharacterDirection.Down;
			}
			else if (e.KeyCode == Keys.Up)
			{
				_board.PacMan.AttemptedDirection = CharacterDirection.Up;
			}
			else if (e.KeyCode == Keys.Left)
			{
				_board.PacMan.AttemptedDirection = CharacterDirection.Left;
			}
			else if (e.KeyCode == Keys.Right)
			{
				_board.PacMan.AttemptedDirection = CharacterDirection.Right;
			}
			else if (e.KeyCode == Keys.F1)
			{
				if (_board.Player1.GameOver && _board.Player2.GameOver) StartGame(1);
			}
			else if (e.KeyCode == Keys.F2)
			{
				if (_board.Player1.GameOver && _board.Player2.GameOver) StartGame(2);
			}
		}

		private void tmrMove_Tick(object sender, System.EventArgs e)
		{
			_board.MoveCharacters();
			picGameBoard.Invalidate();
		}

		private void StartGame(int Players)
		{
			switch (Players)
			{
				case 1:
					_board.Player1.GameOver = false;
					break;

				case 2:
					_board.Player1.GameOver = false;
					_board.Player2.GameOver = false;
					break;
			}
			
			picGameBoard.Visible = true;
			pnlSplash.Visible = false;
			 _board.Player1.CurrentLevel = (GameLevel) _board.Levels[1];
			 _board.Player2.CurrentLevel = (GameLevel) _board.Levels[1];
			_board.Player1.Lives = START_LIVES;
			_board.Player2.Lives = START_LIVES;
			_board.CurrentPlayer = _board.Player1;
			_board.Pellets.GeneratePellets();
			_board.ResetCharacters();
			picGameBoard.Refresh();
			lblStatus.ForeColor = Color.FromArgb(255, 255, 0);
			lblStatus.Text = "READY!";
			lblStatus.Visible = true;
			Application.DoEvents();
			
			Refresh();
			
			_board.PlayBoardSound(BoardSounds.GameIntro);
			System.Threading.Thread.Sleep(4500);
			_board.PlayBoardSound(BoardSounds.BackgroundSiren);
			_board.CurrentPlayer.Lives -= 1;
			Refresh();
			lblStatus.Visible = false;
			tmrBlink.Enabled = true;
			tmrMove.Enabled = true;			
		}

		private void ResetGameBoard()
		{
			picGameBoard.Refresh();
			_board.PlayBoardSound(BoardSounds.None);
			if (_board.Player1.GameOver && _board.Player2.GameOver)
			{
				lblStatus.ForeColor = Color.FromArgb(255, 0, 0);
				lblStatus.Text = "GAME OVER";
			}
			else
			{
				lblStatus.ForeColor = Color.FromArgb(255, 255, 0);
				lblStatus.Text = "READY!";
			}
			_board.PacMan.Visible = true;
			lblStatus.Visible = true;
			lblStatus.Refresh();
			System.Threading.Thread.Sleep(2000);
			lblStatus.Visible = false;
			_board.PlayBoardSound(BoardSounds.BackgroundSiren);
			tmrMove.Enabled = true;
			tmrBlink.Enabled = true;
		}

		private void UpdateLives(Graphics g)
		{
			int x = 60;
			int y = 625;
			for (int i = 0; i< _board.CurrentPlayer.Lives; i++)
			{
				g.DrawImage(_bmpPacman, x, y, 25, 25);
				x += 30;
			}
		}

		private void tmrBlink_Tick(object sender, System.EventArgs e)
		{
			_board.Pellets.BlinkPowerPellet();
		}

		private void tmrPowerMode_Tick(object sender, System.EventArgs e)
		{
			tmrMove.Enabled = false;
			_board.PowerMode = false;
			tmrPowerMode.Enabled = false;
			_board.PlayBoardSound(BoardSounds.None);
			_board.Pinky.ChangeInvincibility(CharacterInvincibility.Invincible);
			_board.Inky.ChangeInvincibility(CharacterInvincibility.Invincible);
			_board.Blinky.ChangeInvincibility(CharacterInvincibility.Invincible);
			_board.Clyde.ChangeInvincibility(CharacterInvincibility.Invincible);
			_board.CurrentEatScore = 200;
			_board.PlayBoardSound(BoardSounds.BackgroundSiren);
			tmrMove.Enabled = true;
		}

		private void GameBoardCleared(object sender, System.EventArgs e)
		{
			tmrMove.Enabled = false;
			_board.PacMan.Visible = false;
			_board.Blinky.Visible = false;
			_board.Inky.Visible = false;
			_board.Pinky.Visible = false;
			_board.Clyde.Visible = false;
			tmrBlink.Enabled = false;
			picGameBoard.Refresh();
			_board.PlayBoardSound(BoardSounds.None);
			System.Threading.Thread.Sleep(2000);
			_board.Pellets.GeneratePellets();
            ResetGameBoard();
			_board.CurrentPlayer.CurrentLevel = _board.NextLevel();
			_board.ResetCharacters();
		}

		private void EndPlayerTurn(object sender, System.EventArgs e)
		{
			_board.PlayBoardSound(BoardSounds.None);
			tmrMove.Enabled = false;
			_board.Pinky.Visible = false;
			_board.Blinky.Visible = false;
			_board.Inky.Visible = false;
			_board.Clyde.Visible = false;
			tmrBlink.Enabled = false;
			System.Threading.Thread.Sleep(1000);
			_board.PacMan.Position = PacmanCharacter.CharacterPosition.OpenMidHigh;
			_board.PacMan.Direction = CharacterDirection.Up;
			picGameBoard.Refresh();
			System.Threading.Thread.Sleep(500);
			_board.PlayBoardSound(BoardSounds.PacmanEat);
			System.Threading.Thread.Sleep(750);
			_board.PacMan.Visible = false;
			picGameBoard.Refresh();
			System.Threading.Thread.Sleep(1000);
			//_board.AnimateDeath();

			if (_board.CurrentPlayer.Lives == 0)
			{
				_board.CurrentPlayer.GameOver = true;
				_board.CurrentPlayer.Score = 0;
			}
			else
			{
				_board.CurrentPlayer.Lives -= 1;
				this.Refresh();
			}
			_board.ResetCharacters();
			ResetGameBoard();
			if (_board.Player1.GameOver && _board.Player2.GameOver) 
			{
				tmrMove.Enabled = false;
				pnlSplash.Visible = true;
				picGameBoard.Visible = false;
			}
		}

		private void ScoreChanged(object source, System.EventArgs e)
		{
			lblPlayer1Points.Text = _board.Player1.Score.ToString();
			lblPlayer2Points.Text = _board.Player2.Score.ToString();
			lblHighScore.Text = _board.HighScore.ToString();
		}

		private void PowerModeInitiated(object source, System.EventArgs e)
		{
			if (tmrPowerMode.Enabled)
			{
				tmrPowerMode.Enabled = false;
				_board.CurrentEatScore = 200;
			}
			_board.PlayBoardSound(BoardSounds.None);
			_board.PlayBoardSound(BoardSounds.BackgroundInvincibility);
			_board.Blinky.ChangeInvincibility(CharacterInvincibility.Vulnerable);
			_board.Inky.ChangeInvincibility(CharacterInvincibility.Vulnerable);
			_board.Pinky.ChangeInvincibility(CharacterInvincibility.Vulnerable);
			_board.Clyde.ChangeInvincibility(CharacterInvincibility.Vulnerable);
			tmrPowerMode.Enabled = true;
			
		}
	}
}
