using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TechChallenge2.Application.Services;
using TechChallenge2.Data.Repositories;
using TechChallenge2.Domain.Interfaces.Repositories;
using Moq;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Interfaces.Services;
using AutoMapper;
using Xunit.Abstractions;
using TechChallenge.Api.IoC;
using TechChallenge2.Domain.Entities.Request;
using TechChallenge2.Identity.Data.Dtos;
using TechChallenge2.Identity.Models;
using TechChallenge2.Domain.Entities.Response;
using TechChallenge2.Domain.Exceptions;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Tests
{
    public class NoticiaTest
    {
        NoticiaService _noticiaService;
        Mock<INoticiaRepository> _noticiaRepositoryMock;
        public ITestOutputHelper SaidaConsoleTeste;
        public IMapper _mapper;

        List<Noticia> _noticias = new List<Noticia>()
            {
                new Noticia(1, "Era uma vez", "Texto do contéudo", DateTime.Now, "David Lima"), 
                new Noticia(2, "Era uma outra vez","Texto do contéudo", DateTime.Now,"Fernanda Lima"),
                new Noticia(3, "Assim que acabou","Texto do contéudo", DateTime.Now,"Diego Lima")
            };

        public NoticiaTest(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;

            _noticiaRepositoryMock = new Mock<INoticiaRepository>();
            

            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NoticiaRequest, Noticia>().ReverseMap();
            });
            _mapper = autoMapperConfig.CreateMapper();


            _noticiaService = new NoticiaService(_mapper, _noticiaRepositoryMock.Object);
        }


        [Fact(DisplayName = "Resgatar todas as noticias")]
        public async void ResgatarTodasAsNoticias()
        {
            //Arrange
            _noticiaRepositoryMock.Setup(x => x.Get()).Returns(Task.FromResult(_noticias));
            var noticias = new List<Noticia>();

            //Act
            noticias = await _noticiaService.Get();

            //Assert
            Assert.Equal(3, noticias.Count());
        }

        [Theory(DisplayName = "Resgatar uma notícia pelo id")]
        [InlineData("David Lima", 1)]
        [InlineData("Fernanda Lima", 2)]
        [InlineData("Diego Lima", 3)]
        public async void ResgatarNoticiaPeloId(string autor, int id)
        {
            //Arrange
            _noticiaRepositoryMock.Setup(x => x.GetById(id)).Returns(Task.FromResult(_noticias.FirstOrDefault(x => x.Id == id)));

            //Act
            var noticia1 = await _noticiaService.GetById(id);
            Noticia teste =  noticia1.Data;

            //Assert
            Assert.Equal(autor, teste.Autor);


        }

        [Fact(DisplayName = "Criando uma noticia com sucesso")]
        public async void CriarNoticiaComSucesso()
        {
            //Arrange
            Noticia novaNoticia = new Noticia(4, "Assim que começou", "Textinho do contéudo", DateTime.Now, "Arthur Santos");
            _noticiaRepositoryMock.Setup(x => x.Create(novaNoticia)).Returns(Task.FromResult(novaNoticia));

            //Act
            var resultado = await _noticiaService.Create(novaNoticia);

            //Assert
            Assert.NotNull(resultado);
        }

        [Fact(DisplayName = "Criando uma noticia com erro titulo nulo")]
        public async void CriarNoticiaComErroTituloNulo()
        {
            //Arrange
            var noticiaRequest = new Noticia(null, "Textinho do contéudo", DateTime.Now, "Autor da Silva");

            //Act
            try
            {
                Validator.ValidateObject(noticiaRequest, new ValidationContext(noticiaRequest), true);
            }
            catch(Exception ex)
            {
                //Assert
                Assert.Equal("O campo Titulo é obrigatório.", ex.Message);
            }
        }

        [Fact(DisplayName = "Criando uma noticia com erro conteudo nulo")]
        public async void CriarNoticiaComErroConteudoloNulo()
        {
            //Arrange
            var noticiaRequest = new Noticia("Titulo de teste", string.Empty, DateTime.Now, "Autor da Silva");
            

            //Act
            try
            {
                Validator.ValidateObject(noticiaRequest, new ValidationContext(noticiaRequest), true);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Equal("O campo Conteudo é obrigatório.", ex.Message);
            }
        }

        [Fact(DisplayName = "Alterar Nome do autor")]
        public async void AlterarNomeDoAutor()
        {
            //Arrange
            Noticia noticiaAlterada = new Noticia(3, "Assim que acabou", "Texto do contéudo", DateTime.Now, "Diogo Otavio");
            _noticiaRepositoryMock.Setup(x => x.Update(noticiaAlterada)).Returns(Task.FromResult(noticiaAlterada));

            //Act
            var noticiaDepois = await _noticiaService.Update(noticiaAlterada);
            Noticia teste = noticiaDepois.Data;

            //Assert
            Assert.Equal("Diogo Otavio", teste.Autor);
        }

        [Fact(DisplayName = "Deletar uma notícia pelo id")]
        public async void DeletarNoticiaPeloId()
        {
            //Arrange
            _noticiaRepositoryMock.Setup(x => x.Remove(1)).Returns(Task.FromResult(true));

            //Act
            var resultado = await _noticiaService.Remove(1);

            //Assert
            Assert.Equal("Notícia deletada com sucesso!", resultado.Message);
        }
    }
}
