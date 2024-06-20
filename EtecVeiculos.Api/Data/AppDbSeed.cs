using EtecVeiculos.Api.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EtecVeiculos.Api.Data;
public class AppDbSeed
{
    public AppDbSeed(ModelBuilder modelBuilder)
    {
        #region TipoVeiculo
        List<TipoVeiculo> tipoVeiculos = [
            new(){
                Id = 1,
                Name = "Moto"
            },
            new(){
                Id = 2,
                Name = "Carro"
            },
            new(){
                Id = 3,
                Name = "Caminhão"
            }
        ];
        modelBuilder.Entity<TipoVeiculo>().HasData(tipoVeiculos);
        #endregion

        #region Marcas
        List<Marca> marca = [
            new(){
                Id = 1,
                Nome = "Ferrari"
            },
            new(){
                Id = 2,
                Nome = "Lamborghini"
            },
            new(){
                Id = 3,
                Nome = "Porsche"
            }
        ];
        #endregion

        #region Modelo
        List<Modelo> modelos = new() {
            new() {
                Id = 1,
                Nome = "Spider",
                MarcaId = 1 
            },
            new() {
                Id = 2,
                Nome = "Huracán",
                MarcaId = 1 
            },
            new() {
                Id = 3,
                Nome = "Cayenne",
                MarcaId = 2 
            },
        };
        modelBuilder.Entity<Modelo>().HasData(modelos);
        #endregion
    }
}