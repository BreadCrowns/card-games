---
name: deck-architect-56-112
description: Deck mathematics, sheet distribution modeling, hypergeometric probability, and zero-token design constraints for 56-card and 112-card physical card games.
---

# Deck Architect (56 & 112 Cards)

Use this skill when designing, calculating distributions, structuring card ratios, or balancing card economies for physical card games built around standard single-sheet (**56 cards**) or double-sheet (**112 cards**) manufacturing boundaries.

---

## 1. Physical Sheet & Tuckbox Economics

Physical card manufacturing (The Game Crafter, MakePlayingCards, Cartamundi) prints poker decks on uncut sheets:
- **Single Deck Sheet**: 56 cards (7 × 8 grid layout).
- **Double Deck Sheet**: 112 cards (2 × 56 cards, standard double-deck tuckbox).
- Any unused slot on the sheet costs the same to produce. Every card slot must be accounted for: playable cards, reference cards, player aids, or solo-mode automa cards.

---

## 2. Standard 56-Card Partition Archetypes

Choose an architecture based on player count and mechanic:

| Architecture | Split Breakdown | Best Suited For | Examples / Mechanics |
| :--- | :--- | :--- | :--- |
| **Traditional 4-Suit** | 4 suits × 13 ranks + 4 special/wilds = 56 | Trick-taking, shedding, climbing | *Tichu*, *The Crew*, Poker variants |
| **Symmetric 4-Faction** | 4 factions × 14 cards (e.g. 1 Hero, 3 Commons × 3, 2 Rares × 2) = 56 | 2-4 player skirmish or lane battles | *Air, Land & Sea*, micro-CCGs |
| **The 7 × 8 Matrix** | 7 archetypes × 8 cards (or 8 suits × 7 ranks) = 56 | Set collection, rummy, market drafting | *Scout*, *Arboretum* style trees |
| **2-Player Duel (28 / 28)** | 2 asymmetric factions of 28 cards each = 56 | 1v1 closed-deck tactical dueling | Micro-battlers, head-to-head duels |
| **Draft / 3-Pack (18 × 3 + 2)**| 3 packs of 18 cards (54 cards) + 2 Rule/Aid cards = 56 | 3-player booster draft or 2-player grid draft | Cube drafts, micro-drafting |
| **Micro-Deck + Market** | 2 × 10 starter decks (20) + 34 market pool + 2 reference = 56 | Micro-deckbuilder | *Star Realms* micro-edition |

---

## 3. Standard 112-Card Partition Archetypes

Double-sheet layouts allow deeper drafting, higher player counts (up to 6–8), or complex deckbuilders:

| Architecture | Split Breakdown | Best Suited For | Examples / Mechanics |
| :--- | :--- | :--- | :--- |
| **The Color Quad (4 × 28)** | 4 colors × (0–9 numbered × 2 + 8 action cards) = 112 | Party shedding, high-variance race | *Uno*, *Phase 10*, *Skip-Bo* |
| **The 8-Player Draft (14 × 8)** | 8 packs/factions of 14 cards = 112 | 4–8 player drafting or 8 distinct hero factions | Battle-box, cube drafting |
| **Asymmetric Duel (56 / 56)** | 2 complete standalone 56-card asymmetric decks = 112 | Heavy tactical 1v1 duels with unique card pools | *Netrunner* micro-box, *Summoner Wars* |
| **5-Faction + Neutral** | 5 factions × 20 cards (100) + 10 neutral cards + 2 rules = 112 | 3–5 player area control or political influence | Multi-faction conflict games |
| **Deckbuilder Core Pool** | 4 × 12 starters (48) + 60 market cards + 4 player mats = 112 | 2–4 player full deckbuilding game | Standalone micro-deckbuilder |

---

## 4. Zero-Token Physical Design

Physical card games are dramatically faster to set up and cheaper to produce when they require **no external dice, chits, or plastic tokens**. Use cards to encode state:

1. **Card Orientation (Tapping / State Toggle)**:
   - **0° (Upright)**: Ready / Active.
   - **90° (Tapped)**: Exhausted / Cooldown / Defending.
   - **180° (Inverted)**: Flipped state with completely different bottom stats/action.
2. **Splaying & Tucking (Counters & Modifiers)**:
   - Cards tucked partially under a leader card with only the top or side edge visible displaying `+1 ATK` or an icon.
   - Stacked cards acting as Health/Armor (each damage discards 1 card from underneath).
3. **Multi-Use Cards**:
   - Top half: Action / Spell.
   - Bottom half: Resource / Currency value when discarded.
   - Corners: Suit/Number for secondary resolution (e.g. combat RNG check without dice).
4. **Cards as Game Boards & Tracks**:
   - 2 reference cards placed side-by-side with a 1–20 track; a face-down card slides along the track to record score/health.

---

## 5. Hypergeometric Math & Opening Hand Consistency

When designing deck size $N$, target copies $K$, hand draw size $n$, and wanting at least $k$ successes:

$$P(X \ge k) = \sum_{x=k}^{\min(n, K)} \frac{\binom{K}{x} \binom{N - K}{n - x}}{\binom{N}{n}}$$

### Key Benchmarks for 56-Card Decks:
- **Draw 5 from 56 cards**:
  - 1 copy in deck: **8.9%** chance in opening hand (Unreliable, legendary/one-off).
  - 3 copies in deck: **24.5%** chance in opening hand.
  - 4 copies in deck: **31.4%** chance in opening hand.
  - 7 copies (1 full archetype): **49.4%** chance (near 50/50).
  - 14 copies (1 full suit / resource): **76.8%** chance (reliable primary resource).
- **Resource Curve Rule of Thumb**:
  - If a player needs 1 resource card on Turn 1 with hand size 5, the deck must contain at least **14–16 resource cards** (~25–28% of a 56-card deck) to achieve >75% consistency without a mulligan.

---

## 6. Python Helper for Instant Probability Checks

When evaluating deck balance, run this in shell or scratch script:

```python
import math

def hypergeom_at_least(N, K, n, k):
    # N = deck size, K = total target cards in deck, n = cards drawn, k = desired copies
    def comb(a, b):
        return math.comb(a, b) if 0 <= b <= a else 0
    total = comb(N, n)
    prob = sum(comb(K, x) * comb(N - K, n - x) for x in range(k, min(n, K) + 1)) / total
    return prob

# Example: 56-card deck, 4 copies of key card, draw 5 cards, chance of at least 1:
print(f"P(>=1): {hypergeom_at_least(56, 4, 5, 1):.1%}")
```
