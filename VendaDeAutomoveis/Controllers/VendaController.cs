using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VendaDeAutomoveis.DAO;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Filters;
using VendaDeAutomoveis.Models;

namespace VendaDeAutomoveis.Controllers
{
    [AutorizacaoFilter]
    public class VendaController : Controller
    {
        private VendaDAO vendaDAO;
        private ClienteDAO clienteDAO;
        private ProdutoDAO produtoDAO;
        private PagamentosDAO pagamentoDAO;
        private EnderecoDAO enderecoDAO;

        public VendaController(VendaDAO vendaDAO, ClienteDAO clienteDao, ProdutoDAO produtoDAO, PagamentosDAO pagamentoDAO, EnderecoDAO enderecoDAO)
        {
            this.vendaDAO = vendaDAO;
            this.clienteDAO = clienteDao;
            this.produtoDAO = produtoDAO;
            this.pagamentoDAO = pagamentoDAO;
            this.enderecoDAO = enderecoDAO;
        }
        public ActionResult FormularioCadastro()
        {
            ViewBag.Cliente = clienteDAO.Lista();
            ViewBag.Produto = produtoDAO.Lista();
            ViewBag.FormaDePagamento = pagamentoDAO.Lista();
            return View();
        }
        public ActionResult AdicionarVenda(Vendas vendas)
        {
            if (ModelState.IsValid)
            {
                FormaDePagamento formaDePagamento = pagamentoDAO.PegarPagamento(vendas.IdFormaDePagamento);
                Clientes cliente = clienteDAO.PegarCliente(vendas.IdCliente);

                if (vendas.TipoEntrega == 0)
                {
                    ModelState.AddModelError("TipoEntrega", "Escolha um tipo de Entrega!");
                    ViewBag.Cliente = clienteDAO.Lista();
                    ViewBag.Produto = produtoDAO.Lista();
                    return View("FormularioCadastro", vendas);
                }
                //if(vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar)
                if (vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.Endereco == null || vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.Endereco.Numero == null
                    || vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.Endereco.Estado == null || vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.Endereco.Cidade == null
                    || vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.Endereco.CEP == null)
                //if (vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.Endereco == null || vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.NumeroDaCasa == null 
                //    || vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.Estado == null || vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.Cidade == null 
                //    || vendas.TipoEntrega == Enum.TipoEntrega.Domiciliar && cliente.CEP == null)
                {

                    ModelState.AddModelError("TipoEntrega", " Para concluir a compra informe seu endereço na tela de clientes");
                    ViewBag.Cliente = clienteDAO.Lista();
                    ViewBag.Produto = produtoDAO.Lista();
                    ViewBag.ExibirCampo = true;
                    return View("FormularioCadastro", vendas);
                }
                vendaDAO.AdicionarVenda(vendas);
                MudarClienteComunParaVip(cliente);
                Produtos produto = produtoDAO.PegarProduto(vendas.IdProduto);
                AumentarValorVeiculoEsportivo(vendas);
                CalcularPagamento(vendas);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Cliente = clienteDAO.Lista();
                ViewBag.Produto = produtoDAO.Lista();
                return View("FormularioCadastro");
            }
        }
        public ActionResult Index()
        {
            IList<Vendas> vendas = vendaDAO.Lista();
            return View(vendas);
        }
        public ActionResult HistoricoPedidos(HistoricoPedidosModel model)
        {
            model.Clientes = clienteDAO.Lista();
            model.Vendas = vendaDAO.BuscaPorCliente(model.IdCliente);
            return View(model);
        }
        private void MudarClienteComunParaVip(Clientes cliente)
        {
            if (cliente.TipoDoCliente == TipoCliente.Comum && vendaDAO.TotalPorCliente(cliente.IdCliente) >= 200000)
            {
                cliente.TipoDoCliente = TipoCliente.Vip;
                clienteDAO.EditarCliente(cliente);
            }
        }
        private decimal AumentarValorVeiculoEsportivo(Vendas venda)
        {
            if (venda.Produto.TipoVeiculo == TipoVeiculo.Esportivo)
            {
                string recebendoObservacao = venda.Observacoes;
                venda.Valor = (venda.Valor + 12000);
                venda.Observacoes = recebendoObservacao + " / Veiculo Esportivo : Acréscimo de R$12.000,00 referente ao período de 12 meses de seguro obrigatório";
                vendaDAO.EditarVenda(venda);
            }
            return venda.Valor;
        }
        private decimal CalcularPagamento(Vendas venda)
        {
            string recebendoObservacao = venda.Observacoes;

            if (venda.FormaDePagamento.IdFormaDePagamento == 1)
            {
                venda.Observacoes = recebendoObservacao + " Financiamento com pagamento à vista";
                //vendaDAO.EditarVenda(venda);
            }
            else if (venda.FormaDePagamento.IdFormaDePagamento == 2)
            {
                decimal parcela = venda.Valor;
                parcela = parcela / 12;
                venda.Observacoes = recebendoObservacao + " Parcelas :" + parcela.ToString("c") + " /mês";
                //vendaDAO.EditarVenda(venda);
            }
            else if (venda.FormaDePagamento.IdFormaDePagamento == 3)
            {
                decimal parcela = venda.Valor;
                parcela = parcela / 60;
                venda.Observacoes = recebendoObservacao + " Parcelas :" + parcela.ToString("c") + " /mês";
                //vendaDAO.EditarVenda(venda);
            }
            else if (venda.FormaDePagamento.IdFormaDePagamento == 4)
            {
                double juros = 0.03;
                venda.Valor = (venda.Valor * Convert.ToDecimal(juros)) + venda.Valor;
                decimal parcela = venda.Valor;
                parcela = parcela / 12;
                venda.Observacoes = recebendoObservacao + " Parcelas :" + parcela.ToString("c") + " /mês";
                //vendaDAO.EditarVenda(venda);
            }
            else if (venda.FormaDePagamento.IdFormaDePagamento == 5)
            {
                double juros = 0.05;
                venda.Valor = (venda.Valor * Convert.ToDecimal(juros)) + venda.Valor;
                decimal parcela = venda.Valor;
                parcela = parcela / 60;
                venda.Observacoes = recebendoObservacao + " Parcelas :" + parcela.ToString("c") + " /mês";
            }
            vendaDAO.EditarVenda(venda);

            return venda.Valor;
        }
        public ActionResult PegarPrecoProduto(int IdProduto)
        {
            var produto = produtoDAO.PegarProduto(IdProduto);
            return Content(produto.Valor.ToString());
        }

        public ActionResult PegarFormaDePagamento(int IdCliente)
        {
            var cliente = clienteDAO.PegarCliente(IdCliente);
            if (cliente.TipoDoCliente == TipoCliente.Vip)
            {
                var pagamento = pagamentoDAO.ListaDePagamentosVip();
                return PartialView("_ParcialViewFormaDePagamento", pagamento);
            }
            else
            {
                var pagamento = pagamentoDAO.ListaDePagamentosComum();
                return PartialView("_ParcialViewFormaDePagamento", pagamento);
            }
        }
        public ActionResult PegarEndereco(int IdCliente)
        {
            var cliente = clienteDAO.PegarCliente(IdCliente);
            return PartialView("_ParcialviewEndereco", cliente);
        }

        public ActionResult AtualizarEnderecos(int IdCliente, string Endereco, string Bairro, string NumeroDaCasa, string CEP, string Cidade, string Estado, string Complemento)
        {

            if (ModelState.IsValid)
            {
                var endereco = enderecoDAO.PegarEnderencoPorIdCliente(IdCliente);

                endereco.EnderecoNome = Endereco;
                endereco.Bairro = Bairro;
                endereco.Numero = NumeroDaCasa;
                endereco.CEP = CEP;
                endereco.Cidade = Cidade;
                endereco.Estado = Estado;
                endereco.Complemento = Complemento;

                enderecoDAO.EditarEndereco(endereco);

                //var cliente = clienteDAO.PegarCliente(IdCliente);

                //cliente.Endereco = Endereco;
                //cliente.Bairro= Bairro;
                //cliente.NumeroDaCasa = NumeroDaCasa;
                //cliente.CEP = CEP;
                //cliente.Cidade = Cidade;
                //cliente.Estado = Estado;
                //cliente.Complemento = Complemento;

                //clienteDAO.EditarCliente(cliente);

                return null;
            }
            else
            {
                return Content("Campo errado");
            }
        }
    }
}