using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookingWebClient.Controllers
{
    public class CommentController : Controller
    {
        private readonly HttpClient client = null;
        private string CommentAPiUrl = "";
        private string AccountAPiUrl = "";
        private string RoomAPiUrl = "";
        private string BillAPiUrl = "";

        public CommentController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CommentAPiUrl = "https://localhost:7159/api/Comment";
            AccountAPiUrl = "https://localhost:7159/api/Account";
            RoomAPiUrl = "https://localhost:7159/api/Room";
            BillAPiUrl = "https://localhost:7159/api/Bill";

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
        public async Task<Account> getUser()
        {
            var idusr = HttpContext.Session.GetString("IdUser");
            if (idusr != null)
            {
                HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/id?id=" + idusr);
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

        public async Task<IActionResult> comment()
        {
            ViewBag.username = await getUser();
            var listRooms = await GetRooms();
            ViewBag.NumRoom = listRooms.Count;
            ViewBag.Earning = await getEarningBill();

            List<Comment> listCommets = await GetCommets();
            ViewBag.NumComment = listCommets.Count();
            return View(listCommets);
        }
       
        public async Task<IActionResult> UserComment()
        {
            ViewBag.username = await getUser();
           
            List<Comment> listComments = await GetCommets(); ;
            ViewData["comment"] = listComments;
            return View();

        }


        public async Task<IActionResult> Index(string? id)
        {
            ViewBag.username = await getUser();

            HttpResponseMessage response = await client.GetAsync(CommentAPiUrl+ "/idacc?idacc=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Comment> listCommets = JsonSerializer.Deserialize<List<Comment>> (strDate, options);

            return View(listCommets);
        }
        


             [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(string comment)
        {
            var idusr = HttpContext.Session.GetString("IdUser");
            if (idusr != null)
            {
                if (comment != null)
                {
                    Comment com = new Comment();
                    com.Idcomment = "C0001";
                    com.Rate = 5;
                    com.Description = comment;
                    com.Idacc = HttpContext.Session.GetString("IdUser");

                    HttpResponseMessage response1 = await client.PostAsJsonAsync(CommentAPiUrl, com);
                    response1.EnsureSuccessStatusCode();

                }
                return RedirectToAction("UserComment");
            }
            else
            {
 
                string error = "you must be login!";
                return RedirectToAction("Login", "Account", new { err = error });
            }

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcomment,Rate,Description,Idacc")] Comment comment)
        {
            ViewBag.username = await getUser();
            comment.Idacc = HttpContext.Session.GetString("IdUser");
            if (comment.Idacc != null)
            {
                comment.Idcomment = "";
                HttpResponseMessage response = await client.PostAsJsonAsync(CommentAPiUrl, comment);
                response.EnsureSuccessStatusCode();

                string strDate = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                return RedirectToAction("UserComment");
            }
            else
            {

                string error = "you must be login!";
                return RedirectToAction("Login", "Account", new { err = error });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Idcomment,Rate,Description,Idacc")] Comment comment)
        {
            if (id != comment.Idcomment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response1 = await client.PutAsJsonAsync(
                     CommentAPiUrl + "/id?id=" + id, comment);
                    response1.EnsureSuccessStatusCode();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        


        public async Task<IActionResult> DeleteComment(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     CommentAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();

            return RedirectToAction("comment");
        }
    }
}
