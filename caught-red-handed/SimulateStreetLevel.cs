using System;
using System.Collections.Generic;
using System.Linq;

namespace CaughtRedHandedSim
{
    class Card
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Val { get; set; }
    }

    class Buyer
    {
        public int Req { get; set; }
        public bool IsDiscerning { get; set; }
        public string Power { get; set; }
        public string Name { get; set; }
    }

    class StreetSetup
    {
        public List<Buyer> Street { get; set; }
        public List<object> StreetDeck { get; set; }
    }

    class SimReport
    {
        public int TotalMatches { get; set; }
        public int TotalRounds { get; set; }
        public int TotalPoliceRaids { get; set; }
        public int TotalCleanGetaways { get; set; }
        public int TotalArrests { get; set; }
        public double AvgRoundTurns { get; set; }
        public double AvgWinningScore { get; set; }
        public int[] WinCountBySeat { get; set; }
        public int[] ArrestCountBySeat { get; set; }
        public int Round1Raids { get; set; }
        public int Round2Raids { get; set; }
        public int Round3Raids { get; set; }
        public int Round4Raids { get; set; }
        public int TotalCombosUsed { get; set; }
        public int TotalSingleCardsUsed { get; set; }
        public int TotalResupplies { get; set; }
        public int TotalEarlyCashOuts { get; set; }

        public SimReport()
        {
            WinCountBySeat = new int[4];
            ArrestCountBySeat = new int[4];
        }
    }

    class Program
    {
        static Random rng = new Random(42);

        // 72-Card Merchandise Deck
        static List<Card> BuildMerchandiseDeck()
        {
            List<Card> deck = new List<Card>();
            int id = 1;

            // 44 Blue Clean Cards: Values 2-8
            // 6 copies of 2-7 (36) + 8 copies of 8 (8) = 44 cards
            for (int val = 2; val <= 7; val++)
            {
                for (int i = 0; i < 6; i++)
                    deck.Add(new Card { Id = id++, Color = "BLUE", Val = val });
            }
            for (int i = 0; i < 8; i++)
                deck.Add(new Card { Id = id++, Color = "BLUE", Val = 8 });

            // 28 Red Contraband Cards: Values 5-10
            // 4 copies of 5,6,7,8,9 (20) + 8 copies of 10 = 28 cards
            for (int val = 5; val <= 9; val++)
            {
                for (int i = 0; i < 4; i++)
                    deck.Add(new Card { Id = id++, Color = "RED", Val = val });
            }
            for (int i = 0; i < 8; i++)
                deck.Add(new Card { Id = id++, Color = "RED", Val = 10 });

            Shuffle(deck);
            return deck;
        }

        // 34 Buyer Contracts + Police Ladder
        static StreetSetup BuildStreetDeck(int round)
        {
            List<Buyer> allBuyers = new List<Buyer>();

            // 16 Gullible Buyers (Values 3-8, 2-3 copies each)
            int[] gullibleReqs = { 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 8 };
            foreach (int req in gullibleReqs)
                allBuyers.Add(new Buyer { Req = req, IsDiscerning = false, Power = null, Name = "Gullible Buyer" });

            // 18 Discerning Buyers with Powers (Values 4-10)
            allBuyers.Add(new Buyer { Req = 4, IsDiscerning = true, Power = "DISCARD", Name = "Smuggler" });
            allBuyers.Add(new Buyer { Req = 4, IsDiscerning = true, Power = "DRAW", Name = "Pawn Broker" });
            allBuyers.Add(new Buyer { Req = 5, IsDiscerning = true, Power = "DRAW", Name = "Appraiser" });
            allBuyers.Add(new Buyer { Req = 5, IsDiscerning = true, Power = "PASS", Name = "Informant" });
            allBuyers.Add(new Buyer { Req = 6, IsDiscerning = true, Power = "PASS", Name = "Shadow Broker" });
            allBuyers.Add(new Buyer { Req = 6, IsDiscerning = true, Power = "DISCARD", Name = "Fence" });
            allBuyers.Add(new Buyer { Req = 7, IsDiscerning = true, Power = "DISCARD", Name = "Elite Collector" });
            allBuyers.Add(new Buyer { Req = 7, IsDiscerning = true, Power = "DRAW", Name = "Auctioneer" });
            allBuyers.Add(new Buyer { Req = 8, IsDiscerning = true, Power = "PASS", Name = "Mob Kingpin" });
            allBuyers.Add(new Buyer { Req = 8, IsDiscerning = true, Power = "DISCARD", Name = "Syndicate Boss" });
            allBuyers.Add(new Buyer { Req = 9, IsDiscerning = true, Power = "DRAW", Name = "Int'l Fence" });
            allBuyers.Add(new Buyer { Req = 9, IsDiscerning = true, Power = "PASS", Name = "Diplomat" });
            allBuyers.Add(new Buyer { Req = 10, IsDiscerning = true, Power = "DISCARD", Name = "Master Fence" });
            allBuyers.Add(new Buyer { Req = 10, IsDiscerning = true, Power = "PASS", Name = "Market Mogul" });
            allBuyers.Add(new Buyer { Req = 10, IsDiscerning = true, Power = "DRAW", Name = "Billionaire" });
            allBuyers.Add(new Buyer { Req = 5, IsDiscerning = true, Power = "DISCARD", Name = "Safehouse Fixer" });
            allBuyers.Add(new Buyer { Req = 7, IsDiscerning = true, Power = "PASS", Name = "Underworld Courier" });
            allBuyers.Add(new Buyer { Req = 8, IsDiscerning = true, Power = "DRAW", Name = "Banker" });

            Shuffle(allBuyers);

            // 3 starting street buyers
            List<Buyer> street = new List<Buyer> { allBuyers[0], allBuyers[1], allBuyers[2] };

            List<object> deckCards = new List<object>();
            for (int i = 3; i < allBuyers.Count; i++)
                deckCards.Add(allBuyers[i]);

            // Police ladder: R1: 3 cops, R2: 3 cops, R3: 4 cops, R4: 5 cops
            int[] policeCounts = new int[] { 3, 3, 4, 5 };
            int copCount = policeCounts[Math.Min(round - 1, 3)];
            for (int c = 0; c < copCount; c++)
                deckCards.Add("POLICE");

            Shuffle(deckCards);
            return new StreetSetup { Street = street, StreetDeck = deckCards };
        }

        static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T val = list[k];
                list[k] = list[n];
                list[n] = val;
            }
        }

        static bool IsMinimalSurchargeCombo(List<Card> combo, int req)
        {
            int total = combo.Sum(c => c.Val);
            if (total < req) return false;
            foreach (Card c in combo)
            {
                if (total - c.Val >= req)
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            int matches = 10000;
            SimReport report = new SimReport { TotalMatches = matches };
            long totalTurns = 0;
            long totalWinScores = 0;

            for (int m = 0; m < matches; m++)
            {
                int[] matchScores = new int[4];
                int startingSeat = m % 4;

                for (int round = 1; round <= 4; round++)
                {
                    report.TotalRounds++;
                    List<Card> merchDeck = BuildMerchandiseDeck();
                    StreetSetup setup = BuildStreetDeck(round);
                    List<Buyer> street = setup.Street;
                    List<object> streetDeck = setup.StreetDeck;

                    List<List<Card>> hands = new List<List<Card>>();
                    List<List<Buyer>> stashes = new List<List<Buyer>>();
                    int[] soldHeats = new int[4];
                    bool[] cashedOut = new bool[4];

                    for (int p = 0; p < 4; p++)
                    {
                        hands.Add(new List<Card>());
                        stashes.Add(new List<Buyer>());
                        for (int i = 0; i < 5; i++)
                            hands[p].Add(merchDeck.Pop());
                    }

                    int policeStrikes = 0;
                    bool roundOver = false;
                    int activePlayer = (startingSeat + round - 1) % 4;
                    int turnsThisRound = 0;

                    while (!roundOver)
                    {
                        if (cashedOut.All(c => c))
                        {
                            report.TotalCleanGetaways++;
                            break;
                        }

                        if (cashedOut[activePlayer])
                        {
                            activePlayer = (activePlayer + 1) % 4;
                            continue;
                        }

                        turnsThisRound++;

                        // Decision Logic: Cash Out?
                        // Under 3 strikes: Players cash out when police strikes == 2 (1 slot remaining) and hand is clean and banked >= 3 buyers!
                        int redsInHand = hands[activePlayer].Count(c => c.Color == "RED");
                        if (policeStrikes >= 2 && redsInHand == 0 && stashes[activePlayer].Count >= 3)
                        {
                            cashedOut[activePlayer] = true;
                            report.TotalEarlyCashOuts++;
                            activePlayer = (activePlayer + 1) % 4;
                            continue;
                        }

                        // Try to Sell
                        Buyer chosenBuyer = null;
                        int buyerSlot = -1;
                        List<Card> bestCombo = null;

                        for (int s = 0; s < street.Count; s++)
                            {
                            Buyer b = street[s];
                            if (b == null) continue;

                            Card single = hands[activePlayer].Where(c => c.Val >= b.Req).OrderBy(c => c.Val).FirstOrDefault();
                            if (single != null)
                            {
                                chosenBuyer = b;
                                buyerSlot = s;
                                bestCombo = new List<Card> { single };
                                break;
                            }

                            for (int i = 0; i < hands[activePlayer].Count; i++)
                            {
                                for (int j = i + 1; j < hands[activePlayer].Count; j++)
                                {
                                    List<Card> pair = new List<Card> { hands[activePlayer][i], hands[activePlayer][j] };
                                    if (IsMinimalSurchargeCombo(pair, b.Req))
                                    {
                                        chosenBuyer = b;
                                        buyerSlot = s;
                                        bestCombo = pair;
                                        break;
                                    }
                                }
                                if (bestCombo != null) break;
                            }
                            if (bestCombo != null) break;
                        }

                        if (bestCombo != null)
                        {
                            if (bestCombo.Count > 1) report.TotalCombosUsed++;
                            else report.TotalSingleCardsUsed++;

                            foreach (Card c in bestCombo)
                                hands[activePlayer].Remove(c);

                            stashes[activePlayer].Add(chosenBuyer);

                            int redCount = bestCombo.Count(c => c.Color == "RED");
                            if (chosenBuyer.IsDiscerning)
                            {
                                soldHeats[activePlayer] += redCount;

                                if (chosenBuyer.Power == "DRAW" && merchDeck.Count > 0)
                                {
                                    hands[activePlayer].Add(merchDeck.Pop());
                                }
                                else if (chosenBuyer.Power == "DISCARD")
                                {
                                    Card hotRed = hands[activePlayer].FirstOrDefault(c => c.Color == "RED");
                                    if (hotRed != null) hands[activePlayer].Remove(hotRed);
                                }
                                else if (chosenBuyer.Power == "PASS" && hands[activePlayer].Count > 0)
                                {
                                    int target = -1;
                                    int maxPts = -1;
                                    for (int opp = 0; opp < 4; opp++)
                                    {
                                        if (opp == activePlayer || cashedOut[opp]) continue;
                                        int pts = stashes[opp].Sum(x => x.Req);
                                        if (pts > maxPts) { maxPts = pts; target = opp; }
                                    }
                                    if (target != -1)
                                    {
                                        Card passCard = hands[activePlayer].FirstOrDefault(c => c.Color == "RED") ?? hands[activePlayer][0];
                                        hands[activePlayer].Remove(passCard);
                                        hands[target].Add(passCard);
                                    }
                                }
                            }

                            if (streetDeck.Count > 0)
                            {
                                object refCard = streetDeck.Pop();
                                if (refCard is string && (string)refCard == "POLICE")
                                {
                                    policeStrikes++;
                                    street[buyerSlot] = null;

                                    if (policeStrikes >= 3)
                                    {
                                        roundOver = true;
                                        report.TotalPoliceRaids++;
                                        if (round == 1) report.Round1Raids++;
                                        else if (round == 2) report.Round2Raids++;
                                        else if (round == 3) report.Round3Raids++;
                                        else if (round == 4) report.Round4Raids++;
                                    }
                                }
                                else if (refCard is Buyer)
                                {
                                    street[buyerSlot] = (Buyer)refCard;
                                }
                            }
                            else
                            {
                                street[buyerSlot] = null;
                            }
                        }
                        else
                        {
                            if (merchDeck.Count > 0)
                            {
                                hands[activePlayer].Add(merchDeck.Pop());
                                report.TotalResupplies++;
                            }
                            else
                            {
                                cashedOut[activePlayer] = true;
                            }
                        }

                        activePlayer = (activePlayer + 1) % 4;
                    }

                    totalTurns += turnsThisRound;

                    int[] totalHeats = new int[4];
                    int[] roundScores = new int[4];
                    for (int p = 0; p < 4; p++)
                    {
                        int handHeat = hands[p].Count(c => c.Color == "RED") * 2;
                        totalHeats[p] = soldHeats[p] + handHeat;
                        roundScores[p] = stashes[p].Sum(b => b.Req);
                    }

                    int maxHeat = totalHeats.Max();
                    for (int p = 0; p < 4; p++)
                    {
                        if (roundOver && totalHeats[p] == maxHeat && maxHeat > 0)
                        {
                            report.TotalArrests++;
                            report.ArrestCountBySeat[p]++;
                        }
                        else
                        {
                            matchScores[p] += roundScores[p];
                        }
                    }
                }

                int maxScore = matchScores.Max();
                int winner = Array.IndexOf(matchScores, maxScore);
                report.WinCountBySeat[winner]++;
                totalWinScores += maxScore;
            }

            report.AvgRoundTurns = (double)totalTurns / report.TotalRounds;
            report.AvgWinningScore = (double)totalWinScores / report.TotalMatches;

            Console.WriteLine("==================================================================");
            Console.WriteLine("   CAUGHT RED-HANDED: 5-7 TURNS/PLAYER SIMULATION (10,000 MATCHES)");
            Console.WriteLine("==================================================================");
            Console.WriteLine("Total Matches Simulated : {0:N0} (40,000 Rounds)", report.TotalMatches);
            Console.WriteLine("Average Turns per Round : {0:F1} turns (~{1:F1} turns per player)", report.AvgRoundTurns, report.AvgRoundTurns / 4.0);
            Console.WriteLine("Average Winning Score   : {0:F1} points", report.AvgWinningScore);
            Console.WriteLine();
            Console.WriteLine("--- POLICE RAID & ENDGAME FREQUENCY ---");
            Console.WriteLine("Police Raids (3 Cops)   : {0:N0} ({1:P1})", report.TotalPoliceRaids, (double)report.TotalPoliceRaids / report.TotalRounds);
            Console.WriteLine("Clean Getaways (All Out): {0:N0} ({1:P1})", report.TotalCleanGetaways, (double)report.TotalCleanGetaways / report.TotalRounds);
            Console.WriteLine("  * Round 1 Raids (3 Cops): {0:N0} ({1:P1})", report.Round1Raids, (double)report.Round1Raids / report.TotalMatches);
            Console.WriteLine("  * Round 2 Raids (3 Cops): {0:N0} ({1:P1})", report.Round2Raids, (double)report.Round2Raids / report.TotalMatches);
            Console.WriteLine("  * Round 3 Raids (4 Cops): {0:N0} ({1:P1})", report.Round3Raids, (double)report.Round3Raids / report.TotalMatches);
            Console.WriteLine("  * Round 4 Raids (5 Cops): {0:N0} ({1:P1})", report.Round4Raids, (double)report.Round4Raids / report.TotalMatches);
            Console.WriteLine();
            Console.WriteLine("--- PLAYER ACTION DYNAMICS ---");
            int totalSales = report.TotalSingleCardsUsed + report.TotalCombosUsed;
            Console.WriteLine("Single Card Sales       : {0:N0} ({1:P1})", report.TotalSingleCardsUsed, (double)report.TotalSingleCardsUsed / totalSales);
            Console.WriteLine("Combo Bundle Sales      : {0:N0} ({1:P1})", report.TotalCombosUsed, (double)report.TotalCombosUsed / totalSales);
            Console.WriteLine("Re-Supplies (Draws)     : {0:N0}", report.TotalResupplies);
            Console.WriteLine("Early Cash-Outs         : {0:N0}", report.TotalEarlyCashOuts);
            Console.WriteLine();
            Console.WriteLine("--- TURN ORDER FAIRNESS (SEAT BALANCE) ---");
            for (int p = 0; p < 4; p++)
            {
                double winRate = (double)report.WinCountBySeat[p] / report.TotalMatches;
                double arrestRate = (double)report.ArrestCountBySeat[p] / report.TotalRounds;
                Console.WriteLine("Seat {0}: Win Rate = {1:P1} | Arrest Rate = {2:P1}", p + 1, winRate, arrestRate);
            }
            Console.WriteLine("==================================================================");
        }
    }

    static class ExtensionMethods
    {
        public static T Pop<T>(this List<T> list)
        {
            if (list.Count == 0) throw new InvalidOperationException("Empty list.");
            int last = list.Count - 1;
            T item = list[last];
            list.RemoveAt(last);
            return item;
        }
    }
}
