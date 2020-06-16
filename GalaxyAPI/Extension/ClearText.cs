using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GalaxyAPI.Extension
{
    internal static class ClearText
    {
        /// <summary>
        /// Удаляем из текста лишние пробелы (в конце и начале слова, а так же более одного пробела везде).
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReplaceSpace(this string text)
        {
            try
            {
                var pattern = @"\s{2,}";
                var target = " ";
                var regex = new Regex(pattern);
                return regex.Replace(text.Trim(), target);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (RegexMatchTimeoutException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
