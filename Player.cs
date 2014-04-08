using System;

namespace Chomp
{
	public enum BoardPlayer
	{
		Player1 = 0,
		Player2 = 1
	}

	public class Player
	{
		private BoardPlayer _playerType;
		private int _score;
		private int _lives;
		private int _freeLives;
		private bool _gameOver;
		private GameLevel _level;

		public Player(BoardPlayer PlayerType)
		{
			_playerType = PlayerType;
			_score = 0;
			_lives = 0;
			_freeLives = 0;
			_gameOver = true;
		}

		public int Score
		{
			get {return _score;}
			set {_score = value;}
		}

		public int Lives
		{
			get {return _lives;}
			set {_lives = value;}
		}

		public int FreeLives
		{
			get {return _freeLives;}
			set {_freeLives = value;}
		}

		public GameLevel CurrentLevel
		{
			get {return _level;}
			set {_level = value;}
		}

		public bool GameOver
		{
			get {return _gameOver;}
			set 
			{
				_gameOver = value;
				if (_gameOver) _freeLives = 0;
			}
		}
	}
}
