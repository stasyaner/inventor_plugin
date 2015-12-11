using System.Collections.Generic;

namespace Settings
{
    public interface ISettings
    {
        void SetSetting(SettingName settingName, int settingValue);
        int GetSetting(SettingName settingName);
    }
}
