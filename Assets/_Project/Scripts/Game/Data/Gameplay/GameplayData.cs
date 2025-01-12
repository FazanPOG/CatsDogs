using System;
using System.Collections.Generic;

namespace _Project.Data
{
    [Serializable]
    public class GameplayData
    {
        public int MoneyAmount;
        public int LevelNumber;
        public List<string> UnlockedSkinIDs;
        public string CurrentSkinID;
    }
}