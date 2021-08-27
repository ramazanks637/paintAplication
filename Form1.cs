using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paintAplication
{
    public partial class Form1 : Form
    {

		// Initializing all the required variables
		private int shapeSelected = 0;
		private Color linePaintColor;
		int pSize = 1;
		int bSize = 5;

		public Point current = new Point();
		public Point old = new Point();
		Points p;
		Bitmap surface;
		Graphics g;
		bool moving = false;
		Pen pen;
		bool pencil = true;
		bool brush = false;
		bool eraser = false;
		bool dotBool = false;
		bool userEnteredCanvas = false;
		Point p1 = new Point();
		Point p2 = new Point();
		String imageName = "";
		Stack<List<Points>> undoStack = new Stack<List<Points>>();
		Stack<List<Points>> redoStack = new Stack<List<Points>>();
		List<Points> curvePoints = new List<Points>();
		private readonly object _undoRedoLocker = new object();
		public List<string> recentlyOpened = new List<string>();
		Queue<string> MRUlist = new Queue<string>();
		const int MRUnumber = 5;
		bool saveSuccess = false;
		public Form1()
        {
            InitializeComponent();
			//Initializing the value of color to be black
			linePaintColor = Color.Black;
			//Setting a pen with default color as linepaintcolor and size of 1
			pen = new Pen(linePaintColor, 1);
			pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
			//Creating a bitmap surface
			surface = new Bitmap(paintingCanvas.Width, paintingCanvas.Height);
			g = Graphics.FromImage(surface);
			//Assigning the bitmap surface to the picturecanvas background
			paintingCanvas.BackgroundImage = surface;
			//Properties of graphics
			g.SmoothingMode = SmoothingMode.None;
			g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			g.InterpolationMode = InterpolationMode.High;
			//Properties of pen
			pen.MiterLimit = pen.Width * 1.25f;
			pen.LineJoin = LineJoin.Bevel;
			//Calling method Loadrecentlist to populate the recently opened files
			LoadRecentList();
			foreach (string item in MRUlist)
			{
				//populating menu
				ToolStripMenuItem fileRecent = new ToolStripMenuItem(item, null, RecentFile_click);
				//add the menu to "Recent" menu
				recentItems.DropDownItems.Add(fileRecent);
			}
		}

      
    }
}
