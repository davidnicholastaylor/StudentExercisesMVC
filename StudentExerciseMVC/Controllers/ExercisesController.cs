using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExerciseMVC.Models.ViewModels;
using StudentExercisesAPI.Data;

namespace StudentExerciseMVC.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IConfiguration _config;

        public ExercisesController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Exercises
        public async Task<ActionResult> Index()
        {
            using (IDbConnection conn = Connection)
            {
                IEnumerable<Exercise> exercises = await conn.QueryAsync<Exercise>(@"
                SELECT
                    e.Id,
                    e.Name,
                    e.Language
                FROM Exercise e
                ");
                return View(exercises);
            }
        }

        // GET: Exercises/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string sql = $@"
            SELECT
                e.Id,
                e.Name,
                e.Language
            FROM Exercise e
            WHERE e.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Exercise exercise = await conn.QueryFirstAsync<Exercise>(sql);
                return View(exercise);
            }
        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Exercises/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string sql = $@"
            SELECT
                e.Id,
                e.Name,
                e.Language
            FROM Exercise e
            WHERE e.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Exercise exercise = await conn.QueryFirstAsync<Exercise>(sql);
                return View(exercise);
            }
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Exercise exercise)
        {
            try
            {
                string sql = $@"
                    UPDATE Exercise
                    SET Name = '{exercise.Name}',
                        Language = '{exercise.Language}'
                    WHERE Id = {id}
                ";

                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);
                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return BadRequest();
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Exercises/Delete/5
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            string sql = $@"
            SELECT
                e.Id,
                e.Name,
                e.Language
            FROM Exercise e
            WHERE e.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Exercise exercise = await conn.QueryFirstAsync<Exercise>(sql);
                return View(exercise);

            }
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            string sql = $@"
            DELETE FROM Exercise WHERE Id = {id}";

            using (IDbConnection conn = Connection)
            {
                int rowsAffected = await conn.ExecuteAsync(sql);
                if (rowsAffected > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                throw new Exception("No rows affected");
            }
        }
    }
}