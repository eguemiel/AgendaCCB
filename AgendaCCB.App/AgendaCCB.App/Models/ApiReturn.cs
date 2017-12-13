using System;
using System.Threading.Tasks;

namespace AgendaCCB.App.Models
{
    public class ApiReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Object { get; set; }

        public bool Success
        {
            get { return Status >= 200 && Status < 300; }
        }
    }

    public class ApiReturn<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Object { get; set; }

        public bool Success
        {
            get { return Status >= 200 && Status < 300; }
        }


        public ApiReturn()
        {

        }

        public ApiReturn(ApiReturn apiReturn, T returnedObject)
        {
            Status = apiReturn.Status;
            Message = apiReturn.Message;
            Object = returnedObject;
        }
        
        public ApiReturn NoType()
        {
            return new ApiReturn
            {
                Status = Status,
                Message = Message,
                Object = Object
            };
        }
    }
}
