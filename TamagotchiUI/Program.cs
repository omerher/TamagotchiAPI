using System;
using TamagotchiUI.Models;
using TamagotchiUI.UI;

namespace TamagotchiUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Scaffold-DbContext "Server = localhost\SQLEXPRESS; Database=TamagotchiDB;Trusted_Connection = true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context TamagotchiContext –DataAnnotations -force

            UIMain ui = new UIMain(new LoginRegisterScreen());
            ui.ApplicationStart();
        }
    }
}