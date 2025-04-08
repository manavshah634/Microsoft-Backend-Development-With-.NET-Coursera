using System;
using Microsoft.AspNetCore.Mvc;
using ApiModels;
using ApiRepositories;

namespace ApiControllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : ControllerBase
    {
        [HttpDelete("{id}")]
        public IActionResult RemoveProfile(int id)
        {
            if (ProfileDataStore.GetById(id) == null) return NotFound(new { error = "Profile not found." });
            ProfileDataStore.Delete(id);
            return NoContent();
        }
        
        [HttpGet]
        public IActionResult GetAllProfiles() => Ok(ProfileDataStore.GetAll());
        
        [HttpPost]
        public IActionResult AddProfile([FromBody] UserProfile profile)
        {
            if (string.IsNullOrEmpty(profile.Name) || string.IsNullOrEmpty(profile.Email))
                return BadRequest(new { error = "Invalid profile data." });
            
            ProfileDataStore.Add(profile);
            return Created($"api/profiles/{profile.Id}", profile);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetProfileById(int id)
        {
            var profile = ProfileDataStore.GetById(id);
            if (profile == null) return NotFound(new { error = "Profile not found." });
            return Ok(profile);
        }
        
        [HttpPut("{id}")]
        public IActionResult ModifyProfile(int id, [FromBody] UserProfile profile)
        {
            var existingProfile = ProfileDataStore.GetById(id);
            if (existingProfile == null) return NotFound(new { error = "Profile not found." });

            profile.Id = id;
            ProfileDataStore.Update(profile);
            return NoContent();
        }
    }
}