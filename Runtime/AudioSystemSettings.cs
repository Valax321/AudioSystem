using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valax321.AudioSystem
{
    
#if PACKAGE_DEV
    [CreateAssetMenu(menuName = "Valax321/Audio System/Audio System Settings")]
#endif
    [HelpURL("https://github.com/Valax321/AudioSystem/Documentation~/AudioSystemSettings.md")]
    public class AudioSystemSettings : ScriptableObject
    {
        [SerializeField, Min(0)] private int m_initialPoolSize = 100;

        public int initialPoolSize => m_initialPoolSize;
    }
}
