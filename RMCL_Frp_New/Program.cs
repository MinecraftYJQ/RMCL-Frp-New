using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMCL_Frp_New
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0) {
                if (args[0] == "-rmcl" && args[1] == "-start" && args[2]=="-ljmk")
                {
                    Application.Run(new Form1());
                }
                else
                {
                    MessageBox.Show("你在此处打开联机模块是没有任何用的awa！\n\n请使用RMCL打开联机模块以进行联机哦。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("你在此处打开联机模块是没有任何用的awa！\n\n请使用RMCL打开联机模块以进行联机哦。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
