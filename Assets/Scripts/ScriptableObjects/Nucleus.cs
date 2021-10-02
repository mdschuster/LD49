using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nucleus", menuName = "Nucleus", order = 1)]
public class Nucleus : ScriptableObject
{
    public string nucleus = "Nucleus";
    public int protons = 0;
    public int baseNeutrons = 0;

    public List<int> neutrons;

    public List<string> decayType;
    public List<int> decayTime;

}
