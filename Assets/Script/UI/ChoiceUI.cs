using System;
using System.Linq;
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
            if (string.IsNullOrEmpty(choice.ThumbnailGuid)) {
                _linkedThumbnailDropdown.value = 0;
            }
            else {
                Thumbnail firstOrDefault = StoryUI.CurrentStory.Thumbnails.FirstOrDefault(thumbnail => thumbnail.Guid == choice.ThumbnailGuid);
                if (firstOrDefault == null)
                    throw new Exception("Cannot find any thumbnail with guid " + choice.ThumbnailGuid);
                _linkedThumbnailDropdown.value = StoryUI.CurrentStory.Thumbnails.IndexOf(firstOrDefault);
            }
        }

        /// <summary>
        /// Convert the ChoiceUI into Choice
        /// </summary>
        /// <returns></returns>
        public Choice ToChoice() {
            if (_linkedThumbnailDropdown.value == 0) return new Choice(_descriptionInputField.text, null);
            return _linkedThumbnailDropdown.value <= StoryUI.CurrentStory.Thumbnails.Count ? 
                new Choice(_descriptionInputField.text, StoryUI.CurrentStory.Thumbnails[_linkedThumbnailDropdown.value - 1].Guid) : 
                new Choice(_descriptionInputField.text, string.Empty);
        }
        
        /// <summary>
        /// Update the content of the linked thumbnail dropdown
        /// </summary>
        public void UpdateLinkedThumbnailDropdown() {
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