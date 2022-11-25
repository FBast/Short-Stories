using Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI {
    public class NewStoryUI : MonoBehaviour {

        [SerializeField] private GameObject _newStoryPanel;
        [SerializeField] private CanvasGroup _overlayCanvasGroup;
        [SerializeField] private InputField _storyNameInputField;
        
        public void Confirm() {
            StoryUI.CurrentStory = new Story {
                StoryName = _storyNameInputField.text
            };
            _newStoryPanel.SetActive(false);
            _overlayCanvasGroup.interactable = true;
        }

        public void Cancel() {
            _newStoryPanel.SetActive(false);
            _overlayCanvasGroup.interactable = true;
        }
        
    }
}