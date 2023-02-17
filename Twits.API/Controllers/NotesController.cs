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
        private readonly INotesRepository _notesRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<LocalUser> _userManager;
        private readonly IMapper _mapper;
        public NotesController(INotesRepository repository, ApiResponse response, UserManager<LocalUser> userManager,
            IMapper mapper, ICategoryRepository categoryRepository)
        {
            _notesRepository = repository;
            _response = response;
            _userManager = userManager;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        
        [HttpGet("get-created-notes")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetAll([FromQuery(Name = "filterCategory")] string? category,
            [FromQuery] string? search, int pageSize = 2, int pageNumber = 1)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                IEnumerable<Note> notes = await _notesRepository.GetAllAsync(n => n.CreatorId== currentUserId,
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
        [HttpPost("create-note")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> CreateNote([FromBody] NoteCreateVM noteVm)
        {
            try
            {
                if(noteVm == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                ClaimsPrincipal currentUser = this.User;
                var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var category = await _categoryRepository.GetAsync(x => x.Title.ToLower() == noteVm.Category.ToLower(), tracked: false);
                Note model = new Note
                {
                    Title = noteVm.Title,
                    Content = noteVm.Content,
                    CreatorId = currentUserId,
                    LocalUser = await _userManager.FindByIdAsync(currentUserId),
                    Category = noteVm.Category
                };
                await _notesRepository.CreateAsync(model);
                _response.Result = noteVm;
                _response.StatusCode = HttpStatusCode.Created;
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
