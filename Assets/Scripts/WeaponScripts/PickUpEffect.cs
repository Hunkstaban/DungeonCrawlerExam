using UnityEngine;

// using Abstract class cuz we dont want to intialize(we cant make an instance of abstract classes. BUT we can inherit from it ).
public abstract class PickUpEffect : ScriptableObject
{

    public abstract void Apply(GameObject target);
    
    
}// end script
