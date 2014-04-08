using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Chomp
{
	public enum PelletType
	{
		RegularPellet = 0,
		PowerPellet = 1
	}

	public class Pellets
	{
		private ArrayList _pellets;
		private ArrayList _powerPellets;

		private PelletType _pelletType;
		private SolidBrush _brushPowerPellet;

		public const int POWER_PELLET_POINTS = 50;
		public const int REGULAR_PELLET_POINTS = 10;

		public delegate void onPowerPelletEatenEventHandler (object Source, PelletEventArgs e);
		public event onPowerPelletEatenEventHandler OnPelletEaten;
		
		public Pellets()
		{
			_brushPowerPellet = new SolidBrush(Color.FromArgb(255,194,159));
		}

		public int RegularPelletCount
		{
			get {return _pellets.Count;}
		}

		public int PowerPelletCount
		{
			get {return _powerPellets.Count;}
		}

		public void BlinkPowerPellet()
		{
			if (_brushPowerPellet.Color == Color.Black)
			{
				_brushPowerPellet.Color = Color.FromArgb(255,194,159);
			}
			else
			{
				_brushPowerPellet.Color = Color.Black;
			}
		}

		public void RemovePellet(Point Location)
		{
			Point pntPelletEaten = Point.Empty;
			Point pntPowerPelletEaten = Point.Empty;
			
			if (_pellets.Contains(new Point(Location.X - 2, Location.Y - 2)))
			{
				pntPelletEaten = new Point(Location.X - 2, Location.Y - 2);
			}
			else if (_powerPellets.Contains(new Point(Location.X - 2, Location.Y - 2)))
			{
				pntPowerPelletEaten = new Point(Location.X - 2, Location.Y - 2);
			}
			else if (_pellets.Contains(new Point(Location.X - 12, Location.Y - 2))) 
			{
				pntPelletEaten = new Point(Location.X - 12, Location.Y - 2);
			}
			else if (_powerPellets.Contains(new Point(Location.X - 12, Location.Y - 2))) 
			{
				pntPowerPelletEaten = new Point(Location.X - 12, Location.Y - 2);
			}
			else if (_pellets.Contains(new Point(Location.X + 12, Location.Y - 2))) 
			{
				pntPelletEaten = new Point(Location.X + 12, Location.Y - 2);
			}
			else if (_powerPellets.Contains(new Point(Location.X + 12, Location.Y - 2))) 
			{
				pntPowerPelletEaten = new Point(Location.X + 12, Location.Y - 2);
			}
			else if (_pellets.Contains(new Point(Location.X -2, Location.Y - 12))) 
			{
				pntPelletEaten = new Point(Location.X -2, Location.Y - 12);
			}
			else if (_powerPellets.Contains(new Point(Location.X -2, Location.Y - 12))) 
			{
				pntPowerPelletEaten = new Point(Location.X -2, Location.Y - 12);
			}
			else if (_pellets.Contains(new Point(Location.X -2, Location.Y + 12))) 
			{
				pntPelletEaten = new Point(Location.X -2, Location.Y + 12);
			}
			else if (_powerPellets.Contains(new Point(Location.X -2, Location.Y + 12))) 
			{
				pntPowerPelletEaten = new Point(Location.X -2, Location.Y + 12);
			}

			if (pntPelletEaten != Point.Empty) 
			{
				_pellets.Remove(pntPelletEaten);

				if (OnPelletEaten != null) OnPelletEaten(this, new PelletEventArgs(PelletType.RegularPellet, pntPelletEaten));
			}
			else if (pntPowerPelletEaten != Point.Empty) 
			{
				_powerPellets.Remove(pntPowerPelletEaten);

				if (OnPelletEaten != null) OnPelletEaten(this, new PelletEventArgs(PelletType.PowerPellet, pntPowerPelletEaten));
			}
		}

		public void GeneratePellets()
		{
			// Add Power Pellets
			_powerPellets = new ArrayList();
			_powerPellets.Add(new Point(21, 60));
			_powerPellets.Add(new Point(420, 60));
			_powerPellets.Add(new Point(21, 414));
			_powerPellets.Add(new Point(420, 414));

			// Row 1 Accross
			_pellets = new ArrayList();
			_pellets.Add(new Point(21, 24));
			_pellets.Add(new Point(37, 24));
			_pellets.Add(new Point(53, 24));
			_pellets.Add(new Point(69, 24));
			_pellets.Add(new Point(85, 24));
			_pellets.Add(new Point(101, 24));
			_pellets.Add(new Point(117, 24));
			_pellets.Add(new Point(133, 24));
			_pellets.Add(new Point(149, 24));
			_pellets.Add(new Point(165, 24));
			_pellets.Add(new Point(181, 24));
			_pellets.Add(new Point(197, 24));
			_pellets.Add(new Point(244, 24));
			_pellets.Add(new Point(260, 24));
			_pellets.Add(new Point(276, 24));
			_pellets.Add(new Point(292, 24));
			_pellets.Add(new Point(308, 24));
			_pellets.Add(new Point(324, 24));
			_pellets.Add(new Point(340, 24));
			_pellets.Add(new Point(356, 24));
			_pellets.Add(new Point(372, 24));
			_pellets.Add(new Point(388, 24));
			_pellets.Add(new Point(404, 24));
			_pellets.Add(new Point(420, 24));

			// Row 2 Accross
			_pellets.Add(new Point(21, 42));
			_pellets.Add(new Point(101, 42));
			_pellets.Add(new Point(197, 42));
			_pellets.Add(new Point(244, 42));
			_pellets.Add(new Point(340, 42));
			_pellets.Add(new Point(420, 42));

			// Row 3 Accorss
			_pellets.Add(new Point(101, 60));
			_pellets.Add(new Point(197, 60));
			_pellets.Add(new Point(244, 60));
			_pellets.Add(new Point(340, 60));
			
			// Row 4 Accorss
			_pellets.Add(new Point(21, 77));
			_pellets.Add(new Point(101, 77));
			_pellets.Add(new Point(197, 77));
			_pellets.Add(new Point(244, 77));
			_pellets.Add(new Point(340, 77));
			_pellets.Add(new Point(420, 77));
					
			// Row 5 Across
			_pellets.Add(new Point(21, 95));
			_pellets.Add(new Point(37, 95));
			_pellets.Add(new Point(53, 95));
			_pellets.Add(new Point(69, 95));
			_pellets.Add(new Point(85, 95));
			_pellets.Add(new Point(101, 95));
			_pellets.Add(new Point(117, 95));
			_pellets.Add(new Point(133, 95));
			_pellets.Add(new Point(149, 95));
			_pellets.Add(new Point(165, 95));
			_pellets.Add(new Point(181, 95));
			_pellets.Add(new Point(197, 95));
			_pellets.Add(new Point(213, 95));
			_pellets.Add(new Point(229, 95));
			_pellets.Add(new Point(244, 95));
			_pellets.Add(new Point(260, 95));
			_pellets.Add(new Point(276, 95));
			_pellets.Add(new Point(292, 95));
			_pellets.Add(new Point(308, 95));
			_pellets.Add(new Point(324, 95));
			_pellets.Add(new Point(340, 95));
			_pellets.Add(new Point(356, 95));
			_pellets.Add(new Point(372, 95));
			_pellets.Add(new Point(388, 95));
			_pellets.Add(new Point(404, 95));
			_pellets.Add(new Point(420, 95));

			// Row 6 Across
			_pellets.Add(new Point(21, 112));
			_pellets.Add(new Point(101, 112));
			_pellets.Add(new Point(149, 112));
			_pellets.Add(new Point(292, 112));
			_pellets.Add(new Point(340, 112));
			_pellets.Add(new Point(420, 112));

			// Row 7 Across
			_pellets.Add(new Point(21, 130));
			_pellets.Add(new Point(101, 130));
			_pellets.Add(new Point(149, 130));
			_pellets.Add(new Point(292, 130));
			_pellets.Add(new Point(340, 130));
			_pellets.Add(new Point(420, 130));

			// Row 8 Across
			_pellets.Add(new Point(21, 148));
			_pellets.Add(new Point(37, 148));
			_pellets.Add(new Point(53, 148));
			_pellets.Add(new Point(69, 148));
			_pellets.Add(new Point(85, 148));
			_pellets.Add(new Point(101, 148));
			_pellets.Add(new Point(149, 148));
			_pellets.Add(new Point(165, 148));
			_pellets.Add(new Point(181, 148));
			_pellets.Add(new Point(197, 148));
			_pellets.Add(new Point(244, 148));
			_pellets.Add(new Point(260, 148));
			_pellets.Add(new Point(276, 148));
			_pellets.Add(new Point(292, 148));
			_pellets.Add(new Point(340, 148));
			_pellets.Add(new Point(356, 148));
			_pellets.Add(new Point(372, 148));
			_pellets.Add(new Point(388, 148));
			_pellets.Add(new Point(404, 148));
			_pellets.Add(new Point(420, 148));

			// Row 9 Across
			_pellets.Add(new Point(101, 166));
			_pellets.Add(new Point(340, 166));
			
			// Row 10 Across
			_pellets.Add(new Point(101, 183));
			_pellets.Add(new Point(340, 183));
			
			// Row 11 Across
			_pellets.Add(new Point(101, 201));
			_pellets.Add(new Point(340, 201));
			
			// Row 12 Across
			_pellets.Add(new Point(101, 219));
			_pellets.Add(new Point(340, 219));
			
			// Row 13 Across
			_pellets.Add(new Point(101, 236));
			_pellets.Add(new Point(340, 236));
			
			// Row 14 Across
			_pellets.Add(new Point(101, 254));
			_pellets.Add(new Point(340, 254));
			
			// Row 15 Across
			_pellets.Add(new Point(101, 272));
			_pellets.Add(new Point(340, 272));
			
			// Row 16 Across
			_pellets.Add(new Point(101, 290));
			_pellets.Add(new Point(340, 290));
			
			// Row 17 Across
			_pellets.Add(new Point(101, 307));
			_pellets.Add(new Point(340, 307));
			
			// Row 18 Across
			_pellets.Add(new Point(101, 325));
			_pellets.Add(new Point(340, 325));
			
			// Row 19 Across
			_pellets.Add(new Point(101, 343));
			_pellets.Add(new Point(340, 343));
			
			// Row 20 Across
			_pellets.Add(new Point(21, 360));
			_pellets.Add(new Point(37, 360));
			_pellets.Add(new Point(53, 360));
			_pellets.Add(new Point(69, 360));
			_pellets.Add(new Point(85, 360));
			_pellets.Add(new Point(101, 360));
			_pellets.Add(new Point(117, 360));
			_pellets.Add(new Point(133, 360));
			_pellets.Add(new Point(149, 360));
			_pellets.Add(new Point(165, 360));
			_pellets.Add(new Point(181, 360));
			_pellets.Add(new Point(197, 360));
			_pellets.Add(new Point(244, 360));
			_pellets.Add(new Point(260, 360));
			_pellets.Add(new Point(276, 360));
			_pellets.Add(new Point(292, 360));
			_pellets.Add(new Point(308, 360));
			_pellets.Add(new Point(324, 360));
			_pellets.Add(new Point(340, 360));
			_pellets.Add(new Point(356, 360));
			_pellets.Add(new Point(372, 360));
			_pellets.Add(new Point(388, 360));
			_pellets.Add(new Point(404, 360));
			_pellets.Add(new Point(420, 360));

			// Row 21 Across
			_pellets.Add(new Point(21, 378));
			_pellets.Add(new Point(101, 378));
			_pellets.Add(new Point(197, 378));
			_pellets.Add(new Point(244, 378));
			_pellets.Add(new Point(340, 378));
			_pellets.Add(new Point(420, 378));

			// Row 22 Across
			_pellets.Add(new Point(21, 396));
			_pellets.Add(new Point(101, 396));
			_pellets.Add(new Point(197, 396));
			_pellets.Add(new Point(244, 396));
			_pellets.Add(new Point(340, 396));
			_pellets.Add(new Point(420, 396));

			// Row 23 Across
			_pellets.Add(new Point(37, 414));
			_pellets.Add(new Point(53, 414));
			_pellets.Add(new Point(101, 414));
			_pellets.Add(new Point(117, 414));
			_pellets.Add(new Point(133, 414));
			_pellets.Add(new Point(149, 414));
			_pellets.Add(new Point(165, 414));
			_pellets.Add(new Point(181, 414));
			_pellets.Add(new Point(197, 414));
			_pellets.Add(new Point(244, 414));
			_pellets.Add(new Point(260, 414));
			_pellets.Add(new Point(276, 414));
			_pellets.Add(new Point(292, 414));
			_pellets.Add(new Point(308, 414));
			_pellets.Add(new Point(324, 414));
			_pellets.Add(new Point(340, 414));
			_pellets.Add(new Point(388, 414));
			_pellets.Add(new Point(404, 414));
			
			// Row 24 Across
			_pellets.Add(new Point(53, 431));
			_pellets.Add(new Point(101, 431));
			_pellets.Add(new Point(149, 431));
			_pellets.Add(new Point(292, 431));
			_pellets.Add(new Point(340, 431));
			_pellets.Add(new Point(388, 431));
			
			// Row 25 Across
			_pellets.Add(new Point(53, 449));
			_pellets.Add(new Point(101, 449));
			_pellets.Add(new Point(149, 449));
			_pellets.Add(new Point(292, 449));
			_pellets.Add(new Point(340, 449));
			_pellets.Add(new Point(388, 449));
			
			// Row 26 Across
			_pellets.Add(new Point(21, 466));
			_pellets.Add(new Point(37, 466));
			_pellets.Add(new Point(53, 466));
			_pellets.Add(new Point(69, 466));
			_pellets.Add(new Point(85, 466));
			_pellets.Add(new Point(101, 466));
			_pellets.Add(new Point(149, 466));
			_pellets.Add(new Point(165, 466));
			_pellets.Add(new Point(181, 466));
			_pellets.Add(new Point(197, 466));
			_pellets.Add(new Point(244, 466));
			_pellets.Add(new Point(260, 466));
			_pellets.Add(new Point(276, 466));
			_pellets.Add(new Point(292, 466));
			_pellets.Add(new Point(340, 466));
			_pellets.Add(new Point(356, 466));
			_pellets.Add(new Point(372, 466));
			_pellets.Add(new Point(388, 466));
			_pellets.Add(new Point(404, 466));
			_pellets.Add(new Point(420, 466));

			// Row 27 Across
			_pellets.Add(new Point(21, 484));
			_pellets.Add(new Point(197, 484));
			_pellets.Add(new Point(244, 484));
			_pellets.Add(new Point(420, 484));
			
			// Row 28 Across
			_pellets.Add(new Point(21, 502));
			_pellets.Add(new Point(197, 502));
			_pellets.Add(new Point(244, 502));
			_pellets.Add(new Point(420, 502));

			// Row 29 Across
			_pellets.Add(new Point(21, 519));
			_pellets.Add(new Point(37, 519));
			_pellets.Add(new Point(53, 519));
			_pellets.Add(new Point(69, 519));
			_pellets.Add(new Point(85, 519));
			_pellets.Add(new Point(101, 519));
			_pellets.Add(new Point(117, 519));
			_pellets.Add(new Point(133, 519));
			_pellets.Add(new Point(149, 519));
			_pellets.Add(new Point(165, 519));
			_pellets.Add(new Point(181, 519));
			_pellets.Add(new Point(197, 519));
			_pellets.Add(new Point(213, 519));
			_pellets.Add(new Point(229, 519));
			_pellets.Add(new Point(244, 519));
			_pellets.Add(new Point(260, 519));
			_pellets.Add(new Point(276, 519));
			_pellets.Add(new Point(292, 519));
			_pellets.Add(new Point(308, 519));
			_pellets.Add(new Point(324, 519));
			_pellets.Add(new Point(340, 519));
			_pellets.Add(new Point(356, 519));
			_pellets.Add(new Point(372, 519));
			_pellets.Add(new Point(388, 519));
			_pellets.Add(new Point(404, 519));
			_pellets.Add(new Point(420, 519));
			_pellets.Add(new Point(420, 519));
		}

		
		public void PaintPellets(Graphics _graphics)
		{
			//Pellets
			SolidBrush b = new SolidBrush(Color.FromArgb(255,194,159));
			
			foreach(Point p in _pellets)
			{
				_graphics.FillEllipse(b, p.X, p.Y, 5, 5);
			}

			foreach(Point p in _powerPellets)
			{
				_graphics.FillEllipse(_brushPowerPellet, p.X - 5, p.Y - 5, 15, 15);
			}
		}

		public class PelletEventArgs : EventArgs
		{
			private Point _pelletLocation;
			private PelletType _pelletType;

			public PelletEventArgs(PelletType PType, Point PelletLocation)
			{
				_pelletType = PType;
				_pelletLocation = PelletLocation;
			}

			public Point PelletLocation
			{
				get {return _pelletLocation;}
				set {_pelletLocation = value;}
			}

			public PelletType PelletType
			{
				get {return _pelletType;}
				set {_pelletType = value;}
			}
		}
	}
}
