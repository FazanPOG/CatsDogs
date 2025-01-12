using System;
using System.Linq;
using ObservableCollections;
using R3;

namespace _Project.Data
{
    public class GameplayDataProxy
    {
        public readonly ReactiveProperty<int> MoneyAmount = new ReactiveProperty<int>();
        public readonly ReactiveProperty<int> LevelNumber = new ReactiveProperty<int>();
        public readonly ObservableList<string> UnlockedSkinIDs = new ObservableList<string>();
        public readonly ReactiveProperty<string> CurrentSkinID = new ReactiveProperty<string>();

        public GameplayDataProxy(GameplayData gameplayData)
        {
            MoneyAmount.Value = gameplayData.MoneyAmount;
            LevelNumber.Value = gameplayData.LevelNumber;
            foreach (var skinID in gameplayData.UnlockedSkinIDs)
                UnlockedSkinIDs.Add(skinID);
            CurrentSkinID.Value = gameplayData.CurrentSkinID;
            
            MoneyAmount.Subscribe(newValue =>
            {
                if(newValue < 0)
                    throw new Exception();
                    
                gameplayData.MoneyAmount = newValue;
            });
            
            LevelNumber.Subscribe(newValue =>
            {
                if(newValue < 1)
                    throw new Exception();
                
                gameplayData.LevelNumber = newValue;
            });
            
            UnlockedSkinIDs.ObserveAdd().Subscribe(newID =>
            {
                var id = newID.Value;
                
                if(gameplayData.UnlockedSkinIDs.Any(x => x == id))
                    throw new Exception();
                
                gameplayData.UnlockedSkinIDs.Add(id);
            });
            
            UnlockedSkinIDs.ObserveRemove().Subscribe(removedID =>
            {
                var id = removedID.Value;
                
                if(gameplayData.UnlockedSkinIDs.Any(x => x == id) == false)
                    throw new Exception();
                
                gameplayData.UnlockedSkinIDs.Remove(id);
            });

            CurrentSkinID.Subscribe(newID =>
            {
                if (gameplayData.UnlockedSkinIDs.Contains(newID) == false)
                    throw new Exception($"Try select locked skin, ID: {newID}");

                gameplayData.CurrentSkinID = newID;
            });
        }
    }
}