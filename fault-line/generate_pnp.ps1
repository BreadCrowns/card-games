$cards = Get-Content -Raw -Path "$PSScriptRoot\cards.json" | ConvertFrom-Json

$colorThemes = @{
    "crimson"  = @{ border = "#dc2626"; text = "#b91c1c"; bg = "#fef2f2"; accent = "#dc2626"; name = "Crimson" }
    "amber"    = @{ border = "#d97706"; text = "#b45309"; bg = "#fffbeb"; accent = "#d97706"; name = "Amber" }
    "emerald"  = @{ border = "#059669"; text = "#047857"; bg = "#ecfdf5"; accent = "#059669"; name = "Emerald" }
    "cobalt"   = @{ border = "#2563eb"; text = "#1d4ed8"; bg = "#eff6ff"; accent = "#2563eb"; name = "Cobalt" }
    "amethyst" = @{ border = "#7c3aed"; text = "#6d28d9"; bg = "#f5f3ff"; accent = "#7c3aed"; name = "Amethyst" }
    "anchor"   = @{ border = "#eab308"; text = "#854d0e"; bg = "#fafaf9"; accent = "#18181b"; name = "Epicenter" }
}

$html = @"
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>Fault Line - Low-Ink Print & Play Poker Deck (8.5x11)</title>
  <style>
    @import url('https://fonts.googleapis.com/css2?family=Cinzel:wght@700;900&family=Montserrat:wght@500;700;800;900&display=swap');
    @page { size: letter portrait; margin: 0.25in 0.5in 0.25in 0.5in; }
    * { box-sizing: border-box; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
    body { margin: 0; padding: 0; font-family: 'Montserrat', sans-serif; background: #f8fafc; color: #0f172a; }
    .no-print-bar { background: #0f172a; color: #f8fafc; padding: 12px 24px; display: flex; justify-content: space-between; align-items: center; position: sticky; top: 0; z-index: 1000; box-shadow: 0 4px 12px rgba(0,0,0,0.15); }
    .no-print-bar button { background: #d97706; color: white; border: none; padding: 8px 20px; border-radius: 8px; font-weight: 800; font-size: 14px; cursor: pointer; }
    .no-print-bar button:hover { background: #b45309; }
    @media print {
      .no-print-bar, .sheet-header-label { display: none !important; }
      body { background: #ffffff !important; margin: 0 !important; padding: 0 !important; }
      .sheet-wrapper { padding: 0 !important; margin: 0 !important; }
      .sheet { margin: 0 auto !important; box-shadow: none !important; page-break-after: always !important; page-break-inside: avoid !important; }
    }
    .sheet-wrapper { padding: 20px 0; }
    .sheet-header-label { text-align: center; font-size: 11px; font-weight: 700; color: #64748b; margin-bottom: 6px; text-transform: uppercase; letter-spacing: 0.1em; }
    .sheet { width: 7.5in; height: 10.5in; display: grid; grid-template-columns: repeat(3, 2.5in); grid-template-rows: repeat(3, 3.5in); gap: 0; margin: 0 auto 30px auto; background: #ffffff; box-shadow: 0 4px 16px rgba(0,0,0,0.06); }
    .card { width: 2.5in; height: 3.5in; border: 0.75px dashed #cbd5e1; padding: 0.08in; position: relative; display: flex; flex-direction: column; justify-content: space-between; overflow: hidden; background: #ffffff; user-select: none; }
    .card-inner { width: 100%; height: 100%; border-radius: 8px; border: 2px solid; display: flex; flex-direction: column; justify-content: space-between; padding: 0.1in; position: relative; overflow: hidden; }
    .header-row { display: flex; justify-content: space-between; align-items: flex-start; width: 100%; }
    .index-num { font-size: 16px; font-weight: 900; line-height: 1; font-family: 'Cinzel', serif; }
    .strike-stars { font-size: 11px; color: #d97706; font-family: monospace; letter-spacing: -1px; }
    .center-art { display: flex; flex-direction: column; align-items: center; justify-content: center; margin: auto 0; text-align: center; }
    .art-num { font-size: 42px; font-weight: 900; font-family: 'Cinzel', serif; line-height: 0.9; }
    .art-color { font-size: 9px; font-weight: 800; text-transform: uppercase; letter-spacing: 1px; margin-top: 4px; }
    .anchor-badge { background: #18181b; color: #facc15; font-size: 8px; font-weight: 800; padding: 2px 6px; border-radius: 4px; text-transform: uppercase; letter-spacing: 1px; }
    .inverted { transform: rotate(180deg); }
  </style>
</head>
<body>
  <div class="no-print-bar">
    <div>
      <strong style="font-size: 16px;">⚡ FAULT LINE — Print & Play Deck (56 Cards)</strong>
      <div style="font-size: 11px; color: #94a3b8;">Standard 2.5" x 3.5" Poker Cards • 9 Cards / Sheet (3x3 Grid) • Low-Ink</div>
    </div>
    <button onclick="window.print()">🖨️ Print PnP Deck</button>
  </div>
"@

$cardsList = [System.Collections.Generic.List[PSObject]]::new()
foreach ($c in $cards) { $cardsList.Add($c) }

# Add 7 Reference / Aid Cards to make 63 cards (exactly 7 sheets of 9 cards)
for ($r = 1; $r -le 7; $r++) {
    $cardsList.Add([PSCustomObject]@{
        id = "ref_$r"
        number = 0
        color = "anchor"
        colorName = "Rules Aid"
        strikes = 0
        isRef = $true
        refTitle = if ($r -eq 1) { "HOW TO PLAY" } elseif ($r -eq 2) { "TURN ACTIONS" } elseif ($r -eq 3) { "SWEEPS & STRIKES" } else { "PLAYER REFERENCE" }
    })
}

$sheetNum = 1
$totalCards = $cardsList.Count
for ($i = 0; $i -lt $totalCards; $i += 9) {
    $html += @"
  <div class="sheet-wrapper">
    <div class="sheet-header-label">Sheet $sheetNum of 7 (Cards $($i + 1) to $([Math]::Min($i + 9, $totalCards)))</div>
    <div class="sheet">
"@
    for ($j = 0; $j -lt 9; $j++) {
        $idx = $i + $j
        if ($idx -lt $totalCards) {
            $c = $cardsList[$idx]
            $theme = $colorThemes[$c.color]
            
            if ($c.isRef) {
                # Reference Card
                $html += @"
      <div class="card">
        <div class="card-inner" style="border-color: #475569; background: #f8fafc; color: #1e293b;">
          <div class="header-row">
            <span style="font-size: 9px; font-weight: 800; color: #64748b;">FAULT LINE AID</span>
            <span class="anchor-badge" style="background: #334155; color: #fff;">REFERENCE</span>
          </div>
          <div class="center-art">
            <div style="font-size: 12px; font-weight: 900; color: #0f172a; margin-bottom: 4px;">$($c.refTitle)</div>
            <div style="font-size: 7.5px; line-height: 1.3; color: #334155; text-align: left; padding: 0 4px;">
              <strong>WING LAW:</strong> Cards &lt; 28 on Left; Cards &gt; 28 on Right.<br>
              <strong>1. OUTER END:</strong> Place on outer edge. Remove any cards towards Anchor that break ascending order.<br>
              <strong>2. COVER COLOR:</strong> Stack on matching color. Remove adjacent cards that break order.<br>
              <strong>STACKS:</strong> Swept piles give ALL cards in that stack as strikes!<br>
              <strong>ROUNDS:</strong> R1: 3 cards • R2: 4 cards • R3: 5 cards. Fewest strikes wins!
            </div>
          </div>
          <div class="header-row inverted">
            <span style="font-size: 9px; font-weight: 800; color: #64748b;">FAULT LINE AID</span>
            <span class="anchor-badge" style="background: #334155; color: #fff;">REFERENCE</span>
          </div>
        </div>
      </div>
"@
            } elseif ($c.isAnchor) {
                # Anchor Card
                $html += @"
      <div class="card">
        <div class="card-inner" style="border-color: #eab308; background: #fafaf9; border-width: 3px;">
          <div class="header-row">
            <div class="index-num" style="color: #854d0e;">28</div>
            <span class="anchor-badge">IMMUNE</span>
          </div>
          <div class="center-art">
            <div class="art-num" style="color: #0f172a;">28</div>
            <div class="art-color" style="color: #854d0e; font-weight: 900; letter-spacing: 2px;">THE EPICENTER</div>
            <div style="font-size: 7.5px; font-weight: 700; color: #71717a; margin-top: 3px;">PERMANENT ANCHOR • NEVER REMOVED</div>
          </div>
          <div class="header-row inverted">
            <div class="index-num" style="color: #854d0e;">28</div>
            <span class="anchor-badge">IMMUNE</span>
          </div>
        </div>
      </div>
"@
            } else {
                # Regular Playable Card
                $stars = "★" * $c.strikes
                $html += @"
      <div class="card">
        <div class="card-inner" style="border-color: $($theme.border); background: $($theme.bg);">
          <div class="header-row">
            <div class="index-num" style="color: $($theme.text);">$($c.number)</div>
            <div class="strike-stars">$stars</div>
          </div>
          <div class="center-art">
            <div class="art-num" style="color: $($theme.text);">$($c.number)</div>
            <div class="art-color" style="color: $($theme.text);">$($theme.name)</div>
            <div style="font-size: 7.5px; font-weight: 700; color: #64748b; margin-top: 2px;">$($c.strikes) STRIKE$(if ($c.strikes -gt 1) { 'S' })</div>
          </div>
          <div class="header-row inverted">
            <div class="index-num" style="color: $($theme.text);">$($c.number)</div>
            <div class="strike-stars">$stars</div>
          </div>
        </div>
      </div>
"@
            }
        }
    }
    $html += @"
    </div>
  </div>
"@
    $sheetNum++
}

$html += @"
</body>
</html>
"@

Set-Content -Path "$PSScriptRoot\print_deck.html" -Value $html -Encoding UTF8
Write-Host "Successfully generated print_deck.html ($totalCards cards across 7 sheets)!"
