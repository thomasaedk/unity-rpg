using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        private bool _alreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (_alreadyTriggered || !other.gameObject.CompareTag("Player"))
            {
                return;
            }

            _alreadyTriggered = true;
            GetComponent<PlayableDirector>().Play();
        }

        public object CaptureState()
        {
            return _alreadyTriggered;
        }

        public void RestoreState(object state)
        {
            _alreadyTriggered = (bool)state;
        }
    }
}