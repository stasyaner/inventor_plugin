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
        private readonly Dictionary<NumericUpDown, bool> _numericUpDownDictionary = new Dictionary<NumericUpDown, bool>();

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
                WorkGroupBox.Controls.Cast<Control>().Where(control => control.GetType() == typeof(NumericUpDown)))
            {
                _numericUpDownDictionary.Add((NumericUpDown) control, true);
            }
        }

        /// <summary>
        /// Обработчик клика на кнопку построения грифа
        /// </summary>
        /// <param name="sender">Объект-отправитель события</param>
        /// <param name="e">Параметры события</param>
        private void BuildButton_Click(object sender, System.EventArgs e)
        {
            if (!_numericUpDownDictionary.ContainsValue(false))
            {
                var inventorConnector = new InventorConnector();
                if (inventorConnector.ConnectionError != null)
                {
                    MessageBox.Show(inventorConnector.ConnectionError, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    #region SettingsInit

                    ISettings neckSettings = new NeckSettings();
                    neckSettings.SetSetting(SettingName.AtNutWidth, Convert.ToInt32(AtNutWidthNumericUpDown.Value));
                    neckSettings.SetSetting(SettingName.AtLastFretWidth, Convert.ToInt32(AtLastFretWidthNumericUpDown.Value));
                    neckSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightNumericUpDown.Value));
                    neckSettings.SetSetting(SettingName.AtTwelveFretHeight, Convert.ToInt32(AtTwelveFretHeightNumericUpDown.Value));
                    neckSettings.SetSetting(SettingName.Length, Convert.ToInt32(LengthNumericUpDown.Value));
                    neckSettings.SetSetting(SettingName.Material, MaterialComboBox.SelectedIndex);

                    ISettings fingerboardSettings = new FingerboardSettings();
                    fingerboardSettings.SetSetting(SettingName.FingerboardMaterial, FingerboardMaterialComboBox.SelectedIndex);
                    fingerboardSettings.SetSetting(SettingName.FingerboardRadius, Convert.ToInt32(FingerboardRadiusNumericUpDown.Value));
                    fingerboardSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightNumericUpDown.Value));
                    fingerboardSettings.SetSetting(SettingName.AtNutWidth, Convert.ToInt32(AtNutWidthNumericUpDown.Value));
                    fingerboardSettings.SetSetting(SettingName.AtLastFretWidth, Convert.ToInt32(AtLastFretWidthNumericUpDown.Value));
                    fingerboardSettings.SetSetting(SettingName.FretNumber, Convert.ToInt32(FretNumberNumericUpDown.Value));
                    fingerboardSettings.SetSetting(SettingName.Length, Convert.ToInt32(LengthNumericUpDown.Value));
                    fingerboardSettings.SetSetting(SettingName.Inlay, Convert.ToByte(InlayCheckBox.Checked));

                    ISettings headstockSettings = new HeadstockSettings();
                    headstockSettings.SetSetting(SettingName.AtNutWidth, Convert.ToInt32(AtNutWidthNumericUpDown.Value));
                    headstockSettings.SetSetting(SettingName.ReverseHeadstock, Convert.ToByte(ReverseHeadstockCheckBox.Checked));
                    headstockSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightNumericUpDown.Value));
                    headstockSettings.SetSetting(SettingName.Material, MaterialComboBox.SelectedIndex);

                    ISettings inlaySettings = new InlaySettings();
                    inlaySettings.SetSetting(SettingName.Inlay, Convert.ToByte(InlayCheckBox.Checked));

                    ISettings fretSettings = new FretSettings();
                    fretSettings.SetSetting(SettingName.FretWidth, Convert.ToInt32(FretWidthNumericUpDown.Value));
                    fretSettings.SetSetting(SettingName.FretHeight, Convert.ToInt32(FretHeightNumericUpDown.Value));
                    fretSettings.SetSetting(SettingName.AtNutWidth, Convert.ToInt32(AtNutWidthNumericUpDown.Value));
                    fretSettings.SetSetting(SettingName.FingerboardRadius, Convert.ToInt32(FingerboardRadiusNumericUpDown.Value));
                    fretSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightNumericUpDown.Value));

                    ISettings tunerSettings = new TunerSettings();
                    tunerSettings.SetSetting(SettingName.TunerAngle, Convert.ToInt32(TunersAngleNumericUpDown.Value));
                    tunerSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(AtNutHeightNumericUpDown.Value));

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
            }
            else
            {
                foreach (var textBoxCell in _numericUpDownDictionary)
                {
                    if (textBoxCell.Value == false)
                    {
                        WorkGroupBox.Controls[textBoxCell.Key.Name].BackColor = Color.LightCoral;
                    }
                }
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатой клавиши приминительно к текстбоксам
        /// </summary>
        /// <param name="sender">Объект-отправитель события</param>
        /// <param name="e">Параметры события</param>
        private void NumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Tab)
            {
                var thisTextBox = sender as NumericUpDown;
                int thisTextBoxTextAsInt;

                if (string.IsNullOrWhiteSpace(thisTextBox.Text))
                {
                    _numericUpDownDictionary[thisTextBox] = false;
                    thisTextBox.BackColor = Color.LightCoral;
                    mainToolTip.Show("Поле обязательно для заполнения.", thisTextBox, 1000);
                }
                else if (!int.TryParse(thisTextBox.Text, out thisTextBoxTextAsInt))
                {
                    _numericUpDownDictionary[thisTextBox] = false;
                    thisTextBox.BackColor = Color.LightCoral;
                    mainToolTip.Show("Только целые числа.", thisTextBox, 1000);
                }
                else
                {
                    thisTextBox.BackColor = Color.White;
                    _numericUpDownDictionary[thisTextBox] = true;
                }
            }
        }

        /// <summary>
        /// Обработчик на уход фокуса с текстбокса
        /// </summary>
        /// <param name="sender">Объект-отправитель события</param>
        /// <param name="e">Параметры события</param>
        private void NumericUpDown_Leave(object sender, EventArgs e)
        {
            var thisTextBox = sender as NumericUpDown;
            int thisTextBoxTextAsInt;

            if (string.IsNullOrWhiteSpace(thisTextBox.Text))
            {
                _numericUpDownDictionary[thisTextBox] = false;
                thisTextBox.BackColor = Color.LightCoral;
                mainToolTip.Show("Поле обязательно для заполнения.", thisTextBox, 3000);
            }
            else if (!int.TryParse(thisTextBox.Text, out thisTextBoxTextAsInt))
            {
                _numericUpDownDictionary[thisTextBox] = false;
                thisTextBox.BackColor = Color.LightCoral;
                mainToolTip.Show("Только целые числа.", thisTextBox, 3000);
            }
            else
            {
                thisTextBox.BackColor = Color.White;
                _numericUpDownDictionary[thisTextBox] = true;
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
