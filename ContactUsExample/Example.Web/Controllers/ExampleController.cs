using Example.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example.Web.Controllers
{
    /*
     * ExampleMVC: The RoutePrefix is combined with the Route on every method below
     * to create a complete Route for each method.
     * 
     * The Method is the endpoint, not the controller.
     * 
     * The purpose of "Routing" is to have a Request find it's way to an end Point.
     * 
     * 
     */

    [RoutePrefix("Example")]
    public class ExampleController : BaseController
    {
        //private IEmployeeService _employeeService;

        //public ExampleController(IEmployeeService employeeService)
        //{
        //    _employeeService = employeeService;
        //}

        #region - Simple Gets - 

        [Route("index"), Route, HttpGet]        
        public ActionResult Index()
        {
            /*
             * FutureOfLatinosMVC: This method has two routes
             * 1) FutureOfLatinos/index
             * 2) FutureOfLatinos
             * 
             * This accepts a GET request 
             * 
             * it renders a view with a file name of Index because that is the name of the method 
             * and when we called the view no other name was provided.
             */

            return View();
        }


        [Route("alpha"), HttpGet]
        public ActionResult Alpha()
        {
            /*
            * FutureOfLatinosMVC: Routes
            * 1) FutureOfLatinos/alpha
            * 
            * 
            * it renders a view with a file name of Alpha because that is the name of the method 
            * and when we called the view no other name was provided.
           */

            return View();
        }

        [Route("Beta")]
        public ActionResult Gamma()
        {
            /*
            * FutureOfLatinosMVC: Routes
            * 1) FutureOfLatinos/beta
            * 
            * 
            * it renders a view with a file name of Gamma because that is the name of the method 
            * and when we called the view no other name was provided.
            */

            return View();
        }

        [Route("Omega")]
        public ActionResult RandomName()
        {
            /*
            * FutureOfLatinosMVC: Routes
            * 1) FutureOfLatinos/Omega
            * 
            * 
            * it renders a view with a file name of Delta because that is the name we provide
             * when calling the View. This overrides the fact that the method in this case is RandomName
            */

            return View("Delta");
        } 
        #endregion

        [Route("Epsilon")]
        public ActionResult Epsilon(int age)
        {
            /*
            * ExampleMVC: Routes
            * 1) Example/epsilon
            * 
            * 
            * it renders a view with a file name of Epsilon because that is the name of the method 
            * and when we called the view no other name was provided.
             * 
             * additionally this method must be passed a parameter of type Int with a name of age
             * 
             * this param is passed into the View wrapped in an ItemViewModel class
            */

            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = age;
            return View(model);
        }

        [Route("Epsilon/{countOfSomething:int}")]
        public ActionResult EpsilonCount(int countOfSomething)
        {

            /*
          * ExampleMVC: Routes
          * 1) Example/Epsilon/{and required to be Followed By Some Int}
          * 
          * 
          * it renders a view with a file name of Epsilon because that is the View Name specified
           * 
           * additionally this method must be passed a parameter of type Int in the route, not as a parameter. 
           * we will refer to this part of the route by the name of countOfSomething
           * 
           * this param is passed into the View wrapped in an ItemViewModel class
          */


            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = countOfSomething;
            return View("Epsilon", model);
        }


        [Route("Epsilon/{uid:guid}")]
        public ActionResult Zeta(Guid uid)
        {
            /*
            * ExampleMVC: Routes
            * 1) Example/Epsilon/{and required to be Followed By Some Guid}
            * 
            * 
            * it renders a view with a file name of Index because that is the View Name specified
            * 
            * additionally this method must be passed a parameter of type Guid in the route, not as a parameter. 
            * we will refer to this part of the route by the name of countOfSomething
            * 
            * this param is passed into the View wrapped in an ItemViewModel class
             * 
             * Make note the route name, method name and view file name do not match
             * 
            */

            ItemViewModel<Guid> model = new ItemViewModel<Guid>();
            model.Item = uid;
            return View("Index",model);
        }


        [Route("zeta/{count}/{firstName}")]
        public ActionResult Zeta(int count, string firstName)
        {
            ExampleViewModel model = new ExampleViewModel();
            model.Data = firstName;
            model.Count = count;
            return View("Index", model);
        }


        [Route("eta")]
        public ActionResult Eta(string firstName,int count = 0)
        {
            ExampleViewModel model = new ExampleViewModel();
            model.Data = firstName;
            model.Count = count;
            return View("Index", model);
        }


        
    }
}
