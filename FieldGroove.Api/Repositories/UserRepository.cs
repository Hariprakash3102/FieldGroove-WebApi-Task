using FieldGroove.Api.Data;
using FieldGroove.Api.Interfaces;
using FieldGroove.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldGroove.Api.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        public async Task Create(RegisterModel entity)
        {
            await dbContext.UserData.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsRegistered(RegisterModel entity)
        {
            return await dbContext.UserData.AsQueryable().AnyAsync(x => x.Email == entity.Email!);
        }

        public async Task<bool> IsValid(LoginModel entity)
        {
            return await dbContext.UserData.AsQueryable().AnyAsync(x => x.Email == entity.Email!);
        }
    }
}
