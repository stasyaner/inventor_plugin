using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
                    //new NeckPart(settings.First(p => p.GetType() == typeof(NeckSettings)), _inventorConnector),
                    new FingerboardPart(settings.First(p => p.GetType() == typeof(FingerboardSettings)), _inventorConnector),
                    new FretPart(),
                    new HeadstockPart()
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
