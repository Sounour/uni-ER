/*
 * Created by SharpDevelop.
 * User: Philipp Heinisch
 * Date: 02.01.2017
 * Time: 19:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ANN_XOR__EvolutionA1
{
	/// <summary>
	/// Description of Global_Vars.
	/// </summary>
	public static class Global_Vars
	{
		//General
		public const int INDIVIDUALS_PER_GENERATION = 400;
		public const int GENERATIONS = 500;
		
		//Selection Method
		/// <summary>
		/// After an generation round (before mutation), there is the pairing time. With which chance an individual have the chance for sex?
		/// <remarks>C:\Users\Philipp\Documents\Einsortiertes\Nach Lebensabschnitten einsortiert\Studiumszeit (2012-2019)\Studium\9. Semester\ER\Vorlesungen\3_evolAlgs.pdf</remarks>
		/// <list type="list">
		/// <item>PROPORTIONATE_SELECTION: p(i) = f(i)/SUMj∈{1,...,N} f(j)</item>
		/// <item>RANK_BASED_SELECTION: rank all individuals about her fitness, then the same like the first selection: p(i) = r(i)/SUMj∈{1,...,N} r(j)</item>
		/// <item>TOURMENT_SELECTION: pick randomly individuals, the best two of them will have a good night (Hamann: "we pick a group of k individuals from the population randomly, the best individual of this group is selected")</item>
		/// </list>
		/// </summary>
		public const SELECTIONMODE SELMODE = SELECTIONMODE.RANK_BASED_SELECTION;
		public enum SELECTIONMODE {
			PROPORTIONATE_SELECTION, RANK_BASED_SELECTION, TOURMENT_SELECTION
		}
		//Replacement
		/// <summary>
		/// 0- no elitsn, no individual will survive longer than ome round
		/// 1- full elitsm, all individuals will survvive all rounds without changes
		/// </summary>
		public const double ELITISM_RATE = 0.1;
		
		//Mutation
		/// <summary>
		/// 0- nonody will mutate
		/// 1- all (except the elitism individuals from the last round) will muteted
		/// </summary>
		public const double MUTATIONRATE = 0.5;
		public const double MAXMUTATIONVALUE_FOREACH_WEIGHT = Global_Vars.WEIGHT_BORDER*2;
		
		//Recombination
		/// <summary>
		/// If two individuals have a good night together, which child will be the outcome?
		/// <list type="list">
		/// <item>MIN_VALUE: compare each coordinate part of the parent. The MIN will be the coordinate part of the child.</item>
		/// <item>MAX_VALUE: compare each coordinate part of the parent. The MAX will be the coordinate part of the child.</item>
		/// <item>AVERAGE: compare each coordinate part of the parent. The AVERAGE will be the coordinate part of the child.</item>
		/// <item>RANDOM_BETWEEN: the coordinate of the child will be a random number between the parents coordinates.</item>
		/// <item>RANDOM_TOTAL: foreach coordination part: the child will be randomly the coordinat part from the mother or father.</item>
		/// </list>
		/// </summary>
		public const RECOMBINATIONMODE RECOMBMODE = RECOMBINATIONMODE.RANDOM_TOTAL;
		public enum RECOMBINATIONMODE {
			MIN_VALUE, MAX_VALUE, AVERAGE, RANDOM_BETWEEN, RANDOM_TOTAL
		}
		
		//ADVANCED
		public const double WEIGHT_BORDER = 1.5;
		public static double activation_function(double x) {
			return (2d/(1+Math.Exp(-2*x)))-1d;
		}
	}
}
