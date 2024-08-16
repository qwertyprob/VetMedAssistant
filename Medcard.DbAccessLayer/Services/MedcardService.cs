using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Services
{
    public class MedcardService
    {
        public readonly IMapper _mapper; 

        public MedcardService(IMapper mapper)
        {

        _mapper = mapper; 
        }

    }
}
