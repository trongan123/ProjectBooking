using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookingWebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient client = null;
        private string AccountAPiUrl = "";
        private string RoomAPiUrl = "";
        private string CommentAPiUrl = "";
        private string BillAPiUrl = "";
        public AccountController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AccountAPiUrl = "https://localhost:7159/api/Account";
            RoomAPiUrl = "https://localhost:7159/api/Room";
            CommentAPiUrl = "https://localhost:7159/api/Comment";
            BillAPiUrl = "https://localhost:7159/api/Bill";

        }


        public async Task<Account> getUser()
        {
            var iduse = HttpContext.Session.GetString("IdUser");
            if (iduse != null)
            {
                HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/id?id=" + iduse);
                string strDate = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Account account = JsonSerializer.Deserialize<Account>(strDate, options);
                return account;
            }
            return null;
        }

        public async Task<IActionResult> Login(string? err)
        {
            HttpContext.Session.Remove("IdUser");
            if (err != null)
            {
                ViewBag.error = err;
            }
            return View();
        }

        public async Task<List<Room>> GetRooms()
        {

            HttpResponseMessage response = await client.GetAsync(RoomAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Room> listRooms = JsonSerializer.Deserialize<List<Room>>(strDate, options);

            if (listRooms != null)
                return listRooms;
            return null;


        }
        public async Task<IActionResult> Admin()
        { var acc = await getUser();
            if (acc != null)
            {
                if (acc.St == 2)
                {
                    ViewBag.username = await getUser();

                    var listRooms = await GetRooms();
                    ViewBag.NumRoom = listRooms.Count;
                    List<Comment> listCommets = await GetCommets();
                    ViewBag.NumComment = listCommets.Count();
                    ViewBag.Earning = await getEarningBill();

                    return View(listRooms);
                }
                else
                {
                    string error = "you must be login!";
                    return RedirectToAction("Login", new { err = error });
                }
            }
            else
            {
                string error = "you must be login!";
                return RedirectToAction("Login", new { err = error });
            }
        }
        public async Task<List<Comment>> GetCommets()
        {
            HttpResponseMessage response = await client.GetAsync(CommentAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Comment> listCommets = JsonSerializer.Deserialize<List<Comment>>(strDate, options);
            if (listCommets != null)
                return listCommets;
            return null;


        }
        public async Task<int> getEarningBill()
        {
            HttpResponseMessage response = await client.GetAsync(BillAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Bill> lists = JsonSerializer.Deserialize<List<Bill>>(strDate, options);
            decimal? total = 0;
            foreach (var item in lists)
            {
                total += item.Price;
            }
            return Convert.ToInt32(total);
        }

        public async Task<IActionResult> customers()
        {
            ViewBag.username = await getUser();
            var listRooms = await GetRooms();
            ViewBag.NumRoom = listRooms.Count;
            List<Comment> listCommets = await GetCommets();
            ViewBag.NumComment = listCommets.Count();
            ViewBag.Earning = await getEarningBill();

            List<Account> listAccounts = await GetAccounts();
            return View(listAccounts);
        }

        public async Task<IActionResult> InfoUsser()
        {
            var idusr = HttpContext.Session.GetString("IdUser");
            HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/id?id=" + idusr);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Account account = JsonSerializer.Deserialize<Account>(strDate, options);

            return View(account);
        }

        public async Task<IActionResult> updateAcc(string idusr)
        {
            ViewBag.username = await getUser();

            HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/id?id=" + idusr);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Account account = JsonSerializer.Deserialize<Account>(strDate, options);

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateAcc([Bind("Idacc,Mail,Password,FullName,Phone,St")] Account account)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PutAsJsonAsync(AccountAPiUrl + "/id?id=" + account.Idacc, account);
                response1.EnsureSuccessStatusCode();
                return RedirectToAction("customers");
            }

            ViewBag.regis = 1;
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InfoUsser([Bind("Idacc,Mail,Password,FullName,Phone,St")] Account account)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PutAsJsonAsync(AccountAPiUrl + "/id?id=" + account.Idacc, account);
                response1.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Room");
            }

            ViewBag.regis = 1;
            return View(account);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            string error = "";
            try
            {

                HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/" + email + "/" + password);
                string strDate = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Account account = JsonSerializer.Deserialize<Account>(strDate, options);

                if (account != null)
                {
                    HttpContext.Session.SetString("IdUser", account.Idacc);
                    if (account.St == 2)
                        return RedirectToAction("Admin");
                    else
                        return RedirectToAction("Index", "Room");
                }
                else
                {
                    return View("Login");
                }
            }
            catch
            {
                error = "you wrong Email or Password";
                return RedirectToAction("Login", new { err = error });
            }
        }


        public async Task<List<Account>> GetAccounts()
        {
            HttpResponseMessage response = await client.GetAsync(AccountAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Account> listAccounts = JsonSerializer.Deserialize<List<Account>>(strDate, options);
            return listAccounts;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Idacc,Mail,Password,FullName,Phone,St")] Account account)
        {

            if (ModelState.IsValid)
            {
                List<Account> listAccounts = await GetAccounts();

                if (listAccounts.FirstOrDefault(a => a.Mail.Equals(account.Mail)) == null)
                {
                    HttpResponseMessage response1 = await client.PostAsJsonAsync(AccountAPiUrl, account);
                    response1.EnsureSuccessStatusCode();

                    HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/" + account.Mail + "/" + account.Password);
                    string strDate = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    account = JsonSerializer.Deserialize<Account>(strDate, options);

                    HttpContext.Session.SetString("IdUser", account.Idacc);

                    return RedirectToAction("Index", "Room");
                }
            }

            ViewBag.regis = 1;
            return View("Login");
        }


        public async Task<IActionResult> CreateAcc()
        {
            ViewBag.username = await getUser();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAcc([Bind("Idacc,Mail,Password,FullName,Phone,St")] Account account)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PostAsJsonAsync(AccountAPiUrl, account);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("customers");
            }

            ViewBag.regis = 1;
            return View("Login");
        }




        public async Task<IActionResult> Delete(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     AccountAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();

            return RedirectToAction("customers");
        }

        public async Task<IActionResult> DeleteRoom(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     RoomAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();

            return RedirectToAction("Admin");
        }
    }
}

