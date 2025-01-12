using System;
using YG;

namespace _Project.API
{
    public class YandexGamesADService : IADService
    {
        public bool IsFullscreenAvailable => true;
        public bool IsRewardedAvailable => true;
        
        public event Action<bool> OnFullscreenClose;
        public event Action<string> OnRewardedReward;

        private string _currentRewardID;

        public YandexGamesADService()
        {
            YandexGame.RewardVideoEvent += RewardVideoEvent;
            YandexGame.CloseFullAdEvent += OnFullscreenCloseInvoke;
        }

        private void RewardVideoEvent(int _)
        {
            if(string.IsNullOrEmpty(_currentRewardID))
                throw new NullReferenceException($"Missing AD reward");
            
            OnRewardedReward?.Invoke(_currentRewardID);
            _currentRewardID = String.Empty;
        }

        private void OnFullscreenCloseInvoke() => OnFullscreenClose?.Invoke(true);

        public void ShowFullscreen()
        {
            YandexGame.FullscreenShow();
        }

        public void ShowRewarded(string rewardID)
        {
            _currentRewardID = rewardID;
            YandexGame.RewVideoShow(0);
        }
    }
}