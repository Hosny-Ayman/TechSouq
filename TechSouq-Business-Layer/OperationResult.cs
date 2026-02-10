using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSouq.Application
{

    public enum OperationStatus
    {
        Success,
        NotFound,
        Faild
    }

    public class OperationResult<T>
    {
        public OperationStatus Status { get; set; }

        public string Message { get; set; } 

        public T Data { get; set; }

        public bool Success => Status == OperationStatus.Success;


    }
}
