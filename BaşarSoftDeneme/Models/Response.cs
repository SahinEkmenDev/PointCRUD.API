namespace BaşarSoftDeneme.Models
{
    public class Response<T>
    {
        public T Value { get; set; }
        public bool ValueStatus { get; set; }
        public string Message { get; set; }
    }
}
