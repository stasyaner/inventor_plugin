using Inventor;
using InventorAPI;
using Settings;

namespace Parts
{
    /// <summary>
    /// Класс детали инкрустации грифа
    /// </summary>
    public class InlayPart : IPart
    {
        /// <summary>
        /// Поле ссылки на коннектор к инвентору
        /// </summary>
        private readonly InventorConnector _inventorConnector;

        /// <summary>
        /// Ссылка на документ детали.
        /// </summary>
        private readonly PartDocument _partDoc;

        /// <summary>
        /// Ссылка на описание компонентов документа детали
        /// </summary>
        public PartComponentDefinition PartDocumentComponentDefinition => _partDoc.ComponentDefinition;

        /// <summary>
        /// Ссылка на настройки детали
        /// </summary>
        private readonly ISettings _settings;

        /// <summary>
        /// Свойство для получения состояния инкрустации (активна/не активна)
        /// </summary>
        public bool Active => _settings.GetSetting(SettingName.Inlay) == 1;

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="inventorConnector"></param>
        public InlayPart(ISettings settings, InventorConnector inventorConnector)
        {
            _settings = settings;
            if (Active)
            {
                _inventorConnector = inventorConnector;
                _partDoc = (PartDocument) inventorConnector.InventorApplication.Documents.Add(
                    DocumentTypeEnum.kPartDocumentObject,
                    inventorConnector.InventorApplication.FileManager.GetTemplateFile(
                        DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure));
            }
        }

        /// <summary>
        /// Метод построения инкрустации грифа
        /// </summary>
        public void Build()
        {
            if (Active)
            {
                //Создаем скетч на рабочей плоскости ZX.
                PlanarSketch inlaySketch = _inventorConnector.MakeNewSketch(2, 0, _partDoc);

                // Создаем точки
                Point2d inlayPoint1 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d();
                Point2d inlayPoint2 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(0.1, 0.1);

                //Рисуем прямоугольник по трем точкам
                inlaySketch.SketchLines.AddAsTwoPointCenteredRectangle(inlayPoint1, inlayPoint2);

                //Выдавливаем прямоугольник
                ExtrudeDefinition inlayExtrudeDef = PartDocumentComponentDefinition.Features.ExtrudeFeatures
                    .CreateExtrudeDefinition(inlaySketch.Profiles.AddForSolid(), PartFeatureOperationEnum.kNewBodyOperation);
                inlayExtrudeDef.SetDistanceExtent(0.12, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
                PartDocumentComponentDefinition.Features.ExtrudeFeatures.Add(inlayExtrudeDef);

                //Меняем материал
                _inventorConnector.ChangeMaterial(_partDoc, "Polystyrene");
            }
        }

        /// <summary>
        /// Метод, который закрывает текущий документ без сохранения.
        /// </summary>
        public void Close()
        {
            if ((_settings != null) && (_settings.GetSetting(SettingName.Inlay) == 1))
            {
                _partDoc.Close(true);
            }
        }
    }
}