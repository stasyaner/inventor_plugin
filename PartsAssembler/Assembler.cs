using System.Collections.Generic;
using System.Linq;
using InventorAPI;
using Parts;
using Settings;

namespace PartsAssembler
{
    /// <summary>
    /// Класс сборщика деталей
    /// </summary>
    public class Assembler
    {
        /// <summary>
        /// Ссылка на коннектор
        /// </summary>
        private readonly InventorConnector _inventorConnector;

        /// <summary>
        /// Список деталей
        /// </summary>
        private readonly List<IPart> _parts;

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="settings">Список настроек</param>
        /// <param name="inventorConnector">Ссылка на коннектор</param>
        public Assembler(List<ISettings> settings, InventorConnector inventorConnector)
        {
            _inventorConnector = inventorConnector;
            _parts = new List<IPart>()
                {
                    //new NeckPart(settings.First(setting => setting.GetType() == typeof(NeckSettings)), _inventorConnector),
                    //new FingerboardPart(settings.First(setting => setting.GetType() == typeof(FingerboardSettings)), _inventorConnector),
                    //new FretPart(settings.First(setting => setting.GetType() == typeof(FretSettings)), _inventorConnector),
                    //new InlayPart(settings.First(setting => setting.GetType() == typeof(InlaySettings)), _inventorConnector),
                    new HeadstockPart(settings.First(setting => setting.GetType() == typeof(HeadstockSettings)), _inventorConnector)
                };
        }

        /// <summary>
        /// Метод сборки деталей
        /// </summary>
        public void Assembly()
        {
            foreach (var part in _parts)
            {
                part.Build();
            }
        }

        /// <summary>
        /// Метод закрытия документа сборки, документов деталей и инвентора
        /// </summary>
        public void Close()
        {
            foreach (var part in _parts)
            {
                part.Close();
            }
            _inventorConnector.InventorApplication.Quit();
        }
    }
}
