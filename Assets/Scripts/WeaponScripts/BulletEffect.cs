using UnityEngine;

// using Abstract class cuz we dont want to intialize(we cant make an instance of abstract classes. BUT we can inherit from it ).
public abstract class BulletEffect : ScriptableObject
{

    public abstract void Apply(GameObject target);
    
    
}// end script
