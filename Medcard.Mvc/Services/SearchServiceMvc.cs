using AutoMapper;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.Mvc.Abstractions;
using Medcard.Mvc.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Medcard.Mvc.Services
{
    public class SearchServiceMvc:ISearchServiceMvc
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IMapper _mapper;
        public SearchServiceMvc(ISearchRepository searchRepository, IMapper mapper) 
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
