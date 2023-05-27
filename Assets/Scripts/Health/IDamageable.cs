using UnityEngine;
// ---------------------
// Cameron Hadfield
// TOJam 2022
// IDamageable.cs
// Interface for all things that have behaviour associated with gettin smacked
// ---------------------
public interface IDamageable{
    public virtual void BeDamaged(float dmg) { Debug.Log("Override me!"); }
}