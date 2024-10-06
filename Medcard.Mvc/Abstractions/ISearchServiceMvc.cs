using Medcard.DbAccessLayer.Dto;
using Medcard.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.Mvc.Abstractions
{
    public interface ISearchServiceMvc
    {
        Task<IReadOnlyCollection<OwnerModel>> GetAllFromSearchAsync(string searchItem);
    }
}