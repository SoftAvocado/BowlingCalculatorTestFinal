using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingCalculator.Tests
{
    [TestClass]
    public class BowlingCalculatorTests
    {
        [TestMethod]
        public void calculate_Throws_standartThrows_lastThrowIsSpare()
        {
            List<int> throws = new List<int>() { 10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3 };
            List<int> expexted_result = new List<int>() { 10, 24, 30, 44, 46, 55, 56, 76, 96, 126, 132, 138, 144, 148, 162, 165, 168 };
            List<int> actual_result = new List<int>();

            Calculator calc = new Calculator();
            actual_result = calc.calculate_Throws(throws);

            CollectionAssert.AreEqual(expexted_result, actual_result);
        }

        [TestMethod]
        public void calculate_Throws_standartThrows_lastThrowNoBonus()
        {
            List<int> throws = new List<int>() { 10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 2};
            List<int> expexted_result = new List<int>() { 10, 24, 30, 44, 46, 55, 56, 76, 96, 126, 132, 138, 144, 148, 162, 164};
            List<int> actual_result = new List<int>();

            Calculator calc = new Calculator();
            actual_result = calc.calculate_Throws(throws);

            CollectionAssert.AreEqual(expexted_result, actual_result);
        }

        [TestMethod]
        public void calculate_Throws_perfectThrows()
        {
            List<int> throws = new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10};
            List<int> expexted_result = new List<int>() { 10, 30, 60, 90, 120, 150, 180, 210, 240, 270, 290, 300};
            List<int> actual_result = new List<int>();

            Calculator calc = new Calculator();
            actual_result = calc.calculate_Throws(throws);

            CollectionAssert.AreEqual(expexted_result, actual_result);
        }

        [TestMethod]
        public void calculate_Throws_otherTrows_lastThrowIsStrike()
        {
            List<int> throws = new List<int>() { 2, 1, 9, 1, 10, 5, 5, 10, 4, 6, 6, 2, 7, 3, 10, 10, 10, 10};
            List<int> expexted_result = new List<int>() { 2, 3, 12, 13, 33, 43, 53, 73, 81, 93, 105, 107, 114, 117, 137, 157, 177, 187};
            List<int> actual_result = new List<int>();

            Calculator calc = new Calculator();
            actual_result = calc.calculate_Throws(throws);

            CollectionAssert.AreEqual(expexted_result, actual_result);
        }

        [TestMethod]
        public void get_FrameResults_FromStandardThrowsFrame()
        {
            List<int> throws = new List<int>() { 10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3 };
            int frame_id = 2;
            List<int> expexted_result = new List<int>() { 7, 3};
            List<int> actual_result = new List<int>();

            Calculator calc = new Calculator();
            calc.calculate_Throws(throws);
            actual_result = calc.Get_FrameResults(frame_id);

            CollectionAssert.AreEqual(expexted_result, actual_result);
        }

        [TestMethod]
        public void get_FrameScores_FromStandardThrowsFrame()
        {
            List<int> throws = new List<int>() { 10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3 };
            int frame_id = 2;
            int expexted_result = 17;
            int actual_result;

            Calculator calc = new Calculator();
            calc.calculate_Throws(throws);
            actual_result = calc.Get_FrameScores(frame_id);

            Assert.AreEqual(expexted_result, actual_result);
        }

        [TestMethod]
        public void get_FrameTotal_FromStandardThrowsFrame()
        {
            List<int> throws = new List<int>() { 10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3 };
            int frame_id = 2;
            int expexted_result = 37;
            int actual_result;

            Calculator calc = new Calculator();
            calc.calculate_Throws(throws);
            actual_result = calc.Get_FrameTotal(frame_id);

            Assert.AreEqual(expexted_result, actual_result);
        }
    }
}
