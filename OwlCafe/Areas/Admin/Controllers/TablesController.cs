using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlCafe.Models;
using OwlCafe.Repositories.Interfaces;

namespace OwlCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TablesController : Controller
    {
        private readonly IRestaurantTableRepository _tableRepository;
        private readonly ILogger<TablesController> _logger;

        public TablesController(IRestaurantTableRepository tableRepository, ILogger<TablesController> logger)
        {
            _tableRepository = tableRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var tables = await _tableRepository.GetAllAsync();
                return View(tables.OrderBy(t => t.TableNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading tables");
                return View(new List<RestaurantTable>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RestaurantTable table)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(table);

                table.CreatedDate = DateTime.UtcNow;

                await _tableRepository.AddAsync(table);
                await _tableRepository.SaveChangesAsync();

                TempData["Success"] = "Table created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating table");
                ModelState.AddModelError("", "Error creating table");
                return View(table);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
                return NotFound();

            return View("Create", table);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RestaurantTable table)
        {
            if (id != table.Id)
                return NotFound();

            try
            {
                var existingTable = await _tableRepository.GetByIdAsync(id);
                if (existingTable == null)
                    return NotFound();

                existingTable.TableNumber = table.TableNumber;
                existingTable.Capacity = table.Capacity;
                existingTable.Location = table.Location;
                existingTable.Status = table.Status;

                _tableRepository.Update(existingTable);
                await _tableRepository.SaveChangesAsync();

                TempData["Success"] = "Table updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating table");
                ModelState.AddModelError("", "Error updating table");
                return View("Create", table);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var table = await _tableRepository.GetByIdAsync(id);
                if (table == null)
                    return NotFound();

                _tableRepository.Remove(table);
                await _tableRepository.SaveChangesAsync();

                TempData["Success"] = "Table deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting table");
                TempData["Error"] = "Error deleting table";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
