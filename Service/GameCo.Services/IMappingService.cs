using System;
using System.Collections.Generic;
using System.Text;

namespace GameCo.Services
{
    public interface IMappingService
    {
        T MapOject<T>(object obj);
    }
}
