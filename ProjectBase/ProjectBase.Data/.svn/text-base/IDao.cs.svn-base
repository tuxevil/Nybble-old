using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBase.Data
{
    public interface IDao<T, IdT>
    {
        T GetById(IdT id, bool shouldLock);
    	T GetById(IdT id);
        IList<T> GetAll();
        T Save(T entity);
        T SaveOrUpdate(T entity);
        void Delete(T entity);
    }
}
