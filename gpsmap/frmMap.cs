using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gpsmap
{
	public partial class frmMap : Form
	{
		private WebBrowser webBrowser2;
		private WebBrowser webBrowser3;
		private WebBrowser webBrowser1;

		public frmMap(string lat, string lon)
		{
			InitializeComponent();

			if (lat == string.Empty || lon == string.Empty)
			{
				this.Dispose();
			}

			try
			{
				StringBuilder queryAddress = new StringBuilder();
				queryAddress.Append("http://maps.google.com/maps?q=");

				if (lat != string.Empty)
				{
					queryAddress.Append(lat + "%2C");
				}

				if (lon != string.Empty)
				{
					queryAddress.Append(lon);
				}

				webBrowser1.Navigate(queryAddress.ToString());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(), "Error");
			}
		}

		private void InitializeComponent()
		{
			this.webBrowser2 = new System.Windows.Forms.WebBrowser();
			this.webBrowser3 = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// webBrowser2
			// 
			this.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser2.Location = new System.Drawing.Point(0, 0);
			this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser2.Name = "webBrowser2";
			this.webBrowser2.Size = new System.Drawing.Size(634, 367);
			this.webBrowser2.TabIndex = 0;
			// 
			// webBrowser3
			// 
			this.webBrowser3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser3.Location = new System.Drawing.Point(0, 0);
			this.webBrowser3.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser3.Name = "webBrowser3";
			this.webBrowser3.Size = new System.Drawing.Size(634, 367);
			this.webBrowser3.TabIndex = 1;
			// 
			// frmMap
			// 
			this.ClientSize = new System.Drawing.Size(634, 367);
			this.Controls.Add(this.webBrowser3);
			this.Controls.Add(this.webBrowser2);
			this.Name = "frmMap";
			this.Load += new System.EventHandler(this.frmMap_Load);
			this.ResumeLayout(false);

		}

		private void frmMap_Load(object sender, EventArgs e)
		{

		}
	}
}