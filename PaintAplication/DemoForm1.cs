using PaintAplication.classes;
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

namespace PaintAplication
{/* TODO:: program generation fonksiyonu içerisinde undo gibi stack içinde bulunan tüm elementleri ekrana yazdırır. Böylece undo gibi connected olaylar sorun çıkarmaz */
	public partial class DemoForm1 : Form
	{

		string _fileName;

		// Initializing all the required variables
		private int shapeSelected = 0;
		private Color linePaintColor;
		int pSize = 1;
		int bSize = 5;

		int startX = 1190;
		int startY = 510;

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

		public DemoForm1()
		{
			InitializeComponent();
			//Initializing the value of color to be black
			linePaintColor = Color.Black;
			//Setting a pen with default color as linepaintcolor and size of 1
			pen = new Pen(linePaintColor, 1);
			pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
			//Creating a bitmap surface
			generation(startX, startY);
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
		private void generation(int width, int height){
			/* Screen.PrimaryScreen.Bounds.Width */
			/* Screen.PrimaryScreen.Bounds.Height-50 */
			cizimAlani.Width = width;
			cizimAlani.Height = height;
			if(_fileName != null){
				Size size;
				if (WindowState == FormWindowState.Maximized){
					size = new Size(Screen.PrimaryScreen.Bounds.Width-5,Screen.PrimaryScreen.Bounds.Height-170);
				}else {
					size = new Size(startX, startY);
				}
				surface = resizeImage(new Bitmap(_fileName), size);
			}
			else
				surface = new Bitmap(width , height);

			g = Graphics.FromImage(surface); 

			//Assigning the bitmap surface to the picturecanvas background
			cizimAlani.BackgroundImage = surface;
			//Properties of graphics
			g.SmoothingMode = SmoothingMode.None;
			g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			g.InterpolationMode = InterpolationMode.High;

			/* START */
			if (undoStack.Count > 0){
				Stack<List<Points>> tempUndoStack = new Stack<List<Points>>();
		
				for (int i = 0; i < undoStack.Count; i++)
				{
					tempUndoStack.Push(undoStack.ElementAt(i));
				}

				int tempCount = tempUndoStack.Count;
				for (int i = 1; i <= tempCount; i++)
				{
					List<Points> temp = tempUndoStack.Pop();
					foreach (var s in temp)
					{
						if (s.Type == "dot")
						{
							//If it is a dot draw a dot
							Rectangle rectangle = new Rectangle();
							PaintEventArgs es = new PaintEventArgs(g, rectangle);
							pen = new Pen(s.LinePaintColor, s.Size);
							es.Graphics.DrawEllipse(pen, s.X1 - s.Y1 / 2, s.X2 - s.Y2 / 2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
						else if (s.Type == "eraser")
						{
							//If the last action is eraser ,erase
							pen = new Pen(cizimAlani.BackColor, s.Size);
							g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
						else
						{
							//If the last action is drawline
							pen = new Pen(s.LinePaintColor, s.Size);
							g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
					}
				}
			}
			/* END */
		}

		private void LoadRecentList()
		{
			//try to load file. If file isn't found, do nothing
			MRUlist.Clear();
			try
			{
				//read file stream
				StreamReader listToRead = new StreamReader(System.Environment.CurrentDirectory + "\\Recent.txt");
				string line;
				//read each line until end of file
				while ((line = listToRead.ReadLine()) != null)
				{
					MRUlist.Enqueue(line); //insert to list
				}
				listToRead.Close(); //close the stream
			}
			catch (Exception) { }
		}

		//Method to save the work in PNG
		public bool saveFile()
		{
			yuklemeCubugu.Value = 0;
			//If the ImageName is empty it means it is the first save attempt
			if (imageName == "")
			{
				SaveFileDialog sf = new SaveFileDialog();
				sf.Filter = "(*.jpg)|*.jpg";
				//If the user hits the ok button
				if (sf.ShowDialog() == DialogResult.OK)
				{
					imageName = sf.FileName;
					//Saving the Bitmap
					surface.Save(imageName);
					saveSuccess = true;
					yuklemeCubugu.Value = 100;

				}
			}
			else
			{
				//If the saving is attempt is already done								
				surface.Save(imageName);
				saveSuccess = true;
				yuklemeCubugu.Value = 100;

			}
			//Returns true if success
			return saveSuccess;
		}

		private void pictureBox28_Click(object sender, EventArgs e)
		{
			//To pick the color user selects from the color panel
			PictureBox p = (PictureBox)sender;
			linePaintColor = p.BackColor;
		}

		private void kalemBtn_Click(object sender, EventArgs e)
		{
			//When the user clicks the pencil button, setting the other buttons unchecked
			eraserBtn.Checked = false;
			brushBtn.Checked = false;
			geriButton.Checked = false;
			ileriButton.Checked = false;
			cizgiBtn.Checked = false;
			toolStripButton1.Checked = false;
			pencil = true;
			brush = false;
			eraser = false;
			shapeSelected = 0;
		}

		private void silgiBtn_Click(object sender, EventArgs e)
		{
			//When the user clicks the Eraser button, setting the other buttons unchecked
			pencilBtn.Checked = false;
			brushBtn.Checked = false;
			geriButton.Checked = false;
			ileriButton.Checked = false;
			cizgiBtn.Checked = false;
			toolStripButton1.Checked = false;
			eraser = true;
			brush = false;
			pencil = false;
			shapeSelected = 0;
		}

		private void fircaBtn_Click(object sender, EventArgs e)
		{
			//When the user clicks the Brush button, setting the other buttons unchecked
			eraserBtn.Checked = false;
			pencilBtn.Checked = false;
			geriButton.Checked = false;
			ileriButton.Checked = false;
			cizgiBtn.Checked = false;
			toolStripButton1.Checked = false;
			brush = true;
			pencil = false;
			eraser = false;
			shapeSelected = 0;
		}

		private void kalemSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Method to set the pencil size based on the user selection from the combo box
			string selected = kalemSize.SelectedItem.ToString();
			pSize = int.Parse(selected);
		}
		private void fircaSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Method to set the Brush size based on the user selection from the combo box
			string selected = fircaSize.SelectedItem.ToString();
			bSize = int.Parse(selected);
		}
		private void cizgiBtn_Click(object sender, EventArgs e)
		{
			//When the user clicks the line button, setting the other buttons unchecked
			brushBtn.Checked = false;
			eraserBtn.Checked = false;
			pencilBtn.Checked = false;
			geriButton.Checked = false;
			ileriButton.Checked = false;
			toolStripButton1.Checked = false;
			shapeSelected = 1;
			brush = false;
			pencil = false;
			eraser = false;
		}


		private void RGBColors_Click(object sender, EventArgs e)
		{
			//When the user clicks on the edit colors, a color dialog opens from where the user can select different colors
			ColorDialog colorWheel = new ColorDialog();
			if (colorWheel.ShowDialog() == DialogResult.OK)
			{
				linePaintColor = colorWheel.Color;
			}
		}

		

		//yeni tuşu kodları aşağıya
		private void yeniTSM_Click(object sender, EventArgs e)
		{
			//Method to clean the screen when the user select New option from the file menu
			if (userEnteredCanvas)
			{
				//If the user has done somthing on the screen then shows a dialog to save the work
				var result = MessageBox.Show("Değişiklikleri kaydetmek istiyor musunuz ?", "Paint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				//Based the user selection
				switch (result)
				{
					case DialogResult.Yes:   // Yes button pressed
											 //If user selects Yes calls the save method to save the file
						if (saveFile())
						{
							//If the save went successfull
							//Disposing the Bitmap
							surface.Dispose();
							//Creating a new bitmap
							surface = new Bitmap(cizimAlani.Width, cizimAlani.Height);
							g = Graphics.FromImage(surface);
							cizimAlani.BackgroundImage = surface;
							cizimAlani.Image = null;
							imageName = "";
							//Clearing all the boolean variables to false
							brushBtn.Checked = false;
							eraserBtn.Checked = false;
							pencilBtn.Checked = false;
							geriButton.Checked = false;
							ileriButton.Checked = false;
							cizgiBtn.Checked = false;
							toolStripButton1.Checked = false;
							undoStack = new Stack<List<Points>>();
							redoStack = new Stack<List<Points>>();
						}
						break;
					case DialogResult.No:    // No button pressed
											 //If the user selects no , proceeds without saving the work

						surface.Dispose();
						//Creating a new bitmap
						surface = new Bitmap(cizimAlani.Width, cizimAlani.Height);
						g = Graphics.FromImage(surface);
						cizimAlani.BackgroundImage = surface;
						cizimAlani.Image = null;
						imageName = "";
						//Clearing all the boolean variables to false
						brushBtn.Checked = false;
						eraserBtn.Checked = false;
						pencilBtn.Checked = false;
						geriButton.Checked = false;
						ileriButton.Checked = false;
						cizgiBtn.Checked = false;
						toolStripButton1.Checked = false;
						undoStack = new Stack<List<Points>>();
						redoStack = new Stack<List<Points>>();
						break;
					default:                 // Neither Yes nor No pressed (just in case)						
						break;
				}
			}
		}

		// aç kodları buraya
		private void acTSM_Click(object sender, EventArgs e)
		{
			//Method to open a file
			try
			{
				OpenFileDialog f = new OpenFileDialog();
				f.Filter = "(*.jpg)|*.jpg|(*.png)|*.png";

				//If the user presses ok then the file opens
				if (f.ShowDialog() == DialogResult.OK)
				{
					//File = Image.FromFile(f.FileName);	
					_fileName = f.FileName;		
					Size size;
					if (WindowState == FormWindowState.Maximized){
						size = new Size(Screen.PrimaryScreen.Bounds.Width-5,Screen.PrimaryScreen.Bounds.Height-170);
					}else {
						size = new Size(startX, startY);
					}
					surface = resizeImage(new Bitmap(f.FileName), size);
					g = Graphics.FromImage(surface);
					cizimAlani.BackgroundImage = surface;
					cizimAlani.BackgroundImageLayout = ImageLayout.None;
					SaveRecentFile(f.FileName);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Bir hata oluştu", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void EfektlercomboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (EfektlercomboBox.SelectedIndex == 0)
			{// gri yap




			}

			if (EfektlercomboBox.SelectedIndex == 1)
			{// sobel efekti yap




			}


		}

		//Method when the users hits the mouse
		private void cizimAlani_MouseDown(object sender, MouseEventArgs e)
		{
			userEnteredCanvas = true;
			if (e.Button == MouseButtons.Left)
			{
				moving = true;
				//The points when the user hits mouse down
				old = e.Location;

				//If the shape selected is line
				if (shapeSelected == 1)
				{
					//Store the starting point in p1
					p1 = e.Location;
				}
				else if (shapeSelected == 2)
				{
					//Store the starting point in p1
					/*  TODO :: dairenin başlangıç noktaları */
					p1 = e.Location;
				}

				//Changing the cursor.
				cizimAlani.Cursor = Cursors.Cross;
			}
		}

		//Method for when the user moves the mouse
		private void cizimAlani_MouseMove(object sender, MouseEventArgs e)
		{
			if (moving && old != null)
			{
				dotBool = true;
				//Based upon the selected tool the if conditions follows
				if (pencil)
				{
					pen = new Pen(linePaintColor, pSize);
					//Setting the current location
					current = e.Location;
					//Storing the object of points in a list to track the points drawn by the user for undo and redo operations
					p = new Points(old.X, old.Y, current.X, current.Y, linePaintColor, pSize, "pencil");
					curvePoints.Add(p);
					//Drawing the line with the points
					g.DrawLine(pen, old, current);
					old = current;
					cizimAlani.Invalidate();
				}
				else if (eraser)
				{
					pen = new Pen(cizimAlani.BackColor, 20);
					//Setting the current location
					current = e.Location;
					//Storing the object of points in a list to track the points drawn by the user for undo and redo operations
					p = new Points(old.X, old.Y, current.X, current.Y, linePaintColor, 20, "eraser");
					curvePoints.Add(p);
					//Drawing the line with the points
					g.DrawLine(pen, old, current);
					old = current;
					cizimAlani.Invalidate();
				}
				else if (brush)
				{
					pen = new Pen(linePaintColor, bSize);
					//Setting the current location
					current = e.Location;
					//Storing the object of points in a list to track the points drawn by the user for undo and redo operations
					p = new Points(old.X, old.Y, current.X, current.Y, linePaintColor, bSize, "brush");
					curvePoints.Add(p);
					//Drawing the line with the points
					g.DrawLine(pen, old, current);
					old = current;
					cizimAlani.Invalidate();
				}
			}
		}

		private void cizimAlani_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				moving = false;

				if (shapeSelected == 1)
				{
					p2 = e.Location;
					pen = new Pen(linePaintColor, 1);
					//Storing the object of points in a list to track the points drawn by the user for undo and redo operations
					p = new Points(p1.X, p1.Y, p2.X, p2.Y, linePaintColor, 1, "line");
					curvePoints.Add(p);
					//Drawing the line with the points
					g.DrawLine(pen, p1, p2);
					cizimAlani.Invalidate();
				}
				else if (shapeSelected == 2)
				{
					/* TODO :: dairenin bitiş noktaları */
					pen = new Pen(linePaintColor, 1);
					p2 = e.Location;
					int cx = p1.X;
					int cy = p1.Y;

					int x = p2.X;
					int y = p2.Y;
					int sx = p2.X - cx;
					int sy = p2.Y - cy;
					/* p = new Points(cx, cy, sx, sy, linePaintColor, 1, "ellipse");
					curvePoints.Add(p); */
					g.DrawEllipse(pen, cx, cy, sx, sy);
					cizimAlani.Invalidate();
				}



				if (dotBool != true)
				{
					Rectangle rectangle = new Rectangle();
					PaintEventArgs es = new PaintEventArgs(g, rectangle);
					pen = new Pen(linePaintColor, pSize);
					//For drawing the dot used drawellipse
					es.Graphics.DrawEllipse(pen, e.X - 1 / 2, e.Y - 1 / 2, 1, 1);
					cizimAlani.Invalidate();
					//Storing the object of points in a list to track the points drawn by the user for undo and redo operations
					p = new Points(e.X, e.Y, 1, 1, linePaintColor, pSize, "dot");
					curvePoints.Add(p);
				}

				dotBool = false;

				//Pushing the curve into the undostack
				undoStack.Push(curvePoints);

				/*  */
				redoStack.Clear();
				/*  */
				//Clearing the list
				curvePoints = new List<Points>();
				//Changing te cursor to default
				cizimAlani.Cursor = Cursors.Default;
			}
		}


		private void kaydetTSM_Click(object sender, EventArgs e)
		{
			//When the user clicks the save 
			saveFile();
		}
		private void farklikaydetTSM_Click(object sender, EventArgs e)
		{
			//When the user presses the save as button
			SaveFileDialog sf = new SaveFileDialog();
			sf.Filter = "(*.jpg)|*.jpg|(*.png)|*.png";
			if (sf.ShowDialog() == DialogResult.OK)
			{
				imageName = sf.FileName;
				surface.Save(imageName);
			}
		}

		//Method for undo the last user action
		private void geriButton_Click(object sender, EventArgs e)
		{

			Console.WriteLine("undo" + Convert.ToString(undoStack.Count));
			ileriButton.Checked = false;

			lock (_undoRedoLocker)
			{
				if (undoStack.Count > 0)
				{
					surface = new Bitmap(cizimAlani.Width, cizimAlani.Height);
					g = Graphics.FromImage(surface);
					//Assigning the bitmap surface to the picturecanvas background
					cizimAlani.BackgroundImage = surface;
					Stack<List<Points>> tempUndoStack = new Stack<List<Points>>();
					//Poping the top item and pushing it to the redostack for redoing that operation
					redoStack.Push(undoStack.Pop());
					int undoCount = undoStack.Count;

					//Copying remaining items of the undostack into tempundostack
					for (int i = 0; i < undoCount; i++)
					{
						tempUndoStack.Push(undoStack.Pop());
					}

					int tempCount = tempUndoStack.Count;

					//Loping until the count of the stack
					for (int i = 1; i <= tempCount; i++)
					{
						//Poping the first element and placing it in the temp list
						List<Points> temp = tempUndoStack.Pop();
						//Drawing all the points in the list 
						foreach (var s in temp)
						{

							if (s.Type == "dot")
							{
								//If it is a dot draw a dot
								Rectangle rectangle = new Rectangle();
								PaintEventArgs es = new PaintEventArgs(g, rectangle);
								pen = new Pen(s.LinePaintColor, s.Size);
								es.Graphics.DrawEllipse(pen, s.X1 - s.Y1 / 2, s.X2 - s.Y2 / 2, s.Y1, s.Y2);
								cizimAlani.Invalidate();
							}
							else if (s.Type == "eraser")
							{
								//If the last action is eraser ,erase
								pen = new Pen(cizimAlani.BackColor, s.Size);
								g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
								cizimAlani.Invalidate();
							}
							else
							{
								//If the last action is drawline
								pen = new Pen(s.LinePaintColor, s.Size);
								g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
								cizimAlani.Invalidate();
							}
						}
						//Pushing back the items into undostack
						undoStack.Push(temp);
					}
				}
			}
		}

		//Method to perform redo when the user clicks redo
		
		private void ileriButton_Click(object sender, EventArgs e)
		{
			Console.WriteLine("redo" + Convert.ToString(redoStack.Count));
			geriButton.Checked = false;

			lock (_undoRedoLocker)
			{
				//If the redo stack is greater than 0
				if (redoStack.Count > 0)
				{
					List<Points> temp = redoStack.Peek();
					foreach (var s in temp)
					{
						if (s.Type == "dot")
						{
							//If it is a dot draw a dot
							Rectangle rectangle = new Rectangle();
							PaintEventArgs es = new PaintEventArgs(g, rectangle);
							pen = new Pen(s.LinePaintColor, s.Size);
							es.Graphics.DrawEllipse(pen, s.X1 - s.Y1 / 2, s.X2 - s.Y2 / 2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
						else if (s.Type == "eraser")
						{
							//If the last action is eraser ,erase
							pen = new Pen(cizimAlani.BackColor, s.Size);
							g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
						else
						{
							//If the last action is drawline
							pen = new Pen(s.LinePaintColor, s.Size);
							g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
					}

					undoStack.Push(redoStack.Pop());

				}

			}
		}

		//Method for keyboard shortcuts, When the user preses the keyboard shortcuts
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{

			if (e.Control && e.KeyCode == Keys.Z)
			{
				//For undo
				geriButton_Click(sender, new EventArgs());
			}
			else if (e.Control && e.KeyCode == Keys.X)
			{
				//For redo
				ileriButton_Click(sender, new EventArgs());
			}
			else if (e.Control && e.KeyCode == Keys.S)
			{
				//For save
				kaydetTSM_Click(sender, new EventArgs());
			}

		}

		//When the user clicks about in menustrip
		private void hakkimizdaTSM_Click(object sender, EventArgs e)
		{
			MessageBox.Show("RK Yazılım\n\nRAMAZAN KÖSE\n");
		}

		//Saving the recently opened file into a file
		private void SaveRecentFile(string path)
		{
			recentItems.DropDownItems.Clear();
			LoadRecentList(); //load list from file
			if (!(MRUlist.Contains(path))) //prevent duplication on recent list
				MRUlist.Enqueue(path); //insert given path into list
									   //keep list number not exceeded the given value
			while (MRUlist.Count > MRUnumber)
			{
				MRUlist.Dequeue();
			}

			foreach (string item in MRUlist)
			{
				//create new menu for each item in list
				ToolStripMenuItem fileRecent = new ToolStripMenuItem(item, null, RecentFile_click);
				//add the menu to "recent" menu
				recentItems.DropDownItems.Add(fileRecent);
			}
			//writing menu list to file
			//create file called "Recent.txt" located on app folder
			StreamWriter stringToWrite = new StreamWriter(System.Environment.CurrentDirectory + "\\Recent.txt");
			foreach (string item in MRUlist)
			{
				stringToWrite.WriteLine(item); //write list to stream
			}
			stringToWrite.Flush(); //write stream to file
			stringToWrite.Close(); //close the stream and reclaim memory
		}

		private void RecentFile_click(object sender, EventArgs e)
		{
			//When the user selects a file in the recently opened files, open that file						
			surface = new Bitmap(sender.ToString());
			g = Graphics.FromImage(surface);
			cizimAlani.BackgroundImage = surface;
			cizimAlani.BackgroundImageLayout = ImageLayout.None;

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			kalemSize.SelectedIndex = 0;
			fircaSize.SelectedIndex = 0;
			this.cizimAlani.SizeMode = PictureBoxSizeMode.AutoSize;
			//For automatically showing the menu dropdowns when the user hovers on the item
			this.menuStrip1.Items.OfType<ToolStripMenuItem>().ToList().ForEach(x =>
			{
				x.MouseHover += (obj, arg) => ((ToolStripDropDownItem)obj).ShowDropDown();
			});
		}

		private void Form1_FormClosing(System.Object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if ((surface != null))
			{
				//cleaning the bitmap object created
				surface.Dispose();
			}
		}

        private void Form1_Resize(object sender, EventArgs e)
        {
			if (WindowState == FormWindowState.Maximized){
				Console.WriteLine("Maximized");
				generation(Screen.PrimaryScreen.Bounds.Width-5,Screen.PrimaryScreen.Bounds.Height-170);
			}else {
				Console.WriteLine("Normal");
				generation(startX, startY);
			}
        }

		private void daireBtn_Click(object sender, EventArgs e)
		{
			//When the user clicks the line button, setting the other buttons unchecked	
			brushBtn.Checked = false;
			eraserBtn.Checked = false;
			pencilBtn.Checked = false;
			geriButton.Checked = false;
			ileriButton.Checked = false;
			cizgiBtn.Checked = false;
			shapeSelected = 2;
			brush = false;
			pencil = false;
			eraser = false;
		}

		private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
			Application.Exit();
        }

      private static Bitmap resizeImage(System.Drawing.Image imgToResize, Size size)  
	{  
			//Get the image current width  
			int sourceWidth = imgToResize.Width;  
			//Get the image current height  
			int sourceHeight = imgToResize.Height;  
			float nPercent = 0;  
			float nPercentW = 0;  
			float nPercentH = 0;  
			//Calulate  width with new desired size  
			nPercentW = ((float)size.Width / (float)sourceWidth);  
			//Calculate height with new desired size  
			nPercentH = ((float)size.Height / (float)sourceHeight);  
			if (nPercentH < nPercentW)  
				nPercent = nPercentH;  
			else  
			nPercent = nPercentW;  
			//New Width  
			int destWidth = (int)(sourceWidth * nPercent);  
			//New Height  
			int destHeight = (int)(sourceHeight * nPercent);  
			Bitmap b = new Bitmap(destWidth, destHeight);  
			Graphics g = Graphics.FromImage((System.Drawing.Image)b);  
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;  
			// Draw image with new width and height  
			g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);  
			g.Dispose();  
			return b;  
			/* (System.Drawing.Image) */
		}

        private void lisansAktifleştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
			LisansAktiflestirForm lisansAktiflestirForm = new LisansAktiflestirForm();
			lisansAktiflestirForm.ShowDialog();
		}

        private void cikisToolStripMenuItem_Click(object sender, EventArgs e)
        {
			new Kullanici_islemleri().cikisYap();
        }

        private void satinAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
			SatinAl satinAl = new SatinAl();
            satinAl.ShowDialog();
        }
    }
}
