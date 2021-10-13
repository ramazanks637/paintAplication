using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintAplication
{
	class Points
	{
		private int x1;
		private int x2;
		private int y1;
		private int y2;		
		private Color linePaintColor;
		private int size;
		private string type;

		
		public Points(int x1, int x2, int y1, int y2, Color linePaintColor, int size, string type)
		{
			this.x1 = x1;
			this.x2 = x2;
			this.y1 = y1;
			this.y2 = y2;
			this.linePaintColor = linePaintColor;
			this.size = size;
			this.type = type;
		}

		public int X1 
		{
			get { return x1; } 
			set { x1 = value; } 
		}

		public int X2 
		{
			get { return x2; } 
			set { x2 = value; } 
		}

		public int Y1 
		{
			get { return y1; } 
			set { y1 = value; } 
		}

		public int Y2 
		{
			get { return y2; } 
			set { y2 = value; } 
		}

		public Color LinePaintColor 
		{
			get { return linePaintColor; }
			set { linePaintColor = value; } 
		}

		public int Size 
		{
			get { return size; }
			set { size = value; } 
		}

		public string Type 
		{
			get { return type; } 
			set { type = value; } 
		}


	}
}
