using Medcard.DbAccessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Interfaces
{
    public interface ISearchRepository
    {
        Task<IReadOnlyCollection<OwnerDto>> GetAllFromSearchAsync(string searchItem);
    }
}