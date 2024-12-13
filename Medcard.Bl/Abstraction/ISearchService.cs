using Medcard.DbAccessLayer.Dto;
using Medcard.Bl.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.Bl.Abstraction
{
    public interface ISearchService
    {
        Task<IReadOnlyCollection<OwnerModel>> GetAllFromSearchAsync(string searchItem);
    }
}