using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Anubis_Main_Swapper
{
	public class Form1 : Form
	{
		private IContainer components = null;

		private WebBrowser webBrowser1;

		private async void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (webBrowser1.Url.ToString().Contains("oauth20_logout.srf"))
			{
				webBrowser1.Navigate("https://social.xbox.com/");
			}
			else
			{
				if (!webBrowser1.Url.ToString().Contains("sid="))
				{
					return;
				}
				try
				{
					string[] String1 = webBrowser1.Url.ToString().Split(new string[1] { "sid=" }, StringSplitOptions.None);
					string String2 = String1[1];
					string[] String3 = String2.Split('&');
					string[] String4 = webBrowser1.Url.ToString().Split(new string[1] { "spt=" }, StringSplitOptions.None);
					string String5 = Interaction.InputBox("Target gamertag:", Text, null);
					if (String5.Count() >= 1)
					{
						string String6 = Interaction.InputBox("Reserve xuid:", Text, null);
						if (String6.Count() >= 1)
						{
							bool Bool = false;
							int Integer = 1;
							WebClient WebClient = new WebClient
							{
								Headers = 
								{
									[HttpRequestHeader.Authorization] = String4[1],
									["x-xbl-contract-version"] = "3"
								}
							};
							while (!Bool)
							{
								try
								{
									await WebClient.UploadStringTaskAsync("https://sisu.xboxlive.com/proxy?sessionid=" + String3[0], "post", "{\"GamertagReserve\":{\"Gamertag\":\"" + String5 + "\",\"ReservationId\":" + String6 + ",\"Duration\":\"1:00:00\"}}");
									Bool = true;
									Hide();
									MessageBox.Show("'" + String5 + "' successfully reserved to '" + String6 + "'", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
									Close();
								}
								catch (Exception Exception)
								{
									if (Exception.Message.Contains("409"))
									{
										Text = String5 + " - " + String6 + " (" + Integer++ + ")";
									}
									else if (Exception.Message.Contains("429"))
									{
										Bool = true;
									}
								}
							}
						}
						else
						{
							webBrowser1.Navigate("https://social.xbox.com/");
						}
					}
					else
					{
						webBrowser1.Navigate("https://social.xbox.com/");
					}
				}
				catch
				{
					Close();
				}
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			webBrowser1.Navigate("https://login.live.com/oauth20_logout.srf");
		}

		public Form1()
		{
			InitializeComponent();
			ServicePointManager.CheckCertificateRevocationList = false;
			ServicePointManager.DefaultConnectionLimit = 1000;
			ServicePointManager.Expect100Continue = false;
			ServicePointManager.UseNagleAlgorithm = false;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(4);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(27, 25);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(640, 439);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 439);
            this.Controls.Add(this.webBrowser1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anubis Mainswapper, made by Joker, Cracked by force#0666";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}
	}
}
