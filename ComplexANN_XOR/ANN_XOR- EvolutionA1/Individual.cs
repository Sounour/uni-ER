/*
 * Created by SharpDevelop.
 * User: Philipp Heinisch
 * Date: 02.01.2017
 * Time: 20:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ANN_XOR__EvolutionA1
{
	/// <summary>
	/// Individual of population.
	/// </summary>
	public class Individual
	{
		private static int ID = 0;
		private static readonly Random r = new Random();
		
		/// <summary>
		/// The genotype of the individual (the coordinates)
		/// </summary>
		//input neurons- we must program that, because a input neuron is not more than Global_Vars.activation_function({sensor}), because by input neuron we don't play with weights
		public Neuron H1, H2, Y;
		public int i_id;
		
		public Individual(Neuron h1, Neuron h2, Neuron y)
		{
			this.H1 = h1;
			this.H2 = h2;
			this.Y = y;
			
			i_id = ID;
			ID++;
		}
		
		public override String ToString() {
			return "Individual ID " + i_id + ": A-B->Neurone(" + H1 + "|" + H2 + ")->Neuron(" + Y + "), Fitness: " + Math.Round(getFitness(), 0);
		}
		
		//get fitness an prozent: 0-100
		public double getFitness() {
			double sum = 0;
			for(int a=0; a<=1; a++) {
				for(int b=0; b<=1; b++) {
					double output_h1 = H1.getOutput(Global_Vars.activation_function(a), Global_Vars.activation_function(b)), output_h2 = H2.getOutput(Global_Vars.activation_function(a), Global_Vars.activation_function(b));
					sum += 1-Math.Abs(((a+b == 2) ? 0: a+b)-Y.getOutput(output_h1, output_h2));
				}
			}
			return 100*sum/4;
		}
		
		public Individual mutate() {
			return new Individual(H1.mutate(), H2.mutate(), Y.mutate());
		}
		
		public Individual mate(Individual otherparent) {
			
			if(Global_Vars.RECOMBMODE == Global_Vars.RECOMBINATIONMODE.RANDOM_TOTAL) {
				return new Individual((r.NextDouble() > 0.5) ? H1 : otherparent.H1, (r.NextDouble() > 0.5) ? H2 : otherparent.H2, (r.NextDouble() > 0.5) ? Y : otherparent.Y);
			}
			
			return new Individual(H1.mate(otherparent.H1), H2.mate(otherparent.H2), Y.mate(otherparent.Y));
		}
	}
}
