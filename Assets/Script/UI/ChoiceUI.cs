using System.Collections.Generic;
using Script.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.UI {
    public class ChoiceUI : MonoBehaviour {

        [SerializeField] private TMP_InputField _descriptionInputField;
        [FormerlySerializedAs("_linkThumbnailDropdown")] [SerializeField] private TMP_Dropdown _linkedThumbnailDropdown;

        /// <summary>
        /// Load an existing choice
        /// </summary>
        /// <param name="choice"></param>
        public void Load(Choice choice) {
            UpdateLinkedThumbnailDropdown();
            _descriptionInputField.text = choice.Description;
            _linkedThumbnailDropdown.value = StoryUI.CurrentStory.Thumbnails.IndexOf(choice.Link);
        }

        /// <summary>
        /// Convert the ChoiceUI into Choice
        /// </summary>
        /// <returns></returns>
        public Choice ToChoice() {
            return _linkedThumbnailDropdown.value < StoryUI.CurrentStory.Thumbnails.Count ? 
                new Choice(_descriptionInputField.text, StoryUI.CurrentStory.Thumbnails[_linkedThumbnailDropdown.value]) : 
                new Choice(_descriptionInputField.text, null);
        }
        
        /// <summary>
        /// Update the content of the linked thumbnail dropdown
        /// </summary>
        public void UpdateLinkedThumbnailDropdown() {
            _linkedThumbnailDropdown.options = new List<TMP_Dropdown.OptionData>();
            foreach (Thumbnail thumbnail in StoryUI.CurrentStory.Thumbnails) {
                _linkedThumbnailDropdown.options.Add(new TMP_Dropdown.OptionData(thumbnail.Title));
            }
        }

        /// <summary>
        /// Destroy the current choice
        /// </summary>
        public void RemoveChoice() {
            Destroy(gameObject);
        }

    }
}