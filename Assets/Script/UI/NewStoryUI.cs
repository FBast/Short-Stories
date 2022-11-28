using Script.Data;
using TMPro;
using UnityEngine;

namespace Script.UI {
    public class NewStoryUI : MonoBehaviour {
        
        [SerializeField] private TMP_InputField _storyNameInputField;
        
        public void Confirm() {
            StoryUI.CurrentStory = new Story(_storyNameInputField.text);
            gameObject.SetActive(false);
        }

        public void Cancel() {
            gameObject.SetActive(false);
        }
        
    }
}