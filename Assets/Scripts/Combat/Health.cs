using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        
        private Animator _animator;
        private bool isDead = false;
        public bool IsDead => isDead;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0f);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            _animator.SetTrigger("die");
        }
    }
}