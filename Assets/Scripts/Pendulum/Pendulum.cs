using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pendulum 
{
    public Transform character_transform;
    public Tether tether;
    public Rope rope;
    public CharacterSwing character;

    Vector3 prevPos;

    public void Initialise()
    {
        character_transform.transform.parent = tether.tether_transform;
        rope.length = Vector3.Distance(character_transform.transform.localPosition, tether.position);
    }

    public Vector3 MoveCharacter(Vector3 pos, float time)
    {
        character.velocity += GetConstrainedVelocity(pos, prevPos, time);

        character.ApplyGravity();
        character.ApplyDamping();

        pos += character.velocity * time
            ;

        if (Vector3.Distance(pos, tether.position) < rope.length)
        {
            pos = Vector3.Normalize(pos - tether.position) * rope.length;
            rope.length = Vector3.Distance(pos, tether.position);
            return pos;
        }
        prevPos = pos;

        return pos;
    }

    public Vector3 GetConstrainedVelocity(Vector3 currentPos, Vector3 prevPos, float time)
    {
        float distanceToTether;
        Vector3 constrainedPos;
        Vector3 predictedPos;

        distanceToTether = Vector3.Distance(currentPos, tether.position);

        if(distanceToTether>rope.length)
        {
            // new vector pointing to currentPos from tether pos
            constrainedPos = Vector3.Normalize(currentPos - tether.position) * rope.length;
            predictedPos = (constrainedPos - prevPos) / time; //velocity=distance*time;
            return predictedPos;
        }
        
        return Vector3.zero;
    }
}
