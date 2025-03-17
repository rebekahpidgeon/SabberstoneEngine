using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneBasicAI.PartialObservation;
using System.Collections.Generic;

//Developed by Oskar Kirmis and Florian Koch and submitted to the 2018 Hearthstone AI Competition's Premade Deck Playing Track

//Modified 17/03/2025, changed the Score function to use RampScore for every class. GreedyAgent made public

namespace SabberStoneBasicAI.AIAgents
{
	// Plain old Greedy Bot
	public class GreedyAgent : AbstractAgent
	{
		public override void InitializeAgent() { }
		public override void InitializeGame() { }
		public override void FinalizeGame() { }
		public override void FinalizeAgent() { }


		public override PlayerTask GetMove(POGame game)
		{
			var player = game.CurrentPlayer;

			// Implement a simple Mulligan Rule
			if (player.MulliganState == Mulligan.INPUT)
			{
				List<int> mulligan = new AggroScore().MulliganRule().Invoke(player.Choice.Choices.Select(p => game.getGame().IdEntityDic[p]).ToList());
				return ChooseTask.Mulligan(player, mulligan);
			}

			// Get all simulation results for simulations that didn't fail
			var validOpts = game.Simulate(player.Options()).Where(x => x.Value != null);

			// If all simulations failed, play end turn option (always exists), else best according to score function
			return validOpts.Any() ?
				validOpts.OrderBy(x => Score(x.Value, player.PlayerId)).Last().Key :
				player.Options().First(x => x.PlayerTaskType == PlayerTaskType.END_TURN);
		}

		// Calculate different scores based on our hero's class
		private static int Score(POGame state, int playerId)
		{
			var p = state.CurrentPlayer.PlayerId == playerId ? state.CurrentPlayer : state.CurrentOpponent;
			return new RampScore { Controller = p }.Rate();
		}
	}
}
