using System.Collections.Generic;
using Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Component
{
    public class ChoiceUiComponent : MonoBehaviour {

        public InputField DescriptionInputField;
        public Dropdown LinkThumbnailDropdown;
        public Story CurrentStory;
        public ThumbnailUiComponent ThumbnailUiComponent;

        public void Build(ThumbnailUiComponent thumbnailUiComponent, Story story)
        {
            ThumbnailUiComponent = thumbnailUiComponent;
            CurrentStory = story;
        }
        
        public void Load(ThumbnailUiComponent thumbnailUiComponent, Story story, Choice choice)
        {
            ThumbnailUiComponent = thumbnailUiComponent;
            CurrentStory = story;
            DescriptionInputField.text = choice.Description;
            LinkThumbnailDropdown.value = story.Thumbnails.IndexOf(choice.Link);
        }
        
        public void UpdateThumbnailDropdown()
        {
            LinkThumbnailDropdown.options = new List<Dropdown.OptionData>();
            foreach (Thumbnail thumbnail in CurrentStory.Thumbnails)
            {
                LinkThumbnailDropdown.options.Add(new Dropdown.OptionData(thumbnail.Title));
            }
        }

        public void RemoveChoice()
        {
            ThumbnailUiComponent.RemoveChoice(this);
        }
        
        public Thumbnail GetThumbnailForDropdown()
        {
            return CurrentStory.Thumbnails[LinkThumbnailDropdown.value];
        } 
        
    }
}
