---
name: card-game-mechanic-lab
description: Game mechanics brainstorming, BGG card mechanism taxonomy, tension curves, micro-rulebooks, and win condition design for physical card games.
---

# Card Game Mechanic Lab

Use this skill when brainstorming core game loops, evaluating card mechanisms, designing win conditions, resolving player interaction tension, or drafting micro-rulebooks that fit onto physical reference cards.

---

## 1. Physical Card Mechanism Taxonomy

Modern card game design draws heavily from distinct mechanical families. When brainstorming a new game, pick **1 Primary Mechanism** and **1 Twist Mechanism**:

### A. Trick-Taking & Climbing
* **Standard Trick-Taking**: Lead suit, follow suit, trump suit (e.g. *The Crew*, *Skull King*).
* **Climbing / Shedding**: Beat previous play with higher single, pair, or combination. First to empty hand wins (e.g. *Tichu*, *Scout*, *Custom Heroes*).
* **Twist ideas**: Must-lose tricks, shifting trumps, cooperative trick-taking, betting on exact tricks won before play starts.

### B. Tableau & Engine Building
* **Spatial Tableaux**: Cards played into 2D grids where adjacency triggers bonuses (e.g. *Sprawlopolis*, *Cascadia* in cards).
* **Chain Reactions**: Cards activate when other specific suits or tags are played.
* **Twist ideas**: Splayed columns where only the top card is active until discarded; cards that harvest or devour adjacent cards.

### C. Hand Management & Multi-Use Cards
* **Strict Hand Order**: You cannot rearrange cards in your hand; you may only play adjacent pairs or cards from the edges (e.g. *Bohnanza*, *Scout*).
* **Dual-Action / Inversion**: Cards rotate 180° to become a different ability, or are discarded for mana vs kept for end-game points (e.g. *Race for the Galaxy*, *Gloomhaven* cards).

### D. Skirmish, Duels & Lane Battlers
* **Fixed Fronts / Lanes**: 3 battlefronts (e.g. *Air, Land & Sea*, *Marvel Snap*, *Smash Up*); win 2 out of 3 fronts.
* **Bluff / Facedown Play**: Cards played facedown with hidden strengths, revealed only during combat phase.

### E. Push-Your-Luck & Market Drafting
* **Draw Till You Bust**: Draw cards to build combinations, but drawing duplicate icons busts your round (e.g. *Port Royal*, *Dead Man's Draw*).
* **I-Cut-You-Choose**: One player divides drawn cards into two piles; opponent picks first pile, cutter takes second (e.g. *Hanamikoji*).

---

## 2. The 4 Golden Pillars of Physical Card Game Feel

When brainstorming, check every design against these 4 physical constraints:

1. **Table Footprint**: Can 2–4 players play this on a small coffee shop table? If each player has a 12-card tableau, will cards slide into each other?
2. **Setup Time (< 60 Seconds)**: Physical players hate sorting 10 mini-piles. Shuffle 1 deck, deal $X$ cards, flip top card, start.
3. **Information Accessibility**: Is card text readable from across the table? Key numbers and icons should be in corners with high contrast.
4. **Catch-up Mechanics**: Avoid "runaway leader" syndrome. If Player A gets ahead, give Player B higher tactical flexibility (e.g. card draw compensation, underdog bonuses, or first pick of next turn's cards).

---

## 3. Micro-Rulebook Architecture (Fitting on 2–4 Cards)

In a 56 or 112 card deck, including physical rule cards in the box eliminates the need for folded paper booklets:

### 2-Card Rule Template:
- **Card 1 (Front: Overview & Setup)**:
  - Game Theme (1 sentence) & Objective / Win Condition.
  - Deck contents breakdown.
  - Setup instructions (deal $N$ cards, starting player criteria, draw pile location).
- **Card 1 (Back: Turn Anatomy)**:
  - Phase 1: Draw / Action.
  - Phase 2: Play / Interact.
  - Phase 3: Cleanup / Hand Limit check.
- **Card 2 (Front: Special Actions & Keywords)**:
  - Glossary of 4–6 bolded keyword icons.
  - Edge-case resolution rule ("Card text overrides rule text; defender wins ties").
- **Card 2 (Back: Scoring / Quick Reference)**:
  - Round end scoring table or sudden death checklist.

---

## 4. Rapid Concept Brainstorming Generator

When the user asks for new game concepts, use this rapid formulation matrix:

1. **Theme / Metaphor**: (e.g., Deep Sea Mining, Victorian Heist, Alchemical Bazaar, Celestial Courier).
2. **Core Deck Structure**: (e.g., 56 cards: 4 suits × 12 + 8 event cards).
3. **Core Loop**: Deal $H$ cards $\rightarrow$ simultaneous selection $\rightarrow$ reveal $\rightarrow$ resolve lane dominance.
4. **The Friction Point**: The agonizing decision each turn (e.g., "Playing a card for high attack gives your opponent mana equal to its cost").
5. **Win Condition**: First to 3 round wins, or highest score after deck empties twice.
