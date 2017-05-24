using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using VendaDeAutomoveis.DAO;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Factory.Base.Upload;
using VendaDeAutomoveis.Filters;

namespace VendaDeAutomoveis.Controllers
{
    [AutorizacaoFilter]
    public class ProdutoController : Controller
    {
        private ProdutoDAO produtoDAO;

        public ProdutoController(ProdutoDAO produtoDAO)
        {
            this.produtoDAO = produtoDAO;
        }

        public ActionResult FormularioCadastro()
        {
            return View();
        }

        public ActionResult AdicionarProduto(Produtos produto)
        {
            foreach (string nomeArquivo in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[nomeArquivo];
                UploadArquivoFactory.Upload(file, nomeArquivo);
            }

            if (ModelState.IsValid)
            {
                produtoDAO.AdicionarProduto(produto);
                return RedirectToAction("Index");
            }
            else
            {
                return View("FormularioCadastro", produto);
            }
        }

        public ActionResult EditarProduto(int idProduto)
        {
            var produto = produtoDAO.PegarProduto(idProduto);
            return View(produto);
        }

        public ActionResult EditarProdutos(Produtos produto)
        {
            if (ModelState.IsValid)
            {
                produtoDAO.EditarProduto(produto);
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditarProduto", produto);
            }
        }

        public ActionResult Excluir(int IdProduto)
        {
            //produtoDAO.Excluir(IdProduto);
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            IList<Produtos> produto = produtoDAO.Lista();
            return View(produto);
        }

        public ActionResult Teste()
        {
            return View();
        }

        public ActionResult VerImagem()
        {
            //Adicione quantas extensões você desejar!
            //List<String> oListTiposImagens = new List<string>();
            //oListTiposImagens.Add("*.gif");
            //oListTiposImagens.Add("*.png");
            //oListTiposImagens.Add("*.jpg");

            //var ImagemNome = uploadArquivoDAO.pegarNomeImagemPorIdProduto(idProduto);

            var nomecompleto = System.IO.Path.Combine(ConfigurationManager.AppSettings["caminhoRepositorioDeArmazenamento"], "uno" + ".png");

            var arquivoInfo = new FileInfo(nomecompleto);
            
            //string[] imagens2 = Directory.GetFiles(caminhoDiretorio, "*.png");
            
            return base.File(nomecompleto, "uno.png");
        }

        public ActionResult SalvarArquivo()
        {
            var fazerUpload = false;

            foreach (string nomeArquivo in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[nomeArquivo];
                fazerUpload = UploadArquivoFactory.Upload(file, nomeArquivo);
            }

            if (fazerUpload)
                return Content("ok");
            else
                return Content("Erro");

            //var caminhoDiretorio = ConfigurationManager.AppSettings["caminhoRepositorioDeArmazenamento"].ToString();//Config fora do sistema (Web config)

            //string nomeA = string.Empty;

            //Guid arquivoGuid;
            //arquivoGuid = Guid.NewGuid();
            //try
            //{
            //    foreach (string nomeArquivo in Request.Files)
            //    {
            //        HttpPostedFileBase file = Request.Files[nomeArquivo];
            //        nomeA = file.FileName;
            //        if (file != null && file.ContentLength > 0)
            //        {
            //            var nomeArquivoCarregado = Path.GetFileName(file.FileName);

            //            bool existenciaDiretorio = System.IO.Directory.Exists(caminhoDiretorio);
            //            if (!existenciaDiretorio)
            //            {
            //                System.IO.Directory.CreateDirectory(caminhoDiretorio);
            //            }
            //            var caminhoArquivo = string.Empty;
            //            var extensao = System.IO.Path.GetExtension(nomeA);

            //            caminhoArquivo = string.Format("{0}\\{1}", caminhoDiretorio, arquivoGuid + ".png");

            //            file.SaveAs(caminhoArquivo);

            //            string recebendoDetalhes = arquivo.Detalhes;

            //            arquivo.Detalhes = "<br/>" + "<ol/>" + " - " + nomeArquivoCarregado + recebendoDetalhes;
            //        }

            //        arquivo.Data = DateTime.Now;

            //        //uploadArquivoDAO.AdicionarArquivo(arquivo);
            //    }
            //    return Content("Ok");
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Message = "Erro em Salvar o Arquivo" });
            //}
        }
    }
}