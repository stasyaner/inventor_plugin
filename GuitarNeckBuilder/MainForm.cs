using System;
using System.ComponentModel;
using System.Windows.Forms;
using HeadstockTypes;
using Parts;
using Settings;

namespace GuitarNeckBuilder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            headstockTypeComboBox.DataSource = new BindingList<IHeadstock>()
            {
                new IbanezHeadstock(),
                new GibsonHeadstock(),
                new EspHeadstock()
            };
            headstockTypeComboBox.DisplayMember = "Name";
        }

        private void buildButton_Click(object sender, System.EventArgs e)
        {
            ISettings neckSettings = new NeckSettings();
            neckSettings.SetSetting(SettingName.AtNutWeight, Convert.ToInt32(atNutWightTextBox.Text));
            neckSettings.SetSetting(SettingName.AtLastFretWeight, Convert.ToInt32(atLastFretWeightTextBox.Text));
            neckSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(atNutHeightTextBox.Text));
            neckSettings.SetSetting(SettingName.AtTwelveFretHeight, Convert.ToInt32(atTwelveFretHeightTextBox.Text));
            neckSettings.SetSetting(SettingName.Length, Convert.ToInt32(lengthTextBox.Text));
            neckSettings.SetSetting(SettingName.FretNumber, Convert.ToInt32(fretNumberTextBox.Text));
            neckSettings.SetSetting(SettingName.Material, materialComboBox.SelectedIndex);

            ISettings fingerboardSettings = new FingerboardSettings();
            fingerboardSettings.SetSetting(SettingName.FingerboardMaterial, fingerboardMaterialComboBox.SelectedIndex);
            fingerboardSettings.SetSetting(SettingName.FingerboardRadius, Convert.ToInt32(fingerboardRadiusTextBox.Text));
            fingerboardSettings.SetSetting(SettingName.AtNutWeight, Convert.ToInt32(atNutWightTextBox.Text));
            fingerboardSettings.SetSetting(SettingName.AtLastFretWeight, Convert.ToInt32(atLastFretWeightTextBox.Text));
            fingerboardSettings.SetSetting(SettingName.FretNumber, Convert.ToInt32(fretNumberTextBox.Text));

            ISettings headstockSettings = new HeadstockSettings();
            headstockSettings.SetSetting(SettingName.AtNutWeight, Convert.ToInt32(atNutWightTextBox.Text));
            neckSettings.SetSetting(SettingName.AtNutHeight, Convert.ToInt32(atNutHeightTextBox.Text));

            IPart neckPart = new NeckPart();
            neckPart.Build(neckSettings);
        }
    }
}
