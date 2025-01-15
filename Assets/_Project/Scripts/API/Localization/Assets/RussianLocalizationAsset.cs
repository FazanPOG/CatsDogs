using System;
using System.Collections.Generic;

namespace _Project.API
{
    public class RussianLocalizationAsset : ILocalizationAsset
    {
        private Dictionary<string, string> _translations;
        
        public RussianLocalizationAsset()
        {
            _translations = new Dictionary<string, string>()
            {
                [LocalizationKeys.PRESS_TO_START_KEY] = "Нажмите для начала",
                [LocalizationKeys.LEVEL_KEY] = "Уровень",
                [LocalizationKeys.HATS_KEY] = "Шляпы",
                [LocalizationKeys.UNLOCK_KEY] = "Открыть",
            };
        }
        
        public string GetTranslation(string key)
        {
            if(_translations.TryGetValue(key, out string value) == false)
                throw new Exception($"Missing localization key: {key} on localization asset: {nameof(RussianLocalizationAsset)}");

            return value;
        }
    }
}