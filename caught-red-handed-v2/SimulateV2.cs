using System;
using System.Collections.Generic;

namespace CaughtRedHandedV2Sim
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
            Console.WriteLine("CAUGHT RED-HANDED V2: THE HEAT — 5,000 MATCH MONTE CARLO SIMULATION");
            Console.WriteLine("======================================================================");

            RunSimulation(4, 5000);
        }

        public static void RunSimulation(int numPlayers, int totalMatches)
        {
            int totalRoundsPlayed = 0;
            int totalTurnsPlayed = 0;
            int[] matchWins = new int[numPlayers];
            int totalStrikesGiven = 0;
            int totalCleanEscapes = 0;
            int totalRedBusts = 0;
            int totalDraws = 0;
            int totalResetsPlayed = 0;

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

                    // Build and shuffle deck
                    List<Card> deck = BuildDeck();
                    Shuffle(deck, rng);

                    // Hands
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

                    while (roundTurns < 200)
                    {
                        roundTurns++;
                        totalTurnsPlayed++;

                        Card top = discardPile[discardPile.Count - 1];
                        var hand = hands[current];

                        // Find playable
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
                                // Highest priority: dump red cards!
                                urgency = 100 - c.Val;
                            }
                            else if (c.Color == "SPECIAL")
                            {
                                // Use reset if holding red cards and top is high
                                bool hasRed = hand.Exists(x => x.Color == "RED");
                                urgency = (hasRed && top.Val >= 4) ? 80 : 10;
                            }
                            else
                            {
                                // Blue: play lowest legal
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

                            if (played.Type == "RESET_REVERSE")
                            {
                                direction = -direction;
                            }
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
                            // Must draw
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

                                // If playable, play it immediately
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

                        // Advance
                        do {
                            current = (current + direction + numPlayers) % numPlayers;
                        } while (eliminated[current] && current != winnerIdx);
                    }

                    // Round end resolution
                    for (int p = 0; p < numPlayers; p++)
                    {
                        if (eliminated[p]) continue;
                        if (p == winnerIdx) continue;

                        int redCount = hands[p].FindAll(c => c.Color == "RED").Count;
                        if (redCount > 0)
                        {
                            totalRedBusts++;
                            totalStrikesGiven++;
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

                // Match winner
                for (int p = 0; p < numPlayers; p++)
                {
                    if (!eliminated[p])
                    {
                        matchWins[p]++;
                        break;
                    }
                }
            }

            Console.WriteLine(string.Format("  Total Matches Simulated:      {0:N0}", totalMatches));
            Console.WriteLine(string.Format("  Average Rounds / Match:       {0:F2} rounds", (double)totalRoundsPlayed / totalMatches));
            Console.WriteLine(string.Format("  Average Turns / Round:        {0:F1} table turns ({1:F1} turns/player)", 
                (double)totalTurnsPlayed / totalRoundsPlayed, 
                ((double)totalTurnsPlayed / totalRoundsPlayed) / numPlayers));
            Console.WriteLine(string.Format("  Resets (Value 0) per Round:   {0:F2} resets", (double)totalResetsPlayed / totalRoundsPlayed));
            Console.WriteLine(string.Format("  Draws per Round:              {0:F2} card draws", (double)totalDraws / totalRoundsPlayed));
            Console.WriteLine(string.Format("  Red Contraband Bust Rate:     {0:F1}% of non-winners bust with Red cards", 
                100.0 * totalRedBusts / (totalRedBusts + totalCleanEscapes)));
            Console.WriteLine(string.Format("  Clean Escapes (Held 0 Red):   {0:F1}% of non-winners avoided strikes", 
                100.0 * totalCleanEscapes / (totalRedBusts + totalCleanEscapes)));
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
            // Red (12 cards)
            for (int i = 0; i < 4; i++) list.Add(new Card { Val = 1, Item = "Pickpocket", Color = "RED", Type = "CONTRABAND" });
            for (int i = 0; i < 4; i++) list.Add(new Card { Val = 2, Item = "Hot Jewelry", Color = "RED", Type = "CONTRABAND" });
            for (int i = 0; i < 4; i++) list.Add(new Card { Val = 3, Item = "Blood Diamond", Color = "RED", Type = "CONTRABAND" });

            // Blue (34 cards)
            for (int i = 0; i < 5; i++) list.Add(new Card { Val = 4, Item = "Cash", Color = "BLUE", Type = "CLEAN" });
            for (int i = 0; i < 5; i++) list.Add(new Card { Val = 5, Item = "Key", Color = "BLUE", Type = "CLEAN" });
            for (int i = 0; i < 5; i++) list.Add(new Card { Val = 6, Item = "Painting", Color = "BLUE", Type = "CLEAN" });
            for (int i = 0; i < 5; i++) list.Add(new Card { Val = 7, Item = "Gold", Color = "BLUE", Type = "CLEAN" });
            for (int i = 0; i < 5; i++) list.Add(new Card { Val = 8, Item = "Crown", Color = "BLUE", Type = "CLEAN" });
            for (int i = 0; i < 5; i++) list.Add(new Card { Val = 9, Item = "Relic", Color = "BLUE", Type = "CLEAN" });
            for (int i = 0; i < 4; i++) list.Add(new Card { Val = 10, Item = "Artifact", Color = "BLUE", Type = "CLEAN" });

            // Resets (8 cards)
            for (int i = 0; i < 4; i++) list.Add(new Card { Val = 0, Item = "The Fence", Color = "SPECIAL", Type = "RESET" });
            for (int i = 0; i < 2; i++) list.Add(new Card { Val = 0, Item = "Smoke Bomb", Color = "SPECIAL", Type = "RESET_REVERSE" });
            for (int i = 0; i < 2; i++) list.Add(new Card { Val = 0, Item = "The Snitch", Color = "SPECIAL", Type = "RESET_DRAW" });

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
