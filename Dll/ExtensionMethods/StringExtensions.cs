using System;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        /// <summary>
        /// Конвертация строки в число
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int ToInt(this string x)
        {
            if (x.IsEmpty()) return 0;

            var result = 0;
            int.TryParse(x, out result);

            return result;
        }

        /// <summary>
        /// Проверка строки на пустое значение
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string x)
        {
            return string.IsNullOrEmpty(x);
        }
    }
}
