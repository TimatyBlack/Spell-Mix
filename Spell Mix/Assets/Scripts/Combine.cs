using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Combine : MonoBehaviour
{
    public Spell fireblast;
    public Spell iceShard;
    public Spell rock;
    public Spell airCloud;
    public Spell spike;
    public Spell lava;
    public Spell woodSpike;

    public Animator animator;
    public Spawner spawner;
    public Button combineButton;
    public GameObject combineSpellSpawn;
    public SpawnedSpell finalSpellCast;

    public bool isReadyToShoot;

    void Update()
    {
        if (spawner.spawnedSpells.Count == 2)
        {
            combineButton.interactable = true;
        }
        else
        {
            combineButton.interactable = false;
        }
    }

    public void CombineButton()
    {
        animator.SetBool("ReadyToShoot", true);
        StartCoroutine(SpellCombine());
    }

    IEnumerator SpellCombine()
    {
        yield return new WaitForSeconds(0.9f);

        spawner.spawnedSpells[0].transform.DOScale(new Vector3(0, 0, 0), 0.2f);
        spawner.spawnedSpells[1].transform.DOScale(new Vector3(0, 0, 0), 0.2f);

        yield return new WaitForSeconds(0.2f);

        SpawnFinalSpell(SpellCombineSystem());

        yield return new WaitForSeconds(0.5f);

        Destroy(spawner.spawnedSpells[1].gameObject);
        Destroy(spawner.spawnedSpells[0].gameObject);

        spawner.spawnedSpells.Remove(spawner.spawnedSpells[1]);
        spawner.spawnedSpells.Remove(spawner.spawnedSpells[0]);

        
    }

    public Spell SpellCombineSystem()
    {
        if (spawner.spawnedSpells[0].type == "fire" && spawner.spawnedSpells[1].type == "fire")
        {
            return fireblast;
        }
        if (spawner.spawnedSpells[0].type == "water" && spawner.spawnedSpells[1].type == "water")
        {
            return iceShard;
        }
        if (spawner.spawnedSpells[0].type == "earth" && spawner.spawnedSpells[1].type == "earth")
        {
            return spike;
        }


        if (spawner.spawnedSpells[0].type == "fire" && spawner.spawnedSpells[1].type == "water")
        {
            return rock;
        }
        if (spawner.spawnedSpells[0].type == "water" && spawner.spawnedSpells[1].type == "fire")
        {
            return airCloud;
        }

        if (spawner.spawnedSpells[0].type == "earth" && spawner.spawnedSpells[1].type == "fire" ||
            spawner.spawnedSpells[0].type == "fire" && spawner.spawnedSpells[1].type == "earth")
        {
            return lava;
        }

        if (spawner.spawnedSpells[0].type == "earth" && spawner.spawnedSpells[1].type == "water" ||
            spawner.spawnedSpells[0].type == "water" && spawner.spawnedSpells[1].type == "earth")
        {
            return woodSpike;
        }

        return null;
    }

    public void SpawnFinalSpell(Spell spell)
    {
        SpawnedSpell finalSpell = Instantiate(spell.prefab, combineSpellSpawn.transform.position, Quaternion.identity, combineSpellSpawn.transform);
        finalSpell.Init(spell);

        isReadyToShoot = true;

        finalSpellCast = finalSpell;

        finalSpell.transform.DOScale(new Vector3(0, 0, 0), 0);

        finalSpell.transform.DOScale(new Vector3(1, 1, 1), 0.5f)
            .SetEase(Ease.InOutSine);
    }
}
