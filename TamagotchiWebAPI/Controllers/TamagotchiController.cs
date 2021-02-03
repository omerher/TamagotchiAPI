using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TamagotchiUI.Models;

namespace TamagotchiWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class TamagotchiController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        TamagotchiContext context;
        public TamagotchiController(TamagotchiContext context)
        {
            this.context = context;
        }
        #endregion
        [Route("login")]
        [HttpGet]
        public PlayerDTO Login([FromQuery] string email, [FromQuery] string pass)
        {
            Player p = context.Login(email, pass);

            //Check user name and password
            if (p != null)
            {
                PlayerDTO pDTO = new PlayerDTO(p);

                HttpContext.Session.SetObject("player", pDTO);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return pDTO;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("getAnimals")]
        [HttpGet]
        public List<AnimalDTO> GetAnimals()
        {
            PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
            //Check if user logged in!
            if (pDto != null)
            {
                Player p = context.Players.Where(pp => pp.PlayerId == pDto.PlayerId).FirstOrDefault();
                List<AnimalDTO> list = new List<AnimalDTO>();
                if (p != null)
                {
                    foreach (Animal pa in p.Animals)
                    {
                        list.Add(new AnimalDTO(pa));
                    }
                }
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return list;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("createAnimal")]
        [HttpGet]
        public AnimalDTO CreateAnimal([FromQuery] string name)
        {
            PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
            //Check if user logged in!
            if (pDto != null)
            {
                Animal a = context.CreateAnimal(name, pDto.PlayerId);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                if (a != null)
                {
                    return new AnimalDTO(a);
                }
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("getPlayerAnimals")]
        [HttpGet]
        public List<AnimalDTO> GetPlayerAnimals()
        {
            PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
            //Check if user logged in!
            if (pDto != null)
            {
                List<Animal> list = context.GetPlayerAnimals(pDto.PlayerId);

                List<AnimalDTO> newList = new List<AnimalDTO>();
                foreach (Animal a in list)
                {
                    newList.Add(new AnimalDTO(a));
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return newList;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("register")]
        [HttpGet]
        public PlayerDTO Register([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string email, [FromQuery] DateTime dt, [FromQuery] string username, [FromQuery] string password)
        {
            PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
            //Check if user logged in!
            if (pDto == null)
            {
                Player p = context.Register(firstName,lastName,email,dt,username,password);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                if (p != null)
                    return new PlayerDTO(p);
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        //[Route("pastAnimal")]
        //[HttpGet]
        //public AnimalDTO PastAnimal(int animalId)
        //{
        //    PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
        //    //Check if user logged in!
        //    if (pDto != null)
        //    {
        //        Animal a = context.GetAnimalByID(animalId);

        //        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

        //        if (a != null)
        //            return new AnimalDTO(a);
        //        else
        //            return null;
        //    }
        //    else
        //    {
        //        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
        //        return null;
        //    }
        //}

        [Route("activeAnimal")]
        [HttpGet]
        public AnimalDTO ActiveAnimal()
        {
            PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
            //Check if user logged in!
            if (pDto != null)
            {
                Player p =context.GetPlayerByID(pDto.PlayerId);

                Animal a = context.GetAnimalByID(p.ActiveAnimalId);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                if (a != null)
                    return new AnimalDTO(a);
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("ActivitiesHistory")]
        [HttpGet]
        public List<ActivitiesHistoryDTO> ActivitiesHistory([FromQuery] int id)
        {
            PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
            //Check if user logged in!
            if (pDto != null)
            {
                List<ActivitiesHistory> o = context.GetActivitiesHistoryByID(id);
                List<ActivitiesHistoryDTO> lst = new List<ActivitiesHistoryDTO>();

                foreach (ActivitiesHistory ah in o)
                    lst.Add(new ActivitiesHistoryDTO(ah));


                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                if (lst != null)
                    return lst;
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("checkIfDead")]
        [HttpGet]
        public bool CheckIfDead([FromQuery] int id)
        {
            PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
            //Check if user logged in!
            if (pDto != null)
            {
                Animal aa = context.Animals.Where(x => x.AnimalId == id).FirstOrDefault();

                if (aa != null)
                {
                    bool b = TamagotchiContext.CheckIfDead(aa);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return b;
                }

                else
                    return true;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return true;
            }
        }

        [Route("getAnimalByID")]
        [HttpGet]
        public AnimalDTO GetAnimalByID([FromQuery] int id)
        {
            PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
            //Check if user logged in!
            if (pDto != null)
            {
                Animal a = context.GetAnimalByID(id);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                if (a != null)
                    return new AnimalDTO(a);
                else
                    return null;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
    }
}
