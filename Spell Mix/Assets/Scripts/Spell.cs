using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]
public class Spell : ScriptableObject
{
    public new string name;
    public string type;
    public SpawnedSpell prefab;

    public GameObject hitParticle;

    public int dmg;
    public float speed;
    public float radius;
}
