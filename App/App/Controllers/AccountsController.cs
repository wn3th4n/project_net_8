using IdentityAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [Route("api/[Account]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class AccountsController 
    {

    }
}
