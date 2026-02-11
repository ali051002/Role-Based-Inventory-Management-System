using AutoMapper;
using IMS.Application.Interfaces.Services;
using IMS.Domain.Entities;
using IMS.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class AuthService(UserManager<User> _userManager, RoleManager<AspNetRoles> _roleManager, IMapper _mapper, SignInManager<User> _signInManager, IConfiguration _config) : IAuthService
    {
        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            try
            {
                //var user = await _userManager.FindByEmailAsync(request.Email);
                var user = await _userManager.FindByNameAsync(request.Username);

                if (user != null)
                {
                    var valid = await _userManager.CheckPasswordAsync(user, request.Password);
                    if (valid)
                    {
                        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
                        var accessTokenString = BuildAccessToken(user, role);
                        var refreshTokenString = BuildRefreshToken();
                        user.RefreshToken = refreshTokenString;
                        var updateResult = await _userManager.UpdateAsync(user);
                        return new LoginResponseDto
                        {
                            AccessToken = accessTokenString,
                            RefreshToken = refreshTokenString,
                            UserName = user.UserName,
                            Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty
                        };
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("Invalid password.");
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("User not found.");
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task Register(SignUpRequestDto request)
        {
            try
            {
                var user = new User
                {
                    Email = request.Email,
                    UserName = request.Username
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                await _userManager.AddToRoleAsync(user, request.Role);
                if (result.Succeeded)
                {
                    return;
                }
                else
                {
                    throw new Exception($"User registration failed: {result}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task InsertRole(string role)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(role))
                {
                    throw new Exception("Role cannot be empty.");
                }
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _roleManager.CreateAsync(new AspNetRoles { Name = role });
                    if (result.Succeeded)
                    {
                        return;
                    }
                    else
                    {
                        throw new Exception($"Role creation failed: {result}");
                    }
                }
                else
                {
                    throw new Exception("Role already exists.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private string BuildAccessToken(User user, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                notBefore: null,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string BuildRefreshToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: new[] { new Claim("type", "refresh") },
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool ValidateRefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false, // set false
                    ValidateLifetime = false, // set false
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!))
                };

                tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims.Any(c => c.Type == "type" && c.Value == "refresh");
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<TokenResponseDto> RefreshToken(string refreshToken)
        {
            try
            {
                var user = _userManager.Users
                            .FirstOrDefault(u => u.RefreshToken == refreshToken);
                var isValid = ValidateRefreshToken(refreshToken);

                if (user == null || !isValid)
                    return null;

                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
                var newAccessToken = BuildAccessToken(user, role);
                var refreshTokenString = BuildRefreshToken();
                user.RefreshToken = refreshTokenString;
                await _userManager.UpdateAsync(user);

                return new TokenResponseDto
                {
                    Token = newAccessToken,
                    RefreshToken = refreshTokenString
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async void RemoveRefreshToken(string refreshToken)
        {
            try
            {
                var user = _userManager.Users
                            .FirstOrDefault(u => u.RefreshToken == refreshToken);

                if (user == null)
                    return;

                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);

                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
