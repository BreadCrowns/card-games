using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace PnpDeckGenerator
{
    public class CardDef
    {
        public string Item;
        public string Name;
        public string Color; // BLUE, RED, GREEN, SPECIAL
        public int Val;
        public int Copies;
        public string Icon;
        public bool IsGrand;
    }

    public class Program
    {
        public static void Main()
        {
            string outHtmlPath = @"c:\Users\bazar\Documents\antigravity\lively-rutherford\caught-red-handed\print_deck.html";

            var cardDefs = new List<CardDef>
            {
                // Petty Clean (Blue) - 16 cards
                new CardDef { Item = "Cash", Name = "Cash Stack", Color = "BLUE", Val = 5, Copies = 4, Icon = "💵", IsGrand = false },
                new CardDef { Item = "Diamond", Name = "Cut Diamond", Color = "BLUE", Val = 5, Copies = 4, Icon = "💎", IsGrand = false },
                new CardDef { Item = "Watch", Name = "Gold Watch", Color = "BLUE", Val = 5, Copies = 4, Icon = "⌚", IsGrand = false },
                new CardDef { Item = "Key", Name = "Vault Key", Color = "BLUE", Val = 5, Copies = 4, Icon = "🗝️", IsGrand = false },

                // Grand Clean (Blue) - 14 cards
                new CardDef { Item = "Painting", Name = "Masterpiece", Color = "BLUE", Val = 5, Copies = 4, Icon = "🖼️", IsGrand = true },
                new CardDef { Item = "Gold", Name = "Gold Ingot", Color = "BLUE", Val = 5, Copies = 4, Icon = "🧈", IsGrand = true },
                new CardDef { Item = "Crown", Name = "Royal Crown", Color = "BLUE", Val = 5, Copies = 3, Icon = "👑", IsGrand = true },
                new CardDef { Item = "Relic", Name = "Ancient Relic", Color = "BLUE", Val = 5, Copies = 3, Icon = "🏺", IsGrand = true },

                // Petty Contraband (Red) - 8 cards
                new CardDef { Item = "Cash", Name = "Marked Cash", Color = "RED", Val = 10, Copies = 2, Icon = "💵", IsGrand = false },
                new CardDef { Item = "Diamond", Name = "Blood Diamond", Color = "RED", Val = 10, Copies = 2, Icon = "💎", IsGrand = false },
                new CardDef { Item = "Watch", Name = "Tracked Watch", Color = "RED", Val = 10, Copies = 2, Icon = "⌚", IsGrand = false },
                new CardDef { Item = "Key", Name = "Evidence Key", Color = "RED", Val = 10, Copies = 2, Icon = "🗝️", IsGrand = false },

                // Grand Contraband (Red) - 8 cards
                new CardDef { Item = "Painting", Name = "Stolen Canvas", Color = "RED", Val = 15, Copies = 2, Icon = "🖼️", IsGrand = true },
                new CardDef { Item = "Gold", Name = "Hot Gold Bar", Color = "RED", Val = 15, Copies = 2, Icon = "🧈", IsGrand = true },
                new CardDef { Item = "Crown", Name = "Cursed Crown", Color = "RED", Val = 15, Copies = 2, Icon = "👑", IsGrand = true },
                new CardDef { Item = "Relic", Name = "Smuggled Relic", Color = "RED", Val = 15, Copies = 2, Icon = "🏺", IsGrand = true },

                // Counterfeit Wilds (Green) - 6 cards
                new CardDef { Item = "Wild", Name = "Fake Receipt", Color = "GREEN", Val = 0, Copies = 6, Icon = "🎭", IsGrand = false },

                // The Snitch (Special / Slate) - 2 cards
                new CardDef { Item = "Snitch", Name = "The Snitch", Color = "SPECIAL", Val = 0, Copies = 2, Icon = "🕵️", IsGrand = false }
            };

            // Exactly 54 play cards (30 Blue + 16 Red + 6 Green + 2 Snitch)
            var deck = new List<CardDef>();
            foreach (var def in cardDefs)
            {
                for (int i = 0; i < def.Copies; i++)
                    deck.Add(def);
            }

            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset=\"UTF-8\">");
            sb.AppendLine("  <title>Caught Red-Handed: Trios & The Snitch - Low-Ink PnP Poker Deck (8.5x11)</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@500;600;700;800;900&display=swap');");
            sb.AppendLine("    @page { size: letter portrait; margin: 0.25in 0.5in 0.25in 0.5in; }");
            sb.AppendLine("    * { box-sizing: border-box; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }");
            sb.AppendLine("    body { margin: 0; padding: 0; font-family: 'Montserrat', system-ui, sans-serif; background: #f8fafc; color: #0f172a; }");
            sb.AppendLine("    .no-print-bar { background: #0f172a; color: #f8fafc; padding: 12px 24px; display: flex; justify-content: space-between; align-items: center; position: sticky; top: 0; z-index: 1000; box-shadow: 0 4px 12px rgba(0,0,0,0.15); }");
            sb.AppendLine("    .no-print-bar button { background: #0284c7; color: white; border: none; padding: 8px 20px; border-radius: 8px; font-weight: 800; font-size: 14px; cursor: pointer; display: flex; align-items: center; gap: 8px; transition: background 0.2s; }");
            sb.AppendLine("    .no-print-bar button:hover { background: #0369a1; }");
            sb.AppendLine("    @media print {");
            sb.AppendLine("      .no-print-bar, .sheet-header-label { display: none !important; }");
            sb.AppendLine("      body { background: #ffffff !important; margin: 0 !important; padding: 0 !important; }");
            sb.AppendLine("      .sheet-wrapper { padding: 0 !important; margin: 0 !important; }");
            sb.AppendLine("      .sheet { margin: 0 auto !important; box-shadow: none !important; page-break-after: always !important; page-break-inside: avoid !important; }");
            sb.AppendLine("    }");
            sb.AppendLine("    .sheet-wrapper { padding: 20px 0; }");
            sb.AppendLine("    .sheet-header-label { text-align: center; font-size: 11px; font-weight: 700; color: #64748b; margin-bottom: 6px; text-transform: uppercase; letter-spacing: 0.1em; }");
            sb.AppendLine("    .sheet { width: 7.5in; height: 10.5in; display: grid; grid-template-columns: repeat(3, 2.5in); grid-template-rows: repeat(3, 3.5in); gap: 0; margin: 0 auto 30px auto; background: #ffffff; box-shadow: 0 4px 16px rgba(0,0,0,0.06); position: relative; }");
            sb.AppendLine("    .card { width: 2.5in; height: 3.5in; border: 0.75px dashed #cbd5e1; padding: 0.08in; position: relative; display: flex; flex-direction: column; justify-content: space-between; overflow: hidden; background: #ffffff; user-select: none; }");
            sb.AppendLine("    .card-inner { width: 100%; height: 100%; border-radius: 8px; border: 1.5px solid #cbd5e1; display: flex; flex-direction: column; justify-content: space-between; padding: 0.08in; position: relative; overflow: hidden; background: #ffffff; }");
            sb.AppendLine("    .header-block { display: flex; flex-direction: column; width: 100%; z-index: 2; }");
            sb.AppendLine("    .header-row { display: flex; justify-content: space-between; align-items: flex-start; width: 100%; }");
            sb.AppendLine("    .index-group { display: flex; flex-direction: column; line-height: 1; }");
            sb.AppendLine("    .corner-top { display: flex; align-items: center; gap: 3px; }");
            sb.AppendLine("    .corner-icon { font-size: 15px; line-height: 1; }");
            sb.AppendLine("    .index-val { font-size: 14px; font-weight: 900; letter-spacing: -0.5px; }");
            sb.AppendLine("    .index-type { font-size: 7px; font-weight: 800; text-transform: uppercase; letter-spacing: 0.4px; margin-top: 1.5px; }");
            sb.AppendLine("    .pill { font-size: 7.5px; font-weight: 800; padding: 2px 5px; border-radius: 4px; border: 1.2px solid; text-transform: uppercase; letter-spacing: 0.2px; display: inline-flex; align-items: center; gap: 2px; background: #ffffff; }");
            sb.AppendLine("    .sub-rule-bar { font-size: 6.8px; font-weight: 700; text-align: center; border-radius: 2px; padding: 1.5px 3px; margin-top: 2px; background: #ffffff; border-top: 1px solid #e2e8f0; border-bottom: 1px solid #e2e8f0; line-height: 1.15; color: #334155; }");
            sb.AppendLine("    .center-art { display: flex; flex-direction: column; align-items: center; justify-content: center; margin: auto 0; text-align: center; z-index: 2; }");
            sb.AppendLine("    .art-icon { font-size: 34px; line-height: 1; }");
            sb.AppendLine("    .art-name { font-size: 12px; font-weight: 900; text-transform: uppercase; letter-spacing: 0.4px; margin-top: 2px; line-height: 1.1; color: #0f172a; }");
            sb.AppendLine("    .art-tier { font-size: 7.5px; font-weight: 800; text-transform: uppercase; letter-spacing: 0.6px; margin-top: 1px; }");
            sb.AppendLine("    .inverted { transform: rotate(180deg); }");

            // Printer-Friendly Clean White Color Themes (No heavy fills, razor-sharp outlines)
            sb.AppendLine("    .card.blue .card-inner { border-color: #0284c7; background: #ffffff; color: #0369a1; }");
            sb.AppendLine("    .card.blue .index-val { color: #0284c7; }");
            sb.AppendLine("    .card.blue .index-type { color: #0369a1; }");
            sb.AppendLine("    .card.blue .art-name { color: #0c4a6e; }");
            sb.AppendLine("    .card.blue .art-tier { color: #0284c7; }");
            sb.AppendLine("    .card.blue .pill { background: #ffffff; border-color: #0284c7; color: #0284c7; }");

            sb.AppendLine("    .card.blue.grand .card-inner { border-color: #0284c7; }");
            sb.AppendLine("    .card.blue.grand .pill { background: #ffffff; border-color: #b45309; color: #b45309; }");
            sb.AppendLine("    .card.blue.grand .art-tier { color: #b45309; font-weight: 900; }");

            sb.AppendLine("    .card.red .card-inner { border-color: #dc2626; background: #ffffff; color: #b91c1c; }");
            sb.AppendLine("    .card.red .index-val { color: #dc2626; }");
            sb.AppendLine("    .card.red .index-type { color: #b91c1c; }");
            sb.AppendLine("    .card.red .art-name { color: #881337; }");
            sb.AppendLine("    .card.red .art-tier { color: #dc2626; }");
            sb.AppendLine("    .card.red .pill { background: #ffffff; border-color: #dc2626; color: #dc2626; font-weight: 900; }");

            sb.AppendLine("    .card.red.grand .card-inner { border-color: #b91c1c; }");
            sb.AppendLine("    .card.red.grand .pill { background: #ffffff; border-color: #991b1b; color: #991b1b; }");

            sb.AppendLine("    .card.green .card-inner { border-color: #16a34a; background: #ffffff; color: #15803d; }");
            sb.AppendLine("    .card.green .index-val { color: #16a34a; }");
            sb.AppendLine("    .card.green .index-type { color: #15803d; }");
            sb.AppendLine("    .card.green .art-name { color: #064e3b; }");
            sb.AppendLine("    .card.green .art-tier { color: #16a34a; }");
            sb.AppendLine("    .card.green .pill { background: #ffffff; border-color: #16a34a; color: #16a34a; }");

            // The Snitch Special Slate Theme
            sb.AppendLine("    .card.special .card-inner { border-color: #475569; background: #ffffff; color: #334155; }");
            sb.AppendLine("    .card.special .index-val { color: #d97706; }");
            sb.AppendLine("    .card.special .index-type { color: #64748b; }");
            sb.AppendLine("    .card.special .art-name { color: #0f172a; }");
            sb.AppendLine("    .card.special .art-tier { color: #dc2626; font-weight: 900; }");
            sb.AppendLine("    .card.special .pill { background: #ffffff; border-color: #dc2626; color: #dc2626; font-weight: 900; }");

            // Printer-Friendly Camera Markers & Card Backs (Pure White, line-art, zero black fills)
            sb.AppendLine("    .card.marker .card-inner { background: #ffffff; color: #0f172a; border-width: 1.5px; }");
            sb.AppendLine("    .card.card-back .card-inner { background: #ffffff; border: 2px solid #0f172a; color: #0f172a; display: flex; flex-direction: column; align-items: center; justify-content: center; text-align: center; padding: 0.15in; position: relative; }");
            sb.AppendLine("    .card-back-pattern { position: absolute; inset: 4px; border: 1px dashed #cbd5e1; border-radius: 5px; pointer-events: none; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <div class=\"no-print-bar\">");
            sb.AppendLine("    <div><span style=\"font-weight: 900; font-size: 16px; color: #38bdf8;\">CAUGHT RED-HANDED</span><span style=\"color: #94a3b8; font-size: 13px; margin-left: 8px;\">• 54-Card Trios & Snitch Deck (8.5\" x 11\")</span><span style=\"background:#059669; color:#fff; font-size:10px; font-weight:800; padding:2px 8px; border-radius:12px; margin-left:12px;\">🌱 ECO INK-SAVER</span></div>");
            sb.AppendLine("    <button onclick=\"window.print()\">🖨️ PRINT DECK TO PDF / PRINTER</button>");
            sb.AppendLine("  </div>");
            sb.AppendLine("  <div class=\"sheet-wrapper\">");

            // 6 sheets of 9 cards = Exactly 54 play cards
            string[] sheetTitles = new string[] {
                "Sheet 1 of 8: Clean Petty Loot (Cash & Diamonds)",
                "Sheet 2 of 8: Clean Petty & Grand Loot (Watches, Keys & Paintings)",
                "Sheet 3 of 8: Clean Grand Loot (Paintings, Gold & Crowns)",
                "Sheet 4 of 8: Relics & Red Contraband (Cash, Diamonds, Watches)",
                "Sheet 5 of 8: Red Contraband (Keys, Paintings, Gold, Crowns & Relic)",
                "Sheet 6 of 8: Relic Contraband, 6 Counterfeit Wilds & 2 Snitches"
            };

            for (int s = 0; s < 6; s++)
            {
                sb.AppendLine(string.Format("    <div class=\"sheet-header-label\">{0}</div>", sheetTitles[s]));
                sb.AppendLine("    <div class=\"sheet\">");

                for (int c = 0; c < 9; c++)
                {
                    int idx = s * 9 + c;
                    sb.Append(RenderCardHtml(deck[idx]));
                }

                sb.AppendLine("    </div>");
            }

            // Sheet 7: Double-Sided Camera Marker & Reference Rules Cards (9 cards total)
            sb.AppendLine("    <div class=\"sheet-header-label\">Sheet 7 of 8: Double-Sided Camera Marker, Trios, Snitch & Reference Cards</div>");
            sb.AppendLine("    <div class=\"sheet\">");

            // Card 1 on Sheet 7: Clockwise Camera Marker (Front: Pass Left)
            sb.AppendLine("      <div class=\"card marker\">");
            sb.AppendLine("        <div class=\"card-inner\" style=\"border-color: #0284c7;\">");
            sb.AppendLine("          <div class=\"header-block\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\"><span class=\"corner-icon\">📹</span><span class=\"index-val\" style=\"color:#0284c7; font-size:13px;\">↻ CW</span></div>");
            sb.AppendLine("                <span class=\"index-type\" style=\"color:#0369a1;\">Pass Left</span>");
            sb.AppendLine("              </div>");
            sb.AppendLine("              <div class=\"pill\" style=\"background:#ffffff; border-color:#0284c7; color:#0284c7;\">↻ CLOCKWISE</div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"sub-rule-bar\" style=\"border-color:#bae6fd; color:#0369a1;\">Double-Sided Marker (Front) • Default: Pass Left (Face Up!)</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div class=\"center-art\">");
            sb.AppendLine("            <div style=\"width:72px; height:72px; display:flex; align-items:center; justify-content:center; margin:0 auto;\">");
            sb.AppendLine("              <svg viewBox=\"0 0 100 100\" width=\"72\" height=\"72\">");
            sb.AppendLine("                <circle cx=\"50\" cy=\"50\" r=\"45\" fill=\"none\" stroke=\"#cbd5e1\" stroke-width=\"1\" stroke-dasharray=\"3,3\"/>");
            sb.AppendLine("                <path d=\"M 22 46 A 34 34 0 0 1 76 30\" fill=\"none\" stroke=\"#0284c7\" stroke-width=\"5\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <polygon points=\"70,18 88,30 72,40\" fill=\"#0284c7\"/>");
            sb.AppendLine("                <path d=\"M 78 54 A 34 34 0 0 1 24 70\" fill=\"none\" stroke=\"#0284c7\" stroke-width=\"5\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <polygon points=\"30,82 12,70 28,60\" fill=\"#0284c7\"/>");
            sb.AppendLine("                <path d=\"M 44 38 L 44 33 L 54 33\" fill=\"none\" stroke=\"#0284c7\" stroke-width=\"2\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <rect x=\"34\" y=\"38\" width=\"26\" height=\"18\" rx=\"3\" fill=\"#ffffff\" stroke=\"#0284c7\" stroke-width=\"2\"/>");
            sb.AppendLine("                <polygon points=\"60,43 71,37 71,51 60,45\" fill=\"#ffffff\" stroke=\"#0284c7\" stroke-width=\"1.5\"/>");
            sb.AppendLine("                <circle cx=\"41\" cy=\"47\" r=\"2.5\" fill=\"#ef4444\"/>");
            sb.AppendLine("                <circle cx=\"50\" cy=\"47\" r=\"3.5\" fill=\"none\" stroke=\"#0284c7\" stroke-width=\"1.5\"/>");
            sb.AppendLine("              </svg>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <span class=\"art-name\" style=\"color:#0c4a6e;\">SECURITY CAMERA</span>");
            sb.AppendLine("            <span class=\"art-tier\" style=\"color:#0284c7; font-weight:800;\">PASS CLOCKWISE ↻</span>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div class=\"header-block inverted\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\"><span class=\"corner-icon\">📹</span><span class=\"index-val\" style=\"color:#0284c7; font-size:13px;\">↻ CW</span></div>");
            sb.AppendLine("                <span class=\"index-type\" style=\"color:#0369a1;\">Pass Left</span>");
            sb.AppendLine("              </div>");
            sb.AppendLine("              <div class=\"pill\" style=\"background:#ffffff; border-color:#0284c7; color:#0284c7;\">↻ CLOCKWISE</div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"sub-rule-bar\" style=\"border-color:#bae6fd; color:#0369a1;\">Flip card over to Reverse (↺) when Green Wild is banked!</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");

            // Card 2 on Sheet 7: Counter-Clockwise Camera Marker (Back: Pass Right)
            sb.AppendLine("      <div class=\"card marker\">");
            sb.AppendLine("        <div class=\"card-inner\" style=\"border-color: #dc2626;\">");
            sb.AppendLine("          <div class=\"header-block\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\"><span class=\"corner-icon\">📹</span><span class=\"index-val\" style=\"color:#dc2626; font-size:13px;\">↺ CCW</span></div>");
            sb.AppendLine("                <span class=\"index-type\" style=\"color:#b91c1c;\">Pass Right</span>");
            sb.AppendLine("              </div>");
            sb.AppendLine("              <div class=\"pill\" style=\"background:#ffffff; border-color:#dc2626; color:#dc2626;\">↺ COUNTER-CW</div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"sub-rule-bar\" style=\"border-color:#fecaca; color:#b91c1c;\">Double-Sided Marker (Back) • Reversed: Pass Right (Face Up!)</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div class=\"center-art\">");
            sb.AppendLine("            <div style=\"width:72px; height:72px; display:flex; align-items:center; justify-content:center; margin:0 auto;\">");
            sb.AppendLine("              <svg viewBox=\"0 0 100 100\" width=\"72\" height=\"72\">");
            sb.AppendLine("                <circle cx=\"50\" cy=\"50\" r=\"45\" fill=\"none\" stroke=\"#cbd5e1\" stroke-width=\"1\" stroke-dasharray=\"3,3\"/>");
            sb.AppendLine("                <path d=\"M 78 46 A 34 34 0 0 0 24 30\" fill=\"none\" stroke=\"#dc2626\" stroke-width=\"5\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <polygon points=\"30,18 12,30 28,40\" fill=\"#dc2626\"/>");
            sb.AppendLine("                <path d=\"M 22 54 A 34 34 0 0 0 76 70\" fill=\"none\" stroke=\"#dc2626\" stroke-width=\"5\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <polygon points=\"70,82 88,70 72,60\" fill=\"#dc2626\"/>");
            sb.AppendLine("                <path d=\"M 56 38 L 56 33 L 46 33\" fill=\"none\" stroke=\"#dc2626\" stroke-width=\"2\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <rect x=\"40\" y=\"38\" width=\"26\" height=\"18\" rx=\"3\" fill=\"#ffffff\" stroke=\"#dc2626\" stroke-width=\"2\"/>");
            sb.AppendLine("                <polygon points=\"40,43 29,37 29,51 40,45\" fill=\"#ffffff\" stroke=\"#dc2626\" stroke-width=\"1.5\"/>");
            sb.AppendLine("                <circle cx=\"59\" cy=\"47\" r=\"2.5\" fill=\"#ef4444\"/>");
            sb.AppendLine("                <circle cx=\"50\" cy=\"47\" r=\"3.5\" fill=\"none\" stroke=\"#dc2626\" stroke-width=\"1.5\"/>");
            sb.AppendLine("              </svg>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <span class=\"art-name\" style=\"color:#881337;\">REVERSE CAMERA</span>");
            sb.AppendLine("            <span class=\"art-tier\" style=\"color:#dc2626; font-weight:800;\">COUNTER-CLOCKWISE ↺</span>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div class=\"header-block inverted\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\"><span class=\"corner-icon\">📹</span><span class=\"index-val\" style=\"color:#dc2626; font-size:13px;\">↺ CCW</span></div>");
            sb.AppendLine("                <span class=\"index-type\" style=\"color:#b91c1c;\">Pass Right</span>");
            sb.AppendLine("              </div>");
            sb.AppendLine("              <div class=\"pill\" style=\"background:#ffffff; border-color:#dc2626; color:#dc2626;\">↺ COUNTER-CW</div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"sub-rule-bar\" style=\"border-color:#fecaca; color:#b91c1c;\">Flip card back to Front (↻) when Green Wild is banked!</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");

            // Card 3: Turn Structure Reference (Face Up passing + Trios)
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-color:#475569;\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">📋</span><span class=\"index-val\" style=\"color:#0f172a;\">HEIST</span></div><span class=\"index-type\" style=\"color:#64748b;\">Turn Guide</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#d97706; color:#b45309;\">RULES</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9px; font-weight:900; color:#0f172a; text-transform:uppercase;\">1. DRAW • 2. TRIO • 3. PASS</span><div style=\"font-size:6.6px; text-align:left; margin-top:3px; line-height:1.32; color:#334155;\">• <b>1. Draw</b> 1 card from Locker.<br>• <b>2. Meld</b> at most 1 Trio (3 cards):<br>&nbsp;&nbsp;<b>GOLDEN LAW: All Trios need ≥1 Blue!</b><br>&nbsp;&nbsp;🔵🔵🔵 Clean Trio = 15 pts<br>&nbsp;&nbsp;🔵🔵🔴 Laundered = 20 / 25 pts<br>&nbsp;&nbsp;🔵🔴🔴 Smuggler's Run = 25 / 35 pts!<br>• <b>3. Pass</b> 1 card <b>FACE UP</b> to neighbor!<br><span style=\"color:#0284c7; font-weight:bold;\">⚡ Passing face-up triggers Intercept opportunity!</span></div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">📋</span><span class=\"index-val\" style=\"color:#0f172a;\">HEIST</span></div><span class=\"index-type\" style=\"color:#64748b;\">Turn Guide</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#d97706; color:#b45309;\">RULES</div></div></div>");
            sb.AppendLine("      </div></div>");

            // Card 4: The Snitch Reference ("Take-That Police Raid")
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-color:#d97706;\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🕵️</span><span class=\"index-val\" style=\"color:#d97706;\">SNITCH</span></div><span class=\"index-type\" style=\"color:#64748b;\">Take-That</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#dc2626; color:#dc2626;\">FED RAID</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9px; font-weight:900; color:#b45309; text-transform:uppercase;\">THE SNITCH (POLICE RAID)</span><div style=\"font-size:6.6px; text-align:left; margin-top:3px; line-height:1.32; color:#334155;\">• Play <b>ANYTIME</b> (even out-of-turn)!<br>• <b>All players reveal hands</b> immediately.<br>• Player(s) with <b>MOST Red cards</b> have their <b>entire banked Vault wiped to 0</b>!<br>• ⚠️ <b>INCLUDES YOU!</b> If you play it while holding the most Red cards, you wipe yourself!<br>• Discard Snitch after resolution.</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🕵️</span><span class=\"index-val\" style=\"color:#d97706;\">SNITCH</span></div><span class=\"index-type\" style=\"color:#64748b;\">Take-That</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#dc2626; color:#dc2626;\">FED RAID</div></div></div>");
            sb.AppendLine("      </div></div>");

            // Card 5: Trio Melds & Scoring Reference
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-color:#16a34a;\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">💎</span><span class=\"index-val\" style=\"color:#16a34a;\">TRIOS</span></div><span class=\"index-type\" style=\"color:#64748b;\">Scoring</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#16a34a; color:#16a34a;\">MELDS</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9px; font-weight:900; color:#15803d; text-transform:uppercase;\">TRIO SCORING MATRIX</span><div style=\"font-size:6.6px; text-align:left; margin-top:3px; line-height:1.32; color:#334155;\">• <b>Clean Syndicate (🔵+🔵+🔵)</b>: 15 pts<br>• <b>Laundered (🔵+🔵+🔴)</b>: Petty 20 / Grand 25<br>• <b>Smuggler's Run (🔵+🔴+🔴)</b>: Petty 25 / Grand 35!<br>• <b>Wild Trio (🎭+🔵+🔵 or 🎭+🔵+🔴)</b>: 10 / 15 / 20 pts & reverses camera direction!<br>• 🚫 Illegal: 🔴🔴🔴, 🎭🎭+ANY, 🎭🔴🔴.</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">💎</span><span class=\"index-val\" style=\"color:#16a34a;\">TRIOS</span></div><span class=\"index-type\" style=\"color:#64748b;\">Scoring</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#16a34a; color:#16a34a;\">MELDS</div></div></div>");
            sb.AppendLine("      </div></div>");

            // Card 6: Intercept & Fall Guy Reference
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-color:#0284c7;\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">⚡</span><span class=\"index-val\" style=\"color:#0284c7;\">SNAP!</span></div><span class=\"index-type\" style=\"color:#64748b;\">Advantage</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#0284c7; color:#0284c7;\">INTERCEPT</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9px; font-weight:900; color:#0284c7; text-transform:uppercase;\">INTERCEPT & FALL GUY</span><div style=\"font-size:6.6px; text-align:left; margin-top:3px; line-height:1.32; color:#334155;\">• <b>The Intercept</b>: If handed 🔴 Red face up & you hold 2 cards completing a legal Trio, shout INTERCEPT! Snap-meld before taking turn!<br>• <b>Fall Guy Bust</b>: At round end, player(s) holding the <b>MOST Red cards</b> bust = <b>0 pts</b>!<br>• Fewer Red than leader banks full vault.</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">⚡</span><span class=\"index-val\" style=\"color:#0284c7;\">SNAP!</span></div><span class=\"index-type\" style=\"color:#64748b;\">Advantage</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#0284c7; color:#0284c7;\">INTERCEPT</div></div></div>");
            sb.AppendLine("      </div></div>");

            // Card 7: Player Setups (2P Duel 28c, 3-4P 54c, 5-6P 108c)
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-color:#7c3aed;\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">👥</span><span class=\"index-val\" style=\"color:#7c3aed;\">SETUPS</span></div><span class=\"index-type\" style=\"color:#64748b;\">Scaling</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#7c3aed; color:#7c3aed;\">SCALING</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9px; font-weight:900; color:#7c3aed; text-transform:uppercase;\">DECK SCALING</span><div style=\"font-size:6.6px; text-align:left; margin-top:3px; line-height:1.32; color:#334155;\">• <b>2P Quickdraw Duel (28c)</b>: 4 Petty Items (16 Blue, 8 Red) + 2 Wilds + 2 Snitches. Target: 200 pts.<br>• <b>3-4P Standard Heist (54c)</b>: Full deck. 30 Blue, 16 Red, 6 Wild, 2 Snitch. Target: 300 pts.<br>• <b>5-6P Grand Syndicate (108c)</b>: Double deck (4 Snitches). Target: 300 pts.</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">👥</span><span class=\"index-val\" style=\"color:#7c3aed;\">SETUPS</span></div><span class=\"index-type\" style=\"color:#64748b;\">Scaling</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#7c3aed; color:#7c3aed;\">SCALING</div></div></div>");
            sb.AppendLine("      </div></div>");

            // 2 Blank Spare Cards (completes 9-card Sheet 7)
            for (int k = 0; k < 2; k++)
            {
                sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-style:dashed; border-color:#94a3b8;\">");
                sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🃏</span><span class=\"index-val\" style=\"color:#64748b;\">SPARE</span></div><span class=\"index-type\" style=\"color:#94a3b8;\">Blank</span></div></div></div>");
                sb.AppendLine("        <div class=\"center-art\"><span class=\"art-icon\" style=\"opacity:0.25;\">🃏</span><span class=\"art-name\" style=\"color:#64748b;\">REPLACEMENT</span><span class=\"art-tier\" style=\"color:#94a3b8;\">Create custom card</span></div>");
                sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🃏</span><span class=\"index-val\" style=\"color:#64748b;\">SPARE</span></div><span class=\"index-type\" style=\"color:#94a3b8;\">Blank</span></div></div></div>");
                sb.AppendLine("      </div></div>");
            }

            sb.AppendLine("    </div>");

            // Sheet 8: 9 Printer-Friendly Eco Card Backs (Clean white, thin line-art, zero black fills)
            sb.AppendLine("    <div class=\"sheet-header-label\">Sheet 8 of 8: Card Backs (Printer-Friendly Minimal Ink • Print on reverse of Sheets 1–7)</div>");
            sb.AppendLine("    <div class=\"sheet\">");
            for (int b = 0; b < 9; b++)
            {
                sb.AppendLine("      <div class=\"card card-back\">");
                sb.AppendLine("        <div class=\"card-inner\">");
                sb.AppendLine("          <div class=\"card-back-pattern\"></div>");
                sb.AppendLine("          <svg style=\"position:absolute; inset:8px; width:calc(100% - 16px); height:calc(100% - 16px); opacity:0.18; pointer-events:none;\" viewBox=\"0 0 100 140\">");
                sb.AppendLine("            <line x1=\"0\" y1=\"0\" x2=\"100\" y2=\"140\" stroke=\"#64748b\" stroke-width=\"0.5\" stroke-dasharray=\"2,2\" />");
                sb.AppendLine("            <line x1=\"100\" y1=\"0\" x2=\"0\" y2=\"140\" stroke=\"#64748b\" stroke-width=\"0.5\" stroke-dasharray=\"2,2\" />");
                sb.AppendLine("            <rect x=\"15\" y=\"20\" width=\"70\" height=\"100\" fill=\"none\" stroke=\"#64748b\" stroke-width=\"0.6\" rx=\"4\" />");
                sb.AppendLine("            <polygon points=\"50,25 80,70 50,115 20,70\" fill=\"none\" stroke=\"#64748b\" stroke-width=\"0.6\" />");
                sb.AppendLine("          </svg>");
                sb.AppendLine("          <span style=\"font-size:30px; z-index:2;\">🔒🚨</span>");
                sb.AppendLine("          <div style=\"font-size:13px; font-weight:900; letter-spacing:1.5px; margin-top:8px; text-transform:uppercase; color:#0f172a; z-index:2;\">CAUGHT</div>");
                sb.AppendLine("          <div style=\"font-size:10px; font-weight:800; color:#dc2626; letter-spacing:2px; text-transform:uppercase; z-index:2;\">RED-HANDED</div>");
                sb.AppendLine("        </div>");
                sb.AppendLine("      </div>");
            }
            sb.AppendLine("    </div>");

            sb.AppendLine("  </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(outHtmlPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine("SUCCESS: Generated " + outHtmlPath);
        }

        public static string RenderCardHtml(CardDef card)
        {
            var sb = new StringBuilder();
            string colorClass = card.Color.ToLower();
            string tierClass = card.IsGrand ? "grand" : "petty";

            string valDisplay = (card.Color == "SPECIAL") ? "RAID" : ((card.Color == "GREEN") ? "WILD" : ("+" + card.Val));
            string typeDisplay = (card.Color == "BLUE") ? "Clean" : ((card.Color == "RED") ? "Contraband" : ((card.Color == "GREEN") ? "Counterfeit" : "The Snitch"));

            string pillHtml = "";
            if (card.Color == "BLUE")
            {
                int targetVal = card.IsGrand ? 15 : 10;
                pillHtml = string.Format("<div class=\"pill\">🔴 +{0} Target</div>", targetVal);
            }
            else if (card.Color == "RED")
            {
                string tag = card.IsGrand ? "🚨 GRAND" : "🚨 HOT";
                pillHtml = string.Format("<div class=\"pill\">{0}</div>", tag);
            }
            else if (card.Color == "GREEN")
            {
                pillHtml = "<div class=\"pill\">🔵 CONNECTOR</div>";
            }
            else
            {
                pillHtml = "<div class=\"pill\">🚨 TAKE THAT</div>";
            }

            string ruleText = "";
            if (card.Color == "BLUE")
            {
                int redMeld = card.IsGrand ? 25 : 20;
                ruleText = string.Format("Trio: 🔵🔵🔵 (15pts), 🔵🔵🔴 ({0}pts), 🔵🔴🔴 ({1}pts)", redMeld, card.IsGrand ? 35 : 25);
            }
            else if (card.Color == "RED")
            {
                int redMeld = card.IsGrand ? 25 : 20;
                ruleText = string.Format("Trio: Needs 🔵 {0} ({1}pts) • Intercept if holding 2 matching!", card.Item, redMeld);
            }
            else if (card.Color == "GREEN")
            {
                ruleText = "Wild Trio: Connects matching Blue+Blue or Blue+Red & flips Camera";
            }
            else
            {
                ruleText = "Play anytime: reveals all hands. Most Red loses entire banked Vault!";
            }

            string artTier = (card.Color == "BLUE") ? (card.IsGrand ? "👑 Grand Clean" : "⭐ Petty Clean")
                           : (card.Color == "RED") ? (card.IsGrand ? "👑 Grand Hot" : "⭐ Petty Hot")
                           : (card.Color == "GREEN") ? "Pairs w/ Matching Blue"
                           : "⚡ Fed Police Raid";

            string artIcon = (card.Color == "SPECIAL") ? "🕵️🚨" : ((card.Color == "GREEN") ? "🎭🔄" : card.Icon);

            sb.AppendLine(string.Format("      <div class=\"card {0} {1}\">", colorClass, tierClass));
            sb.AppendLine("        <div class=\"card-inner\">");

            // Top Header & Rule
            sb.AppendLine("          <div class=\"header-block\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\">");
            sb.AppendLine(string.Format("                  <span class=\"corner-icon\">{0}</span>", card.Icon));
            sb.AppendLine(string.Format("                  <span class=\"index-val\">{0}</span>", valDisplay));
            sb.AppendLine("                </div>");
            sb.AppendLine(string.Format("                <span class=\"index-type\">{0}</span>", typeDisplay));
            sb.AppendLine("              </div>");
            sb.AppendLine("              " + pillHtml);
            sb.AppendLine("            </div>");
            sb.AppendLine(string.Format("            <div class=\"sub-rule-bar\">{0}</div>", ruleText));
            sb.AppendLine("          </div>");

            // Center Art
            sb.AppendLine("          <div class=\"center-art\">");
            sb.AppendLine(string.Format("            <span class=\"art-icon\">{0}</span>", artIcon));
            sb.AppendLine(string.Format("            <span class=\"art-name\">{0}</span>", card.Name));
            sb.AppendLine(string.Format("            <span class=\"art-tier\">{0}</span>", artTier));
            sb.AppendLine("          </div>");

            // Bottom Inverted Header & Rule (Rotated 180deg)
            sb.AppendLine("          <div class=\"header-block inverted\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\">");
            sb.AppendLine(string.Format("                  <span class=\"corner-icon\">{0}</span>", card.Icon));
            sb.AppendLine(string.Format("                  <span class=\"index-val\">{0}</span>", valDisplay));
            sb.AppendLine("                </div>");
            sb.AppendLine(string.Format("                <span class=\"index-type\">{0}</span>", typeDisplay));
            sb.AppendLine("              </div>");
            sb.AppendLine("              " + pillHtml);
            sb.AppendLine("            </div>");
            sb.AppendLine(string.Format("            <div class=\"sub-rule-bar\">{0}</div>", ruleText));
            sb.AppendLine("          </div>");

            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");
            return sb.ToString();
        }
    }
}
