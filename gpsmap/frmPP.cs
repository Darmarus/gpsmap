using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace gpsmap
{
	public partial class frmPP : Form
	{
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.button1 = new System.Windows.Forms.Button();
			this.btnMapIt = new System.Windows.Forms.Button();
			this.txtLat = new System.Windows.Forms.TextBox();
			this.txtLong = new System.Windows.Forms.TextBox();
			this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(143, 137);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// btnMapIt
			// 
			this.btnMapIt.Location = new System.Drawing.Point(143, 167);
			this.btnMapIt.Name = "btnMapIt";
			this.btnMapIt.Size = new System.Drawing.Size(75, 23);
			this.btnMapIt.TabIndex = 1;
			this.btnMapIt.Text = "button2";
			this.btnMapIt.UseVisualStyleBackColor = true;
			this.btnMapIt.Click += new System.EventHandler(this.btnMapIt_Click);
			// 
			// txtLat
			// 
			this.txtLat.Location = new System.Drawing.Point(119, 43);
			this.txtLat.Name = "txtLat";
			this.txtLat.Size = new System.Drawing.Size(100, 20);
			this.txtLat.TabIndex = 2;
			// 
			// txtLong
			// 
			this.txtLong.Location = new System.Drawing.Point(119, 70);
			this.txtLong.Name = "txtLong";
			this.txtLong.Size = new System.Drawing.Size(100, 20);
			this.txtLong.TabIndex = 3;
			// 
			// frmPP
			// 
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.txtLong);
			this.Controls.Add(this.txtLat);
			this.Controls.Add(this.btnMapIt);
			this.Controls.Add(this.button1);
			this.Name = "frmPP";
			this.Load += new System.EventHandler(this.frmPP_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		public string Latitude;
		private Button button1;
		private Button btnMapIt;
		private TextBox txtLat;
		private TextBox txtLong;
		private SerialPort serialPort1;
		private IContainer components;
		private Timer timer1;
		public string Longitude;

		#region Constructor

		/// <span class="code-SummaryComment"><summary></span>
		/// Constructor
		/// <span class="code-SummaryComment"></summary></span>
		public frmPP()
		{
			InitializeComponent();

			// Try to open the serial port
			try
			{
				serialPort1.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				timer1.Enabled = false;
				button1.Text = "Update";
				return;
			}
		}
		/// <span class="code-SummaryComment"><summary></span>
		/// Try to update present position if the port is setup correctly
		/// and the GPS device is returning values
		/// <span class="code-SummaryComment"></summary></span>
		/// <span class="code-SummaryComment"><param name="sender"></param></span>
		/// <span class="code-SummaryComment"><param name="e"></param></span>
		private void timer1_Tick(object sender, EventArgs e)
		{
			if (serialPort1.IsOpen)
			{
				string data = serialPort1.ReadExisting();
				string[] strArr = data.Split('$');
				for (int i = 0; i < strArr.Length; i++)
				{
					string strTemp = strArr[i];
					string[] lineArr = strTemp.Split(',');
					if (lineArr[0] == "GPGGA")
					{
						try
						{
							//Latitude
							Double dLat = Convert.ToDouble(lineArr[2]);
							dLat = dLat / 100;
							string[] lat = dLat.ToString().Split('.');
							Latitude = lineArr[3].ToString() +
							lat[0].ToString() + "." +
							((Convert.ToDouble(lat[1]) /
							60)).ToString("#####");

							//Longitude
							Double dLon = Convert.ToDouble(lineArr[4]);
							dLon = dLon / 100;
							string[] lon = dLon.ToString().Split('.');
							Longitude = lineArr[5].ToString() +
							lon[0].ToString() + "." +
							((Convert.ToDouble(lon[1]) /
							60)).ToString("#####");

							//Display
							txtLat.Text = Latitude;
							txtLong.Text = Longitude;

							btnMapIt.Enabled = true;
						}
						catch
						{
							//Cannot Read GPS values
							txtLat.Text = "GPS Unavailable";
							txtLong.Text = "GPS Unavailable";
							btnMapIt.Enabled = false;
						}
					}
				}
			}
			else
			{
				txtLat.Text = "COM Port Closed";
				txtLong.Text = "COM Port Closed";
				btnMapIt.Enabled = false;
			}
		}
		#endregion

		/// <span class="code-SummaryComment"><summary></span>
		/// Enable or disable the timer to start continuous
		/// updates or disable all updates
		/// <span class="code-SummaryComment"></summary></span>
		/// <span class="code-SummaryComment"><param name="sender"></param></span>
		/// <span class="code-SummaryComment"><param name="e"></param></span>
		private void button1_Click(object sender, EventArgs e)
		{
			if (timer1.Enabled == true)
			{
				timer1.Enabled = false;
			}
			else
			{
				timer1.Enabled = true;
			}

			if (button1.Text == "Update")
				button1.Text = "Stop Updates";
			else
				button1.Text = "Update";
		}

		/// <span class="code-SummaryComment"><summary></span>
		/// Swap serialPort1 to port COM1
		/// <span class="code-SummaryComment"></summary></span>
		/// <span class="code-SummaryComment"><param name="sender"></param></span>
		/// <span class="code-SummaryComment"><param name="e"></param></span>
		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			try
			{
				serialPort1.Close();
				serialPort1.PortName = "COM1";
				serialPort1.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "COM1");
			}
		}

		private void btnMapIt_Click(object sender, EventArgs e)
		{
			frmMap f = new frmMap(Latitude, Longitude);
			f.Show();
		}

		private void frmPP_Load(object sender, EventArgs e)
		{

		}
	}
}