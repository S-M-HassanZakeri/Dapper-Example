using Dapper.WebUI.Models;
using Layer.Common.ErrorExaptation;
using Layer.Domain.Service.Contracts.IUserService;
using Layer.Domian.Entities.DB.Users;
using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic;
using System.Diagnostics;
using static Dapper.SqlMapper;

namespace Dapper.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            this._userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            Tuple<IEnumerable<UserEntity>, ResultStatusOpration> query = await _userService.GetAllAsync();
            switch (query.Item2.TypeStatus)
            {
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Success:
                    return View(query.Item1);
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Warning:
                    return View(query.Item1);
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Error:
                    return Redirect("/Home/Error");

            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserEntity entity)
        {
           
            Tuple<ResultStatusOpration> query = await _userService.InsertAsync(entity);
            switch (query.Item1.TypeStatus)
            {
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Success:
                    return RedirectToAction("Index");
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Warning:
                    return View(query.Item1);
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Error:
                    return Redirect("/Home/Error");

            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            Tuple<UserEntity,ResultStatusOpration> query = await _userService.GetByIdAsync(id);

            switch (query.Item2.TypeStatus)
            {
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Success:
                    return View(query.Item1);
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Warning:
                    return View(query.Item1);
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Error:
                    return Redirect("/Home/Error");

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserEntity entity)
        {

            Tuple<ResultStatusOpration> query = await _userService.UpdateAsync(entity);
            switch (query.Item1.TypeStatus)
            {
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Success:
                    return RedirectToAction("Index");
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Warning:
                    return View(query.Item1);
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Error:
                    return Redirect("/Home/Error");

            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            Tuple<ResultStatusOpration> query = await _userService.DeleteAsync(id);

            switch (query.Item1.TypeStatus)
            {
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Success:
                    return RedirectToAction("index");
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Warning:
                    return View("index");
                case Layer.Common.Enumerate.Enumerate.ResultStatusEnum.Error:
                    return Redirect("/Home/Error");

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
