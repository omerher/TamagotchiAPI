//using System;
//using System.Collections.Generic;
//using System.Text;
//using TamagotchiUI.Models;
//using System.Linq;
//using System.Globalization;

//namespace TamagotchiUI.UI
//{
//    class PlayScreen : Screen
//    {
//        public PlayScreen() : base("Play")
//        {

//        }

//        public override void Show()
//        {
//            base.Show();
//            string[] categories = { "Eat", "Play", "Clean" };

//            // print out options to choose from
//            Console.WriteLine("Chose an activity category (1-3):");
//            int counter = 1;
//            foreach (string cat in categories)
//            {
//                Console.WriteLine($"{counter}) {cat}");
//                counter++;
//            }
//            Console.WriteLine("Or press any other key to go back to the main menu");

//            char c1 = Console.ReadKey().KeyChar;
//            int catergoryID = 0;
//            int.TryParse(c1.ToString(), out catergoryID);

//            if (catergoryID >= 1 && catergoryID <= categories.Length)
//            {
//                // gets all activities in the chosen category and shows
//                ObjectsList activities = GetActivitiesByCategory(catergoryID, categories[catergoryID - 1]);
//                base.Show();
//                activities.Show();

//                // asks the user which activity he wants to do and sends that to a function that does it
//                Console.WriteLine("\nEnter the ID of the activity you want to do:");
//                string c2 = Console.ReadLine();
//                int activityID = int.Parse(c2);
//                UIMain.db.DoActivity(catergoryID, activityID);

//                Console.WriteLine("Activity done! Press B to go back to your animal screen or any other key to go back to the main menu");
//                char c3 = Console.ReadKey().KeyChar;

//                if (c3 == 'B' || c3 == 'b')
//                    new ActiveAnimalScreen().Show();
//                else
//                    new MainMenu().Show();
//            }
//            else
//            {
//                // return to main menu
//                new MainMenu().Show();
//            }

//        }

//        // returns a list of the activities in the given category
//        private ObjectsList GetActivitiesByCategory(int id, string name)
//        {
//            List<Object> activities = (from activitiesList in UIMain.db.Activities
//                                       where activitiesList.ActivityCategoryId == id
//                                       select new
//                                       {
//                                           ID = activitiesList.ActivityId,
//                                           Name = activitiesList.ActivityName,
//                                           ImprovementRate = activitiesList.ImprovementRate
//                                       }).ToList<Object>();
//            return new ObjectsList(name, activities);
//        }

//        // gets the activity and makes all the calculations needed and updates the db
//        //private void DoActivity(int categoryID, int activityID)
//        //{
//        //    int improvementRate = UIMain.db.Activities.Where(a => a.ActivityId == activityID).FirstOrDefault().ImprovementRate; // find the desired activity, and get the improvement rate

//        //    // finds the animal age by dividing the difference of days between now and the creation of animal by 365
//        //    TimeSpan t1 = DateTime.UtcNow - new DateTime(1970, 1, 1);
//        //    int currentDays = (int)t1.TotalDays;
//        //    TimeSpan t2 = UIMain.CurrentPlayer.ActiveAnimal.CreationDate.Value - new DateTime(1970, 1, 1);
//        //    int animalCreationDays = (int)t2.TotalDays;
//        //    int animalAge = (currentDays - animalCreationDays) / 365;

//        //    // get all current stats and decrease 5, but if under if value is under 0, set it to 0
//        //    int aWeight = UIMain.CurrentPlayer.ActiveAnimal.Aweight <= 1 ? 0 : UIMain.CurrentPlayer.ActiveAnimal.Aweight - 1;  // weight is a different scale
//        //    int aHunger = UIMain.CurrentPlayer.ActiveAnimal.Ahunger <= 5 ? 0 : UIMain.CurrentPlayer.ActiveAnimal.Ahunger - 5;
//        //    int aHappiness = UIMain.CurrentPlayer.ActiveAnimal.Ahappiness <= 5 ? 0 : UIMain.CurrentPlayer.ActiveAnimal.Ahappiness - 5;
//        //    int aCleanliness = UIMain.CurrentPlayer.ActiveAnimal.Acleanliness <= 5 ? 0 : UIMain.CurrentPlayer.ActiveAnimal.Acleanliness - 5;
//        //    switch (categoryID)
//        //    {
//        //        case 1:
//        //            aHunger = (aHunger + improvementRate) > 100 ? 100 : aHunger + improvementRate; // checks if the stat is over 100, if it is set it to 100, otherwise set it normal
//        //            aWeight += improvementRate / 20;
//        //            break;
//        //        case 2:
//        //            aHappiness = (aHappiness + improvementRate) > 100 ? 100 : aHappiness + improvementRate; // checks if the stat is over 100, if it is set it to 100, otherwise set it normal
//        //            break;
//        //        case 3:
//        //            aCleanliness = (aCleanliness + improvementRate) > 100 ? 100 : aCleanliness + improvementRate; // checks if the stat is over 100, if it is set it to 100, otherwise set it normal
//        //            break;
//        //    }

//        //    // create new row in activities history
//        //    ActivitiesHistory ah = new ActivitiesHistory()
//        //    {
//        //        AnimalId = UIMain.CurrentPlayer.ActiveAnimal.AnimalId,
//        //        ActivityId = activityID,
//        //        AnimalAge = animalAge,
//        //        Aweight = aWeight,
//        //        Ahunger = aHunger,
//        //        Ahappiness = aHappiness,
//        //        Acleanliness = aCleanliness,
//        //        AnimalCycleStatusId = UIMain.CurrentPlayer.ActiveAnimal.LifeCycleId,
//        //        AnimalHealthStatusId = UIMain.CurrentPlayer.ActiveAnimal.HealthStatusId
//        //    };

//        //    UIMain.db.ActivitiesHistories.Add(ah);

//        //    // change active animals stats to updated stats
//        //    UIMain.CurrentPlayer.ActiveAnimal.Aweight = aWeight;
//        //    UIMain.CurrentPlayer.ActiveAnimal.Ahunger = aHunger;
//        //    UIMain.CurrentPlayer.ActiveAnimal.Ahappiness = aHappiness;
//        //    UIMain.CurrentPlayer.ActiveAnimal.Acleanliness = aCleanliness;

//        //    UIMain.db.SaveChanges();
//        //}
//    }
//}
