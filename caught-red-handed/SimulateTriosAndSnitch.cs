using System;
using System.Collections.Generic;
using System.Linq;

namespace CaughtRedHandedTrios
{
    public class Card
    {
        public string Item;
        public string Color; // BLUE, RED, GREEN, SPECIAL
        public int Val;
        public bool IsGrand;
        public bool IsWild;
        public bool IsSnitch;

        public Card(string item, string color, int val, bool isGrand, bool isWild, bool isSnitch)
        {
            Item = item;
            Color = color;
            Val = val;
            IsGrand = isGrand;
            IsWild = isWild;
            IsSnitch = isSnitch;
        }
    }

    public class TrioResult
    {
        public Card C1;
        public Card C2;
        public Card C3;
        public int Points;
        public bool FlipsCamera;
        public string Name;
    }

    public class RoundStats
    {
        public bool GetawayEscape;
        public bool Standoff;
        public int Turns;
        public int CameraFlips;
        public int InterceptCount;
        public int SnitchPlays;
        public int SnitchBusts;
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

        static List<Card> BuildDeck(bool isDoubleDeck)
        {
            var deck = new List<Card>();

            string[] pettyItems = new string[] { "Cash", "Diamond", "Watch", "Key" };
            string[] grandItems = new string[] { "Painting", "Gold", "Crown", "Relic" };

            int copies = isDoubleDeck ? 2 : 1;

            for (int k = 0; k < copies; k++)
            {
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

                // Green Wilds: 6 per 54 cards
                for (int i = 0; i < 6; i++)
                {
                    deck.Add(new Card("Wild", "GREEN", 0, false, true, false));
                }

                // The Snitch: 2 per 54 cards
                for (int i = 0; i < 2; i++)
                {
                    deck.Add(new Card("Snitch", "SPECIAL", 0, false, false, true));
                }
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

        static TrioResult FindBestTrio(List<Card> hand)
        {
            var blues = hand.Where(c => c.Color == "BLUE").ToList();
            if (blues.Count == 0) return null;

            var reds = hand.Where(c => c.Color == "RED").ToList();
            var greens = hand.Where(c => c.Color == "GREEN").ToList();

            var candidates = new List<TrioResult>();
            var items = blues.Select(b => b.Item).Distinct().ToList();

            foreach (var item in items)
            {
                var itemBlues = blues.Where(b => b.Item == item).ToList();
                var itemReds = reds.Where(r => r.Item == item).ToList();

                // 1. The Smuggler's Run: 1 Blue + 2 Red (35 pts for Grand, 25 pts for Petty)
                if (itemBlues.Count >= 1 && itemReds.Count >= 2)
                {
                    int pts = itemBlues[0].Val + itemReds[0].Val + itemReds[1].Val;
                    candidates.Add(new TrioResult
                    {
                        C1 = itemBlues[0],
                        C2 = itemReds[0],
                        C3 = itemReds[1],
                        Points = pts,
                        FlipsCamera = false,
                        Name = "Smuggler's Run (1B+2R)"
                    });
                }

                // 2. Laundered Heist: 2 Blue + 1 Red (25 pts for Grand, 20 pts for Petty)
                if (itemBlues.Count >= 2 && itemReds.Count >= 1)
                {
                    int pts = itemBlues[0].Val + itemBlues[1].Val + itemReds[0].Val;
                    candidates.Add(new TrioResult
                    {
                        C1 = itemBlues[0],
                        C2 = itemBlues[1],
                        C3 = itemReds[0],
                        Points = pts,
                        FlipsCamera = false,
                        Name = "Laundered Trio (2B+1R)"
                    });
                }

                // 3. Clean Syndicate: 3 Blue (15 pts)
                if (itemBlues.Count >= 3)
                {
                    candidates.Add(new TrioResult
                    {
                        C1 = itemBlues[0],
                        C2 = itemBlues[1],
                        C3 = itemBlues[2],
                        Points = 15,
                        FlipsCamera = false,
                        Name = "Clean Syndicate (3B)"
                    });
                }

                // 4. Wild Trio with 1 Blue + 1 Red + 1 Wild
                if (greens.Count >= 1 && itemBlues.Count >= 1 && itemReds.Count >= 1)
                {
                    int pts = itemBlues[0].Val + itemReds[0].Val;
                    candidates.Add(new TrioResult
                    {
                        C1 = itemBlues[0],
                        C2 = itemReds[0],
                        C3 = greens[0],
                        Points = pts,
                        FlipsCamera = true,
                        Name = "Wild Launder Trio"
                    });
                }

                // 5. Wild Trio with 2 Blues + 1 Wild
                if (greens.Count >= 1 && itemBlues.Count >= 2)
                {
                    candidates.Add(new TrioResult
                    {
                        C1 = itemBlues[0],
                        C2 = itemBlues[1],
                        C3 = greens[0],
                        Points = 10,
                        FlipsCamera = true,
                        Name = "Wild Clean Trio"
                    });
                }
            }

            if (candidates.Count == 0) return null;
            return candidates.OrderByDescending(c => c.Points).First();
        }

        static TrioResult FindInterceptTrio(List<Card> hand, Card passedCard)
        {
            if (passedCard.Color != "RED") return null;

            var blues = hand.Where(c => c.Color == "BLUE" && c.Item == passedCard.Item).ToList();
            if (blues.Count == 0) return null;

            var reds = hand.Where(c => c.Color == "RED" && c.Item == passedCard.Item).ToList();
            var greens = hand.Where(c => c.Color == "GREEN").ToList();

            if (blues.Count >= 1 && reds.Count >= 1)
            {
                int pts = blues[0].Val + reds[0].Val + passedCard.Val;
                return new TrioResult { C1 = blues[0], C2 = reds[0], C3 = passedCard, Points = pts, FlipsCamera = false };
            }

            if (blues.Count >= 2)
            {
                int pts = blues[0].Val + blues[1].Val + passedCard.Val;
                return new TrioResult { C1 = blues[0], C2 = blues[1], C3 = passedCard, Points = pts, FlipsCamera = false };
            }

            if (blues.Count >= 1 && greens.Count >= 1)
            {
                int pts = blues[0].Val + passedCard.Val;
                return new TrioResult { C1 = blues[0], C2 = greens[0], C3 = passedCard, Points = pts, FlipsCamera = true };
            }

            return null;
        }

        static int ChooseCardToPass(List<Card> hand, int minOpponentHand)
        {
            int bestIdx = 0;
            double maxScore = -9999;

            for (int i = 0; i < hand.Count; i++)
            {
                var c = hand[i];
                double score = 0;

                if (c.IsSnitch)
                {
                    int myReds = hand.Count(x => x.Color == "RED");
                    score += myReds * 25;
                }
                else if (c.Color == "RED")
                {
                    score += 40;
                    if (minOpponentHand <= 3) score += (4 - minOpponentHand) * 20;
                    score += (c.Val == 15 ? 10 : 5);
                }
                else if (c.Color == "GREEN")
                {
                    score += 20;
                    int blues = hand.Count(x => x.Color == "BLUE");
                    if (blues == 0) score += 15;
                }
                else if (c.Color == "BLUE")
                {
                    score += 10;
                    int sameItemBlues = hand.Count(x => x.Color == "BLUE" && x.Item == c.Item);
                    if (sameItemBlues >= 3) score -= 40;
                }

                if (score > maxScore)
                {
                    maxScore = score;
                    bestIdx = i;
                }
            }

            return bestIdx;
        }

        static RoundStats SimulateRound(bool isDoubleDeck, int startHand, int starter)
        {
            int numPlayers = 4;
            var deck = BuildDeck(isDoubleDeck);

            var hands = new List<List<Card>>();
            var stashes = new List<List<int>>();
            for (int p = 0; p < numPlayers; p++)
            {
                hands.Add(new List<Card>());
                stashes.Add(new List<int>());
            }

            // Deal cards
            for (int i = 0; i < startHand; i++)
            {
                for (int p = 0; p < numPlayers; p++)
                {
                    var card = deck[deck.Count - 1];
                    deck.RemoveAt(deck.Count - 1);
                    hands[p].Add(card);
                }
            }

            int currPlayer = starter % numPlayers;
            int direction = 1;
            int turns = 0;
            int consecutiveNoMeld = 0;
            int cameraFlips = 0;
            int interceptCount = 0;
            int snitchPlays = 0;
            int snitchBusts = 0;
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

                if (hands[currPlayer].Count == 0)
                {
                    winner = currPlayer;
                    getaway = true;
                    break;
                }

                // AI SNITCH CHECK: Does any player holding The Snitch want to play it right now?
                for (int p = 0; p < numPlayers; p++)
                {
                    var snitchCard = hands[p].FirstOrDefault(c => c.IsSnitch);
                    if (snitchCard != null)
                    {
                        int myRed = hands[p].Count(c => c.Color == "RED");
                        int maxOpponentStash = hands.Where((h, idx) => idx != p).Max(h => stashes[hands.IndexOf(h)].Sum());
                        int oppWithMostRed = hands.Where((h, idx) => idx != p).Max(h => h.Count(c => c.Color == "RED"));

                        if (myRed == 0 || (myRed == 1 && oppWithMostRed >= 2 && maxOpponentStash >= 25))
                        {
                            hands[p].Remove(snitchCard);
                            snitchPlays++;

                            int[] redCountsRaid = hands.Select(h => h.Count(c => c.Color == "RED")).ToArray();
                            int maxRedRaid = redCountsRaid.Max();

                            if (maxRedRaid > 0)
                            {
                                for (int r = 0; r < numPlayers; r++)
                                {
                                    if (redCountsRaid[r] == maxRedRaid)
                                    {
                                        stashes[r].Clear();
                                        snitchBusts++;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }

                // 2. Meld Trio
                var bestTrio = FindBestTrio(hands[currPlayer]);
                if (bestTrio != null)
                {
                    hands[currPlayer].Remove(bestTrio.C1);
                    hands[currPlayer].Remove(bestTrio.C2);
                    hands[currPlayer].Remove(bestTrio.C3);
                    stashes[currPlayer].Add(bestTrio.Points);

                    if (bestTrio.FlipsCamera)
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

                if (hands[currPlayer].Count == 0)
                {
                    winner = currPlayer;
                    getaway = true;
                    break;
                }

                if (deck.Count == 0 && consecutiveNoMeld >= numPlayers)
                {
                    standoff = true;
                    break;
                }

                // 3. Pass
                if (hands[currPlayer].Count > 0)
                {
                    int targetP = (currPlayer + direction + 4) % 4;

                    int minOpp = hands.Where((h, idx) => idx != currPlayer).Min(h => h.Count);
                    int passIdx = ChooseCardToPass(hands[currPlayer], minOpp);

                    var passed = hands[currPlayer][passIdx];
                    hands[currPlayer].RemoveAt(passIdx);

                    // Intercept Trio Check
                    var interceptTrio = FindInterceptTrio(hands[targetP], passed);

                    if (interceptTrio != null)
                    {
                        hands[targetP].Remove(interceptTrio.C1);
                        hands[targetP].Remove(interceptTrio.C2);
                        stashes[targetP].Add(interceptTrio.Points);
                        if (interceptTrio.FlipsCamera)
                        {
                            direction = -direction;
                            cameraFlips++;
                        }
                        interceptCount++;
                        consecutiveNoMeld = 0;

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

                if (hands[currPlayer].Count == 0)
                {
                    winner = currPlayer;
                    getaway = true;
                    break;
                }

                if (deck.Count == 0 && consecutiveNoMeld >= numPlayers)
                {
                    standoff = true;
                    break;
                }

                currPlayer = (currPlayer + direction + 4) % 4;
                if (turns > 400) { standoff = true; break; }
            }

            var stats = new RoundStats();
            stats.GetawayEscape = getaway;
            stats.Standoff = standoff;
            stats.Turns = turns;
            stats.CameraFlips = cameraFlips;
            stats.InterceptCount = interceptCount;
            stats.SnitchPlays = snitchPlays;
            stats.SnitchBusts = snitchBusts;
            stats.CardsLeftInDeck = deck.Count;
            stats.WinnerSeat = winner;

            int[] redCounts = new int[numPlayers];
            for (int p = 0; p < numPlayers; p++)
            {
                redCounts[p] = hands[p].Count(c => c.Color == "RED");
                stats.RedCounts[p] = redCounts[p];
            }

            int maxRed = redCounts.Max();
            int busted = 0; int evaded = 0; int clean = 0;

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

        static void RunSuite(string title, bool isDoubleDeck, int startHand, int numRounds)
        {
            int escapes = 0; int standoffs = 0;
            long totalTurns = 0; long totalFlips = 0; long totalIntercepts = 0;
            long totalSnitchPlays = 0; long totalSnitchBusts = 0; long totalCardsLeft = 0;
            long totalBusted = 0; long totalEvaded = 0; long totalClean = 0;
            long totalScoreSurvivor = 0; long totalSurvivors = 0;
            int[] seatEscapes = new int[4];

            for (int i = 0; i < numRounds; i++)
            {
                int starter = i % 4;
                var res = SimulateRound(isDoubleDeck, startHand, starter);

                if (res.GetawayEscape) escapes++;
                if (res.Standoff) standoffs++;
                totalTurns += res.Turns;
                totalFlips += res.CameraFlips;
                totalIntercepts += res.InterceptCount;
                totalSnitchPlays += res.SnitchPlays;
                totalSnitchBusts += res.SnitchBusts;
                totalCardsLeft += res.CardsLeftInDeck;
                totalBusted += res.BustedPlayerCount;
                totalEvaded += res.EvadedPlayerCount;
                totalClean += res.CleanPlayerCount;

                if (res.WinnerSeat >= 0 && res.WinnerSeat < 4) seatEscapes[res.WinnerSeat]++;

                for (int p = 0; p < 4; p++)
                {
                    if (res.FinalScores[p] > 0)
                    {
                        totalScoreSurvivor += res.FinalScores[p];
                        totalSurvivors++;
                    }
                }
            }

            double avgTurns = (double)totalTurns / numRounds;
            double avgFlips = (double)totalFlips / numRounds;
            double avgIntercepts = (double)totalIntercepts / numRounds;
            double avgSnitchPlays = (double)totalSnitchPlays / numRounds;
            double avgSnitchBusts = (double)totalSnitchBusts / numRounds;
            double escapePct = (double)escapes / numRounds * 100.0;
            double standoffPct = (double)standoffs / numRounds * 100.0;
            double avgCardsLeft = (double)totalCardsLeft / numRounds;
            double avgBusted = (double)totalBusted / numRounds;
            double avgEvaded = (double)totalEvaded / numRounds;
            double avgClean = (double)totalClean / numRounds;
            double avgScore = totalSurvivors > 0 ? (double)totalScoreSurvivor / totalSurvivors : 0;

            Console.WriteLine("======================================================================");
            Console.WriteLine(title);
            Console.WriteLine("======================================================================");
            Console.WriteLine(string.Format("• Average Turns / Player:      {0:F1} turns ({1:F1} table turns) 🎯", avgTurns / 4.0, avgTurns));
            Console.WriteLine(string.Format("• Getaway Escapes (Empty Hand):{0:F2}%", escapePct));
            Console.WriteLine(string.Format("• Empty Locker Standoffs:      {0:F2}%", standoffPct));
            Console.WriteLine(string.Format("• Cards Left in Locker:        {0:F1} cards", avgCardsLeft));
            Console.WriteLine(string.Format("• Camera Flips per Round:      {0:F2} flips", avgFlips));
            Console.WriteLine(string.Format("• Intercept Trios per Round:   {0:F2} intercepts", avgIntercepts));
            Console.WriteLine(string.Format("• 🕵️ The Snitch Played / Round: {0:F2} raids", avgSnitchPlays));
            Console.WriteLine(string.Format("• 💥 Mid-Round Vault Wipes:     {0:F2} players wiped to 0 pts!", avgSnitchBusts));
            Console.WriteLine(string.Format("• Average Final Fall Guys:     {0:F2} players busted at round end", avgBusted));
            Console.WriteLine(string.Format("• Survivor Avg Score:          {0:F1} pts / round", avgScore));
            Console.WriteLine(string.Format("• Seat Balance: S1: {0:F1}% | S2: {1:F1}% | S3: {2:F1}% | S4: {3:F1}%\n",
                (double)seatEscapes[0] / escapes * 100.0,
                (double)seatEscapes[1] / escapes * 100.0,
                (double)seatEscapes[2] / escapes * 100.0,
                (double)seatEscapes[3] / escapes * 100.0));
        }

        static void Main(string[] args)
        {
            RunSuite("MODE 1: 54-CARD SINGLE DECK (Hand 7) • Trios + 2 Snitches", false, 7, 5000);
            RunSuite("MODE 2: 108-CARD DOUBLE DECK (Hand 8) • Trios + 4 Snitches", true, 8, 5000);
            RunSuite("MODE 3: 108-CARD DOUBLE DECK (Hand 9) • Trios + 4 Snitches", true, 9, 5000);
        }
    }
}
