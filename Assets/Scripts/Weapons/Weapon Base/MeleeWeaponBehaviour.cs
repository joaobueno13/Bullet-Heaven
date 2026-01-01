using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponsScriptableObject weaponData;

    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
}
