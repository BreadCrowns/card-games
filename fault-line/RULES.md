# FAULT LINE

> A cutthroat number-line survival card game of mounting tectonic pressure, precision coverage, and catastrophic sweeps.

---

## 1. Overview & Objective

In **Fault Line**, players take turns placing numbered cards onto a central tectonic fault line anchored by the immovable **Epicenter (#28)**. Cards placed on the left must decrease outward; cards placed on the right must increase outward. 

You can extend the edges cleanly, or cover existing cards of matching color to relieve pressure—stacking cards higher and higher. But if a play disrupts numerical order, the fault fractures: **you must sweep every violating card and stack into your personal Strike Pile!**

* **Player Count**: 2 to 4 Players (expandable to 5)
* **Deck Size**: Exactly 56 Cards (Standard Poker Sheet)
* **Goal**: Accumulate the **fewest total Strikes** across 3 escalating rounds.

---

## 2. Component Architecture (56 Cards)

The deck contains **56 cards** with zero external tokens needed. Every card displays its **Number** and its intrinsic **Strike Value** ($\bigstar$ icons):

```
       [ LEFT WING: 1 to 27 ]            [ ANCHOR: 28 ]           [ RIGHT WING: 29 to 56 ]
Numbers decrease outward from Center      The Epicenter     Numbers increase outward from Center
```

### Card Distribution

1. **The Anchor (#28 - "The Epicenter")** (1 Card):
   * Unique neutral obsidian art frame.
   * Starts in the center of the table every round.
   * **Cannot be covered, matched, or removed.**
2. **Playable Fault Cards** (55 Cards):
   * **5 Tectonic Colors**: Crimson, Amber, Emerald, Cobalt, Amethyst (11 cards each).
   * Numbers run from **1 to 27** (Left Wing) and **29 to 56** (Right Wing).

### The Strike Curve (Bell Curve)

Each card has its Strike penalty ($1$ to $5$ stars) clearly printed on its corners:

| Numbers | Wing | Strike Value | Tectonic Risk |
| :--- | :--- | :---: | :--- |
| **1 – 5** | Far Left | $\bigstar$ (1 Strike) | Minor Tremor (Safe to sweep) |
| **6 – 12** | Mid Left | $\bigstar\bigstar$ (2 Strikes) | Moderate Shift |
| **13 – 19** | Near Left | $\bigstar\bigstar\bigstar$ (3 Strikes) | Severe Rupture |
| **20 – 27** | Epicenter Left | $\bigstar\bigstar\bigstar\bigstar\bigstar$ (5 Strikes) | Catastrophic Fracture! |
| **[ 28 ]** | **CENTER ANCHOR** | **IMMUNE** | **Cannot be taken** |
| **29 – 36** | Epicenter Right | $\bigstar\bigstar\bigstar\bigstar\bigstar$ (5 Strikes) | Catastrophic Fracture! |
| **37 – 43** | Near Right | $\bigstar\bigstar\bigstar$ (3 Strikes) | Severe Rupture |
| **44 – 50** | Mid Right | $\bigstar\bigstar$ (2 Strikes) | Moderate Shift |
| **51 – 56** | Far Right | $\bigstar$ (1 Strike) | Minor Tremor (Safe to sweep) |

---

## 3. Setup

1. Place the **Anchor (#28)** face-up in the dead center of the play area.
2. Shuffle the remaining 55 cards thoroughly.
3. Form a face-down draw pile.
4. Choose a starting player randomly for Round 1.

---

## 4. The 3-Round Escalation Structure

The game is played across **3 escalating rounds**:

* **Round 1**: Deal **3 cards** to each player.
* **Round 2**: Deal **4 cards** to each player.
* **Round 3**: Deal **5 cards** to each player.

In each round, players take turns clockwise playing **1 card from hand** until **all players have played every card in their hand**.

---

## 5. Turn Anatomy & The Unified Law

On your turn, you **must play 1 card** to the Fault Line:
* Cards $< 28$ **must** be played on the **Left Wing**.
* Cards $> 28$ **must** be played on the **Right Wing**.

You have two ways to play:

### Action 1: Outer End Play (Tectonic Sweep)
Place your card at the **outermost edge** of its wing:
* **The Sweep Check**: Examine all cards between your newly placed card and the Anchor.
* **Left Wing Rule**: Order toward Anchor must be strictly **ascending** ($C_{new} < \dots < 28$).
* **Right Wing Rule**: Order away from Anchor must be strictly **ascending** ($28 < \dots < C_{new}$).
* **Resolution**:
  * If the numbers are strictly in order: **0 Strikes taken.**
  * If any cards between your new card and the Anchor violate this order: **You must sweep all violating cards/stacks from the line into your Strike Pile!**

### Action 2: Color Cover Play (Stacking Pressure)
Place your card **directly on top of any card or stack of the same color** on that wing.
* The covered card is **not** discarded; it remains underneath, forming a **taller stack**.
* **The Check**: Does the new top card maintain strictly ascending order with its immediate left and right neighbors?
  * **If YES (Clean Play)**: **0 Strikes taken.**
  * **If NO (Disruption)**: You must remove all adjacent cards or stacks that violate numerical order with your new top card. They go into your Strike Pile!

---

## 6. The Stacking Stakes Rule (Multi-Card Sweeps)

Covering a card is safe on the turn it is played, but it increases tectonic pressure at that position:
* If a position has been covered multiple times, it forms a **stack of cards**.
* Whenever a position is swept due to a disruption or outer end play, **the player must take the ENTIRE stack of cards at that position**.
* Every single card in that stack contributes its full printed Strike value ($\bigstar$) to the player's penalty count!

---

## 7. Round Cleanup & Next Round Setup

When all players have emptied their hands:
1. The remaining Fault Line cards on the table (except the Anchor) are swept into the discard pile.
2. **Strike piles remain face-up in front of each player.** Strikes carry over across all rounds!
3. Gather all non-strike cards (discards + remaining draw pile) and shuffle them thoroughly.
4. **New Round Starting Player**:
   * The new round begins with the **player with the most accumulated strikes** (catch-up agency).
   * **Tie-Breaker**: Ties go to the player seated closest clockwise from the previous round's starter.
5. Deal the appropriate number of cards for the new round (4 cards in Round 2, 5 cards in Round 3).

---

## 8. Winning the Game

At the end of **Round 3**, all players total their accumulated Strikes from all three rounds.

* **The player with the fewest total Strikes wins!**
* **Tie-Breaker**: The tied player who holds the single card with the lowest Strike value wins. If still tied, the tied player who took the fewest cards total wins.
