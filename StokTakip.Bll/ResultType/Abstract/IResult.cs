using StokTakip.Bll.ResultType.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.ResultType.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }//ResultStatus.Success//ResultStatus.Error//enum
        public string Message { get; }//dönecek mesaj
        public Exception Exception { get; }
    }

    
}
