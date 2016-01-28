using System;
using NUnit.Framework;
using NUnit.Framework.Internal.Filters;
using Settings;

namespace UnitTests.Settings
{
    [TestFixture]
    public class NeckSettingsTests
    {
        [TestCase(SettingName.AtNutHeight, 13, TestName = "Задать толщину грифа на порожке = 13. Позитив.")]
        public void SetAtNutHeightPositive(SettingName settingName, int value)
        {
            var set = new NeckSettings();
            Assert.DoesNotThrow(() => set.SetSetting(settingName, value));
        }

        [TestCase(SettingName.AtNutHeight, -1, TestName = "Задать толщину грифа на порожке = -13. Негатив.")]
        public void SetAtNutHeightNegative(SettingName settingName, int value)
        {
            var set = new NeckSettings();
            Assert.Throws<ArgumentException>(() => set.SetSetting(settingName, value));
        }

        [TestCase(SettingName.AtNutHeight, 0, TestName = "Задать толщину грифа на порожке = 0. Негатив.")]
        public void SetAtNutHeightNull(SettingName settingName, int value)
        {
            var set = new NeckSettings();
            Assert.Throws<ArgumentException>(() => set.SetSetting(settingName, value));
        }

        [TestCase(SettingName.AtNutHeight, TestName = "Получить толщину грифа на порожке = 1.3. Позитив.")]
        public void GetAtNutHeight(SettingName settingName)
        {
            var set = new NeckSettings();
            set.SetSetting(settingName, 13);
            Assert.AreEqual(set.GetSetting(settingName), 1.3);
        }

        [TestCase(SettingName.Material, TestName = "Получить материал грифа = 1.0 (не должен делиться на 10, как остальные). Позитив.")]
        public void GetMaterial(SettingName settingName)
        {
            var set = new NeckSettings();
            set.SetSetting(settingName, 1);
            Assert.AreEqual(set.GetSetting(settingName), 1.0);
        }

        [TestCase(SettingName.Inlay, TestName = "Получить несуществующую настройку. Негатив.")]
        public void GetNotExisted(SettingName settingName)
        {
            var set = new NeckSettings();
            Assert.Throws<ArgumentException>(() => set.GetSetting(settingName));
        }
    }
}
