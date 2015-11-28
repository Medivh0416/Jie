using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Jie
{
    public partial class Play : Form
    {
        public Play()
        {
            InitializeComponent();
        }

        
        int i = 100;
        int m = 1;
        private void onTime(object sender, EventArgs e)
        {
            Graphics d = pictureBox1.CreateGraphics();
            d.Clear(Color.White);
            //this.pictureBox1.Image = null;
            //this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\Image\\null.bmp");
            Thread.Sleep(80);
            string filePath = Application.StartupPath + "\\Image\\" + i.ToString() + m.ToString() + ".bmp";
            this.pictureBox1.Image = Image.FromFile(filePath);
            m++;
            //当m=3时，m变为0，然后重新开始；
            if (m == 3)
            {
                m = 1;
            }
        }

        private void Play_Load(object sender, EventArgs e)
        {
            
            timeS = DateTime.Now;
            textBox1.Enabled = false;
            button1.Enabled = false;
            start.Enabled = false;
        }
        //按键触发
        private void keyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (Char)Keys.Y)
            //{
            //    timer1.Stop();
            //    textBox1.Enabled = true;
            //}
        }
        DateTime timeS;
        DateTime timeE;
        TimeSpan time;

        //点击鼠标计时器停止
        private void click_(object sender, EventArgs e)
        {
            button1.Enabled = true;
            timer1.Stop();
            timer2.Stop();
            textBox1.Enabled = true;
            timeE = DateTime.Now;
        }
        string str;
        private void button1_Click(object sender, EventArgs e)
        {
            time = timeE - timeS;
            FileStream file = new FileStream("dialog.txt", FileMode.Append);//建立一个文件
            StreamWriter writer = new StreamWriter(file); //设置文件时可以写的
            str = " \r\n 第" + i + "组，用时" + time + "ms。 \r\n  不同处为" + textBox1.Text + "\r\n";
            writer.Write(str);
            writer.Close();
            file.Close();
            MessageBox.Show("Your illustration has already been saved, please click “start” to continue.");
            start.Enabled = true;
        }
        //start button 点击 进入下一组图片
        private void start_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            i++;
            if (i == 30)
            {
                MessageBox.Show("实验结束，感谢参加");
                this.Close();
            }
            button1.Enabled = false;
            textBox1.Enabled = false;
            timeS = DateTime.Now;
            timer1.Start();
            timer2.Start();
            start.Enabled = false;
            
        }
        //60秒计时，若没有操作输出没有发现不同
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            start.Enabled = true;
            FileStream file = new FileStream("dialog.txt", FileMode.Append);//建立一个文件
            StreamWriter writer = new StreamWriter(file); //设置文件时可以写的
            str = " \r\n 第" + i + "组 \r\n 未发现不同 \r\n";
            writer.Write(str);
            writer.Close();
            file.Close();
            MessageBox.Show("该组时间到，点击Start进入下一组");
        }
    }
}
