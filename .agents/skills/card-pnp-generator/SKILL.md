---
name: card-pnp-generator
description: Generates print-and-play (PnP) 9-card (3x3) poker-size printable HTML/PDF sheets with cut/bleed marks, card dataset schemas (CSV/JSON), and Tabletop Simulator / PlayingCards.io deck exports.
---

# Card PnP & Virtual Tabletop Generator

Use this skill when converting a brainstormed card list into tangible physical printouts (Print-and-Play / PnP) or importing your deck into Tabletop Simulator (TTS) and PlayingCards.io.

---

## 1. Physical Poker Card Printing Specs

Standard physical poker card dimensions:
- **Finished Trim Size**: 2.5 in × 3.5 in (63.5 mm × 88.9 mm).
- **Home Printing Layout**: **3 × 3 grid (9 cards per page)** on standard US Letter (8.5" × 11") or A4 paper.
- **56-Card Breakdown**: 6 full 9-card sheets (54 cards) + 1 partial sheet (2 cards + 7 blank/notes) = 56 cards total.
- **112-Card Breakdown**: 12 full 9-card sheets (108 cards) + 1 partial sheet (4 cards) = 112 cards total.

---

## 2. Print-Ready HTML/CSS Template (`@media print`)

To print cards directly with 100% exact physical scaling using any browser (Chrome/Edge $\rightarrow$ Ctrl+P $\rightarrow$ Margins: None $\rightarrow$ Scale: 100%):

```html
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>PnP Card Sheet</title>
  <style>
    @page {
      size: letter portrait;
      margin: 0.5in 0.5in 0.5in 0.5in;
    }
    body {
      margin: 0;
      padding: 0;
      font-family: system-ui, -apple-system, sans-serif;
      background: #fff;
    }
    .sheet {
      display: grid;
      grid-template-columns: repeat(3, 2.5in);
      grid-template-rows: repeat(3, 3.5in);
      gap: 0;
      page-break-after: always;
      justify-content: center;
      margin-bottom: 0.5in;
    }
    .card {
      width: 2.5in;
      height: 3.5in;
      box-sizing: border-box;
      border: 1px dashed #bbb; /* Cut guide */
      position: relative;
      padding: 0.2in;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      overflow: hidden;
      background: white;
    }
    .cost-badge {
      position: absolute;
      top: 0.15in;
      left: 0.15in;
      font-weight: bold;
      font-size: 1.1rem;
    }
    .card-title {
      font-size: 0.95rem;
      font-weight: 800;
      margin-left: 0.35in;
      text-transform: uppercase;
    }
    .card-art-placeholder {
      height: 1.3in;
      background: #f1f5f9;
      border: 1px solid #e2e8f0;
      border-radius: 4px;
      display: flex;
      align-items: center;
      justify-content: center;
      color: #94a3b8;
      font-size: 0.75rem;
    }
    .card-body {
      font-size: 0.75rem;
      line-height: 1.25;
      color: #1e293b;
    }
    .card-footer {
      display: flex;
      justify-content: space-between;
      font-size: 0.8rem;
      font-weight: bold;
      border-top: 1px solid #e2e8f0;
      padding-top: 0.05in;
    }
  </style>
</head>
<body>
  <!-- 9 cards per sheet -->
  <div class="sheet" id="sheet-1">
    <!-- Card elements -->
  </div>
</body>
</html>
```

---

## 3. Card Dataset Schema (JSON & CSV)

Maintain the master card definitions in a simple structured format (`cards.json` or `cards.csv`):

```json
[
  {
    "id": "c01",
    "name": "Shield Guardian",
    "suit": "Iron",
    "cost": 2,
    "atk": 1,
    "def": 4,
    "type": "Unit",
    "text": "Taunt. When attacked, draw 1 card.",
    "copies": 3
  }
]
```

---

## 4. Virtual Tabletop Export Formats

### A. Tabletop Simulator (TTS)
- TTS custom decks use a single stitched image grid: **10 cards wide by 7 cards tall** (up to 70 cards per sheet).
- For a **56-card deck**: Fits completely within a single TTS 10×7 sheet (56 card fronts + slot 70 reserved for hidden card back).
- For a **112-card deck**: Export as **2 sheets** of 56 cards each.

### B. PlayingCards.io
- Import custom card decks using a flat CSV with columns:
  - `label`: Card Name
  - `suit`: Category / Faction
  - `rules`: Rules text
  - `quantity`: Number of copies in deck
- PlayingCards.io renders these directly using standard text-box widgets without needing pre-rendered graphics.
