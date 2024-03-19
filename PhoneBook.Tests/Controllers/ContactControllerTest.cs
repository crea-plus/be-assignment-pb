using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhoneBook.Controllers;
using PhoneBook.Core;
using PhoneBook.Services;

namespace PhoneBook.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest : ContactControllerDriver
    {
        [TestMethod]
        public async Task GetContact_Success_ReturnOK()
        {
            var result = await Sut.GetContact(ContactId);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(Response, okObjectResult.Value);
        }

        [TestMethod]
        public async Task GetContact_ServiceThrowException_ReturnInternalServerError()
        {
            SetupLogicThrowError();

            var result = await Sut.GetContact(ContactId);

            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.IsTrue(statusCodeResult.Value.Equals("An error occurred while fetching contact: error"));
        }
    }

    public class ContactControllerDriver
    {
        private readonly Mock<IContactService> _contactService;

        public Guid ContactId { get; set; }

        public Contact Response { get; set; }
        public ContactController Sut { get; set; }

        public ContactControllerDriver()
        {
            ContactId = Guid.NewGuid();
            Response = new Contact(new DataSource.Models.ContactDbo());
            _contactService = new Mock<IContactService>();

            _contactService.Setup(i => i.Get(ContactId)).ReturnsAsync(Response);

            Sut = new ContactController(_contactService.Object);
        }

        public void SetupLogicThrowError()
        {
            _contactService.Setup(i => i.Get(ContactId)).ThrowsAsync(new Exception("error"));
        }
    }
}