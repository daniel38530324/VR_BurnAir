using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoundData")]
public class SoundData : ScriptableObject
{
    public Sounds[] Music;
    public Sounds[] Sound;
}
