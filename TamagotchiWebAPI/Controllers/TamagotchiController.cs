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
                Animal a = context.CreateAnimal(name);

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

        [Route("ActivitiesHistory")]
        [HttpGet]
        public List<Object> ActivitiesHistory([FromQuery] int id)
        {
            PlayerDTO pDto = HttpContext.Session.GetObject<PlayerDTO>("player");
            //Check if user logged in!
            if (pDto != null)
            {
                List<Object> o = context.GetActivitiesHistoryByID(id);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                if (o != null)
                    return o;
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
