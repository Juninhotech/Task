using Task.DTO;
using Task.Model;

namespace Task.IRepository
{
    public interface IQuestionTypes
    {
        Task<DataResponse> CreateQuestionType(QuestionTypeDTO type);
        //Task<string> CreateQuestion(QuestionDTO question);
        Task<DataResponse<QuestionType>> GetQuestionType(string type);
    }
}
