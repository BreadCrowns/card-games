# 🚨 Caught Red-Handed: The Black Market Edition
*A Knizian Heist Card Game of Selling Contraband, Framing Your Crew & Surviving the Raid*

---

## 1. Core Principles & Overview
* **Players**: 4 (Supports 3–5)
* **Match Length**: Exactly $N$ rounds (1 round per player as Dealer)
* **Win Condition**: Have the **most cash** at the end of the match without getting arrested.
* **Component Architecture (56 Cards)**:
  * **Card Faces**: Merchandise (`Blue` Clean Loot or `Red` Stolen Contraband)
  * **Card Backs**: **The Buyers** (Face-down cards on table are active Buyers demanding specific items and offering perks)
  * **Dealer Tokens**: 5 Siren Tokens (`[ 3 ]`, `[ 4 ]`, `[ 5 ]`, `[ 6 ]`, `[ 7 ]`) used once per match

---

## 2. Card Anatomy & The Knizian Economic Spectrum

| Category | Count & Items | Cash Payout | Heat Penalty | Powers |
| :--- | :--- | :---: | :---: | :--- |
| **Blue (Clean Loot)** | **24 Cards**<br>6 Items $\times$ 4 copies<br>💵 Cash, ⌚ Watch, 🗝️ Key, 💎 Diamond, 🖼️ Painting, 🧈 Gold | **$5** | **0 Heat** | **UNLOCKS POWERS!**<br>💵 *Bribe*: Skip mandatory pass<br>⌚ *Alibi*: Reverse table direction<br>🗝️ *Wiretap*: Look at target's hand<br>💎 *Vault*: Protect 1 card<br>🖼️ *Swap*: Swap 1 card with opponent<br>🧈 *Launder*: Discard 1 Red from hand |
| **Red (Stolen Contraband)** | **18 Cards**<br>6 Items $\times$ 3 copies | **$15** | **1 Heat (Sold)**<br>**2 Heat (Held in Hand)** | **NO POWERS!**<br>Pure financial muscle ($15) and pure radioactive liability. |
| **Buyer Backs (On Table)** | Displayed on the back of all cards | N/A | N/A | **3 Buyer Flavors**:<br>1. *Financial*: +$5 Cash Bonus<br>2. *Defensive*: Flip 1 Sold Red in Stash to Blue (0 Heat)<br>3. *Offensive*: Force target opponent to draw 1 card |
| **Siren Tokens** | 5 Tokens (`3, 4, 5, 6, 7`) | N/A | N/A | Sets Countdown Pile size |

---

## 3. Setup (Each Round)
1. Deal **5 cards** face-down to each player.
2. Establish **The Street (3 Active Buyers)**:
   * Top card of the face-down Evidence Locker is **Buyer #1**.
   * Deal 2 cards face-down next to it as **Buyer #2** and **Buyer #3**.
3. **The Dealer's Lever**:
   * The Dealer inspects their 5-card opening hand.
   * From the available Siren Tokens, the Dealer selects one (e.g. `[ 5 ]`). That token is discarded from the match.
   * The Dealer counts out that many cards from the deck into the **Countdown Pile** in the center.

---

## 4. Turn Structure

```
[1. SELL MERCHANDISE] ──▶ [2. TRIGGER POWERS] ──▶ [3. REFILL STREET] ──▶ [4. THE PLANT (PASS)]
```

1. **Sell Merchandise**:
   * Choose 1 card from your hand that matches an active Buyer's demanded item icon.
   * Place the face-down Buyer into your personal **Stash**.
   * Place your sold Merchandise **face-up on top of it**.
   * *Blue Sale*: Earn **$5**, add **0 Heat**.
   * *Red Sale*: Earn **$15**, add **1 Heat**.
2. **Trigger Powers**:
   * **Buyer Perk**: Resolves first (Bonus Cash, Flip a sold Red to Blue, or Force opponent draw).
   * **Blue Power**: If you sold Blue, execute its item ability! (If you sold Red, no power activates).
3. **Refill the Street**:
   * Slide a new card face-down from the Evidence Locker into the empty Buyer slot.
4. **The Plant (Mandatory Pass)**:
   * Choose 1 card from your hand and slide it **face-down to your neighbor** (in current direction).

---

## 5. The Dealer's Turn & The Countdown Pile
When the turn order reaches the Dealer:
1. The Dealer draws the top card of the center **Countdown Pile**.
2. **The Informant's Plant**: The Dealer inspects it and may either **keep it** or **slide it face-down to ANY player** at the table!
3. If that was the **final card** of the Countdown Pile:
   👉 **THE SIRENS WAIL! RAID! THE ROUND ENDS IMMEDIATELY!**

---

## 6. The Raid & Scoring
When the Raid triggers, all hands freeze:
$$\mathbf{Total\ Red\ Heat} = (\text{Red Cards Sold in Stash} \times 1) + (\text{Red Cards in Hand} \times 2)$$

1. **The Arrest**: The player(s) with the **HIGHEST Total Red Heat** are **ARRESTED**! Their round earnings are wiped to **$0**.
2. **The Haul**: All surviving clean players bank their total cash in their match bank.
3. **Match Victory**: After $N$ rounds (everyone has been Dealer once), the player with the most total accumulated cash wins!
