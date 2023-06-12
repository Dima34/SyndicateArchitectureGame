using System;
using Services.Inputs;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        private int _layerMask;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp())
                Attack();
        }

        private void Attack()
        {
            
        }

        private int Hit()
        {
            return 0;   
        }

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        
    }
}