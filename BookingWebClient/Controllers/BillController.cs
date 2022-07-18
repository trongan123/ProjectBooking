using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookingWebClient.Controllers
{
    public class BillController : Controller
    {
        private readonly HttpClient client = null;
        private string BillAPiUrl = "";
        private string AccountAPiUrl = "";
        private string RoomAPiUrl = "";
        private string CommentAPiUrl = "";
        private string BookingRoomDetailAPiUrl = "";
        private string BookingSeviceDetailAPiUrl = "";
        private string BookingTransportDetailAPiUrl = "";


        public BillController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BillAPiUrl = "https://localhost:7159/api/Bill";
            AccountAPiUrl = "https://localhost:7159/api/Account";
            RoomAPiUrl = "https://localhost:7159/api/Room";
            CommentAPiUrl = "https://localhost:7159/api/Comment";
            BookingRoomDetailAPiUrl = "https://localhost:7159/api/BookingRoomDetail";
            BookingSeviceDetailAPiUrl = "https://localhost:7159/api/BookingSeviceDetail";
            BookingTransportDetailAPiUrl = "https://localhost:7159/api/BookingTransportDetail";

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

        public async Task<List<BookingRoomDetail>> getBookingRoomDetails(string id)
        {

            HttpResponseMessage response = await client.GetAsync(BookingRoomDetailAPiUrl + "/idbill?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<BookingRoomDetail> listbills = JsonSerializer.Deserialize<List<BookingRoomDetail>>(strDate, options);

            if (listbills != null)
                return listbills;
            return null;
        }
        public async Task<List<BookingSeviceDetail>> getBookingSeviceDetails(string id)
        {

            HttpResponseMessage response = await client.GetAsync(BookingSeviceDetailAPiUrl + "/idbill?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<BookingSeviceDetail> listbills = JsonSerializer.Deserialize<List<BookingSeviceDetail>>(strDate, options);

            if (listbills != null)
                return listbills;
            return null;


        }
        public async Task<List<BookingTransportDetail>> getBookingTransportDetails(string id)
        {

            HttpResponseMessage response = await client.GetAsync(BookingTransportDetailAPiUrl + "/idbill?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<BookingTransportDetail> listbills = JsonSerializer.Deserialize<List<BookingTransportDetail>>(strDate, options);

            if (listbills != null)
                return listbills;
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
            decimal? total =0;
            foreach(var item in lists)
            {
                total += item.Price;
            }
            return Convert.ToInt32(total);
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
        public async Task<IActionResult> Index(string? id)
        {
            ViewBag.username = await getUser();

            HttpResponseMessage response = await client.GetAsync(BillAPiUrl + "/idacc?idacc=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Bill> listCommets = JsonSerializer.Deserialize<List<Bill>>(strDate, options);

            return View(listCommets);
        }

        public async Task<Account> getadmin(string admin)
        {
            
            if (admin != null)
            {
                HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/id?id=" + admin);
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
        public async Task<IActionResult> DetailBill(string? id)
        {
            HttpResponseMessage response = await client.GetAsync(BillAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Bill bill = JsonSerializer.Deserialize<Bill>(strDate, options);

            if (bill != null)
            {
                ViewBag.username = await getUser();
                ViewBag.Roomdetail = await getBookingRoomDetails(bill.Idbill);
                ViewBag.Servicedetail = await getBookingSeviceDetails(bill.Idbill);
                ViewBag.Transportdetail = await getBookingTransportDetails(bill.Idbill);
                if (!bill.Idadmin.Equals("admin"))
                    ViewBag.Admin = await getadmin(bill.Idadmin);

                return View(bill);
            }
            return RedirectToAction("History");
        }

        public async Task<IActionResult> ViewDetailUser(string id)
        {
            HttpResponseMessage response = await client.GetAsync(BillAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Bill bill = JsonSerializer.Deserialize<Bill>(strDate, options);



            if (bill != null)
            {
                ViewBag.username = await getUser();
                ViewBag.Roomdetail = await getBookingRoomDetails(bill.Idbill);
                ViewBag.Servicedetail = await getBookingSeviceDetails(bill.Idbill);
                ViewBag.Transportdetail = await getBookingTransportDetails(bill.Idbill);


                return View("bill");
            }
            return RedirectToAction("History");
        }


        public async Task<IActionResult> BillAdmin()
        {
            ViewBag.username = await getUser();

            var listRooms = await GetRooms();
            ViewBag.NumRoom = listRooms.Count;
            List<Comment> listCommets = await GetCommets();
            ViewBag.NumComment = listCommets.Count();
            ViewBag.Earning = await getEarningBill();

            HttpResponseMessage response = await client.GetAsync(BillAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Bill> lists = JsonSerializer.Deserialize<List<Bill>>(strDate, options);

            return View(lists);
        }
        public async Task<IActionResult> History()
        {
            ViewBag.username = await getUser();

            var idusr = HttpContext.Session.GetString("IdUser");


            HttpResponseMessage response = await client.GetAsync(BillAPiUrl + "/idacc?id=" + idusr);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Bill> lists = JsonSerializer.Deserialize<List<Bill>>(strDate, options);

            return View(lists);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBill,Rate,Description,Idacc")] Bill Bill)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PostAsJsonAsync(BillAPiUrl, Bill);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdBill,Rate,Description,Idacc")] Bill Bill)
        {
            if (id != Bill.Idbill)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response1 = await client.PutAsJsonAsync(
                     BillAPiUrl + "/id?id=" + id, Bill);
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



        

        public async Task<IActionResult> ComfirmBill(string id)
        {
            HttpResponseMessage response = await client.GetAsync(BillAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Bill bill = JsonSerializer.Deserialize<Bill>(strDate, options);

            if (bill != null)
            {
                bill.Idadmin = HttpContext.Session.GetString("IdUser");
                HttpResponseMessage response1 = await client.PutAsJsonAsync(
                          BillAPiUrl + "/id?id=" + id, bill);
                response1.EnsureSuccessStatusCode();
            }
            return RedirectToAction("BillAdmin");
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     BillAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
    }
}
