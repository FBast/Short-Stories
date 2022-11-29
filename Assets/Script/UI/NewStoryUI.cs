using Script.Data;
using Script.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI {
    public class NewStoryUI : MonoBehaviour {
        
        [SerializeField] private TMP_InputField _storyNameInputField;
        [SerializeField] private Button _cancelButton;

        private void OnEnable() {
            _cancelButton.interactable = DataManager.StoryFiles.Count > 0;
        }

        public void Confirm() {
            StoryUI.CurrentStory = new Story(_storyNameInputField.text);
            gameObject.SetActive(false);
        }

        public void Cancel() {
            gameObject.SetActive(false);
        }
        
    }
}