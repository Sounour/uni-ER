/*
 * Created by SharpDevelop.
 * User: Philipp Heinisch
 * Date: 02.01.2017
 * Time: 17:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace ANN_XOR__EvolutionA1
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			Show();
			
			Evolve.run(this);
		}
	}
}
