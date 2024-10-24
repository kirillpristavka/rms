using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RMS.UI.Forms.Mail.Tests
{
    [TestClass()]
    public class LetterEditTests
    {
        [TestMethod()]
        public void GetEmailAddressTest()
        {
            var email = "ya@list.ru, xseasdasdnsdasusasd92@gmail.com - sass@gt.ru, s@s.e  ssda asdas asd qw 1 1 da ;as as 12 sa d;@ asdas i2d2lasdaseL@lissst.ru";
            var listemail = LetterEdit.GetEmailAddress(email);

            Assert.AreEqual("ya@list.ru", listemail[0]);
            Assert.AreEqual("xseasdasdnsdasusasd92@gmail.com", listemail[1]);
            Assert.AreEqual("sass@gt.ru", listemail[2]);
            Assert.AreNotEqual("i2d2lasdaseL@lissst.ru", listemail[3]);
        }

        [TestMethod()]
        public async void GetTimesOfDayTest()
        {
            //var dateTime1 = new DateTime(2021, 1, 1, 0, 0, 0);
            //var dateTime2 = new DateTime(2021, 1, 1, 1, 0, 0);
            //var dateTime3 = new DateTime(2021, 1, 1, 11, 0, 0);
            //var dateTime4 = new DateTime(2021, 1, 1, 11, 59, 0);
            //var dateTime5 = new DateTime(2021, 1, 1, 12, 0, 0);
            //var dateTime6 = new DateTime(2021, 1, 1, 16, 0, 0);
            //var dateTime7 = new DateTime(2021, 1, 1, 16, 59, 0);
            //var dateTime8 = new DateTime(2021, 1, 1, 17, 0, 0);
            //var dateTime9 = new DateTime(2021, 1, 1, 23, 0, 0);

            //var message1 = await LetterEdit.GetTimesOfDay(dateTime1);
            //var message2 = await LetterEdit.GetTimesOfDay(dateTime2);
            //var message3 = await LetterEdit.GetTimesOfDay(dateTime3);
            //var message4 = await LetterEdit.GetTimesOfDay(dateTime4);
            //var message5 = await LetterEdit.GetTimesOfDay(dateTime5);
            //var message6 = await LetterEdit.GetTimesOfDay(dateTime6);
            //var message7 = await LetterEdit.GetTimesOfDay(dateTime7);
            //var message8 = await LetterEdit.GetTimesOfDay(dateTime8);
            //var message9 = await LetterEdit.GetTimesOfDay(dateTime9);
            
            //Assert.AreEqual($", доброе утро{Environment.NewLine}", message1);
            //Assert.AreEqual($", доброе утро{Environment.NewLine}", message2);
            //Assert.AreEqual($", доброе утро{Environment.NewLine}", message3);
            //Assert.AreEqual($", доброе утро{Environment.NewLine}", message4);
            //Assert.AreEqual($", доброе день{Environment.NewLine}", message5);
            //Assert.AreEqual($", доброе день{Environment.NewLine}", message6);
            //Assert.AreEqual($", доброе день{Environment.NewLine}", message7);
            //Assert.AreEqual($", доброе вечер{Environment.NewLine}", message8);
            //Assert.AreEqual($", доброе вечер{Environment.NewLine}", message9);
        }
    }
}