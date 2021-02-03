//using System;
//using System.Collections.Generic;
//using System.Text;
//using TamagotchiUI.Models;
//using System.Linq;

//namespace TamagotchiUI.UI
//{
//    class PastAnimalScreen : Screen
//    {
//        public int animalID;
//        public PastAnimalScreen(int AnimalID) : base("Past Animal")
//        {
//            this.animalID = AnimalID;
//        }
//        public override void Show()
//        {
//            base.Show();
//            Animal AAnimal = UIMain.db.GetAnimalByID(animalID); // searches for the animal
//            ObjectView showAnimal = new ObjectView("", AAnimal); 

//            showAnimal.Show(); //printing animal

//            //if player chooses go to activities history
//            Console.WriteLine("Press H to see activities history or any other key to go back!");
//            char pScreenChoice = Console.ReadKey().KeyChar;
//            if (pScreenChoice == 'H' || pScreenChoice == 'h')
//            {
//                new ActivitiesHistoryScreen(AAnimal).Show();
//            }
//        }
//    }
//}
