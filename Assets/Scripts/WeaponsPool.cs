using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPool : MonoBehaviour
{
    //[SerializeField] private GameObject weapon1;
    [SerializeField] private int poolSize;
    [SerializeField] private bool expandable;

    public GameObject weapon;
    public Transform throwingPoint;

    private List<GameObject> usabaleWeapons;
    private List<GameObject> usedWeapons;

    private void Awake()
    {
        usabaleWeapons = new List<GameObject>();
        usedWeapons = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GenerateWeapon();
        }
    }

    public GameObject GetWeapon()
    {
        if (usabaleWeapons.Count == 0 && !expandable)
            return null;
        else if (usabaleWeapons.Count == 0)
            GenerateWeapon();

        GameObject go = usabaleWeapons[usabaleWeapons.Count -1];
        usabaleWeapons.RemoveAt(usabaleWeapons.Count - 1);
        usedWeapons.Add(go);

        return go;
    }

    public void ReturnWeapon(GameObject game)
    {
        Debug.Assert(usedWeapons.Contains(game));
        game.SetActive(false);
        usedWeapons.Remove(game);
        usabaleWeapons.Add(game);
    }

    private void GenerateWeapon()
    {
        GameObject go = Instantiate(weapon, throwingPoint.position, weapon.transform.rotation);
        //go.transform.parent = transform;
        go.SetActive(false);
        usabaleWeapons.Add(go);
    }
}
