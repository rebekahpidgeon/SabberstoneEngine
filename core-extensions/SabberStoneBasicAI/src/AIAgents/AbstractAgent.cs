﻿using System.Collections.Generic;
using SabberStoneBasicAI.PartialObservation;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneCore.Model;
using SabberStoneCore.Enums;

// Modified 17/03/2025 AbstractAgent made public

namespace SabberStoneBasicAI.AIAgents
{
	public abstract class AbstractAgent

	{
		public List<Card> preferedDeck;
		public CardClass preferedHero;


		public abstract void InitializeAgent();

		public abstract void InitializeGame();

		public abstract PlayerTask GetMove(POGame poGame);

		public abstract void FinalizeGame();

		public abstract void FinalizeAgent();

	}
}
