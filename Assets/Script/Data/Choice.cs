using System;
using Script.Component;
using UnityEngine;

namespace Script.Data {
    [Serializable]
    public class Choice : ScriptableObject {

        public string Description;
        public Thumbnail Link;
        
        public void Build(ChoiceUiComponent choiceUiComponent)
        {
            Description = choiceUiComponent.DescriptionInputField.text;
            Link = choiceUiComponent.GetThumbnailForDropdown();
        }

    }
}