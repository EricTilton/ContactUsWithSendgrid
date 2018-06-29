using ExampleModels.Domain;
using Example.Models.Requests;
using System.Collections.Generic;

namespace Example.Services.Interfaces
{
    public interface IContactUsService
    {
        void Delete(int Id);
        List<ContactUs> GetAll();
        ContactUs GetById(int Id);
        int Insert(ContactUsAddRequest model);
        ContactUsRequest Post(ContactUsAddRequest model);
    }
}