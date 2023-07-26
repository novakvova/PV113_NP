using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using WebApiServer.Data;
using WebApiServer.Data.Entities.Identity;
using WebApiServer.Helpers;
using WebApiServer.Models;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppEFContext _context;

        public AccountController(UserManager<AppUser> userManager, IMapper mapper, AppEFContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            string randomFilename = Path.GetRandomFileName() + ".jpeg";
            try
            {
                var img = model.Photo.FromBase64ToImage();
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", randomFilename);
                img.Save(dir, ImageFormat.Jpeg);
            }
            catch
            {
                return BadRequest(new { errors = "Помилка збереження фото!" });
            }

            var user = _mapper.Map<AppUser>(model);
            user.Photo = randomFilename;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });


            return Ok(_mapper.Map<UserItemViewModel>(user));
        }

        [HttpGet]
        //[Authorize]
        [Route("users")]
        public async Task<IActionResult> Users()
        {
            Thread.Sleep(1000);
            var list = _context.Users.Select(x => _mapper.Map<UserItemViewModel>(x)).ToList();

            return Ok(list);
        }
    }
}
