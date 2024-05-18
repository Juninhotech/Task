using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.DTO;
using Task.IRepository;
using Task.Model;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionTypes _question;

        public QuestionTypeController(IQuestionTypes question)
        {
            _question = question;
        }

        [HttpPost("CreateQuestionType")]
        public async Task<DataResponse> CreateNewQuestionType(QuestionTypeDTO type)
        {
            var query = await _question.CreateQuestionType(type);
            return query;
        }

        [HttpGet("Get")]
        public async Task<DataResponse<QuestionType>> GetAll(string type)
        {
            var query = await _question.GetQuestionType(type);
            return query;
        }
    }
}
