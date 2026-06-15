using UnityEngine;

namespace Projects.StudyPractice.Gameplay
{
    public class PauseMenuController
    {
        public PauseMenuController(GameplayUIView UI, PauseMenuView view, SettingsView settingsView)
        {
            var verificationView = Object.Instantiate(Resources.Load<VerificationView>("VerificationPopup"), UI.transform);
            verificationView.Initialize(verificationView.transform.position);

            view.ContinueButton.onClick.AddListener(view.Close);
            view.SettingsButton.onClick.AddListener(()=>
            {
                settingsView.Toggle();
                view.Close();
            });
            view.BackButton.onClick.AddListener(() =>
            {
                verificationView.Open();
                view.Close();
            });

            verificationView.Yes.onClick.AddListener(Root.Root.Instance.LoadMainMenu);
            verificationView.No.onClick.AddListener(() =>
            {
                verificationView.Close();
                view.Open();
            });
        }
    }
}