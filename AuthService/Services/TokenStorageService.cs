using AuthService.Models;
using AuthService.Models.TableStorage;
using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.AzureStorage;

namespace AuthService.Services
{
    public class TokenStorageService : ITokenService
    {
        private readonly ITableStorageContext _context;
        private const string TableName = "Tokens";
        private readonly IMapper _mapper;

        public TokenStorageService(ITableStorageContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RefreshToken> CreateTokenAsync(RefreshToken token)
        {
            var tokenEntity = await _context.AddAsync(TableName, _mapper.Map<RefreshTokenEntity>(token));
            return _mapper.Map<RefreshToken>(tokenEntity);

        } 

        public async Task<RefreshToken> GetTokenAsync(string token, string login)
        {
            var result = await _context.GetAsync<RefreshTokenEntity>(TableName, token, login);
            return _mapper.Map<RefreshToken>(result);
        }
    }
}
