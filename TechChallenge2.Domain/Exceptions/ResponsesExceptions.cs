using TechChallenge2.Domain.Entities.Response;

namespace TechChallenge2.Domain.Exceptions
{
    public static class ResponsesExceptions
    {
        public static BaseResponse ApplicationErrorMessage()
        {
            return new BaseResponse
            {
                Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
                Success = false,
                Errors = null
            };
        }

        public static BaseResponse DomainErrorMessage(string message)
        {
            return new BaseResponse
            {
                Message = message,
                Success = false,
                Errors = null
            };
        }

        public static BaseResponse DomainErrorMessage(string message, List<string> errors)
        {
            return new BaseResponse
            {
                Message = message,
                Success = false,
                Errors = errors
            };
        }

        public static BaseResponse UnauthorizedErrorMessage()
        {
            return new BaseResponse
            {
                Message = "A combinação de login e senha está incorreto!",
                Success = false,
                Errors = null
            };
        }
    }
}
