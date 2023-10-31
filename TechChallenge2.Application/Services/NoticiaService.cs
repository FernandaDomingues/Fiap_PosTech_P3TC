using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Entities.Response;
using TechChallenge2.Domain.Interfaces.Repositories;
using TechChallenge2.Domain.Interfaces.Services;

namespace TechChallenge2.Application.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly IMapper _mapper;
        private readonly INoticiaRepository _noticiaRepository;


        public NoticiaService(IMapper mapper, INoticiaRepository noticiaRepository)
        {
            _mapper = mapper;
            _noticiaRepository = noticiaRepository;
        }
        public async Task<Noticia> Create(Noticia noticiaDTO)
        {
            var noticia = _mapper.Map<Noticia>(noticiaDTO);

            return await _noticiaRepository.Create(noticia);
        }

        public async Task<Noticia> Get(int id)
        {

            return await _noticiaRepository.Get(id);
            
        }

        public async Task<List<Noticia>> Get()
        {
            return await _noticiaRepository.Get();
        }

        public async Task<BaseResponse> Remove(int id)
        {
            var isValid = await _noticiaRepository.Remove(id);

            BaseResponse baseResponse = null;

            if (isValid)
            {
                baseResponse = (new BaseResponse
                {
                    Message = "Notícia deletada com sucesso!",
                    Success = true,
                    Errors = null
                });
            }
            else
            {
                baseResponse = (new BaseResponse
                {
                    Message = "Notícia não encontrada",
                    Success = false,
                    Errors = null
                });
            }

            return baseResponse;
        }

        public async Task<BaseResponse> GetById(int id)
        {
            var noticia = await _noticiaRepository.GetById(id);
            return await ReturnResponse("Notícia localizada com sucesso!", noticia);
            
        }

        public async Task<BaseResponse> Update(Noticia noticiaDto)
        {
            var noticia = _mapper.Map<Noticia>(noticiaDto);
            var noticiaAlt = await _noticiaRepository.Update(noticia);

            return await ReturnResponse("Notícia alterada com sucesso!", noticiaAlt);

        }

        public async Task<BaseResponse> ReturnResponse(string SuccessMessage, Noticia noticia)
        {
            BaseResponse baseResponse = null;

            if (noticia != null)
            {
                baseResponse = (new BaseResponse
                {
                    Message = SuccessMessage,
                    Success = true,
                    Errors = null,
                    Data = noticia
                });
            }
            else
            {
                baseResponse = (new BaseResponse
                {
                    Message = "Notícia não encontrada",
                    Success = false,
                    Errors = null
                });
            }

            return baseResponse;
        }

    }
}
