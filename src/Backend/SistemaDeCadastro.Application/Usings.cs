global using FluentValidation;
global using Microsoft.Extensions.DependencyInjection;

global using SistemaDeCadastro.Domain.Entidades;
global using SistemaDeCadastro.Domain.Enum;
global using SistemaDeCadastro.Domain.Repositorios;
global using SistemaDeCadastro.Domain.Repositorios.Cadastro;
global using SistemaDeCadastro.Domain.Repositorios.Pessoa;
global using SistemaDeCadastro.Domain.ValueObjects;
global using SistemaDeCadastro.Domain.Servicos.ViaCep;

global using SistemaDeCadastro.Application.UseCases.Cadastro;
global using SistemaDeCadastro.Application.UseCases.Cadastro.Registrar;
global using SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarTodos;

global using SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarTodos;
global using SistemaDeCadastro.Application.UseCases.Pessoa.Registrar;


global using SistemaDeCadastro.Communication.Requisicoes.Cadastro;
global using SistemaDeCadastro.Communication.Respostas.Cadastro;
global using SistemaDeCadastro.Communication.Requisicoes.Pessoa;
global using SistemaDeCadastro.Communication.Respostas.Pessoa;

global using SistemaDeCadastro.Exceptions;
global using SistemaDeCadastro.Exceptions.ExceptionsBase;
