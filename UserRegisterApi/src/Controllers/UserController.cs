using Microsoft.AspNetCore.Mvc;
using UserRegisterApi.Models;
using UserRegisterApi.Services;

namespace UserRegisterApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await _service.CreateAsync(user);
            return CreatedAtAction(nameof(GetByEmail), new { email = user.Email }, user);
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            var user = await _service.GetByEmailAsync(email);
            if (user == null)
            {
                return Problem(
                    statusCode: 404,
                    title: "Not Found",
                    type: "https://tools.ietf.org/html/rfc9110#section-15.5.5"
                );
            }
            return Ok(user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User updated)
        {
            var user = await _service.GetByEmailAsync(updated.Email);
            if (user == null || user.Id != id)
            {
                return Problem(
                    statusCode: 404,
                    title: "Not Found",
                    type: "https://tools.ietf.org/html/rfc9110#section-15.5.5"
                );
            }
            if (updated == null)
            {
                return Problem(
                    statusCode: 400,
                    title: "One or more validation errors occurred.",
                    type: "https://tools.ietf.org/html/rfc9110#section-15.5.1"
                );
            }
            updated.Id = id;
            await _service.UpdateAsync(id, updated);
            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _service.GetAllAsync();
            if (!user.Any(u => u.Id == id))
            {
                return Problem(
                    statusCode: 404,
                    title: "Not Found",
                    type: "https://tools.ietf.org/html/rfc9110#section-15.5.5"
                );
            }
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // PATCH: api/User/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] Dictionary<string, object> updates)
        {
            var users = await _service.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Problem(
                    statusCode: 404,
                    title: "Not Found",
                    type: "https://tools.ietf.org/html/rfc9110#section-15.5.5"
                );
            }
            if (updates == null || updates.Count == 0)
            {
                return Problem(
                    statusCode: 400,
                    title: "One or more validation errors occurred.",
                    type: "https://tools.ietf.org/html/rfc9110#section-15.5.1"
                );
            }
            // Atualiza apenas os campos enviados usando reflexão
            var userType = typeof(User);
            foreach (var entry in updates)
            {
                var property = userType.GetProperty(entry.Key, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (property != null && property.CanWrite)
                {
                    try
                    {
                        var convertedValue = Convert.ChangeType(entry.Value, property.PropertyType);
                        property.SetValue(user, convertedValue);
                    }
                    catch
                    {
                        // Ignora conversão inválida
                    }
                }
            }
            await _service.UpdateAsync(id, user);
            return NoContent();
        }
    }
}