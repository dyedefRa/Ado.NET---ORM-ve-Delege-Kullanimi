using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_INTERFACE
{
   public interface IORM<T>
    {

        DataTable RunSELECT();

        bool RunINSERT(T entity);
        bool RunDELETE(int silinecekID);
        bool RunUPDATE(T entity);
    }
}
