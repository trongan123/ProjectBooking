using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RoomingWebClient.Controllers
{
    public class RoomController : Controller
    {
        private readonly HttpClient client = null;
        private string RoomAPiUrl = "";
        private string RoomTypeAPiUrl = "";
        private string AccountAPiUrl = "";
        private string BillAPiUrl = "";
        private string BookingRoomDetailAPiUrl = "";

        public RoomController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            RoomAPiUrl = "https://localhost:7159/api/Room";
            RoomTypeAPiUrl = "https://localhost:7159/api/RoomType";
            BillAPiUrl = "https://localhost:7159/api/Bill";
            AccountAPiUrl = "https://localhost:7159/api/Account";
            BookingRoomDetailAPiUrl = "https://localhost:7159/api/BookingRoomDetail";


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

        public async Task<string> GetIdBiiCuoi()
        {

            HttpResponseMessage response = await client.GetAsync(BillAPiUrl + "/bill?bill=room");
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            string account = JsonSerializer.Deserialize<string>(strDate, options);
            return account;
        }

        public async Task<List<Room>> GetemptyRoom(string type)
        {


            HttpResponseMessage response = await client.GetAsync(RoomAPiUrl + "/type?Type=" + type);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Room> listRooms = JsonSerializer.Deserialize<List<Room>>(strDate, options);
            return listRooms;

        }

        public async Task<IActionResult> Index(string? err)
        {
            ViewBag.username = await getUser();
            if (err != null)
            {
                ViewBag.error = err;
            }
            HttpResponseMessage response = await client.GetAsync(RoomAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Room> listRooms = JsonSerializer.Deserialize<List<Room>>(strDate, options);

            return View(listRooms);

        }
        public async Task<IActionResult> AboutUs(string? err)
        {
            ViewBag.username = await getUser();
            if (err != null)
            {
                ViewBag.error = err;
            }
            HttpResponseMessage response = await client.GetAsync(RoomAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Room> listRooms = JsonSerializer.Deserialize<List<Room>>(strDate, options);

            return View(listRooms);

        }

        public async Task<IActionResult> ShowRoom()
        {
            ViewBag.username = await getUser();

            HttpResponseMessage response = await client.GetAsync(RoomAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Room> listRooms = JsonSerializer.Deserialize<List<Room>>(strDate, options);

            return View(listRooms);

        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync(RoomAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Room Room = JsonSerializer.Deserialize<Room>(strDate, options);
            if (Room == null)
            {
                return NotFound();
            }

            return View(Room);
        }

        public async Task<IActionResult> AddRoom()
        {
            HttpResponseMessage response = await client.GetAsync(RoomTypeAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<RoomType> list = JsonSerializer.Deserialize<List<RoomType>>(strDate, options);
            ViewData["RoomTypeID"] = new SelectList(list, "IdroomType", "NameRoomType");
            return View();
        }



      


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoom([Bind("Idroom,NumberRoom,IdroomType,Stroom,Image,Price,Description")] Room Room)
        {
            if (ModelState.IsValid)
            {
                Room.Image = "/images/" + Room.Image;
                HttpResponseMessage response1 = await client.PostAsJsonAsync(RoomAPiUrl, Room);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("Admin", "Account");
            }


            HttpResponseMessage response = await client.GetAsync(RoomTypeAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<RoomType> list = JsonSerializer.Deserialize<List<RoomType>>(strDate, options);
            ViewData["RoomTypeID"] = new SelectList(list, "IdroomType", "NameRoomType");
            return View(Room);
        }


        public async Task<Bill> getbill(DateTime StartDay, DateTime EndDay)
        {


            Account user = await getUser();

            Bill bill = new Bill();
            bill.Idbill = "B0001";
            bill.Idacc = user.Idacc;
            bill.Idadmin = "admin";
            bill.StartDay = StartDay;
            bill.EndDay = EndDay;
            bill.Price = 0;
            bill.Phone = user.Phone;

            return bill;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Booking(string name, DateTime StartDay, DateTime EndDay, int adults, int room)
        {
            string error = "";
            if (await getUser() == null)
            {
                return RedirectToAction("Login", "Account");

            }
            if (StartDay >= EndDay)
            {
                error = "You must be choice checkout date after date checkin";
                return RedirectToAction("Index", new { err = error });
            }
            if (adults == 0) adults = 1;
            if (room == 0) room = 1;

            try
            {

                int don = 0;
                int doi = 0;
                int Sroom = room * 2;
                bool check = true;
                decimal total = 0;
                int i = 0;
                do
                {
                    if (Sroom >= adults)
                    {
                        check = false;
                        don = room;
                        doi = i;
                    }
                    else
                    {
                        i++;
                        Sroom += 2;
                        room--;
                    }

                } while (check);

                List<Room> listSingleRoom = await GetemptyRoom("RT001");
                List<Room> listDoubleRoom = await GetemptyRoom("RT002");

                HttpResponseMessage response = await client.GetAsync(BillAPiUrl);
                string strDate = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Bill> listBills = JsonSerializer.Deserialize<List<Bill>>(strDate, options);

                foreach (var item in listBills)
                {
                    if (StartDay <= item.EndDay && EndDay >= item.StartDay)
                    {
                        response = await client.GetAsync(BookingRoomDetailAPiUrl + "/idbill?id=" + item.Idbill);
                        string strDate1 = await response.Content.ReadAsStringAsync();
                        List<BookingRoomDetail> listdetaiRooms = JsonSerializer.Deserialize<List<BookingRoomDetail>>(strDate1, options);
                        foreach (var iroom in listdetaiRooms)
                        {
                            if (iroom.IdroomNavigation.IdroomType.Equals("RT001"))
                                listSingleRoom.FirstOrDefault(a => a.Idroom.Equals(iroom.Idroom)).Stroom = 2;
                            if (iroom.IdroomNavigation.IdroomType.Equals("RT002"))
                                listDoubleRoom.FirstOrDefault(a => a.Idroom.Equals(iroom.Idroom)).Stroom = 2;
                        }
                    }
                }
                listSingleRoom = listSingleRoom.Where(a => a.Stroom == 1).ToList();
                listDoubleRoom = listDoubleRoom.Where(a => a.Stroom == 1).ToList();

                if (listSingleRoom.Count >= don && listDoubleRoom.Count >= doi)
                {

                    HttpContext.Session.SetString("Idbill", await GetIdBiiCuoi());
                    HttpContext.Session.SetInt32("People", adults);

                    var idbill = HttpContext.Session.GetString("Idbill");
                    Bill bill = await getbill(StartDay, EndDay);
                    bill.Idbill = idbill;

                    HttpResponseMessage response1 = await client.PostAsJsonAsync(BillAPiUrl, bill);
                    response1.EnsureSuccessStatusCode();

                    foreach (var item in listSingleRoom)
                    {
                        if (don != 0)
                        {
                            BookingRoomDetail detail = new BookingRoomDetail();
                            detail.IdbookingRoomDetail = "BR001";
                            detail.Idbill = idbill;
                            detail.Idroom = item.Idroom;
                            detail.Price = item.Price;
                            detail.St = 0;
                            response1 = await client.PostAsJsonAsync(BookingRoomDetailAPiUrl, detail);
                            response1.EnsureSuccessStatusCode();
                            total += item.Price;

                            don--;
                        }
                        else
                        {
                            break;
                        }
                    }
                    foreach (var item in listDoubleRoom)
                    {
                        if (doi != 0)
                        {
                            BookingRoomDetail detail = new BookingRoomDetail();
                            detail.IdbookingRoomDetail = "BR001";
                            detail.Idbill = idbill;
                            detail.Idroom = item.Idroom;
                            detail.Price = item.Price;
                            detail.St = 0;
                            response1 = await client.PostAsJsonAsync(BookingRoomDetailAPiUrl, detail);
                            response1.EnsureSuccessStatusCode();

                            total += item.Price;
                            doi--;
                        }
                        else
                        {
                            break;
                        }
                    }
                    TimeSpan Time = bill.EndDay - bill.StartDay;
                    int TongSoNgay = Time.Days;

                    bill.Price = total * TongSoNgay;
                    response1 = await client.PutAsJsonAsync(BillAPiUrl + "/id?id=" + bill.Idbill, bill);
                    response1.EnsureSuccessStatusCode();
                }
                else
                {
                    error = "We just have " + listSingleRoom.Count + " Single and " + listDoubleRoom.Count + " Double Room";
                    return RedirectToAction("Index", new { err = error });
                }

            }
            catch
            {
                return RedirectToAction("Index");
            }


            return RedirectToAction("Index", "Service");

        }
        public async Task<Room> getaRoom(string id)
        {
         
            HttpResponseMessage response = await client.GetAsync(RoomAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Room room = JsonSerializer.Deserialize<Room>(strDate, options);
            return room;
        }



        // GET: Rooms/Edit/5
        public async Task<IActionResult> UpdateRoom(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Room Room = await getaRoom(id);

            if (Room == null)
            {
                return NotFound();

            }
            HttpResponseMessage response = await client.GetAsync(RoomTypeAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<RoomType> list = JsonSerializer.Deserialize<List<RoomType>>(strDate, options);

            ViewData["RoomTypeID"] = new SelectList(list, "IdroomType", "NameRoomType");

            return View(Room);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRoom(string id, [Bind("Idroom,NumberRoom,IdroomType,Stroom,Image,Price,Description")] Room Room)
        {
            if (!id.Equals(Room.Idroom))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Room.Image == null)
                    {
                        Room R = await getaRoom(id);
                        Room.Image = R.Image;
                    }
                    else
                    {
                        Room.Image = "/images/" + Room.Image;
                    }
                    HttpResponseMessage response1 = await client.PutAsJsonAsync(
                     RoomAPiUrl + "/id?id=" + id, Room);
                    response1.EnsureSuccessStatusCode();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction("Admin", "Account");
            }

            HttpResponseMessage response = await client.GetAsync(RoomTypeAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<RoomType> list = JsonSerializer.Deserialize<List<RoomType>>(strDate, options);
            ViewData["RoomTypeID"] = new SelectList(list, "IdroomType", "NameRoomType");

            return View(Room);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     RoomAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }

    }
}
