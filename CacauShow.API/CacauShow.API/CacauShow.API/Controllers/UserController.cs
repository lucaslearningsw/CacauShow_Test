using CacauShow.API.Domain.Interfaces;
using CacauShow.API.Domain.Models;
using CacauShow.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CacauShow.API.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : Controller
    {
        private  readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginUser userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            var user = await _userService.Login(userObj.Email, Cryptograph.Cryptograph.GetSHA512FromPassword(userObj.Password));

           if (user ==  null)
           {
               return (BadRequest(new { Message = "Email ou senha incorretos!" }));
            }

            return(Ok(new {Message = "Login Success!"}));
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("lista-usuarios")]
        public async Task<IActionResult> GettAllUser()
        {
            try
            {
               var users = await _userService.GetAll();

               return Ok(users);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }


        [HttpGet]
        [Route("detalhe-usuario/{id:guid}")]
        public async Task<IActionResult> DetailsUser([FromRoute] Guid id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        [Route("registrar-usuario")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]User userRequest)
        {

            //TODO: fluent validion
            try
            {
                userRequest.Password = Cryptograph.Cryptograph.GetSHA512FromPassword(userRequest.Password);
                await _userService.Create(userRequest);

                return Ok(userRequest);
            }
            catch (Exception e)
            {
                return (BadRequest(new { Message = e.Message }));
            }
               

        }

        [HttpPost]
        [Route("atualizar-usuario/{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] User userUpdateObj)
        {
            //TODO: fluent validion
            var user = await _userService.GetById(id);

            if (user == null)
            {
              return  NotFound();
            }
            try
            {
                await _userService.Update(userUpdateObj, id);

            }
            catch (Exception e)
            {
                return (BadRequest(new { Message = e.Message }));
            }

            return Ok(userUpdateObj);
        }

        [HttpDelete]
        [Route("excluir-usuario/{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.Delete(id);

            return Ok();
        }
    }
}
