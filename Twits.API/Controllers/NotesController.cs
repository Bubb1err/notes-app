using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Claims;
using Twits.API.Services.IRepository;
using Twits.Data.Models;
using Twits.Data.Models.ViewModels;

namespace Twits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        protected ApiResponse _response;
        private readonly INotesRepository _repository;
        private readonly UserManager<LocalUser> _userManager;
        private readonly IMapper _mapper;
        public NotesController(INotesRepository repository, ApiResponse response, UserManager<LocalUser> userManager,
            IMapper mapper)
        {
            _repository = repository;
            _response = response;
            _userManager = userManager;
            _mapper = mapper;
        }
        
        [HttpGet("get-created-notes")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> Get([FromQuery(Name = "filterCategory")] string? category,
            [FromQuery] string? search, int pageSize = 2, int pageNumber = 1)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                IEnumerable<Note> notes = await _repository.GetAllAsync(n => n.CreatorId== currentUserId,
                    pageSize:pageSize, pageNumber:pageNumber);
                if(!string.IsNullOrEmpty(category))
                {
                    notes = notes.Where(n => n.Category.ToString().ToLower() == category.ToLower());
                }
                if(!string.IsNullOrEmpty(search)) 
                {
                    notes = notes.Where(n => n.Title.ToLower().Contains(search.ToLower()));
                }
                _response.Result = _mapper.Map<List<NotesVM>>(notes);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess= true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
