using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Chomp
{
	public enum FruitType
	{
		Cherry = 0,
		Strawberry = 1,
		Orange = 2,
		Apple = 3,
		Grape = 4,
        Bell = 5,
		Galaxian = 6,
		Key = 7
	}

	public class GameLevel
	{
        private Bitmap		_btmFruit;
		private int			_characterSpeed;
		private int			_invincibilityLength;
		private FruitType	_fruit;
		private int			_levelNumber;

		public GameLevel(int LevelNumber, FruitType Fruit, int CharacterSpeed, int InvincibilityLength)
		{
			_fruit = Fruit;
			_characterSpeed = CharacterSpeed;
			_invincibilityLength = InvincibilityLength;
		}

		public int CharacterSpeed
		{
			get {return _characterSpeed;}
			set {_characterSpeed = value;}
		}

		public int InvincibilityLength
		{
			get {return _invincibilityLength;}
			set {_invincibilityLength = value;}
		}

		public int LevelNumber
		{
			get {return _levelNumber;}
			set {_levelNumber = value;}
		}


		public FruitType Fruit
		{
			get {return _fruit;}
			set {_fruit = value;}
		}
	}
}
