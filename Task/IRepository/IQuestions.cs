using Task.DTO;
using Task.Model;

namespace Task.IRepository
{
    public interface IQuestions
    {
        Task<DataResponse> CreateQuestion(QuestionDTO question);
        Task<DataResponse> EditQuestion(EditQuestionDTO edit);
        Task<DataResponse<Questions>> GetQuestionByType(string type);

    }
}
