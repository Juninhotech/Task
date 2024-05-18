using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Task.DTO;
using Task.IRepository;
using Task.Model;

namespace Task.Repository
{
    public class QuestionTypeService : IQuestionTypes
    {

        private readonly CosmosClient _client;
        private readonly IConfiguration _configuration;
        private readonly Container _container;
        DataResponse<QuestionType> res;
        DataResponse ress;

        public QuestionTypeService(IConfiguration configuration, CosmosClient client)
        {
            _client = client;
            _configuration = configuration;
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var taskContainerName = "QuestionType";
            _container = client.GetContainer(databaseName, taskContainerName);
            res = new DataResponse<QuestionType>();
            ress = new DataResponse();
        }
        public async Task<DataResponse> CreateQuestionType(QuestionTypeDTO type)
        {
            try
            {
                var question = new QuestionType
                {
                    Id = Guid.NewGuid().ToString(),
                    type = type.type

                };
                var response = await _container.CreateItemAsync(question);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ress.ErrorMessage = "An error occur while creating new question";
                    ress.ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest).ToString();
                }

                else
                {
                    ress.ResponseMessage = "Created Successful";
                    ress.ResponseCode = ((int)response.StatusCode).ToString();
                };
            }
            catch (CosmosException ex)
            {

                ress.ErrorMessage = ex.Message;
            }
            return ress;
        }

        public async Task<DataResponse<QuestionType>> GetQuestionType(string type)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<QuestionType>().Where(t => t.type == type).ToFeedIterator();
                var qq = new List<QuestionType>();


                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();

                    if (response.Count == 0)
                    {
                        res.ErrorMessage = "No type found";
                        res.ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest).ToString();
                        res.data = null;
                    }

                    else
                    {
                        qq.AddRange(response);
                        res.ResponseMessage = "Success";
                        res.ResponseCode = ((int)response.StatusCode).ToString();
                        res.data = qq;

                    }

                }
            }
            catch (CosmosException ex)
            {

                res.ErrorMessage = ex.Message;
            }
            return res;
        }
    }
}
