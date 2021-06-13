using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;

        private GameObject _player;
        private Health _health;
        private Fighter _fighter;
        private Mover _mover;
        private ActionScheduler _actionScheduler;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = float.PositiveInfinity;
        
        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (_health.IsDead) return;
            if (InAttackRange() && _fighter.CanAttack(_player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            _fighter.Attack(_player);
        }
        
        private void SuspicionBehaviour()
        {
            _actionScheduler.CancelCurrentAction();
        }
        
        private void GuardBehaviour()
        {
            _mover.StartMoveAction(guardPosition);
        }

        private bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }    
}

