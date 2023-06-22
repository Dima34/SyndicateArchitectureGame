using Logic;
using UnityEngine;

namespace UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _health;

        private void UpdateHpBar() =>
            _hpBar.SetValue(_health.CurrentHP, _health.MaxHP);

        
        public void Construct(IHealth health)
        {
            _health = health;
            _health.OnHealthChanged += UpdateHpBar;
        }

        private void OnDestroy() =>
            _health.OnHealthChanged -= UpdateHpBar;
    }
}