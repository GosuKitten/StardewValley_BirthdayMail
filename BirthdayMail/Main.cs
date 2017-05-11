using System;
using StardewModdingAPI;
using StardewValley;

namespace BirthdayMail
{
    public class Main : Mod
    {
        /*********
        ** Properties
        *********/
        private bool firstUpdate = true;


        /*********
        ** Public methods
        *********/
        public override void Entry(IModHelper helper)
        {
            // submit to events in StardewModdingAPI
            StardewModdingAPI.Events.TimeEvents.DayOfMonthChanged += Event_DayOfMonthChanged;
        }


        /*********
        ** Private methods
        *********/
        // runs once per second from the start of the game 
        // used for first update to solve the mail not being sent on initial load when adding the mod
        private void Event_OneSecondTick(object sender, EventArgs e)
        {
            // ...check for birthdays and send mail
            BirthdayMail();
            firstUpdate = false;
            // ...unsubmit from this event for optimization.
            StardewModdingAPI.Events.GameEvents.OneSecondTick -= Event_OneSecondTick;
        }

        // runs when the day changes
        private void Event_DayOfMonthChanged(object sender, EventArgs e)
        {
            // if this is the initial update...
            if (firstUpdate)
            {
                // subscribe the Event_OneSecondTick() function to the OneSecondTick event
                StardewModdingAPI.Events.GameEvents.OneSecondTick += Event_OneSecondTick;
            }
            else // otherwise...
            {
                // ...check for birthdays and send mail as usual
                BirthdayMail();
            }

        }

        // checks for birthdays and sends mail if needed
        private void BirthdayMail()
        {
            // test if today is someone's birthday...
            if (Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth) != null)
            {
                // ...set birthday NPC and their mail item
                NPC birthdayNPC = Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth);
                string birthdayMail = birthdayNPC.name + "Birth";

                // if the player knows this NPC...
                if (Game1.player.friendships.ContainsKey(birthdayNPC.name))
                {
                    // ...if the mailbox doesn't already have the birthday mail...
                    if (!Game1.mailbox.Contains(birthdayMail))
                    {
                        // ...add the birthday reminder to the mailbox
                        Game1.mailbox.Enqueue(birthdayMail);
                    }
                }
            }
        }
    }
}
