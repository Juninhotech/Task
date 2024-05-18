using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.DTO;
using Task.IRepository;
using Task.Model;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestions _question;

        public QuestionController(IQuestions questions)
        {
            _question = questions;
        }

        [HttpPost("CreateQuestion")]
        public async Task<DataResponse> CreateNewQuestionType(QuestionDTO type)
        {
            var query = await _question.CreateQuestion(type);
            return query;
        }

        [HttpPut("EditQuestion")]
        public async Task<DataResponse> EditQuestion(EditQuestionDTO edit)
        {
            var query = await _question.EditQuestion(edit);
            return query;
        }

        [HttpGet("GetByType")]
        public async Task<DataResponse<Questions>> GetByType(string type)
        {
            var query = await _question.GetQuestionByType(type);
            return query;
        }
    }
}
