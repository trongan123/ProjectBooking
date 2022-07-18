using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookingWebClient.Controllers
{
    public class TransportController : Controller
    {
        private readonly HttpClient client = null;
        private string TransportAPiUrl = "";
        private string TypeTransportAPiUrl = "";
        private string BookingTransportDetailAPiUrl = "";
        private string AccountAPiUrl = "";
        private string BillAPiUrl = "";
        private string RoomAPiUrl = "";
        private string CommentAPiUrl = "";
        public TransportController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            TransportAPiUrl = "https://localhost:7159/api/Transport";
            TypeTransportAPiUrl = "https://localhost:7159/api/TypeTransport";
            AccountAPiUrl = "https://localhost:7159/api/Account";
            BillAPiUrl = "https://localhost:7159/api/Bill";
            BookingTransportDetailAPiUrl = "https://localhost:7159/api/BookingTransportDetail";
            RoomAPiUrl = "https://localhost:7159/api/Room";
            CommentAPiUrl = "https://localhost:7159/api/Comment";

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

        public async Task<IActionResult> transport()
        {
            ViewBag.username = await getUser();
            var listRooms = await GetRooms();
            ViewBag.NumRoom = listRooms.Count;
            ViewBag.Earning = await getEarningBill();

            List<Comment> listCommets = await GetCommets();
            ViewBag.NumComment = listCommets.Count();

            List<Transport> listMoto = await GetTransportbytype("TT002");
            List<Transport> listBicycle = await GetTransportbytype("TT001");

            ViewBag.QuantityBicycle = listBicycle.Count;
            ViewBag.priceBicycle = Convert.ToInt32(listBicycle.SingleOrDefault(t => t.Idtransport.Equals("T0001")).Price);
            ViewBag.QuantityMotorcycle = listMoto.Count;
            ViewBag.priceMotorcycle = Convert.ToInt32(listMoto.SingleOrDefault(t => t.Idtransport.Equals("T0002")).Price);


            return View();

        }

        public async Task<IActionResult> Index()
        {

            ViewBag.username = await getUser();
            List<Transport> listMoto = await GetTransportbytype("TT002");
            List<Transport> listBicycle = await GetTransportbytype("TT001");
            ViewBag.idbill = HttpContext.Session.GetString("Idbill");

            ViewBag.priceBicycle = Convert.ToInt32(listBicycle.SingleOrDefault(t => t.Idtransport.Equals("T0001")).Price);

            ViewBag.priceMotorcycle = Convert.ToInt32(listMoto.SingleOrDefault(t => t.Idtransport.Equals("T0002")).Price);
            HttpResponseMessage response = await client.GetAsync(TransportAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Transport> listTransports = JsonSerializer.Deserialize<List<Transport>>(strDate, options);

            return View(listTransports);

        }
        public async Task<List<Transport>> GetTransportbytype(string type)
        {
            HttpResponseMessage response = await client.GetAsync(TransportAPiUrl + "/type?Type=" + type);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Transport> Transports = JsonSerializer.Deserialize<List<Transport>>(strDate, options);
            return Transports;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addmotobike(int motobike)
        {
            bool icheck = true;
            ViewBag.username = await getUser();
            Account user = await getUser();
            var idbill = HttpContext.Session.GetString("Idbill");

            List<Transport> listMoto = await GetTransportbytype("TT002");
            if (listMoto != null && listMoto.Count >= motobike)
            {
                HttpResponseMessage response = await client.GetAsync(BookingTransportDetailAPiUrl + "/idbill?id=" + idbill);
                string strDate1 = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<BookingTransportDetail> listdetaiTransports = JsonSerializer.Deserialize<List<BookingTransportDetail>>(strDate1, options);
                if (listdetaiTransports == null || listdetaiTransports.SingleOrDefault(t => t.Idtransport.Equals("T0002")) == null)
                {

                    BookingTransportDetail detail = new BookingTransportDetail();
                    detail.IdbookingTransportDetail = "BT001";
                    detail.Idbill = idbill;
                    detail.Idtransport = "T0002";
                    detail.Price = listMoto.SingleOrDefault(t => t.Idtransport.Equals("T0002")).Price;
                    detail.St = 0;
                    detail.Quantity = motobike;
                    response = await client.PostAsJsonAsync(BookingTransportDetailAPiUrl, detail);
                    response.EnsureSuccessStatusCode();


                    response = await client.GetAsync(BillAPiUrl + "/id?id=" + idbill);
                    string strDate2 = await response.Content.ReadAsStringAsync();

                    Bill bill = JsonSerializer.Deserialize<Bill>(strDate2, options);

                    TimeSpan Time = bill.EndDay - bill.StartDay;
                    int TongSoNgay = Time.Days;

                    bill.Price += (listMoto.SingleOrDefault(t => t.Idtransport.Equals("T0002")).Price * motobike * TongSoNgay);

                    response = await client.PutAsJsonAsync(BillAPiUrl + "/id?id=" + bill.Idbill, bill);
                    response.EnsureSuccessStatusCode();
                }
                else
                {
                    BookingTransportDetail detail = listdetaiTransports.SingleOrDefault(t => t.Idtransport.Equals("T0002"));
                    if (detail.Quantity != motobike)
                    {
                        int a = detail.Quantity;
                        detail.Quantity = motobike;
                        response = await client.PutAsJsonAsync(BookingTransportDetailAPiUrl + "/id?id=" + detail.IdbookingTransportDetail, detail);
                        response.EnsureSuccessStatusCode();


                        response = await client.GetAsync(BillAPiUrl + "/id?id=" + idbill);
                        string strDate2 = await response.Content.ReadAsStringAsync();

                        Bill bill = JsonSerializer.Deserialize<Bill>(strDate2, options);
                        TimeSpan Time = bill.EndDay - bill.StartDay;
                        int TongSoNgay = Time.Days;
                        bill.Price = (bill.Price - (a * detail.Price * TongSoNgay)) + (detail.Price * motobike * TongSoNgay);

                        response = await client.PutAsJsonAsync(BillAPiUrl + "/id?id=" + bill.Idbill, bill);
                        response.EnsureSuccessStatusCode();
                    }
                }
            }

            return RedirectToAction("Index");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addbycicle(int bycicle)
        {

            ViewBag.username = await getUser();
            Account user = await getUser();
            var idbill = HttpContext.Session.GetString("Idbill");

            List<Transport> listMoto = await GetTransportbytype("TT001");
            if (listMoto != null && listMoto.Count >= bycicle)
            {
                HttpResponseMessage response = await client.GetAsync(BookingTransportDetailAPiUrl + "/idbill?id=" + idbill);
                string strDate1 = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<BookingTransportDetail> listdetaiTransports = JsonSerializer.Deserialize<List<BookingTransportDetail>>(strDate1, options);
                if (listdetaiTransports == null || listdetaiTransports.SingleOrDefault(t => t.Idtransport.Equals("T0001")) == null)
                {

                    BookingTransportDetail detail = new BookingTransportDetail();
                    detail.IdbookingTransportDetail = "BT001";
                    detail.Idbill = idbill;
                    detail.Idtransport = "T0001";
                    detail.Price = listMoto.SingleOrDefault(t => t.Idtransport.Equals("T0001")).Price;
                    detail.St = 0;
                    detail.Quantity = bycicle;
                    response = await client.PostAsJsonAsync(BookingTransportDetailAPiUrl, detail);
                    response.EnsureSuccessStatusCode();


                    response = await client.GetAsync(BillAPiUrl + "/id?id=" + idbill);
                    string strDate2 = await response.Content.ReadAsStringAsync();

                    Bill bill = JsonSerializer.Deserialize<Bill>(strDate2, options);

                    TimeSpan Time = bill.EndDay - bill.StartDay;
                    int TongSoNgay = Time.Days;
                    bill.Price += (listMoto.SingleOrDefault(t => t.Idtransport.Equals("T0001")).Price * bycicle * TongSoNgay);

                    response = await client.PutAsJsonAsync(BillAPiUrl + "/id?id=" + bill.Idbill, bill);
                    response.EnsureSuccessStatusCode();
                }
                else
                {
                    BookingTransportDetail detail = listdetaiTransports.SingleOrDefault(t => t.Idtransport.Equals("T0001"));
                    if (detail.Quantity != bycicle)
                    {
                        int a = detail.Quantity;
                        detail.Quantity = bycicle;
                        response = await client.PutAsJsonAsync(BookingTransportDetailAPiUrl + "/id?id=" + detail.IdbookingTransportDetail, detail);
                        response.EnsureSuccessStatusCode();


                        response = await client.GetAsync(BillAPiUrl + "/id?id=" + idbill);
                        string strDate2 = await response.Content.ReadAsStringAsync();

                        Bill bill = JsonSerializer.Deserialize<Bill>(strDate2, options);

                        TimeSpan Time = bill.EndDay - bill.StartDay;
                        int TongSoNgay = Time.Days;

                        bill.Price = (bill.Price - (a * detail.Price * TongSoNgay)) + (detail.Price * bycicle * TongSoNgay);

                        response = await client.PutAsJsonAsync(BillAPiUrl + "/id?id=" + bill.Idbill, bill);
                        response.EnsureSuccessStatusCode();
                    }
                }

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync(TransportAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Transport Transport = JsonSerializer.Deserialize<Transport>(strDate, options);
            if (Transport == null)
            {
                return NotFound();
            }

            return View(Transport);
        }







        // GET: Transports/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(TransportAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Transport Transport = JsonSerializer.Deserialize<Transport>(strDate, options);

            if (Transport == null)
            {
                return NotFound();

            }
            response = await client.GetAsync(TypeTransportAPiUrl);
            strDate = await response.Content.ReadAsStringAsync();


            List<TypeTransport> list = JsonSerializer.Deserialize<List<TypeTransport>>(strDate, options);
            ViewData["TypeTransportID"] = new SelectList(list, "IdtypeTransport", "NameTranspors");

            return View(Transport);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Idtransport,IdtypeTransport,Price,Description")] Transport Transport)
        {
            if (id.Equals(Transport.Idtransport))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response1 = await client.PutAsJsonAsync(
                     TransportAPiUrl + "/id?id=" + id, Transport);
                    response1.EnsureSuccessStatusCode();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction(nameof(Index));
            }

            HttpResponseMessage response = await client.GetAsync(TypeTransportAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<TypeTransport> list = JsonSerializer.Deserialize<List<TypeTransport>>(strDate, options);
            ViewData["TypeTransportID"] = new SelectList(list, "IdtypeTransport", "NameTranspors");

            return View(Transport);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateBicycle(int priceBicycle)
        {
            HttpResponseMessage response = await client.GetAsync(TransportAPiUrl + "/id?id=T0001");
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Transport transport = JsonSerializer.Deserialize<Transport>(strDate, options);

            transport.Price = Convert.ToDecimal(priceBicycle);
            response = await client.PutAsJsonAsync(TransportAPiUrl + "/id?id=T0001", transport);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("transport");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateMotorcycle(int priceMotorcycle)
        {
            HttpResponseMessage response = await client.GetAsync(TransportAPiUrl + "/id?id=T0002");
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Transport transport = JsonSerializer.Deserialize<Transport>(strDate, options);

            transport.Price = Convert.ToDecimal(priceMotorcycle);
            response = await client.PutAsJsonAsync(TransportAPiUrl + "/id?id=T0002", transport);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("transport");

        }



        public async Task<IActionResult> Delete(string id)
        {
            List<Transport> listMoto = await GetTransportbytype(id);
            if (listMoto.Count > 1)
            {
                string idtran = listMoto[listMoto.Count - 1].Idtransport;
                HttpResponseMessage response1 = await client.DeleteAsync(
                         TransportAPiUrl + "/id?id=" + idtran);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("transport");
            }
            return RedirectToAction("transport");
        }

        public async Task<IActionResult> Create(string id)
        {
            HttpResponseMessage response = await client.GetAsync(TransportAPiUrl + "/id?id="+ id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Transport transport = JsonSerializer.Deserialize<Transport>(strDate, options);

            HttpResponseMessage response1 = await client.PostAsJsonAsync(TransportAPiUrl, transport);
            response1.EnsureSuccessStatusCode();



            return RedirectToAction("transport");
        }
    }
}
