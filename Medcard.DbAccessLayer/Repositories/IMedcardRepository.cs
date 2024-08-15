using Medcard.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer
{
    public interface IMedcardRepository
    {
        Task<List<OwnerModel>> GetAll();
    }
}