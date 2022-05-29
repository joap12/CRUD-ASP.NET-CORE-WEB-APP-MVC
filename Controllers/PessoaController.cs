using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Repositories;
using System.Data;

namespace WebApplication3.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString;
        private PessoaRepository respository;
        public PessoaController(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            respository = new PessoaRepository(ConnectionString);
        }

        // GET: Pessoa
        public ActionResult Index() // Retorna os dados do banco de dados
        {
            return View(respository.GetAll()); // respository é a conexão ao banco de dados; GetAll retorna os itens na coleção especificada; Portanto respository.GetAll retornara os itens do banco de dados
        }

        // GET: Pessoa/Create
        [HttpGet]
        public ActionResult Create() // Cria os dados no banco de dados *se forem validos*
        {
            return View(); // No View temos um ActionLink que referencia a ação a este Create Get, que por sua vez, retorna View() ao create.cshtml
        }

        // POST: Pessoa/Create
        [HttpPost]
        public ActionResult Create(Pessoa pessoa) // Cria os dados no banco de dados *se forem validos*
        {
            if (ModelState.IsValid) // ModelState.IsValid, retorna um valor de true ou false, que por sua vez determina se a entrada de dados possue valor
            {
                respository.Save(pessoa); // repository.Save é um metódo que cria de um objeto recebido em dados para a tabela
                return RedirectToAction("Index"); // RedirectToAction("Index) Redireciona o Client a Página Index
            }
            else // Caso os dados não forem válidos
            {
                return View(pessoa); // Retorna os campos que o cliente precisa preencher
            }
        }
        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id) // Busca pelo o ID e retorna os dados do ID para um futuro UPDATE
        {
            var pessoa = respository.GetById(id); // atribui a pessoa o id requisitado nos args; o id foi passado por um @ActionLink no index.cshtml

            if (pessoa == null) // Se o id equivaler a null redireciona para Error 404
            {

                return NotFound(); // Redireciona o Cliente Para Error 404
            }

            return View(pessoa); // Mostra os elementos do objeto pessoa
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        public ActionResult Edit(Pessoa pessoa) // Atualiza os dados *caso forem validos* e redireciona para o index
        {
            if (ModelState.IsValid)  // ModelState.IsValid, retorna um valor de true ou false, que por sua vez determina se a entrada de dados possue valor
            {
                respository.Update(pessoa); // repository.Save é um metódo que cria de um objeto recebido em dados para a tabela
                return RedirectToAction("Index"); // RedirectToAction("Index) Redireciona o Client a Página Index
            }
            else
            {
                return View(pessoa); // Retorna com mensagens nos campos que o cliente precisa preencher
            }
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id) // Recebe o id, deleta ele do banco de dados e faz a leitura do banco de dados novamente
        {
            respository.DeleteById(id); // respository.DeleteByID é método que deleta do banco de dados com o id como argumento
            return Json(respository.GetAll()); // Json(respository.GetAll()); Faz a releitura do banco de dados
        }
    }
}
