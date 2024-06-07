global using System.Reflection;
global using System.Data;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Data.SqlClient;
global using Dapper;
global using FluentMigrator;
global using FluentMigrator.Runner;
global using FluentMigrator.Builders.Create.Table;
global using Microsoft.EntityFrameworkCore;

global using SistemaDeCadastro.Domain.Entidades;
global using SistemaDeCadastro.Domain.Enum;
global using SistemaDeCadastro.Domain.ValueObjects;
global using SistemaDeCadastro.Domain.Extension;
global using SistemaDeCadastro.Domain.Repositorios;
global using SistemaDeCadastro.Domain.Repositorios.Cadastro;
global using SistemaDeCadastro.Domain.Repositorios.Pessoa;

global using SistemaDeCadastro.Infrastructure.AcessoRepositorio;
global using SistemaDeCadastro.Infrastructure.AcessoRepositorio.Repositorio;
global using SistemaDeCadastro.Infrastructure.AcessoRepositorio.Map;
global using SistemaDeCadastro.Infrastructure.AcessoRepositorio.Queries;




