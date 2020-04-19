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
                    return new Color(0.4622f, 0.1527f, 0.0f, 1.0f);
                case Element.Lightning:
                    return Color.yellow;
                case Element.Water:
                    return Color.cyan;
                default:
                    return Color.black;
            }
        }

        public static float Compare(this Element e, Element other, float currentDamage, float multiplier)
        {
            switch (e)
            {
                case Element.General:
                    return currentDamage;
                case Element.Fire:
                    return CalculateDamage(other, e, Element.Water, currentDamage, multiplier);
                case Element.Water:
                    return CalculateDamage(other, e, Element.Lightning, currentDamage, multiplier);
                case Element.Lightning:
                    return CalculateDamage(other, e, Element.Earth, currentDamage, multiplier);
                case Element.Earth:
                    return CalculateDamage(other, e, Element.Fire, currentDamage, multiplier);
                default:
                    return 0;

            }
        }

        private static float CalculateDamage(Element current, Element strength, Element weakness, float currentDamage, float multiplier)
        {
            if(current == strength)
            {
                return -currentDamage;
            }
            else if(current == weakness)
            {
                return currentDamage * multiplier;
            }

            return currentDamage;
        }
    }
}
