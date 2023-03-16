using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    public Spell fireball;
    public Spell waterSphere;
    public Spell earth;

    public List<GameObject> spawnPoints;
    public List<SpawnedSpell> spawnedSpells;

    public bool LisEmpty;
    public bool RisEmpty;

    void Start()
    {
        LisEmpty = true;
        RisEmpty = true;
    }

    public void SpawnFire()
    {
        SpawnSpell(fireball);
    }
    public void SpawnWater()
    {
        SpawnSpell(waterSphere);
    }
    public void SpawnEarth()
    {
        SpawnSpell(earth);
    }

    public void SpawnSpell(Spell spell)
    {
        if(LisEmpty == true)
        {
            LisEmpty = false;
            SpawnedSpell spellCast = Instantiate(spell.prefab, spawnPoints[0].transform);
            spellCast.Init(spell);
            spawnedSpells.Add(spellCast);
            SpawnAnimation(spellCast);
        }
        else if(RisEmpty == true)
        {
            RisEmpty = false;
            SpawnedSpell spellCast = Instantiate(spell.prefab, spawnPoints[1].transform);
            spellCast.Init(spell);
            spawnedSpells.Add(spellCast);
            SpawnAnimation(spellCast);
        }
    }

    public void SpawnAnimation(SpawnedSpell spell)
    {
        spell.transform.DOScale(new Vector3(0, 0, 0), 0);

        spell.transform.DOScale(new Vector3(1, 1, 1), 0.5f)
            .SetEase(Ease.InOutSine);
    }
}
