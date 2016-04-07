using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewValley;

namespace BirthdayMail
{
    public class Main : Mod
    {
        private NPC birthdayNPC;        // NPC object of the villiger who has a birthday
        private string birthdayMail;    // birthday mail item that corresponds to this NPC

        public override void Entry(params object[] objects)
        {
            // submit to events in StardewModdingAPI
            StardewModdingAPI.Events.TimeEvents.DayOfMonthChanged += Event_DayOfMonthChanged;
        }

        // runs when the day changes
        private void Event_DayOfMonthChanged(object sender, EventArgs e)
        {
            // test if today is someone's birthday...
            if (Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth) != null)
            {
                // ...set nirthday NPC and their mail item
                birthdayNPC = Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth);
                birthdayMail = birthdayNPC.name + "Birth";

                // if the player knows this NPC...
                if (Game1.player.friendships.ContainsKey(birthdayNPC.name))
                {
                    // ...add the birthday reminder to the mailbox
                    Game1.mailbox.Enqueue(birthdayMail);
                }
            }
            else // otherwise...
            {
                // ...reset veriables
                birthdayNPC = null;
                birthdayMail = null;
            }
        }
    }
}
