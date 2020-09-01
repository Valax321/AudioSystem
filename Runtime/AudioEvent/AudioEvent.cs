using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Valax321.AudioSystem
{
    /// <summary>
    /// Asset that manages a collection of audio clips and how they are played at runtime.
    /// </summary>
    [CreateAssetMenu(menuName = "Audio Event", order = 220)]
    [HelpURL("https://github.com/Valax321/AudioSystem/blob/master/Documentation~/AudioEvent.md")]
    public class AudioEvent : ScriptableObject
    {
        #region Playback Properties
        
        [Tooltip(@"The playback mode used by this event.
Sequential: Sounds are played one after the other. One per loop.
Random: Sounds are played in a random order.
Random No Repeats: Sounds are played in a random order. The same sound will never play twice in a row.")]
        [SerializeField] private ClipPlayMode m_clipPlayMode = ClipPlayMode.Random;

        [Tooltip("The volume the sound event is played back at.")]
        [SerializeField, Range(0, 1)] private float m_volume = 1.0f;
        
        [Tooltip("A list of AudioClips that this audio event can play.")]
        [SerializeField] private AudioClip[] m_clips;

        [Tooltip("Should this event loop? Looped events must be manually stopped.")]
        [SerializeField] private bool m_loop;

        [Tooltip("The mixer group that this event outputs to.")]
        [SerializeField] private AudioMixerGroup m_mixerGroup;

        public ClipPlayMode clipPlayMode
        {
            get => m_clipPlayMode;
            set => m_clipPlayMode = value;
        }

        public float volume
        {
            get => m_volume;
            set => m_volume = value;
        }

        public AudioClip[] clips
        {
            get => m_clips;
            set => m_clips = value;
        }

        public bool loop
        {
            get => m_loop;
            set => m_loop = value;
        }

        public AudioMixerGroup mixerGroup
        {
            get => m_mixerGroup;
            set => m_mixerGroup = value;
        }
        
        #endregion
        
        #region Emitter Properties

        [SerializeField] private EmitterProperties m_emitterProperties;

        public EmitterProperties emitterProperties
        {
            get => m_emitterProperties;
            set => m_emitterProperties = value;
        }

        #endregion
    }
}
