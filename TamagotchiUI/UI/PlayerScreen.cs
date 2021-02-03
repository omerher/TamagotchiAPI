//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Linq;
//using TamagotchiUI.Models;



//namespace TamagotchiUI.UI
//{
//    class PlayerScreen : Screen
//    {
//        public PlayerScreen() : base("Show Player")
//        {

//        }

//        public override void Show()
//        {
//            base.Show();
//            ObjectView showPlayer = new ObjectView("", UIMain.CurrentPlayer);
//            showPlayer.Show();
//            Console.WriteLine("Press A to see Player animals or other key to go back!");
//            char c = Console.ReadKey().KeyChar;
//            if (c == 'a' || c == 'A')
//            {
//                Console.WriteLine();
//                //Create list to be displayed on screen
//                //Format the desired fields to be shown! (screen is not wide enough to show all)
//                List<Object> animals = (from animalList in UIMain.CurrentPlayer.Animals
//                                        select new
//                                        {
//                                            ID = animalList.AnimalId,
//                                            Name = animalList.AnimalName,
//                                            BirthDate = animalList.CreationDate.Value.ToShortDateString(),
//                                            Weight = $"{animalList.Aweight:F2}"
//                                        }).ToList<Object>();
//                ObjectsList list = new ObjectsList("Animals", animals);
//                list.Show();
//                Console.WriteLine("\nPress I to see more info about the animal or any other key to go back!");
//                char c2 = Console.ReadKey().KeyChar;
//                if (c2 == 'I' || c2 == 'i')
//                {
//                    if (animals.Count() == 1)
//                    {
//                        Animal a = UIMain.CurrentPlayer.ActiveAnimal;
//                        if (TamagotchiContext.CheckIfDead(a))
//                            new PastAnimalScreen(a.AnimalId);
//                        else
//                            new ActiveAnimalScreen().Show();
//                    }
//                    else
//                    {
//                        Console.WriteLine("\nEnter the ID of the animal you want to see more info:");
//                        int aID = int.Parse(Console.ReadLine());
//                        Animal a = UIMain.db.GetAnimalByID(aID);
//                        if (a == null)
//                        {
//                            Console.WriteLine("\nInvalid animal ID! Press any key to go back to the main menu");
//                            Console.ReadKey();
//                            new MainMenu().Show();
//                        }
//                        else
//                        {
//                            if (TamagotchiContext.CheckIfDead(a))
//                                new PastAnimalScreen(a.AnimalId).Show();
//                            else
//                                new ActiveAnimalScreen().Show();
//                        }
//                    }

//                }

//                else
//                    new MainMenu().Show();
//            }
//        }
//    }
//}