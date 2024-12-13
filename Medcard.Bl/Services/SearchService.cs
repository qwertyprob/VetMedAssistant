using AutoMapper;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.Bl.Abstraction;
using Medcard.Bl.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Medcard.Bl.Services
{
    public class SearchService:ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IMapper _mapper;
        public SearchService(ISearchRepository searchRepository, IMapper mapper) 
        {
            _searchRepository = searchRepository;
            _mapper=mapper;

        }


        public async Task<IReadOnlyCollection<OwnerModel>> GetAllFromSearchAsync(string searchItem)
        {
            var medcard = await _searchRepository.GetAllFromSearchAsync(searchItem);

            var mappedMedcard = _mapper.Map<IReadOnlyCollection<OwnerModel>>(medcard);

            return mappedMedcard;
        }
    }
}
