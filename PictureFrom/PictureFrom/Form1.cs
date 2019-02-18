using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureFrom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string filePath;
        private string foldPath;
        private string picturePath = null;
        private List<string> imagePathList = new List<string>();
        private int index =0;

        //private int current_file_size = 0;
        private int next_page_index = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.foldPath = folderBrowserDialog1.SelectedPath;
                    
                    showPicture();
                    listbox1.Items.Clear();
                }
                else if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBox.Show("取消显示图片列表");
                }
            }
            catch (Exception msg)
            {
                throw msg;
            }
        }

        private void showPicture()
        {
            
           
            DirectoryInfo dic = new DirectoryInfo(foldPath);
            DirectoryInfo[] foldInfo = dic.GetDirectories();
            if (index>=foldInfo.Length)
            {
                index = foldInfo.Length - 1;
                MessageBox.Show("Final Picture");
            }
            filePath = foldInfo[index].FullName;
            label1.Text = index.ToString()+"\\"+foldInfo.Length;
            DirectoryInfo dir = new DirectoryInfo(filePath);
            FileInfo[] fileInfo = dir.GetFiles("*.jpg");
            for (int i = 0; i < fileInfo.Length; i++)
            {
                picturePath = fileInfo[i].FullName;
                imagePathList.Add(picturePath);
                
                this.imageList1.Images.Add(Image.FromFile(picturePath));
            }
            //this.imageList1.ColorDepth = ColorDepth.Depth32Bit;
            //imageList1.ImageSize = new Size(200, 200);
            //this.listView1.Items.Clear();
            this.listView1.LargeImageList = this.imageList1;
           // this.listView1.View = View.LargeIcon;
            Loadpicture();
        }


            //current_file_size = fileInfo.Length;

            
        private void Loadpicture()
            {
            int totle = imageList1.Images.Count / 50;
            label2.Text = next_page_index.ToString() + "\\" + totle.ToString();
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();
            int start = next_page_index * 50;
            int end = next_page_index * 50 + 50;
            if (end > imageList1.Images.Count)
            {
                end = imageList1.Images.Count;
            }

            //if (start == end)
            //{
            //    return;
            //}

            for (int i = start;i< end;i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = i;
                this.listView1.Items.Add(lvi);
            }
            this.listView1.EndUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            next_page_index = 0;
            this.listView1.Items.Clear();
            this.imageList1.Images.Clear();
            Console.WriteLine(foldPath.Length);
            if (textBox1.Text.Trim() == string.Empty)
            {
                index++;
            }
            else if (IsNumber(textBox1.Text.Trim()) == false)
            {
                MessageBox.Show("文本框里只能为数字");
            }         
            else
            {
                index = Convert.ToInt32(textBox1.Text.Trim());
            }
            showPicture();


        }

        private bool IsNumber(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (!File.Exists("‪C:\\Users\\admin\\Desktop\\PicturePath.txt"))
            {
                FileStream fs1 = new FileStream("C:\\Users\\admin\\Desktop\\PicturePath.txt", FileMode.Append, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(filePath.ToString());//开始写入值
                sw.Close();
                fs1.Close();
                listbox1.Items.Add(filePath.ToString());
            }
            else
            {
                FileStream fs = new FileStream("C:\\Users\\admin\\Desktop\\PicturePath.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(filePath.ToString());//开始写入值
                sr.Close();
                fs.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            next_page_index++;
            Loadpicture();
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Q)
            {
                button2_Click(sender, e);
            }
            if ((Keys)e.KeyChar == Keys.E)
            {
                button4_Click(sender, e);
            }
            if ((Keys)e.KeyChar == Keys.S)
                button3_Click(sender, e);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyValue== Keys.Q)
            {
                button2_Click(sender, e);
            }
            if ((Keys)e.KeyValue == Keys.E)
            {
                button4_Click(sender, e);
            }
            if ((Keys)e.KeyValue == Keys.S)
                button3_Click(sender, e);

        }
    }
}

