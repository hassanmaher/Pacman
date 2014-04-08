using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Chomp
{
	public enum CharacterDirection
	{
		Left = 0,
		Right = 1,
		Down = 2,
		Up = 3,
		None = 4
	}

	public enum CharacterState
	{
		Moving = 0,
		Stopped = 1,
	}

	public enum CharacterInvincibility
	{
		Invincible = 0,
		Vulnerable = 1
	}

	public abstract class GameCharacter
	{
		protected Point _currentLocation;
		protected Point _previousLocation;
		protected Point _startingLocation;
				
		protected  int _width;
		protected  int _height;
		protected  int _moveInterval;
		
		protected  CharacterDirection _direction;
		protected  CharacterDirection _attemptedDirection;
		protected  CharacterDirection _previousDirection;

		protected CharacterState _currentCharacterState;
		protected CharacterInvincibility _currentInvincibility;
				
		protected  GameBoard _board;

		protected int _frame = 0;
		protected RectangleF _collisionArea;

		protected bool _visible;
        
		public GameCharacter(GameBoard Board, Point StartingLocation, int Width, int Height)
		{
			_board = Board;
			_startingLocation = StartingLocation;
			_currentLocation = _startingLocation;
			_moveInterval = 1;
			_width = Width;
			_height = Height;
			_currentCharacterState = CharacterState.Stopped;
			_visible = true;
		}

		public virtual void Initialize()
		{

		}

		public int Width
		{
			get {return _width;}
			set {_width = value;}
		}

		public int Height
		{
			get {return _height;}
			set {_height = value;}
		}

		public CharacterDirection Direction
		{
			get {return _direction;}
			set {_direction = value;}
		}

		public CharacterDirection AttemptedDirection
		{
			get {return _attemptedDirection;}
			set {_attemptedDirection = value;}
		}

		public CharacterInvincibility CurrentInvinsibility
		{
			get {return _currentInvincibility;}
			set {_currentInvincibility = value;}
		}

		public GameBoard GameBoard
		{
			get {return _board;}
			set {_board = value;}
		}

		public bool Visible
		{
			get {return _visible;}
			set {_visible = value;}
		}

		public Point StartingLocation
		{
			get {return _startingLocation;}
			set {_startingLocation = value;}
		}

		public Point CurrentLocation
		{
			get {return _currentLocation;}
			set {_currentLocation = value;}
		}

		public Point CurrentLocationToPath
		{
			get {return new Point(_currentLocation.X + 1 - _width / 2, _currentLocation.Y + 1 - _height / 2);}
		}

		public Point PreviousLocation
		{
			get {return _previousLocation;}
			set {_previousLocation = value;}
		}

		public bool HasCollided(Point p)
		{
			if (_collisionArea.Contains(p) && _visible == true)
				return true;
			else
				return false;
		}

		public int MoveInterval
		{
			get {return _moveInterval;}
			set {_moveInterval = value;}
		}

		public virtual void Move()
		{
			_currentCharacterState = CharacterState.Stopped;

			// Tunnel
			if (_currentLocation.X == 448) 
				_currentLocation.X = 0;
			else if (_currentLocation.X == 0)
				_currentLocation.X = 448;

			if (_attemptedDirection != CharacterDirection.None && _board.CanMove(GetNextCoordinate(_attemptedDirection, _currentLocation)))
			{
				// Test attempted direction
				MoveToEndPoint(_attemptedDirection, GetDestinationCoordinate(_attemptedDirection, _currentLocation));
				_direction = _attemptedDirection;
				_previousDirection = _attemptedDirection;
				_attemptedDirection = CharacterDirection.None;
				_currentCharacterState = CharacterState.Moving;
				return;
			}
			else
			{
				_direction = _previousDirection;
			}
			
			if (_board.CanMove(GetNextCoordinate(_direction, _currentLocation)))
			{
				// Test Actual Direction
				MoveToEndPoint(_direction, GetDestinationCoordinate(_direction, _currentLocation));		
				_previousDirection = _direction;
				_currentCharacterState = CharacterState.Moving;
			}
		}

		protected virtual void MoveToEndPoint(CharacterDirection Direction, Point p)
		{
			Point TestPoint;
			Point TestAttemptedPoint;

			int PointsToMove = _moveInterval;

			while (PointsToMove > 0)
			{
				if (_attemptedDirection != CharacterDirection.None)
				{
					TestAttemptedPoint = GetNextCoordinate(_attemptedDirection, _currentLocation);
					if (_board.CanMove(TestAttemptedPoint))
					{
						_currentLocation = TestAttemptedPoint;
						_board.Pellets.RemovePellet(_currentLocation);
						PointsToMove --;
					}
				}
									
				TestPoint = GetNextCoordinate(Direction, _currentLocation);
				if (_board.CanMove(TestPoint))
				{
					_currentLocation = TestPoint;
					_board.Pellets.RemovePellet(_currentLocation);
					PointsToMove --;
				}
				else
				{
					break;
				}
			}
		}

		protected Point GetNextCoordinate(CharacterDirection Direction, Point CurrentCoordinate)
		{
			int x = CurrentCoordinate.X;
			int y = CurrentCoordinate.Y;

			switch (Direction)
			{
				case CharacterDirection.Right:
					x++;
					break;
				case CharacterDirection.Left:
					x--;
					break;
				case CharacterDirection.Down:
					y++;
					break;
				case CharacterDirection.Up:
					y--;
					break;
			}
			return new Point(x, y);
		}

		protected Point GetDestinationCoordinate(CharacterDirection Direction, Point CurrentCoordinate)
		{
			int x = CurrentCoordinate.X;
			int y = CurrentCoordinate.Y;

			switch (Direction)
			{
				case CharacterDirection.Right:
					x+= _moveInterval;
					break;
				case CharacterDirection.Left:
					x-= _moveInterval;
					break;
				case CharacterDirection.Down:
					y+= _moveInterval;
					break;
				case CharacterDirection.Up:
					y-= _moveInterval;
					break;
			}
			return new Point(x, y);
		}

		public virtual void PaintCharacter(Graphics g)
		{
			//g.DrawImageUnscaled(b, p.X, p.Y);
		}

		public virtual void ChangeInvincibility(CharacterInvincibility Invinsibility)
		{
			switch (Invinsibility)
			{
				case CharacterInvincibility.Invincible:
				{
					if (_currentInvincibility != CharacterInvincibility.Invincible)
					{
						_moveInterval += 2;
						_currentInvincibility = CharacterInvincibility.Invincible;
					}
					break;
				}
				
				case CharacterInvincibility.Vulnerable:
				{
					if (_currentInvincibility != CharacterInvincibility.Vulnerable)
					{
						_moveInterval -= 2;
						_currentInvincibility = CharacterInvincibility.Vulnerable;
					}
					break;
				}
			}
		}

	}
}
