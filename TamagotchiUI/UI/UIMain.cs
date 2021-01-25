using System;
using System.Collections.Generic;
using System.Text;
using TamagotchiUI.Models;
using System.Linq;

namespace TamagotchiUI.UI
{
    class UIMain
    {
        //UI Main object is perfect for storing all UI state as it is initialized first and detroyed last.
        public static Player CurrentPlayer { get; set; }
        public static TamagotchiContext db { get; private set; }

        private Screen initialScreen;
        public UIMain(Screen initial)
        {
            this.initialScreen = initial;
        }
        public void ApplicationStart()
        {
            //Initialize db context and current player
            db = new TamagotchiContext();
            CurrentPlayer = null;
            //Show Screen and start app!
            initialScreen.Show();
            //Save changes to db
            db.SaveChanges();
        }
    }
}
