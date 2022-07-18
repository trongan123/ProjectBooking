using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IService;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService account)
        {
            this.accountService = account;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccount() => accountService.GetAccounts();

        [HttpGet("id")]
        public ActionResult<Account> GetAccountById(string id) => accountService.GetAccountById(id);

        [HttpGet("{email}/{pass}")]
        public ActionResult<Account> Login(string email, string pass) => accountService.Login(email,pass);

        [HttpGet("phone")]
        public ActionResult<IEnumerable<Account>> GetAccountByPhone(String phone) => accountService.GetAccountsByPhone(phone);

        [HttpPost]
        public IActionResult PortAccount(Account a)
        {
            accountService.AddAccount(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteAccount(string id)
        {
            var a = accountService.GetAccountById(id);
            if (a == null)
            {
                return NotFound();
            }
            accountService.DeleteAccount(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateAccount(string id, Account a)
        {
            var aTmp = accountService.GetAccountById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            accountService.UpdateAccount(a);
            return NoContent();
        }
    }
}
