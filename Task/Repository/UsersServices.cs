using Microsoft.Azure.Cosmos;
using Task.DTO;
using Task.IRepository;
using Task.Model;

namespace Task.Repository
{
    public class UsersServices : IUsers
    {
        private readonly CosmosClient _client;
        private readonly IConfiguration _configuration;
        private readonly Container _container;
        DataResponse res;
        DataResponse ress;

        public UsersServices(IConfiguration configuration, CosmosClient client)
        {
            _client = client;
            _configuration = configuration;
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var taskContainerName = "Users";
            _container = client.GetContainer(databaseName, taskContainerName);
            res = new DataResponse();
            ress = new DataResponse();
        }
        public async Task<DataResponse> Apply(UserDTO users)
        {
            try
            {
                var apply = new Users
                {
                    Id = Guid.NewGuid().ToString(),
                    Firstname = users.Firstname,
                    LastName = users.LastName,
                    UserId = $"{users.Email}",
                    Phone = users.Phone,
                    Email = users.Email,
                    Nationality = users.Nationality,
                    CurrentResidence = users.CurrentResidence,
                    IdNumber = users.IdNumber,
                    DateOfBirth = users.DateOfBirth,
                    Gender = users.Gender,
                    Questions = users.Questions,
                    //Paragraph = users.Paragraph,
                    //YesNo = users.YesNo,
                    //DropDown = users.DropDown,
                    //Date = users.Date,
                    //Number = users.Number,
                };

                var response = await _container.CreateItemAsync(apply);

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
    }
}
