using System;
using System.Collections.Generic;
using Script.Component;
using UnityEngine;

namespace Script.Data {
    [Serializable]
    public class Thumbnail : ScriptableObject {

        public string Title;
        public string Description;
        public List<Choice> Choices;

        public void Build(ThumbnailUiComponent thumbnailUiComponent) 
        {
            Title = thumbnailUiComponent.TitleInputField.text;
            Description = thumbnailUiComponent.DescriptionInputField.text;
            Choices = new List<Choice>();
            foreach (ChoiceUiComponent choiceUiComponent in thumbnailUiComponent.ChoiceUiComponents)
            {
                Choice choice = CreateInstance<Choice>();
                choice.Build(choiceUiComponent);
                Choices.Add(choice);
            }
        }
        
    }
}