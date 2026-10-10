---
name: card-digital-playtester
description: Rapidly builds standalone playable web prototypes (HTML/Tailwind/JS) and Monte Carlo balance test scripts for instant card game testing and bot simulation.
---

# Card Digital Playtester

Use this skill when you want to immediately test a card game concept, simulate deck balance, run hotseat playtests with a friend, or let an AI bot play 1,000 automated rounds to evaluate balance and first-player advantage.

---

## 1. Zero-Install Interactive Web Prototype

When prototyping an idea, build a self-contained, single-file HTML app in the workspace (e.g. `prototype.html`) that can be opened directly in any browser without needing `npm install` or compilation.

### Key Capabilities of the Prototype:
1. **Interactive Deck Engine**:
   - Deck array shuffle (`Fisher-Yates`).
   - Draw deck, discard pile, active player hand, table/lane slots.
2. **Play Modes**:
   - **Hotseat (2-Player)**: Player 1 and Player 2 share the screen, with a "Pass Device / Flip Hand" curtain if hidden hands are needed.
   - **Solo vs Simple Bot**: AI chooses plays using a simple heuristic (e.g. highest value legal card, greedily capturing lanes).
3. **Action Log Feed**:
   - Scrollable log recording every card drawn, played, and state change for post-play review.
4. **Card Hover / Inspect**:
   - Hover or click opens a larger inspection preview of the card with rules text and stats.

---

## 2. Minimalist Single-File Prototype Structure

Use Tailwind via CDN and Vanilla JS:

```html
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>Card Game Prototype</title>
  <script src="https://cdn.tailwindcss.com"></script>
</head>
<body class="bg-slate-900 text-slate-100 min-h-screen flex flex-col p-4 select-none">
  <!-- Top Bar: Scores / Deck Counters -->
  <header class="flex justify-between items-center mb-4 pb-2 border-b border-slate-700">
    <div class="font-bold text-lg text-amber-400" id="game-title">Game Prototype</div>
    <div class="flex gap-4 text-sm" id="status-bar">
      <span>Deck: <b id="deck-count">0</b></span>
      <span>Discard: <b id="discard-count">0</b></span>
      <span>Turn: <b id="current-turn">P1</b></span>
    </div>
  </header>

  <!-- Tabletop / Lanes Area -->
  <main class="flex-1 grid grid-cols-3 gap-4 mb-4" id="board-area">
    <!-- Active card slots / lanes rendered here -->
  </main>

  <!-- Player Hand & Controls -->
  <footer class="bg-slate-800/80 rounded-xl p-4 border border-slate-700">
    <div class="flex justify-between items-center mb-2">
      <span class="text-sm font-semibold text-slate-400">YOUR HAND</span>
      <button id="end-turn-btn" class="px-4 py-1.5 bg-amber-500 hover:bg-amber-400 text-slate-950 font-bold rounded-lg text-sm transition">End Turn</button>
    </div>
    <div class="flex gap-2 overflow-x-auto pb-2 justify-center" id="player-hand">
      <!-- Cards rendered here -->
    </div>
  </footer>

  <script>
    // State, Deck definition, Shuffle, and Turn logic here
  </script>
</body>
</html>
```

---

## 3. Monte Carlo Headless Balance Tester

When you need objective data on balance, create a lightweight Python or Node.js script to simulate games between two automated bots:

### Metrics to Track:
- **P1 vs P2 Win Rate**: Ideal is 48%–52%. If P1 wins >60%, the first-player advantage is too severe.
- **Game Length**: Number of turns/rounds until victory. Look for outliers (games that drag on forever or end on Turn 1).
- **Dead Draw Rate**: Percentage of turns where a player has 0 legal plays.
- **Card Impact Score**: Win rate of players when a specific card is drawn vs when it is not.

### Python Simulation Blueprint:

```python
import random
from collections import Counter

def run_simulation(num_games=1000):
    wins = {"P1": 0, "P2": 0, "Draw": 0}
    turn_counts = []

    for _ in range(num_games):
        winner, turns = simulate_single_game()
        wins[winner] += 1
        turn_counts.append(turns)

    print(f"Results across {num_games} games:")
    print(f"P1 Win Rate: {wins['P1'] / num_games:.1%}")
    print(f"P2 Win Rate: {wins['P2'] / num_games:.1%}")
    print(f"Average Turn Count: {sum(turn_counts) / len(turn_counts):.1f}")

def simulate_single_game():
    # Implement quick turn loop and simple bot choices
    return ("P1" if random.random() > 0.5 else "P2"), random.randint(4, 9)

if __name__ == "__main__":
    run_simulation(1000)
```
