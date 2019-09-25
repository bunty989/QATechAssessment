using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using APITest.Base;
using APITest.Library;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;

namespace APITest
{
    [TestFixture]
    public abstract class HttpTestBase
    {

        [OneTimeSetUp]
        public void Initialise()
        {
        }
        

        [OneTimeTearDown]
        public void TearDown()
        {
        }
    }
}
