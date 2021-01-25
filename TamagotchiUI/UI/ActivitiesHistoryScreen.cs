using System;
using System.Collections.Generic;
using System.Text;
using TamagotchiUI.Models;
using System.Linq;

namespace TamagotchiUI.UI
{
    class ActivitiesHistoryScreen : Screen
    {
        Animal a;

        public ActivitiesHistoryScreen(Animal a) : base("Activities History Screen")
        {
            this.a = a; 
        }

        public override void Show()
        {
            base.Show();

            List<Object> ActivitiesHistories = UIMain.db.GetActivitiesHistoryByID(a.AnimalId);
            ObjectsList list = new ObjectsList("Animals", ActivitiesHistories);
            list.Show();

            Console.WriteLine("\nPress any key to go back to the main menu!");
            Console.ReadKey();
            new MainMenu().Show();
        }
    }
}
