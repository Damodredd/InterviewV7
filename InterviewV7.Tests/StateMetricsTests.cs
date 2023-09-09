using InterviewV7.Models;
using InterviewV7.Tools;

namespace InterviewV7.Tests
{
    public class StateMetricsTests
    {
        #region percent calculations tests

        [Fact]
        public void CalculateTotalTime_10PercentRunning()
        {
            // Arrange
            var states = new List<State>
        {
            new Running { Duration = TimeSpan.FromSeconds( 1) },
            new Faulted { Duration = TimeSpan.FromSeconds(9) },
        };

            // Act
            var totalRunningTime = StateMetrics.CalculateTotalTime<Running>(states);
            var totalFaultedTime = StateMetrics.CalculateTotalTime<Faulted>(states);
            var runningPercentage = Math.Round(totalRunningTime / (totalRunningTime + totalFaultedTime) * 100, 2);
            var faultedPercentage = 100 - runningPercentage;

            // Assert
            Assert.Equal(10, runningPercentage);
            Assert.Equal(90, faultedPercentage);
        }

        [Fact]
        public void CalculateTotalTime_0PercentRunning()
        {
            // Arrange
            var states = new List<State>
        {
            new Faulted { Duration = TimeSpan.FromSeconds(10) },
        };

            // Act
            var totalRunningTime = StateMetrics.CalculateTotalTime<Running>(states);
            var totalFaultedTime = StateMetrics.CalculateTotalTime<Faulted>(states);
            var runningPercentage = Math.Round(totalRunningTime / (totalRunningTime + totalFaultedTime) * 100, 2);
            var faultedPercentage = 100 - runningPercentage;

            // Assert
            Assert.Equal(0, runningPercentage);
            Assert.Equal(100, faultedPercentage);
        }

        [Fact]
        public void CalculateTotalTime_100PercentRunning()
        {
            // Arrange
            var states = new List<State>
        {
            new Running { Duration = TimeSpan.FromSeconds(10) },
        };

            // Act
            var totalRunningTime = StateMetrics.CalculateTotalTime<Running>(states);
            var totalFaultedTime = StateMetrics.CalculateTotalTime<Faulted>(states);
            var runningPercentage = Math.Round(totalRunningTime / (totalRunningTime + totalFaultedTime) * 100, 2);
            var faultedPercentage = 100 - runningPercentage;

            // Assert
            Assert.Equal(100, runningPercentage);
            Assert.Equal(0, faultedPercentage);
        }

        #endregion percent calculations tests

        #region top 5 alarms tests

        [Fact]
        public void TopAlarmCodesByDuration_ShouldReturn1TopFault_For10Seconds()
        {
            // Arrange
            var states = new List<State>
        {
            new Faulted { AlarmCode = 3, Duration = TimeSpan.FromSeconds(10) }
        };

            // Act
            var topAlarmCodes = StateMetrics.TopAlarmCodesByDuration(states);
            var topAlarmCodesList = topAlarmCodes.ToList();

            // Assert
            Assert.Single(topAlarmCodes);
            Assert.Equal(3, topAlarmCodesList[0].AlarmCode);
            Assert.Equal(10, topAlarmCodesList[0].Duration);
        }

        [Fact]
        public void TopAlarmCodesByDuration_ShouldReturn54321Unique_For15Unique()
        {
            // Arrange
            var states = new List<State>
        {
            new Faulted { AlarmCode = 1, Duration = TimeSpan.FromSeconds(6) },
            new Faulted { AlarmCode = 2, Duration = TimeSpan.FromSeconds(7) },
            new Faulted { AlarmCode = 3, Duration = TimeSpan.FromSeconds(8) },
            new Faulted { AlarmCode = 4, Duration = TimeSpan.FromSeconds(9) },
            new Faulted { AlarmCode = 5, Duration = TimeSpan.FromSeconds(10) },
            new Faulted { AlarmCode = 6, Duration = TimeSpan.FromSeconds(2) },
            new Faulted { AlarmCode = 7, Duration = TimeSpan.FromSeconds(2) },
            new Faulted { AlarmCode = 8, Duration = TimeSpan.FromSeconds(2) },
            new Faulted { AlarmCode = 9, Duration = TimeSpan.FromSeconds(2) },
            new Faulted { AlarmCode = 10, Duration = TimeSpan.FromSeconds(2) },
            new Faulted { AlarmCode = 11, Duration = TimeSpan.FromSeconds(2) },
            new Faulted { AlarmCode = 12, Duration = TimeSpan.FromSeconds(2) },
            new Faulted { AlarmCode = 13, Duration = TimeSpan.FromSeconds(2) },
            new Faulted { AlarmCode = 14, Duration = TimeSpan.FromSeconds(2) },
            new Faulted { AlarmCode = 15, Duration = TimeSpan.FromSeconds(2) },
            // ... continue for all 15 faults ...
        };

            // Act
            var topAlarmCodes = StateMetrics.TopAlarmCodesByDuration(states);
            var topAlarmCodesList = topAlarmCodes.ToList();

            // Assert
            Assert.Equal(5, topAlarmCodesList.Count);
            Assert.Equal(5, topAlarmCodesList[0].AlarmCode);
            Assert.Equal(4, topAlarmCodesList[1].AlarmCode);
            Assert.Equal(3, topAlarmCodesList[2].AlarmCode);
            Assert.Equal(2, topAlarmCodesList[3].AlarmCode);
            Assert.Equal(1, topAlarmCodesList[4].AlarmCode);
        }

        [Fact]
        public void TopAlarmCodesByDuration_ShouldCombineFor2ndAnd5thPosition_For15Faults()
        {
            // Arrange
            var states = new List<State>
            {
                // Add states here that combine durations to fit your requirements for 2nd and 5th
            };

            // Act
            var topAlarmCodes = StateMetrics.TopAlarmCodesByDuration(states);
            var topAlarmCodesList = topAlarmCodes.ToList();

            // Assert
            // Add assertions here based on your combined scenario requirements

            Assert.Equal(5, topAlarmCodesList.Count);
        }

        #endregion top 5 alarms tests
    }
}