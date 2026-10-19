---
name: card-designer
description: Comprehensive physical card game design guidelines, table-first ergonomics, Holy Trinity parity (Rules, Prototype, Print), simulation validation, and design principles.
---

# Card Designer: Core Principles & Workflow

This skill guides the design, iteration, balance, and production of physical card games. It codifies hard-won lessons from real card game prototyping, Monte Carlo testing, and physical playability.

---

## 1. Core Operating Principles

### A. Simplicity is Sacred (The "Uno Standard")
* **No Band-Aid Rules**: Never patch an edge case (like an endgame standoff) by adding an exception or secondary rule that contradicts the game's core mental model (e.g., if the rule is *"every pair requires a Blue card"*, do not allow Green cards to pair with Red cards just to break a tie).
* **Component-Level Fixes Over Rule Bloat**: Solve problems through deck composition, card counts, or component cues before inventing new rules.
* **Avoid Pure Liability Cards**: If a card is 100% danger with zero upside, passing or discarding it becomes an autopilot reflex. Always give players light tactical tension (e.g., combo-laundering bonuses, face-up vs face-down passing, or push-your-luck point tradeoffs).

### B. Explicit Trade-Offs (No Hidden Cuts)
* **Never Silently Adjust Deck Math**: When adding new cards (like special artifacts or reference cards) to fit standard manufacturing boundaries (56 or 112 cards), **never quietly cut an existing card** to make room.
* **Present Layout Options Explicitly**: Lay out the trade-off (e.g., *"We can make this a 55-card play deck + 1 double-sided camera card to reach 56, or expand to 60"*) and confirm before altering files.

### C. The "Holy Trinity" Synchronization Law
Any gameplay, card count, scoring, or mechanical adjustment must be updated **simultaneously** across all three pillars:
1. **`RULES.md`**: The definitive single source of truth for all human rules.
2. **Interactive Prototype (`prototype.html`)**: The digital testbed for tactile feel, solo testing, and bot AI validation.
3. **Print-and-Play Deck (`print_deck.html` / PDF)**: The physical production artifact ready for cutting and sleeving.

*If a rule changes in one file, update the other two immediately to avoid state desynchronization.*

### D. Simulation-Backed Validation
* **Verify with Code, Don't Guess**: For any card ratio or victory condition adjustment, write or run a Monte Carlo simulation (10,000+ hands) to quantitatively verify:
  * Standoff / infinite loop frequency (must be 0%).
  * Average turn count and game duration.
  * Win-rate fairness across turn order (Player 1 advantage check).
  * Draw consistency using hypergeometric distributions.

---

## 2. Table-First Ergonomics & Physical Card Anatomy

Physical cards must be designed for human hands, small tables, and home printers:

1. **Corner Indexing (Fanning Check)**:
   * Key suit icons, numbers, and colors must sit in the **top-left corner** (and opposite bottom-right) so they remain visible when fanned in a player's left hand.
2. **Two-Way Rotational Symmetry**:
   * Card layouts and icons should read correctly whether held right-side up or upside down, unless 180° rotation is an explicit game mechanic.
3. **Dedicated Physical State Markers**:
   * Game states (e.g., turn direction, active player, security camera) must have dedicated, high-contrast physical cards with bold vector graphics (e.g., double-sided circular arrows), rather than relying on player memory or digital-only HUDs.
4. **Printer-Friendly / Low-Ink Modes**:
   * At-home playtesters print on desktop inkjet printers. Ensure a minimal-ink mode is available: white backgrounds, crisp thin borders, and high contrast without heavy solid black fills.

---

## 3. Card Micro-Copy & Rule Precision

* **The "7-Year-Old Test"**: Card text must be 100% literal and completely unambiguous for first-time players.
* **Precision Over Brevity**: Never trim words if it introduces confusion.
  * *Bad*: "Pairs with any card and flips camera." (Does it pair with Red? Does it pair with another Wild?)
  * *Good*: "Can ONLY pair with any BLUE card. When discarded, immediately flip the Camera Card."
* **Standardized Keywords**: Bold active verbs (`Draw`, `Pair`, `Pass`, `Launder`, `Bust`) and keep terminology strictly consistent across card faces and rules text.
