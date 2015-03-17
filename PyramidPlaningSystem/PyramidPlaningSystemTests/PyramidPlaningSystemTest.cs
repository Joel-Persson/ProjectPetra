using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PyramidPlaningSystem.Models;

namespace PyramidPlaningSystemTests
{
    [TestFixture]
    public class PyramidPlaningSystemTest
    {
        private ConvertClass _sut;

        [SetUp]
        public void Init()
        {
            _sut = new ConvertClass();
        }

        [Test]
        public void ContactViewModel_Method_ContactConvert_Returns_Contact()
        {
            var expectedContact = new Contact()
            {
                Address = "Address",
                City = "City",
                Firstname = "Firstname",
                Lastname = "Lastname",
                Phone = "Phone",
                ZipCode = "zip"
            };

            var contactViewModel = new ContactInfoViewModel()
            {
                Address = "Address",
                City = "City",
                Firstname = "Firstname",
                Lastname = "Lastname",
                Phone = "Phone",
                ZipCode = "zip"
            };


            Contact contact = _sut.ContactConvert(contactViewModel);

            bool areEqual = contact == expectedContact;

            Assert.AreEqual(expectedContact, contact);
            Assert.True(areEqual);
        }

        [Test]
        public void NullValue_Method_Convert_ThrowsException()
        {

            var contactViewModel = new ContactInfoViewModel()
            {
                Address = "Address",
                City = "City",
                Firstname = "Firstname",
                Lastname = "Lastname",
                Phone = "Phone"
            };

            Assert.Throws<NullReferenceException>(() =>
            {
                Contact contact = _sut.ContactConvert(null);
                _sut.ContactConvert(contactViewModel);
            });
        }
    }

    public class ContactInfoViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(20)]
        public string ZipCode { get; set; }
    }
    public class Contact : IEquatable<Contact>
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(20)]
        public string ZipCode { get; set; }

        public bool Equals(Contact other)
        {
            if (other == null)

                return false;

            if
                (this.Address == other.Address
                && this.City == other.City
                && this.Firstname == other.Firstname
                && this.Lastname == other.Lastname
                && this.Phone == other.Phone
                && this.ZipCode == other.ZipCode)
                return true;

            else
                return false;
        }

        public static bool operator ==(Contact a, Contact b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Address == b.Address &&
             a.City == b.City &&
             a.Firstname == b.Firstname &&
             a.Lastname == b.Lastname &&
             a.Phone == b.Phone &&
             a.ZipCode == b.ZipCode;
        }

        public static bool operator !=(Contact a, Contact b)
        {
            return !(a == b);
        }
    }
}
