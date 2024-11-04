using FieldGroove.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FieldGroove.Api.Interfaces
{
    public interface IUserRepository
    {
        Task Create(RegisterModel entity);
        Task<bool> IsValid(LoginModel enitity);
        Task<bool> IsRegistered(RegisterModel enitity);

    }
}
