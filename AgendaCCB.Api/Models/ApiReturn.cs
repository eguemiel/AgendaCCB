namespace AgendaCCB.Api.Models
{
    public class ApiReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Object { get; set; }        
    }
}