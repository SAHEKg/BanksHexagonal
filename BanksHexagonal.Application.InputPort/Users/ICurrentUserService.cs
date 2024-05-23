using BanksHexagonal.Application.Models;

namespace BanksHexagonal.Application.InputPort;

public interface ICurrentUserService
{
    User? CurrentUser { get; }
}
