using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "Name Dataset")]
    public class Names : ScriptableObject
    {
        public List<string> FirstNames;
        public List<string> LastNames;
    }
}
