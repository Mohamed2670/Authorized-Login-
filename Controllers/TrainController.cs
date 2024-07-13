using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MwTesting.Data;
using MwTesting.Dtos;
using MwTesting.Model;

namespace MwTesting.Controllers
{

    [Route("api/train")]
    [ApiController]
    public class TrainController(IUserAc userAc, IMapper mapper) : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> Get()
        {
            var item = userAc.GetAllUsers();
            // Get this values form that 
            var final = mapper.Map<IEnumerable<UserReadDto>>(item);
            return Ok(final);
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<UserReadDto> GetById(int id)
        {
            var item = userAc.GetUserById(id);
            if (item == null)
            {
                return NotFound();
            }
            var final = mapper.Map<UserReadDto>(item);
            return Ok(final);
        }
        [HttpPost]
        public ActionResult<User> CreateUser(UserCreateDto user)
        {
            var temp = mapper.Map<User>(user);
            userAc.CreateUser(temp);
            userAc.SaveChanges();
            return Ok(temp);
        }
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, UserUpdateDto user)
        {
            var item = userAc.GetUserById(id);
            if (item == null)
            {
                return NotFound();
            }
            mapper.Map(user, item);
            userAc.SaveChanges();
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)

        {
            var item = userAc.GetUserById(id);
            if (item == null)
            {
                return NotFound();
            }
            userAc.DeleteUser(item);
            userAc.SaveChanges();
            return Ok();
        }


        [HttpPut("updateRole/{id}")]
        [Authorize(Policy = "SuperUser")]
        public ActionResult<User> UpdateRole(int id, string role)
        {
            var item = userAc.GetUserById(id);
            role = role != "Admin" && role != "User" ? "User" : role;
            if (item == null)
                return NotFound();
            item.Role = role;
            userAc.SaveChanges();
            return Ok(item);
        }

    }
}