namespace MainSender
{
    partial class SerialDebug
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialDebug));
            this.comboBox_Serial = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.status_timer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus_Port = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_recestatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_sendstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatus_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.comboBox_BandRate = new System.Windows.Forms.ComboBox();
            this.comboBox_Check = new System.Windows.Forms.ComboBox();
            this.comboBox_Data = new System.Windows.Forms.ComboBox();
            this.comboBox_Stop = new System.Windows.Forms.ComboBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.richTextBox_Send = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.richTextBox_Receive = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_ClcSendData = new System.Windows.Forms.Button();
            this.button_ClcReceData = new System.Windows.Forms.Button();
            this.button_SendData = new System.Windows.Forms.Button();
            this.textBox_selectTime = new System.Windows.Forms.TextBox();
            this.checkBox_SendData = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_Filepath = new System.Windows.Forms.TextBox();
            this.checkBox_Sendfile = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_ReceData = new System.Windows.Forms.ToolStripMenuItem();
            this.发送数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerSend = new System.Windows.Forms.Timer(this.components);
            this.processDatatimer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Serial
            // 
            this.comboBox_Serial.Font = new System.Drawing.Font("宋体", 13F);
            this.comboBox_Serial.FormattingEnabled = true;
            this.comboBox_Serial.Location = new System.Drawing.Point(121, 38);
            this.comboBox_Serial.Name = "comboBox_Serial";
            this.comboBox_Serial.Size = new System.Drawing.Size(121, 30);
            this.comboBox_Serial.TabIndex = 0;
            this.comboBox_Serial.SelectedIndexChanged += new System.EventHandler(this.Select_Ports_Change);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13F);
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "串口号:";
            // 
            // status_timer
            // 
            this.status_timer.Enabled = true;
            this.status_timer.Interval = 1000;
            this.status_timer.Tick += new System.EventHandler(this.Update_StatusTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 13F);
            this.label2.Location = new System.Drawing.Point(11, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 13F);
            this.label3.Location = new System.Drawing.Point(11, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "校验位:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 13F);
            this.label4.Location = new System.Drawing.Point(11, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 22);
            this.label4.TabIndex = 4;
            this.label4.Text = "数据位:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 13F);
            this.label5.Location = new System.Drawing.Point(11, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 22);
            this.label5.TabIndex = 5;
            this.label5.Text = "停止位:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus_Port,
            this.toolStripStatus_recestatus,
            this.toolStripStatus_sendstatus,
            this.toolStripStatus_Time});
            this.statusStrip1.Location = new System.Drawing.Point(0, 631);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1003, 26);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatus_Port
            // 
            this.toolStripStatus_Port.Name = "toolStripStatus_Port";
            this.toolStripStatus_Port.Size = new System.Drawing.Size(120, 20);
            this.toolStripStatus_Port.Text = "串口状态：False";
            this.toolStripStatus_Port.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_recestatus
            // 
            this.toolStripStatus_recestatus.Name = "toolStripStatus_recestatus";
            this.toolStripStatus_recestatus.Size = new System.Drawing.Size(93, 20);
            this.toolStripStatus_recestatus.Text = "收到数据：0";
            this.toolStripStatus_recestatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_sendstatus
            // 
            this.toolStripStatus_sendstatus.Name = "toolStripStatus_sendstatus";
            this.toolStripStatus_sendstatus.Size = new System.Drawing.Size(93, 20);
            this.toolStripStatus_sendstatus.Text = "发送数据：0";
            this.toolStripStatus_sendstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatus_Time
            // 
            this.toolStripStatus_Time.Name = "toolStripStatus_Time";
            this.toolStripStatus_Time.Size = new System.Drawing.Size(0, 20);
            // 
            // comboBox_BandRate
            // 
            this.comboBox_BandRate.Font = new System.Drawing.Font("宋体", 13F);
            this.comboBox_BandRate.FormattingEnabled = true;
            this.comboBox_BandRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBox_BandRate.Location = new System.Drawing.Point(121, 87);
            this.comboBox_BandRate.Name = "comboBox_BandRate";
            this.comboBox_BandRate.Size = new System.Drawing.Size(121, 30);
            this.comboBox_BandRate.TabIndex = 7;
            this.comboBox_BandRate.Tag = "";
            // 
            // comboBox_Check
            // 
            this.comboBox_Check.Font = new System.Drawing.Font("宋体", 13F);
            this.comboBox_Check.FormattingEnabled = true;
            this.comboBox_Check.Items.AddRange(new object[] {
            "None"});
            this.comboBox_Check.Location = new System.Drawing.Point(121, 136);
            this.comboBox_Check.Name = "comboBox_Check";
            this.comboBox_Check.Size = new System.Drawing.Size(121, 30);
            this.comboBox_Check.TabIndex = 8;
            // 
            // comboBox_Data
            // 
            this.comboBox_Data.Font = new System.Drawing.Font("宋体", 13F);
            this.comboBox_Data.FormattingEnabled = true;
            this.comboBox_Data.Items.AddRange(new object[] {
            "8",
            "9"});
            this.comboBox_Data.Location = new System.Drawing.Point(121, 185);
            this.comboBox_Data.Name = "comboBox_Data";
            this.comboBox_Data.Size = new System.Drawing.Size(121, 30);
            this.comboBox_Data.TabIndex = 9;
            // 
            // comboBox_Stop
            // 
            this.comboBox_Stop.Font = new System.Drawing.Font("宋体", 13F);
            this.comboBox_Stop.FormattingEnabled = true;
            this.comboBox_Stop.Items.AddRange(new object[] {
            "1",
            "0"});
            this.comboBox_Stop.Location = new System.Drawing.Point(121, 234);
            this.comboBox_Stop.Name = "comboBox_Stop";
            this.comboBox_Stop.Size = new System.Drawing.Size(121, 30);
            this.comboBox_Stop.TabIndex = 10;
            // 
            // button_OK
            // 
            this.button_OK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_OK.Font = new System.Drawing.Font("宋体", 13F);
            this.button_OK.Location = new System.Drawing.Point(4, 288);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(118, 31);
            this.button_OK.TabIndex = 11;
            this.button_OK.Text = "打开串口";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Refresh
            // 
            this.button_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Refresh.Font = new System.Drawing.Font("宋体", 13F);
            this.button_Refresh.Location = new System.Drawing.Point(128, 288);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(121, 31);
            this.button_Refresh.TabIndex = 12;
            this.button_Refresh.Text = "刷新串口";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // richTextBox_Send
            // 
            this.richTextBox_Send.Location = new System.Drawing.Point(264, 466);
            this.richTextBox_Send.Name = "richTextBox_Send";
            this.richTextBox_Send.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_Send.Size = new System.Drawing.Size(529, 101);
            this.richTextBox_Send.TabIndex = 14;
            this.richTextBox_Send.Text = "01 03 00 00 00 0A C5 CD";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 13F);
            this.label6.Location = new System.Drawing.Point(260, 432);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 22);
            this.label6.TabIndex = 15;
            this.label6.Text = "发送数据:";
            // 
            // richTextBox_Receive
            // 
            this.richTextBox_Receive.Location = new System.Drawing.Point(264, 62);
            this.richTextBox_Receive.Name = "richTextBox_Receive";
            this.richTextBox_Receive.Size = new System.Drawing.Size(689, 357);
            this.richTextBox_Receive.TabIndex = 16;
            this.richTextBox_Receive.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 13F);
            this.label7.Location = new System.Drawing.Point(274, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 22);
            this.label7.TabIndex = 17;
            this.label7.Text = "接收数据:";
            // 
            // button_ClcSendData
            // 
            this.button_ClcSendData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_ClcSendData.Font = new System.Drawing.Font("宋体", 13F);
            this.button_ClcSendData.Location = new System.Drawing.Point(799, 513);
            this.button_ClcSendData.Name = "button_ClcSendData";
            this.button_ClcSendData.Size = new System.Drawing.Size(154, 31);
            this.button_ClcSendData.TabIndex = 18;
            this.button_ClcSendData.Text = "清除发送数据";
            this.button_ClcSendData.UseVisualStyleBackColor = true;
            this.button_ClcSendData.Click += new System.EventHandler(this.button_ClcSendData_Click);
            // 
            // button_ClcReceData
            // 
            this.button_ClcReceData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_ClcReceData.Font = new System.Drawing.Font("宋体", 13F);
            this.button_ClcReceData.Location = new System.Drawing.Point(623, 32);
            this.button_ClcReceData.Name = "button_ClcReceData";
            this.button_ClcReceData.Size = new System.Drawing.Size(154, 31);
            this.button_ClcReceData.TabIndex = 19;
            this.button_ClcReceData.Text = "清除接收数据";
            this.button_ClcReceData.UseVisualStyleBackColor = true;
            this.button_ClcReceData.Click += new System.EventHandler(this.button_ClcReceData_Click);
            // 
            // button_SendData
            // 
            this.button_SendData.Enabled = false;
            this.button_SendData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_SendData.Font = new System.Drawing.Font("宋体", 13F);
            this.button_SendData.Location = new System.Drawing.Point(813, 466);
            this.button_SendData.Name = "button_SendData";
            this.button_SendData.Size = new System.Drawing.Size(118, 31);
            this.button_SendData.TabIndex = 24;
            this.button_SendData.Text = "发送";
            this.button_SendData.UseVisualStyleBackColor = true;
            this.button_SendData.Click += new System.EventHandler(this.button_SendData_Click);
            // 
            // textBox_selectTime
            // 
            this.textBox_selectTime.Enabled = false;
            this.textBox_selectTime.Font = new System.Drawing.Font("宋体", 10F);
            this.textBox_selectTime.Location = new System.Drawing.Point(128, 339);
            this.textBox_selectTime.Name = "textBox_selectTime";
            this.textBox_selectTime.Size = new System.Drawing.Size(80, 27);
            this.textBox_selectTime.TabIndex = 25;
            this.textBox_selectTime.Text = "1000";
            // 
            // checkBox_SendData
            // 
            this.checkBox_SendData.AutoSize = true;
            this.checkBox_SendData.Enabled = false;
            this.checkBox_SendData.Font = new System.Drawing.Font("宋体", 12F);
            this.checkBox_SendData.Location = new System.Drawing.Point(5, 339);
            this.checkBox_SendData.Name = "checkBox_SendData";
            this.checkBox_SendData.Size = new System.Drawing.Size(111, 24);
            this.checkBox_SendData.TabIndex = 26;
            this.checkBox_SendData.Text = "定时发送";
            this.checkBox_SendData.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 13F);
            this.label8.Location = new System.Drawing.Point(211, 342);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 22);
            this.label8.TabIndex = 27;
            this.label8.Text = "ms";
            // 
            // textBox_Filepath
            // 
            this.textBox_Filepath.Enabled = false;
            this.textBox_Filepath.Font = new System.Drawing.Font("宋体", 10F);
            this.textBox_Filepath.Location = new System.Drawing.Point(5, 411);
            this.textBox_Filepath.Name = "textBox_Filepath";
            this.textBox_Filepath.Size = new System.Drawing.Size(243, 27);
            this.textBox_Filepath.TabIndex = 29;
            this.textBox_Filepath.Click += new System.EventHandler(this.textBox_Filepath_Click);
            // 
            // checkBox_Sendfile
            // 
            this.checkBox_Sendfile.AutoSize = true;
            this.checkBox_Sendfile.Enabled = false;
            this.checkBox_Sendfile.Font = new System.Drawing.Font("宋体", 12F);
            this.checkBox_Sendfile.Location = new System.Drawing.Point(5, 375);
            this.checkBox_Sendfile.Name = "checkBox_Sendfile";
            this.checkBox_Sendfile.Size = new System.Drawing.Size(111, 24);
            this.checkBox_Sendfile.TabIndex = 30;
            this.checkBox_Sendfile.Text = "发送文件";
            this.checkBox_Sendfile.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.MenuItem_About,
            this.MenuItem_Quit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1003, 28);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_ReceData,
            this.发送数据ToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.saveToolStripMenuItem.Text = "保存";
            // 
            // MenuItem_ReceData
            // 
            this.MenuItem_ReceData.Name = "MenuItem_ReceData";
            this.MenuItem_ReceData.Size = new System.Drawing.Size(152, 26);
            this.MenuItem_ReceData.Text = "接收数据";
            this.MenuItem_ReceData.Click += new System.EventHandler(this.MenuItem_ReceData_Click);
            // 
            // 发送数据ToolStripMenuItem
            // 
            this.发送数据ToolStripMenuItem.Name = "发送数据ToolStripMenuItem";
            this.发送数据ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.发送数据ToolStripMenuItem.Text = "发送数据";
            this.发送数据ToolStripMenuItem.Click += new System.EventHandler(this.SaveData_Click);
            // 
            // MenuItem_About
            // 
            this.MenuItem_About.Name = "MenuItem_About";
            this.MenuItem_About.Size = new System.Drawing.Size(53, 24);
            this.MenuItem_About.Text = "关于";
            this.MenuItem_About.Click += new System.EventHandler(this.MenuItem_About_Click);
            // 
            // MenuItem_Quit
            // 
            this.MenuItem_Quit.Name = "MenuItem_Quit";
            this.MenuItem_Quit.Size = new System.Drawing.Size(53, 24);
            this.MenuItem_Quit.Text = "退出";
            this.MenuItem_Quit.Click += new System.EventHandler(this.MenuItem_Quit_Click);
            // 
            // timerSend
            // 
            this.timerSend.Interval = 1000;
            this.timerSend.Tick += new System.EventHandler(this.timerSend_Tick);
            // 
            // processDatatimer1
            // 
            this.processDatatimer1.Interval = 50;
            this.processDatatimer1.Tick += new System.EventHandler(this.processDatatimer1_Tick);
            // 
            // SerialDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(1003, 657);
            this.Controls.Add(this.checkBox_Sendfile);
            this.Controls.Add(this.textBox_Filepath);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBox_SendData);
            this.Controls.Add(this.textBox_selectTime);
            this.Controls.Add(this.button_SendData);
            this.Controls.Add(this.button_ClcReceData);
            this.Controls.Add(this.button_ClcSendData);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.richTextBox_Receive);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.richTextBox_Send);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.comboBox_Stop);
            this.Controls.Add(this.comboBox_Data);
            this.Controls.Add(this.comboBox_Check);
            this.Controls.Add(this.comboBox_BandRate);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_Serial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "SerialDebug";
            this.Text = "串口调试助手 ";
            this.Load += new System.EventHandler(this.SerialDebug_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Serial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer status_timer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox comboBox_BandRate;
        private System.Windows.Forms.ComboBox comboBox_Check;
        private System.Windows.Forms.ComboBox comboBox_Data;
        private System.Windows.Forms.ComboBox comboBox_Stop;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.RichTextBox richTextBox_Send;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox richTextBox_Receive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_ClcSendData;
        private System.Windows.Forms.Button button_ClcReceData;
        private System.Windows.Forms.Button button_SendData;
        private System.Windows.Forms.TextBox textBox_selectTime;
        private System.Windows.Forms.CheckBox checkBox_SendData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_Filepath;
        private System.Windows.Forms.CheckBox checkBox_Sendfile;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 发送数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_ReceData;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_About;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Quit;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Port;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_recestatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_sendstatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus_Time;
        private System.Windows.Forms.Timer timerSend;
        private System.Windows.Forms.Timer processDatatimer1;
    }
}

