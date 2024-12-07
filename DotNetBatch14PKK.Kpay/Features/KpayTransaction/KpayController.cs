using DotNetBatch14PKK.Kpay.Features.User;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PKK.Kpay.Features.KpayTransaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class KpayController : ControllerBase
    {
        private readonly IDapperTranServices _tranServices;
        private readonly IDapperUserService _UserServices;
        public KpayController()
        {
            //_tranServices = new TranServices();
            _UserServices = new UserServices();
            //_tranServices = new DapperTranServices();
            //_UserServices = new DapperUserService();
            _tranServices = new EfcoreTranServices();
            //_UserServices = new EfcoreUserService();

        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _UserServices.GetUsers();
            return Ok(users);
        }
        [HttpGet("MobileNo")]
        public IActionResult GetUser(string mobile)
        {
            var user = _UserServices.GetUser(mobile);
            if (user == null) return NotFound("No data found.");
            return Ok(user);
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel requestModel)
        {
            var model = _UserServices.CreateUser(requestModel);
            if (!model.IsSuccessful) return BadRequest(model);
            return Ok(model);
        }

        [HttpPatch("Deposit")]
        public IActionResult Deposit(string mobile, int amount, string password)
        {
            var model = _UserServices.Deposit(mobile,amount,password);
            if (!model.IsSuccessful) return BadRequest(model);
            return Ok(model);
        }

        [HttpPatch("withdraw")]
        public IActionResult withdraw(string mobile, int amount, string password)
        {
            var model = _UserServices.Withdraw(mobile, amount, password);
            if (!model.IsSuccessful) return BadRequest(model);
            return Ok(model);
        }

        [HttpPatch("Transaction")]
        public IActionResult transaction(string frommobile, string tomobile, int amount,DateTime date, string note, string password)
        {
            var model = _tranServices.Transaction(frommobile, tomobile, amount,date, note, password);
            if (!model.IsSuccessful) return BadRequest(model);
            return Ok(model);
        }

        [HttpGet("Transaction History")]
        public IActionResult GetTrans(string mobile)
        {
            var trans = _tranServices.GetTransactionHistory(mobile);
            return Ok(trans);
        }
    }
}
