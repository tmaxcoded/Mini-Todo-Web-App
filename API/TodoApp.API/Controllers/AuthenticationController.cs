



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace TodoApp.API.Controllers
{
    
    public class AuthenticationController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManagerService _authManager;

        public AuthenticationController(IMapper mapper,
       UserManager<User> userManager, IAuthenticationManagerService authManager)
        {

            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserForRegistration userForRegistration)
        {
            
            var user = _mapper.Map<User>(userForRegistration);
            
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(new GenericResponse<ModelStateDictionary>(ResponseType.Failed,
                   (int)HttpStatusCode.BadRequest, ResponseMessage.CreateUsersErrorMessage, ModelState));
            }

            var _user = await _userManager.FindByEmailAsync(user.Email);
            if (!await _authManager.ValidateUserByEmail(new UserForAuthentication{Email = _user.Email}))
            {
                return Unauthorized(new GenericResponse<UserForRegistration>(ResponseType.Failed,
                                   (int)HttpStatusCode.Unauthorized, ResponseMessage.UnAuthorizedAccess, null));
            }


            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return Ok(new GenericResponse<NewToken>(ResponseType.Success,
                   (int)HttpStatusCode.Created, ResponseMessage.Success, new NewToken { Token = await _authManager.CreateToken(), Email = user.Email }));


        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthentication user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                
                return Unauthorized(new GenericResponse<UserForRegistration>(ResponseType.Failed,
                   (int)HttpStatusCode.Unauthorized, ResponseMessage.UnAuthorizedAccess, null));
            }
            return Ok(new GenericResponse<NewToken>(ResponseType.Success,
                   (int)HttpStatusCode.Created, ResponseMessage.Success, new NewToken{ Token = await _authManager.CreateToken(),Email = user.Email }));
        }


        
        [HttpGet("getcurrentuser")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCurrentUse()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var _user = await _userManager.FindByEmailAsync(email);
            if (!await _authManager.ValidateUserByEmail(new UserForAuthentication{Email = _user.Email}))
            {
                return Unauthorized(new GenericResponse<UserForRegistration>(ResponseType.Failed,
                                   (int)HttpStatusCode.Unauthorized, ResponseMessage.UnAuthorizedAccess, null));
            }
              
            return Ok(new GenericResponse<NewToken>(ResponseType.Success,
                   (int)HttpStatusCode.Created, ResponseMessage.Success, new NewToken { Token = await _authManager.CreateToken(), Email = _user.Email }));

         
        }
    }
 }
