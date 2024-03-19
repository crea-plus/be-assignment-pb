using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhoneBook.Core;
using PhoneBook.DataSource.Models;
using PhoneBook.DataSource.Repositories;
using PhoneBook.Services;

namespace PhoneBook.Tests.Services
{
    [TestClass]
    public class ContactServiceTest : ContactServiceDriver
    {
        //[TestMethod]
        //public async Task AddContact_Sucess_ReturnContact()
        //{
        //    var result = await Sut.AddContact(RequestContact, Id);

        //    Assert.AreEqual(result.Email, ContactResponse.Email);

        //    VerifyAddWasCalled();
        //}


        //TEST CASSES FOR USER NULL  && DB THROW EXCEPTION
    }

    public class ContactServiceDriver
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly string _avatarUri;
        private readonly string _email;
        private readonly string _name;
        private readonly string _number;

        private readonly UserDbo _user;

        public AddContactModel RequestContact { get; private set; }

        public Guid Id { get; set; }
        public ContactResponse ContactResponse { get; private set; }
        public IContactService Sut { get; }

        public ContactServiceDriver()
        {
            _avatarUri = "someUri";
            _email = "someEmail";
            _name = "someName";
            _number = "soomeNumber";

            _user = new UserDbo();

            RequestContact = new AddContactModel
            {
                AvatarImageUrl = _avatarUri,
                Email = _email,
                Name = _name,
                PhoneNumber = _number
            };
            Id = Guid.NewGuid();


            ContactResponse = new ContactResponse(new ContactDbo
            {
                AvatarImageUri = _avatarUri,
                Email = _email,
                Name = _name,
                Number = _number
            });

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(i => i.UserRepository.GetById(Id)).ReturnsAsync(_user);

            Sut = new ContactService(_unitOfWork.Object);
        }

        public void VerifyAddWasCalled()
        {
            _unitOfWork.Verify(i => i.UserContactRepository.Add(It.Is<UserContactDbo>(i => i.Id == It.IsAny<Guid>() &&
                                                                                 i.Favorite == false &&
                                                                                 i.User == _user &&
                                                                                 i.Contact == It.IsAny<ContactDbo>())), Times.Once);

            _unitOfWork.Verify(i => i.SaveAsync(), Times.Once);
        }
    }
}