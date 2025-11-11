using Microsoft.AspNetCore.Mvc;
using PraticProect.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PraticProect.Controllers
{
    public class HomeController : Controller
    {
        private static List<Equipment> _equipment = new()
        {
            new Equipment
            {
                Id = 1,
                Name = "Canon EOS R5",
                Description = "Профессиональная беззеркальная камера с высоким разрешением",
                Category = "Камеры",
                Brand = "Canon",
                Model = "EOS R5",
                PricePerDay = 2500,
                IsAvailable = true,
                ImageUrl = "https://images.unsplash.com/photo-1502920917128-1aa500764cbd?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80"
            },
            new Equipment
            {
                Id = 2,
                Name = "Nikon Z7 II",
                Description = "Полнокадровая беззеркальная камера для профессионалов",
                Category = "Камеры",
                Brand = "Nikon",
                Model = "Z7 II",
                PricePerDay = 2200,
                IsAvailable = true,
                ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80"
            },
            new Equipment
            {
                Id = 3,
                Name = "Sony 24-70mm f/2.8",
                Description = "Профессиональный зум-объектив для полнокадровых камер",
                Category = "Объективы",
                Brand = "Sony",
                Model = "24-70mm f/2.8 GM",
                PricePerDay = 800,
                IsAvailable = true,
                ImageUrl = "https://images.unsplash.com/photo-1606983340126-99ab4feaa64a?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80"
            },
            new Equipment
            {
                Id = 4,
                Name = "Godox SL-60W",
                Description = "Светодиодный осветитель для видеосъемки и фото",
                Category = "Освещение",
                Brand = "Godox",
                Model = "SL-60W",
                PricePerDay = 400,
                IsAvailable = true,
                ImageUrl = "https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80"
            }
        };

        private static List<Rental> _rentals = new();

        public IActionResult Index()
        {
            var availableEquipment = _equipment.Where(e => e.IsAvailable).Take(3).ToList();
            return View(availableEquipment);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Equipment()
        {
            return View(_equipment);
        }

        [HttpGet]
        public IActionResult Rent(int id)
        {
            var equipment = _equipment.FirstOrDefault(e => e.Id == id);
            if (equipment == null)
                return NotFound();

            var rental = new Rental
            {
                EquipmentId = id,
                Equipment = equipment,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(2)
            };

            return View(rental);
        }

        [HttpPost]
        public IActionResult Rent(Rental rental)
        {
            if (ModelState.IsValid)
            {
                var equipment = _equipment.FirstOrDefault(e => e.Id == rental.EquipmentId);
                if (equipment != null && equipment.IsAvailable)
                {
                    var days = (rental.EndDate - rental.StartDate).Days;
                    if (days > 0)
                    {
                        rental.Id = _rentals.Count + 1;
                        rental.TotalPrice = days * equipment.PricePerDay;
                        rental.CreatedAt = DateTime.Now;
                        rental.Status = "Подтвержден";

                        _rentals.Add(rental);

                        TempData["SuccessMessage"] = $"Аренда оформлена! Сумма: {rental.TotalPrice} руб.";
                        return RedirectToAction("Index");
                    }
                }
            }

            rental.Equipment = _equipment.FirstOrDefault(e => e.Id == rental.EquipmentId);
            return View(rental);
        }

        public IActionResult MyRentals(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View(new List<Rental>());
            }

            var userRentals = _rentals.Where(r => r.UserEmail == email).ToList();
            return View(userRentals);
        }

        public IActionResult SiteInformation()
        {
            return View();
        }
    }
}