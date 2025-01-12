using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

namespace _Project.Data
{
    public class PlayerPrefsGameplayDataProvider : IGameplayDataProvider
    {
        private const string GAMEPLAY_DATA_KEY = nameof(GAMEPLAY_DATA_KEY);
        
        private readonly DefaultDataConfig _config;

        private GameplayData _originData;
        
        public GameplayDataProxy GameplayDataProxy { get; private set; }

        public PlayerPrefsGameplayDataProvider(DefaultDataConfig config)
        {
            _config = config;
        }
        
        public GameplayDataProxy LoadGameplayData()
        {
            if (PlayerPrefs.HasKey(GAMEPLAY_DATA_KEY) == false)
            {
                GameplayDataProxy = CreateGameDataFromSettings();
                SaveGameplayData();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAMEPLAY_DATA_KEY);
                _originData = JsonUtility.FromJson<GameplayData>(json);
                GameplayDataProxy = new GameplayDataProxy(_originData);
                
                Debug.Log($"LOAD DATA: {json}");
            }
            
            return GameplayDataProxy;
        }

        public void SaveGameplayData()
        {
            var json = JsonUtility.ToJson(_originData, true);
            PlayerPrefs.SetString(GAMEPLAY_DATA_KEY, json);
            PlayerPrefs.Save();
            
            Debug.Log($"SAVE DATA: {json}");
        }

        private GameplayDataProxy CreateGameDataFromSettings()
        {
            List<string> unlockedSkinIDs = _config.DefaultUnlockedSkins.Select(x => x.ID).ToList();
            
            if(unlockedSkinIDs.IsEmpty())
                throw new Exception($"Must be unlocked minimum 1 skin, check {nameof(DefaultDataConfig)}");
            
            _originData = new GameplayData()
            {
                LevelNumber = _config.DefaultLevelNumber,
                MoneyAmount = _config.DefaultMoney,
                UnlockedSkinIDs = unlockedSkinIDs,
                CurrentSkinID = unlockedSkinIDs.First()
            };
            
            return new GameplayDataProxy(_originData);
        }
    }
}