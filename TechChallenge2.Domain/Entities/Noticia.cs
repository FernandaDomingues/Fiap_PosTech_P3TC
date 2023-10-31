using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge2.Domain.Entities;
using System.Security.Principal;

namespace TechChallenge2.Domain.Entities
{
    [Table("Noticia")]
    public class Noticia : Base
    {
        public Noticia()
        {
            
        }

        [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O campo Titulo deve ter entre 3 e 255 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Conteudo é obrigatório.")]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "O campo Conteudo deve ter no minimo caracteres.")]
        public string Conteudo { get; set; }

        [Required(ErrorMessage = "O campo DataPublicacao é obrigatório.")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo DataPublicacao deve ser uma data e hora válida.")]
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "O campo Autor é obrigatório.")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O campo Autor deve ter entre 3 e 255 caracteres.")]
        public string Autor { get; set; }


        public Noticia(string titulo, string conteudo, DateTime dataPublicacao, string autor)
        {
            Titulo = titulo;
            Conteudo = conteudo;
            DataPublicacao = dataPublicacao;
            Autor = autor;
        }

        public Noticia(int id, string titulo, string conteudo, DateTime dataPublicacao, string autor)
        {
            Id = id;
            Titulo = titulo;
            Conteudo = conteudo;
            DataPublicacao = dataPublicacao;
            Autor = autor;
        }
    }
}
