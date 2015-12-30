using System.Collections.Generic;

namespace Settings
{
    public interface ISettings
    {
        void SetSetting(SettingName settingName, double settingValue);
        double GetSetting(SettingName settingName);
    }
}
