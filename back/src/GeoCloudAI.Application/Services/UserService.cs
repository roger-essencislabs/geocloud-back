using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace GeoCloudAI.Application.Services
{
    /// <summary>
    /// This class provides user-related services such as adding, updating, deleting, and retrieving users.
    /// </summary>
    /// <seealso cref="GeoCloudAI.Application.Contracts.IUserService" />
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        /// <summary>
        /// The key
        /// </summary>
        public readonly  SymmetricSecurityKey _key;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="accountService">The account service.</param>
        /// <param name="profileService">The profile service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="config">The configuration.</param>
        public UserService(IUserRepository userRepository,
                           IAccountService accountService,
                           IProfileService profileService, 
                           IMapper mapper,
                           IConfiguration config)
        {
            _userRepository = userRepository;
            _accountService = accountService;
            _profileService = profileService;
            _mapper = mapper;
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"].ToString()));
        }
        /// <summary>
        /// Adds the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<UserDto> Add(UserDto userDto) 
        {
            try
            {
                //Map Dto > Class
                var addUser = _mapper.Map<User>(userDto); 
                //Add User
                var resultCode = await _userRepository.Add(addUser); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New User
                var result = await _userRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<UserDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        /// <summary>
        /// Registers the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<string> Register(UserDto userDto) 
        {
            try
            {   
                if(userDto.Id == -1 && userDto.ProfileId == -2 && userDto.Profile.AccountId == -3)
                {
                    var resEmail = await this.GetByEmail(userDto.Email!);
                    if (resEmail == null)
                    {
                        //Add Account
                        if (userDto.Profile.Account == null) { return "erro"; } else
                        {
                            //Guid 
                            Guid g = Guid.NewGuid();
                            while (await _accountService.GetByGuid(g.ToString()) > 0) 
                            g = Guid.NewGuid();
                            userDto.Profile.Account.Guid = g.ToString();
                            var resAccount = await _accountService.Add(userDto.Profile.Account);
                            if (resAccount == null) { return "erro"; } else
                            {
                                userDto.Profile.AccountId = resAccount.Id;
                                //Add Profile
                                var resProfile = await _profileService.Add(userDto.Profile);
                                if (resAccount == null) { return "erro"; } else
                                {
                                    userDto.ProfileId = resProfile.Id;
                                    //Add User
                                    var resUser = await this.Add(userDto);
                                    if (resUser != null) 
                                    {
                                        return "Ok";
                                    } 
                                    else 
                                    {
                                        return "Signup error ! ";
                                    }
                                
                                }
                            } 
                        }
                    } else
                    {
                        return "Email already registered !";
                    }
                } else
                {
                    return "Validation ids incorrect !";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        /// <summary>
        /// Updates the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<UserDto> Update(UserDto userDto) 
        {
            try
            {
                //Check if exist User
                var existUser = await _userRepository.GetById(userDto.Id);
                if (existUser == null) return null;
                //Map Dto > Class
                var updateUser = _mapper.Map<User>(userDto);
                //Update User
                var resultCode = await _userRepository.Update(updateUser); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated User
                var result = await _userRepository.GetById(updateUser.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<UserDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<int> Delete(int userId) 
        {
            try
            {
                return await _userRepository.Delete(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        /// <summary>
        /// Gets the specified page parameters.
        /// </summary>
        /// <param name="pageParams">The page parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<PageList<UserDto>> Get(PageParams pageParams) 
        {
            try
            {
                var users = await _userRepository.Get(pageParams);
                if (users == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<UserDto>>(users);
                result.TotalCount  = users.TotalCount;
                result.CurrentPage = users.CurrentPage;
                result.PageSize    = users.PageSize;
                result.TotalPages  = users.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }
        /// <summary>
        /// Gets the by account.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="pageParams">The page parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<PageList<UserDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var users = await _userRepository.GetByAccount(accountId, pageParams);
                if (users == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<UserDto>>(users);
                result.TotalCount  = users.TotalCount;
                result.CurrentPage = users.CurrentPage;
                result.PageSize    = users.PageSize;
                result.TotalPages  = users.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<UserDto> GetById(int userId) 
        {
            try
            {
                var user = await _userRepository.GetById(userId);
                if (user == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<UserDto>(user);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        /// <summary>
        /// Gets the by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<UserDto> GetByEmail(string email) 
        {
            try
            {
                var user = await _userRepository.GetByEmail(email);
                if (user == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<UserDto>(user);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<UserDto> Login(string email, string password) 
        {
            try
            {
                var user = await _userRepository.Login(email, password);
                if (user == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<UserDto>(user);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<string> GenerateToken(UserDto userDto)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userDto.Email.ToString()),
                        new Claim(ClaimTypes.Role, userDto.Profile.Name.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(12),
                    SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
                };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}