using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RMCL_Frp_New
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void cmd(string programPath)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.StandardInput.WriteLine("@echo off&cls&" + programPath + "&exit");
            p.StandardInput.AutoFlush = true;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox4.Enabled)
                {
                    cmd("RMCL\\RMCL_WAN_Online\\frps\\frps.exe");
                    textBox4.Enabled = false;
                    textBox1.Enabled = false;
                    textBox5.Enabled = false;
                    textBox6.Enabled = false;
                    textBox7.Enabled = false;
                    button1.Enabled = false;
                    button2.Text = "结束";
                    MessageBox.Show($"本机联机码:{jm_s(textBox4.Text)}\n请将本联机码输入至联机客户端内进行内网穿透！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show($"创建成功！\n对接本服务器地址:{textBox4.Text}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cmd("taskkill /f /im frps.exe");
                    textBox4.Enabled = true;
                    textBox1.Enabled = true;
                    textBox5.Enabled = true;
                    textBox6.Enabled = true;
                    textBox7.Enabled = true;
                    button1.Enabled = true;
                    button2.Text = "创建";
                }
                
            }
        }
        private string jm_s(string ip)
        {
            string temps="";
            string inp = ip+".";
            var ran = new Random();
            int temp = 0, bs = ran.Next(100, 999);
            for (int i = 0; i < inp.Length; i++)
            {
                if (inp[i] == '.')
                {
                    i++;
                    try
                    {
                        string a = inp.Substring(temp, i - temp - 1);
                        Console.WriteLine(a);
                        temps += (int.Parse(a) * bs).ToString() + "D";
                        temp = i;
                    }
                    catch (Exception) { }
                }
            }
            temps += bs.ToString();
            return temps;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"本机联机码:{jm_s(textBox4.Text)}\n请将本联机码输入至联机客户端内进行内网穿透！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void start()
        {
            string texts = $"serverAddr = \"{textBox1.Text}\"\r\nserverPort = 7000\r\n\r\n[[proxies]]\r\nname = \"rmcl-ol\"\r\ntype = \"tcp\"\r\nlocalIP = \"{textBox5.Text}\"\r\nlocalPort = {textBox6.Text}\r\nremotePort = {textBox7.Text}\r\n";
            Console.WriteLine($"地址:{textBox1.Text} 端口:7000 服务器总地址:{textBox1.Text}:7000");
            File.WriteAllText("RMCL\\RMCL_WAN_Online\\frpc\\frpc.ini", texts);
            string programPath = $"\"{AppDomain.CurrentDomain.BaseDirectory}RMCL\\RMCL_WAN_Online\\frpc\\frpc.exe\" -c \"{AppDomain.CurrentDomain.BaseDirectory}RMCL\\Online\\frpc\\frpc.ini\"";

            cmd(programPath);
            button1.Text = "结束";
            textBox4.Enabled = false;
            textBox1.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            button2.Enabled = false;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                if (textBox1.Enabled)
                {
                    start();
                    MessageBox.Show("联机IP地址:" + jjm_c(textBox1.Text) + ":" + textBox7.Text + "\n请在房客的Minecraft客户端中的多人游戏使用直接连接输入此IP进入房间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cmd("taskkill /f /im frpc.exe");
                    textBox4.Enabled = true;
                    textBox1.Enabled = true;
                    textBox5.Enabled = true;
                    textBox6.Enabled = true;
                    textBox7.Enabled = true;
                    button2.Enabled = true;
                    button1.Text = "创建";
                }
            }
            else
            {
                MessageBox.Show("不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string jjm_c(string my)
        {
            textBox4.Text = "";
            string inp = my,outp="";
            Console.WriteLine(my);
            int temp = 0, bs = int.Parse(my.Substring(my.Length - 3, 3));
            for (int i = 0; i < inp.Length; i++)
            {
                if (inp[i] == 'D')
                {
                    i++;
                    try
                    {
                        string a = inp.Substring(temp, i - temp - 1);
                        Console.WriteLine(a);
                        outp += (int.Parse(a) / bs).ToString() + ".";
                        temp = i;
                    }
                    catch (Exception) { }
                }
            }
            outp = outp.Substring(0, outp.Length - 1);
            return outp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                MessageBox.Show("联机IP地址:" + jjm_c(textBox1.Text) + ":" + textBox7.Text + "\n请在房客的Minecraft客户端中的多人游戏使用直接连接输入此IP进入房间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
