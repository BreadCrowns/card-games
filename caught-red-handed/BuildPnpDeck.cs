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
        public string Color; // BLUE, RED, GREEN
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
                // Petty Clean (Blue)
                new CardDef { Item = "Cash", Name = "Cash Stack", Color = "BLUE", Val = 5, Copies = 4, Icon = "💵", IsGrand = false },
                new CardDef { Item = "Diamond", Name = "Cut Diamond", Color = "BLUE", Val = 5, Copies = 4, Icon = "💎", IsGrand = false },
                new CardDef { Item = "Watch", Name = "Gold Watch", Color = "BLUE", Val = 5, Copies = 4, Icon = "⌚", IsGrand = false },
                new CardDef { Item = "Key", Name = "Vault Key", Color = "BLUE", Val = 5, Copies = 4, Icon = "🗝️", IsGrand = false },

                // Grand Clean (Blue)
                new CardDef { Item = "Painting", Name = "Masterpiece", Color = "BLUE", Val = 5, Copies = 4, Icon = "🖼️", IsGrand = true },
                new CardDef { Item = "Gold", Name = "Gold Ingot", Color = "BLUE", Val = 5, Copies = 4, Icon = "🧈", IsGrand = true },
                new CardDef { Item = "Crown", Name = "Royal Crown", Color = "BLUE", Val = 5, Copies = 3, Icon = "👑", IsGrand = true },
                new CardDef { Item = "Relic", Name = "Ancient Relic", Color = "BLUE", Val = 5, Copies = 3, Icon = "🏺", IsGrand = true },

                // Petty Contraband (Red)
                new CardDef { Item = "Cash", Name = "Marked Cash", Color = "RED", Val = 10, Copies = 2, Icon = "💵", IsGrand = false },
                new CardDef { Item = "Diamond", Name = "Blood Diamond", Color = "RED", Val = 10, Copies = 2, Icon = "💎", IsGrand = false },
                new CardDef { Item = "Watch", Name = "Tracked Watch", Color = "RED", Val = 10, Copies = 2, Icon = "⌚", IsGrand = false },
                new CardDef { Item = "Key", Name = "Evidence Key", Color = "RED", Val = 10, Copies = 2, Icon = "🗝️", IsGrand = false },

                // Grand Contraband (Red)
                new CardDef { Item = "Painting", Name = "Stolen Canvas", Color = "RED", Val = 15, Copies = 2, Icon = "🖼️", IsGrand = true },
                new CardDef { Item = "Gold", Name = "Hot Gold Bar", Color = "RED", Val = 15, Copies = 2, Icon = "🧈", IsGrand = true },
                new CardDef { Item = "Crown", Name = "Cursed Crown", Color = "RED", Val = 15, Copies = 2, Icon = "👑", IsGrand = true },
                new CardDef { Item = "Relic", Name = "Smuggled Relic", Color = "RED", Val = 15, Copies = 2, Icon = "🏺", IsGrand = true },

                // Counterfeit Wilds (Green)
                new CardDef { Item = "Wild", Name = "Fake Receipt", Color = "GREEN", Val = 0, Copies = 8, Icon = "🎭", IsGrand = false }
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
            sb.AppendLine("  <title>Caught Red-Handed - Two-Way Symmetrical PnP Poker Deck (8.5x11)</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@500;600;700;800;900&display=swap');");
            sb.AppendLine("    @page { size: letter portrait; margin: 0.25in 0.5in 0.25in 0.5in; }");
            sb.AppendLine("    * { box-sizing: border-box; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }");
            sb.AppendLine("    body { margin: 0; padding: 0; font-family: 'Montserrat', system-ui, sans-serif; background: #e2e8f0; color: #0f172a; }");
            sb.AppendLine("    .no-print-bar { background: #0f172a; color: #f8fafc; padding: 12px 24px; display: flex; justify-content: space-between; align-items: center; position: sticky; top: 0; z-index: 1000; box-shadow: 0 4px 12px rgba(0,0,0,0.25); }");
            sb.AppendLine("    .no-print-bar button { background: #e11d48; color: white; border: none; padding: 8px 20px; border-radius: 8px; font-weight: 800; font-size: 14px; cursor: pointer; display: flex; align-items: center; gap: 8px; transition: background 0.2s; }");
            sb.AppendLine("    .no-print-bar button:hover { background: #be123c; }");
            sb.AppendLine("    @media print {");
            sb.AppendLine("      .no-print-bar, .sheet-header-label { display: none !important; }");
            sb.AppendLine("      body { background: #fff !important; margin: 0 !important; padding: 0 !important; }");
            sb.AppendLine("      .sheet-wrapper { padding: 0 !important; margin: 0 !important; }");
            sb.AppendLine("      .sheet { margin: 0 auto !important; box-shadow: none !important; page-break-after: always !important; page-break-inside: avoid !important; }");
            sb.AppendLine("    }");
            sb.AppendLine("    .sheet-wrapper { padding: 20px 0; }");
            sb.AppendLine("    .sheet-header-label { text-align: center; font-size: 11px; font-weight: 700; color: #64748b; margin-bottom: 6px; text-transform: uppercase; letter-spacing: 0.1em; }");
            sb.AppendLine("    .sheet { width: 7.5in; height: 10.5in; display: grid; grid-template-columns: repeat(3, 2.5in); grid-template-rows: repeat(3, 3.5in); gap: 0; margin: 0 auto 30px auto; background: #ffffff; box-shadow: 0 8px 24px rgba(0,0,0,0.15); position: relative; }");
            sb.AppendLine("    .card { width: 2.5in; height: 3.5in; border: 1px dashed #94a3b8; padding: 0.1in; position: relative; display: flex; flex-direction: column; justify-content: space-between; overflow: hidden; background: #ffffff; user-select: none; }");
            sb.AppendLine("    .card-inner { width: 100%; height: 100%; border-radius: 10px; border: 2px solid; display: flex; flex-direction: column; justify-content: space-between; padding: 0.08in; position: relative; overflow: hidden; }");
            sb.AppendLine("    .header-block { display: flex; flex-direction: column; width: 100%; z-index: 2; }");
            sb.AppendLine("    .header-row { display: flex; justify-content: space-between; align-items: flex-start; width: 100%; }");
            sb.AppendLine("    .index-group { display: flex; flex-direction: column; line-height: 1; }");
            sb.AppendLine("    .corner-top { display: flex; align-items: center; gap: 3px; }");
            sb.AppendLine("    .corner-icon { font-size: 15px; line-height: 1; }");
            sb.AppendLine("    .index-val { font-size: 14px; font-weight: 900; letter-spacing: -0.5px; }");
            sb.AppendLine("    .index-type { font-size: 7px; font-weight: 800; text-transform: uppercase; letter-spacing: 0.4px; margin-top: 1.5px; opacity: 0.85; }");
            sb.AppendLine("    .pill { font-size: 7.5px; font-weight: 900; padding: 2px 5px; border-radius: 4px; border: 1.2px solid; text-transform: uppercase; letter-spacing: 0.2px; display: inline-flex; align-items: center; gap: 2px; box-shadow: 0 1px 2px rgba(0,0,0,0.06); }");
            sb.AppendLine("    .sub-rule-bar { font-size: 6.8px; font-weight: 700; text-align: center; border-radius: 3px; padding: 1.5px 3px; margin-top: 2px; background: rgba(0,0,0,0.035); border: 1px solid rgba(0,0,0,0.06); line-height: 1.15; }");
            sb.AppendLine("    .center-art { display: flex; flex-direction: column; align-items: center; justify-content: center; margin: auto 0; text-align: center; z-index: 2; }");
            sb.AppendLine("    .art-icon { font-size: 34px; line-height: 1; filter: drop-shadow(0 2px 3px rgba(0,0,0,0.12)); }");
            sb.AppendLine("    .art-name { font-size: 12px; font-weight: 900; text-transform: uppercase; letter-spacing: 0.4px; margin-top: 2px; line-height: 1.1; }");
            sb.AppendLine("    .art-tier { font-size: 7.5px; font-weight: 800; text-transform: uppercase; letter-spacing: 0.6px; margin-top: 1px; opacity: 0.85; }");
            sb.AppendLine("    .inverted { transform: rotate(180deg); }");

            // Theme colors
            sb.AppendLine("    .card.blue .card-inner { background: linear-gradient(180deg, #f0f9ff 0%, #ffffff 50%, #f0f9ff 100%); border-color: #0284c7; color: #0369a1; }");
            sb.AppendLine("    .card.blue .index-val { color: #0284c7; }");
            sb.AppendLine("    .card.blue .index-type { color: #0369a1; }");
            sb.AppendLine("    .card.blue .art-name { color: #0c4a6e; }");
            sb.AppendLine("    .card.blue .pill { background: #fff1f2; border-color: #e11d48; color: #be123c; }");

            sb.AppendLine("    .card.blue.grand .card-inner { background: linear-gradient(180deg, #f0f9ff 0%, #fffbeb 50%, #f0f9ff 100%); border-color: #0284c7; }");
            sb.AppendLine("    .card.blue.grand .pill { background: #fffbeb; border-color: #f59e0b; color: #b45309; }");
            sb.AppendLine("    .card.blue.grand .art-tier { color: #b45309; font-weight: 900; }");

            sb.AppendLine("    .card.red .card-inner { background: linear-gradient(180deg, #fff1f2 0%, #ffffff 50%, #fff1f2 100%); border-color: #e11d48; color: #be123c; }");
            sb.AppendLine("    .card.red .index-val { color: #e11d48; }");
            sb.AppendLine("    .card.red .index-type { color: #be123c; }");
            sb.AppendLine("    .card.red .art-name { color: #881337; }");
            sb.AppendLine("    .card.red .pill { background: #e11d48; border-color: #9f1239; color: #ffffff; }");

            sb.AppendLine("    .card.red.grand .card-inner { background: linear-gradient(180deg, #fff1f2 0%, #fef3c7 50%, #fff1f2 100%); border-color: #b91c1c; }");
            sb.AppendLine("    .card.red.grand .pill { background: #b91c1c; border-color: #7f1d1d; color: #ffffff; }");

            sb.AppendLine("    .card.green .card-inner { background: linear-gradient(180deg, #ecfdf5 0%, #ffffff 50%, #ecfdf5 100%); border-color: #059669; color: #047857; }");
            sb.AppendLine("    .card.green .index-val { color: #059669; }");
            sb.AppendLine("    .card.green .index-type { color: #047857; }");
            sb.AppendLine("    .card.green .art-name { color: #064e3b; }");
            sb.AppendLine("    .card.green .pill { background: #eff6ff; border-color: #0284c7; color: #0369a1; }");

            sb.AppendLine("    .card.marker .card-inner { background: linear-gradient(180deg, #0f172a 0%, #1e293b 50%, #0f172a 100%); border-color: #f59e0b; color: #f8fafc; }");
            sb.AppendLine("    .card.card-back .card-inner { background: radial-gradient(circle at center, #1e293b 0%, #0f172a 100%); border: 3px double #f59e0b; color: #f59e0b; display: flex; flex-direction: column; align-items: center; justify-content: center; text-align: center; padding: 0.2in; }");
            sb.AppendLine("    .card-back-pattern { position: absolute; inset: 6px; border: 1px dashed rgba(245, 158, 11, 0.3); border-radius: 6px; pointer-events: none; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <div class=\"no-print-bar\">");
            sb.AppendLine("    <div><span style=\"font-weight: 900; font-size: 16px; color: #f43f5e;\">CAUGHT RED-HANDED</span><span style=\"color: #94a3b8; font-size: 13px; margin-left: 8px;\">• 56-Card Two-Way Symmetrical PnP Poker Deck (8.5\" x 11\")</span></div>");
            sb.AppendLine("    <button onclick=\"window.print()\">🖨️ PRINT DECK TO PDF / PRINTER</button>");
            sb.AppendLine("  </div>");
            sb.AppendLine("  <div class=\"sheet-wrapper\">");

            // 6 sheets of 9 cards (54 cards)
            string[] sheetTitles = new string[] {
                "Sheet 1 of 8: Clean Petty Loot (Cash & Diamonds)",
                "Sheet 2 of 8: Clean Petty & Grand Loot (Watches, Keys & Paintings)",
                "Sheet 3 of 8: Clean Grand Loot (Paintings, Gold & Crowns)",
                "Sheet 4 of 8: Relics & Red Contraband (Cash, Diamonds, Watches)",
                "Sheet 5 of 8: Red Contraband (Keys, Paintings, Gold, Crowns & Relic)",
                "Sheet 6 of 8: Relic Contraband & Counterfeit Wilds (Blue-Only Connector)"
            };

            for (int s = 0; s < 6; s++)
            {
                sb.AppendLine(string.Format("    <div class=\"sheet-header-label\">{0}</div>", sheetTitles[s]));
                sb.AppendLine("    <div class=\"sheet\">");

                for (int c = 0; c < 9; c++)
                {
                    int idx = s * 9 + c;
                    var card = deck[idx];
                    string colorClass = card.Color.ToLower();
                    string tierClass = card.IsGrand ? "grand" : "petty";

                    string valDisplay = (card.Color == "GREEN") ? "WILD" : ("+" + card.Val);
                    string typeDisplay = (card.Color == "BLUE") ? "Clean" : ((card.Color == "RED") ? "Contraband" : "Counterfeit");

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
                    else
                    {
                        pillHtml = "<div class=\"pill\">🔵 ONLY</div>";
                    }

                    string ruleText = "";
                    if (card.Color == "BLUE")
                    {
                        int redMeld = card.IsGrand ? 20 : 15;
                        ruleText = string.Format("Pairs w/ Blue {0} (10pts) • Red {0} ({1}pts)", card.Item, redMeld);
                    }
                    else if (card.Color == "RED")
                    {
                        int redMeld = card.IsGrand ? 20 : 15;
                        ruleText = string.Format("Pairs ONLY w/ Blue {0} (+{1}pts) • Most Red = 0pts!", card.Item, redMeld);
                    }
                    else
                    {
                        ruleText = "Can only pair with any blue card and flips camera card";
                    }

                    string artTier = (card.Color == "BLUE") ? (card.IsGrand ? "👑 Grand Heist" : "⭐ Petty Loot")
                                   : (card.Color == "RED") ? (card.IsGrand ? "👑 Grand Hot" : "⭐ Petty Hot")
                                   : "Pairs w/ Any Blue";

                    string artIcon = (card.Color == "GREEN") ? "🎭🔄" : card.Icon;

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
                }

                sb.AppendLine("    </div>");
            }

            // Sheet 7: Reference & Camera Markers
            sb.AppendLine("    <div class=\"sheet-header-label\">Sheet 7 of 8: Quick Reference Rules & Circular Camera Direction Markers</div>");
            sb.AppendLine("    <div class=\"sheet\">");

            // Clockwise Camera Marker
            sb.AppendLine("      <div class=\"card marker\">");
            sb.AppendLine("        <div class=\"card-inner\" style=\"border-color: #38bdf8;\">");
            sb.AppendLine("          <div class=\"header-block\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\"><span class=\"corner-icon\">📹</span><span class=\"index-val\" style=\"color:#38bdf8; font-size:13px;\">↻ CW</span></div>");
            sb.AppendLine("                <span class=\"index-type\" style=\"color:#7dd3fc;\">Pass Left</span>");
            sb.AppendLine("              </div>");
            sb.AppendLine("              <div class=\"pill\" style=\"background:#0369a1; border-color:#38bdf8; color:#fff;\">↻ CLOCKWISE</div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"sub-rule-bar\" style=\"color:#7dd3fc;\">Table Center • Default Play Direction (Pass to Left)</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div class=\"center-art\">");
            sb.AppendLine("            <div style=\"width:72px; height:72px; display:flex; align-items:center; justify-content:center; margin:0 auto;\">");
            sb.AppendLine("              <svg viewBox=\"0 0 100 100\" width=\"72\" height=\"72\">");
            sb.AppendLine("                <circle cx=\"50\" cy=\"50\" r=\"45\" fill=\"none\" stroke=\"#0284c7\" stroke-width=\"1.2\" stroke-dasharray=\"3,3\" opacity=\"0.6\"/>");
            sb.AppendLine("                <path d=\"M 22 46 A 34 34 0 0 1 76 30\" fill=\"none\" stroke=\"#38bdf8\" stroke-width=\"7\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <polygon points=\"70,16 90,30 72,42\" fill=\"#38bdf8\"/>");
            sb.AppendLine("                <path d=\"M 78 54 A 34 34 0 0 1 24 70\" fill=\"none\" stroke=\"#38bdf8\" stroke-width=\"7\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <polygon points=\"30,84 10,70 28,58\" fill=\"#38bdf8\"/>");
            sb.AppendLine("                <path d=\"M 44 38 L 44 32 L 54 32\" fill=\"none\" stroke=\"#38bdf8\" stroke-width=\"3\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <rect x=\"34\" y=\"38\" width=\"26\" height=\"18\" rx=\"4\" fill=\"#0f172a\" stroke=\"#38bdf8\" stroke-width=\"2.5\"/>");
            sb.AppendLine("                <polygon points=\"60,43 72,37 72,51 60,45\" fill=\"#38bdf8\"/>");
            sb.AppendLine("                <circle cx=\"41\" cy=\"47\" r=\"2.8\" fill=\"#ef4444\"/>");
            sb.AppendLine("                <circle cx=\"50\" cy=\"47\" r=\"3.5\" fill=\"none\" stroke=\"#38bdf8\" stroke-width=\"1.8\"/>");
            sb.AppendLine("              </svg>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <span class=\"art-name\" style=\"color:#38bdf8;\">SECURITY CAMERA</span>");
            sb.AppendLine("            <span class=\"art-tier\" style=\"color:#bae6fd; font-weight:800;\">PASS CLOCKWISE ↻</span>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div class=\"header-block inverted\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\"><span class=\"corner-icon\">📹</span><span class=\"index-val\" style=\"color:#38bdf8; font-size:13px;\">↻ CW</span></div>");
            sb.AppendLine("                <span class=\"index-type\" style=\"color:#7dd3fc;\">Pass Left</span>");
            sb.AppendLine("              </div>");
            sb.AppendLine("              <div class=\"pill\" style=\"background:#0369a1; border-color:#38bdf8; color:#fff;\">↻ CLOCKWISE</div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"sub-rule-bar\" style=\"color:#7dd3fc;\">Flip 180° when Green Wild is banked! 🔄</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");

            // Counter-Clockwise Camera Marker
            sb.AppendLine("      <div class=\"card marker\">");
            sb.AppendLine("        <div class=\"card-inner\" style=\"border-color: #f43f5e;\">");
            sb.AppendLine("          <div class=\"header-block\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\"><span class=\"corner-icon\">📹</span><span class=\"index-val\" style=\"color:#f43f5e; font-size:13px;\">↺ CCW</span></div>");
            sb.AppendLine("                <span class=\"index-type\" style=\"color:#fda4af;\">Pass Right</span>");
            sb.AppendLine("              </div>");
            sb.AppendLine("              <div class=\"pill\" style=\"background:#be123c; border-color:#f43f5e; color:#fff;\">↺ COUNTER-CW</div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"sub-rule-bar\" style=\"color:#fda4af;\">Table Center • Reversed Play Direction (Pass to Right)</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div class=\"center-art\">");
            sb.AppendLine("            <div style=\"width:72px; height:72px; display:flex; align-items:center; justify-content:center; margin:0 auto;\">");
            sb.AppendLine("              <svg viewBox=\"0 0 100 100\" width=\"72\" height=\"72\">");
            sb.AppendLine("                <circle cx=\"50\" cy=\"50\" r=\"45\" fill=\"none\" stroke=\"#be123c\" stroke-width=\"1.2\" stroke-dasharray=\"3,3\" opacity=\"0.6\"/>");
            sb.AppendLine("                <path d=\"M 78 46 A 34 34 0 0 0 24 30\" fill=\"none\" stroke=\"#f43f5e\" stroke-width=\"7\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <polygon points=\"30,16 10,30 28,42\" fill=\"#f43f5e\"/>");
            sb.AppendLine("                <path d=\"M 22 54 A 34 34 0 0 0 76 70\" fill=\"none\" stroke=\"#f43f5e\" stroke-width=\"7\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <polygon points=\"70,84 90,70 72,58\" fill=\"#f43f5e\"/>");
            sb.AppendLine("                <path d=\"M 56 38 L 56 32 L 46 32\" fill=\"none\" stroke=\"#f43f5e\" stroke-width=\"3\" stroke-linecap=\"round\"/>");
            sb.AppendLine("                <rect x=\"40\" y=\"38\" width=\"26\" height=\"18\" rx=\"4\" fill=\"#0f172a\" stroke=\"#f43f5e\" stroke-width=\"2.5\"/>");
            sb.AppendLine("                <polygon points=\"40,43 28,37 28,51 40,45\" fill=\"#f43f5e\"/>");
            sb.AppendLine("                <circle cx=\"59\" cy=\"47\" r=\"2.8\" fill=\"#ef4444\"/>");
            sb.AppendLine("                <circle cx=\"50\" cy=\"47\" r=\"3.5\" fill=\"none\" stroke=\"#f43f5e\" stroke-width=\"1.8\"/>");
            sb.AppendLine("              </svg>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <span class=\"art-name\" style=\"color:#f43f5e;\">REVERSE CAMERA</span>");
            sb.AppendLine("            <span class=\"art-tier\" style=\"color:#fecdd3; font-weight:800;\">COUNTER-CLOCKWISE ↺</span>");
            sb.AppendLine("          </div>");
            sb.AppendLine("          <div class=\"header-block inverted\">");
            sb.AppendLine("            <div class=\"header-row\">");
            sb.AppendLine("              <div class=\"index-group\">");
            sb.AppendLine("                <div class=\"corner-top\"><span class=\"corner-icon\">📹</span><span class=\"index-val\" style=\"color:#f43f5e; font-size:13px;\">↺ CCW</span></div>");
            sb.AppendLine("                <span class=\"index-type\" style=\"color:#fda4af;\">Pass Right</span>");
            sb.AppendLine("              </div>");
            sb.AppendLine("              <div class=\"pill\" style=\"background:#be123c; border-color:#f43f5e; color:#fff;\">↺ COUNTER-CW</div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"sub-rule-bar\" style=\"color:#fda4af;\">Flip back when another Wild is banked! 🔄</div>");
            sb.AppendLine("          </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");

            // Card 3: Turn Structure
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">📋</span><span class=\"index-val\" style=\"color:#f59e0b;\">HEIST</span></div><span class=\"index-type\" style=\"color:#cbd5e1;\">Turn Guide</span></div><div class=\"pill\" style=\"background:#b45309; border-color:#f59e0b; color:#fff;\">RULES</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9.5px; font-weight:900; color:#f59e0b; text-transform:uppercase;\">1. DRAW • 2. MELD • 3. PASS</span><div style=\"font-size:6.8px; text-align:left; margin-top:3px; line-height:1.35; color:#cbd5e1;\">• <b>1. Draw</b> 1 card from Locker.<br>• <b>2. Meld</b> 1 pair (optional):<br>&nbsp;&nbsp;<b>GOLDEN LAW: Needs Blue!</b><br>&nbsp;&nbsp;🔵+🔵 = 10 pts (Clean)<br>&nbsp;&nbsp;🔵+🔴 Petty = 15 pts | Grand = 20 pts<br>&nbsp;&nbsp;🔵+🎭 Wild = 5 pts & Flips Camera 🔄<br>• <b>3. Pass</b> 1 card face-down to neighbor.<br><span style=\"color:#f43f5e; font-weight:bold;\">Red+Red & Red+Green are strictly illegal!</span></div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">📋</span><span class=\"index-val\" style=\"color:#f59e0b;\">HEIST</span></div><span class=\"index-type\" style=\"color:#cbd5e1;\">Turn Guide</span></div><div class=\"pill\" style=\"background:#b45309; border-color:#f59e0b; color:#fff;\">RULES</div></div></div>");
            sb.AppendLine("      </div></div>");

            // Card 4: Scoring & Fall Guy
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🚨</span><span class=\"index-val\" style=\"color:#f43f5e;\">SCORING</span></div><span class=\"index-type\" style=\"color:#cbd5e1;\">End Round</span></div><div class=\"pill\" style=\"background:#b45309; border-color:#f59e0b; color:#fff;\">FALL GUY</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9.5px; font-weight:900; color:#f43f5e; text-transform:uppercase;\">THE ESCAPE CHECK</span><div style=\"font-size:6.8px; text-align:left; margin-top:3px; line-height:1.35; color:#cbd5e1;\">• <b>Getaway Driver</b> (First empty hand) triggers alarm & banks full haul!<br>• <b>Caught Red-Handed</b>: Only player(s) holding the <b>MOST Red Contraband</b> take the fall = <b>0 POINTS</b>!<br>• <b>Evaded Arrest</b>: Anyone holding fewer Red cards than the leader banks full stash!<br>• <b>Win Goal</b>: First to 300 pts (200 in 2P Duel).</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🚨</span><span class=\"index-val\" style=\"color:#f43f5e;\">SCORING</span></div><span class=\"index-type\" style=\"color:#cbd5e1;\">End Round</span></div><div class=\"pill\" style=\"background:#b45309; border-color:#f59e0b; color:#fff;\">FALL GUY</div></div></div>");
            sb.AppendLine("      </div></div>");

            // Card 5: 2-Player Quickdraw Setup C1
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">⚔️</span><span class=\"index-val\" style=\"color:#38bdf8;\">2-PLAYER</span></div><span class=\"index-type\" style=\"color:#cbd5e1;\">Duel Setup</span></div><div class=\"pill\" style=\"background:#0369a1; border-color:#38bdf8; color:#fff;\">28 CARDS</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9.5px; font-weight:900; color:#38bdf8; text-transform:uppercase;\">QUICKDRAW DUEL (28c)</span><div style=\"font-size:6.8px; text-align:left; margin-top:3px; line-height:1.35; color:#cbd5e1;\">For high-voltage 2-player matches:<br>• Remove all 4 Grand items & 4 Wilds.<br>• Play with Petty items only:<br>&nbsp;&nbsp;Cash, Diamond, Watch, Key + 4 Wilds.<br>• Deal 7 cards each (14 in Locker).<br>• Target Score: <b>200 Points</b>.<br>• Rapid 12-turn rounds with 93% escapes!</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">⚔️</span><span class=\"index-val\" style=\"color:#38bdf8;\">2-PLAYER</span></div><span class=\"index-type\" style=\"color:#cbd5e1;\">Duel Setup</span></div><div class=\"pill\" style=\"background:#0369a1; border-color:#38bdf8; color:#fff;\">28 CARDS</div></div></div>");
            sb.AppendLine("      </div></div>");

            // Card 6: Double Deck 5-6 Players
            sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\">");
            sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">👑</span><span class=\"index-val\" style=\"color:#a855f7;\">5-6 PLAYERS</span></div><span class=\"index-type\" style=\"color:#cbd5e1;\">Syndicate</span></div><div class=\"pill\" style=\"background:#6b21a8; border-color:#a855f7; color:#fff;\">108 CARDS</div></div></div>");
            sb.AppendLine("        <div class=\"center-art\" style=\"margin:2px 0;\"><span style=\"font-size:9.5px; font-weight:900; color:#a855f7; text-transform:uppercase;\">GRAND SYNDICATE</span><div style=\"font-size:6.8px; text-align:left; margin-top:3px; line-height:1.35; color:#cbd5e1;\">For 5 or 6 players at the table:<br>• Shuffle <b>2 full 54-card decks</b> together.<br>• Total: 60 Blue, 32 Red, 16 Wilds.<br>• Deal 7 cards each.<br>• Target Score: <b>300 Points</b>.<br>• Close match rate exceeds 55%!</div></div>");
            sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">👑</span><span class=\"index-val\" style=\"color:#a855f7;\">5-6 PLAYERS</span></div><span class=\"index-type\" style=\"color:#cbd5e1;\">Syndicate</span></div><div class=\"pill\" style=\"background:#6b21a8; border-color:#a855f7; color:#fff;\">108 CARDS</div></div></div>");
            sb.AppendLine("      </div></div>");

            // 3 Blank Spare Cards
            for (int k = 0; k < 3; k++)
            {
                sb.AppendLine("      <div class=\"card marker\"><div class=\"card-inner\" style=\"border-style:dashed; border-color:#64748b;\">");
                sb.AppendLine("        <div class=\"header-block\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🃏</span><span class=\"index-val\" style=\"color:#64748b;\">SPARE</span></div><span class=\"index-type\" style=\"color:#94a3b8;\">Blank</span></div></div></div>");
                sb.AppendLine("        <div class=\"center-art\"><span class=\"art-icon\" style=\"opacity:0.3;\">🃏</span><span class=\"art-name\" style=\"color:#64748b;\">REPLACEMENT</span><span class=\"art-tier\" style=\"color:#94a3b8;\">Create custom item</span></div>");
                sb.AppendLine("        <div class=\"header-block inverted\"><div class=\"header-row\"><div class=\"index-group\"><div class=\"corner-top\"><span class=\"corner-icon\">🃏</span><span class=\"index-val\" style=\"color:#64748b;\">SPARE</span></div><span class=\"index-type\" style=\"color:#94a3b8;\">Blank</span></div></div></div>");
                sb.AppendLine("      </div></div>");
            }

            sb.AppendLine("    </div>");

            // Sheet 8: 9 Card Backs
            sb.AppendLine("    <div class=\"sheet-header-label\">Sheet 8 of 8: Card Backs (Print on reverse of Sheets 1–7)</div>");
            sb.AppendLine("    <div class=\"sheet\">");
            for (int b = 0; b < 9; b++)
            {
                sb.AppendLine("      <div class=\"card card-back\"><div class=\"card-inner\"><div class=\"card-back-pattern\"></div><span style=\"font-size:32px; filter:drop-shadow(0 2px 6px rgba(0,0,0,0.4));\">🔒🚨</span><div style=\"font-size:13px; font-weight:900; letter-spacing:1px; margin-top:8px; text-transform:uppercase;\">CAUGHT</div><div style=\"font-size:10px; font-weight:800; color:#f43f5e; letter-spacing:1.5px; text-transform:uppercase;\">RED-HANDED</div></div></div>");
            }
            sb.AppendLine("    </div>");

            sb.AppendLine("  </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(outHtmlPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine("SUCCESS: Generated " + outHtmlPath);
        }
    }
}
