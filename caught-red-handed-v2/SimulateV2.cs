using System;
using System.Collections.Generic;
using System.Linq;

namespace CaughtRedHandedV2Sim
{
    public enum CardType
    {
        BlueNumber,
        RedNumber,
        RedHotEvidence,
        Reset
    }

    public class Card
    {
        public CardType Type;
        public int Val; // 0 for Reset, 1..10 for numbers, -1 for RedHotEvidence
        public string Name;

        public bool IsRed { get { return Type == CardType.RedNumber || Type == CardType.RedHotEvidence; } }
        public bool IsReset { get { return Type == CardType.Reset; } }

        public override string ToString()
        {
            if (Type == CardType.RedHotEvidence) return "🚨 Red HOT Evidence";
            if (Type == CardType.Reset) return string.Format("⚡ Reset ({0})", Name);
            if (Type == CardType.RedNumber) return string.Format("🚨 Red {0}", Val);
            return string.Format("💎 Blue {0}", Val);
        }
    }

    public class Player
    {
        public int Id;
        public List<Card> Hand = new List<Card>();
        public List<Card> PenaltyPile = new List<Card>();
        public int Strikes = 0;

        public int TotalRedCount
        {
            get
            {
                return Hand.Count(c => c.IsRed) + PenaltyPile.Count(c => c.IsRed);
            }
        }
    }

    public class PlayDeclaration
    {
        public int PlayerId;
        public Card ActualCard;
        public int DeclaredVal; // 1..10, or 0 for Reset
        public bool DeclaredReset;

        public bool IsLegal
        {
            get
            {
                if (DeclaredReset) return ActualCard.IsReset;
                if (ActualCard.Type == CardType.RedHotEvidence) return false;
                return ActualCard.Val == DeclaredVal;
            }
        }
    }

    public class Program
    {
        static Random rng = new Random(42);

        public static List<Card> BuildDeck()
        {
            var deck = new List<Card>();

            // 36 Blue Cards (1..10)
            int[] blueDistribution = new int[] { 3, 3, 4, 4, 4, 4, 4, 4, 3, 3 };
            for (int v = 1; v <= 10; v++)
            {
                int count = blueDistribution[v - 1];
                for (int i = 0; i < count; i++)
                {
                    deck.Add(new Card { Type = CardType.BlueNumber, Val = v, Name = string.Format("Blue {0}", v) });
                }
            }

            // 8 Red Numbered Cards (1..6)
            deck.Add(new Card { Type = CardType.RedNumber, Val = 1, Name = "Red 1" });
            deck.Add(new Card { Type = CardType.RedNumber, Val = 1, Name = "Red 1" });
            deck.Add(new Card { Type = CardType.RedNumber, Val = 2, Name = "Red 2" });
            deck.Add(new Card { Type = CardType.RedNumber, Val = 2, Name = "Red 2" });
            deck.Add(new Card { Type = CardType.RedNumber, Val = 3, Name = "Red 3" });
            deck.Add(new Card { Type = CardType.RedNumber, Val = 4, Name = "Red 4" });
            deck.Add(new Card { Type = CardType.RedNumber, Val = 5, Name = "Red 5" });
            deck.Add(new Card { Type = CardType.RedNumber, Val = 6, Name = "Red 6" });

            // 6 Red Hot Evidence (Unnumbered)
            for (int i = 0; i < 6; i++)
            {
                deck.Add(new Card { Type = CardType.RedHotEvidence, Val = -1, Name = "Red Hot Evidence" });
            }

            // 4 Resets
            deck.Add(new Card { Type = CardType.Reset, Val = 0, Name = "The Fence" });
            deck.Add(new Card { Type = CardType.Reset, Val = 0, Name = "The Fence" });
            deck.Add(new Card { Type = CardType.Reset, Val = 0, Name = "Smoke Bomb" });
            deck.Add(new Card { Type = CardType.Reset, Val = 0, Name = "Smoke Bomb" });

            return deck;
        }

        public static void Shuffle(List<Card> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var val = list[k];
                list[k] = list[n];
                list[n] = val;
            }
        }

        public static void Main()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("CAUGHT RED-HANDED V2: THE INTERROGATION - MONTE CARLO SIM");
            Console.WriteLine("Deck: 36 Blue + 8 Red Numbered + 6 Red Hot + 4 Resets = 54 Cards");
            Console.WriteLine("==========================================================");

            for (int numPlayers = 3; numPlayers <= 5; numPlayers++)
            {
                RunSimulation(numPlayers, 2000);
            }
        }

        static void RunSimulation(int numPlayers, int matches)
        {
            int totalRounds = 0;
            int totalTurns = 0;
            int totalBluffs = 0;
            int totalBluffsCaught = 0;
            int totalFalseAccusations = 0;
            int totalPasses = 0;
            int[] seatBusts = new int[numPlayers];
            int[] seatEscapes = new int[numPlayers];

            for (int m = 0; m < matches; m++)
            {
                var players = new List<Player>();
                for (int i = 0; i < numPlayers; i++)
                    players.Add(new Player { Id = i });

                int roundCount = 0;
                while (!players.Any(p => p.Strikes >= 3))
                {
                    roundCount++;
                    // Play a single round
                    PlayRound(players, ref totalTurns, ref totalBluffs, ref totalBluffsCaught, 
                              ref totalFalseAccusations, ref totalPasses, seatEscapes);
                }

                totalRounds += roundCount;
                var maxStrikes = players.Max(p => p.Strikes);
                for (int i = 0; i < numPlayers; i++)
                {
                    if (players[i].Strikes == maxStrikes)
                        seatBusts[i]++;
                }
            }

            Console.WriteLine(string.Format("\n--- Results for {0} Players ({1} Full Matches to 3 Strikes) ---", numPlayers, matches));
            Console.WriteLine(string.Format("Average Rounds per Match: {0:F2}", (double)totalRounds / matches));
            Console.WriteLine(string.Format("Average Turns per Round: {0:F1}", (double)totalTurns / totalRounds));
            Console.WriteLine(string.Format("Bluff Frequency: {0:F1}% of plays", (double)totalBluffs / totalTurns * 100));
            int totalInvestigations = totalBluffsCaught + totalFalseAccusations;
            Console.WriteLine(string.Format("Total Investigations: {0} (Caught: {1}, False: {2})", totalInvestigations, totalBluffsCaught, totalFalseAccusations));
            Console.WriteLine(string.Format("Investigation Accuracy: {0:F1}% caught bluffing", (double)totalBluffsCaught / Math.Max(1, totalInvestigations) * 100));
            Console.WriteLine(string.Format("Pass / Draw Frequency: {0:F1}%", (double)totalPasses / totalTurns * 100));
            Console.Write("Seat Bust % (Fairness): ");
            for (int i = 0; i < numPlayers; i++)
            {
                Console.Write(string.Format("P{0}: {1:F1}% | ", i + 1, (double)seatBusts[i] / matches * 100));
            }
            Console.WriteLine();
            Console.Write("Seat Escape %: ");
            for (int i = 0; i < numPlayers; i++)
            {
                Console.Write(string.Format("P{0}: {1:F1}% | ", i + 1, (double)seatEscapes[i] / totalRounds * 100));
            }
            Console.WriteLine();
        }

        static void PlayRound(List<Player> players, ref int totalTurns, ref int totalBluffs, 
                              ref int totalBluffsCaught, ref int totalFalseAccusations, 
                              ref int totalPasses, int[] seatEscapes)
        {
            var deck = BuildDeck();
            Shuffle(deck);

            // Reset round state
            foreach (var p in players)
            {
                p.Hand.Clear();
                p.PenaltyPile.Clear();
                for (int i = 0; i < 7; i++)
                    p.Hand.Add(deck[0]);
                deck.RemoveRange(0, 7);
            }

            var evidencePile = new List<Card>();
            PlayDeclaration lastPlay = null;
            int activeDeclaredVal = 0;
            int currentTurnPlayer = rng.Next(players.Count);
            int consecutivePasses = 0;
            int turnDirection = 1;

            bool roundOver = false;

            while (!roundOver)
            {
                totalTurns++;
                var player = players[currentTurnPlayer];

                // If there's an active lastPlay from the previous player, player may decide to INVESTIGATE!
                bool investigate = false;
                if (lastPlay != null)
                {
                    int knownCopiesOfDeclared = player.Hand.Count(c => c.Val == lastPlay.DeclaredVal);
                    double suspicion = 0.15; // base suspicion

                    if (lastPlay.DeclaredVal >= 8) suspicion += 0.30;
                    else if (lastPlay.DeclaredVal >= 6) suspicion += 0.15;

                    if (lastPlay.DeclaredReset) suspicion += 0.20;
                    if (knownCopiesOfDeclared >= 2) suspicion += 0.35;

                    var prevPlayer = players[lastPlay.PlayerId];
                    if (prevPlayer.Hand.Count == 0) suspicion += 0.25;

                    int redInPile = evidencePile.Count(c => c.IsRed);
                    if (redInPile >= 3) suspicion -= 0.15;

                    if (rng.NextDouble() < suspicion)
                    {
                        investigate = true;
                    }
                }

                if (investigate)
                {
                    bool wasLegal = lastPlay.IsLegal;
                    var prevPlayer = players[lastPlay.PlayerId];

                    if (!wasLegal)
                    {
                        totalBluffsCaught++;
                        var reds = evidencePile.Where(c => c.IsRed).ToList();
                        prevPlayer.PenaltyPile.AddRange(reds);

                        evidencePile.Clear();
                        activeDeclaredVal = 0;
                        lastPlay = null;
                        consecutivePasses = 0;
                        continue;
                    }
                    else
                    {
                        totalFalseAccusations++;
                        var reds = evidencePile.Where(c => c.IsRed).ToList();
                        player.PenaltyPile.AddRange(reds);

                        evidencePile.Clear();
                        activeDeclaredVal = 0;
                        lastPlay = null;
                        consecutivePasses = 0;

                        if (prevPlayer.Hand.Count == 0)
                        {
                            seatEscapes[prevPlayer.Id]++;
                            roundOver = true;
                            break;
                        }

                        currentTurnPlayer = prevPlayer.Id;
                        continue;
                    }
                }

                if (lastPlay != null && players[lastPlay.PlayerId].Hand.Count == 0)
                {
                    seatEscapes[lastPlay.PlayerId]++;
                    roundOver = true;
                    break;
                }

                var truthfulNumbers = player.Hand.Where(c => c.Type == CardType.BlueNumber || c.Type == CardType.RedNumber)
                                                 .Where(c => c.Val > activeDeclaredVal)
                                                 .OrderBy(c => c.Val).ToList();

                var truthfulResets = player.Hand.Where(c => c.IsReset).ToList();

                var redHots = player.Hand.Where(c => c.Type == CardType.RedHotEvidence).ToList();
                var toxicReds = player.Hand.Where(c => c.Type == CardType.RedNumber && c.Val <= activeDeclaredVal).ToList();

                bool played = false;

                if ((redHots.Count > 0 || toxicReds.Count > 0) && activeDeclaredVal <= 7 && rng.NextDouble() < 0.60)
                {
                    Card cardToPlay = redHots.Count > 0 ? redHots[0] : toxicReds[0];
                    player.Hand.Remove(cardToPlay);
                    evidencePile.Add(cardToPlay);

                    int claim = Math.Min(10, activeDeclaredVal + rng.Next(1, 3));
                    lastPlay = new PlayDeclaration
                    {
                        PlayerId = player.Id,
                        ActualCard = cardToPlay,
                        DeclaredVal = claim,
                        DeclaredReset = false
                    };
                    activeDeclaredVal = claim;
                    totalBluffs++;
                    played = true;
                }
                else if (truthfulNumbers.Count > 0)
                {
                    var bestCard = truthfulNumbers.FirstOrDefault(c => c.IsRed) ?? truthfulNumbers[0];
                    player.Hand.Remove(bestCard);
                    evidencePile.Add(bestCard);

                    lastPlay = new PlayDeclaration
                    {
                        PlayerId = player.Id,
                        ActualCard = bestCard,
                        DeclaredVal = bestCard.Val,
                        DeclaredReset = false
                    };
                    activeDeclaredVal = bestCard.Val;
                    played = true;
                }
                else if (truthfulResets.Count > 0 && activeDeclaredVal >= 6)
                {
                    var resetCard = truthfulResets[0];
                    player.Hand.Remove(resetCard);
                    evidencePile.Add(resetCard);

                    lastPlay = new PlayDeclaration
                    {
                        PlayerId = player.Id,
                        ActualCard = resetCard,
                        DeclaredVal = 0,
                        DeclaredReset = true
                    };
                    activeDeclaredVal = 0;
                    if (resetCard.Name.Contains("Smoke Bomb"))
                    {
                        turnDirection *= -1;
                    }
                    played = true;
                }
                else if (player.Hand.Count > 0 && (deck.Count == 0 || rng.NextDouble() < 0.40))
                {
                    var cardToPlay = player.Hand.OrderBy(c => c.IsRed ? 0 : 1).First();
                    player.Hand.Remove(cardToPlay);
                    evidencePile.Add(cardToPlay);

                    int claim = Math.Min(10, activeDeclaredVal + 1);
                    if (claim == activeDeclaredVal) claim = 10; // Max claim
                    lastPlay = new PlayDeclaration
                    {
                        PlayerId = player.Id,
                        ActualCard = cardToPlay,
                        DeclaredVal = claim,
                        DeclaredReset = false
                    };
                    activeDeclaredVal = claim;
                    totalBluffs++;
                    played = true;
                }

                if (played)
                {
                    consecutivePasses = 0;
                }
                else
                {
                    totalPasses++;
                    consecutivePasses++;
                    if (deck.Count > 0)
                    {
                        player.Hand.Add(deck[0]);
                        deck.RemoveAt(0);
                    }

                    if (consecutivePasses >= players.Count)
                    {
                        evidencePile.Clear();
                        activeDeclaredVal = 0;
                        lastPlay = null;
                        consecutivePasses = 0;
                    }
                }

                currentTurnPlayer = (currentTurnPlayer + turnDirection + players.Count) % players.Count;
            }

            int maxReds = -1;
            foreach (var p in players)
            {
                if (p.Hand.Count > 0)
                {
                    int r = p.TotalRedCount;
                    if (r > maxReds) maxReds = r;
                }
            }

            if (maxReds > 0)
            {
                foreach (var p in players)
                {
                    if (p.Hand.Count > 0 && p.TotalRedCount == maxReds)
                    {
                        p.Strikes++;
                    }
                }
            }
        }
    }
}
