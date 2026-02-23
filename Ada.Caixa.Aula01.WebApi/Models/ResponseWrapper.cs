public class ResponseWrapper<T>
{
    public bool Success { get; set; } = true;
    public int Status { get; set; }
    public T Data { get; set; }
}