using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge2.Domain.Entities.Request
{
    public class NoticiaRequest
    {
        [Required(ErrorMessage = "O campo título não pode ser nulo.")]
        [MinLength(3, ErrorMessage = "O título da notícia deve ter no mínimo 3 caracteres.")]
        [MaxLength(255, ErrorMessage = "O título da notícia deve ter no máximo 255 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo conteúdo não pode ser nulo.")]
        [MinLength(3, ErrorMessage = "O conteúdo da notícia deve ter no mínimo 3 caracteres.")]
        public string Conteudo { get; set; }

        [Required(ErrorMessage = "O campo de data da publicação é obrigatório.")]

        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "O campo autor não pode ser nulo.")]
        [MinLength(3, ErrorMessage = "O nome do autor da notícia ter no mínimo 3 caracteres.")]
        [MaxLength(255, ErrorMessage = "O nome do autor da notícia deve ter no máximo 255 caracteres.")]

        public string Autor { get; set; }
    }
}
