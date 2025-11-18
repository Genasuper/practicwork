using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraticProect.Models;
using PraticProect.DATA;
using System.Linq;
using System.Threading.Tasks;

namespace PraticProect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var availableEquipment = await _context.Equipment
                .Where(e => e.IsAvailable)
                .Take(3)
                .ToListAsync();

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

        public async Task<IActionResult> Equipment()
        {
            var equipment = await _context.Equipment.ToListAsync();
            return View(equipment);
        }

        [HttpGet]
        public async Task<IActionResult> Rent(int id)
        {
            var equipment = await _context.Equipment.FirstOrDefaultAsync(e => e.Id == id);
            if (equipment == null)
                return NotFound();

            var rental = new Rental
            {
                EquipmentId = id,
                Equipment = equipment,
                StartDate = System.DateTime.Today.AddDays(1),
                EndDate = System.DateTime.Today.AddDays(2)
            };

            return View(rental);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(Rental rental)
        {
            if (ModelState.IsValid)
            {
                var equipment = await _context.Equipment.FirstOrDefaultAsync(e => e.Id == rental.EquipmentId);
                if (equipment != null && equipment.IsAvailable)
                {
                    var days = (rental.EndDate - rental.StartDate).Days;
                    if (days > 0)
                    {
                        rental.TotalPrice = days * equipment.PricePerDay;
                        rental.CreatedAt = System.DateTime.Now;
                        rental.Status = "Подтвержден";

                        _context.Rentals.Add(rental);
                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = $"Аренда оформлена! Сумма: {rental.TotalPrice} руб.";
                        return RedirectToAction("Index");
                    }
                }
            }

            rental.Equipment = await _context.Equipment.FirstOrDefaultAsync(e => e.Id == rental.EquipmentId);
            return View(rental);
        }

        public async Task<IActionResult> MyRentals(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View(new System.Collections.Generic.List<Rental>());
            }

            var userRentals = await _context.Rentals
                .Where(r => r.UserEmail == email)
                .Include(r => r.Equipment)
                .ToListAsync();

            return View(userRentals);
        }

        public IActionResult SiteInformation()
        {
            return View();
        }
    }
}