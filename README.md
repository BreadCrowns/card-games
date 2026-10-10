# Card Games Lab 🃏

A development space for brainstorming, designing, mathematically balancing, and digitally prototyping physical card games built strictly to **56-card** and **112-card** physical print sheet constraints.

---

## Games

### 1. [Caught Red-Handed](./caught-red-handed/)
* **Players**: 2–6 (Optimized for 4) | **Ages**: 7+ | **Length**: ~8–10 mins | **Deck Size**: 56 Cards
* **Genre**: Fast Shedding, Evidence Planting, Old Maid on Steroids
* **The Premise**: A heist went wrong! Draw from the Locker, discard matching loot pairs, and plant hot Red Contraband onto your neighbors before someone shouts **"I'M CLEAN!"**
* **Prototype**: Open [`caught-red-handed/prototype.html`](./caught-red-handed/prototype.html) directly in any web browser to play against 3 AI bots!
* **Rulebook**: Complete micro-rulebook in [`caught-red-handed/RULES.md`](./caught-red-handed/RULES.md).
* **Card Data**: Print-ready 56-card dataset in [`caught-red-handed/cards.json`](./caught-red-handed/cards.json).

---

## Built-in Design Skills (`.agents/skills/`)
This repo includes 4 specialized AI pair-design skills:
1. `deck-architect-56-112`: 56 & 112 sheet distribution math, hypergeometric probability calculators, zero-token design.
2. `card-game-mechanic-lab`: BGG mechanism taxonomy, micro-rulebook compression, tension curve design.
3. `card-digital-playtester`: Monte Carlo balance simulation engines and standalone HTML5 prototypes.
4. `card-pnp-generator`: 3x3 poker size (2.5" x 3.5") printable sheet layouts, TTS and PlayingCards.io export.
