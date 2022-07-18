using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookingWebClient.Controllers
{
    public class ServiceController : Controller
    {
        private readonly HttpClient client = null;
        private string ServiceAPiUrl = "";
        private string AccountAPiUrl = "";
        private string BookingSeviceDetailAPiUrl = "";
        private string BillAPiUrl = "";

        public ServiceController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ServiceAPiUrl = "https://localhost:7159/api/Service";
            AccountAPiUrl = "https://localhost:7159/api/Account";
            BillAPiUrl = "https://localhost:7159/api/Bill";
            BookingSeviceDetailAPiUrl = "https://localhost:7159/api/BookingSeviceDetail";

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

        public async Task<IActionResult> Index()
        {
            ViewBag.username = await getUser();

            HttpResponseMessage response = await client.GetAsync(ServiceAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Service> listServices = JsonSerializer.Deserialize<List<Service>>(strDate, options);

            return View(listServices);
        }

        public async Task<IActionResult> AddtoBill(string id)
        {
            bool icheck = true;
            ViewBag.username = await getUser();
            Account user = await getUser();
            var idbill = HttpContext.Session.GetString("Idbill");

            HttpResponseMessage response = await client.GetAsync(ServiceAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Service Service = JsonSerializer.Deserialize<Service>(strDate, options);

            response = await client.GetAsync(BookingSeviceDetailAPiUrl + "/idbill?id=" + idbill);
            string strDate1 = await response.Content.ReadAsStringAsync();

            List<BookingSeviceDetail> listdetaiServices = JsonSerializer.Deserialize<List<BookingSeviceDetail>>(strDate1, options);
            if (listdetaiServices != null)
            {
                foreach (var item in listdetaiServices)
                {
                    if (item.Idsevice.Equals(id))
                    {
                        icheck = false;
                        break;
                    }
                }
            }
            if (icheck)
            {
                var Np = HttpContext.Session.GetInt32("People");
                BookingSeviceDetail detail = new BookingSeviceDetail();
                detail.IdbookingSeviceDetail = "BS001";
                detail.Idbill = idbill;
                detail.Idsevice = id;
                detail.Price = Service.Price;
                detail.St = 0;
                detail.Quantity = Convert.ToInt32(Np);
                response = await client.PostAsJsonAsync(BookingSeviceDetailAPiUrl, detail);
                response.EnsureSuccessStatusCode();

                response = await client.GetAsync(BillAPiUrl + "/id?id=" + idbill);
                string strDate2 = await response.Content.ReadAsStringAsync();

                Bill bill = JsonSerializer.Deserialize<Bill>(strDate2, options);

                TimeSpan Time = bill.EndDay - bill.StartDay;
                int TongSoNgay = Time.Days;


                bill.Price += (Service.Price * Np * TongSoNgay);

                response = await client.PutAsJsonAsync(BillAPiUrl + "/id?id=" + bill.Idbill, bill);
                response.EnsureSuccessStatusCode();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowService()
        {
            ViewBag.username = await getUser();

            HttpResponseMessage response = await client.GetAsync(ServiceAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Service> listServices = JsonSerializer.Deserialize<List<Service>>(strDate, options);

            return View(listServices);

        }
    }
}