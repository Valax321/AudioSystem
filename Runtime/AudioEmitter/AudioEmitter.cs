using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Valax321.AudioSystem
{
    /// <summary>
    /// Component for playing <see cref="AudioEvent"/>s at runtime.
    /// </summary>
    [HelpURL("https://github.com/Valax321/AudioSystem/blob/master/Documentation~/AudioEmitter.md")]
    [AddComponentMenu("Audio/Audio Emitter")]
    public class AudioEmitter : MonoBehaviour
    {
        [Tooltip("The audio event attached to this emitter.")]
        [SerializeField] private AudioEvent m_event;
        
        [Tooltip("Should the emitter start playing automatically when play begins?")]
        [SerializeField] private bool m_playOnStart;
        
        [Tooltip("Should this emitter destroy itself when it's done playing?")]
        [SerializeField] private bool m_destroyGameObjectOnComplete;
        
        [Tooltip("Overrides the emitter properties of the event on a per-emitter basis.")]
        [SerializeField] private bool m_overrideEmitterProperties;
        
        [SerializeField] private EmitterProperties m_emitterProperties;

        private AudioSource m_source;
        private int m_loop;
        private int m_lastPlayedClipIndex = -1;
        private float m_clipPlayTimeRemaining;
        private bool m_isPlaying;

        /// <summary>
        /// The audio event attached to this emitter.
        /// Changing this will stop all sound currently playing through this emitter.
        /// </summary>
        public AudioEvent audioEvent
        {
            get => m_event;
            set
            {
                var ev = m_event;
                m_event = value;
                if (ev != m_event)
                {
                    ConfigureAudioSource();
                    Stop(); // Stop the event playback if it changes
                }
            }
        }

        /// <summary>
        /// Should the emitter start playing automatically when play begins?
        /// </summary>
        public bool playOnStart
        {
            get => m_playOnStart;
            set => m_playOnStart = value;
        }

        /// <summary>
        /// Should this emitter destroy itself when it's done playing?
        /// </summary>
        public bool destroyGameObjectOnComplete
        {
            get => m_destroyGameObjectOnComplete;
            set => m_destroyGameObjectOnComplete = value;
        }

        /// <summary>
        /// Overrides the emitter properties of the event on a per-emitter basis.
        /// </summary>
        public bool overrideEmitterProperties
        {
            get => m_overrideEmitterProperties;
            set => m_overrideEmitterProperties = value;
        }

        /// <summary>
        /// The emitter properties for this emitter. Only used if <see cref="overrideEmitterProperties"/> is true.
        /// </summary>
        public EmitterProperties emitterProperties
        {
            get => m_emitterProperties;
            set => m_emitterProperties = value;
        }

        /// <summary>
        /// The currently playing clip.
        /// </summary>
        public AudioClip currentClip => m_source ? m_source.clip : null;

        public void Play()
        {
            if (!m_event)
                return;
            
            AudioClip clip = null;
            switch (m_event.clipPlayMode)
            {
                case ClipPlayMode.Random:
                    clip = m_event.clips[Random.Range(0, m_event.clips.Length)];
                    break;
                case ClipPlayMode.RandomNoRepeats:
                    var id = 0;
                    do
                    {
                        id = Random.Range(0, m_event.clips.Length);
                    } while (m_lastPlayedClipIndex == id);

                    m_lastPlayedClipIndex = id;
                    clip = m_event.clips[id];
                    break;
                case ClipPlayMode.Sequential:
                    clip = m_event.clips[m_loop % m_event.clips.Length];
                    m_loop++;
                    break;
            }

            m_source.clip = clip;
            m_source.Play();
            m_clipPlayTimeRemaining = clip.length;
            m_isPlaying = true;
        }

        public void Stop()
        {
            m_isPlaying = false;
            m_source.Stop();
        }

        private void Awake()
        {
            m_source = GetComponent<AudioSource>();
            if (!m_source)
            {
                m_source = gameObject.AddComponent<AudioSource>();
#if !PACKAGE_DEV
                m_source.hideFlags = HideFlags.HideInInspector;
#endif
            }
            
            ConfigureAudioSource();
        }

        private void Start()
        {
            if (m_playOnStart)
            {
                Play();
            }
        }

        private void Update()
        {
            if (m_isPlaying)
            {
                m_clipPlayTimeRemaining -= Time.unscaledDeltaTime;
                
                if (m_clipPlayTimeRemaining <= 0)
                {
                    if (m_event.loop)
                    {
                        Play();
                    }
                    else if (m_event.clipPlayMode == ClipPlayMode.Sequential && m_loop < m_event.clips.Length)
                    {
                        Play();
                    }
                    else if (m_destroyGameObjectOnComplete)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        private void ConfigureAudioSource()
        {
            if (!m_event)
                return;
            
            m_loop = 0;
            m_lastPlayedClipIndex = -1;

            m_source.outputAudioMixerGroup = m_event.mixerGroup;
            m_source.volume = m_event.volume;
            var emitterProps = m_overrideEmitterProperties ? m_emitterProperties : m_event.emitterProperties;
            m_source.spatialize = emitterProps.spatialize;
            m_source.spatialBlend = emitterProps.blendSpace;
            m_source.dopplerLevel = emitterProps.dopplerScale;
            m_source.minDistance = emitterProps.minRange;
            m_source.maxDistance = emitterProps.maxRange;
            m_source.velocityUpdateMode = AudioVelocityUpdateMode.Auto;
            m_source.rolloffMode = emitterProps.rolloffMode;
            if (emitterProps.customRolloffCurve.keys.LongLength > 0)
            {
                m_source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, emitterProps.customRolloffCurve);
            }
        }
    }
}
