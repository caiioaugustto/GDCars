﻿using System;
using System.Web.Mvc;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Filters;
using VendaDeAutomoveis.Repository;
using VendaDeAutomoveis.Services;

namespace VendaDeAutomoveis.Controllers
{
    //ModuloCliente (Claims/Permissões) - AD, ED, LT, VI, EX

    [RoutePrefix("acesso-ao-sistema")]
    public class LoginController : Controller
    {
        //private GDCarsContext db = new GDCarsContext();
        private LoginRepository loginRepository;
        public LoginController(LoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autenticar(string email, string senha)
        {
            if (ModelState.IsValid)
            {
                var senhaCripto = Criptografia.CriptografaMd5(senha);

                var validarAcesso = loginRepository.AutenticarAcesso(email, senhaCripto);

                if (validarAcesso == null)
                {
                    ModelState.AddModelError("login.Invalido", "Usuário ou senha Inválido, tente novamente!");
                }
                else if(validarAcesso.TipoAcesso == NivelAcesso.Bloqueado)
                {
                    ModelState.AddModelError("login.Invalido", "Seu usuário está bloqueado, por favor, contate o administrador!");
                }
                else
                {
                    SessionManager.UsuarioLogado = validarAcesso;
                    System.Web.Security.FormsAuthentication.SetAuthCookie(validarAcesso.Email, true);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("login.Invalido", "Preencha o campo com login e senha!");
            }
           
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return View("Index");
        }

        [ClaimsAuthorize("CriarAcesso", "CA")]
        public ActionResult CriarAcesso()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-acesso")]
        [ClaimsAuthorize("CriarAcesso", "CA")]
        [ValidateAntiForgeryToken]
        public ActionResult CriarAcesso([Bind(Include = "Id,Nome,SobreNome,Email,Senha,ConfirmarSenha")] Login login)
        {
            if (ModelState.IsValid)
            {
                var verificarExistenciaEmail = false;
                //var verificarExistenciaEmail = VerificarEmailExistente.ValidarEmailExistente(loginDAO, login);

                if (verificarExistenciaEmail)
                {
                    ModelState.AddModelError("login.invalido", "Esse e-mail já está sendo utilizado, por favor, utilize um e-mail alternativo");
                    return View(login);
                }
                else
                {
                    login.Senha = Criptografia.CriptografaMd5(login.Senha);
                    loginRepository.Adicionar(login);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(login);
            }
        }

        [ClaimsAuthorize("ListarAcesso", "LTA")]
        public ActionResult ListarAcesso()
        {
            var usuarios = loginRepository.ObterTodos();
            return View(usuarios);
        }

        public void BloquearAcesso(Guid id)
        {
            loginRepository.BloquearAcesso(id);
        }
    }
}