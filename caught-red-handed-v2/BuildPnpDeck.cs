using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace PnpDeckGeneratorV2
{
    public class CardDef
    {
        public int Val; // -1 for Hot Evidence, 0 for Reset, 1..10 for numbers
        public string Title;
        public string Color; // RED, BLUE, SPECIAL
        public int Copies;
        public string Icon;
        public string Sub;
        public string DisplayVal;
    }

    public class Program
    {
        public static void Main()
        {
            string outHtmlPath = @"c:\Users\bazar\Documents\antigravity\lively-rutherford\caught-red-handed-v2\print_deck.html";

            var cardDefs = new List<CardDef>();

            // 1. Blue Clean Loot (36 Cards)
            // Values 1..10
            cardDefs.Add(new CardDef { Val = 1, Title = "Marked Cash", Color = "BLUE", Copies = 1, Icon = "💵", DisplayVal = "1", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 1, Title = "Safe Gems", Color = "BLUE", Copies = 1, Icon = "💎", DisplayVal = "1", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 1, Title = "Rolex Watch", Color = "BLUE", Copies = 1, Icon = "⌚", DisplayVal = "1", Sub = "Clean Loot • Zero Contraband" });

            cardDefs.Add(new CardDef { Val = 2, Title = "Vault Key", Color = "BLUE", Copies = 1, Icon = "🗝️", DisplayVal = "2", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 2, Title = "Gallery Art", Color = "BLUE", Copies = 1, Icon = "🖼️", DisplayVal = "2", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 2, Title = "Gold Bullion", Color = "BLUE", Copies = 1, Icon = "🧈", DisplayVal = "2", Sub = "Clean Loot • Zero Contraband" });

            cardDefs.Add(new CardDef { Val = 3, Title = "Marked Cash", Color = "BLUE", Copies = 1, Icon = "💵", DisplayVal = "3", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 3, Title = "Safe Gems", Color = "BLUE", Copies = 1, Icon = "💎", DisplayVal = "3", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 3, Title = "Rolex Watch", Color = "BLUE", Copies = 1, Icon = "⌚", DisplayVal = "3", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 3, Title = "Vault Key", Color = "BLUE", Copies = 1, Icon = "🗝️", DisplayVal = "3", Sub = "Clean Loot • Zero Contraband" });

            cardDefs.Add(new CardDef { Val = 4, Title = "Gallery Art", Color = "BLUE", Copies = 1, Icon = "🖼️", DisplayVal = "4", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 4, Title = "Gold Bullion", Color = "BLUE", Copies = 1, Icon = "🧈", DisplayVal = "4", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 4, Title = "Marked Cash", Color = "BLUE", Copies = 1, Icon = "💵", DisplayVal = "4", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 4, Title = "Safe Gems", Color = "BLUE", Copies = 1, Icon = "💎", DisplayVal = "4", Sub = "Clean Loot • Zero Contraband" });

            cardDefs.Add(new CardDef { Val = 5, Title = "Rolex Watch", Color = "BLUE", Copies = 1, Icon = "⌚", DisplayVal = "5", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 5, Title = "Vault Key", Color = "BLUE", Copies = 1, Icon = "🗝️", DisplayVal = "5", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 5, Title = "Gallery Art", Color = "BLUE", Copies = 1, Icon = "🖼️", DisplayVal = "5", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 5, Title = "Gold Bullion", Color = "BLUE", Copies = 1, Icon = "🧈", DisplayVal = "5", Sub = "Clean Loot • Zero Contraband" });

            cardDefs.Add(new CardDef { Val = 6, Title = "Marked Cash", Color = "BLUE", Copies = 1, Icon = "💵", DisplayVal = "6", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 6, Title = "Safe Gems", Color = "BLUE", Copies = 1, Icon = "💎", DisplayVal = "6", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 6, Title = "Rolex Watch", Color = "BLUE", Copies = 1, Icon = "⌚", DisplayVal = "6", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 6, Title = "Vault Key", Color = "BLUE", Copies = 1, Icon = "🗝️", DisplayVal = "6", Sub = "Clean Loot • Zero Contraband" });

            cardDefs.Add(new CardDef { Val = 7, Title = "Gallery Art", Color = "BLUE", Copies = 1, Icon = "🖼️", DisplayVal = "7", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 7, Title = "Gold Bullion", Color = "BLUE", Copies = 1, Icon = "🧈", DisplayVal = "7", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 7, Title = "Marked Cash", Color = "BLUE", Copies = 1, Icon = "💵", DisplayVal = "7", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 7, Title = "Safe Gems", Color = "BLUE", Copies = 1, Icon = "💎", DisplayVal = "7", Sub = "Clean Loot • Zero Contraband" });

            cardDefs.Add(new CardDef { Val = 8, Title = "Rolex Watch", Color = "BLUE", Copies = 1, Icon = "⌚", DisplayVal = "8", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 8, Title = "Vault Key", Color = "BLUE", Copies = 1, Icon = "🗝️", DisplayVal = "8", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 8, Title = "Gallery Art", Color = "BLUE", Copies = 1, Icon = "🖼️", DisplayVal = "8", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 8, Title = "Gold Bullion", Color = "BLUE", Copies = 1, Icon = "🧈", DisplayVal = "8", Sub = "Clean Loot • Zero Contraband" });

            cardDefs.Add(new CardDef { Val = 9, Title = "Marked Cash", Color = "BLUE", Copies = 1, Icon = "💵", DisplayVal = "9", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 9, Title = "Safe Gems", Color = "BLUE", Copies = 1, Icon = "💎", DisplayVal = "9", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 9, Title = "Gold Bullion", Color = "BLUE", Copies = 1, Icon = "🧈", DisplayVal = "9", Sub = "Clean Loot • Zero Contraband" });

            cardDefs.Add(new CardDef { Val = 10, Title = "Vault Key", Color = "BLUE", Copies = 1, Icon = "🗝️", DisplayVal = "10", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 10, Title = "Gallery Art", Color = "BLUE", Copies = 1, Icon = "🖼️", DisplayVal = "10", Sub = "Clean Loot • Zero Contraband" });
            cardDefs.Add(new CardDef { Val = 10, Title = "Rolex Watch", Color = "BLUE", Copies = 1, Icon = "⌚", DisplayVal = "10", Sub = "Clean Loot • Zero Contraband" });

            // 2. Numbered Red Contraband (8 Cards)
            cardDefs.Add(new CardDef { Val = 1, Title = "Hot Cash", Color = "RED", Copies = 2, Icon = "💵🚨", DisplayVal = "1", Sub = "Petty Contraband • Legal if > previous!" });
            cardDefs.Add(new CardDef { Val = 2, Title = "Blood Diamond", Color = "RED", Copies = 2, Icon = "💎🚨", DisplayVal = "2", Sub = "Petty Contraband • Legal if > previous!" });
            cardDefs.Add(new CardDef { Val = 3, Title = "Hot Rolex", Color = "RED", Copies = 1, Icon = "⌚🚨", DisplayVal = "3", Sub = "Larceny Contraband • Legal if > previous!" });
            cardDefs.Add(new CardDef { Val = 4, Title = "Master Key", Color = "RED", Copies = 1, Icon = "🗝️🚨", DisplayVal = "4", Sub = "Grand Larceny • Legal if > previous!" });
            cardDefs.Add(new CardDef { Val = 5, Title = "Stolen Canvas", Color = "RED", Copies = 1, Icon = "🖼️🚨", DisplayVal = "5", Sub = "Syndicate Felony • Legal if > previous!" });
            cardDefs.Add(new CardDef { Val = 6, Title = "Smuggled Gold", Color = "RED", Copies = 1, Icon = "🧈🚨", DisplayVal = "6", Sub = "Federal Evidence • Legal if > previous!" });

            // 3. Unnumbered Hot Evidence (6 Cards)
            cardDefs.Add(new CardDef { Val = -1, Title = "Crime Dossier", Color = "RED", Copies = 2, Icon = "📁🚨", DisplayVal = "🚨", Sub = "HOT EVIDENCE • NO NUMBER (MUST BLUFF!)" });
            cardDefs.Add(new CardDef { Val = -1, Title = "Federal Bug", Color = "RED", Copies = 2, Icon = "🎙️🚨", DisplayVal = "🚨", Sub = "HOT EVIDENCE • NO NUMBER (MUST BLUFF!)" });
            cardDefs.Add(new CardDef { Val = -1, Title = "Syndicate Seal", Color = "RED", Copies = 2, Icon = "💼🚨", DisplayVal = "🚨", Sub = "HOT EVIDENCE • NO NUMBER (MUST BLUFF!)" });

            // 4. Tactical Resets (4 Cards)
            cardDefs.Add(new CardDef { Val = 0, Title = "The Fence", Color = "SPECIAL", Copies = 2, Icon = "🤝0", DisplayVal = "0", Sub = "Resets Count to 0 • Next declares 1-10" });
            cardDefs.Add(new CardDef { Val = 0, Title = "Smoke Bomb", Color = "SPECIAL", Copies = 2, Icon = "💨🔄", DisplayVal = "0", Sub = "Resets Count to 0 & REVERSES DIRECTION" });

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
            sb.AppendLine("  <title>Caught Red-Handed V2: The Interrogation - Low-Ink PnP Deck (8.5x11)</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@500;600;700;800;900&display=swap');");
            sb.AppendLine("    @page { size: letter portrait; margin: 0.25in 0.5in 0.25in 0.5in; }");
            sb.AppendLine("    * { box-sizing: border-box; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }");
            sb.AppendLine("    body { margin: 0; padding: 0; font-family: 'Montserrat', system-ui, sans-serif; background: #f8fafc; color: #0f172a; }");
            sb.AppendLine("    .no-print-bar { background: #0f172a; color: #f8fafc; padding: 12px 24px; display: flex; justify-content: space-between; align-items: center; position: sticky; top: 0; z-index: 1000; box-shadow: 0 4px 12px rgba(0,0,0,0.15); }");
            sb.AppendLine("    .no-print-bar button { background: #dc2626; color: white; border: none; padding: 8px 20px; border-radius: 8px; font-weight: 800; font-size: 14px; cursor: pointer; display: flex; align-items: center; gap: 8px; transition: background 0.2s; }");
            sb.AppendLine("    .no-print-bar button:hover { background: #b91c1c; }");
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
            sb.AppendLine("    .index-val { font-size: 18px; font-weight: 900; letter-spacing: -0.5px; }");
            sb.AppendLine("    .index-type { font-size: 7px; font-weight: 800; text-transform: uppercase; letter-spacing: 0.4px; margin-top: 1px; }");
            sb.AppendLine("    .pill { font-size: 7.5px; font-weight: 800; padding: 2px 5px; border-radius: 4px; border: 1.2px solid; text-transform: uppercase; letter-spacing: 0.2px; display: inline-flex; align-items: center; gap: 2px; background: #ffffff; }");
            sb.AppendLine("    .sub-rule-bar { font-size: 6.8px; font-weight: 700; text-align: center; border-radius: 2px; padding: 1.5px 3px; margin-top: 2px; background: #ffffff; border-top: 1px solid #e2e8f0; border-bottom: 1px solid #e2e8f0; line-height: 1.15; color: #334155; }");
            sb.AppendLine("    .center-art { display: flex; flex-direction: column; align-items: center; justify-content: center; margin: auto 0; text-align: center; z-index: 2; }");
            sb.AppendLine("    .art-icon { font-size: 36px; line-height: 1; }");
            sb.AppendLine("    .art-name { font-size: 12px; font-weight: 900; text-transform: uppercase; letter-spacing: 0.4px; margin-top: 2px; line-height: 1.1; color: #0f172a; }");
            sb.AppendLine("    .art-tier { font-size: 7.5px; font-weight: 800; text-transform: uppercase; letter-spacing: 0.6px; margin-top: 1px; }");
            sb.AppendLine("    .inverted { transform: rotate(180deg); }");

            // Card Themes
            sb.AppendLine("    .card.blue .card-inner { border-color: #0284c7; background: #ffffff; color: #0369a1; }");
            sb.AppendLine("    .card.blue .index-val { color: #0284c7; }");
            sb.AppendLine("    .card.blue .index-type { color: #0369a1; }");
            sb.AppendLine("    .card.blue .art-name { color: #0c4a6e; }");
            sb.AppendLine("    .card.blue .art-tier { color: #0284c7; }");
            sb.AppendLine("    .card.blue .pill { background: #ffffff; border-color: #0284c7; color: #0284c7; }");

            sb.AppendLine("    .card.red .card-inner { border-color: #dc2626; background: #ffffff; color: #b91c1c; }");
            sb.AppendLine("    .card.red .index-val { color: #dc2626; }");
            sb.AppendLine("    .card.red .index-type { color: #b91c1c; }");
            sb.AppendLine("    .card.red .art-name { color: #881337; }");
            sb.AppendLine("    .card.red .art-tier { color: #dc2626; }");
            sb.AppendLine("    .card.red .pill { background: #ffffff; border-color: #dc2626; color: #dc2626; font-weight: 900; }");

            sb.AppendLine("    .card.special .card-inner { border-color: #d97706; background: #ffffff; color: #b45309; }");
            sb.AppendLine("    .card.special .index-val { color: #d97706; }");
            sb.AppendLine("    .card.special .index-type { color: #b45309; }");
            sb.AppendLine("    .card.special .art-name { color: #78350f; }");
            sb.AppendLine("    .card.special .art-tier { color: #dc2626; font-weight: 900; }");
            sb.AppendLine("    .card.special .pill { background: #ffffff; border-color: #d97706; color: #d97706; font-weight: 900; }");

            // Marker & Card Back Themes
            sb.AppendLine("    .card.marker .card-inner { background: #ffffff; color: #0f172a; border: 2px solid #0f172a; }");
            sb.AppendLine("    .card.card-back .card-inner { background: #ffffff; border: 2px solid #0f172a; color: #0f172a; display: flex; flex-direction: column; align-items: center; justify-content: center; text-align: center; padding: 0.15in; position: relative; }");
            sb.AppendLine("    .card-back-pattern { position: absolute; inset: 4px; border: 1px dashed #cbd5e1; border-radius: 5px; pointer-events: none; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <div class=\"no-print-bar\">");
            sb.AppendLine("    <div><span style=\"font-weight: 900; font-size: 16px; color: #f87171;\">CAUGHT RED-HANDED V2</span><span style=\"color: #94a3b8; font-size: 13px; margin-left: 8px;\">• The Interrogation Deck (8.5\" x 11\")</span><span style=\"background:#059669; color:#fff; font-size:10px; font-weight:800; padding:2px 8px; border-radius:12px; margin-left:12px;\">🌱 ECO INK-SAVER</span></div>");
            sb.AppendLine("    <button onclick=\"window.print()\">🖨️ PRINT DECK TO PDF / PRINTER</button>");
            sb.AppendLine("  </div>");
            sb.AppendLine("  <div class=\"sheet-wrapper\">");

            // 6 sheets of 9 cards = Exactly 54 play cards
            string[] sheetTitles = new string[] {
                "Sheet 1 of 8: Clean Loot (Blues 1..3)",
                "Sheet 2 of 8: Clean Loot (Blues 3..5)",
                "Sheet 3 of 8: Clean Loot (Blues 6..8)",
                "Sheet 4 of 8: Clean Loot (Blues 8..10)",
                "Sheet 5 of 8: Numbered Red Contraband (1..6) & Hot Evidence",
                "Sheet 6 of 8: Hot Evidence (🚨) & Tactical Resets (0)"
            };

            for (int s = 0; s < 6; s++)
            {
                sb.AppendLine(string.Format("    <div class=\"sheet-header-label\">{0}</div>", sheetTitles[s]));
                sb.AppendLine("    <div class=\"sheet\">");

                for (int c = 0; c < 9; c++)
                {
                    int idx = s * 9 + c;
                    sb.Append(RenderCard(deck[idx]));
                }

                sb.AppendLine("    </div>");
            }

            // Sheet 7: Reference / Aid Cards & Strike Trackers + Card Backs
            sb.AppendLine("    <div class=\"sheet-header-label\">Sheet 7 of 8: Reference Aids & Strike Trackers</div>");
            sb.AppendLine("    <div class=\"sheet\">");
            
            // Reference Card 1
            sb.AppendLine("      <div class=\"card marker\">");
            sb.AppendLine("        <div class=\"card-inner\" style=\"padding: 8px; font-size: 7.5px; line-height: 1.25; display: flex; flex-direction: column; justify-content: space-between;\">");
            sb.AppendLine("          <div style=\"border-bottom: 2px solid #0f172a; padding-bottom: 4px; text-align: center;\">");
            sb.AppendLine("            <div style=\"font-size: 11px; font-weight: 900; color: #dc2626;\">🚨 TURN REFERENCE</div>");
            sb.AppendLine("            <div style=\"font-size: 6.5px; font-weight: 700; color: #64748b;\">CAUGHT RED-HANDED V2</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div style=\"margin-top: 4px;\">");
            sb.AppendLine("            <b>ON YOUR TURN, CHOOSE 1:</b><br>");
            sb.AppendLine("            <b>1. PLAY & DECLARE</b>: Play face-down & declare number &gt; previous (or declare Reset).<br>");
            sb.AppendLine("            <b>2. INVESTIGATE!</b>: Challenge the previous player's card.<br>");
            sb.AppendLine("            <b>3. PASS & DRAW</b>: Draw 1 card from locker. Turn ends.");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div style=\"background: #fef2f2; border: 1px solid #f87171; border-radius: 4px; padding: 4px; margin-top: 4px;\">");
            sb.AppendLine("            <b style=\"color:#b91c1c;\">🔍 INVESTIGATION VERDICT:</b><br>");
            sb.AppendLine("            • <b>ILLEGAL / LIE</b>: Bluffer takes ALL Red cards from evidence pile into Penalty Pile!<br>");
            sb.AppendLine("            • <b>LEGAL / TRUTH</b>: Investigator takes ALL Red cards into Penalty Pile!<br>");
            sb.AppendLine("            <i>(Blue & Reset cards are secured to Vault).</i>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div style=\"background: #f0fdf4; border: 1px solid #86efac; border-radius: 4px; padding: 4px; margin-top: 4px;\">");
            sb.AppendLine("            <b style=\"color:#15803d;\">🚗 GETAWAY CAR (ROUND END):</b><br>");
            sb.AppendLine("            First to empty hand escapes (0 Strikes)! Remaining players count:<br>");
            sb.AppendLine("            <code>(Red in Hand) + (Red in Penalty Pile)</code>.<br>");
            sb.AppendLine("            <b>Most Red cards = 1 Strike! 3 Strikes = PRISON!</b>");
            sb.AppendLine("          </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");

            // Reference Card 2
            sb.AppendLine("      <div class=\"card marker\">");
            sb.AppendLine("        <div class=\"card-inner\" style=\"padding: 8px; font-size: 7.5px; line-height: 1.25; display: flex; flex-direction: column; justify-content: space-between;\">");
            sb.AppendLine("          <div style=\"border-bottom: 2px solid #0f172a; padding-bottom: 4px; text-align: center;\">");
            sb.AppendLine("            <div style=\"font-size: 11px; font-weight: 900; color: #0284c7;\">📋 DECK ARCHITECTURE</div>");
            sb.AppendLine("            <div style=\"font-size: 6.5px; font-weight: 700; color: #64748b;\">54 CARDS TOTAL</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div style=\"margin-top: 4px;\">");
            sb.AppendLine("            <b>• 36 CLEAN LOOT (BLUE 1-10)</b>: Safe goods. Zero penalty points.<br><br>");
            sb.AppendLine("            <b>• 8 NUMBERED RED (1-6)</b>: Low contraband. Can be played legally if &gt; previous number!<br><br>");
            sb.AppendLine("            <b>• 6 HOT EVIDENCE (🚨 RED)</b>: Unnumbered toxic contraband. CANNOT match any number! Must bluff to shed!<br><br>");
            sb.AppendLine("            <b>• 4 RESETS (0 SPECIAL)</b>: Resets count to 0. The Fence (0 🤝) & Smoke Bomb (0 💨🔄 reverses play).");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div style=\"background: #fffbeb; border: 1px solid #fcd34d; border-radius: 4px; padding: 4px; text-align:center; font-weight:800; color:#b45309;\">");
            sb.AppendLine("            💡 TIP: Call bluff when the pile has few red cards; avoid false accusations when the pile is loaded!");
            sb.AppendLine("          </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");

            // Strike Trackers (2 cards)
            for (int t = 1; t <= 2; t++)
            {
                sb.AppendLine("      <div class=\"card marker\">");
                sb.AppendLine("        <div class=\"card-inner\" style=\"padding: 8px; display: flex; flex-direction: column; justify-content: space-between; text-align: center;\">");
                sb.AppendLine("          <div style=\"border-bottom: 2px solid #0f172a; padding-bottom: 4px;\">");
                sb.AppendLine("            <div style=\"font-size: 12px; font-weight: 900; color: #dc2626;\">🚨 MUGSHOT TRACKER</div>");
                sb.AppendLine(string.Format("            <div style=\"font-size: 7px; font-weight: 700; color: #64748b;\">PLAYER {0} DOSSIER</div>", t));
                sb.AppendLine("          </div>");
                sb.AppendLine("          <div style=\"display:flex; flex-direction:column; gap:6px; margin: auto 0;\">");
                sb.AppendLine("            <div style=\"border:1.5px solid #cbd5e1; border-radius:6px; padding:4px; font-weight:800; font-size:8px;\">STRIKE 1: ⚠️ SUSPECT RECORD</div>");
                sb.AppendLine("            <div style=\"border:1.5px solid #cbd5e1; border-radius:6px; padding:4px; font-weight:800; font-size:8px;\">STRIKE 2: 🚨 WANTED FUGITIVE</div>");
                sb.AppendLine("            <div style=\"border:2px solid #dc2626; background:#fef2f2; border-radius:6px; padding:4px; font-weight:900; font-size:8.5px; color:#b91c1c;\">STRIKE 3: 🔒 FEDERAL PRISON!</div>");
                sb.AppendLine("          </div>");
                sb.AppendLine("          <div style=\"font-size:6.5px; font-weight:700; color:#64748b;\">Place token or paperclip on active strike level</div>");
                sb.AppendLine("        </div>");
                sb.AppendLine("      </div>");
            }

            // 5 Card Backs to fill Sheet 7
            for (int b = 0; b < 5; b++)
            {
                sb.AppendLine(RenderCardBack());
            }

            sb.AppendLine("    </div>");

            // Sheet 8: 9 Card Backs for double-sided printing
            sb.AppendLine("    <div class=\"sheet-header-label\">Sheet 8 of 8: Card Backs (For Duplex Printing)</div>");
            sb.AppendLine("    <div class=\"sheet\">");
            for (int b = 0; b < 9; b++)
            {
                sb.AppendLine(RenderCardBack());
            }
            sb.AppendLine("    </div>");

            sb.AppendLine("  </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(outHtmlPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine("PnP Deck HTML successfully written to: " + outHtmlPath);
        }

        static string RenderCardBack()
        {
            var sb = new StringBuilder();
            sb.AppendLine("      <div class=\"card card-back\">");
            sb.AppendLine("        <div class=\"card-inner\">");
            sb.AppendLine("          <div class=\"card-back-pattern\"></div>");
            sb.AppendLine("          <div style=\"font-size: 28px; line-height: 1;\">🚨</div>");
            sb.AppendLine("          <div style=\"font-size: 13px; font-weight: 900; letter-spacing: 1px; margin-top: 4px;\">CAUGHT</div>");
            sb.AppendLine("          <div style=\"font-size: 10px; font-weight: 800; color: #dc2626; letter-spacing: 0.5px;\">RED-HANDED</div>");
            sb.AppendLine("          <div style=\"font-size: 7px; font-weight: 800; color: #64748b; margin-top: 2px;\">V2: THE INTERROGATION</div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");
            return sb.ToString();
        }

        static string RenderCard(CardDef card)
        {
            var sb = new StringBuilder();
            string colorClass = card.Color.ToLower();
            string typeDisplay = (card.Color == "RED") ? "CONTRABAND"
                               : (card.Color == "SPECIAL") ? "TACTICAL"
                               : "CLEAN LOOT";

            string pillHtml = string.Format("<span class=\"pill\">{0}</span>", card.Color == "RED" ? "🚨 HOT" : (card.Color == "SPECIAL" ? "⚡ RESET" : "🛡️ SAFE"));

            string ruleText = card.Sub;

            string artTier = (card.Color == "RED") ? (card.Val == -1 ? "🚨 Pure Toxic Evidence" : "🚨 Numbered Contraband")
                           : (card.Color == "SPECIAL") ? "⚡ Tactical Reset"
                           : "Safe Clean Loot";

            sb.AppendLine(string.Format("      <div class=\"card {0}\">", colorClass));
            sb.AppendLine("        <div class=\"card-inner\">");

            // Top Header
            sb.AppendLine("          <div class=\"header-block\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\">");
            sb.AppendLine(string.Format("                  <span class=\"corner-icon\">{0}</span>", card.Icon));
            sb.AppendLine(string.Format("                  <span class=\"index-val\">{0}</span>", card.DisplayVal));
            sb.AppendLine("                </div>");
            sb.AppendLine(string.Format("                <span class=\"index-type\">{0}</span>", typeDisplay));
            sb.AppendLine("              </div>");
            sb.AppendLine("              " + pillHtml);
            sb.AppendLine("            </div>");
            sb.AppendLine(string.Format("            <div class=\"sub-rule-bar\">{0}</div>", ruleText));
            sb.AppendLine("          </div>");

            // Center Art
            sb.AppendLine("          <div class=\"center-art\">");
            sb.AppendLine(string.Format("            <span class=\"art-icon\">{0}</span>", card.Icon));
            sb.AppendLine(string.Format("            <span class=\"art-name\">{0} {1}</span>", card.Title, (card.Val > 0 ? card.Val.ToString() : "")));
            sb.AppendLine(string.Format("            <span class=\"art-tier\">{0}</span>", artTier));
            sb.AppendLine("          </div>");

            // Bottom Inverted Header (Rotated 180deg)
            sb.AppendLine("          <div class=\"header-block inverted\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\">");
            sb.AppendLine(string.Format("                  <span class=\"corner-icon\">{0}</span>", card.Icon));
            sb.AppendLine(string.Format("                  <span class=\"index-val\">{0}</span>", card.DisplayVal));
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
