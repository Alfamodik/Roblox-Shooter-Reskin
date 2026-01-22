using UnityEngine;

namespace GoldMagnat
{
    public class Tool : MonoBehaviour
    {
        [SerializeField] private ToolData toolData;
        public ToolData ToolData => toolData;
    }
}
