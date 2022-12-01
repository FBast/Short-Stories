using Script.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI {
    public class NewStoryUI : MonoBehaviour {

        [SerializeField] private StoryUI _storyUI;
        [SerializeField] private TMP_InputField _storyNameInputField;
        [SerializeField] private Button _cancelButton;

        private void OnEnable() {
            _cancelButton.interactable = DataManager.StoryFiles.Count > 0;
        }

        public void Confirm() {
            _storyUI.New(_storyNameInputField.text);
            _storyNameInputField.text = string.Empty;
            gameObject.SetActive(false);
        }

        public void Cancel() {
            gameObject.SetActive(false);
        }
        
    }
}