using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InventorAPI;
using PartsAssembler;
using Settings;

namespace GuitarNeckBuilder.View
{
    /// <summary>
    /// Класс главной формы программы
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Словарь для хранения текстбоксов и флага корректности.
        /// </summary>
        private readonly Dictionary<TextBox, bool> _textBoxDictionary = new Dictionary<TextBox, bool>();

        /// <summary>
        /// Поле ссылки на класс сборщика деталей
        /// </summary>
        private Assembler _assembler;

        /// <summary>
        /// Поле словаря с материалами
        /// </summary>
        private readonly Dictionary<string, string> _materialsDictionary = new Dictionary<string, string>()
        {
            {"Красное дерево", "Mahogany"},
            {"Клен", "Maple"},
            { "Ясень", "Ash" }
        };

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            MaterialComboBox.DataSource = new BindingSource(_materialsDictionary, null);
            MaterialComboBox.DisplayMember = "Key";

            FingerboardMaterialComboBox.DataSource = new BindingSource(_materialsDictionary, null);
            FingerboardMaterialComboBox.DisplayMember = "Key";

            //Заполняется словарь текстбоксов с флагами.
            foreach (Control control in
                WorkGroupBox.Controls.Cast<Control>().Where(control => control.GetType() == typeof(TextBox)))
            {
                _textBoxDictionary.Add((TextBox) control, false);
            }
        }

        /// <summary>
        /// Обработчик клика на кнопку построения грифа
        /// </summary>
        /// <param name="sender">Объект-отправитель события</param>
        /// <param name="e">Параметры события</param>
        private void BuildButton_Click(object sender, System.EventArgs e)
        {
            ////Если все текстбоксы не пустые
            //if (!_textBoxDictionary.ContainsValue(false))
            //{
                var inventorConnector = new InventorConnector();
            if (inventorConnector.ConnectionError != null)
            {
                MessageBox.Show(inventorConnector.ConnectionError, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                //Инициализация настроек
                #region SettingsInit

                ISettings neckSettings = new NeckSettings();
                neckSettings.SetSetting(SettingName.AtNutWidth, Convert.ToInt32(AtNutWidthTextBox.Text));
                neckSettings.SetSetting(SettingName.AtLastFretWidth, Convert.ToInt32(AtLastFretWidthTextBox.Text));
                neckSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightTextBox.Text));
                neckSettings.SetSetting(SettingName.AtTwelveFretHeight, Convert.ToInt32(AtTwelveFretHeightTextBox.Text));
                neckSettings.SetSetting(SettingName.Length, Convert.ToInt32(LengthTextBox.Text));
                neckSettings.SetSetting(SettingName.Material, MaterialComboBox.SelectedIndex);

                ISettings fingerboardSettings = new FingerboardSettings();
                fingerboardSettings.SetSetting(SettingName.FingerboardMaterial, FingerboardMaterialComboBox.SelectedIndex);
                fingerboardSettings.SetSetting(SettingName.FingerboardRadius, Convert.ToInt32(FingerboardRadiusTextBox.Text));
                fingerboardSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightTextBox.Text));
                fingerboardSettings.SetSetting(SettingName.AtNutWidth, Convert.ToInt32(AtNutWidthTextBox.Text));
                fingerboardSettings.SetSetting(SettingName.AtLastFretWidth, Convert.ToInt32(AtLastFretWidthTextBox.Text));
                fingerboardSettings.SetSetting(SettingName.FretNumber, Convert.ToInt32(FretNumberTextBox.Text));
                fingerboardSettings.SetSetting(SettingName.Length, Convert.ToInt32(LengthTextBox.Text));
                fingerboardSettings.SetSetting(SettingName.Inlay, Convert.ToByte(InlayCheckBox.Checked));

                ISettings headstockSettings = new HeadstockSettings();
                headstockSettings.SetSetting(SettingName.AtNutWidth, Convert.ToInt32(AtNutWidthTextBox.Text));
                headstockSettings.SetSetting(SettingName.ReverseHeadstock, Convert.ToByte(ReverseHeadstockCheckBox.Checked));
                headstockSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightTextBox.Text));
                headstockSettings.SetSetting(SettingName.Material, MaterialComboBox.SelectedIndex);

                ISettings inlaySettings = new InlaySettings();
                inlaySettings.SetSetting(SettingName.Inlay, Convert.ToByte(InlayCheckBox.Checked));

                ISettings fretSettings = new FretSettings();
                fretSettings.SetSetting(SettingName.FretWidth, Convert.ToInt32(FretWidthTextBox.Text));
                fretSettings.SetSetting(SettingName.FretHeight, Convert.ToInt32(FretHeightTextBox.Text));
                fretSettings.SetSetting(SettingName.AtNutWidth, Convert.ToInt32(AtNutWidthTextBox.Text));
                fretSettings.SetSetting(SettingName.FingerboardRadius, Convert.ToInt32(FingerboardRadiusTextBox.Text));
                fretSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightTextBox.Text));

                ISettings tunerSettings = new TunerSettings();
                tunerSettings.SetSetting(SettingName.TunerAngle, Convert.ToInt32(TunerAngleTextBox.Text));
                tunerSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightTextBox.Text));

                #endregion

                _assembler = new Assembler(new List<ISettings>()
                {
                    neckSettings,
                    fingerboardSettings,
                    headstockSettings,
                    inlaySettings,
                    fretSettings,
                    tunerSettings
                }, 
                inventorConnector);
                _assembler.Assembly();
            }
            //}
            ////Иначе подсвечиваем пустые
            //else
            //{
                //foreach (var textBoxCell in _textBoxDictionary)
                //{
                //    if (textBoxCell.Value == false)
                //    {
                //        workGroupBox.Controls[textBoxCell.Key.Name].BackColor = Color.LightCoral;
                //    }
                //}
                //MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        /// <summary>
        /// Обработчик нажатой клавиши приминительно к текстбоксам
        /// </summary>
        /// <param name="sender">Объект-отправитель события</param>
        /// <param name="e">Параметры события</param>
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Tab)
            {
                var thisTextBox = sender as TextBox;
                int thisTextBoxTextAsInt;

                if (string.IsNullOrWhiteSpace(thisTextBox.Text))
                {
                    thisTextBox.BackColor = Color.LightCoral;
                    mainToolTip.Show("Поле обязательно для заполнения.", thisTextBox, 1000);
                }
                else if (!int.TryParse(thisTextBox.Text, out thisTextBoxTextAsInt))
                {
                    thisTextBox.BackColor = Color.LightCoral;
                    mainToolTip.Show("Только целые числа.", thisTextBox, 1000);
                }
                else
                {
                    thisTextBox.BackColor = Color.White;
                    _textBoxDictionary[thisTextBox] = true;
                }
            }
        }

        /// <summary>
        /// Обработчик на уход фокуса с текстбокса
        /// </summary>
        /// <param name="sender">Объект-отправитель события</param>
        /// <param name="e">Параметры события</param>
        private void TextBox_Leave(object sender, EventArgs e)
        {
            var thisTextBox = sender as TextBox;
            int thisTextBoxTextAsInt;

            if (string.IsNullOrWhiteSpace(thisTextBox.Text))
            {
                thisTextBox.BackColor = Color.LightCoral;
                mainToolTip.Show("Поле обязательно для заполнения.", thisTextBox, 1000);
            }
            else if (!int.TryParse(thisTextBox.Text, out thisTextBoxTextAsInt))
            {
                thisTextBox.BackColor = Color.LightCoral;
                mainToolTip.Show("Только целые числа.", thisTextBox, 1000);
            }
            else
            {
                thisTextBox.BackColor = Color.White;
                _textBoxDictionary[thisTextBox] = true;
            }
        }

        /// <summary>
        /// Обработчик закрытия формы
        /// </summary>
        /// <param name="sender">Объект-отправитель события</param>
        /// <param name="e">Параметры события</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _assembler?.Close();
        }
    }
}
