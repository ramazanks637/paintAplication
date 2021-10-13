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
{
	public partial class PremiumForm : Form
	{

		string _fileName;

		// Gerekli olan değişkenler tanımlandı
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

		public PremiumForm()
		{
			InitializeComponent();
			linePaintColor = Color.Black;
			pen = new Pen(linePaintColor, 1);
			pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

			generation(startX, startY);
			//Kalemin değerleri atandı
			pen.MiterLimit = pen.Width * 1.25f;
			pen.LineJoin = LineJoin.Bevel;
			//Geçmiş Dosyaların Yüklenmesi
			LoadRecentList();
			foreach (string item in MRUlist)
			{
				ToolStripMenuItem fileRecent = new ToolStripMenuItem(item, null, RecentFile_click);
				// Menüye "Geçmiş" Menüsünün eklenmesi
				recentItems.DropDownItems.Add(fileRecent);
			}
		}

		// Çizim ekranının verilen boyuta göre (yeniden veya baştan) oluşmasını sağlayan fonksiyon
		private void generation(int width, int height)
		{
			cizimAlani.Width = width;
			cizimAlani.Height = height;
			if (_fileName != null)
			{
				Size size;
				if (WindowState == FormWindowState.Maximized)
				{
					size = new Size(Screen.PrimaryScreen.Bounds.Width - 5, Screen.PrimaryScreen.Bounds.Height - 170);
				}
				else
				{
					size = new Size(startX, startY);
				}
				surface = resizeImage(new Bitmap(_fileName), size);
			}
			else
				surface = new Bitmap(width, height);

			g = Graphics.FromImage(surface);

			//Çizim canvasının arkplanının ayarlanması
			cizimAlani.BackgroundImage = surface;
			//Grafik objenin proplarının tanımlanması
			g.SmoothingMode = SmoothingMode.None;
			g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			g.InterpolationMode = InterpolationMode.High;

			/* START */
			if (undoStack.Count > 0)
			{
				// Geçmiş adımların ekrana yeniden çizilmesi
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
							Rectangle rectangle = new Rectangle();
							PaintEventArgs es = new PaintEventArgs(g, rectangle);
							pen = new Pen(s.LinePaintColor, s.Size);
							es.Graphics.DrawEllipse(pen, s.X1 - s.Y1 / 2, s.X2 - s.Y2 / 2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
						else if (s.Type == "eraser")
						{
							pen = new Pen(cizimAlani.BackColor, s.Size);
							g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
						else
						{
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
			//Dosya yükleme
			MRUlist.Clear();
			try
			{
				StreamReader listToRead = new StreamReader(System.Environment.CurrentDirectory + "\\Recent.txt");
				string line;
				while ((line = listToRead.ReadLine()) != null)
				{
					MRUlist.Enqueue(line);
				}
				listToRead.Close();
			}
			catch (Exception) { }
		}

		//Çizimin kaydedilmesini sağlayan fonksiyon
		public bool saveFile()
		{
			yuklemeCubugu.Value = 0;
			if (imageName == "")
			{
				SaveFileDialog sf = new SaveFileDialog();
				sf.Filter = "(*.jpg)|*.jpg";
				if (sf.ShowDialog() == DialogResult.OK)
				{
					imageName = sf.FileName;
					surface.Save(imageName);
					saveSuccess = true;
					yuklemeCubugu.Value = 100;

				}
			}
			else
			{
				surface.Save(imageName);
				saveSuccess = true;
				yuklemeCubugu.Value = 100;
			}
			return saveSuccess;
		}

		private void pictureBox28_Click(object sender, EventArgs e)
		{
			//Kullanıcınn panelden seçtiği rengin çizim rengi olarak ayarlanması
			PictureBox p = (PictureBox)sender;
			linePaintColor = p.BackColor;
		}

		private void kalemBtn_Click(object sender, EventArgs e)
		{
			//Kalem objesinin seçilmesi
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
			//silgi objesinin seçilmesi
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
			//Fırça objesinin seçilmesi
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

		
		
		private void cizgiBtn_Click(object sender, EventArgs e)
		{
			//Çizgi boyutunun seçilmesi
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
			//Özel renk seçilmesi
			ColorDialog colorWheel = new ColorDialog();
			if (colorWheel.ShowDialog() == DialogResult.OK)
			{
				linePaintColor = colorWheel.Color;
			}
		}



		//yeni tuşu kodları
		private void yeniTSM_Click(object sender, EventArgs e)
		{
			//Ekranın temizlenmesini sağlayan kod
			if (userEnteredCanvas)
			{
				var result = MessageBox.Show("Değişiklikleri kaydetmek istiyor musunuz ?", "Paint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				switch (result)
				{
					case DialogResult.Yes:
						if (saveFile())
						{
							surface.Dispose();
							surface = new Bitmap(cizimAlani.Width, cizimAlani.Height);
							g = Graphics.FromImage(surface);
							cizimAlani.BackgroundImage = surface;
							cizimAlani.Image = null;
							imageName = "";
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
					case DialogResult.No:
						surface.Dispose();
						surface = new Bitmap(cizimAlani.Width, cizimAlani.Height);
						g = Graphics.FromImage(surface);
						cizimAlani.BackgroundImage = surface;
						cizimAlani.Image = null;
						imageName = "";
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
					default:
						break;
				}
			}
		}

		// aç kodları buraya
		private void acTSM_Click(object sender, EventArgs e)
		{
			//Dosya açma fonksiyonu
			try
			{
				OpenFileDialog f = new OpenFileDialog();
				f.Filter = "(*.jpg)|*.jpg|(*.png)|*.png";

				if (f.ShowDialog() == DialogResult.OK)
				{
					_fileName = f.FileName;
					Size size;
					if (WindowState == FormWindowState.Maximized)
					{
						size = new Size(Screen.PrimaryScreen.Bounds.Width - 5, Screen.PrimaryScreen.Bounds.Height - 170);
					}
					else
					{
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

				Efektler efektler = new Efektler();

				Bitmap image = new Bitmap(cizimAlani.BackgroundImage);
				Bitmap gri = efektler.griyap(image);

				cizimAlani.Image = gri;

			}

			if (EfektlercomboBox.SelectedIndex == 1)
			{// sobel kenar bulma efekti yap

				Efektler efektler = new Efektler();
				Bitmap image = new Bitmap(cizimAlani.BackgroundImage);
				Bitmap gri = efektler.sobel(image);

				cizimAlani.Image = gri;
			}


		}

		//Mouse click olduğunda çalışacak fonksiyon
		private void cizimAlani_MouseDown(object sender, MouseEventArgs e)
		{
			userEnteredCanvas = true;
			if (e.Button == MouseButtons.Left)
			{
				moving = true;
				old = e.Location;

				//çizgi seçilmiş ise
				if (shapeSelected == 1)
				{
					//başlangıç noktası
					p1 = e.Location;
				}
				else if (shapeSelected == 2)
				{
					p1 = e.Location;
				}

				cizimAlani.Cursor = Cursors.Cross;
			}
		}

		//Mouse click olmuş ve hareket ettiriliyor ise çalışacak kod
		private void cizimAlani_MouseMove(object sender, MouseEventArgs e)
		{
			if (moving && old != null)
			{
				dotBool = true;
				// Kalem seçilmiş ise
				if (pencil)
				{
					pen = new Pen(linePaintColor, pSize);
					current = e.Location;
					// Geri alma ve ileri alma özelliğine kayıt edilmesi için çizilen noktaların Point objesi olarak oluşurulması
					p = new Points(old.X, old.Y, current.X, current.Y, linePaintColor, pSize, "pencil");
					curvePoints.Add(p);
					//Çizginin ekrana çizilmesi
					g.DrawLine(pen, old, current);
					old = current;
					cizimAlani.Invalidate();
				}
				// silgi seçilmiş ise
				else if (eraser)
				{
					pen = new Pen(cizimAlani.BackColor, 20);
					current = e.Location;
					// Geri alma ve ileri alma özelliğine kayıt edilmesi için çizilen noktaların Point objesi olarak oluşurulması
					p = new Points(old.X, old.Y, current.X, current.Y, linePaintColor, 20, "eraser");
					curvePoints.Add(p);
					//Çizginin ekrana çizilmesi
					g.DrawLine(pen, old, current);
					old = current;
					cizimAlani.Invalidate();
				}
				// fırça seçilmiş ise
				else if (brush)
				{
					pen = new Pen(linePaintColor, bSize);
					current = e.Location;
					// Geri alma ve ileri alma özelliğine kayıt edilmesi için çizilen noktaların Point objesi olarak oluşurulması
					p = new Points(old.X, old.Y, current.X, current.Y, linePaintColor, bSize, "brush");
					curvePoints.Add(p);
					//Çizginin ekrana çizilmesi
					g.DrawLine(pen, old, current);
					old = current;
					cizimAlani.Invalidate();
				}
			}
		}

		// Fare click tuşundan çıktığında çalışacak fonksiyon
		private void cizimAlani_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				moving = false;

				if (shapeSelected == 1)
				{
					p2 = e.Location;
					pen = new Pen(linePaintColor, 1);
					// Geri alma ve ileri alma özelliğine kayıt edilmesi için çizilen noktaların Point objesi olarak oluşurulması
					p = new Points(p1.X, p1.Y, p2.X, p2.Y, linePaintColor, 1, "line");
					curvePoints.Add(p);
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
					//nokta çizmek için ellipse çizme fonksiyonu kullanıldı
					es.Graphics.DrawEllipse(pen, e.X - 1 / 2, e.Y - 1 / 2, 1, 1);
					cizimAlani.Invalidate();
					// Geri alma ve ileri alma özelliğine kayıt edilmesi için çizilen noktaların Point objesi olarak oluşurulması
					p = new Points(e.X, e.Y, 1, 1, linePaintColor, pSize, "dot");
					curvePoints.Add(p);
				}

				dotBool = false;

				undoStack.Push(curvePoints);
				redoStack.Clear();
				curvePoints = new List<Points>();
				//cursor default ayarlama
				cizimAlani.Cursor = Cursors.Default;
			}
		}


		private void kaydetTSM_Click(object sender, EventArgs e)
		{
			saveFile();
		}
		private void farklikaydetTSM_Click(object sender, EventArgs e)
		{
			SaveFileDialog sf = new SaveFileDialog();
			sf.Filter = "(*.jpg)|*.jpg|(*.png)|*.png";
			if (sf.ShowDialog() == DialogResult.OK)
			{
				imageName = sf.FileName;
				surface.Save(imageName);
			}
		}

		//Geri al tuşuna bastığında çalışacak fonksiyon
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
					//Çizim alanının yeniden oluşturulması
					cizimAlani.BackgroundImage = surface;
					Stack<List<Points>> tempUndoStack = new Stack<List<Points>>();
					// Geri alma özelliğinin çalışma mantığı olarak
					//daha öncesinde Geri alma özelliğinin çalışması için oluşturulan noktaların depolandığı dizinin son elemanın silinmiş halini alıyoruz.
					redoStack.Push(undoStack.Pop());
					int undoCount = undoStack.Count;

					//Tüm noktaların geçici diziye kopyalanması
					for (int i = 0; i < undoCount; i++)
					{
						tempUndoStack.Push(undoStack.Pop());
					}

					int tempCount = tempUndoStack.Count;

					//Tüm noktaların yeniden çizilmesi
					for (int i = 1; i <= tempCount; i++)
					{
						List<Points> temp = tempUndoStack.Pop();
						foreach (var s in temp)
						{

							if (s.Type == "dot")
							{
								Rectangle rectangle = new Rectangle();
								PaintEventArgs es = new PaintEventArgs(g, rectangle);
								pen = new Pen(s.LinePaintColor, s.Size);
								es.Graphics.DrawEllipse(pen, s.X1 - s.Y1 / 2, s.X2 - s.Y2 / 2, s.Y1, s.Y2);
								cizimAlani.Invalidate();
							}
							else if (s.Type == "eraser")
							{
								pen = new Pen(cizimAlani.BackColor, s.Size);
								g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
								cizimAlani.Invalidate();
							}
							else
							{
								pen = new Pen(s.LinePaintColor, s.Size);
								g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
								cizimAlani.Invalidate();
							}
						}
						undoStack.Push(temp);
					}
				}
			}
		}

		//ileri al butonuna basıldığında çalışacak
		private void ileriButton_Click(object sender, EventArgs e)
		{
			Console.WriteLine("redo" + Convert.ToString(redoStack.Count));
			geriButton.Checked = false;

			lock (_undoRedoLocker)
			{
				//eğer ileri alınabilecek adım var ise
				if (redoStack.Count > 0)
				{
					List<Points> temp = redoStack.Peek();
					// noktaların yeniden oluşturulması
					foreach (var s in temp)
					{
						if (s.Type == "dot")
						{
							Rectangle rectangle = new Rectangle();
							PaintEventArgs es = new PaintEventArgs(g, rectangle);
							pen = new Pen(s.LinePaintColor, s.Size);
							es.Graphics.DrawEllipse(pen, s.X1 - s.Y1 / 2, s.X2 - s.Y2 / 2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
						else if (s.Type == "eraser")
						{
							pen = new Pen(cizimAlani.BackColor, s.Size);
							g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
						else
						{
							pen = new Pen(s.LinePaintColor, s.Size);
							g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
							cizimAlani.Invalidate();
						}
					}

					undoStack.Push(redoStack.Pop());

				}

			}
		}

		//Klavye kısayolları
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{

			if (e.Control && e.KeyCode == Keys.Z)
			{
				//Geri alma
				geriButton_Click(sender, new EventArgs());
			}
			else if (e.Control && e.KeyCode == Keys.X)
			{
				//İleri alma
				ileriButton_Click(sender, new EventArgs());
			}
			else if (e.Control && e.KeyCode == Keys.S)
			{
				//Kaydetme
				kaydetTSM_Click(sender, new EventArgs());
			}

		}

		private void hakkimizdaTSM_Click(object sender, EventArgs e)
		{
			MessageBox.Show("RK Yazılım\n\nRAMAZAN KÖSE\n");
		}

		//Geçmişte açılan dosyaların geçmişnin kaydedilmesi
		private void SaveRecentFile(string path)
		{
			recentItems.DropDownItems.Clear();
			LoadRecentList();
			if (!(MRUlist.Contains(path)))
				MRUlist.Enqueue(path);
			while (MRUlist.Count > MRUnumber)
			{
				MRUlist.Dequeue();
			}

			foreach (string item in MRUlist)
			{
				ToolStripMenuItem fileRecent = new ToolStripMenuItem(item, null, RecentFile_click);
				recentItems.DropDownItems.Add(fileRecent);
			}

			//Uygulamanın bulunduğu dizine "Recent.txt" isimli dosya oluşturulması
			StreamWriter stringToWrite = new StreamWriter(System.Environment.CurrentDirectory + "\\Recent.txt");
			foreach (string item in MRUlist)
			{
				stringToWrite.WriteLine(item);
			}
			stringToWrite.Flush();
			stringToWrite.Close();
		}

		private void RecentFile_click(object sender, EventArgs e)
		{
			//Kullanıcı geçmiş bir dosyayı açtığında çalışacak				
			surface = new Bitmap(sender.ToString());
			g = Graphics.FromImage(surface);
			cizimAlani.BackgroundImage = surface;
			cizimAlani.BackgroundImageLayout = ImageLayout.None;

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
			this.cizimAlani.SizeMode = PictureBoxSizeMode.AutoSize;
			this.menuStrip1.Items.OfType<ToolStripMenuItem>().ToList().ForEach(x =>
			{
				x.MouseHover += (obj, arg) => ((ToolStripDropDownItem)obj).ShowDropDown();
			});
		}

		private void Form1_FormClosing(System.Object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if ((surface != null))
			{
				surface.Dispose();
			}
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			// Form boyutu değiştiğinde çalışacak fonksiyon
			if (WindowState == FormWindowState.Maximized)
			{
				Console.WriteLine("Maximized");
				generation(Screen.PrimaryScreen.Bounds.Width - 5, Screen.PrimaryScreen.Bounds.Height - 170);
			}
			else
			{
				Console.WriteLine("Normal");
				generation(startX, startY);
			}
		}

		private void daireBtn_Click(object sender, EventArgs e)
		{
			//Daire butona bastığında çalışacak
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
			//mevcut resmin genişliğini alıyor
			int sourceWidth = imgToResize.Width;
			//mevcut resmin yüksekliğini alıyor
			int sourceHeight = imgToResize.Height;
			float nPercent = 0;
			float nPercentW = 0;
			float nPercentH = 0;
			//yeni resmin genişliğini alıyor
			nPercentW = ((float)size.Width / (float)sourceWidth);
			//yeni resmin yüksekliğini alıyor
			nPercentH = ((float)size.Height / (float)sourceHeight);
			if (nPercentH < nPercentW)
				nPercent = nPercentH;
			else
				nPercent = nPercentW;
			// yeni genişlik
			int destWidth = (int)(sourceWidth * nPercent);
			//yeni yükseklik 
			int destHeight = (int)(sourceHeight * nPercent);
			Bitmap b = new Bitmap(destWidth, destHeight);
			Graphics g = Graphics.FromImage((System.Drawing.Image)b);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			// Yeni resmin çizilmesi
			g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
			g.Dispose();
			return b;
		}

		private void cikisToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new Kullanici_islemleri().cikisYap();
		}

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
			string selected = numericUpDown1.Value.ToString();
			pSize = int.Parse(selected);
		}

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
			//Fırça boyutunun seçilmesi
			string selected = numericUpDown2.Value.ToString();
			bSize = int.Parse(selected);
		}
    }
}
