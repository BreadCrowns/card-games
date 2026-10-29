using System;
using System.Collections.Generic;

namespace Option3Sim
{
    public class Card
    {
        public int Val;
        public string Item;
        public string Color; // RED, BLUE, SPECIAL
        public string Type;  // CONTRABAND, CLEAN, RESET, RESET_REVERSE, RESET_DRAW
    }

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("======================================================================");
            Console.WriteLine("OPTION 3 SIMULATION: HIGHER NUMBER OR MATCH ITEM (5,000 Matches)");
            Console.WriteLine("======================================================================");

            RunSim(4, 5000);
        }

        public static void RunSim(int numPlayers, int totalMatches)
        {
            int totalRounds = 0;
            int totalTurns = 0;
            int totalItemMatches = 0;
            int totalNumberClimbs = 0;
            int totalResetsPlayed = 0;
            int totalDraws = 0;
            int totalRedBusts = 0;
            int totalCleanEscapes = 0;
            int[] matchWins = new int[numPlayers];

            Random rng = new Random(42);

            for (int m = 0; m < totalMatches; m++)
            {
                int[] strikes = new int[numPlayers];
                bool[] eliminated = new bool[numPlayers];
                int activeCount = numPlayers;
                int matchRound = 0;

                while (activeCount > 1 && matchRound < 20)
                {
                    matchRound++;
                    totalRounds++;

                    var deck = BuildDeck();
                    Shuffle(deck, rng);

                    var hands = new List<Card>[numPlayers];
                    for (int p = 0; p < numPlayers; p++)
                    {
                        hands[p] = new List<Card>();
                        if (!eliminated[p])
                        {
                            for (int i = 0; i < 7; i++) hands[p].Add(deck[deck.Count - 1]);
                            deck.RemoveRange(deck.Count - 7, 7);
                        }
                    }

                    var discardPile = new List<Card>();
                    Card startCard = deck[deck.Count - 1];
                    deck.RemoveAt(deck.Count - 1);
                    discardPile.Add(startCard);

                    int current = 0;
                    while (eliminated[current]) current = (current + 1) % numPlayers;
                    int direction = 1;
                    int roundTurns = 0;
                    int winnerIdx = -1;

                    while (roundTurns < 200)
                    {
                        roundTurns++;
                        totalTurns++;

                        Card top = discardPile[discardPile.Count - 1];
                        var hand = hands[current];

                        // Find all legal plays
                        // Condition A: Val > top.Val
                        // Condition B: Item == top.Item (Item Bridge!)
                        // Condition C: Special Reset (Val == 0)
                        int bestIdx = -1;
                        int bestUrgency = -999;
                        string playMethod = "";

                        for (int i = 0; i < hand.Count; i++)
                        {
                            Card c = hand[i];
                            bool isReset = (c.Val == 0);
                            bool isHigher = (c.Val > top.Val);
                            bool isItemMatch = (c.Item == top.Item && top.Item != "Wild");

                            if (!isReset && !isHigher && !isItemMatch) continue;

                            int urgency = 0;
                            if (c.Color == "RED")
                            {
                                // Top priority: Dump red contraband!
                                urgency = 100 - c.Val;
                            }
                            else if (isReset)
                            {
                                bool hasRed = hand.Exists(x => x.Color == "RED");
                                urgency = hasRed ? 70 : 15;
                            }
                            else
                            {
                                // Clean Blue: if bridging via item match, prefer dumping lowest blue
                                if (isItemMatch && c.Val <= top.Val)
                                {
                                    urgency = 40 - c.Val;
                                }
                                else
                                {
                                    urgency = 30 - c.Val;
                                }
                            }

                            if (urgency > bestUrgency)
                            {
                                bestUrgency = urgency;
                                bestIdx = i;
                                playMethod = isItemMatch ? "ITEM" : (isHigher ? "NUMBER" : "RESET");
                            }
                        }

                        if (bestIdx != -1)
                        {
                            Card played = hand[bestIdx];
                            hand.RemoveAt(bestIdx);
                            discardPile.Add(played);

                            if (playMethod == "ITEM") totalItemMatches++;
                            else if (playMethod == "NUMBER") totalNumberClimbs++;
                            else if (playMethod == "RESET") totalResetsPlayed++;

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

                                bool isReset = (drawn.Val == 0);
                                bool isHigher = (drawn.Val > top.Val);
                                bool isItemMatch = (drawn.Item == top.Item && top.Item != "Wild");

                                if (isReset || isHigher || isItemMatch)
                                {
                                    hand.RemoveAt(hand.Count - 1);
                                    discardPile.Add(drawn);
                                    if (isItemMatch) totalItemMatches++;
                                    else if (isHigher) totalNumberClimbs++;
                                    else if (isReset) totalResetsPlayed++;

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
                        if (eliminated[p] || p == winnerIdx) continue;
                        int redCount = hands[p].FindAll(c => c.Color == "RED").Count;
                        if (redCount > 0)
                        {
                            totalRedBusts++;
                            strikes[p]++;
                            if (strikes[p] >= 3)
                            {
                                eliminated[p] = true;
                                activeCount--;
                            }
                        }
                        else
                        {
                            totalCleanEscapes++;
                        }
                    }
                }

                for (int p = 0; p < numPlayers; p++)
                {
                    if (!eliminated[p]) { matchWins[p]++; break; }
                }
            }

            double totalNonWinners = totalRedBusts + totalCleanEscapes;
            Console.WriteLine(string.Format("  Avg Rounds / Match:           {0:F2} rounds", (double)totalRounds / totalMatches));
            Console.WriteLine(string.Format("  Avg Table Turns / Round:      {0:F1} turns ({1:F1} turns/player)", 
                (double)totalTurns / totalRounds, ((double)totalTurns / totalRounds) / numPlayers));
            Console.WriteLine(string.Format("  Play Method Breakdown:"));
            Console.WriteLine(string.Format("    • Number Climbs (> X):      {0:F1}%", 100.0 * totalNumberClimbs / (totalNumberClimbs + totalItemMatches + totalResetsPlayed)));
            Console.WriteLine(string.Format("    • Item Matches (Bridges):   {0:F1}%", 100.0 * totalItemMatches / (totalNumberClimbs + totalItemMatches + totalResetsPlayed)));
            Console.WriteLine(string.Format("    • Reset Cards (Val 0):      {0:F1}%", 100.0 * totalResetsPlayed / (totalNumberClimbs + totalItemMatches + totalResetsPlayed)));
            Console.WriteLine(string.Format("  Draws per Round:              {0:F2} draws (down from 54+ draws!)", (double)totalDraws / totalRounds));
            Console.WriteLine(string.Format("  🚨 Red Bust Rate:              {0:F1}% of non-winners caught with Red cards", 100.0 * totalRedBusts / totalNonWinners));
            Console.WriteLine(string.Format("  🛡️ Clean Escape Rate:          {0:F1}% avoided strikes by dumping all Red cards", 100.0 * totalCleanEscapes / totalNonWinners));
            Console.WriteLine(string.Format("  Seat Balance (Win Rate S1-S4):S1: {0:F1}% | S2: {1:F1}% | S3: {2:F1}% | S4: {3:F1}%",
                100.0 * matchWins[0] / totalMatches,
                100.0 * matchWins[1] / totalMatches,
                100.0 * matchWins[2] / totalMatches,
                100.0 * matchWins[3] / totalMatches));
            Console.WriteLine("======================================================================");
        }

        public static List<Card> BuildDeck()
        {
            var list = new List<Card>();
            string[] items = new string[] { "Cash", "Diamond", "Watch", "Key", "Art", "Gold" };

            // 6 Items x 8 cards = 48 cards
            foreach (var it in items)
            {
                // Red: 1, 2
                list.Add(new Card { Val = 1, Item = it, Color = "RED", Type = "CONTRABAND" });
                list.Add(new Card { Val = 2, Item = it, Color = "RED", Type = "CONTRABAND" });

                // Blue: 3, 4, 5, 6, 7, 8
                for (int v = 3; v <= 8; v++)
                {
                    list.Add(new Card { Val = v, Item = it, Color = "BLUE", Type = "CLEAN" });
                }
            }

            // 6 Special Resets
            list.Add(new Card { Val = 0, Item = "Wild", Color = "SPECIAL", Type = "RESET" });
            list.Add(new Card { Val = 0, Item = "Wild", Color = "SPECIAL", Type = "RESET" });
            list.Add(new Card { Val = 0, Item = "Wild", Color = "SPECIAL", Type = "RESET_REVERSE" });
            list.Add(new Card { Val = 0, Item = "Wild", Color = "SPECIAL", Type = "RESET_REVERSE" });
            list.Add(new Card { Val = 0, Item = "Wild", Color = "SPECIAL", Type = "RESET_DRAW" });
            list.Add(new Card { Val = 0, Item = "Wild", Color = "SPECIAL", Type = "RESET_DRAW" });

            return list;
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
