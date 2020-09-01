using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valax321.AudioSystem
{
    [System.Serializable]
    public class EmitterProperties
    {
        [Tooltip("Should this event be spatialized when played?")]
        [SerializeField] private bool m_spatialize;
        
        [Tooltip("Blend between 2d and 3d playback spaces.")]
        [SerializeField, Range(0, 1)] private float m_blendSpace;

        [Tooltip("Control for how much the doppler effect is applied to this sound.")]
        [SerializeField, Range(0, 1)] private float m_dopplerLevel;
        
        [Tooltip("The sound's min and max range.")]
        [SerializeField] private FloatRange m_range = new FloatRange(1, 50);

        [Tooltip("The rolloff mode for this sound in 3d space.")]
        [SerializeField] private AudioRolloffMode m_rolloffMode = AudioRolloffMode.Linear;

        [Tooltip("Custom rolloff curve for when rolloffMode is set to custom.")] [SerializeField]
        private AnimationCurve m_customRolloffCurve = AnimationCurve.Linear(0, 0, 1, 1);

        /// <summary>
        /// Should this event be spatialized when played?
        /// </summary>
        public bool spatialize
        {
            get => m_spatialize;
            set => m_spatialize = value;
        }

        /// <summary>
        /// Blend between 2d and 3d playback spaces.
        /// </summary>
        public float blendSpace
        {
            get => m_blendSpace;
            set => m_blendSpace = value;
        }

        public float dopplerScale
        {
            get => m_dopplerLevel;
            set => m_dopplerLevel = value;
        }

        /// <summary>
        /// The minimum sound range.
        /// </summary>
        public float minRange
        {
            get => m_range.min;
            set => m_range.min = value;
        }

        /// <summary>
        /// The maximum sound range.
        /// </summary>
        public float maxRange
        {
            get => m_range.max;
            set => m_range.max = value;
        }

        public AudioRolloffMode rolloffMode
        {
            get => m_rolloffMode;
            set => m_rolloffMode = value;
        }

        public AnimationCurve customRolloffCurve => m_customRolloffCurve;
    }
}
