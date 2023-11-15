using TMPro;
using UnityEngine;
namespace _Game.Scripts.Canvas
{
    public class HeathPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textHeath, textBoom, textRange ,textSpeed;

        public void SetText(int heath, int boom, int range, int speed)
        {
            textHeath.SetText(heath.ToString());
            textBoom.SetText(boom.ToString());
            textRange.SetText(range.ToString());
            textSpeed.SetText(speed.ToString());
        }
    }
}
