using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Data {
    [Serializable]
    public class Thumbnail {

        public string Title;
        public string Description;
        public List<Choice> Choices;

    }
}