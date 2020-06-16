using GalaxyAPI.Services.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GalaxyAPI.Extension
{
    internal static class XmlParse
    {
        /// <summary>
        /// Получает секции trans-unit xlif-файла, содержащие source (исх. текст) и target (перевод).
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TransUnit> GetTransUnits(this XDocument document)
        {
            var result = new List<TransUnit>();
            var df = document.Root.Name.Namespace;
            var transElemens = document.Descendants(df + "trans-unit").OrderBy(t => t.Attribute("id").Value);

            try
            {
                var id = 0;
                foreach (var item in transElemens)
                {
                    result.Add(new TransUnit()
                    {
                        ID = int.Parse(item.Attribute("id").Value),
                        LocalID = ++id,
                        Source = item.Element(df + "source").Value,
                        Target = item.Element(df + "target").Value
                    });
                }
            }
            catch (System.ArgumentNullException ex)
            {
                throw ex;
            }
            catch (System.FormatException ex)
            {
                throw ex;
            }
            catch (System.OverflowException ex)
            {
                throw ex;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
