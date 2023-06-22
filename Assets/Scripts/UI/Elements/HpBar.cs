using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBarImage;

        public void SetValue(float current, float max) =>
            _hpBarImage.fillAmount = current / max;
    }
}