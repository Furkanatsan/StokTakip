using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.ResultType.Abstract
{
    public interface IDataResult<out T>:IResult//out kullanarak tek bir prop sayesinde ister liste ister entity taşıyabiliyoruz.
    {
        public T Data { get; }//new DataResult<Book>(ResultStatus.Success,book);
                              //new DataResult<IList<Book>>(ResultStatus.Success,bookList);
    }
}
