﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Bll.ResultType.Enums
{
    public enum ResultStatus
    {
        Success = 0,
        Error = 1,
        Warning = 2,//ResultStatus.Warning sayı olarak tutulacaklar
        Info = 3//ResultStatus.Info
    }
}
