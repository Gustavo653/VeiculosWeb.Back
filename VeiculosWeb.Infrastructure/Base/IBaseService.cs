﻿using VeiculosWeb.DTO.Base;

namespace VeiculosWeb.Infrastructure.Base
{
    public interface IBaseService<T>
    {
        Task<ResponseDTO> Create(T objectDTO);
        Task<ResponseDTO> Update(Guid id, T objectDTO);
        Task<ResponseDTO> Remove(Guid id);
        Task<ResponseDTO> GetList();
    }
}
