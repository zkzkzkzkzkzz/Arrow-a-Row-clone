using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemEffect
{
    void ApplyEffect(Player player);
    int Level { get; }
    string GetEffectName();
}
