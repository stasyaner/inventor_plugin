using System;
using System.Runtime.InteropServices;
using Inventor;
using Application = Inventor.Application;

namespace InventorAPI
{
    /// <summary>
    /// Класс коннектора с api inventor'a
    /// </summary>
    public class InventorConnector
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public InventorConnector()
        {
            InventorApplication = null;
            try
            {
                InventorApplication = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
                InventorApplication.Visible = true;
            }
            catch (Exception)
            {
                try
                {
                    Type invAppType = Type.GetTypeFromProgID("Inventor.Application");
                    InventorApplication = (Application)Activator.CreateInstance(invAppType);
                    InventorApplication.Visible = true;
                }
                catch (Exception)
                {
                    ConnectionError = @"Не получилось запустить инвентор.
                        Проверьте установен ли Autodesk Inventor 2016";
                }
            }
        }

        /// <summary>
        /// InventorApplication свойство.
        /// </summary>
        /// <value>
        /// Содержит указатель на приложение.
        /// </value>
        public Inventor.Application InventorApplication { get; }

        /// <summary>
        /// ConnectionError свойтво.
        /// </summary>
        public string ConnectionError;

        /// <summary>
        /// Метод для построения скетча с помощью рабочих плоскостей.
        /// На вход подается номер рабочей плоскости от которой будет сдвиг и сам сдвиг.
        /// //[1 - ZY ; 2 - ZX ; 3 - XY] - номера плоскостей
        /// </summary>
        /// <param name="n">Номер плоскости</param>
        /// <param name="offset">Сдвиг</param>
        /// <param name="partDocument">Ссылка на документ детали</param>
        /// <returns>Полученный скетч</returns>
        public PlanarSketch MakeNewSketch(int n, double offset, PartDocument partDocument)
        {
            WorkPlane mainPlane = partDocument.ComponentDefinition.WorkPlanes[n];
            WorkPlane offsetPlane = partDocument.ComponentDefinition.WorkPlanes.AddByPlaneAndOffset(mainPlane, offset);
            PlanarSketch sketch = partDocument.ComponentDefinition.Sketches.Add(offsetPlane);
            offsetPlane.Visible = false;

            return sketch;
        }

        /// <summary>
        /// Метод для смены материала детали
        /// </summary>
        /// <param name="partDocument">Ссылка на документ детали</param>
        /// <param name="materialName">Название материала</param>
        public void ChangeMaterial(PartDocument partDocument, string materialName)
        {
            Materials materialsLibrary = partDocument.Materials;
            Material myMaterial = materialsLibrary[materialName];
            Material tempMaterial = myMaterial.StyleLocation == StyleLocationEnum.kLibraryStyleLocation
                ? myMaterial.ConvertToLocal()
                : myMaterial;
            partDocument.ComponentDefinition.Material = tempMaterial;
            partDocument.Update();
        }
    }
}