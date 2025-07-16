using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTO
{
    public record AuthDataDTO(
        string email,
        string nickname);
    public record RegisterAuthDataDTO(
        string email,
        string password,
        string nickName);

    public record LoginAuthDataDTO(
        string email,
        string password);
}
