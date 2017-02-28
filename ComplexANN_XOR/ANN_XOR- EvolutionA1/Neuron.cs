/*
 * Created by SharpDevelop.
 * User: Philipp Heinisch
 * Date: 04.01.2017
 * Time: 13:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ANN_XOR__EvolutionA1
{
	/// <summary>
	/// Neuron in ANN.
	/// </summary>
	public class Neuron
	{
		private double weight_input1, weight_input2;
		private readonly Random r = new Random();
		
		public Neuron() {
			weight_input1 = (r.NextDouble()-0.5)*Global_Vars.WEIGHT_BORDER*2;
			System.Threading.Thread.Sleep(5);
			weight_input2 = (r.NextDouble()-0.5)*Global_Vars.WEIGHT_BORDER*2;
		}
		
		public Neuron(double weight_input1, double weight_input2)
		{
			this.weight_input1 = weight_input1;
			this.weight_input2 = weight_input2;
		}
		
		public override String ToString() {
			return "{(weight_input1|weight_input2) = (" + Math.Round(weight_input1, 1) + "|" + Math.Round(weight_input2, 1) + ")}";
		}
		
		public double getOutput(double i1, double i2=0d) {
			double input = i1*weight_input1 + i2*weight_input2;
			return Global_Vars.activation_function(input);
		}
		
		public Neuron mutate() {
			double new_x = weight_input1+(r.NextDouble()-0.5)*Global_Vars.MAXMUTATIONVALUE_FOREACH_WEIGHT;
			new_x = Math.Min(new_x, Global_Vars.WEIGHT_BORDER);
			new_x = Math.Max(new_x, -(Global_Vars.WEIGHT_BORDER));
			double new_y = weight_input2+(r.NextDouble()-0.5)*Global_Vars.MAXMUTATIONVALUE_FOREACH_WEIGHT;
			new_y = Math.Min(new_y, Global_Vars.WEIGHT_BORDER);
			new_y = Math.Max(new_y, -(Global_Vars.WEIGHT_BORDER));
			
			return new Neuron(new_x, new_y);
		}
		
		public Neuron mate(Neuron otherparent) {
			double new_x, new_y;
			double gen_x_min = Math.Min(weight_input1, otherparent.weight_input1), gen_y_min = Math.Min(weight_input2, otherparent.weight_input2);
			double gen_x_max = Math.Max(weight_input1, otherparent.weight_input1), gen_y_max = Math.Max(weight_input2, otherparent.weight_input2);
			
			switch(Global_Vars.RECOMBMODE) {
					case Global_Vars.RECOMBINATIONMODE.MIN_VALUE : new_x = gen_x_min; new_y = gen_y_min; break;
					case Global_Vars.RECOMBINATIONMODE.MAX_VALUE : new_x = gen_x_max; new_y = gen_y_max; break;
					case Global_Vars.RECOMBINATIONMODE.AVERAGE : new_x = (weight_input1+otherparent.weight_input1)/2; new_y = (weight_input1+otherparent.weight_input2)/2; break;
					case Global_Vars.RECOMBINATIONMODE.RANDOM_BETWEEN : new_x = gen_x_min+r.NextDouble()*(gen_x_max-gen_x_min); new_y = gen_y_min+r.NextDouble()*(gen_y_max-gen_y_min); break;
					case Global_Vars.RECOMBINATIONMODE.RANDOM_TOTAL : new_x = (r.NextDouble() > 0.5) ? weight_input1 : otherparent.weight_input1; new_y = (r.NextDouble() > 0.5) ? weight_input2 : otherparent.weight_input2; break;
			}
			
			return new Neuron(new_x, new_y);
		}
	}
}
