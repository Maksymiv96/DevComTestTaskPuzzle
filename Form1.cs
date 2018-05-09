using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevComTestTaskPuzzle
{




    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool isDown;
        private void _MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                isDown = true;
                ((Control)sender).BringToFront();
                //((Control)sender).Parent = panel1;


            }
            if (e.Button == MouseButtons.Right)
            {
                if (sender is PictureBox)
                {
                    //using ( ModifPictureBox mdf  = (ModifPictureBox)sender);
                    Bitmap bmp = new Bitmap(((ModifPictureBox)sender).Image);
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipXY);

                    ((ModifPictureBox)sender).Image = bmp;
                    ((ModifPictureBox)sender).Rotate++;
                    if (((ModifPictureBox)sender).Rotate == 4) ((ModifPictureBox)sender).Rotate = 0;
                    //MessageBox.Show(((ModifPictureBox)sender).Index.ToString() + " " + ((ModifPictureBox)sender).Rotate.ToString());
                    //MessageBox.Show("try");

                }
            }
        }

        private void _MouseMove(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            //Control c = (PictureBox)sender;
            if (isDown)
            {

                c.Location = c.Parent.PointToClient(Control.MousePosition);
                // if (c.Location.X < c.Parent.Location.X) c.Location = c.Parent.PointToClient(;
            }


        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control c = sender as Control;
                Point p = c.Parent.PointToClient(Control.MousePosition);
                //c.Location = p;
                if (p.X >= panel1.Width) p.X = panel1.Width - 1;
                if (p.X <= 0) p.X = 1;
                if (p.Y >= panel1.Height) p.Y = panel1.Height - 1;
                if (p.Y <= 0) p.Y = 1;
                c.Location = new Point((int)(p.X / sizeoflittlebox) * sizeoflittlebox, (int)(p.Y / sizeoflittlebox) * sizeoflittlebox);
                // if (c.Location.X>panel1.Size.Width||c.Location.Y>panel1.Size.Height)c.Location = new Point
                isDown = false;
                ((ModifPictureBox)sender).CurrentInd = (p.X / sizeoflittlebox * Convert.ToInt32(Math.Sqrt(levelbox)) + p.Y / sizeoflittlebox);
                

            }
            if (levelsuccess()) MessageBox.Show(" Old Kinderhook");
        }

        int sizeoflittlebox;

        public bool levelsuccess()
        {

            foreach (ModifPictureBox mdpb in pb)
            {
                if (mdpb.Index != mdpb.CurrentInd || mdpb.Rotate != 0) return false;

            }
            return true;
        }

        Puzzles puzz;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((fd.OpenFile()) != null)
                    {
                        {

                            pictureBox1.Image = new Bitmap(Image.FromFile(fd.FileName), pictureBox1.Width, pictureBox1.Height);

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            // fd.OpenFile();

        }
        ModifPictureBox[] pb;
        int levelbox;
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            if (panel1.Controls.Count > 0) panel1.Controls.Clear();
            levelbox = (int)Math.Pow(Convert.ToInt32(textBox1.Text), 2);
            sizeoflittlebox = Convert.ToInt32(pictureBox1.Width / Math.Sqrt(levelbox));

            panel1.Size = pictureBox1.Size;
            //panel1.Size = new Size(sizeoflittlebox, pictureBox1.Size.Height);
            pb = new ModifPictureBox[levelbox];
            puzz = new Puzzles(new Bitmap(pictureBox1.Image), levelbox);


            for (int i = 0; i < levelbox; i++)
            {

                pb[i] = new ModifPictureBox();
                //pb[i].Name = "pictureBox" + i.ToString();
                pb[i].Index = i;
                pb[i].Image = puzz.elements[i].puzzleselem;
                //pb[i].Location = new Point(Convert.ToInt32(sizeoflittlebox / Math.Sqrt(levelbox)) + i,Convert.ToInt32(sizeoflittlebox/Math.Sqrt(levelbox))+i);
                pb[i].Location = new Point(new Random().Next(sizeoflittlebox / 4, panel1.Height - sizeoflittlebox), new Random().Next(sizeoflittlebox / 4, panel1.Width - sizeoflittlebox));
                pb[i].Size = new Size(sizeoflittlebox, sizeoflittlebox);//puzz.elements[i].puzzleselem.Width, puzz.elements[i].puzzleselem.Height);
                pb[i].SizeMode = PictureBoxSizeMode.Zoom;
                pb[i].BorderStyle = BorderStyle.Fixed3D;
                pb[i].MouseDown += _MouseDown;
                pb[i].MouseMove += _MouseMove;
                pb[i].MouseUp += _MouseUp;




            }
            shuffle(ref pb);
            for (int i = 0; i < levelbox; i++)
                panel1.Controls.Add(pb[i]);


        }
        private void shuffle(ref ModifPictureBox[] array)
        {
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n);
                n--;
                ModifPictureBox temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            if (panel1.Controls.Count > 0) panel1.Controls.Clear();
            levelbox = (int)Math.Pow(Convert.ToInt32(textBox1.Text), 2);
            sizeoflittlebox = Convert.ToInt32(pictureBox1.Width / Math.Sqrt(levelbox));

            panel1.Size = pictureBox1.Size;
            //panel1.Size = new Size(sizeoflittlebox, pictureBox1.Size.Height);
            pb = new ModifPictureBox[levelbox];
            puzz = new Puzzles(new Bitmap(pictureBox1.Image), levelbox);


            for (int i = 0; i < levelbox; i++)
            {

                pb[i] = new ModifPictureBox();
                //pb[i].Name = "pictureBox" + i.ToString();
                pb[i].Index = i;
                pb[i].Image = puzz.elements[i].puzzleselem;
                pb[i].Size = new Size(sizeoflittlebox, sizeoflittlebox);//puzz.elements[i].puzzleselem.Width, puzz.elements[i].puzzleselem.Height);
                pb[i].SizeMode = PictureBoxSizeMode.Zoom;
                pb[i].BorderStyle = BorderStyle.Fixed3D;

            }
            shuffle(ref pb);
            

            ai(ref pb);
            for (int i = 0; i < levelbox; i++)
                 panel1.Controls.Add(pb[i]);


        }

        enum Side
        {
            top,
            down,
            left,
            right

        }

        void ai(ref ModifPictureBox[] pb)
        {
           // int[,] matrix = new int[levelbox, levelbox];
            List<puzzleforai> massofpuzzleai = new List<puzzleforai>();
            for (int i = 0; i < pb.Length; i++)
            {
                for (int side = 0; side < 4; side++)
                {
                    int mini = i, minj = 0, min = diff(((Bitmap)pb[i].Image), (Bitmap)pb[0].Image, ((Side)side).ToString());
                    for (int j = 0; j < pb.Length; j++)
                    {
                        if (i == j) continue;

                        int buf = diff(((Bitmap)pb[i].Image), (Bitmap)pb[j].Image, ((Side)side).ToString());
                        if (min > buf)
                        {
                            minj = j; mini = i; min = buf;
                        }

                    }

                    massofpuzzleai.Add(new puzzleforai(((Side)side).ToString(), mini, minj, min));

                }
            }
            massofpuzzleai.Sort(delegate (puzzleforai sp1, puzzleforai sp2) { return sp1.diff.CompareTo(sp2.diff); });
            foreach (puzzleforai buf in massofpuzzleai)
            {
                textBox3.Text += buf.Shovv().ToString() + Environment.NewLine;
            }
            int[] finalmatr = new int[(levelbox)];

            for (int i = 0; i < (levelbox); i++)
                finalmatr[i] = -1;

            List<int> availelem = new List<int>();
            availelem.Add(massofpuzzleai[0].a);
            finalmatr[0] = massofpuzzleai[0].a;
            textBox2.Text += string.Join(";", availelem) + Environment.NewLine;
            for (int i = 0; i < massofpuzzleai.Count; i++)
            {

                if (inttobool(availelem.IndexOf(massofpuzzleai[i].a)) ^ inttobool(availelem.IndexOf(massofpuzzleai[i].b)))
                {
                    //availelem.Add(inttobool(availelem.IndexOf(massofpuzzleai[i].b)) ? massofpuzzleai[i].a : massofpuzzleai[i].b);
                    if (inttobool(availelem.IndexOf(massofpuzzleai[i].b)))
                    {
                        string bufside = "";
                        if (massofpuzzleai[i].side == "down") bufside = "top";
                        if (massofpuzzleai[i].side == "top") bufside = "down";
                        if (massofpuzzleai[i].side == "right") bufside = "left";
                        if (massofpuzzleai[i].side == "left") bufside = "right";

                        if (addingAnearB(ref finalmatr, massofpuzzleai[i].b, massofpuzzleai[i].a, bufside))
                            availelem.Add(massofpuzzleai[i].a);
                        else continue;
                    }
                    else
                    {
                        if (addingAnearB(ref finalmatr, massofpuzzleai[i].a, massofpuzzleai[i].b, massofpuzzleai[i].side))
                            availelem.Add(massofpuzzleai[i].b);
                        else continue;
                    }
                        


                    textBox2.Text += string.Join(";", availelem) + Environment.NewLine;
                    //finalmatr.i
                    if (availelem.Count >= levelbox) break;
                    i = 0;

                }
                
            }
            textBox2.Text = "Probably this position" + Environment.NewLine + string.Join(";", finalmatr);
            //MessageBox.Show(string.Join(";", finalmatr));

            for (int i = 0;i<levelbox;i++)
            {
                //finalmatr;
                int level = (int)(Math.Sqrt(levelbox));
                //MessageBox.Show((finalmatr[i] % level * panel1.Height / Math.Sqrt(levelbox)) + " " + (int)(finalmatr[i] / level * panel1.Height / Math.Sqrt(levelbox)));
                if (finalmatr[i]>=0)pb[finalmatr[i]].Location = new  Point((int)(i % level * panel1.Height/ Math.Sqrt(levelbox)) , (int)(i/level * panel1.Height / Math.Sqrt(levelbox)) );
               // panel1.Controls.Add(pb[finalmatr[i]]);
            }
            

        }

        public int[] Shift(int[] myArray, int shift)
        {
            Array.Reverse(myArray, 0, shift);
            Array.Reverse(myArray, shift, myArray.Length - shift);
            Array.Reverse(myArray);
            return myArray;
        }


        public bool addingAnearB(ref int[] array, int num1, int num2, string side)
        {
            int globala = levelbox;
            int[] backup = (from elem in array select elem).ToArray();
            //array = (from elem in backup select elem).ToArray();
            for (int i = 0; i < levelbox; i++)
                if (array[i] == num1)
                {
                    globala = i;
                    break;
                }
            //topdovvnleftright
            switch (side)
            {
                case "top": //top
                    {
                        try
                        {
                            if ((int)globala / (int)Math.Sqrt(levelbox) == 0)
                                array = Shift(array, array.Length - (int)Math.Sqrt(levelbox));
                            if (array[Array.IndexOf(array, num1) - (int)Math.Sqrt(levelbox)] == -1)
                            {
                                array[Array.IndexOf(array, num1) - (int)Math.Sqrt(levelbox)] = num2;
                                return true;
                            }
                            else
                            {
                                array = (from elem in backup select elem).ToArray();
                                return false;
                            }
                        }
                        catch { return false; }

                    }
                case "down": //dovvn
                    {
                        try
                        {
                            if (globala / (int)Math.Sqrt(levelbox) == (int)Math.Sqrt(levelbox) - 1) array = Shift(array, (int)Math.Sqrt(levelbox));
                            if (array[globala + (int)Math.Sqrt(levelbox)] == -1)
                            {
                                array[globala + (int)Math.Sqrt(levelbox)] = num2;
                                return true;
                            }
                            else
                            {
                                array = (from elem in backup select elem).ToArray();
                                return false;
                            }
                        }
                        catch { return false; }
                    }
                case "left": //left
                    {
                        try
                        {
                            if (globala % (int)Math.Sqrt(levelbox) == 0)
                                array = Shift(array, array.Length - 1);
                            if (array[Array.IndexOf(array, num1) - 1] == -1)
                            {
                                array[Array.IndexOf(array, num1) - 1] = num2;
                                return true;
                            }
                            else
                            {
                                array = (from elem in backup select elem).ToArray();
                                return false;
                            }
                        }
                        catch { return false; }
                    } 
                case "right": //right
                    {

                        try
                        {
                            if (globala == array.Length - 1) array[0] = num2;
                            if (globala % Math.Sqrt(levelbox) == Math.Sqrt(levelbox) - 1) array = Shift(array, 1);
                            if (array[globala + 1] == -1)
                            {
                                array[globala + 1] = num2;
                                return true;
                            }
                            else
                            {
                                array = (from elem in backup select elem).ToArray();
                                return false;
                            }

                        }
                        catch { return false; }
                    }
            }

            return false;
        }

        public int diffincolor(Color col1, Color col2)
    {
        return Math.Abs(col1.R - col2.R + col1.G - col2.G + col1.B - col2.B);
    }
        public bool inttobool(int num)
        {
            if (num >= 0) return true;
            else return false;
        }

    public int diff(Bitmap img1, Bitmap img2, string side)
    {
        int sum = 0;
        for (int i = 0; i < img1.Height; i++)
        {
                switch (side)
                {
                    case "top":
                        {
                            sum += (diffincolor(img1.GetPixel(i, 0), img2.GetPixel(i, img2.Height - 1)));
                            break;
                        }
                    case "down":
                        {
                            sum += (diffincolor(img1.GetPixel(i, img1.Height - 1), img2.GetPixel(i, 0)));
                            break;
                        }
                    case "left":
                        {
                            sum += (diffincolor(img1.GetPixel(0, i), img2.GetPixel(img2.Width - 1, i)));
                            break;
                        }
                    case "right":
                        {
                            sum += (diffincolor(img1.GetPixel(img1.Width - 1, i), img2.GetPixel(0, i)));
                            break;
                        }
                    default: break;
                }


            //MessageBox.Show((((Bitmap)pb[i].Image).GetPixel(i,j).R - ((Bitmap)pb[i+1].Image).GetPixel(i, j).R).ToString());
                        
                //MessageBox.Show(img1.GetPixel(img1.Width - 1, i).ToString() + img2.GetPixel(0, i).ToString());

        }
        return sum;
    }

        List<int> DFS(int[,] matr, int start)
        {
            
            List<int> buf = new List<int>() ;
            List<int> LIFO = new List<int>();
            LIFO.Add(start);
            List<int> UsedVertices = new List<int>();
            for (int i = 0; i < Math.Sqrt(matr.Length); i++)
                if (matr[start, i] != 0)
                {
                    buf.Add(start);
                    
                    break;
                }
            if (buf.Count < 1) return buf;
            
            UsedVertices.Add(start);
            while (LIFO.Count != 0)
            {



                for (int j = 0; j < Convert.ToInt16(Math.Sqrt(matr.Length)); j++)
                {
                    int i = LIFO[LIFO.Count - 1];
                    if (matr[i, j] != 0)
                    {
                        if (UsedVertices.IndexOf(j) < 0)
                        {
                            LIFO.Add(j);
                            UsedVertices.Add(j);
                            buf.Add (j);
                            //i = LIFO[LIFO.Count - 1];
                            j = 0;
                            continue;
                        }

                        matr[i, j] = 0;
                        matr[j, i] = 0;
                    }
                }
                LIFO.RemoveAt(LIFO.Count - 1);

            }
            //Console.WriteLine("Ocinka shliahu = " + (ocinka(buf, end+1)).ToString());
            return buf;
        }

        List<int> BFS(int[,] matr, int start)
        {

            List<int> buf = new List<int>() ;
            List<int> FIFO = new List<int>();
            FIFO.Add(start);
            List<int> UsedVertices = new List<int>();
            for (int i = 0; i < Math.Sqrt(matr.Length); i++)
                if (matr[start, i] != 0)
                {
                    buf.Add(start);

                    break;
                }
            if (buf.Count < 1) return buf;
            UsedVertices.Add(start);
            while (FIFO.Count != 0)
            {
                int i = FIFO[0];
                FIFO.RemoveAt(0);

                for (int j = 0; j < Convert.ToInt16(Math.Sqrt(matr.Length)); j++)
                {
                    if (matr[i, j] != 0)
                    {
                        if (UsedVertices.IndexOf(j) < 0)
                        {
                            FIFO.Add(j);
                            UsedVertices.Add(j);
                            buf.Add (j);
                        }

                        matr[i, j] = 0;
                        matr[j, i] = 0;
                    }
                }

            }
            // Console.WriteLine("Ocinka shliahu = " + (ocinka(buf, end+1)).ToString());
            return buf;
        }

        string availableelem(int [,] matr)
        {
            string result = "";
            for (int i = 0; i < Math.Sqrt( matr.Length); i++)
                for (int j = 0; j < Math.Sqrt(matr.Length); j++)
                {
                    if (matr[i, j] >= 0) result += matr[i,j].ToString();
                    continue;
                }
           // MessageBox.Show(result);
            return result;
        }




    }

    public class puzzleforai
    {
        public string side = "";
        public int a, b;
        public int diff;
        public puzzleforai(string side, int a, int b, int diff)
        {
            this.side = side;
            this.a = a;
            this.b = b;
            this.diff = diff;
        }
        public string Shovv()
        {
            return (side + " " + a + " " + b + " " + diff);
        }
       
    }




    public class PartofPuzzle
    {
        public int position;
        public int rotate = 0;
        public Bitmap puzzleselem;
        public PartofPuzzle(int PositionOfFragment, Bitmap PuzzlesElement)
        {
            position = PositionOfFragment;
            puzzleselem = PuzzlesElement;
        }

        public bool isMatch(int currentPoss)
        {
            return (position == currentPoss && rotate == 0);
        }

    }

    public class Puzzles
    {
        Bitmap puzzle;
        int levelbox = 0;
        public List<PartofPuzzle> elements = new List<PartofPuzzle>();
        public Bitmap CutImage(Bitmap src, Rectangle rect)
        {

            Bitmap bmp = new Bitmap(src.Width / (int)Math.Sqrt(levelbox), src.Height / (int)Math.Sqrt(levelbox));

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel);
            return bmp;
        }
        private void CutPazzle()
        {
            int count = 0;
            Image temp = puzzle;
            //Bitmap src = new Bitmap(temp, puzzle.Width, puzzle.Height);
            for (int i = 0; i < (int)Math.Sqrt(levelbox); i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(levelbox); j++)

                {
                    Rectangle rect = new Rectangle(new Point(i * puzzle.Width / (int)Math.Sqrt(levelbox), j * puzzle.Height / (int)Math.Sqrt(levelbox)), new Size(puzzle.Width / (int)Math.Sqrt(levelbox), puzzle.Height / (int)Math.Sqrt(levelbox)));
                    Bitmap CuttedImage = CutImage(puzzle, rect);


                    elements.Add(new PartofPuzzle(count, CuttedImage));
                    //CuttedImage.Save(count + ".jpg");
                    count++;
                }
            }
            // this.elements.Add();
        }
        public Puzzles(Bitmap Image, int level)
        {
            puzzle = Image;
            levelbox = level;
            CutPazzle();

        }
    }
    

    class ModifPictureBox : PictureBox
    {
        int index, rotate = 0, currentind = -1;

        public int Index { get => index; set => index = value; }
        public int Rotate { get => rotate; set => rotate = value; }
        public int CurrentInd { get => currentind; set => currentind = value; }

        public bool isMatch()
        {
            return (index == currentind && rotate == 0);
        }
    }


    


}
