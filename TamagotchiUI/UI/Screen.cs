using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiUI.Models;


namespace TamagotchiUI.UI
{
    class Screen
    {
        public string Title { get; set; }
        public Screen(string title)
        {
            Title = title;
        }
        public virtual void Show()
        {
            Console.Clear();
            Console.WriteLine($"{Title}".PadLeft(Console.WindowWidth / 2));
        }
    }
}
