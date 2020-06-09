using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Async.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList()
        {
            //Create a stopwatch for getting excution time
            var watch = new Stopwatch();
            watch.Start();

            var country = GetCountry();
            var state = GetState();
            var city = GetCity();

            watch.Stop();
            ViewBag.WatchMilliseconds = watch.ElapsedMilliseconds;

            return View();
        }

        public async Task<ActionResult> GetListAsync()
        {
            //Create a stopwatch for getting excution time
            var watch = new Stopwatch();
            watch.Start();

            Debug.WriteLine("GetListAsync BEGIN: " + Thread.CurrentThread.ManagedThreadId);

            var country = GetCountryAsync();
            var state = GetStateAsync();
            var city = GetCityAsync();

            var content = await country;
            var count = await state;
            var name = await city;

            watch.Stop();
            Debug.WriteLine("GetListAsync END: " + Thread.CurrentThread.ManagedThreadId);
            ViewBag.WatchMilliseconds = watch.ElapsedMilliseconds;

            return View();
        }

        #region --> GetCountry Methods for GetList && GetListAsync
        public string GetCountry()
        {
            Thread.Sleep(3000); //Use - when you want to block the current thread.
            return "India";
        }

        public async Task<string> GetCountryAsync()
        {
            Debug.WriteLine("GetCountryAsync BEGIN: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(3000); //Use - when you want a logical delay without blocking the current thread.
            Debug.WriteLine("GetCountryAsync END: " + Thread.CurrentThread.ManagedThreadId);
            return "India";
        }
        #endregion

        #region --> GetState Methods for GetList && GetListAsync
        public string GetState()
        {
            Thread.Sleep(5000); //Use - when you want to block the current thread.
            return "Gujarat";
        }

        public async Task<string> GetStateAsync()
        {
            Debug.WriteLine("GetStateAsync BEGIN: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(5000); //Use - when you want a logical delay without blocking the current thread.
            Debug.WriteLine("GetStateAsync END: " + Thread.CurrentThread.ManagedThreadId);
            return "Gujarat";
        }
        #endregion

        #region --> GetCity Methods for GetList && GetListAsync
        public string GetCity()
        {
            Thread.Sleep(6000); //Use - when you want to block the current thread.
            return "Junagadh";
        }

        public async Task<string> GetCityAsync()
        {
            Debug.WriteLine("GetCityAsync BEGIN: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(6000); //Use - when you want a logical delay without blocking the current thread.
            Debug.WriteLine("GetCityAsync END: " + Thread.CurrentThread.ManagedThreadId);
            return "Junagadh";
        }
        #endregion

    }
}