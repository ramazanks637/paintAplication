using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace paintAplication
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

		//kurucu
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

		public int X1 // İsim yöntemi
		{
			get { return x1; } //Mülk almak
			set { x1 = value; } // Mülkü ayarlamak
		}

		public int X2 // İsim yöntemi
		{
			get { return x2; } //Mülk almak
			set { x2 = value; } // Mülkü ayarlamak
		}

		public int Y1 // İsim yöntemi
		{
			get { return y1; } //Mülk almak
			set { y1 = value; } // Mülkü ayarlamak
		}

		public int Y2 // İsim yöntemi
		{
			get { return y2; } //Mülk almak
			set { y2 = value; } // Mülkü ayarlamak
		}

		public Color LinePaintColor // İsim yöntemi
		{
			get { return linePaintColor; } //Mülk almak
			set { linePaintColor = value; } // Mülkü ayarlamak
		}

		public int Size // İsim yöntemi
		{
			get { return size; } //Mülk almak
			set { size = value; } // Mülkü ayarlamak
		}

		public string Type // İsim yöntemi
		{
			get { return type; } //Mülk almak
			set { type = value; } // Mülkü ayarlamak
		}
	}
}
