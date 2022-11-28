using System.Collections.Generic;
using Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI {
    public class ChoiceUI : MonoBehaviour {

        [SerializeField] private InputField _descriptionInputField;
        [SerializeField] private Dropdown _linkThumbnailDropdown;
        [SerializeField] private ThumbnailUI _thumbnailUI;

        public void Build(ThumbnailUI thumbnailUI) {
            _thumbnailUI = thumbnailUI;
        }
        
        public void Load(ThumbnailUI thumbnailUI, Choice choice) {
            _thumbnailUI = thumbnailUI;
            _descriptionInputField.text = choice.Description;
            _linkThumbnailDropdown.value = StoryUI.CurrentStory.Thumbnails.IndexOf(choice.Link);
        }

        public Choice ToChoice() {
            return new Choice(_descriptionInputField.text, GetThumbnailForDropdown());
        }
        
        public void UpdateThumbnailDropdown() {
            _linkThumbnailDropdown.options = new List<Dropdown.OptionData>();
            foreach (Thumbnail thumbnail in StoryUI.CurrentStory.Thumbnails) {
                _linkThumbnailDropdown.options.Add(new Dropdown.OptionData(thumbnail.Title));
            }
        }

        public void RemoveChoice() {
            _thumbnailUI.RemoveChoice(this);
        }
        
        public Thumbnail GetThumbnailForDropdown() {
            return StoryUI.CurrentStory.Thumbnails[_linkThumbnailDropdown.value];
        }

    }
}
