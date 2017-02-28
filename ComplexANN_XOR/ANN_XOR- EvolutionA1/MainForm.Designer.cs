/*
 * Created by SharpDevelop.
 * User: Philipp Heinisch
 * Date: 02.01.2017
 * Time: 17:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Windows.Forms;
using System.Diagnostics;
namespace ANN_XOR__EvolutionA1
{
	partial class MainForm
	{
		
		Process gnu;
		
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		public System.Windows.Forms.ProgressBar progress;
		public System.Windows.Forms.TextBox log;
		public System.Windows.Forms.Button button_graph;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.progress = new System.Windows.Forms.ProgressBar();
			this.log = new System.Windows.Forms.TextBox();
			this.button_graph = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point(13, 13);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(459, 23);
			this.progress.Step = 1;
			this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progress.TabIndex = 0;
			// 
			// log
			// 
			this.log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.log.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.log.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.log.Location = new System.Drawing.Point(13, 43);
			this.log.Multiline = true;
			this.log.Name = "log";
			this.log.ReadOnly = true;
			this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.log.Size = new System.Drawing.Size(459, 406);
			this.log.TabIndex = 1;
			this.log.Text = "Log started...";
			// 
			// button_graph
			// 
			this.button_graph.BackColor = System.Drawing.SystemColors.Highlight;
			this.button_graph.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button_graph.Location = new System.Drawing.Point(13, 13);
			this.button_graph.Name = "button_graph";
			this.button_graph.Size = new System.Drawing.Size(459, 23);
			this.button_graph.TabIndex = 2;
			this.button_graph.Text = "GnuPlot öffnen (-> Ctrl+C)";
			this.button_graph.UseVisualStyleBackColor = false;
			this.button_graph.Visible = false;
			this.button_graph.Click += new System.EventHandler(this.GraphClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 461);
			this.Controls.Add(this.button_graph);
			this.Controls.Add(this.log);
			this.Controls.Add(this.progress);
			this.Name = "MainForm";
			this.Text = "ANN with hidden layer: XOR- EvolutionA1";
			this.ResumeLayout(false);
			this.PerformLayout();
			this.FormClosed += GNUShut;

		}

		private void GraphClick(object sender, System.EventArgs e)
		{
			gnu = new Process();
			gnu.StartInfo.FileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86) + "\\gnuplot\\bin\\gnuplot.exe";
			//p.StartInfo.Arguments = "plot '" + Evolve.path_to_GNUdat.Replace('\\', '/') + "' index 0 with line lc rgb '#ff0000' lw 2 title 'MAX'";
			//MessageBox.Show(p.StartInfo.Arguments);
			System.Windows.Forms.Clipboard.SetDataObject("plot '" + Evolve.path_to_GNUdat.Replace('\\', '/') + "' index 0 with line lc rgb '#ff0000' lw 2 title 'MAX'",true);
			gnu.Start();
		}
		
		private void GNUShut(object sender, FormClosedEventArgs e) {
			if(gnu != null) {
				if(!gnu.HasExited)
					gnu.Kill();
			}
		}
	}
}
