using Example.Models.Domain;
using Example.Models.Requests;
using Example.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Example.Test.Services
{
    [TestClass]
    public class ContactUsTest
    {
        public readonly IContactUsService _ContactUsService;

        public ContactUsTest()
        {
            DateTime arrangeNow = DateTime.Now;
            List<ContactUs> ContactUsClassList = new List<ContactUs> {
                new ContactUs{Id=1, Email="Email_1@Email.com", Name="Billy", Subject="Subject1", Question="Question 1", CreatedDate=arrangeNow, ModifiedDate=arrangeNow },
                new ContactUs{Id=2, Email="Email_2@Email.com", Name="Bob", Subject="Subject2", Question="Question 2", CreatedDate=arrangeNow, ModifiedDate=arrangeNow },
                new ContactUs{Id=3, Email="Email_3@Email.com", Name="Banjo", Subject="Subject3", Question="Question 3", CreatedDate=arrangeNow, ModifiedDate=arrangeNow }
                };

            var mock = new Mock<IContactUsService>();

            mock.Setup(m => m.Insert(It.IsAny<ContactUsAddRequest>())).Returns(
                (ContactUsAddRequest insertRequestModel) =>
                {
                    List<ValidationResult> validationResults = ValidateModel<ContactUsAddRequest>(insertRequestModel);
                    if (validationResults.Count > 0)
                        throw new ValidationException(validationResults[0], null, insertRequestModel);

                    ContactUs newModel = new ContactUs
                    {
                        Id = ContactUsClassList.Count + 1,
                        Email = insertRequestModel.Email,
                        Name = insertRequestModel.Name,
                        Subject = insertRequestModel.Subject,
                        Question = insertRequestModel.Question,
                        ModifiedDate = arrangeNow,
                      
                    };
                    ContactUsClassList.Add(newModel);
                    return newModel.Id;
                }
            );

            mock.Setup(m => m.GetAll()).Returns(ContactUsClassList);

            mock.Setup(m => m.GetById(It.IsAny<int>())).Returns(
                (int i) =>
                {
                    ContactUs model = ContactUsClassList.Where(m => m.Id == i).FirstOrDefault();
                    ContactUs newModel = null;
                    if (model != null)
                    {
                        newModel = new ContactUs
                        {
                            Id = model.Id,
                            Email = model.Email,
                            Name = model.Name,
                            Subject = model.Subject,
                            Question = model.Question,
                            CreatedDate = model.CreatedDate,
                            ModifiedDate = model.ModifiedDate,
                        };
                    }
                    return newModel;
                }
            );


            mock.Setup(m => m.Delete(It.IsAny<int>())).Callback(
                (int i) =>
                {
                    ContactUs ContactUsClass = ContactUsClassList.Where(m => m.Id == i).Single();
                    ContactUsClassList.Remove(ContactUsClass);
                }
            );

            _ContactUsService = mock.Object;
        }

        private static List<ValidationResult> ValidateModel<T>(T insertRequestModel)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext ctx = new ValidationContext(insertRequestModel, null, null);
            Validator.TryValidateObject(insertRequestModel, ctx, validationResults, true);
            return validationResults;
        }

        [TestMethod]
        public void Insert_Test()
        {
            //Arrange
            ContactUsAddRequest insertRequestModel = new ContactUsAddRequest
            {
                Email = "NewEmail@NewEmail.com",
                Name = "Jimmy",
                Subject = "New Subject",
                Question = "New Question",
            };
            //Act
            int result = _ContactUsService.Insert(insertRequestModel);

            //Assert
            Assert.IsInstanceOfType(result, typeof(int), "Id has to be int");
            Assert.IsTrue(result > 0);
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void Insert_Failed_Questions_Required_Test()
        {
            //Arrange
            ContactUsAddRequest InsertRequestModel = new ContactUsAddRequest
            {
                Email = "NewEmail@NewEmail.com",
                Name = "Jimmy",
                Subject = "New Subject",
                //Question = "New Question",
                
            };

            //Act
            int result = _ContactUsService.Insert(InsertRequestModel);

            //Assert
            //If you made it this far the test failed
            Assert.IsInstanceOfType(result, typeof(int), "Id has to be int");
            Assert.IsTrue(result > 0);
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void Insert_Failed_Name_Required_Test()
        {
            //Arrange
            ContactUsAddRequest InsertRequestModel = new ContactUsAddRequest
            {
                Email = "NewEmail@NewEmail.com",
                //Name = "Jimmy",
                Subject = "New Subject",
                Question = "New Question",
            };

            //Act
            int result = _ContactUsService.Insert(InsertRequestModel);

            //Assert
            //If you made it this far the test failed
            Assert.IsInstanceOfType(result, typeof(int), "Id has to be int");
            Assert.IsTrue(result > 0);
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void Insert_Failed_Subject_Required_Test()
        {
            //Arrange
            ContactUsAddRequest InsertRequestModel = new ContactUsAddRequest
            {
                Email = "NewEmail@NewEmail.com",
                Name = "Jimmy",
                //Subject = "New Subject",
                Question = "New Question",
            };

            //Act
            int result = _ContactUsService.Insert(InsertRequestModel);

            //Assert
            //If you made it this far the test failed
            Assert.IsInstanceOfType(result, typeof(int), "Id has to be int");
            Assert.IsTrue(result > 0);
        }

        [TestMethod, ExpectedException(typeof(ValidationException))]
        public void Insert_Failed_Email_Required_Test()
        {
            try
            {
                //Arrange
                ContactUsAddRequest InsertRequestModel = new ContactUsAddRequest
                {
                    //Email = "NewEmail@NewEmail.com",
                    Name = "Jimmy",
                    Subject = "New Subject",
                    Question = "New Question",
                };

                //Act
                int result = _ContactUsService.Insert(InsertRequestModel);
            }
            catch (ValidationException validationEx)
            {
                //Assert
                //Make sure we're throwing for the right reason
                foreach (string item in validationEx.ValidationResult.MemberNames)
                {
                    if (item == "Email")
                    {
                        //If the field being tested is found
                        // the negative test passes
                        throw;
                    }
                }
            }
            //if you made it this far the test failed
        }

        [TestMethod]
        public void GetAll_Test()
        {
            //Act
            List<ContactUs> ContactUsClassList = _ContactUsService.GetAll();

            //Assert
            Assert.IsNotNull(ContactUsClassList, "Contact Us Profile cannot be null");
            Assert.IsTrue(ContactUsClassList.Count > 0, "Contact Us Profile List has to have a count greater than 0");
        }

       
        [TestMethod]
        public void GetById_Test()
        {
            //Act
            ContactUs ContactUsClass = _ContactUsService.GetById(1);

            //Assert
            Assert.IsNotNull(ContactUsClass, "Id does not exist, check the id");
            Assert.IsInstanceOfType(ContactUsClass, typeof(ContactUs), "The type returned is not correct");
        }

       
        [TestMethod]
        public void Delete_Test()
        {
            //Arrange
            ContactUs ContactUsClass = _ContactUsService.GetById(3);

            //Act 
            _ContactUsService.Delete(ContactUsClass.Id);
            ContactUs deletedContactUsprofile = _ContactUsService.GetById(3);

            //Assert
            Assert.IsNull(deletedContactUsprofile, "Delete is not a success");
        }
    }
}
