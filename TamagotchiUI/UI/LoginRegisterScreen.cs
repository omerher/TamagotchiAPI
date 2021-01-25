using System;
using System.Collections.Generic;
using System.Text;
using TamagotchiUI.Models;
using System.Net.Mail;

namespace TamagotchiUI.UI
{
    class LoginRegisterScreen : Screen
    {
        public LoginRegisterScreen() : base("Login/Register")
        {

        }

        public override void Show()
        {
            // Clear screen and set title (implemented by Screen Show)
            base.Show();

            //Check if we should logout first
            if (UIMain.CurrentPlayer != null)
            {
                Console.WriteLine($"Currently, {0} is logged in. Press Y to log out or other key to go back to menu!");
                char c = Console.ReadKey().KeyChar;
                if (c == 'Y' || c == 'y')
                {
                    //Save all changes to DB before logging out
                    UIMain.db.SaveChanges();
                    UIMain.CurrentPlayer = null;
                }
            }

            Console.WriteLine("Do you want to login or register? Press 1 to login or 2 to register");
            Char playerLoginOrRegister = Console.ReadKey().KeyChar;

            if (playerLoginOrRegister == '1')
            {
                //if user is still logged in, we should go out!= back to menu
                while (UIMain.CurrentPlayer == null)
                {
                    //Clear screen again
                    this.Title = "Login";
                    base.Show();

                    Console.WriteLine($"Please enter your email: ");
                    string email = Console.ReadLine();
                    Console.WriteLine($"Please enter your password: ");
                    string password = Console.ReadLine();

                    UIMain.CurrentPlayer = UIMain.db.Login(email, password);

                    if (UIMain.CurrentPlayer == null)
                    {
                        Console.WriteLine("Login failed! Press R to register or any other key to try again!");
                        char isRegister = Console.ReadKey().KeyChar;
                        if (isRegister == 'R' || isRegister == 'r')
                        {
                            playerLoginOrRegister = '2';
                            break;

                        }
                    }
                }
            }

            if (playerLoginOrRegister == '2')
            {
                //if user is still logged in, we should go out!= back to menu
                while (UIMain.CurrentPlayer == null)
                {
                    //Clear screen again
                    this.Title = "Register";
                    base.Show();

                    Console.WriteLine($"Please enter your first name: ");
                    string firstName = Console.ReadLine();
                    Console.WriteLine($"Please enter your last name: ");
                    string lastName = Console.ReadLine();
                    Console.WriteLine($"Please enter your email: ");
                    string email = Console.ReadLine();
                    while (!UIMain.db.IsValidEmail(email))
                    {
                        Console.WriteLine("Uh oh! The email you entered in invalid! Please enter a valid email address:");
                        email = Console.ReadLine();
                    }

                    Console.WriteLine($"Please enter your birth date: ");
                    string birthDate = Console.ReadLine();
                    DateTime dt = DateTime.Parse(birthDate);
                    Console.WriteLine($"Please enter your username: ");
                    string username = Console.ReadLine();
                    Console.WriteLine($"Please enter your password: ");
                    string password = Console.ReadLine();

                    UIMain.CurrentPlayer = UIMain.db.Register(firstName, lastName, email, dt, username, password);

                    //Checking if the email is valid, If not returns an error messege;
                    if (UIMain.CurrentPlayer == null)
                    {
                        Console.WriteLine("Register failed! Press any key to try again!");
                        Console.ReadKey();
                    }
                }
            }
            //Show main menu once user is logged in
            MainMenu menu = new MainMenu();
            menu.Show();
        }
    }
}