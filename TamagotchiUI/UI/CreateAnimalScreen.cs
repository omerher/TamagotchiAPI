//using System;
//using System.Collections.Generic;
//using System.Text;
//using TamagotchiUI.Models;

//namespace TamagotchiUI.UI
//{
//    class CreateAnimalScreen : Screen
//    {
//        public CreateAnimalScreen() : base("Create Animal")
//        {

//        }

//        public override void Show()
//        {
//            base.Show();
            
//            Console.WriteLine("Enter new animal name: ");
//            string name = Console.ReadLine();
//            while (name == "")
//            {
//                Console.WriteLine("Can't have a blank name! Enter a different name:");
//                name = Console.ReadLine();
//            }

//            Animal a = UIMain.db.CreateAnimal(name, UIMain.CurrentPlayer.PlayerId);
//            if (a == null)
//                Console.WriteLine("Failed!");

//            MainMenu mm = new MainMenu();
//            mm.Show();
//        }
//    }
//}
