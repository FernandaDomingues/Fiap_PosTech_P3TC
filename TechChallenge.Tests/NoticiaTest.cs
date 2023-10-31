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
using static System.Net.Mime.MediaTypeNames;

namespace TechChallenge.Tests
{
    public class NoticiaTest
    {
        NoticiaService _noticiaService;
        Mock<INoticiaRepository> _noticiaRepositoryMock;
        public ITestOutputHelper SaidaConsoleTeste;
        private readonly IMapper _mapper;

        List<Noticia> _noticias = new List<Noticia>()
            {
                new Noticia(1, "Era uma vez", "Texto do contéudo", DateTime.Now, "David Lima"), 
                new Noticia(2, "Era uma outra vez","Texto do contéudo", DateTime.Now,"Fernanda Lima"),
                new Noticia(3, "Assim que acabou","Texto do contéudo", DateTime.Now,"Diego Lima")
            };


        public NoticiaTest( ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;

            var teste = new Noticia(1, "Assim que acabou", "Texto do contéudo", DateTime.Now, "Diogo Otavio");
            _noticiaRepositoryMock = new Mock<INoticiaRepository>();
            _noticiaRepositoryMock.Setup(x => x.Get()).Returns(Task.FromResult(_noticias));
            _noticiaRepositoryMock.Setup(x => x.Remove(1)).Returns(Task.FromResult(true));

            _noticiaRepositoryMock.Setup(x => x.Update(teste)).Returns(Task.FromResult(teste));


            _noticiaService = new NoticiaService(_mapper, _noticiaRepositoryMock.Object);
        }


        [Fact(DisplayName = "Resgatar todas as noticias")]
        public async void ResgatarTodasAsNoticias()
        {
            //Arrange
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

            _noticiaRepositoryMock.Setup(x => x.GetById(id)).Returns(Task.FromResult(_noticias.FirstOrDefault(x => x.Id == id)));


            var noticia1 = await _noticiaService.GetById(id);

            Noticia teste =  noticia1.Data;

            Assert.Equal(autor, teste.Autor);


        }

        //[Fact(DisplayName ="Alterar Nome do autor")]
        //public async void AlterarNomeDoAutor()
        //{
        //    Noticia noticiaAlterada = new Noticia(3, "Assim que acabou", "Texto do contéudo", DateTime.Now, "Diogo Otavio");


        //    var noticiaDepois = await _noticiaService.Update(noticiaAlterada);

            

        //    Noticia teste = noticiaDepois.Data;


        //    Assert.Equal("Diogo Otavio", teste.Autor);
        //}

        [Fact(DisplayName = "Deletar uma notícia pelo id")]
        public async void DeletarNoticiaPeloId()
        {
            var resultado = await _noticiaService.Remove(1);

            Assert.Equal("Notícia deletada com sucesso!", resultado.Message);
        }
    }
}
