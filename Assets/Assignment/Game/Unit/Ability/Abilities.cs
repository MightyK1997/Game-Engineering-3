using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {

    //This contains a list of all abilities that a unit can have.
    //Each unit can have multiple abilities.
    public enum AbilitiesEnum
    {
        Fireball, Blast, Push, Teleport, Stealth, Meteor, Gravity
    }
}