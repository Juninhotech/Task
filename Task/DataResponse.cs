namespace Task
{
    public class DataResponse
    {
        public string ErrorMessage { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }

    }


    public class DataResponse<T>
    {
        public string ErrorMessage { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public List<T> data { get; set; }

    }
}
