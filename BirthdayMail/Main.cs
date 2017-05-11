using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace BirthdayMail
{
    public class Main : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            TimeEvents.AfterDayStarted += this.TimeEvents_AfterDayStarted;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>The event called when a new day begins.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void TimeEvents_AfterDayStarted(object sender, EventArgs e)
        {
            this.BirthdayMail();
        }

        /// <summary>Check if there's a birthday today, and add mail if needed.</summary>
        private void BirthdayMail()
        {
            // get birthday NPC
            NPC npc = Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth);
            if (npc == null || !Game1.player.friendships.ContainsKey(npc.name))
                return;

            // add reminder to mailbox
            string mailID = npc.name + "Birth";
            if (!Game1.mailbox.Contains(mailID))
                Game1.mailbox.Enqueue(mailID);
        }
    }
}
