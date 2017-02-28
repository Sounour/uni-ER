/*
 * Created by SharpDevelop.
 * User: Philipp Heinisch
 * Date: 02.01.2017
 * Time: 21:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace ANN_XOR__EvolutionA1
{
	/// <summary>
	/// Population.
	/// </summary>
	public class Population
	{
		
		private class IndividualComparer : IComparer<Individual> {
			#region IComparer implementation
			public int Compare(Individual x, Individual y)
			{
				if (y.getFitness() < x.getFitness()) {
					return -1;
				}
				if (y.getFitness() > x.getFitness()) {
					return 1;
				}
				return 2;
			}
			#endregion
		}
		
		private static SortedList<Individual, double> individuals = new SortedList<Individual, double>(Global_Vars.INDIVIDUALS_PER_GENERATION, new IndividualComparer());
		private static readonly Random r= new Random();
		
		public Population()
		{
			for(int i=0; i<Global_Vars.INDIVIDUALS_PER_GENERATION; i++) {
				Individual ini = new Individual(new Neuron(), new Neuron(), new Neuron());
				individuals.Add(ini, ini.getFitness());
			}
		}
		
		public Individual getFittestIndividual() {
			foreach(Individual i in individuals.Keys)
				return i;
			throw new IndexOutOfRangeException();
		}
		public double getFittest() {
			foreach(double i in individuals.Values)
				return i;
			throw new IndexOutOfRangeException();
		}
		public double getAverage() {
			double sum = 0;
			foreach(double i_fitness in individuals.Values) {
				sum += i_fitness;
			}
			
			return sum/individuals.Count;
		}
		public List<Individual> get_k_Fittest(int k) {
			if(k <= 0)
				throw new ArgumentException();
			
			List<Individual> ret = new List<Individual>(k);
			
			foreach(Individual i in  individuals.Keys) {
				if(k <= 0)
					break;
				ret.Add(i);
				k--;
			}
			
			return ret;
		}
		
		public void replace_UnFittestWith(List<Individual> newGeneration) {
			for(int i=individuals.Count-newGeneration.Count; i<individuals.Count; ) {
				individuals.RemoveAt(i);
			}
			
			foreach(Individual newindividual in newGeneration) {
				individuals.Add(newindividual, newindividual.getFitness());
			}
		}
		
		public void mutatePopulation() {
			SortedList<Individual, double> newindividuals = new SortedList<Individual, double>(Global_Vars.INDIVIDUALS_PER_GENERATION, new IndividualComparer());
			int i=0;
			foreach(KeyValuePair<Individual, double> entry in individuals) {
				if(r.NextDouble() < Global_Vars.MUTATIONRATE && i >= Math.Round(Global_Vars.ELITISM_RATE*Global_Vars.INDIVIDUALS_PER_GENERATION)) {
					Individual newindividual = entry.Key.mutate();
					newindividuals.Add(newindividual, newindividual.getFitness());
				} else {
					newindividuals.Add(entry.Key, entry.Value);
				}
				
				i++;
			}
			
			individuals = newindividuals;
		}
		
		private double sumUpAllFitness() {
			double ret = 0;
			foreach(double fi in individuals.Values)
				ret += fi;
			
			return ret;
		}
		
		private double getFitnessOfIndividualOfRank(int rank) {
			if(rank < 0)
				throw new ArgumentException();
			foreach(double fitness in individuals.Values) {
				if(rank <= 0)
					return fitness;
				rank--;
			}
			throw new IndexOutOfRangeException();
		}
		
		public Individual giveAIndividual(Individual NotThis=null, Global_Vars.SELECTIONMODE selmode = Global_Vars.SELMODE) {
			switch(selmode) {
				case Global_Vars.SELECTIONMODE.RANK_BASED_SELECTION :
					int choose1 = r.Next(1, (int)Math.Round((Math.Pow(individuals.Count, 2)+individuals.Count)/2+1));
					int rank1 = 1;
					for(int i=(int)Math.Round((Math.Pow(individuals.Count, 2)+individuals.Count)/2+1)-individuals.Count; i>=1; i-=(individuals.Count-(rank1-1))) {
						if(i <= choose1)
							break;
						rank1++;
					}
					foreach(Individual ini in individuals.Keys) {
						if(rank1 <= 1 && ini != NotThis)
							return ini;
						rank1--;
					}
					break;
				case Global_Vars.SELECTIONMODE.PROPORTIONATE_SELECTION :
					int choose2 = r.Next(1, (int)Math.Ceiling(sumUpAllFitness()+1));
					int rank2 = 0;
					for(double i=sumUpAllFitness()-getFitnessOfIndividualOfRank(0); i > 0; i-=(getFitnessOfIndividualOfRank(rank2))) {
						if(i < choose2)
							break;
						rank2++;
					}
					rank2++;
					foreach(Individual ini in individuals.Keys) {
						if(rank2 <= 1 && ini != NotThis)
							return ini;
						rank2--;
					}
					break;
				case Global_Vars.SELECTIONMODE.TOURMENT_SELECTION :
					throw new NotImplementedException();
				default :
					throw new ArgumentException();
			}
			
			foreach(Individual ini in individuals.Keys) {
				return ini;
			}
			
			throw new NullReferenceException();
		}
		
		public override String ToString() {
			String ret = "Es sind " + individuals.Count + " Individuen in der Menge:" + Environment.NewLine;
			foreach(Individual ini in individuals.Keys) {
				ret += "\t " + ini + Environment.NewLine;
			}
			
			return ret;
		}
	}
}
