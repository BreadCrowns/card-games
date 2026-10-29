using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace PnpDeckGeneratorV2
{
    public class CardDef
    {
        public int Val;
        public string Item;
        public string Color; // RED, BLUE, SPECIAL
        public int Copies;
        public string Icon;
        public string Sub;
    }

    public class Program
    {
        public static void Main()
        {
            string outHtmlPath = @"c:\Users\bazar\Documents\antigravity\lively-rutherford\caught-red-handed-v2\print_deck.html";

            var cardDefs = new List<CardDef>
            {
                // Red Contraband: 12 cards total (1, 2, 3)
                new CardDef { Val = 1, Item = "Pickpocket", Color = "RED", Copies = 4, Icon = "👛", Sub = "Toxic Contraband" },
                new CardDef { Val = 2, Item = "Hot Jewelry", Color = "RED", Copies = 4, Icon = "⌚", Sub = "Toxic Contraband" },
                new CardDef { Val = 3, Item = "Blood Diamond", Color = "RED", Copies = 4, Icon = "💎", Sub = "Toxic Contraband" },

                // Clean Loot (Blue): 34 cards total (4 - 10)
                new CardDef { Val = 4, Item = "Cash Stack", Color = "BLUE", Copies = 5, Icon = "💵", Sub = "Clean Loot" },
                new CardDef { Val = 5, Item = "Vault Key", Color = "BLUE", Copies = 5, Icon = "🗝️", Sub = "Clean Loot" },
                new CardDef { Val = 6, Item = "Masterpiece", Color = "BLUE", Copies = 5, Icon = "🖼️", Sub = "Clean Loot" },
                new CardDef { Val = 7, Item = "Gold Ingot", Color = "BLUE", Copies = 5, Icon = "🧈", Sub = "Clean Loot" },
                new CardDef { Val = 8, Item = "Royal Crown", Color = "BLUE", Copies = 5, Icon = "👑", Sub = "Clean Loot" },
                new CardDef { Val = 9, Item = "Ancient Relic", Color = "BLUE", Copies = 5, Icon = "🏺", Sub = "Clean Loot" },
                new CardDef { Val = 10, Item = "Priceless Artifact", Color = "BLUE", Copies = 4, Icon = "🗿", Sub = "Clean Loot" },

                // Special Resets: 8 cards total (0)
                new CardDef { Val = 0, Item = "The Fence", Color = "SPECIAL", Copies = 4, Icon = "🤝", Sub = "Resets Pile to 0" },
                new CardDef { Val = 0, Item = "Smoke Bomb", Color = "SPECIAL", Copies = 2, Icon = "💨🔄", Sub = "Resets to 0 & Flips 🔄" },
                new CardDef { Val = 0, Item = "The Snitch", Color = "SPECIAL", Copies = 2, Icon = "🕵️🚨", Sub = "Resets to 0 & Next Draws 1" }
            };

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
            sb.AppendLine("  <title>Caught Red-Handed V2: The Heat - Low-Ink PnP Poker Deck (8.5x11)</title>");
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
            sb.AppendLine("    .card.marker .card-inner { background: #ffffff; color: #0f172a; border-width: 1.5px; }");
            sb.AppendLine("    .card.card-back .card-inner { background: #ffffff; border: 2px solid #0f172a; color: #0f172a; display: flex; flex-direction: column; align-items: center; justify-content: center; text-align: center; padding: 0.15in; position: relative; }");
            sb.AppendLine("    .card-back-pattern { position: absolute; inset: 4px; border: 1px dashed #cbd5e1; border-radius: 5px; pointer-events: none; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <div class=\"no-print-bar\">");
            sb.AppendLine("    <div><span style=\"font-weight: 900; font-size: 16px; color: #38bdf8;\">CAUGHT RED-HANDED V2</span><span style=\"color: #94a3b8; font-size: 13px; margin-left: 8px;\">• 54-Card Shedding Deck (8.5\" x 11\")</span><span style=\"background:#059669; color:#fff; font-size:10px; font-weight:800; padding:2px 8px; border-radius:12px; margin-left:12px;\">🌱 ECO INK-SAVER</span></div>");
            sb.AppendLine("    <button onclick=\"window.print()\">🖨️ PRINT DECK TO PDF / PRINTER</button>");
            sb.AppendLine("  </div>");
            sb.AppendLine("  <div class=\"sheet-wrapper\">");

            // 6 sheets of 9 cards = Exactly 54 play cards
            string[] sheetTitles = new string[] {
                "Sheet 1 of 8: Red Contraband (1s & 2s) & Blue 4s",
                "Sheet 2 of 8: Red Contraband (3s) & Blue 4s, 5s",
                "Sheet 3 of 8: Blue Clean Loot (5s & 6s)",
                "Sheet 4 of 8: Blue Clean Loot (6s, 7s & 8s)",
                "Sheet 5 of 8: Blue Clean Loot (8s & 9s)",
                "Sheet 6 of 8: Blue 10s & 8 Tactical Reset Cards (0)"
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

            // Sheet 7: Rules & Reference Cards (9 cards total)
            sb.AppendLine("    <div class=\"sheet-header-label\">Sheet 7 of 8: V2 Rules, Turn Guides & Reference Cards</div>");
            sb.AppendLine("    <div class=\"sheet\">");

            // Card 1: Turn Guide
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-color:#475569;\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">📋</span><span class=\"index-val\" style=\"color:#0f172a;\">RULES</span></div><span class=\"index-type\" style=\"color:#64748b;\">Turn Guide</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#d97706; color:#b45309;\">V2 CORE</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9px; font-weight:900; color:#0f172a; text-transform:uppercase;\">CLIMBING & SHEDDING</span><div style=\"font-size:6.6px; text-align:left; margin-top:3px; line-height:1.32; color:#334155;\">• On your turn, execute 1 choice:<br>&nbsp;&nbsp;<b>1. Play 1 Card</b> > current top discard<br>&nbsp;&nbsp;<b>-OR- Play Reset Card (0)</b> on any card!<br>&nbsp;&nbsp;<b>2. Draw 1 Card</b> from Locker if unable/unwilling to play.<br>• If drawn card is legal, you may play it immediately!</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">📋</span><span class=\"index-val\" style=\"color:#0f172a;\">RULES</span></div><span class=\"index-type\" style=\"color:#64748b;\">Turn Guide</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#d97706; color:#b45309;\">V2 CORE</div></div></div>");
            sb.AppendLine("      </div></div>");

            // Card 2: Contraband & Elimination
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-color:#dc2626;\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🚨</span><span class=\"index-val\" style=\"color:#dc2626;\">BUST</span></div><span class=\"index-type\" style=\"color:#b91c1c;\">Elimination</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#dc2626; color:#dc2626;\">3 STRIKES</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9px; font-weight:900; color:#dc2626; text-transform:uppercase;\">CAUGHT RED-HANDED!</span><div style=\"font-size:6.6px; text-align:left; margin-top:3px; line-height:1.32; color:#334155;\">• Round ends when any player empties hand (Getaway Car!).<br>• All remaining players reveal hands:<br>&nbsp;&nbsp;🚨 <b>Holding ANY Red (1,2,3)</b> = <b>+1 Strike!</b><br>&nbsp;&nbsp;🛡️ <b>Holding 0 Red</b> = <b>Escaped Clean (0 Strikes)!</b><br>• <b>3 Strikes = Eliminated!</b> Last survivor wins!</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🚨</span><span class=\"index-val\" style=\"color:#dc2626;\">BUST</span></div><span class=\"index-type\" style=\"color:#b91c1c;\">Elimination</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#dc2626; color:#dc2626;\">3 STRIKES</div></div></div>");
            sb.AppendLine("      </div></div>");

            // Card 3: The Reset Cards
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-color:#d97706;\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">⚡</span><span class=\"index-val\" style=\"color:#d97706;\">RESETS</span></div><span class=\"index-type\" style=\"color:#b45309;\">Tactics</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#d97706; color:#d97706;\">VALUE 0</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9px; font-weight:900; color:#b45309; text-transform:uppercase;\">TACTICAL RESET CARDS</span><div style=\"font-size:6.6px; text-align:left; margin-top:3px; line-height:1.32; color:#334155;\">Play on ANY card to reset count to 0:<br>• <b>The Fence (4x)</b>: Resets to 0. Opens window to dump low Red cards!<br>• <b>Smoke Bomb (2x)</b>: Resets to 0 & reverses turn order 🔄!<br>• <b>The Snitch (2x)</b>: Resets to 0 & forces next player to draw 1 card 🚨!</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">⚡</span><span class=\"index-val\" style=\"color:#d97706;\">RESETS</span></div><span class=\"index-type\" style=\"color:#b45309;\">Tactics</span></div><div class=\"pill\" style=\"background:#ffffff; border-color:#d97706; color:#d97706;\">VALUE 0</div></div></div>");
            sb.AppendLine("      </div></div>");

            // 6 Spare / Notes Cards to complete 9-card sheet
            for (int k = 0; k < 6; k++)
            {
                sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-style:dashed; border-color:#94a3b8;\">");
                sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🃏</span><span class=\"index-val\" style=\"color:#64748b;\">SPARE</span></div><span class=\"index-type\" style=\"color:#94a3b8;\">Blank</span></div></div></div>");
                sb.AppendLine("        <div class=\"center-art\"><span class=\"art-icon\" style=\"opacity:0.25;\">🃏</span><span class=\"art-name\" style=\"color:#64748b;\">V2 REPLACEMENT</span><span class=\"art-tier\" style=\"color:#94a3b8;\">Custom action</span></div>");
                sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🃏</span><span class=\"index-val\" style=\"color:#64748b;\">SPARE</span></div><span class=\"index-type\" style=\"color:#94a3b8;\">Blank</span></div></div></div>");
                sb.AppendLine("      </div></div>");
            }

            sb.AppendLine("    </div>");

            // Sheet 8: Card Backs
            sb.AppendLine("    <div class=\"sheet-header-label\">Sheet 8 of 8: Card Backs (Printer-Friendly Minimal Ink • Print on reverse of Sheets 1–7)</div>");
            sb.AppendLine("    <div class=\"sheet\">");
            for (int b = 0; b < 9; b++)
            {
                sb.AppendLine("      <div class=\"card card-back\">");
                sb.AppendLine("        <div class=\"card-inner\">");
                sb.AppendLine("          <div class=\"card-back-pattern\"></div>");
                sb.AppendLine("          <span style=\"font-size:30px; z-index:2;\">🔒🚨</span>");
                sb.AppendLine("          <div style=\"font-size:13px; font-weight:900; letter-spacing:1.5px; margin-top:8px; text-transform:uppercase; color:#0f172a; z-index:2;\">CAUGHT</div>");
                sb.AppendLine("          <div style=\"font-size:10px; font-weight:800; color:#dc2626; letter-spacing:2px; text-transform:uppercase; z-index:2;\">RED-HANDED V2</div>");
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

            string typeDisplay = (card.Color == "RED") ? "Contraband" : ((card.Color == "SPECIAL") ? "Reset" : "Clean Loot");
            string pillHtml = "";
            if (card.Color == "RED")
            {
                pillHtml = "<div class=\"pill\">🚨 TOXIC</div>";
            }
            else if (card.Color == "SPECIAL")
            {
                pillHtml = "<div class=\"pill\">⚡ RESET 0</div>";
            }
            else
            {
                pillHtml = "<div class=\"pill\">🔵 CLEAN</div>";
            }

            string ruleText = "";
            if (card.Color == "RED")
            {
                ruleText = "Must play > top card. Caught with this in hand = 1 STRIKE!";
            }
            else if (card.Color == "SPECIAL")
            {
                ruleText = card.Sub;
            }
            else
            {
                ruleText = "Safe Loot. Must play > top card to climb discard pile.";
            }

            string artTier = (card.Color == "RED") ? "🚨 Dangerous Evidence"
                           : (card.Color == "SPECIAL") ? "⚡ Tactical Action"
                           : "Safe Loot";

            sb.AppendLine(string.Format("      <div class=\"card {0}\">", colorClass));
            sb.AppendLine("        <div class=\"card-inner\">");

            // Top Header
            sb.AppendLine("          <div class=\"header-block\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\">");
            sb.AppendLine(string.Format("                  <span class=\"corner-icon\">{0}</span>", card.Icon));
            sb.AppendLine(string.Format("                  <span class=\"index-val\">{0}</span>", card.Val));
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
            sb.AppendLine(string.Format("            <span class=\"art-name\">{0}</span>", card.Item));
            sb.AppendLine(string.Format("            <span class=\"art-tier\">{0}</span>", artTier));
            sb.AppendLine("          </div>");

            // Bottom Inverted Header (Rotated 180deg)
            sb.AppendLine("          <div class=\"header-block inverted\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\">");
            sb.AppendLine(string.Format("                  <span class=\"corner-icon\">{0}</span>", card.Icon));
            sb.AppendLine(string.Format("                  <span class=\"index-val\">{0}</span>", card.Val));
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
