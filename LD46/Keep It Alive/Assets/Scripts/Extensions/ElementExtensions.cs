using LPSoft.LD46.Enums;
using UnityEngine;

namespace LPSoft.LD46.Extensions
{
    public static class ElementExtensions
    {
        public static Color ToColor(this Element e)
        {
            switch (e)
            {
                case Element.General:
                    return Color.white;
                case Element.Fire:
                    return Color.red;
                case Element.Earth:
                    return Color.green;
                case Element.Lightning:
                    return Color.yellow;
                case Element.Water:
                    return Color.cyan;
                default:
                    return Color.black;
            }
        }
    }
}
