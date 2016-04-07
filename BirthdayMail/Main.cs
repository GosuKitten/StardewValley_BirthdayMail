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
        private static NPC birthdayNPC;

        public override void Entry(params object[] objects)
        {
            StardewModdingAPI.Events.TimeEvents.DayOfMonthChanged += Event_DayOfMonthChanged;
        }

        static void Event_DayOfMonthChanged(object sender, EventArgs e)
        {
            //Log.Debug("It's a new day!");
            if (Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth) != null)
            {
                birthdayNPC = Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth);
                //Log.Debug(string.Format("Today is a birthday of {0}", birthdayNPC.name));

                if (Game1.player.friendships.ContainsKey(birthdayNPC.name))
                {
                    //Log.Debug(string.Format("Player knows {0}", birthdayNPC.name));
                    Game1.mailbox.Enqueue(birthdayNPC.name + "Birth");
                }
                else
                {
                    //Log.Debug(string.Format("Player does not know {0}", birthdayNPC.name));
                }
            }
            else
            {
                //Log.Debug("No birthdays today");
                birthdayNPC = null;
            }

            //for (int i = 0; i < Game1.mailbox.Count; i++)
            //    Log.Debug(Game1.mailbox.ElementAt(i));
        }
    }
}
