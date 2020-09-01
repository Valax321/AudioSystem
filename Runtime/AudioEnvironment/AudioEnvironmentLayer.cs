using System;
using UnityEngine;

namespace Valax321.AudioSystem
{
    [Serializable]
    public class AudioEnvironmentLayer
    {
        [SerializeField] private AudioEnvironmentType m_type;
        [SerializeField] private AudioEvent m_event;
        [SerializeField] private AudioEnvironment m_subEnvironment;

        [SerializeField] private string m_positionKey;
        [SerializeField] private Vector3 m_positionOffset;
        [SerializeField] private FloatRange m_randomRadius = new FloatRange(0, 0);
        [SerializeField] private FloatRange m_playFrequency = new FloatRange(0.5f, 5.0f);
    }
}