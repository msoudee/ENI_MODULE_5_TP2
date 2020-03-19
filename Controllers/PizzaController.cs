using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TP2.Controllers
{
    public class PizzaController : Controller
    {
        public static List<Pizza> Pizzas { get; set; }
        public static List<Pate> Pates { get; set; }
        public static List<Ingredient> Ingredients { get; set; }

        public PizzaController()
        {
            if(Pizzas == null)
            {
                Pizzas = new List<Pizza>();
            }

            if (Pates == null)
            {
                Pates = Pizza.PatesDisponibles;
            }

            if (Ingredients == null)
            {
                Ingredients = Pizza.IngredientsDisponibles;
            }
        }

        // GET: Pizza
        public ActionResult Index()
        {
            return View(Pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            var pizza = Pizzas.FirstOrDefault(p => p.Id == id);

            return View(pizza);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(Pizza pizza, int pate, List<int> ingredients)
        {
            try
            {
                if(pizza != null)
                {
                    Pate patePizza = Pates.FirstOrDefault(p => p.Id == pate);

                    List<Ingredient> ingredientsPizza = new List<Ingredient>();
                    foreach (var ingr in ingredients)
                    {
                        ingredientsPizza.Add(Ingredients.FirstOrDefault(i => i.Id == ingr));
                    }

                    pizza.Id = Pizzas.Count();
                    pizza.Pate = patePizza;
                    pizza.Ingredients = ingredientsPizza;

                    Pizzas.Add(pizza);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            var pizza = Pizzas.FirstOrDefault(p => p.Id == id);

            return View(pizza);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Pizza pizza, int pate, List<int> ingredients)
        {
            try
            { 
                Pate patePizza = Pates.FirstOrDefault(p => p.Id == pate);

                List<Ingredient> ingredientsPizza = new List<Ingredient>();
                foreach (var ingr in ingredients)
                {
                    ingredientsPizza.Add(Ingredients.FirstOrDefault(i => i.Id == ingr));
                }


                var maPizza = Pizzas.FirstOrDefault(p => p.Id == id);

                maPizza.Nom = pizza.Nom;
                maPizza.Pate = patePizza;
                maPizza.Ingredients = ingredientsPizza;

                Pizzas.Remove(Pizzas.FirstOrDefault(p => p.Id == maPizza.Id));
                Pizzas.Add(maPizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            var pizza = Pizzas.FirstOrDefault(p => p.Id == id);

            return View(pizza);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizzas.Remove(Pizzas.FirstOrDefault(p => p.Id == id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
