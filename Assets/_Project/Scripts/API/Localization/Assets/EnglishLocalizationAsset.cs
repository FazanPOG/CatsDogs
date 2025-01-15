using System;
using System.Collections.Generic;

namespace _Project.API
{
    public class EnglishLocalizationAsset : ILocalizationAsset
    {
        private Dictionary<string, string> _translations;
        
        public EnglishLocalizationAsset()
        {
            _translations = new Dictionary<string, string>()
            {
                [LocalizationKeys.PRESS_TO_START_KEY] = "Press to start",
                [LocalizationKeys.LEVEL_KEY] = "Level",
                [LocalizationKeys.HATS_KEY] = "Hats",
                [LocalizationKeys.UNLOCK_KEY] = "Unlock",
            };
        }
        
        public string GetTranslation(string key)
        {
            if(_translations.TryGetValue(key, out string value) == false)
                throw new Exception($"Missing localization key: {key} on localization asset: {nameof(EnglishLocalizationAsset)}");

            return value;
        }
    }
}