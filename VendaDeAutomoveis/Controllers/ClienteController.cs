using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Filters;
using VendaDeAutomoveis.Repository;
using VendaDeAutomoveis.Repository.ConnectionContext.Context;
using VendaDeAutomoveis.Repository.ConnectionContext;

namespace VendaDeAutomoveis.Controllers
{
    [AutorizacaoFilter]
    public class ClienteController : Controller
    {
        private ClienteRepository clienteRepository;
        private VendaRepository vendaRepository;
        private EnderecoRepository enderecoRepository;

        public ClienteController(ClienteRepository clienteRepository, VendaRepository vendaRepository, EnderecoRepository enderecoRepository)
        {
            this.clienteRepository = clienteRepository;
            this.vendaRepository = vendaRepository;
            this.enderecoRepository = enderecoRepository;
        }

        public ActionResult Index()
        {
            IList<Cliente> clientes = clienteRepository.ObterTodos();
            return View(clientes);
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
            if (ModelState.IsValid)
            {
                //enderecoDAO.AdicionarEndereco(endereco);
            }

            return RedirectToAction("Index");
        }

        public ActionResult AdicionarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var cpfExistente = clienteRepository.VerificarCPFExistente(cliente.CPF);

                if (cpfExistente == null)
                {
                    clienteRepository.Adicionar(cliente);
                    return View("CadastrarEndereco", new { IdCliente = cliente.IdCliente});
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

        public ActionResult EditarCliente(Guid id)
        {
            var cliente = clienteRepository.ObterPorId(id);
            return View(cliente);
        }

        public ActionResult EditarClientes(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                clienteRepository.Editar(cliente);
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditarCliente", cliente);
            }
        }
    }
}