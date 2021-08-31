
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
		
		// Gerekli tüm değişkenlerin başlatılması
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
			
			//Siyah olacak rengin değerini başlatmak
			linePaintColor = Color.Black;
			//LinePaintColor ve 1 boyutunda varsayılan rengiyle bir kalem belirleme
			pen = new Pen(linePaintColor, 1);
			pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
			//Bir bitmap yüzey oluşturma
			surface = new Bitmap(paintingCanvas.Width, paintingCanvas.Height);
			g = Graphics.FromImage(surface);
			//Bitmap yüzeyini PictureCanvas arka plana atama
			paintingCanvas.BackgroundImage = surface;
			//Grafiklerin Özellikleri
			g.SmoothingMode = SmoothingMode.None;
			g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			g.InterpolationMode = InterpolationMode.High;
			//Properties of pen
			pen.MiterLimit = pen.Width * 1.25f;
			pen.LineJoin = LineJoin.Bevel;
			//Calling metodu LoadRecentList yakın zamanda açılan dosyaları doldurmak için
			LoadRecentList();
			foreach (string item in MRUlist)
			{
				//doldurma menüsü
				ToolStripMenuItem fileRecent = new ToolStripMenuItem(item, null, RecentFile_click);
				//Menüyü "En Yeni" menüsüne ekleyin
				recentItems.DropDownItems.Add(fileRecent);
			}
		}

		private void LoadRecentList()
		{
			//Dosyayı yüklemeye çalışın. Dosya bulunmazsa, hiçbir şey yapmayın
			MRUlist.Clear();
			try
			{
				//Dosya akışını oku
				StreamReader listToRead = new StreamReader(System.Environment.CurrentDirectory + "\\Recent.txt");
				string line;
				//Dosyanın sonuna kadar her satırı okuyun
				while ((line = listToRead.ReadLine()) != null)
				{
					MRUlist.Enqueue(line); //Listeye ekle
				}
				listToRead.Close(); //akışı kapat
			}
			catch (Exception) { }
		}

		//PNG'deki işi kaydetme yöntemi
		public bool saveFile()
		{
			progressBar.Value = 0;
			//ImagegeMame boşsa, ilk tasarruf girişimi olduğu anlamına gelir.
			if (imageName == "")
			{
				SaveFileDialog sf = new SaveFileDialog();
				sf.Filter = "PNG(*.PNG)|*.png";
				sf.Filter = "JPG(*.JPG)|*.jpg";
				//Kullanıcı Tamam düğmesine vurursa
				if (sf.ShowDialog() == DialogResult.OK)
				{
					imageName = sf.FileName;
					//Bitmap kaydetme
					surface.Save(imageName);
					saveSuccess = true;
					progressBar.Value = 100;

				}
			}
			else
			{
				//Tasarruf denemesi zaten yapılırsa								
				surface.Save(imageName);
				saveSuccess = true;
				progressBar.Value = 100;

			}
			//Başarı varsa doğru döndürür
			return saveSuccess;
		}

		private void pictureBox28_Click(object sender, EventArgs e)
		{
			//Renkli kullanıcıyı seçmek için renk panelinden seçer
			PictureBox p = (PictureBox)sender;
			linePaintColor = p.BackColor;
		}

		private void pencilBtn_Click(object sender, EventArgs e)
		{
			//Kullanıcı kalem düğmesini tıkladığında, diğer düğmeleri işaretlenmemiş
			eraserBtn.Checked = false;
			brushBtn.Checked = false;
			undoButton.Checked = false;
			RedoButton.Checked = false;
			lineBtn.Checked = false;
			pencil = true;
			brush = false;
			eraser = false;
			shapeSelected = 0;
		}

		private void eraserBtn_Click(object sender, EventArgs e)
		{
			//Kullanıcı silgi düğmesini tıkladığında, diğer düğmeleri işaretlenmemiş
			pencilBtn.Checked = false;
			brushBtn.Checked = false;
			undoButton.Checked = false;
			RedoButton.Checked = false;
			lineBtn.Checked = false;
			eraser = true;
			brush = false;
			pencil = false;
			shapeSelected = 0;
		}

		private void brushBtn_Click(object sender, EventArgs e)
		{
			//Kullanıcı fırça düğmesini tıkladığında, diğer düğmeleri işaretlenmemiş
			eraserBtn.Checked = false;
			pencilBtn.Checked = false;
			undoButton.Checked = false;
			RedoButton.Checked = false;
			lineBtn.Checked = false;
			brush = true;
			pencil = false;
			eraser = false;
			shapeSelected = 0;
		}

		private void pencilSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Combo kutusundan kullanıcı seçimine dayanarak kalem boyutunu ayarlama yöntemi
			string selected = pencilSize.SelectedItem.ToString();
			pSize = int.Parse(selected);
		}

		private void lineBtn_Click(object sender, EventArgs e)
		{
			//Kullanıcı satır düğmesini tıkladığında, diğer düğmeleri işaretlenmemiş
			brushBtn.Checked = false;
			eraserBtn.Checked = false;
			pencilBtn.Checked = false;
			undoButton.Checked = false;
			RedoButton.Checked = false;
			shapeSelected = 1;
			brush = false;
			pencil = false;
			eraser = false;
		}

		private void RGBColors_Click(object sender, EventArgs e)
		{
			//Kullanıcı Düzenleme renklerini tıkladığında, kullanıcının farklı renkleri seçebileceği bir renk iletişim kutusu açılır.
			ColorDialog colorWheel = new ColorDialog();
			if (colorWheel.ShowDialog() == DialogResult.OK)
			{
				linePaintColor = colorWheel.Color;
			}
		}

		private void brushSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Açılan kutunun kullanıcı seçimine göre fırça boyutunu ayarlama yöntemi
			string selected = brushSize.SelectedItem.ToString();
			bSize = int.Parse(selected);
		}

		private void newMSI_Click(object sender, EventArgs e)
		{
			//Kullanıcı Dosya menüsünden Yeni Seçeneği Seçtiğinde Ekranı Temizleme Yöntemi
			if (userEnteredCanvas)
			{
				//Kullanıcı ekranda bir şey yapmışsa, çalışmayı kaydetmek için bir iletişim kutusu gösterir.
				var result = MessageBox.Show("Do you want to save changes", "Paint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				//Kullanıcı seçimine dayanarak
				switch (result)
				{
					case DialogResult.Yes:   // Evet düğmesine basıldı
											 //Kullanıcı Evet'i seçerse, dosyayı kaydetmek için Kaydet yöntemini çağırır.
						if (saveFile())
						{
							//Kaydet başarılı oldu ise
							//Bitmap'in atılması
							surface.Dispose();
							//Yeni bir bitmap oluşturma
							surface = new Bitmap(paintingCanvas.Width, paintingCanvas.Height);
							g = Graphics.FromImage(surface);
							paintingCanvas.BackgroundImage = surface;
							paintingCanvas.Image = null;
							imageName = "";
							//Boolean değişkenlerini yanlış olarak temizlemek
							brushBtn.Checked = false;
							eraserBtn.Checked = false;
							pencilBtn.Checked = false;
							undoButton.Checked = false;
							RedoButton.Checked = false;
							lineBtn.Checked = false;
							undoStack = new Stack<List<Points>>();
							redoStack = new Stack<List<Points>>();
						}
						break;
					case DialogResult.No:    // Düğmeye basılmaz
											 //Kullanıcı Hayır'ı seçerse, işi kaydetmeden ilerleyin

						surface.Dispose();
						//Yeni bir bitmap oluşturma
						surface = new Bitmap(paintingCanvas.Width, paintingCanvas.Height);
						g = Graphics.FromImage(surface);
						paintingCanvas.BackgroundImage = surface;
						paintingCanvas.Image = null;
						imageName = "";
						//Boolean değişkenlerini yanlış olarak temizlemek
						brushBtn.Checked = false;
						eraserBtn.Checked = false;
						pencilBtn.Checked = false;
						undoButton.Checked = false;
						RedoButton.Checked = false;
						lineBtn.Checked = false;
						undoStack = new Stack<List<Points>>();
						redoStack = new Stack<List<Points>>();
						break;
					default:                 // Ne evet ne de basılmaz (sadece durumunda)						
						break;
				}
			}
		}

		private void openMSI_Click(object sender, EventArgs e)
		{
			//Bir dosyayı açma yöntemi
			try
			{
				OpenFileDialog f = new OpenFileDialog();
				f.Filter = "PNG(*.PNG)|*.png";

				//Kullanıcı Tamam'a basarsa, dosya açılır.
				if (f.ShowDialog() == DialogResult.OK)
				{
					//File = Image.FromFile(f.FileName);					
					surface = new Bitmap(f.FileName);
					g = Graphics.FromImage(surface);
					paintingCanvas.BackgroundImage = surface;
					paintingCanvas.BackgroundImageLayout = ImageLayout.None;
					SaveRecentFile(f.FileName);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//Kullanıcılar fareye vururken yöntem
		private void paintingCanvas_MouseDown(object sender, MouseEventArgs e)
		{
			userEnteredCanvas = true;
			if (e.Button == MouseButtons.Left)
			{
				moving = true;
				//Kullanıcı fareyi isabet ettiğinde puan
				old = e.Location;

				//Seçilen şekil satır ise
				if (shapeSelected == 1)
				{
					//p1 başlayan noktayı Mağaza
					p1 = e.Location;
				}

				//İmleci değiştirme.
				paintingCanvas.Cursor = Cursors.Cross;
			}
		}

		//Kullanıcı fareyi hareket ettirdiğinde yöntem
		private void paintingCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (moving && old != null)
			{
				dotBool = true;
				//Seçilen aracın üzerine göre şartlar
				if (pencil)
				{
					pen = new Pen(linePaintColor, pSize);
					//Geçerli konumu ayarlama
					current = e.Location;
					//Kullanıcı tarafından çizilen noktaları izlemek için bir listedeki nokta nesnesini saklamak
					p = new Points(old.X, old.Y, current.X, current.Y, linePaintColor, pSize, "pencil");
					curvePoints.Add(p);
					//Çizgiyi noktalarla çizmek
					g.DrawLine(pen, old, current);
					old = current;
					paintingCanvas.Invalidate();
				}
				else if (eraser)
				{
					pen = new Pen(paintingCanvas.BackColor, 20);
					//Geçerli konumu ayarlama
					current = e.Location;
					//Kullanıcı tarafından çizilen noktaları izlemek için bir listedeki nokta nesnesini saklamak
					p = new Points(old.X, old.Y, current.X, current.Y, linePaintColor, 20, "eraser");
					curvePoints.Add(p);
					//Çizgiyi noktalarla çizmek
					g.DrawLine(pen, old, current);
					old = current;
					paintingCanvas.Invalidate();
				}
				else if (brush)
				{
					pen = new Pen(linePaintColor, bSize);
					//Geçerli konumu ayarlama
					current = e.Location;
					//Kullanıcı tarafından çizilen noktaları izlemek için bir listedeki nokta nesnesini saklamak
					p = new Points(old.X, old.Y, current.X, current.Y, linePaintColor, bSize, "brush");
					curvePoints.Add(p);
					//Çizgiyi noktalarla çizmek
					g.DrawLine(pen, old, current);
					old = current;
					paintingCanvas.Invalidate();
				}
			}
		}

		private void paintingCanvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				moving = false;

				if (shapeSelected == 1)
				{
					p2 = e.Location;
					pen = new Pen(linePaintColor, 1);
					//Kullanıcı tarafından çizilen noktaları izlemek için bir listedeki nokta nesnesini saklamak
					p = new Points(p1.X, p1.Y, p2.X, p2.Y, linePaintColor, 1, "line");
					curvePoints.Add(p);
					//Çizgiyi noktalarla çizmek
					g.DrawLine(pen, p1, p2);
					paintingCanvas.Invalidate();

				}


				if (dotBool != true)
				{
					Rectangle rectangle = new Rectangle();
					PaintEventArgs es = new PaintEventArgs(g, rectangle);
					pen = new Pen(linePaintColor, pSize);
					//Kullanılan nokta çizimi çekmek için
					es.Graphics.DrawEllipse(pen, e.X - 1 / 2, e.Y - 1 / 2, 1, 1);
					paintingCanvas.Invalidate();
					//Kullanıcı tarafından çizilen noktaları izlemek için bir listedeki nokta nesnesini saklamak
					p = new Points(e.X, e.Y, 1, 1, linePaintColor, pSize, "dot");
					curvePoints.Add(p);
				}

				dotBool = false;

				//Eğriyi undodack içine itmek
				undoStack.Push(curvePoints);
				//Listeyi temizleme
				curvePoints = new List<Points>();
				//İmleci varsayılana değiştirme
				paintingCanvas.Cursor = Cursors.Default;
			}
		}

		private void saveMSI_Click(object sender, EventArgs e)
		{
			//Kullanıcı kaydetmeyi tıkladığında 
			saveFile();
		}

		private void saveAsMSI_Click(object sender, EventArgs e)
		{
			//Kullanıcı Farklı Kaydet düğmesine bastığında
			SaveFileDialog sf = new SaveFileDialog();
			sf.Filter = "PNG(*.PNG)|*.png";
			if (sf.ShowDialog() == DialogResult.OK)
			{
				imageName = sf.FileName;
				surface.Save(imageName);
			}
		}

		//Son kullanıcı eylemini geri alma yöntemi
		private void undoButton_Click(object sender, EventArgs e)
		{
			RedoButton.Checked = false;

			lock (_undoRedoLocker)
			{
				if (undoStack.Count > 0)
				{
					surface = new Bitmap(paintingCanvas.Width, paintingCanvas.Height);
					g = Graphics.FromImage(surface);
					//Bitmap yüzeyini PictureCanvas arka plana atama
					paintingCanvas.BackgroundImage = surface;
					Stack<List<Points>> tempUndoStack = new Stack<List<Points>>();
					//Üst öğeyi doldurmak ve bu işlemi tekrarlamak için RedoStack'e iterek
					redoStack.Push(undoStack.Pop());
					int undoCount = undoStack.Count;

					//UNDOSTACK'ın kalan öğelerinin tempundostack'e kopyalanması
					for (int i = 0; i < undoCount; i++)
					{
						tempUndoStack.Push(undoStack.Pop());
					}

					int tempCount = tempUndoStack.Count;

					//Yığının sayısına kadar loping
					for (int i = 1; i <= tempCount; i++)
					{
						//İlk elemanı doldurmak ve sıcaklık listesine yerleştirmek
						List<Points> temp = tempUndoStack.Pop();
						//Listedeki tüm noktaları çizin 
						foreach (var s in temp)
						{

							if (s.Type == "dot")
							{
								//Bir nokta ise bir nokta çizin
								Rectangle rectangle = new Rectangle();
								PaintEventArgs es = new PaintEventArgs(g, rectangle);
								pen = new Pen(s.LinePaintColor, s.Size);
								es.Graphics.DrawEllipse(pen, s.X1 - s.Y1 / 2, s.X2 - s.Y2 / 2, s.Y1, s.Y2);
								paintingCanvas.Invalidate();
							}
							else if (s.Type == "eraser")
							{
								//Son işlem silgi ise, silin
								pen = new Pen(paintingCanvas.BackColor, s.Size);
								g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
								paintingCanvas.Invalidate();
							}
							else
							{
								//Son eylem çizilme ise
								pen = new Pen(s.LinePaintColor, s.Size);
								g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
								paintingCanvas.Invalidate();
							}
						}
						//Eşyaları geri yüklüye itmek
						undoStack.Push(temp);
					}
				}
			}
		}

		//Kullanıcı yinelenmeyi tıkladığında yineleme yapmak için yöntem
		private void RedoButton_Click(object sender, EventArgs e)
		{
			undoButton.Checked = false;

			lock (_undoRedoLocker)
			{
				//Redo yığını 0'dan büyükse
				if (redoStack.Count > 0)
				{
					List<Points> temp = redoStack.Peek();
					foreach (var s in temp)
					{
						if (s.Type == "dot")
						{
							//Bir nokta ise bir nokta çizin
							Rectangle rectangle = new Rectangle();
							PaintEventArgs es = new PaintEventArgs(g, rectangle);
							pen = new Pen(s.LinePaintColor, s.Size);
							es.Graphics.DrawEllipse(pen, s.X1 - s.Y1 / 2, s.X2 - s.Y2 / 2, s.Y1, s.Y2);
							paintingCanvas.Invalidate();
						}
						else if (s.Type == "eraser")
						{
							//Son işlem silgi ise, silin
							pen = new Pen(paintingCanvas.BackColor, s.Size);
							g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
							paintingCanvas.Invalidate();
						}
						else
						{
							//Son eylem çizilme ise
							pen = new Pen(s.LinePaintColor, s.Size);
							g.DrawLine(pen, s.X1, s.X2, s.Y1, s.Y2);
							paintingCanvas.Invalidate();
						}
					}

					undoStack.Push(redoStack.Pop());

				}

			}
		}

		//Klavye kısayolları için yöntem, kullanıcı klavye kısayollarını kutlarken
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{

			if (e.Control && e.KeyCode == Keys.Z)
			{
				//Geri almak için
				undoButton_Click(sender, new EventArgs());
			}
			else if (e.Control && e.KeyCode == Keys.X)
			{
				//Redo için
				RedoButton_Click(sender, new EventArgs());
			}
			else if (e.Control && e.KeyCode == Keys.S)
			{
				//Kaydetmek için
				saveMSI_Click(sender, new EventArgs());
			}

		}

		//Kullanıcı menüstinde tıkladığında
		private void aboutMSI_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Our Group Name is Fortnite\nGroup Members :\nSandeep Alla\nGayathri Sanikommu\nSurya Vamsi Maddukuri!");
		}

		//Son zamanlarda açılan dosyayı bir dosyaya kaydetme
		private void SaveRecentFile(string path)
		{
			recentItems.DropDownItems.Clear();
			LoadRecentList(); //Dosyadan Yük Listesi
			if (!(MRUlist.Contains(path))) //Son listede çoğaltmayı önleme
				MRUlist.Enqueue(path); //Verilen yolu listeye yerleştirin
									   //Liste numarasını belirli bir değeri aşmamış tutun
			while (MRUlist.Count > MRUnumber)
			{
				MRUlist.Dequeue();
			}

			foreach (string item in MRUlist)
			{
				//Listedeki her öğe için yeni menü oluşturun
				ToolStripMenuItem fileRecent = new ToolStripMenuItem(item, null, RecentFile_click);
				//Menüyü "En Yeni" menüsüne ekleyin
				recentItems.DropDownItems.Add(fileRecent);
			}
			//Dosyaya menü listesi yazma
			//Uygulama klasöründe bulunan "en son.txt" adlı dosyayı oluşturun.
			StreamWriter stringToWrite = new StreamWriter(System.Environment.CurrentDirectory + "\\Recent.txt");
			foreach (string item in MRUlist)
			{
				stringToWrite.WriteLine(item); //Stream için liste yaz
			}
			stringToWrite.Flush(); //Dosyaya akışı yaz
			stringToWrite.Close(); //Akışı kapatın ve hafızayı geri kazanın
		}

		private void RecentFile_click(object sender, EventArgs e)
		{
			//Kullanıcı yakın zamanda açılan dosyalarda bir dosyayı seçtiğinde, bu dosyayı açın.					
			surface = new Bitmap(sender.ToString());
			g = Graphics.FromImage(surface);
			paintingCanvas.BackgroundImage = surface;
			paintingCanvas.BackgroundImageLayout = ImageLayout.None;

		}

		private void Form1_Load(object sender, EventArgs e)
		{

			this.paintingCanvas.SizeMode = PictureBoxSizeMode.AutoSize;
			//Kullanıcı öğeye ulaştığında, menü açılır menüsünü otomatik olarak göstermek için
			this.menuStrip1.Items.OfType<ToolStripMenuItem>().ToList().ForEach(x =>
			{
				x.MouseHover += (obj, arg) => ((ToolStripDropDownItem)obj).ShowDropDown();
			});
		}

		private void Form1_FormClosing(System.Object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if ((surface != null))
			{
				//Oluşturulan bitmap nesnesinin temizlenmesi
				surface.Dispose();
			}
		}
    }
}
