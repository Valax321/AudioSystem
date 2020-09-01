using UnityEngine;

namespace Valax321.AudioSystem
{
    /// <summary>
    /// Asset storing audio environment information for play at runtime.
    /// </summary>
    [HelpURL("https://github.com/Valax321/AudioSystem/blob/master/Documentation~/AudioEnvironment.md")]
    [CreateAssetMenu(menuName = "Audio Environment", order = 221)]
    public class AudioEnvironment : ScriptableObject
    {
        [SerializeField] private AudioEnvironmentLayer[] m_layers;
    }
}