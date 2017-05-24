using System.Collections.Generic;
using System.Web.Mvc;
using VendaDeAutomoveis.DAO;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Filters;

namespace VendaDeAutomoveis.Controllers
{
    [AutorizacaoFilter]
    public class ClienteController : Controller
    {
        private ClienteDAO clienteDAO;
        private VendaDAO vendaDAO;
        private EnderecoDAO enderecoDAO;

        public ClienteController(ClienteDAO clienteDAO, VendaDAO vendaDAO, EnderecoDAO enderecoDAO)
        {
            this.clienteDAO = clienteDAO;
            this.vendaDAO = vendaDAO;
            this.enderecoDAO = enderecoDAO;
        }

        public ActionResult Index()
        {
            IList<Clientes> clientes = clienteDAO.Lista();
            return View(clienteDAO.Lista());
        }

        public ActionResult FormularioCadastro()
        {
            return View();
        }

        //public ActionResult CadastrarEndereco()
        //{
        //    return View();
        //}

        public ActionResult AdicionarEndereco(Endereco endereco)
        {
            if(ModelState.IsValid)
            {
                enderecoDAO.AdicionarEndereco(endereco);
            }

            return RedirectToAction("Index");
        }

        public ActionResult AdicionarCliente(Clientes cliente)
        {
            if (ModelState.IsValid )
            {
               bool cpfExistente = clienteDAO.VerificarCPFExistente(cliente);

               if (cpfExistente == false)
               {
                    clienteDAO.AdicionarCliente(cliente);
                    return View("CadastrarEndereco", new { IdCliente = cliente.IdCliente });
                    //return RedirectToAction("Index");
                }
               else
               {
                   ModelState.AddModelError("CPF", "O CPF já existe no sistema!");
                   return View("FormularioCadastro", cliente);
               }            
            }
            else
            {
                return View("FormularioCadastro", cliente);
            }
        }

        public ActionResult EditarCliente(int idCliente)
        {
            var cliente = clienteDAO.PegarCliente(idCliente);
            return View(cliente);
        }

        public ActionResult EditarClientes(Clientes cliente)
        {        
            if (ModelState.IsValid)
            {
                clienteDAO.EditarCliente(cliente);
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditarCliente", cliente);
            }     
        }

        
    }
}