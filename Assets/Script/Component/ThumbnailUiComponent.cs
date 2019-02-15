using System.Collections.Generic;
using Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Component
{
    public class ThumbnailUiComponent : MonoBehaviour {

        public InputField TitleInputField;
        public InputField DescriptionInputField;
        public Dropdown ThumbnailSelectorDropdown;
        public List<ChoiceUiComponent> ChoiceUiComponents = new List<ChoiceUiComponent>();
        public Transform ChoiceContent;
        public GameObject ChoicePrefab;
        public Story CurrentStory;

        public void Build(Story story)
        {
            CurrentStory = story;
        }
        
        public void Load(Story story, Thumbnail thumbnail)
        {
            CurrentStory = story;
            TitleInputField.text = thumbnail.Title;
            DescriptionInputField.text = thumbnail.Description;
            UpdateThumbnailDropdown();
            foreach (Choice choice in thumbnail.Choices)
            {
                GameObject instantiate = Instantiate(ChoicePrefab, ChoiceContent);
                ChoiceUiComponent choiceUiComponent = instantiate.GetComponent<ChoiceUiComponent>();
                choiceUiComponent.Load(this, story, choice);
                ChoiceUiComponents.Add(choiceUiComponent);
            }
        }
        
        public void Reset()
        {
            TitleInputField.text = string.Empty;
            DescriptionInputField.text = string.Empty;
            ChoiceUiComponents.ForEach(component => Destroy(component.gameObject));
            ChoiceUiComponents = new List<ChoiceUiComponent>();
        }
        
        public void UpdateThumbnailDropdown()
        {
            ThumbnailSelectorDropdown.ClearOptions();
            CurrentStory.Thumbnails.ForEach(thumbnail => ThumbnailSelectorDropdown.options.Add(new Dropdown.OptionData(thumbnail.Title)));
            ChoiceUiComponents.ForEach(component => component.UpdateThumbnailDropdown());
            ThumbnailSelectorDropdown.interactable = CurrentStory.Thumbnails.Count != 0;
        }
        
        public void AddChoice()
        {
            GameObject instantiate = Instantiate(ChoicePrefab, ChoiceContent);
            ChoiceUiComponent choiceUiComponent = instantiate.GetComponent<ChoiceUiComponent>();
            choiceUiComponent.Build(this, CurrentStory);
            ChoiceUiComponents.Add(choiceUiComponent);
        }
        
        public void RemoveChoice(ChoiceUiComponent choiceUiComponent)
        {
            ChoiceUiComponents.Remove(choiceUiComponent);
            Destroy(choiceUiComponent.gameObject);
        }
        
    }
}