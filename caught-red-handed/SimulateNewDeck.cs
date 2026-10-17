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
        public bool IsArtifact;
        public bool IsWild;

        public Card(string item, string color, int val, bool isGrand, bool isArtifact, bool isWild)
        {
            Item = item;
            Color = color;
            Val = val;
            IsGrand = isGrand;
            IsArtifact = isArtifact;
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
        public int ArtifactPasses;
        public bool ArtifactHolderBusted;
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
                    deck.Add(new Card(item, "BLUE", 5, false, false, false));
            }

            // Petty Red (2 copies each = 8)
            foreach (var item in pettyItems)
            {
                for (int i = 0; i < 2; i++)
                    deck.Add(new Card(item, "RED", 10, false, false, false));
            }

            if (!is2P)
            {
                // Grand Blue: Painting 4, Gold 4, Crown 3, Relic 3 = 14
                for (int i = 0; i < 4; i++) deck.Add(new Card("Painting", "BLUE", 5, true, false, false));
                for (int i = 0; i < 4; i++) deck.Add(new Card("Gold", "BLUE", 5, true, false, false));
                for (int i = 0; i < 3; i++) deck.Add(new Card("Crown", "BLUE", 5, true, false, false));
                for (int i = 0; i < 3; i++) deck.Add(new Card("Relic", "BLUE", 5, true, false, false));

                // Grand Red (2 copies each = 8)
                foreach (var item in grandItems)
                {
                    for (int i = 0; i < 2; i++)
                        deck.Add(new Card(item, "RED", 15, true, false, false));
                }
            }

            // Priceless Artifact (Red Contraband, unpairable)
            deck.Add(new Card("Artifact", "RED", 0, true, true, false));

            // Green Wilds: 8 in 4P, 4 in 2P
            int wildCount = is2P ? 4 : 8;
            for (int i = 0; i < wildCount; i++)
            {
                deck.Add(new Card("Wild", "GREEN", 0, false, false, true));
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

            var reds = hand.Where(c => c.Color == "RED" && !c.IsArtifact).ToList();
            var greens = hand.Where(c => c.Color == "GREEN").ToList();

            // 1. Red + Matching Blue (highest points: 20 or 15)
            // Prioritize Grand Red (20 pts) then Petty Red (15 pts)
            var redPairs = new List<PairResult>();
            foreach (var r in reds)
            {
                var matchingBlue = blues.FirstOrDefault(b => b.Item == r.Item);
                if (matchingBlue != null)
                {
                    redPairs.Add(new PairResult
                    {
                        C1 = r,
                        C2 = matchingBlue,
                        Points = r.Val + matchingBlue.Val,
                        FlipsCamera = false
                    });
                }
            }
            if (redPairs.Count > 0)
            {
                return redPairs.OrderByDescending(p => p.Points).First();
            }

            // 2. Blue + Blue (10 pts)
            var blueGroups = blues.GroupBy(b => b.Item).Where(g => g.Count() >= 2).ToList();
            if (blueGroups.Count > 0)
            {
                var grp = blueGroups.First().ToList();
                return new PairResult
                {
                    C1 = grp[0],
                    C2 = grp[1],
                    Points = 10,
                    FlipsCamera = false
                };
            }

            // 3. Green Wild + Any Blue (5 pts & flips camera)
            if (greens.Count > 0 && blues.Count > 0)
            {
                return new PairResult
                {
                    C1 = greens[0],
                    C2 = blues[0],
                    Points = 5,
                    FlipsCamera = true
                };
            }

            return null;
        }

        static int ChooseCardToPass(List<Card> hand, int minOppHand)
        {
            int bestIdx = 0;
            double maxUrgency = -9999;

            for (int i = 0; i < hand.Count; i++)
            {
                var c = hand[i];
                double score = 0;

                if (c.IsArtifact)
                {
                    // Priceless Artifact cannot be melded and adds to Red bust count
                    score = 200; // Top pass urgency!
                }
                else if (c.Color == "RED")
                {
                    score = 70;
                    if (minOppHand <= 3) score += (4 - minOppHand) * 35;
                    score += (c.Val == 15 ? 15 : 5);
                }
                else if (c.Color == "GREEN")
                {
                    score = 25;
                    int bCount = hand.Count(x => x.Color == "BLUE");
                    int gCount = hand.Count(x => x.Color == "GREEN");
                    if (bCount == 0 && gCount >= 2) score += 20;
                }
                else if (c.Color == "BLUE")
                {
                    score = 15;
                    int sameItem = hand.Count(x => x.Color == "BLUE" && x.Item == c.Item);
                    if (sameItem >= 2) score -= 40; // Protect pairs
                }

                if (score > maxUrgency)
                {
                    maxUrgency = score;
                    bestIdx = i;
                }
            }

            return bestIdx;
        }

        static RoundStats SimulateRound(int numPlayers, int starter)
        {
            bool is2P = (numPlayers == 2);
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
                    hands[p].Add(deck[deck.Count - 1]);
                    deck.RemoveAt(deck.Count - 1);
                }
            }

            int currPlayer = starter % numPlayers;
            int direction = 1; // 1 = clockwise, -1 = counter-clockwise
            int turns = 0;
            int consecutiveNoMeld = 0;
            int cameraFlips = 0;
            int artifactPasses = 0;
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
                    hands[targetP].Add(passed);

                    if (passed.IsArtifact)
                    {
                        artifactPasses++;
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

                // Safety circuit breaker
                if (turns > 300)
                {
                    standoff = true;
                    break;
                }
            }

            // End of round: The Escape Check / Most Red Bust
            var stats = new RoundStats();
            stats.GetawayEscape = getaway;
            stats.Standoff = standoff;
            stats.Turns = turns;
            stats.CameraFlips = cameraFlips;
            stats.ArtifactPasses = artifactPasses;
            stats.CardsLeftInDeck = deck.Count;
            stats.WinnerSeat = winner;

            int[] redCounts = new int[numPlayers];
            int artifactHolder = -1;

            for (int p = 0; p < numPlayers; p++)
            {
                redCounts[p] = hands[p].Count(c => c.Color == "RED");
                if (hands[p].Any(c => c.IsArtifact))
                {
                    artifactHolder = p;
                }
                stats.RedCounts[p] = redCounts[p];
            }

            int maxRed = redCounts.Max();

            int bustedCount = 0;
            int evadedCount = 0;
            int cleanCount = 0;

            for (int p = 0; p < numPlayers; p++)
            {
                int red = redCounts[p];
                int stash = stashes[p].Sum();

                if (red == maxRed && maxRed > 0)
                {
                    // Bust!
                    stats.FinalScores[p] = 0;
                    bustedCount++;
                }
                else
                {
                    // Evaded arrest or Clean
                    stats.FinalScores[p] = stash;
                    if (red > 0) evadedCount++;
                    else cleanCount++;
                }
            }

            stats.BustedPlayerCount = bustedCount;
            stats.EvadedPlayerCount = evadedCount;
            stats.CleanPlayerCount = cleanCount;

            if (artifactHolder != -1)
            {
                stats.ArtifactHolderBusted = (redCounts[artifactHolder] == maxRed && maxRed > 0);
            }

            return stats;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("======================================================================");
            Console.WriteLine("    CAUGHT RED-HANDED: MONTE CARLO SIMULATION (5,000 ROUNDS)");
            Console.WriteLine("    Deck: 55 Play Cards (30 Blue, 16 Red, 8 Wilds, 1 Priceless Artifact)");
            Console.WriteLine("    Penalty Rule: Only player(s) with MOST Red Contraband bust (0 pts)");
            Console.WriteLine("======================================================================\n");

            int numRounds = 5000;
            var rounds4P = new List<RoundStats>();

            for (int r = 0; r < numRounds; r++)
            {
                rounds4P.Add(SimulateRound(4, r % 4));
            }

            double getawayPct = rounds4P.Count(r => r.GetawayEscape) * 100.0 / numRounds;
            double standoffPct = rounds4P.Count(r => r.Standoff) * 100.0 / numRounds;
            double avgTurns = rounds4P.Average(r => r.Turns);
            double avgDeckLeft = rounds4P.Average(r => r.CardsLeftInDeck);
            double avgFlips = rounds4P.Average(r => r.CameraFlips);
            double avgArtifactPasses = rounds4P.Average(r => r.ArtifactPasses);
            double artifactCurseRate = rounds4P.Count(r => r.ArtifactHolderBusted) * 100.0 / numRounds;
            double avgBusted = rounds4P.Average(r => r.BustedPlayerCount);
            double avgEvaded = rounds4P.Average(r => r.EvadedPlayerCount);
            double avgClean = rounds4P.Average(r => r.CleanPlayerCount);

            double avgScoreBusted = 0;
            double avgScoreEvaded = rounds4P.SelectMany(r => r.FinalScores).Where(s => s > 0).DefaultIfEmpty(0).Average();

            Console.WriteLine(">>> 4-PLAYER HEIST (55 Cards, Target 300 pts) <<<");
            Console.WriteLine(string.Format("• Total Simulated Rounds:      {0:N0}", numRounds));
            Console.WriteLine(string.Format("• Getaway Escapes (Empty Hand):{0:F2}% (Natural win)", getawayPct));
            Console.WriteLine(string.Format("• Empty Locker Standoffs:      {0:F2}% (Dry deck resolution)", standoffPct));
            Console.WriteLine(string.Format("• Average Turns per Round:     {0:F1} turns ({1:F1} turns/player)", avgTurns, avgTurns / 4.0));
            Console.WriteLine(string.Format("• Average Cards Left in Deck:  {0:F1} cards remaining", avgDeckLeft));
            Console.WriteLine(string.Format("• Camera Flips per Round:      {0:F2} flips (Active direction churn)", avgFlips));
            Console.WriteLine();

            Console.WriteLine(">>> 🗿 PRICELESS ARTIFACT DYNAMICS <<<");
            Console.WriteLine(string.Format("• Average Artifact Passes:     {0:F2} passes / round (Circulates as hot potato!)", avgArtifactPasses));
            Console.WriteLine(string.Format("• Artifact Curse Rate:         {0:F2}% of rounds, holder had MOST Red & BUSTED!", artifactCurseRate));
            Console.WriteLine(string.Format("• Artifact Evade Rate:         {0:F2}% holder survived because opponent had MORE red!", 100.0 - artifactCurseRate));
            Console.WriteLine();

            Console.WriteLine(">>> 🚨 FALL GUY & ARREST OUTCOMES <<<");
            Console.WriteLine(string.Format("• Average Busted Players:      {0:F2} players / round (Took the fall for most red)", avgBusted));
            Console.WriteLine(string.Format("• Average Evaded Players:      {0:F2} players / round (Held red, but avoided bust!)", avgEvaded));
            Console.WriteLine(string.Format("• Average Clean Players:       {0:F2} players / round (Zero contraband held)", avgClean));
            Console.WriteLine(string.Format("• Avg Stash Points (Survivors):{0:F1} pts / round", avgScoreEvaded));
            Console.WriteLine();

            // Seat Position Balance Check
            Console.WriteLine(">>> 🪑 SEAT POSITION BALANCE (First-Player Bias Check) <<<");
            for (int s = 0; s < 4; s++)
            {
                int wins = rounds4P.Count(r => r.WinnerSeat == s);
                double winPct = wins * 100.0 / rounds4P.Count(r => r.GetawayEscape);
                Console.WriteLine(string.Format("• Seat {0} Escape Rate:         {1:F2}% of getaway escapes", s + 1, winPct));
            }
            Console.WriteLine();

            // 2-Player Duel Simulation
            Console.WriteLine(">>> ⚔️ 2-PLAYER DUEL C1 (29 Cards, Target 200 pts) <<<");
            var rounds2P = new List<RoundStats>();
            for (int r = 0; r < numRounds; r++)
            {
                rounds2P.Add(SimulateRound(2, r % 2));
            }

            double getaway2P = rounds2P.Count(r => r.GetawayEscape) * 100.0 / numRounds;
            double standoff2P = rounds2P.Count(r => r.Standoff) * 100.0 / numRounds;
            double avgTurns2P = rounds2P.Average(r => r.Turns);
            double avgFlips2P = rounds2P.Average(r => r.CameraFlips);
            double artifactPasses2P = rounds2P.Average(r => r.ArtifactPasses);
            double artifactCurse2P = rounds2P.Count(r => r.ArtifactHolderBusted) * 100.0 / numRounds;

            Console.WriteLine(string.Format("• Getaway Escapes:             {0:F2}%", getaway2P));
            Console.WriteLine(string.Format("• Standoff Rate:               {0:F2}%", standoff2P));
            Console.WriteLine(string.Format("• Average Turns per Round:     {0:F1} turns ({1:F1} turns/player)", avgTurns2P, avgTurns2P / 2.0));
            Console.WriteLine(string.Format("• Artifact Hot-Potato Passes:  {0:F2} passes / duel round", artifactPasses2P));
            Console.WriteLine(string.Format("• Artifact Curse Rate:         {0:F2}% bust rate when holding artifact", artifactCurse2P));
            Console.WriteLine();
            Console.WriteLine("Simulation complete.");
        }
    }
}
