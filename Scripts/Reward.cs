using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKCommon
{
    [System.Serializable]
    public class Reward
    {
        public string Category { get; set; }

        public string Type { get; set; }

        public uint Count { get; set; }
    }
}