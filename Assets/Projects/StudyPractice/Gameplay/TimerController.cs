namespace Projects.StudyPractice.Gameplay
{
    public class TimerController
    {
        public TimerController(TimerView view, ShopSystem shopSystem)
        {
            shopSystem.tick += OnTick;
            return;

            void OnTick(float time)
            {
                view.Set(time, ShopSystem.TIME_TO_UPDATE);
            }
        }
    }
}