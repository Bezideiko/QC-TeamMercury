using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleFieldNamespace;

namespace UnitTestsBattleField
{
    [TestClass]
    public class UnitTestsBattleFieldGame
    {
        [TestMethod]
        public void IsInputCoordinatesInRangeIncorrectRowInputBoundaryCaseTest()
        {
            int battleFieldSize = 2;
            BattleFieldGame battleFieldGame = new BattleFieldGame(battleFieldSize);
            bool actual = battleFieldGame.IsInputCoordinatesInRange(2, 1);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsInputCoordinatesInRangeIncorrectColInputBoundaryCaseTest()
        {
            int battleFieldSize = 2;
            BattleFieldGame battleFieldGame = new BattleFieldGame(battleFieldSize);
            bool actual = battleFieldGame.IsInputCoordinatesInRange(1, 2);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsInputCoordinatesInRangeWithCorrectInputTest()
        {
            int battleFieldSize = 2;
            BattleFieldGame battleFieldGame = new BattleFieldGame(battleFieldSize);
            bool actual = battleFieldGame.IsInputCoordinatesInRange(1, 1);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsInputCoordinatesInRangeWithIncorrectRowInputTest()
        {
            int battleFieldSize = 2;
            BattleFieldGame battleFieldGame = new BattleFieldGame(battleFieldSize);
            bool actual = battleFieldGame.IsInputCoordinatesInRange(4, 1);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsInputCoordinatesInRangeWithIncorrectColInputTest()
        {
            int battleFieldSize = 4;
            BattleFieldGame battleFieldGame = new BattleFieldGame(battleFieldSize);
            bool actual = battleFieldGame.IsInputCoordinatesInRange(4, 5);
            Assert.IsFalse(actual);
        }
    }
}
