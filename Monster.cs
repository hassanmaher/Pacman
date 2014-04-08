using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace Chomp
{
	public class GhostCharacter : GameCharacter
	{
		private Bitmap _bmpCharUp1;
		private Bitmap _bmpCharUp2;
		private Bitmap _bmpCharLeft1;
		private Bitmap _bmpCharLeft2;
		private Bitmap _bmpCharRight1;
		private Bitmap _bmpCharRight2;
		private Bitmap _bmpCharDown1;
		private Bitmap _bmpCharDown2;
		private Bitmap _bmpCharVincibleBlue1;
		private Bitmap _bmpCharVincibleBlue2;
		private Bitmap _bmpCharVincibleWhite1;
		private Bitmap _bmpCharVincibleWhite2;
		private Color _ghostColor;
		private Random _rnd;
		private GameBoard _board;

		private enum CharacterPosition
		{
			LegsOut = 0,
			LegsIn = 1,
		}

		private CharacterPosition _currentCharacterPosition;

		public GhostCharacter(GameBoard Board, Point CurrentLocation, int Width, int Height, Color CharacterColor) : base (Board, CurrentLocation, Width, Height)
		{
			_ghostColor = CharacterColor;
			_currentCharacterPosition = CharacterPosition.LegsOut;
			_currentInvincibility = CharacterInvincibility.Invincible;
			_rnd = new Random(CharacterColor.R + CharacterColor.G + CharacterColor.B);
			_board = Board;
		}

		public override sealed void Initialize()
		{
			GenerateCharacters();
		}

		public Color GhostColor
		{
			get {return _ghostColor;}
			set {_ghostColor = value;}
		}

		public override sealed void Move()
		{
			try
			{
				// Increment frame
				_frame ++;
				if (_frame > 4)
				{
					_frame = 1;
					if (_currentCharacterPosition == CharacterPosition.LegsOut)
						_currentCharacterPosition = CharacterPosition.LegsIn;
					else
						_currentCharacterPosition = CharacterPosition.LegsOut;
				}

				// Tunnel
				if (_currentLocation.X == 448) 
					_currentLocation.X = 0;
				else if (_currentLocation.X == 0)
					_currentLocation.X = 448;
 
				ArrayList pts = GetNextPossibleCoordinates(_direction, _currentLocation);
				int iSelected = _rnd.Next(1, pts.Count +1);
				Point SelectedPoint = (Point) pts[iSelected -1];

				if (_currentLocation.Y < SelectedPoint.Y) 
					_direction = CharacterDirection.Down;
				else if (_currentLocation.X < SelectedPoint.X) 
					_direction = CharacterDirection.Right;
				else if (_currentLocation.X > SelectedPoint.X) 
					_direction = CharacterDirection.Left;
				else if (_currentLocation.Y > SelectedPoint.Y) 
					_direction = CharacterDirection.Up;

				MoveToEndPoint(_direction, GetDestinationCoordinate(_direction, _currentLocation));
			}
			catch (Exception expMove)
			{
				System.Diagnostics.Debug.WriteLine(expMove.Message);
			}
		}

		protected override sealed void MoveToEndPoint(CharacterDirection Direction, Point p)
		{
			Point TestPoint;
			
			while (_currentLocation != p)
			{
				
				TestPoint = GetNextCoordinate(Direction, _currentLocation);
				if (_board.CanMove(TestPoint))
				{
					_currentLocation = TestPoint;
					
					switch (_direction)
					{
						case CharacterDirection.Left:
							if (_board.CanMove(_currentLocation, CharacterDirection.Up)) return;
							if (_board.CanMove(_currentLocation, CharacterDirection.Down)) return;
							break;

						case CharacterDirection.Right:
							if (_board.CanMove(_currentLocation, CharacterDirection.Up)) return;
							if (_board.CanMove(_currentLocation, CharacterDirection.Down)) return;
							break;

						case CharacterDirection.Up:
							if (_board.CanMove(_currentLocation, CharacterDirection.Left)) return;
							if (_board.CanMove(_currentLocation, CharacterDirection.Right)) return;
							break;

						case CharacterDirection.Down:
							if (_board.CanMove(_currentLocation, CharacterDirection.Left)) return;
							if (_board.CanMove(_currentLocation, CharacterDirection.Right)) return;
							break;
					}
				}
				else
				{
					break;
				}
			}
		}

		private ArrayList GetNextPossibleCoordinates(CharacterDirection Direction, Point CurrentCoordinate)
		{
			int x = CurrentCoordinate.X;
			int y = CurrentCoordinate.Y;

			ArrayList pts = new ArrayList();
			Point pntCheck;

			switch (Direction)
			{
				case CharacterDirection.Right:
					// Can move Right?
					pntCheck = new Point(CurrentCoordinate.X + 1, CurrentCoordinate.Y);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					// Can move Up?
					pntCheck = new Point(CurrentCoordinate.X, CurrentCoordinate.Y-1);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					// Can move Down?
					pntCheck = new Point(CurrentCoordinate.X, CurrentCoordinate.Y+1);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					break;

				case CharacterDirection.Left:

					// Can move Left?
					pntCheck = new Point(CurrentCoordinate.X-1, CurrentCoordinate.Y);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					// Can move Up?
					pntCheck = new Point(CurrentCoordinate.X, CurrentCoordinate.Y-1);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					// Can move Down?
					pntCheck = new Point(CurrentCoordinate.X, CurrentCoordinate.Y+1);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					break;

				case CharacterDirection.Down:

					// Can move Right?
					pntCheck = new Point(CurrentCoordinate.X + 1, CurrentCoordinate.Y);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					// Can move Down?
					pntCheck = new Point(CurrentCoordinate.X, CurrentCoordinate.Y+1);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					// Can move Left?
					pntCheck = new Point(CurrentCoordinate.X-1, CurrentCoordinate.Y);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					break;

				case CharacterDirection.Up:
					
					// Can move Right?
					pntCheck = new Point(CurrentCoordinate.X+1, CurrentCoordinate.Y);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					// Can move Left?
					pntCheck = new Point(CurrentCoordinate.X-1, CurrentCoordinate.Y);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					// Can move Up?
					pntCheck = new Point(CurrentCoordinate.X, CurrentCoordinate.Y-1);
					if (_board.CanMove(pntCheck)) pts.Add(pntCheck);

					break;
			}
			return pts;
		}


		private CharacterPosition GetNextCharacterPosition()
		{
			CharacterPosition NewMouthPosition = CharacterPosition.LegsOut;

			switch (_currentCharacterPosition)
			{
				case CharacterPosition.LegsIn:
					NewMouthPosition = CharacterPosition.LegsOut;
					break;
					
				case CharacterPosition.LegsOut:
					NewMouthPosition = CharacterPosition.LegsIn;
					break;
			}
			return NewMouthPosition;
		}

		public override sealed void PaintCharacter(Graphics g)
		{
			Point p = CurrentLocationToPath;
			Bitmap b = null;

			if (_currentInvincibility == CharacterInvincibility.Vulnerable)
			{
				switch (_currentCharacterPosition)
				{
					case CharacterPosition.LegsIn:
						b = _bmpCharVincibleBlue1;
						break;

					case CharacterPosition.LegsOut:
						b = _bmpCharVincibleBlue1; 
						break;
				}
			}
			else
			{
				switch (_currentCharacterPosition)
				{
					case CharacterPosition.LegsIn:
						switch (Direction)
						{
							case CharacterDirection.Right:
								b = _bmpCharRight2;
								break;

							case CharacterDirection.Left:
								b = _bmpCharLeft2;					
								break;

							case CharacterDirection.Down:
								b = _bmpCharDown2;
								break;

							case CharacterDirection.Up:
								b = _bmpCharUp2;
								break;
						}
						break;

					case CharacterPosition.LegsOut:
						switch (Direction)
						{
							case CharacterDirection.Right:
								b = _bmpCharRight1;
								break;

							case CharacterDirection.Left:
								b = _bmpCharLeft1;					
								break;

							case CharacterDirection.Down:
								b = _bmpCharDown1;
								break;

							case CharacterDirection.Up:
								b = _bmpCharUp1;
								break;
						}
						break;
				}
			}		
			_collisionArea = new RectangleF(p.X + 1, p.Y + 1, b.Width - 2, b.Height - 2);

			if (_visible) g.DrawImageUnscaled(b, p.X, p.Y);
		}

		private void GenerateCharacters()
		{
			Graphics g; 
			SolidBrush b = new SolidBrush(_ghostColor);
			SolidBrush EyeBrush = new SolidBrush(Color.White);
			SolidBrush PupilBrush = new SolidBrush(Color.FromArgb(34, 32, 216));
			SolidBrush CoverBrush = new SolidBrush(Color.Black);
			SolidBrush VincibleBody1Brush = new SolidBrush(Color.FromArgb(33, 33, 222));
			SolidBrush VincibleBody2Brush = new SolidBrush(Color.FromArgb(222, 222, 222));
			SolidBrush VincibleFace1Brush = new SolidBrush(Color.FromArgb(241, 175, 155));
			SolidBrush VincibleFace2Brush = new SolidBrush(Color.FromArgb(255, 0, 0));

			Point p = new Point(-1, -1);

			Bitmap bmpCharBase = new Bitmap(_width, _height); 
			bmpCharBase = new Bitmap(_width, _height); 
			g = Graphics.FromImage(bmpCharBase);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillEllipse(b, p.X + 3, p.Y, _width - 4, _height-2);
			g.FillRectangle(b, p.X + 3, p.Y + _height/2 -1, _width - 4, _height/2);

			// Character Down 1
			_bmpCharDown1 = (Bitmap) bmpCharBase.Clone();
			g = Graphics.FromImage(_bmpCharDown1);
            g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillPie(CoverBrush, p.X + 3, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillRectangle(CoverBrush, p.X + (_width/2 - 1), p.Y + _height - 5, 4, 5);
			g.FillPie(CoverBrush, p.X +  _width - 11, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillEllipse(EyeBrush, p.X + 7, p.Y + 10, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 8, p.Y + 15, 5, 5);
			g.FillEllipse(EyeBrush, p.X + 17, p.Y + 10, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 18, p.Y + 15, 5, 5);
			_bmpCharDown1.MakeTransparent(Color.Black);

			// Character Down 2
			_bmpCharDown2 = (Bitmap) bmpCharBase.Clone();
			g = Graphics.FromImage(_bmpCharDown2);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillPie(CoverBrush, p.X - 4, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X + 6, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X +  _width - 14, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X +  _width - 4, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillEllipse(EyeBrush, p.X + 7, p.Y + 10, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 8, p.Y + 15, 5, 5);
			g.FillEllipse(EyeBrush, p.X + 17, p.Y + 10, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 18, p.Y + 15, 5, 5);
			_bmpCharDown2.MakeTransparent(Color.Black);

			// Character Up 1
			_bmpCharUp1 = (Bitmap) bmpCharBase.Clone();
			g = Graphics.FromImage(_bmpCharUp1);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillPie(CoverBrush, p.X + 3, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillRectangle(CoverBrush, p.X + (_width/2 - 1), p.Y + _height - 5, 4, 5);
			g.FillPie(CoverBrush, p.X +  _width - 11, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillEllipse(EyeBrush, p.X + 7, p.Y + 2, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 8, p.Y + 2, 5, 5);
			g.FillEllipse(EyeBrush, p.X + 17, p.Y + 2, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 18, p.Y + 2, 5, 5);
			_bmpCharUp1.MakeTransparent(Color.Black);

			// Character Up 2
			_bmpCharUp2 = (Bitmap) bmpCharBase.Clone();
			g = Graphics.FromImage(_bmpCharUp2);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillPie(CoverBrush, p.X - 4, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X + 6, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X +  _width - 14, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X +  _width - 4, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillEllipse(EyeBrush, p.X + 7, p.Y + 2, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 8, p.Y + 2, 5, 5);
			g.FillEllipse(EyeBrush, p.X + 17, p.Y + 2, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 18, p.Y + 2, 5, 5);
			_bmpCharUp2.MakeTransparent(Color.Black);

			// Character Left 1
			_bmpCharLeft1 = (Bitmap) bmpCharBase.Clone();
			g = Graphics.FromImage(_bmpCharLeft1);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillPie(CoverBrush, p.X + 3, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillRectangle(CoverBrush, p.X + (_width/2 - 1), p.Y + _height - 5, 4, 5);
			g.FillPie(CoverBrush, p.X +  _width - 11, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillEllipse(EyeBrush, p.X + 5, p.Y + 5, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 5, p.Y + 9, 5, 5);
			g.FillEllipse(EyeBrush, p.X + 15, p.Y + 5, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 15, p.Y + 9, 5, 5);
			_bmpCharLeft1.MakeTransparent(Color.Black);

			// Character Left 2
			_bmpCharLeft2 = (Bitmap) bmpCharBase.Clone();
			g = Graphics.FromImage(_bmpCharLeft2);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillPie(CoverBrush, p.X - 4, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X + 6, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X +  _width - 14, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X +  _width - 4, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillEllipse(EyeBrush, p.X + 5, p.Y + 5, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 5, p.Y + 9, 5, 5);
			g.FillEllipse(EyeBrush, p.X + 15, p.Y + 5, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 15, p.Y + 9, 5, 5);
			_bmpCharLeft2.MakeTransparent(Color.Black);

			// Character Right 1
			_bmpCharRight1 = (Bitmap) bmpCharBase.Clone();
			g = Graphics.FromImage(_bmpCharRight1);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillPie(CoverBrush, p.X + 3, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillRectangle(CoverBrush, p.X + (_width/2 - 1), p.Y + _height - 5, 4, 5);
			g.FillPie(CoverBrush, p.X +  _width - 11, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillEllipse(EyeBrush, p.X + 10, p.Y + 5, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 13, p.Y + 9, 5, 5);
			g.FillEllipse(EyeBrush, p.X + 20, p.Y + 5, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 23, p.Y + 9, 5, 5);
			_bmpCharRight1.MakeTransparent(Color.Black);

			// Character Right 2
			_bmpCharRight2 = (Bitmap) bmpCharBase.Clone();
			g = Graphics.FromImage(_bmpCharRight2);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillPie(CoverBrush, p.X - 4, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X + 6, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X +  _width - 14, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillPie(CoverBrush, p.X +  _width - 4, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillEllipse(EyeBrush, p.X + 10, p.Y + 5, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 13, p.Y + 9, 5, 5);
			g.FillEllipse(EyeBrush, p.X + 20, p.Y + 5, 7, 10);
			g.FillEllipse(PupilBrush, p.X + 23, p.Y + 9, 5, 5);
			_bmpCharRight2.MakeTransparent(Color.Black);

			// Character Vincible Blue 1
			_bmpCharVincibleBlue1 = new Bitmap(_width, _height); 
			g = Graphics.FromImage(_bmpCharVincibleBlue1);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillEllipse(VincibleBody1Brush, p.X + 3, p.Y, _width - 4, _height-2);
			g.FillRectangle(VincibleBody1Brush, p.X + 3, p.Y + _height/2 -1, _width - 4, _height/2);
			g.FillPie(CoverBrush, p.X + 3, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillRectangle(CoverBrush, p.X + (_width/2 - 1), p.Y + _height - 5, 4, 5);
			g.FillPie(CoverBrush, p.X +  _width - 11, p.Y + Height - 11, 10, 11, 45, 90);
			g.FillRectangle(VincibleFace1Brush, p.X + 10, p.Y + 11, 4, 4);
			g.FillRectangle(VincibleFace1Brush, p.X + 18, p.Y + 11, 4, 4);
			g.DrawArc(new Pen(VincibleFace1Brush, 1), p.X + 5, p.Y + 20, 5, 4, 180, 160);
			g.DrawArc(new Pen(VincibleFace1Brush, 1), p.X + 9, p.Y + 18, 5, 4, 20, 160);
			g.DrawArc(new Pen(VincibleFace1Brush, 1), p.X + 13, p.Y + 20, 6, 4, 200, 140);
			g.DrawArc(new Pen(VincibleFace1Brush, 1), p.X + 18, p.Y + 18, 5, 4, 40, 140);
			g.DrawArc(new Pen(VincibleFace1Brush, 1), p.X + 22, p.Y + 20, 5, 4, 200, 160);

			b.Dispose();
			CoverBrush.Dispose();
			EyeBrush.Dispose();
			PupilBrush.Dispose();
		}

		

	}
}
