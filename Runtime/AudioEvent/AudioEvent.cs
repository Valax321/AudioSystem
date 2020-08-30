using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Valax321.AudioSystem
{
    [CreateAssetMenu(menuName = "Audio Event", order = 220)]
    [HelpURL("https://github.com/Valax321/AudioSystem/Documentation~/AudioEvent.md")]
    public class AudioEvent : ScriptableObject
    {
        #region Playback Properties
        
        [Tooltip(@"The playback mode used by this event.
Sequential: Sounds are played one after the other. One per loop.
Random: Sounds are played in a random order.
Random No Repeats: Sounds are played in a random order. The same sound will never play twice in a row.")]
        [SerializeField] private ClipPlayMode m_clipPlayMode = ClipPlayMode.Random;
        
        [Tooltip("A list of AudioClips that this audio event can play.")]
        [SerializeField] private AudioClip[] m_clips;

        [Tooltip("How many times does this sound event loop?")]
        [SerializeField, Min(0)] private int m_loopCount;

        [Tooltip("The mixer group that this event outputs to.")]
        [SerializeField] private AudioMixerGroup m_mixerGroup;

        public ClipPlayMode clipPlayMode
        {
            get => m_clipPlayMode;
            set => m_clipPlayMode = value;
        }

        public AudioClip[] clips
        {
            get => m_clips;
            set => m_clips = value;
        }

        public int loopCount
        {
            get => m_loopCount;
            set => m_loopCount = value;
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
