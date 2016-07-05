using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PP_s_EmptyDirsKiller
{
    public partial class DirsKiller : Form
    {
        public DirsKiller()
        {
            InitializeComponent();
        }

        void AddMonText(string msg, bool isNeeedClearText = false)
        {
            if (isNeeedClearText)
                txtMon.Text = null;
            txtMon.AppendText(string.Format("{0} {1}\r\n", DateTime.Now.ToString("HH:mm:ss"), msg));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text;
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                AddMonText("不存在玩个杰宝！");
                return;
            }
            CheckAndDestory(path);
        }


        void CheckAndDestory(string path)
        {
            if (!Directory.Exists(path))
                return;
            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            if (dirs != null && dirs.Length > 0)
            {
                foreach (string dir in dirs)
                    CheckAndDestory(dir);
            }
            dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            if ((dirs == null || dirs.Length == 0) && (files == null || files.Length == 0))
            {
                Directory.Delete(path);
                AddMonText("已清除：" + path);
            }
        }
    }
}
