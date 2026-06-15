namespace Projects.StudyPractice.Gameplay
{
    public class MoneyController
    {
        public MoneyController(MoneyMenuView view, MoneyModel model)
        {
            view.AddMoneyButton.onClick.AddListener(() => model.Add(250));
            model.moneyChanged += v => view.Text.SetText(v.ToString());
            model.Update();
        }
    }
}