using Extensions;
using ThunderRoad;
using static ThunderRoad.TextData;
using Random = UnityEngine.Random;

namespace Spawner;

public class Spell : SpellCastCharge
{
    [ModOption("Spawn on Cast", "If this is enabled a item will spawn when you cast the spell", valueSourceName = "Spawn on Cast", defaultValueIndex = 1)]
    public static bool spawnOnCast;
    [ModOption("Spawn on Release", "If this is enabled a item will spawn when you stop casting the spell", valueSourceName = "Spawn on Release", defaultValueIndex = 0)]
    public static bool spawnOnRelease;
    public string[] itemIDs = { "DaggerCommon", "SwordShortCommon", "SwordShortLarge", "SwordShortAntic", "AxeShortCommon" };

    public override void Fire(bool active)
    {
        base.Fire(active);
        if (spawnOnCast && active)
        {
            Catalog.GetData<ItemData>(itemIDs[Random.Range(0, itemIDs.Length)]).SpawnAsync(item =>
            {
                spellCaster.ragdollHand.Grab(item.GetMainHandle(spellCaster.ragdollHand.side));
            }, spellCaster.magic.position);
            spellCaster.EndCast();
        }

        if (spawnOnRelease && !active && currentCharge >= 0.01f)
        {
            Catalog.GetData<ItemData>(itemIDs[Random.Range(0, itemIDs.Length)]).SpawnAsync(item =>
            {
                spellCaster.ragdollHand.Grab(item.GetMainHandle(spellCaster.ragdollHand.side));
            }, spellCaster.magic.position);
            spellCaster.EndCast();
        }
    }
}