using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolios = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolios);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPortfolio(string system)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySystemAsync(system);

            if (stock == null)
            {
                return BadRequest("Stock not found");
            }
            var userPortfolios = await _portfolioRepo.GetUserPortfolio(appUser);
            if (userPortfolios.Any(s => s.System.ToLower() == system.ToLower()))
            {
                return BadRequest("Cannot add the same stock to portfolio");
            }
            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };
            await _portfolioRepo.CreateAsync(portfolioModel);
            if (portfolioModel == null)
            {
                return StatusCode(500, "Failed to add stock to portfolio");
            }
            else
            {
                return Created();
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string system)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
        
            var userPortfolios = await _portfolioRepo.GetUserPortfolio(appUser);
           var filteredPortfolio = userPortfolios.Where(s => s.System.ToLower() == system.ToLower()).ToList();
            if (filteredPortfolio.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolio(appUser, system);
            }
            else
            {
                return BadRequest("Cannot delete portfolio with multiple stocks of the same system");
            }
            
            return NoContent();
        }
    }
}