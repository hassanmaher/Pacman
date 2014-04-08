using System;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Chomp
{
	

	public class PacmanCharacter : GameCharacter
	{
		Bitmap _bmpCharClosed;
		Bitmap _bmpCharOpenMidLeft;
		Bitmap _bmpCharOpenMidRight;
		Bitmap _bmpCharOpenMidUp;
		Bitmap _bmpCharOpenMidDown;
		Bitmap _bmpCharOpenAllLeft;
		Bitmap _bmpCharOpenAllRight;
		Bitmap _bmpCharOpenAllUp;
		Bitmap _bmpCharOpenAllDown;
		
		public enum CharacterPosition
		{
			Closed = 0,
			OpenMidLow = 1,
			OpenMidHigh = 3,
			OpenAll = 4,
		}

		private CharacterPosition _currentCharacterPosition;

		public PacmanCharacter(GameBoard Board, Point CurrentLocation, int Width, int Height): base (Board, CurrentLocation, Width, Height)
		{
			_currentCharacterPosition = CharacterPosition.Closed;
		}	

		public CharacterPosition Position
		{
			get {return _currentCharacterPosition;}
			set {_currentCharacterPosition = value;}
		}
		
		public override sealed void Initialize()
		{
			GenerateCharacters();
		}

		public void AnimateDeath()
		{
			
		}
		
		public override sealed void Move()
		{
			base.Move();

			if (_currentCharacterState == CharacterState.Moving) _currentCharacterPosition = GetNextCharacterPosition();
		}

		public override sealed void PaintCharacter(Graphics g)
		{
			Point p = CurrentLocationToPath;
			Bitmap b = null;
			
			switch (_currentCharacterPosition)
			{
				case CharacterPosition.Closed:
					b = _bmpCharClosed;
					break;

				case CharacterPosition.OpenMidLow:
				switch (_direction)
				{
					case CharacterDirection.Down:
						b = _bmpCharOpenMidDown;
						break;
					case CharacterDirection.Up:
						b = _bmpCharOpenMidUp;
						break;
					case CharacterDirection.Right:
						b = _bmpCharOpenMidRight;
						break;
					case CharacterDirection.Left:
						b = _bmpCharOpenMidLeft;
						break;
				}
					break;

				case CharacterPosition.OpenMidHigh:
				switch (_direction)
				{
					case CharacterDirection.Down:
						b = _bmpCharOpenMidDown;
						break;
					case CharacterDirection.Up:
						b = _bmpCharOpenMidUp;
						break;
					case CharacterDirection.Right:
						b = _bmpCharOpenMidRight;
						break;
					case CharacterDirection.Left:
						b = _bmpCharOpenMidLeft;
						break;
				}
					break;

				case CharacterPosition.OpenAll:
				switch (_direction)
				{
					case CharacterDirection.Down:
						b = _bmpCharOpenAllDown;
						break;
					case CharacterDirection.Up:
						b = _bmpCharOpenAllUp;
						break;
					case CharacterDirection.Right:
						b = _bmpCharOpenAllRight;
						break;
					case CharacterDirection.Left:
						b = _bmpCharOpenAllLeft;
						break;
				}
					
					break;
			}

			_collisionArea = new RectangleF(p.X + 1, p.Y + 1, b.Width - 2, b.Height - 2);
			
			if (_visible) g.DrawImageUnscaled(b, p.X, p.Y);
		}
				
		private void GenerateCharacters()
		{
			Graphics g;
			SolidBrush b = new SolidBrush(Color.FromArgb(255, 255, 0));
			SolidBrush CoverBrush = new SolidBrush(Color.Black);

			Point p = new Point(-1, -1);

			_bmpCharClosed = new Bitmap(_width, _height); 
			g = Graphics.FromImage(_bmpCharClosed);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillEllipse(b, p.X, p.Y, _width, _height);
		
			_bmpCharOpenMidLeft = new Bitmap(_width, _height);
			g = Graphics.FromImage(_bmpCharOpenMidLeft);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillEllipse(b, p.X, p.Y, _width, _height);
			g.FillPie(CoverBrush, p.X-2, p.Y, 41, 30, 155, 50);
			g.FillRectangle(CoverBrush, p.X, p.Y + 5, 4, 6);
			g.FillRectangle(CoverBrush, p.X, p.Y + 20, 4, 6);

			_bmpCharOpenMidRight = (Bitmap)_bmpCharOpenMidLeft.Clone();
			_bmpCharOpenMidRight.RotateFlip(RotateFlipType.Rotate180FlipNone);
			_bmpCharOpenMidRight.MakeTransparent(Color.Black);

			_bmpCharOpenMidUp = (Bitmap)_bmpCharOpenMidLeft.Clone();
			_bmpCharOpenMidUp.RotateFlip(RotateFlipType.Rotate90FlipNone);
			_bmpCharOpenMidUp.MakeTransparent(Color.Black);

			_bmpCharOpenMidDown = (Bitmap)_bmpCharOpenMidLeft.Clone();
			_bmpCharOpenMidDown.RotateFlip(RotateFlipType.Rotate270FlipNone);
			_bmpCharOpenMidDown.MakeTransparent(Color.Black);

			_bmpCharOpenAllLeft = new Bitmap(_width, _height);
			g = Graphics.FromImage(_bmpCharOpenAllLeft);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillEllipse(b, p.X, p.Y, _width, _height);
			g.FillPie(CoverBrush, p.X-2, p.Y, 41, 30, 135, 90);
			g.FillRectangle(CoverBrush, p.X+2, p.Y, 6, 6);
			g.FillRectangle(CoverBrush, p.X+2, p.Y + 25, 6, 6);
			_bmpCharOpenAllLeft.MakeTransparent(Color.Black);

			_bmpCharOpenAllRight = (Bitmap)_bmpCharOpenAllLeft.Clone();
			_bmpCharOpenAllRight.RotateFlip(RotateFlipType.Rotate180FlipNone);
			_bmpCharOpenAllRight.MakeTransparent(Color.Black);

			_bmpCharOpenAllUp = (Bitmap)_bmpCharOpenAllLeft.Clone();
			_bmpCharOpenAllUp.RotateFlip(RotateFlipType.Rotate90FlipNone);
			_bmpCharOpenAllUp.MakeTransparent(Color.Black);

			_bmpCharOpenAllDown = (Bitmap)_bmpCharOpenAllLeft.Clone();
			_bmpCharOpenAllDown.RotateFlip(RotateFlipType.Rotate270FlipNone);
			_bmpCharOpenAllDown.MakeTransparent(Color.Black);
		}

		private CharacterPosition GetNextCharacterPosition()
		{
			CharacterPosition NewMouthPosition = CharacterPosition.Closed;

			switch (_currentCharacterPosition)
			{
				case CharacterPosition.Closed:
					NewMouthPosition = CharacterPosition.OpenMidHigh;
					break;
					
				case CharacterPosition.OpenMidHigh:
					NewMouthPosition = CharacterPosition.OpenAll;
					break;
					
				case CharacterPosition.OpenAll:
					NewMouthPosition = CharacterPosition.OpenMidLow;
					break;
					
				case CharacterPosition.OpenMidLow:
					NewMouthPosition = CharacterPosition.Closed;
					break;
			}
			return NewMouthPosition;
		}
	}
}
