namespace _Project.UI
{
    public class FortuneWheelRewardViewPresenter
    {
        public FortuneWheelRewardViewPresenter(
            FortuneWheelRewardView view, 
            FortuneWheelRewardConfig config, 
            SpriteReferencesConfig referencesConfig)
        {
            view.SetRewardSprite(referencesConfig.CurrencySprite);
            view.SetRewardText($"+{config.RewardValue}");
        }
    }
}