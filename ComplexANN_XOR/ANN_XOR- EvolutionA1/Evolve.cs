/*
 * Created by SharpDevelop.
 * User: Philipp Heinisch
 * Date: 02.01.2017
 * Time: 20:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace ANN_XOR__EvolutionA1
{
	/// <summary>
	/// Description of Evolve.
	/// </summary>
	public static class Evolve
	{
		//private static List<String> log = new List<String>();
		private static Log log;
		//private static List<Individual> individuals = new List<Individual>(Global_Vars.INDIVIDUALS_PER_GENERATION);
		private static Random r = new Random();
		private static Population p;
		
		public static String path_to_GNUdat;
		
		public static void run(MainForm form) {
			//Start
			log = new Log(form.log);
			log.hiddenLog("Evolution starts with the follow parameters:");
			log.hiddenLog("Global_Vars.INDIVIDUALS_PER_GENERATION: " + Global_Vars.INDIVIDUALS_PER_GENERATION);
			log.hiddenLog("Global_Vars.GENERATIONS: " + Global_Vars.GENERATIONS);
			log.hiddenLog("Global_Vars.ELITISM_RATE: " + Global_Vars.ELITISM_RATE);
			log.hiddenLog("Global_Vars.SELMODE: " + Global_Vars.SELMODE);
			log.hiddenLog("Global_Vars.RECOMBMODE" + Global_Vars.RECOMBMODE);
			p = new Population();
			log.openLog("Eine Startpopulation wurde erstellt.");
			log.hiddenLog(p.ToString());
			
			double[] maxfitness = new double[Global_Vars.GENERATIONS+1];
			maxfitness[0] = p.getFittest();
			log.openLog("Generation 0, der Fitteste: " + p.getFittestIndividual());
			double[] averagefitness = new double[Global_Vars.GENERATIONS+1];
			averagefitness[0] = p.getAverage();
			log.openLog("Generation 0, der Durchschnitt: " + Math.Round(averagefitness[0], 1));
			
			form.progress.Value = form.progress.Step;
			
			for(int round=1; round<=Global_Vars.GENERATIONS; round++) {
				//mate - paaren
				log.openLog("Es beginnt die Paarungszeit in der Runde " + round);
				List<Individual> offspring = new List<Individual>();
				for(int i=(int)Math.Ceiling(Global_Vars.ELITISM_RATE*Global_Vars.INDIVIDUALS_PER_GENERATION); i<Global_Vars.INDIVIDUALS_PER_GENERATION; i++) {
					Individual mother = p.giveAIndividual(), father = p.giveAIndividual(mother);
					Individual child = mother.mate(father);
					offspring.Add(child);
					log.hiddenLog("Ein Nachkomme wurde aus [" + mother + "] und [" + father + "] gezeugt: " + child);
				}
				
				log.openLog("Es wurden " + (Global_Vars.INDIVIDUALS_PER_GENERATION - Math.Ceiling(Global_Vars.ELITISM_RATE*Global_Vars.INDIVIDUALS_PER_GENERATION)) + " gezeugt. Diese werden nun die schlechtesten der Generation " + (round-1) + " ersetzen.");
				p.replace_UnFittestWith(offspring);
				
				log.hiddenLog("Nun erfolgt die Mutation!");
				p.mutatePopulation();
				
				averagefitness[round] = p.getAverage();
				maxfitness[round] = p.getFittest();
				log.openLog("Es gibt nun eine neue Generation (" + round + "). Im Durschnitt ist diese " + Math.Round(averagefitness[round], 1) + " fitt, der Fitteste ist " + p.getFittestIndividual());
				
					log.hiddenLog(p.ToString());
					
					form.progress.Value = (int)Math.Floor((round/(Global_Vars.GENERATIONS + 0d))*form.progress.Maximum);
			}
			
			log.openLog("Evolution vorbei!");
			log.openLog("Im Vergleich zum Anfang gab es eine Fittnesssteigerung bei den Besten um " + Math.Round(maxfitness[Global_Vars.GENERATIONS]-maxfitness[0], 0) + " Prozentpunkte, der Durschnitt liegt nun bei " + Math.Round(averagefitness[Global_Vars.GENERATIONS], 2) + "%");
			log.openLog("Der Fitteste: " + p.getFittestIndividual());
			
			log.hiddenLog("Datenreihe MAXFITNESS: " + maxfitness);
			log.hiddenLog("Datenreihe AVERAGEFITNESS: " + averagefitness);
			
			
			//Logging& gnuplot
			String logfilepath = log.printLogToFile();
			path_to_GNUdat = Path.Combine(Path.GetDirectoryName(logfilepath), Path.GetFileNameWithoutExtension(logfilepath) + ".dat");
			StreamWriter stream = File.CreateText(path_to_GNUdat);
			stream.WriteLine("#Maximal fitness (index 0)");
			stream.WriteLine("#Generation Fitness");
			for(int i=0; i<maxfitness.Length; i++) {
				stream.WriteLine(i + " " + maxfitness[i]);
			}
			stream.WriteLine();
			stream.WriteLine("#Average Fitness (index 1)");
			stream.WriteLine("#Generation Fitness");
			for(int i=0; i<averagefitness.Length; i++) {
				stream.WriteLine(i + " " + averagefitness[i]);
			}
			stream.Close();
			
			form.progress.Value = form.progress.Maximum;
			
			log.openLog("Das Logfile ist zu finden unter: " + logfilepath + "(im selben Order die Gnuplotdatei: " + Path.GetFileName(path_to_GNUdat) + ")");
			
			form.progress.Visible = false;
			form.button_graph.Visible = true;
		}
	}
}
