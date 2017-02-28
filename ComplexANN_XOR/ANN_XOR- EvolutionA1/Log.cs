/*
 * Created by SharpDevelop.
 * User: Philipp Heinisch
 * Date: 03.01.2017
 * Time: 09:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace ANN_XOR__EvolutionA1
{
	/// <summary>
	/// For Logging.
	/// </summary>
	public class Log
	{
		private readonly List<String> log;
		private readonly TextBox guiconsole;
		
		public Log(TextBox guiconsole=null)
		{
			log = new List<String>();
			this.guiconsole = guiconsole;
		}
		
		public void hiddenLog(String message) {
			log.Add("\t " + message);
		}
		public void openLog(String message) {
			if(guiconsole == null)
				throw new NullReferenceException("Methode openLog ohne GUI nicht erlaubt! Benutze hiddenLog!");
			log.Add(message);
			guiconsole.AppendText(System.Environment.NewLine + message);
			guiconsole.Update();
		}
		
		public String printLogToFile() {
			String ret = Path.Combine(Path.GetTempPath(), "AckleyProblem", DateTime.Now.ToFileTime() + ".log");
			
			if(!Directory.Exists(Path.GetDirectoryName(ret))) {
				Directory.CreateDirectory(Path.GetDirectoryName(ret));
			}
			
			StreamWriter stream = File.CreateText(ret);
			stream.WriteLine("#################Neues Log für ER-Aufgbabe Blatt 3 Aufgabe 1 um " + DateTime.Now + "#################");
			foreach(String message in log) {
				stream.WriteLine(message);
			}
			stream.Close();
			
			return ret;
		}
	}
}
