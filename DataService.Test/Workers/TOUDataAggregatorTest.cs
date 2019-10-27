using System;
using System.Linq;
using DataService.Model;
using DataService.Workers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataService.Test.Workers
{
    [TestClass]
    public class TOUDataAggregatorTest
    {
        private TOUDataAggregator _subject;

        [TestInitialize]
        public void SetUp()
        {
            _subject = new TOUDataAggregator();
        }

        [TestMethod]
        public void Aggregate_OneGroupEvenCount_ReturnsAverageAsMedian()
        {
            //Arrange
            var source = new[]
            {
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh", Energy= (decimal)-0.86},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)-4.19},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)6.50},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)7.77}
            };
            //Act
            var actual = _subject.Aggregate(source);
            //Assert
            Assert.AreEqual(actual.Count(), 1);
            Assert.AreEqual(actual.First().MeterCode, 1111);
            Assert.AreEqual(actual.First().Maximum, (decimal)7.77);
            Assert.AreEqual(actual.First().Minimum, (decimal)-4.19);
            Assert.AreEqual(actual.First().Median, (decimal)2.82);
        }

        [TestMethod]
        public void Aggregate_OneGroupOddCount_ReturnsMiddleItemAsMedian()
        {
            //Arrange
            var source = new[]
            {
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh", Energy= (decimal)-0.86},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)-4.19},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)6.50},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)7.77},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)6.876}
            };
            //Act
            var actual = _subject.Aggregate(source);
            //Assert
            Assert.AreEqual(actual.Count(), 1);
            Assert.AreEqual(actual.First().MeterCode, 1111);
            Assert.AreEqual(actual.First().Maximum, (decimal)7.77);
            Assert.AreEqual(actual.First().Minimum, (decimal)-4.19);
            Assert.AreEqual(actual.First().Median, (decimal)6.5);
        }

        [TestMethod]
        public void Aggregate_TwoMeterCodes_ReturnsTwoGroups()
        {
            //Arrange
            var source = new[]
            {
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh", Energy= (decimal)-0.86},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)-4.19},
                new TOUData{MeterCode=2222,Date=DateTime.Now,DataType="KWh",Energy= (decimal)6.50},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)7.77},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)6.876}
            };
            //Act
            var actual = _subject.Aggregate(source);
            //Assert
            Assert.AreEqual(actual.Count(), 2);
        }

        [TestMethod]
        public void Aggregate_TwoMeterCodesOnDifferentDates_ReturnsThreeGroups()
        {
            //Arrange
            var source = new[]
            {
                new TOUData{MeterCode=1111,Date=DateTime.Now.AddDays(1),DataType="KWh", Energy= (decimal)-0.86},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)-4.19},
                new TOUData{MeterCode=2222,Date=DateTime.Now,DataType="KWh",Energy= (decimal)6.50},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)7.77},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)6.876}
            };
            //Act
            var actual = _subject.Aggregate(source);
            //Assert
            Assert.AreEqual(actual.Count(), 3);
        }

        [TestMethod]
        public void Aggregate_TwoMeterCodesOnDifferentDatesAndDataType_ReturnsFourGroups()
        {
            //Arrange
            var source = new[]
            {
                new TOUData{MeterCode=1111,Date=DateTime.Now.AddDays(1),DataType="KWh", Energy= (decimal)-0.86},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)-4.19},
                new TOUData{MeterCode=2222,Date=DateTime.Now,DataType="KWh",Energy= (decimal)6.50},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KWh",Energy= (decimal)7.77},
                new TOUData{MeterCode=1111,Date=DateTime.Now,DataType="KW",Energy= (decimal)6.876}
            };
            //Act
            var actual = _subject.Aggregate(source);
            //Assert
            Assert.AreEqual(actual.Count(), 4);
        }
    }
}
