using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using TamagotchiUI.UI;
using System.Collections.Generic;
using System.Net.Mail;

#nullable disable

namespace TamagotchiUI.Models
{
    public partial class TamagotchiContext : DbContext
    {
        //Here write general methods to extract root objects from the database!
        public Player GetPlayerByID(int id)
        {
            Player p = this.Players.Where(p => p.PlayerId == id).FirstOrDefault();
            return p;
        }

        public ActivitiesCategory GetActivityByID(int id)
        {
            ActivitiesCategory ac = this.ActivitiesCategories.Single(item => item.AcitivtyCategoryId == id);
            return ac;
        }

        public Animal GetAnimalByID(int id)
        {
            Animal a = this.Animals.Where(a => a.AnimalId == id).FirstOrDefault();
            return a;
        }
        
        //Log in method. Return a player or null if not succeed!
        public Player Login(string email, string pswd)
        {
            Player p = this.Players.Where(p => p.PlayerEmail == email && p.Password == pswd).FirstOrDefault();
            return p;
        }

        //Register method. Return a player or null if not succeed!
        public Player Register(string firstName, string lastName, string email, DateTime birthDate, string username, string pswd)
        {
            try {
                Player p = new Player()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PlayerEmail = email,
                    BirthDate = birthDate,
                    Username = username,
                    Password = pswd
                };

                this.Add(p);
                this.SaveChanges();

                return p;
            }

            catch {
                return null;
            }
        }

        public Animal CreateAnimal(string name)
        {
            try {
                Animal a = new Animal()
                {
                    AnimalName = name,
                    PlayerId = UIMain.CurrentPlayer.PlayerId
                };
                a.Player = UIMain.CurrentPlayer;

                this.Add(a);
                this.SaveChanges();

                UIMain.CurrentPlayer.ActiveAnimal = a;
                UIMain.CurrentPlayer.ActiveAnimalId = a.AnimalId;
                this.SaveChanges();

                return a;
            }
            catch
            {
                return null;
            }
        }

        //Credentials Email Change method. Return a player or null if not succeed!
        public Player ChangeCredentialsEmail(string email)
        {
            try
            {
                UIMain.CurrentPlayer.PlayerEmail = email;

                UIMain.db.SaveChanges();

                return UIMain.CurrentPlayer;
            }

            catch
            {
                return null;
            }
        }

        //Credentials Username Change method. Return a player or null if not succeed!
        public Player ChangeCredentialsUsername(string username)
        {
            try
            {
                UIMain.CurrentPlayer.Username = username;

                UIMain.db.SaveChanges();

                return UIMain.CurrentPlayer;
            }

            catch
            {
                return null;
            }
        }

        //Credentials Password Change method. Return a player or null if not succeed!
        public Player ChangeCredentialsPassword(string password)
        {
            try
            {
                UIMain.CurrentPlayer.Password = password;

                UIMain.db.SaveChanges();

                return UIMain.CurrentPlayer;
            }

            catch
            {
                return null;
            }
        }

        public static bool CheckIfDead(Animal a)
        {
            return (a.LifeCycleId == 5);
        }

        public List<Animal> SearchAnimalsByName(string name)
        {
            return this.Animals.Where(a => a.AnimalName == name).ToList();
        }

        public void DoActivity(int categoryID, int activityID)
        {
            int improvementRate = this.Activities.Where(a => a.ActivityId == activityID).FirstOrDefault().ImprovementRate; // find the desired activity, and get the improvement rate

            // finds the animal age by dividing the difference of days between now and the creation of animal by 365
            TimeSpan t1 = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int currentDays = (int)t1.TotalDays;
            TimeSpan t2 = UIMain.CurrentPlayer.ActiveAnimal.CreationDate.Value - new DateTime(1970, 1, 1);
            int animalCreationDays = (int)t2.TotalDays;
            int animalAge = (currentDays - animalCreationDays) / 365;

            // get all current stats and decrease 5, but if under if value is under 0, set it to 0
            int aWeight = UIMain.CurrentPlayer.ActiveAnimal.Aweight <= 1 ? 0 : UIMain.CurrentPlayer.ActiveAnimal.Aweight - 1;  // weight is a different scale
            int aHunger = UIMain.CurrentPlayer.ActiveAnimal.Ahunger <= 5 ? 0 : UIMain.CurrentPlayer.ActiveAnimal.Ahunger - 5;
            int aHappiness = UIMain.CurrentPlayer.ActiveAnimal.Ahappiness <= 5 ? 0 : UIMain.CurrentPlayer.ActiveAnimal.Ahappiness - 5;
            int aCleanliness = UIMain.CurrentPlayer.ActiveAnimal.Acleanliness <= 5 ? 0 : UIMain.CurrentPlayer.ActiveAnimal.Acleanliness - 5;
            switch (categoryID)
            {
                case 1:
                    aHunger = (aHunger + improvementRate) > 100 ? 100 : aHunger + improvementRate; // checks if the stat is over 100, if it is set it to 100, otherwise set it normal
                    aWeight += improvementRate / 20;
                    break;
                case 2:
                    aHappiness = (aHappiness + improvementRate) > 100 ? 100 : aHappiness + improvementRate; // checks if the stat is over 100, if it is set it to 100, otherwise set it normal
                    break;
                case 3:
                    aCleanliness = (aCleanliness + improvementRate) > 100 ? 100 : aCleanliness + improvementRate; // checks if the stat is over 100, if it is set it to 100, otherwise set it normal
                    break;
            }

            // create new row in activities history
            ActivitiesHistory ah = new ActivitiesHistory()
            {
                AnimalId = UIMain.CurrentPlayer.ActiveAnimal.AnimalId,
                ActivityId = activityID,
                AnimalAge = animalAge,
                Aweight = aWeight,
                Ahunger = aHunger,
                Ahappiness = aHappiness,
                Acleanliness = aCleanliness,
                AnimalCycleStatusId = UIMain.CurrentPlayer.ActiveAnimal.LifeCycleId,
                AnimalHealthStatusId = UIMain.CurrentPlayer.ActiveAnimal.HealthStatusId
            };

            this.ActivitiesHistories.Add(ah);

            // change active animals stats to updated stats
            UIMain.CurrentPlayer.ActiveAnimal.Aweight = aWeight;
            UIMain.CurrentPlayer.ActiveAnimal.Ahunger = aHunger;
            UIMain.CurrentPlayer.ActiveAnimal.Ahappiness = aHappiness;
            UIMain.CurrentPlayer.ActiveAnimal.Acleanliness = aCleanliness;

            this.SaveChanges();
        }

        public List<Object> GetActivitiesHistoryByID(int id)
        {
            return (from activitiesList in UIMain.db.ActivitiesHistories
                    where activitiesList.AnimalId == id
                    select new
                    {
                        Name = UIMain.db.Activities.Where(a => a.ActivityId == activitiesList.ActivityId).FirstOrDefault().ActivityName,
                        ActivitiesCategory = UIMain.db.Activities.Where(a => a.ActivityCategoryId == activitiesList.Activity.ActivityCategoryId).FirstOrDefault().ActivityCategory,
                        Aweight = UIMain.db.ActivitiesHistories.Where(a => a.Aweight == activitiesList.Aweight).FirstOrDefault().Aweight,
                        Ahunger = UIMain.db.ActivitiesHistories.Where(a => a.Ahunger == activitiesList.Ahunger).FirstOrDefault().Ahunger,
                        Ahappiness = UIMain.db.ActivitiesHistories.Where(a => a.Ahappiness == activitiesList.Ahappiness).FirstOrDefault().Ahappiness,
                        Acleanliness = UIMain.db.ActivitiesHistories.Where(a => a.Acleanliness == activitiesList.Acleanliness).FirstOrDefault().Acleanliness,
                        Date = UIMain.db.ActivitiesHistories.Where(a => a.ActivityDate == activitiesList.ActivityDate).FirstOrDefault().ActivityDate,
                        AnimalCycleStatus = UIMain.db.ActivitiesHistories.Where(a => a.AnimalCycleStatus == activitiesList.AnimalCycleStatus).FirstOrDefault().AnimalCycleStatus,
                        HealthStatus = UIMain.db.ActivitiesHistories.Where(a => a.AnimalHealthStatus == activitiesList.AnimalHealthStatus).FirstOrDefault().AnimalHealthStatus,
                    }).ToList<Object>();
        }

        //Checking if the email is valid, If not returns exception;
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
