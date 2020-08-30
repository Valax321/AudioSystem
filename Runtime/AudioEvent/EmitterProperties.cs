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
        
        [Tooltip("The minimum sound range.")]
        [SerializeField, Min(0)] private float m_minRange = 1.0f;
        
        [Tooltip("The maximum sound range.")]
        [SerializeField, Min(0)] private float m_maxRange = 50.0f;

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

        /// <summary>
        /// The minimum sound range.
        /// </summary>
        public float minRange
        {
            get => m_minRange;
            set => m_minRange = value;
        }

        /// <summary>
        /// The maximum sound range.
        /// </summary>
        public float maxRange
        {
            get => m_maxRange;
            set => m_maxRange = value;
        }
    }
}
