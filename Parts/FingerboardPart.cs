using Inventor;
using InventorAPI;
using Settings;

namespace Parts
{
    public class FingerboardPart : IPart
    {
        /// <summary>
        /// Поле ссылки на коннектор к инвентору
        /// </summary>
        private readonly InventorConnector _invetorConnector;

        /// <summary>
        /// Ссылка на документ детали.
        /// </summary>
        private readonly PartDocument _partDoc;

        /// <summary>
        /// Ссылка на настройки детали
        /// </summary>
        private readonly ISettings _settings;

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="inventorConnector"></param>
        public FingerboardPart(ISettings settings, InventorConnector inventorConnector)
        {
            _settings = settings;
            _invetorConnector = inventorConnector;
            _partDoc = (PartDocument)inventorConnector.InventorApplication.Documents.Add(
                DocumentTypeEnum.kPartDocumentObject, inventorConnector.InventorApplication.FileManager.GetTemplateFile(
                    DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure));
        }

        /// <summary>
        /// Метод построения накладки грифа
        /// </summary>
        public void Build()
        {
            //Создаем скетч на рабочей плоскости XY.
            PlanarSketch nutSketch = _invetorConnector.MakeNewSketch(3, 0, _partDoc);
        }

        /// <summary>
        /// Метод, который закрывает текущий документ без сохранения.
        /// </summary>
        public void Close()
        {
            _partDoc.Close(true);
        }
    }
}