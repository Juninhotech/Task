using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.ComponentModel;
using Task.DTO;
using Task.IRepository;
using Task.Model;
using Container = Microsoft.Azure.Cosmos.Container;

namespace Task.Repository
{
    public class QuestionServices : IQuestions
    {
        private readonly CosmosClient _client;
        private readonly IConfiguration _configuration;
        private readonly Container _container;
        DataResponse res;
        DataResponse<Questions> ress;

        public QuestionServices(IConfiguration configuration, CosmosClient client)
        {
            _client = client;
            _configuration = configuration;
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var taskContainerName = "Questions";
            _container = client.GetContainer(databaseName, taskContainerName);
            res = new DataResponse();
            ress = new DataResponse<Questions>();
        }
        public async Task<DataResponse>CreateQuestion(QuestionDTO question)
        {
            try
            {
                var query = new Questions
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = question.Type,
                    Question = question.Question,
                };

                var response = await _container.CreateItemAsync(query);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    res.ErrorMessage = "An error occur while creating new question";
                    res.ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest).ToString();
                }

                else
                {
                    res.ResponseMessage = "Created Successful";
                    res.ResponseCode = ((int)response.StatusCode).ToString();
                };
            }
            catch (CosmosException ex)
            {
                res.ErrorMessage = ex.Message;

            }
            return res;
        }

        public async Task<DataResponse> EditQuestion(EditQuestionDTO edit)
        {
            try
            {

                var existingItemResponse = await _container.ReadItemAsync<EditQuestionDTO>(edit.id, new PartitionKey(edit.type));

                if (existingItemResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var existingIterm = existingItemResponse.Resource;

                    existingIterm.Question = edit.Question;




                    var query = await _container.ReplaceItemAsync(existingIterm, edit.id);

                    if (query.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        res.ErrorMessage = "An error occur while trying to edit";
                        res.ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest).ToString();

                    }

                    else
                    {
                        res.ResponseMessage = "Edited successfully";
                        res.ResponseCode = ((int)query.StatusCode).ToString();

                    }


                }

                else
                {
                    res.ErrorMessage = "No record found";
                    res.ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest).ToString();
                }


            }
            catch (CosmosException ex)
            {
                res.ErrorMessage = ex.Message;
            }
            return res;
        }

        public async Task<DataResponse<Questions>> GetQuestionByType(string type)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<Questions>().Where(t => t.Type == type).ToFeedIterator();
                var qq = new List<Questions>();


                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();

                    if (response.Count == 0)
                    {
                        ress.ErrorMessage = "No Question found";
                        ress.ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest).ToString();
                        ress.data = null;
                    }

                    else
                    {
                        qq.AddRange(response);
                        ress.ResponseMessage = "Success";
                        ress.ResponseCode = ((int)response.StatusCode).ToString();
                        ress.data = qq;

                    }

                }
            }
            catch (CosmosException ex)
            {

                ress.ErrorMessage = ex.Message;
            }
            return ress;
        }
    }
}
