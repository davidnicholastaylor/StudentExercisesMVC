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
    public class CohortsController : Controller
    {
        private readonly IConfiguration _config;

        public CohortsController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET: Cohorts
        public async Task<ActionResult> Index()
        {
            using (IDbConnection conn = Connection)
            {

                IEnumerable<Cohort> cohorts = await conn.QueryAsync<Cohort>(@"
                    SELECT 
                        c.Id,
                        c.Name
                    FROM Cohort c
                ");
                return View(cohorts);
            }
        }

        // GET: Cohorts/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string sql = $@"
            SELECT 
                c.Id,
                c.Name
            FROM Cohort c
            WHERE c.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Cohort cohort = await conn.QueryFirstAsync<Cohort>(sql);
                return View(cohort);
            }
        }

        // GET: Cohorts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cohorts/Create
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

        // GET: Cohorts/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string sql = $@"
            SELECT
                c.Id,
                c.Name
            FROM Cohort c
            WHERE c.Id = {id}
            ";
            using (IDbConnection conn = Connection)
            {
                Cohort cohort = await conn.QueryFirstAsync<Cohort>(sql);
                return View();
            }
        }

        // POST: Cohorts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Cohort cohort)
        {
            try
            {
                string sql = $@"
                UPDATE Cohort
                SET CohortName = '{cohort.Name}'
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

        // GET: Cohorts/Delete/5
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            string sql = $@"
            SELECT
                c.Id,
                c.Name
            FROM Cohort c
            WHERE c.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Cohort cohort = await conn.QueryFirstAsync<Cohort>(sql);
                return View();
            }
        }

        // POST: Cohorts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            string sql = $@"
            DELETE FROM Cohort WHERE Id = {id}";

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