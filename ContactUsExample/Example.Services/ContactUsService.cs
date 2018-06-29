using Example.Data;
using Example.Models.Domain;
using Example.Models.Requests;
using Example.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Hosting;

namespace Example.Services
{
    public class ContactUsService : BaseService, IContactUsService
    {
        private IConfigService _configService;

        public ContactUsService(IConfigService configService)
        {
            _configService = configService;
        }

        public int Insert(ContactUsAddRequest model)
        {
            int result = 0;
            this.DataProvider.ExecuteNonQuery(
                "ContactUs_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = "@Id";
                    parm.SqlDbType = SqlDbType.Int;
                    parm.Direction = ParameterDirection.Output;
                    paramCol.Add(parm);

                    paramCol.AddWithValue("@Email", model.Email);
                    paramCol.AddWithValue("@Name", model.Name);
                    paramCol.AddWithValue("@Subject", model.Subject);
                    paramCol.AddWithValue("@Question", model.Question);
                },
                returnParameters: delegate (SqlParameterCollection paramcol)
                {
                    result = (int)paramcol["@Id"].Value;
                }
            );
            return result;
        }

        public List<ContactUs> GetAll()
        {
            List<ContactUs> ContactUsClassList = new List<ContactUs>();
            this.DataProvider.ExecuteCmd(
                "ContactUs_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    ContactUs model = Mapper(reader);
                    ContactUsClassList.Add(model);
                }
            );
            return ContactUsClassList;
        }

        private ContactUs Mapper(IDataReader reader)
        {
            ContactUs model = new ContactUs();
            int index = 0;
            model.Id = reader.GetSafeInt32(index++); //index is 0 then increment by one 
            model.Email = reader.GetSafeString(index++); //index is 1, then increment by one
            model.Name = reader.GetSafeString(index++);
            model.Subject = reader.GetSafeString(index++);
            model.Question = reader.GetSafeString(index++);
            model.CreatedDate = reader.GetDateTime(index++);
            model.ModifiedDate = reader.GetDateTime(index++);
            return model;
        }

        public ContactUs GetById(int id)
        {
            ContactUs model = null;
            this.DataProvider.ExecuteCmd(
                "ContactUs_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    model = Mapper(reader);
                }
            );
            return model;
        }

        public void Delete(int Id)
        {
            this.DataProvider.ExecuteNonQuery(
                "ContactUs_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", Id);
                }
            );
        }

        public ContactUsRequest Post(ContactUsAddRequest model)
        {
            ContactUsRequest contactusRequest = new ContactUsRequest
            {
                FromEmail = model.Email,
                FromName = model.Name,
                ToEmail = _configService.ConvertConfigValue_String("To_Email"),
                Subject = model.Subject,
                Question = model.Question,
                ContactUsEmailTemplate = HostingEnvironment.MapPath("~/Content/ContactUsEmailTemplate.html"),
                SendGridKey = _configService.ConvertConfigValue_String("SendGrid_Key"),
            };
            return contactusRequest;
        }
    }
}

