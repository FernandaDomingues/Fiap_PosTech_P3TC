namespace TechChallenge2.Domain.Entities.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public BaseResponse() =>
            Errors = new List<string>();

        public BaseResponse(bool sucesso = true) : this() =>
            Success = sucesso;

        public void AdicionarErros(IEnumerable<string> erros) =>
            Errors.AddRange(erros);

    }
}
