﻿using System;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Repository.ConnectionContext.Context;
using static VendaDeAutomoveis.Entidades.Cliente;

namespace VendaDeAutomoveis.Repository.ConnectionContext.Adapters
{
    public static class ClienteAdapter
    {
        public static Cliente ToDomain(this GDC_Clientes dbClientes)
        {
            if (dbClientes == null)
                return null;
            
            return new Cliente
            {
                IdCliente = Guid.Parse(dbClientes.Id),
                Nome = dbClientes.Nome,
                CPF = dbClientes.CPF.ToString(),
                RG = dbClientes.RG,
                Email = dbClientes.Email,
                TipoDoCliente = TipoCliente.Comum,
                IdEndereco = Guid.Parse(dbClientes.IdEndereco)
            };
        }

        public static GDC_Clientes ToDbEntity(this Cliente domain)
        {
            if (domain == null)
                return null;

            return new GDC_Clientes
            {
                Id = domain.IdCliente.ToString(),
                Nome = domain.Nome,
                CPF = domain.CPF,
                RG = domain.RG,
                Email = domain.Email,
                Tipo = Convert.ToString(TipoCliente.Comum),
                IdEndereco = domain.IdEndereco.ToString()
            };
        }
    }
}