using System.Data;
using CRUD_Dapper1.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace CRUD_Dapper1.Controllers
{
    public class PessoaController : Controller
    {
        private readonly string ConnectionString = "User ID=postgres;Password=123456789;Host=localhost;Port=5432;Database=PessoasDB;";
        public IActionResult Index()
        {
            IDbConnection con;
            try
            {
                string selecaoQuery = "Select * FROM pessoas";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                IEnumerable<Pessoas> listaPessoas = con.Query<Pessoas>(selecaoQuery).ToList();
                return View(listaPessoas);
            }

            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Pessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                IDbConnection con;

                try
                {
                    string InsercaoQuery = "INSERT INTO pessoas(nome, idade, peso) Values( @Nome, @Idade, @Peso)";
                    con = new NpgsqlConnection(ConnectionString);
                    con.Open();
                    con.Execute(InsercaoQuery, pessoas);
                    con.Close();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View(pessoas);
         
        }
        [HttpGet]
        public IActionResult Edit(int pessoaid)
        {
            IDbConnection con;
            try
            {
                string selecaoQuery = "SELECT * FROM pessoas WHERE pessoaid = @pessoaid";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                Pessoas pessoas = con.Query<Pessoas>(selecaoQuery, new { pessoaid = pessoaid }).FirstOrDefault();
                con.Close();
                return View(pessoas);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult Edit(int pessoaid, Pessoas pessoas)
        {
            if (pessoaid != pessoas.PessoaID)
                return NotFound();  
            if (ModelState.IsValid)
            {
                IDbConnection con;
                try
                {
                    con = new NpgsqlConnection(ConnectionString);
                    string atualizarQuery = "UPDATE pessoas SET Nome=@Nome, Idade = @idade, Peso= @peso WHERE PessoaID = @pessoaId";
                    con.Open();
                    con.Execute(atualizarQuery, pessoas);
                    con.Close();
                    return RedirectToAction(nameof(Index)); 

                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return View(pessoas);
        }
        [HttpPost]
        public IActionResult Delete(int pessoaid)
        {
            IDbConnection con;
            try
            {
                string excluirQuery = "DELETE FROM pessoas WHERE PessoaID = @pessoaid";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                con.Execute(excluirQuery, new { pessoaid = @pessoaid });
                con.Close();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    } 
}
