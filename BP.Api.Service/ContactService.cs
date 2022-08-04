using AutoMapper;
using BP.Api.Data.Models;
using BP.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BP.Api.Service
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactService(IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }
        public ContactDTO GetContactById(int id)
        {
            Contact contact = getDummyContact();


            var client = _httpClientFactory.CreateClient("garantiapi");


            ContactDTO resultContact = _mapper.Map<ContactDTO>(contact);

            return resultContact;

        }

        private Contact getDummyContact()
        {
            return new Contact()
            {
                Id = 1,
                Name = "John",
                LastName = "Doe"
            };
        }
    }
}
