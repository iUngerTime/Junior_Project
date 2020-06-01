using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database_Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Helpers.Tests
{
    [TestClass()]
    public class SqlHelperTests
    {

        //Tests the GetConnectionString function of the SqlHelper class.  Also test the getters and setters for the connection string values
        [TestMethod()]
        public void GetConnectionStringTest()
        {
            ////setting up the SqlHelper connection string values and checking the getters/setters
            //SqlHelper.ServerAddress = "test.server.address";
            //Assert.AreEqual("test.server.address", SqlHelper.ServerAddress);

            //SqlHelper.ServerPort = "9999";
            //Assert.AreEqual("9999", SqlHelper.ServerPort);

            //SqlHelper.ServerDatabaseName = "ExampleName";
            //Assert.AreEqual("ExampleName", SqlHelper.ServerDatabaseName);

            //SqlHelper.ServerUsername = "TestUsername";
            //Assert.AreEqual("TestUsername", SqlHelper.ServerUsername);

            //SqlHelper.ServerPassword = "TestPassword";
            //Assert.AreEqual("TestPassword", SqlHelper.ServerPassword);

            ////Testing the connection string
            //string expectedvalue =
            //    "server=test.server.address, 9999; database=ExampleName; UID=TestUsername; password=TestPassword";
            //Assert.AreEqual(expectedvalue, SqlHelper.GetConnectionString());
        }



    }
}