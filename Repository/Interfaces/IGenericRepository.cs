﻿using SalesCrud.Model;

namespace SalesCrud.Repository.Interfaces;

public interface IGenericRepository<T> where T : BaseModel
{
   Task<(List<T>, int)> FindAll(int pageNumber, int pageSize);
    T FindById(Guid id);
    T Created(T item);
    T Update(T item);
    void Delete(Guid id);
    bool Exists(Guid id);
}
