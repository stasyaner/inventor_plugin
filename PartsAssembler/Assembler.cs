using System.Collections.Generic;
using System.Linq;
using InventorAPI;
using Parts;
using Settings;

namespace PartsAssembler
{
    public class Assembler
    {
        private readonly InventorConnector _inventorConnector;
        private readonly List<IPart> _parts;

        public Assembler(List<ISettings> settings, InventorConnector inventorConnector)
        {
            _inventorConnector = inventorConnector;
            _parts = new List<IPart>()
                {
                    new NeckPart(settings.First(setting => setting.GetType() == typeof(NeckSettings)), _inventorConnector),
                    new FingerboardPart(settings.First(setting => setting.GetType() == typeof(FingerboardSettings)), _inventorConnector),
                    //new FretPart(settings.First(setting => setting.GetType() == typeof(FretSettings)), _inventorConnector),
                    new InlayPart(settings.First(setting => setting.GetType() == typeof(InlaySettings)), _inventorConnector),
                    new HeadstockPart(settings.First(setting => setting.GetType() == typeof(HeadstockSettings)), _inventorConnector)
                };
        }

        public void Assembly()
        {
            foreach (var part in _parts)
            {
                part.Build();
            }
        }

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
