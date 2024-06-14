using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;

namespace MainSender
{
    public partial class mainForm : Skin_Metro
    {
        private int curSelect = 0;
        static public Form[] selectFrm = null;


        public mainForm()
        {
            InitializeComponent();

            #region 初始化控件缩放

            x = Width;
            y = Height;
            setTag(this);

            #endregion

            //窗体初始化
            selectFrm = new Form[6];

            /*            this.skinPanel2.Controls.Clear();
                        selectFrm[0] = new SerialDebug();

                        Control_Add(selectFrm[0]);*/

            
                skinPanel2.Controls.Clear();    //移除所有控件
               
                    selectFrm[0] = new SerialDebug();
                    selectFrm[0].TopLevel = false;
                    selectFrm[0].Dock = DockStyle.Fill;
                
         
                selectFrm[0].Show();

                this.skinPanel2.Controls.Add(selectFrm[0]);
            
          
        }


        private void Control_Add(Form form)
        {
            skinPanel2.Controls.Clear();    //移除所有控件
            form.TopLevel = false;      //设置为非顶级窗体
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; //设置窗体为非边框样式
            form.Dock = System.Windows.Forms.DockStyle.Fill;                  //设置样式是否填充整个panel
            skinPanel2.Controls.Add(form);        //添加窗体
            form.Show();                      //窗体运行
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (curSelect != 0)
            {
                skinPanel2.Controls.Clear();    //移除所有控件
                if (selectFrm[0] == null || selectFrm[0].IsDisposed)
                {
                    selectFrm[0] = new SerialDebug();
                    selectFrm[0].TopLevel = false;
                    selectFrm[0].Dock = DockStyle.Fill;
                }
                else
                {
                    selectFrm[0].Activate();
                }

                selectFrm[curSelect].Hide();
                selectFrm[0].Show();

                this.skinPanel2.Controls.Add(selectFrm[0]);
            }
            curSelect = 0;
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {

            if (curSelect != 1)
            {
                skinPanel2.Controls.Clear();    //移除所有控件
                if (selectFrm[1] == null || selectFrm[1].IsDisposed)
                {
                    selectFrm[1] = new SysForm1();
                    selectFrm[1].TopLevel = false;
                    selectFrm[1].Dock = DockStyle.Fill;
                }
                else
                {
                    selectFrm[1].Activate();
                }

                selectFrm[curSelect].Hide();
                selectFrm[1].Show();

                this.skinPanel2.Controls.Add(selectFrm[1]);
            }
            curSelect = 1;

        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            if (curSelect != 2)
            {
                skinPanel2.Controls.Clear();    //移除所有控件
                if (selectFrm[2] == null || selectFrm[2].IsDisposed)
                {
                    selectFrm[2] = new UpdateForm();
                    selectFrm[2].TopLevel = false;
                    selectFrm[2].Dock = DockStyle.Fill;
                }
                else
                {
                    selectFrm[2].Activate();
                }

                selectFrm[curSelect].Hide();
                selectFrm[2].Show();

                this.skinPanel2.Controls.Add(selectFrm[2]);
            }
            curSelect = 2;
        }

        private void skinButton4_Click(object sender, EventArgs e)
        {

        }

        private void skinButton5_Click(object sender, EventArgs e)
        {

        }

        private void skinButton6_Click(object sender, EventArgs e)
        {
            if (curSelect != 5)
            {
                skinPanel2.Controls.Clear();    //移除所有控件
                if (selectFrm[5] == null || selectFrm[5].IsDisposed)
                {
                    selectFrm[5] = new ControlForm();
                    selectFrm[5].TopLevel = false;
                    selectFrm[5].Dock = DockStyle.Fill;
                }
                else
                {
                    selectFrm[5].Activate();
                }

                selectFrm[curSelect].Hide();
                selectFrm[5].Show();

                this.skinPanel2.Controls.Add(selectFrm[5]);
            }
            curSelect = 5;

        }









        #region 控件大小随窗体大小等比例缩放

        private readonly float x; //定义当前窗体的宽度
        private readonly float y; //定义当前窗体的高度

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0) setTag(con);
            }
        }

        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    var mytag = con.Tag.ToString().Split(';');
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(Convert.ToSingle(mytag[0]) * newx); //宽度
                    con.Height = Convert.ToInt32(Convert.ToSingle(mytag[1]) * newy); //高度
                    con.Left = Convert.ToInt32(Convert.ToSingle(mytag[2]) * newx); //左边距
                    con.Top = Convert.ToInt32(Convert.ToSingle(mytag[3]) * newy); //顶边距
                    var currentSize = Convert.ToSingle(mytag[4]) * newy; //字体大小                   
                    if (currentSize > 0) con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    con.Focus();
                    if (con.Controls.Count > 0) setControls(newx, newy, con);
                }
        }


        /// <summary>
        /// 重置窗体布局
        /// </summary>
        private void ReWinformLayout()
        {
            var newx = Width / x;
            var newy = Height / y;
            setControls(newx, newy, this);

        }

        #endregion

        private void mainForm_Resize(object sender, EventArgs e)
        {
            ReWinformLayout();
        }

        
    }
}
