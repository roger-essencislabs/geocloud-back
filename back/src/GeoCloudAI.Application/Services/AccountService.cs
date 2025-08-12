using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    /// <summary>
    /// This class provides account-related services such as adding, updating, deleting, and retrieving accounts.
    /// </summary>
    /// <seealso cref="GeoCloudAI.Application.Contracts.IAccountService" />
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="accountRepository">The account repository.</param>
        /// <param name="mapper">The mapper.</param>
        public AccountService(IAccountRepository accountRepository,
                           IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Adds the specified account dto.
        /// </summary>
        /// <param name="accountDto">The account dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<AccountDto> Add(AccountDto accountDto) 
        {
            try
            {
                //Map Dto > Class
                var addAccount = _mapper.Map<Account>(accountDto); 
                //Add Account
                var resultCode = await _accountRepository.Add(addAccount); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Account
                var result = await _accountRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<AccountDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        /// <summary>
        /// Updates the specified account dto.
        /// </summary>
        /// <param name="accountDto">The account dto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<AccountDto> Update(AccountDto accountDto) 
        {
            try
            {
                //Check if exist Account
                var existAccount = await _accountRepository.GetById(accountDto.Id);
                if (existAccount == null) return null;
                //Map Dto > Class
                var updateAccount = _mapper.Map<Account>(accountDto);
                //Update Account
                var resultCode = await _accountRepository.Update(updateAccount); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Account
                var result = await _accountRepository.GetById(updateAccount.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<AccountDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        /// <summary>
        /// Deletes the specified account identifier.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<int> Delete(int accountId) 
        {
            try
            {
                return await _accountRepository.Delete(accountId);
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
        public async Task<PageList<AccountDto>> Get(PageParams pageParams) 
        {
            try
            {
                var accounts = await _accountRepository.Get(pageParams);
                if (accounts == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<AccountDto>>(accounts);
                result.TotalCount  = accounts.TotalCount;
                result.CurrentPage = accounts.CurrentPage;
                result.PageSize    = accounts.PageSize;
                result.TotalPages  = accounts.TotalPages;

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
        /// <param name="accountId">The account identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<AccountDto> GetById(int accountId) 
        {
            try
            {
                var account = await _accountRepository.GetById(accountId);
                if (account == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<AccountDto>(account);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        /// <summary>
        /// Gets the by unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<int> GetByGuid(string guid) 
        {
            try
            {
                var qtt = await _accountRepository.GetByGuid(guid);
                return qtt;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}