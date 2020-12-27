using ExtensionMethods;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaperRace.Classes
{
    public static class GameSettings
    {
        private static IConfigurationRoot _configuration;

        /// <summary>
        /// Ширина фрагмента дороги
        /// </summary>
        public static int RoadWidth
        {
            get
            {
                var value = _configuration["RoadWidth"].ToInt();
                if (value == 0)
                {
                    value = DefaultSettings.RoadWidth;
                }
                return value;
            }
        }

        /// <summary>
        /// Точка машины на карте, X
        /// </summary>
        public static int UserPositionX
        {
            get
            {
                var value = _configuration["UserPosition:X"].ToInt();
                if (value == 0 || value % 20 != 0)
                {
                    value = DefaultSettings.UserPositionX;
                }

                return value;
            }
        }

        /// <summary>
        /// Точка машины на карте, Y
        /// </summary>
        public static int UserPositionY
        {
            get
            {
                var value = _configuration["UserPosition:Y"].ToInt();
                if (value == 0 || value % 20 != 0)
                {
                    value = DefaultSettings.UserPositionY;
                }

                return value;
            }
        }

        /// <summary>
        /// Количество фрагментов дороги
        /// </summary>
        public static int RoadElementsCount
        {
            get
            {
                var value = _configuration["RoadElementsCount"].ToInt();
                if (value == 0)
                {
                    value = DefaultSettings.RoadElementsCount;
                }

                return value;
            }
        }

        /// <summary>
        /// Максимальный угол поворота влево
        /// </summary>
        public static int MinAngle
        {
            get
            {
                var value = _configuration["MinAngle"].ToInt();
                if (value == 0)
                {
                    value = DefaultSettings.MinAngle;
                }

                return value;
            }
        }

        /// <summary>
        /// Максимальный угол поворота направо
        /// </summary>
        public static int MaxAngle
        {
            get
            {
                var value = _configuration["MaxAngle"].ToInt();
                if (value == 0)
                {
                    value = DefaultSettings.MaxAngle;
                }

                return value;
            }
        }

        /// <summary>
        /// Минимальное значение длины фрагмента дороги
        /// </summary>
        public static int MinRoadLength
        {
            get
            {
                var value = _configuration["MinRoadLength"].ToInt();
                if (value == 0)
                {
                    value = DefaultSettings.MinRoadLength;
                }

                return value;
            }
        }

        /// <summary>
        /// Максимальное значение длины фрагмента дороги
        /// </summary>
        public static int MaxRoadLength
        {
            get
            {
                var value = _configuration["MaxRoadLength"].ToInt();
                if (value == 0)
                {
                    value = DefaultSettings.MaxRoadLength;
                }

                return value;
            }
        }

        static GameSettings()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("game.config", optional: true).Build();
        }
    }

    /// <summary>
    /// Класс настроек по умолчанию
    /// </summary>
    public static class DefaultSettings
    {
        public static readonly int RoadWidth = 100;
        public static readonly int UserPositionX = 400;
        public static readonly int UserPositionY = 400;
        public static readonly int RoadElementsCount = 20;
        public static readonly int MinAngle = -45;
        public static readonly int MaxAngle = 45;
        public static readonly int MinRoadLength = 100;
        public static readonly int MaxRoadLength = 200;
    }
}
