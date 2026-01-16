using IMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task Register(SignUpRequestDto request);
        Task InsertRole(string role);
    }
}
