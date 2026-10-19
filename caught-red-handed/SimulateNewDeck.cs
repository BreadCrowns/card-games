using System;
using System.Collections.Generic;
using System.Linq;

namespace CaughtRedHandedSimulation
{
    public class Card
    {
        public string Item;
        public string Color; // BLUE, RED, GREEN
        public int Val;
        public bool IsGrand;
        public bool IsWild;

        public Card(string item, string color, int val, bool isGrand, bool isWild)
        {
            Item = item;
            Color = color;
            Val = val;
            IsGrand = isGrand;
            IsWild = isWild;
        }
    }

    public class PairResult
    {
        public Card C1;
        public Card C2;
        public int Points;
        public bool FlipsCamera;
    }

    public class RoundStats
    {
        public bool GetawayEscape;
        public bool Standoff;
        public int Turns;
        public int CameraFlips;
        public int InterceptCount;
        public int CardsLeftInDeck;
        public int BustedPlayerCount;
        public int EvadedPlayerCount;
        public int CleanPlayerCount;
        public int WinnerSeat;
        public int[] FinalScores = new int[4];
        public int[] RedCounts = new int[4];
    }

    class Program
    {
        static Random rng = new Random(42);

        static List<Card> BuildDeck(bool is2P)
        {
            var deck = new List<Card>();

            string[] pettyItems = new string[] { "Cash", "Diamond", "Watch", "Key" };
            string[] grandItems = new string[] { "Painting", "Gold", "Crown", "Relic" };

            // Petty Blue (4 copies each = 16)
            foreach (var item in pettyItems)
            {
                for (int i = 0; i < 4; i++)
                    deck.Add(new Card(item, "BLUE", 5, false, false));
            }

            // Petty Red (2 copies each = 8)
            foreach (var item in pettyItems)
            {
                for (int i = 0; i < 2; i++)
                    deck.Add(new Card(item, "RED", 10, false, false));
            }

            if (!is2P)
            {
                // Grand Blue: Painting 4, Gold 4, Crown 3, Relic 3 = 14
                for (int i = 0; i < 4; i++) deck.Add(new Card("Painting", "BLUE", 5, true, false));
                for (int i = 0; i < 4; i++) deck.Add(new Card("Gold", "BLUE", 5, true, false));
                for (int i = 0; i < 3; i++) deck.Add(new Card("Crown", "BLUE", 5, true, false));
                for (int i = 0; i < 3; i++) deck.Add(new Card("Relic", "BLUE", 5, true, false));

                // Grand Red (2 copies each = 8)
                foreach (var item in grandItems)
                {
                    for (int i = 0; i < 2; i++)
                        deck.Add(new Card(item, "RED", 15, true, false));
                }
            }

            // Green Wilds: 8 in 4P (54 total), 4 in 2P (28 total)
            int wildCount = is2P ? 4 : 8;
            for (int i = 0; i < wildCount; i++)
            {
                deck.Add(new Card("Wild", "GREEN", 0, false, true));
            }

            // Shuffle Fisher-Yates
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                var temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }

            return deck;
        }

        static PairResult FindBestMeld(List<Card> hand)
        {
            var blues = hand.Where(c => c.Color == "BLUE").ToList();
            if (blues.Count == 0) return null;

            var reds = hand.Where(c => c.Color == "RED").ToList();
            var greens = hand.Where(c => c.Color == "GREEN").ToList();

            var candidates = new List<PairResult>();

            // 1. Red + Blue
            foreach (var r in reds)
            {
                var matchBlue = blues.FirstOrDefault(b => b.Item == r.Item);
                if (matchBlue != null)
                {
                    candidates.Add(new PairResult
                    {
                        C1 = r,
                        C2 = matchBlue,
                        Points = matchBlue.Val + r.Val,
                        FlipsCamera = false
                    });
                }
            }

            // 2. Blue + Blue
            var blueGroups = blues.GroupBy(b => b.Item);
            foreach (var grp in blueGroups)
            {
                var list = grp.ToList();
                if (list.Count >= 2)
                {
                    candidates.Add(new PairResult
                    {
                        C1 = list[0],
                        C2 = list[1],
                        Points = 10,
                        FlipsCamera = false
                    });
                }
            }

            // 3. Green Wild + Blue
            if (greens.Count > 0 && blues.Count > 0)
            {
                candidates.Add(new PairResult
                {
                    C1 = greens[0],
                    C2 = blues[0],
                    Points = 5,
                    FlipsCamera = true
                });
            }

            if (candidates.Count == 0) return null;

            // Prioritize highest points (Red 20/15 > Blue 10 > Wild 5)
            return candidates.OrderByDescending(c => c.Points).First();
        }

        static int ChooseCardToPass(List<Card> hand, int minOpponentHand)
        {
            int bestIdx = 0;
            double maxScore = -9999;

            for (int i = 0; i < hand.Count; i++)
            {
                var c = hand[i];
                double score = 0;

                if (c.Color == "RED")
                {
                    score += 50;
                    if (minOpponentHand <= 3) score += (4 - minOpponentHand) * 30;
                    score += (c.Val == 15 ? 15 : 5);
                }
                else if (c.Color == "GREEN")
                {
                    score += 25;
                    int blues = hand.Count(x => x.Color == "BLUE");
                    if (blues == 0) score += 20;
                }
                else if (c.Color == "BLUE")
                {
                    score += 15;
                    int sameItemBlues = hand.Count(x => x.Color == "BLUE" && x.Item == c.Item);
                    if (sameItemBlues >= 2) score -= 35;
                }

                if (score > maxScore)
                {
                    maxScore = score;
                    bestIdx = i;
                }
            }

            return bestIdx;
        }

        static RoundStats SimulateRound(bool is2P, int starter)
        {
            int numPlayers = is2P ? 2 : 4;
            var deck = BuildDeck(is2P);

            var hands = new List<List<Card>>();
            var stashes = new List<List<int>>();
            for (int p = 0; p < numPlayers; p++)
            {
                hands.Add(new List<Card>());
                stashes.Add(new List<int>());
            }

            // Deal 7 cards each
            for (int i = 0; i < 7; i++)
            {
                for (int p = 0; p < numPlayers; p++)
                {
                    var card = deck[deck.Count - 1];
                    deck.RemoveAt(deck.Count - 1);
                    hands[p].Add(card);
                }
            }

            int currPlayer = starter % numPlayers;
            int direction = 1; // 1 = clockwise, -1 = counter-clockwise
            int turns = 0;
            int consecutiveNoMeld = 0;
            int cameraFlips = 0;
            int interceptCount = 0;
            int winner = -1;
            bool getaway = false;
            bool standoff = false;

            while (true)
            {
                turns++;

                // 1. Draw
                if (deck.Count > 0)
                {
                    var drawn = deck[deck.Count - 1];
                    deck.RemoveAt(deck.Count - 1);
                    hands[currPlayer].Add(drawn);
                }

                // Check escape
                if (hands[currPlayer].Count == 0)
                {
                    winner = currPlayer;
                    getaway = true;
                    break;
                }

                // 2. Meld
                var bestMeld = FindBestMeld(hands[currPlayer]);
                if (bestMeld != null)
                {
                    hands[currPlayer].Remove(bestMeld.C1);
                    hands[currPlayer].Remove(bestMeld.C2);
                    stashes[currPlayer].Add(bestMeld.Points);

                    if (bestMeld.FlipsCamera)
                    {
                        direction = -direction;
                        cameraFlips++;
                    }
                    consecutiveNoMeld = 0;
                }
                else
                {
                    consecutiveNoMeld++;
                }

                // Check escape after meld
                if (hands[currPlayer].Count == 0)
                {
                    winner = currPlayer;
                    getaway = true;
                    break;
                }

                // Standoff check: deck dry and full rotation without meld
                if (deck.Count == 0 && consecutiveNoMeld >= numPlayers)
                {
                    standoff = true;
                    break;
                }

                // 3. Pass
                if (hands[currPlayer].Count > 0)
                {
                    int targetP = (is2P)
                        ? 1 - currPlayer
                        : (currPlayer + direction + 4) % 4;

                    int minOpp = hands.Where((h, idx) => idx != currPlayer).Min(h => h.Count);
                    int passIdx = ChooseCardToPass(hands[currPlayer], minOpp);

                    var passed = hands[currPlayer][passIdx];
                    hands[currPlayer].RemoveAt(passIdx);

                    // RECIPIENT ADVANTAGE (THE INTERCEPT):
                    // If passed card is RED, check if targetP holds matching BLUE card:
                    Card matchBlue = null;
                    if (passed.Color == "RED")
                    {
                        matchBlue = hands[targetP].FirstOrDefault(c => c.Color == "BLUE" && c.Item == passed.Item);
                    }

                    if (matchBlue != null)
                    {
                        // Target intercepts!
                        hands[targetP].Remove(matchBlue);
                        stashes[targetP].Add(matchBlue.Val + passed.Val);
                        interceptCount++;
                        consecutiveNoMeld = 0;

                        // Check if target emptied hand by intercepting!
                        if (hands[targetP].Count == 0)
                        {
                            winner = targetP;
                            getaway = true;
                            break;
                        }
                    }
                    else
                    {
                        hands[targetP].Add(passed);
                    }
                }

                // Check escape after pass
                if (hands[currPlayer].Count == 0)
                {
                    winner = currPlayer;
                    getaway = true;
                    break;
                }

                // Standoff check
                if (deck.Count == 0 && consecutiveNoMeld >= numPlayers)
                {
                    standoff = true;
                    break;
                }

                // Advance
                if (is2P)
                {
                    currPlayer = 1 - currPlayer;
                }
                else
                {
                    currPlayer = (currPlayer + direction + 4) % 4;
                }

                if (turns > 300)
                {
                    standoff = true;
                    break;
                }
            }

            var stats = new RoundStats();
            stats.GetawayEscape = getaway;
            stats.Standoff = standoff;
            stats.Turns = turns;
            stats.CameraFlips = cameraFlips;
            stats.InterceptCount = interceptCount;
            stats.CardsLeftInDeck = deck.Count;
            stats.WinnerSeat = winner;

            // Fall Guy Scoring check
            int[] redCounts = new int[numPlayers];
            for (int p = 0; p < numPlayers; p++)
            {
                redCounts[p] = hands[p].Count(c => c.Color == "RED");
                stats.RedCounts[p] = redCounts[p];
            }

            int maxRed = redCounts.Max();

            int busted = 0;
            int evaded = 0;
            int clean = 0;

            for (int p = 0; p < numPlayers; p++)
            {
                int rCount = redCounts[p];
                int stashPts = stashes[p].Sum();

                bool isCaught = (rCount == maxRed && maxRed > 0);

                if (isCaught)
                {
                    stats.FinalScores[p] = 0;
                    busted++;
                }
                else
                {
                    stats.FinalScores[p] = stashPts;
                    if (rCount > 0) evaded++;
                    else clean++;
                }
            }

            stats.BustedPlayerCount = busted;
            stats.EvadedPlayerCount = evaded;
            stats.CleanPlayerCount = clean;

            return stats;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("======================================================================");
            Console.WriteLine("    CAUGHT RED-HANDED: MONTE CARLO SIMULATION (5,000 ROUNDS)");
            Console.WriteLine("    Deck: 54 Play Cards (30 Blue, 16 Red, 8 Wilds)");
            Console.WriteLine("    Mechanic: Face-Up Passing & Recipient Advantage (The Intercept)");
            Console.WriteLine("    Penalty Rule: Only player(s) with MOST Red Contraband bust (0 pts)");
            Console.WriteLine("======================================================================\n");

            int numRounds = 5000;

            // --- 4-PLAYER HEIST ---
            int escapes4P = 0;
            int standoffs4P = 0;
            long totalTurns4P = 0;
            long totalFlips4P = 0;
            long totalIntercepts4P = 0;
            long totalCardsLeft4P = 0;
            long totalBusted4P = 0;
            long totalEvaded4P = 0;
            long totalClean4P = 0;
            long totalScoreSurvivor4P = 0;
            long totalSurvivors4P = 0;
            int[] seatEscapes4P = new int[4];

            for (int i = 0; i < numRounds; i++)
            {
                int starter = i % 4;
                var res = SimulateRound(false, starter);

                if (res.GetawayEscape) escapes4P++;
                if (res.Standoff) standoffs4P++;
                totalTurns4P += res.Turns;
                totalFlips4P += res.CameraFlips;
                totalIntercepts4P += res.InterceptCount;
                totalCardsLeft4P += res.CardsLeftInDeck;
                totalBusted4P += res.BustedPlayerCount;
                totalEvaded4P += res.EvadedPlayerCount;
                totalClean4P += res.CleanPlayerCount;

                if (res.WinnerSeat >= 0 && res.WinnerSeat < 4)
                {
                    seatEscapes4P[res.WinnerSeat]++;
                }

                for (int p = 0; p < 4; p++)
                {
                    if (res.FinalScores[p] > 0)
                    {
                        totalScoreSurvivor4P += res.FinalScores[p];
                        totalSurvivors4P++;
                    }
                }
            }

            double avgTurns4P = (double)totalTurns4P / numRounds;
            double avgFlips4P = (double)totalFlips4P / numRounds;
            double avgIntercepts4P = (double)totalIntercepts4P / numRounds;
            double escapePct4P = (double)escapes4P / numRounds * 100.0;
            double standoffPct4P = (double)standoffs4P / numRounds * 100.0;
            double avgCardsLeft4P = (double)totalCardsLeft4P / numRounds;
            double avgBusted4P = (double)totalBusted4P / numRounds;
            double avgEvaded4P = (double)totalEvaded4P / numRounds;
            double avgClean4P = (double)totalClean4P / numRounds;
            double avgScoreSurvivor4P = totalSurvivors4P > 0 ? (double)totalScoreSurvivor4P / totalSurvivors4P : 0;

            Console.WriteLine(">>> 4-PLAYER HEIST (54 Cards, Target 300 pts) <<<");
            Console.WriteLine(string.Format("• Total Simulated Rounds:      {0:N0}", numRounds));
            Console.WriteLine(string.Format("• Getaway Escapes (Empty Hand):{0:F2}% (Natural win)", escapePct4P));
            Console.WriteLine(string.Format("• Empty Locker Standoffs:      {0:F2}% (Dry deck resolution)", standoffPct4P));
            Console.WriteLine(string.Format("• Average Turns per Round:     {0:F1} turns ({1:F1} turns/player)", avgTurns4P, avgTurns4P / 4.0));
            Console.WriteLine(string.Format("• Average Cards Left in Deck:  {0:F1} cards remaining", avgCardsLeft4P));
            Console.WriteLine(string.Format("• Camera Flips per Round:      {0:F2} flips (Active direction churn)", avgFlips4P));
            Console.WriteLine(string.Format("• ⚡ INTERCEPTS PER ROUND:       {0:F2} intercepts (Snap matches on face-up pass!)", avgIntercepts4P));

            Console.WriteLine("\n>>> 🚨 FALL GUY & ARREST OUTCOMES <<<");
            Console.WriteLine(string.Format("• Average Busted Players:      {0:F2} players / round (Took the fall for most red)", avgBusted4P));
            Console.WriteLine(string.Format("• Average Evaded Players:      {0:F2} players / round (Held red, but avoided bust!)", avgEvaded4P));
            Console.WriteLine(string.Format("• Average Clean Players:       {0:F2} players / round (Zero contraband held)", avgClean4P));
            Console.WriteLine(string.Format("• Avg Stash Points (Survivors):{0:F1} pts / round", avgScoreSurvivor4P));

            Console.WriteLine("\n>>> 🪑 SEAT POSITION BALANCE (First-Player Bias Check) <<<");
            for (int s = 0; s < 4; s++)
            {
                double pct = escapes4P > 0 ? (double)seatEscapes4P[s] / escapes4P * 100.0 : 0;
                Console.WriteLine(string.Format("• Seat {0} Escape Rate:         {1:F2}% of getaway escapes", s + 1, pct));
            }

            // --- 2-PLAYER DUEL C1 ---
            int escapes2P = 0;
            int standoffs2P = 0;
            long totalTurns2P = 0;
            long totalIntercepts2P = 0;

            for (int i = 0; i < numRounds; i++)
            {
                int starter = i % 2;
                var res = SimulateRound(true, starter);

                if (res.GetawayEscape) escapes2P++;
                if (res.Standoff) standoffs2P++;
                totalTurns2P += res.Turns;
                totalIntercepts2P += res.InterceptCount;
            }

            double avgTurns2P = (double)totalTurns2P / numRounds;
            double escapePct2P = (double)escapes2P / numRounds * 100.0;
            double standoffPct2P = (double)standoffs2P / numRounds * 100.0;
            double avgIntercepts2P = (double)totalIntercepts2P / numRounds;

            Console.WriteLine("\n>>> ⚔️ 2-PLAYER DUEL C1 (28 Cards, Target 200 pts) <<<");
            Console.WriteLine(string.Format("• Getaway Escapes:             {0:F2}%", escapePct2P));
            Console.WriteLine(string.Format("• Standoff Rate:               {0:F2}%", standoffPct2P));
            Console.WriteLine(string.Format("• Average Turns per Round:     {0:F1} turns ({1:F1} turns/player)", avgTurns2P, avgTurns2P / 2.0));
            Console.WriteLine(string.Format("• ⚡ Intercepts per Duel Round:  {0:F2} intercepts", avgIntercepts2P));

            Console.WriteLine("\nSimulation complete.\n");
        }
    }
}
