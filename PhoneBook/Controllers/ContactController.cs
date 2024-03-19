using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.Helpers;
using PhoneBook.Core;
using PhoneBook.Services;

namespace PhoneBook.Controllers
{
    [ApiController]
    [Authorize(Roles = "Contact")]
    [Route("api/v1/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("contact/{id}")]
        [Authorize("OnlyAllowAnonymous")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            try
            {
                var contact = await _contactService.Get(id);

                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching contact: {ex.Message}");
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] AddContactModel model)
        {
            try
            {
                if (!User.TryGetUserData(out var userData))
                {
                    return Unauthorized();
                }
                var userClientId = await _contactService.AddContact(model, userData.Id);
                if (userClientId != null)
                {
                    return Created($"contacts/{userClientId}", null);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding a new contact: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromQuery] Guid id, [FromBody] AddContactModel model)
        {
            try
            {
                if (!User.TryGetUserData(out var userData))
                {
                    return Unauthorized();
                }

                bool result = await _contactService.UpdateContact(id, model);
                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating a contact: {ex.Message}");
            }
        }

        [HttpPost("favorite/{id}")]
        public async Task<IActionResult> MarkAsFavorite([FromQuery] Guid id)
        {
            try
            {
                if (!User.TryGetUserData(out var userData))
                {
                    return Unauthorized();
                }

                bool result = await _contactService.MarkAsFavorite(id, userData?.Id, true);
                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while marking a contact as favorite: {ex.Message}");
            }
        }


        [HttpPost("unfavorite/{id}")]
        public async Task<IActionResult> UnMarkAsFavorite([FromQuery] Guid id)
        {
            try
            {
                if (!User.TryGetUserData(out var userData))
                {
                    return Unauthorized();
                }

                bool result = await _contactService.MarkAsFavorite(id, userData?.Id, false);
                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating a contact: {ex.Message}");
            }
        }
    }
}