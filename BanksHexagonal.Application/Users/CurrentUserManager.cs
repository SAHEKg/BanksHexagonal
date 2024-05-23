using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Application.Models;

namespace BanksHexagonal.Application;

public class CurrentUserManager : ICurrentUserService
{
    public User? CurrentUser { get; set; }
}