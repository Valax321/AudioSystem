using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valax321.AudioSystem;

namespace Valax321.AudioSystem
{
    [HelpURL("https://github.com/Valax321/AudioSystem/Documentation~/AudioEmitter.md")]
    [AddComponentMenu("Audio/Audio Emitter")]
    public class AudioEmitter : MonoBehaviour
    {
        [SerializeField] private AudioEvent m_event;
        [SerializeField] private bool m_playOnStart;
        [SerializeField] private bool m_destroyGameObjectOnComplete;
        [SerializeField] private bool m_overrideEmitterProperties;
        [SerializeField] private EmitterProperties m_emitterProperties;

        public AudioEvent audioEvent
        {
            get => m_event;
            set => m_event = value;
        }

        public bool playOnStart
        {
            get => m_playOnStart;
            set => m_playOnStart = value;
        }

        public bool destroyGameObjectOnComplete
        {
            get => m_destroyGameObjectOnComplete;
            set => m_destroyGameObjectOnComplete = value;
        }

        public bool overrideEmitterProperties
        {
            get => m_overrideEmitterProperties;
            set => m_overrideEmitterProperties = value;
        }

        public EmitterProperties emitterProperties
        {
            get => m_emitterProperties;
            set => m_emitterProperties = value;
        }
        
        private AudioSource m_source;

        private void Awake()
        {
            if (!m_event)
                return;
            
            m_source = GetComponent<AudioSource>();
            if (!m_source)
            {
                m_source = gameObject.AddComponent<AudioSource>();
            }

            m_source.outputAudioMixerGroup = m_event.mixerGroup;
        }
    }
}
