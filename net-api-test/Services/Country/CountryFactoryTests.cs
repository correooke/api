using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace net_api.Services.Tests
{
    [TestClass()]
    public class CountryFactoryTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var factory = new CountryFactory();
            var r = new Infraestructure.ExternalAPI.SerializedObjects.DataCountry.Root();

            r.latlng = new List<double>();
            r.latlng.Add(1);
            r.latlng.Add(2);
            r.currencies = new List<Infraestructure.ExternalAPI.SerializedObjects.DataCountry.Currency>();
            r.currencies.Add(Moq.Mock.Of<Infraestructure.ExternalAPI.SerializedObjects.DataCountry.Currency>());
            r.languages = new List<Infraestructure.ExternalAPI.SerializedObjects.DataCountry.Language>();
            r.languages.Add(Moq.Mock.Of<Infraestructure.ExternalAPI.SerializedObjects.DataCountry.Language>());
            r.timezones = new List<string>();
            r.timezones.Add("");

            var f = factory.Create(r);

            Assert.AreEqual(r.alpha2Code, f.Alpha2Code);
        }

        [TestMethod()]
        public void CreateIncompleteDataTest()
        {
            var factory = new CountryFactory();
            var r = new Infraestructure.ExternalAPI.SerializedObjects.DataCountry.Root();

            Assert.ThrowsException<Exception>(() => factory.Create(r));
        }

        [TestMethod()]
        public void CreateCaseNullTest()
        {
            var factory = new CountryFactory();
            var r = new Infraestructure.ExternalAPI.SerializedObjects.DataCountry.Root();

            Assert.ThrowsException<Exception>(() => factory.Create(r));
        }

    }
}