namespace BaşarSoftDeneme.Models
{
    public class Response
    {
        public bool Status { get; internal set; }
        public string Message { get; set; }
    }
    public class Response<T> : Response
    {
        public T Value { get; set; }
    }


}
