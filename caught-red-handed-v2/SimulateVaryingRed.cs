using System;
using System.Collections.Generic;

namespace VaryingRedSim
{
    public class Card
    {
        public int Val;
        public string Item;
        public string Color; // RED, BLUE, SPECIAL
        public string Type;
    }

    public class SimResult
    {
        public string Label;
        public int Matches;
        public double AvgRoundsPerMatch;
        public double AvgTableTurnsPerRound;
        public double AvgTurnsPerPlayer;
        public double RedBustRatePercent;
        public double CleanEscapeRatePercent;
        public double AvgDrawsPerRound;
        public double AvgResetsPerRound;
        public double AvgRedCardsLeftInHands;
    }

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("==================================================================================");
            Console.WriteLine("CAUGHT RED-HANDED V2: VARYING TOP RED VALUE SIMULATION (5,000 Matches Each)");
            Console.WriteLine("==================================================================================");

            // MODEL A: Fixed 12 Red Cards, Expanding Range (1..TopRed)
            // Baseline: 1, 2, 3 (4 copies each)
            // +1 (Top 4): 1, 2, 3, 4 (3 copies each)
            // +2 (Top 5): 1, 2, 3, 4, 5 (approx 12 cards: 3, 3, 2, 2, 2)
            // +3 (Top 6): 1, 2, 3, 4, 5, 6 (2 copies each)
            Console.WriteLine("\n--- EXPERIMENT A: FIXED 12 RED CARDS SPREAD ACROSS WIDER RANGE ---");
            Console.WriteLine("Testing how higher top-end red values affect playability when red density is constant (12 cards).");
            
            var resA0 = RunExperiment("Baseline (Top Red = 3: Values 1, 2, 3)", new int[] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 });
            var resA1 = RunExperiment("+1 (Top Red = 4: Values 1, 2, 3, 4)", new int[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4 });
            var resA2 = RunExperiment("+2 (Top Red = 5: Values 1, 2, 3, 4, 5)", new int[] { 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 5 });
            var resA3 = RunExperiment("+3 (Top Red = 6: Values 1, 2, 3, 4, 5, 6)", new int[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 });

            PrintComparisonTable(new List<SimResult> { resA0, resA1, resA2, resA3 });

            // MODEL B: Stretched 3-Rank Red Cards (1, 2, TopVal)
            Console.WriteLine("\n--- EXPERIMENT B: 3 RED RANKS (1, 2, TopRed) ---");
            Console.WriteLine("Keeping low 1 and 2, but elevating the highest contraband card.");
            var resB0 = RunExperiment("Baseline: Reds = {1, 2, 3}", new int[] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 });
            var resB1 = RunExperiment("+1: Reds = {1, 2, 4}", new int[] { 1, 1, 1, 1, 2, 2, 2, 2, 4, 4, 4, 4 });
            var resB2 = RunExperiment("+2: Reds = {1, 2, 5}", new int[] { 1, 1, 1, 1, 2, 2, 2, 2, 5, 5, 5, 5 });
            var resB3 = RunExperiment("+3: Reds = {1, 2, 6}", new int[] { 1, 1, 1, 1, 2, 2, 2, 2, 6, 6, 6, 6 });

            PrintComparisonTable(new List<SimResult> { resB0, resB1, resB2, resB3 });

            // MODEL C: Increasing Red Count & Territory (4 copies of each rank)
            Console.WriteLine("\n--- EXPERIMENT C: EXPANDING RED TERRITORY (4 copies per rank) ---");
            Console.WriteLine("Red cards claim more ranks in the deck (e.g. 1..4 = 16 cards, 1..5 = 20 cards, 1..6 = 24 cards).");
            var resC0 = RunExperimentRanksCount("Baseline (Top 3): 1..3 (12 Red cards)", 3);
            var resC1 = RunExperimentRanksCount("+1 (Top 4): 1..4 (16 Red cards)", 4);
            var resC2 = RunExperimentRanksCount("+2 (Top 5): 1..5 (20 Red cards)", 5);
            var resC3 = RunExperimentRanksCount("+3 (Top 6): 1..6 (24 Red cards)", 6);

            PrintComparisonTable(new List<SimResult> { resC0, resC1, resC2, resC3 });
        }

        public static void PrintComparisonTable(List<SimResult> results)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(string.Format("{0,-40} | {1,10} | {2,12} | {3,12} | {4,10} | {5,10}", 
                "Configuration", "Turns/Plr", "Red Bust %", "Clean Esc %", "Draws/Rd", "Resets/Rd"));
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");
            foreach (var r in results)
            {
                Console.WriteLine(string.Format("{0,-40} | {1,10:F1} | {2,11:F1}% | {3,11:F1}% | {4,10:F1} | {5,10:F1}",
                    r.Label, r.AvgTurnsPerPlayer, r.RedBustRatePercent, r.CleanEscapeRatePercent, r.AvgDrawsPerRound, r.AvgResetsPerRound));
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");
        }

        public static SimResult RunExperiment(string label, int[] redValues)
        {
            // Build deck: 12 red cards, 34 blue cards (values 4..10), 8 resets (0) = 54 cards
            Func<List<Card>> deckBuilder = () =>
            {
                var list = new List<Card>();
                foreach (int v in redValues)
                {
                    list.Add(new Card { Val = v, Item = "Contraband", Color = "RED", Type = "CONTRABAND" });
                }
                // Blue: 34 cards from (MaxRed + 1) to 10
                int minBlue = 4;
                for (int i = 0; i < 34; i++)
                {
                    int val = minBlue + (i % (11 - minBlue));
                    list.Add(new Card { Val = val, Item = "Loot", Color = "BLUE", Type = "CLEAN" });
                }
                // Resets: 8 cards
                for (int i = 0; i < 4; i++) list.Add(new Card { Val = 0, Item = "Fence", Color = "SPECIAL", Type = "RESET" });
                for (int i = 0; i < 2; i++) list.Add(new Card { Val = 0, Item = "Smoke", Color = "SPECIAL", Type = "RESET_REVERSE" });
                for (int i = 0; i < 2; i++) list.Add(new Card { Val = 0, Item = "Snitch", Color = "SPECIAL", Type = "RESET_DRAW" });
                return list;
            };

            return ExecuteSim(label, deckBuilder, 5000);
        }

        public static SimResult RunExperimentRanksCount(string label, int topRedRank)
        {
            // 4 copies of each red rank 1..topRedRank
            Func<List<Card>> deckBuilder = () =>
            {
                var list = new List<Card>();
                for (int r = 1; r <= topRedRank; r++)
                {
                    for (int c = 0; c < 4; c++)
                        list.Add(new Card { Val = r, Item = "Contraband", Color = "RED", Type = "CONTRABAND" });
                }
                int redCount = topRedRank * 4;
                int blueCount = 54 - 8 - redCount; // keep total at 54
                int minBlue = topRedRank + 1;
                for (int i = 0; i < blueCount; i++)
                {
                    int val = minBlue + (i % (11 - minBlue));
                    list.Add(new Card { Val = val, Item = "Loot", Color = "BLUE", Type = "CLEAN" });
                }
                // Resets: 8 cards
                for (int i = 0; i < 4; i++) list.Add(new Card { Val = 0, Item = "Fence", Color = "SPECIAL", Type = "RESET" });
                for (int i = 0; i < 2; i++) list.Add(new Card { Val = 0, Item = "Smoke", Color = "SPECIAL", Type = "RESET_REVERSE" });
                for (int i = 0; i < 2; i++) list.Add(new Card { Val = 0, Item = "Snitch", Color = "SPECIAL", Type = "RESET_DRAW" });
                return list;
            };

            return ExecuteSim(label, deckBuilder, 5000);
        }

        public static SimResult ExecuteSim(string label, Func<List<Card>> deckBuilder, int totalMatches)
        {
            int numPlayers = 4;
            int totalRoundsPlayed = 0;
            int totalTurnsPlayed = 0;
            int totalRedBusts = 0;
            int totalCleanEscapes = 0;
            int totalDraws = 0;
            int totalResetsPlayed = 0;
            int totalRedRemaining = 0;

            Random rng = new Random(42);

            for (int m = 0; m < totalMatches; m++)
            {
                int[] strikes = new int[numPlayers];
                bool[] eliminated = new bool[numPlayers];
                int activePlayersCount = numPlayers;
                int matchRound = 0;

                while (activePlayersCount > 1 && matchRound < 20)
                {
                    matchRound++;
                    totalRoundsPlayed++;

                    List<Card> deck = deckBuilder();
                    Shuffle(deck, rng);

                    List<Card>[] hands = new List<Card>[numPlayers];
                    for (int p = 0; p < numPlayers; p++)
                    {
                        hands[p] = new List<Card>();
                        if (!eliminated[p])
                        {
                            for (int i = 0; i < 7; i++)
                                hands[p].Add(deck[deck.Count - 1]);
                            deck.RemoveRange(deck.Count - 7, 7);
                        }
                    }

                    List<Card> discardPile = new List<Card>();
                    Card startCard = deck[deck.Count - 1];
                    deck.RemoveAt(deck.Count - 1);
                    discardPile.Add(startCard);

                    int current = 0;
                    while (eliminated[current]) current = (current + 1) % numPlayers;
                    int direction = 1;
                    int roundTurns = 0;
                    int winnerIdx = -1;

                    while (roundTurns < 250)
                    {
                        roundTurns++;
                        totalTurnsPlayed++;

                        Card top = discardPile[discardPile.Count - 1];
                        var hand = hands[current];

                        int bestPlayIdx = -1;
                        int bestUrgency = -999;

                        for (int i = 0; i < hand.Count; i++)
                        {
                            Card c = hand[i];
                            bool legal = (c.Val == 0) || (c.Val > top.Val);
                            if (!legal) continue;

                            int urgency = 0;
                            if (c.Color == "RED")
                            {
                                urgency = 100 - c.Val;
                            }
                            else if (c.Color == "SPECIAL")
                            {
                                bool hasRed = hand.Exists(x => x.Color == "RED");
                                urgency = (hasRed && top.Val >= 4) ? 80 : 10;
                            }
                            else
                            {
                                urgency = 50 - c.Val;
                            }

                            if (urgency > bestUrgency)
                            {
                                bestUrgency = urgency;
                                bestPlayIdx = i;
                            }
                        }

                        if (bestPlayIdx != -1)
                        {
                            Card played = hand[bestPlayIdx];
                            hand.RemoveAt(bestPlayIdx);
                            discardPile.Add(played);

                            if (played.Val == 0) totalResetsPlayed++;

                            if (played.Type == "RESET_REVERSE") direction = -direction;
                            else if (played.Type == "RESET_DRAW")
                            {
                                int victim = current;
                                do {
                                    victim = (victim + direction + numPlayers) % numPlayers;
                                } while (eliminated[victim] && victim != current);

                                if (deck.Count > 0)
                                {
                                    hands[victim].Add(deck[deck.Count - 1]);
                                    deck.RemoveAt(deck.Count - 1);
                                }
                            }

                            if (hand.Count == 0)
                            {
                                winnerIdx = current;
                                break;
                            }
                        }
                        else
                        {
                            totalDraws++;
                            if (deck.Count == 0 && discardPile.Count > 1)
                            {
                                Card topCard = discardPile[discardPile.Count - 1];
                                discardPile.RemoveAt(discardPile.Count - 1);
                                deck.AddRange(discardPile);
                                discardPile.Clear();
                                discardPile.Add(topCard);
                                Shuffle(deck, rng);
                            }

                            if (deck.Count > 0)
                            {
                                Card drawn = deck[deck.Count - 1];
                                deck.RemoveAt(deck.Count - 1);
                                hand.Add(drawn);

                                if ((drawn.Val == 0) || (drawn.Val > top.Val))
                                {
                                    hand.RemoveAt(hand.Count - 1);
                                    discardPile.Add(drawn);
                                    if (drawn.Val == 0) totalResetsPlayed++;
                                    if (hand.Count == 0)
                                    {
                                        winnerIdx = current;
                                        break;
                                    }
                                }
                            }
                        }

                        do {
                            current = (current + direction + numPlayers) % numPlayers;
                        } while (eliminated[current] && current != winnerIdx);
                    }

                    // Score round
                    for (int p = 0; p < numPlayers; p++)
                    {
                        if (eliminated[p]) continue;
                        if (p == winnerIdx) continue;

                        int redCount = hands[p].FindAll(c => c.Color == "RED").Count;
                        totalRedRemaining += redCount;
                        if (redCount > 0)
                        {
                            totalRedBusts++;
                            strikes[p]++;
                            if (strikes[p] >= 3)
                            {
                                eliminated[p] = true;
                                activePlayersCount--;
                            }
                        }
                        else
                        {
                            totalCleanEscapes++;
                        }
                    }
                }
            }

            double totalNonWinners = totalRedBusts + totalCleanEscapes;
            return new SimResult
            {
                Label = label,
                Matches = totalMatches,
                AvgRoundsPerMatch = (double)totalRoundsPlayed / totalMatches,
                AvgTableTurnsPerRound = (double)totalTurnsPlayed / totalRoundsPlayed,
                AvgTurnsPerPlayer = ((double)totalTurnsPlayed / totalRoundsPlayed) / numPlayers,
                RedBustRatePercent = 100.0 * totalRedBusts / totalNonWinners,
                CleanEscapeRatePercent = 100.0 * totalCleanEscapes / totalNonWinners,
                AvgDrawsPerRound = (double)totalDraws / totalRoundsPlayed,
                AvgResetsPerRound = (double)totalResetsPlayed / totalRoundsPlayed,
                AvgRedCardsLeftInHands = (double)totalRedRemaining / totalNonWinners
            };
        }

        public static void Shuffle(List<Card> list, Random rng)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}
