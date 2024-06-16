global using System.Reflection;
global using System.Data;
global using System.Text.Json;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore;

global using SistemaDeCadastro.Domain.Entidades;
global using SistemaDeCadastro.Domain.Enum;
global using SistemaDeCadastro.Domain.ValueObjects;
global using SistemaDeCadastro.Domain.Repositorios;
global using SistemaDeCadastro.Domain.Repositorios.Cadastro;
global using SistemaDeCadastro.Domain.Repositorios.Pessoa;
global using SistemaDeCadastro.Domain.Servicos.ViaCep;

global using SistemaDeCadastro.Infrastructure.AcessoRepositorio;
global using SistemaDeCadastro.Infrastructure.AcessoRepositorio.Repositorio;
global using SistemaDeCadastro.Infrastructure.Servicos.ViaCep;

global using SistemaDeCadastro.Exceptions;
global using SistemaDeCadastro.Exceptions.ExceptionsBase;



