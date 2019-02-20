//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/Equip Random Item")]
public class EquipRandomItem : MonoBehaviour
{
    public InvEquipment equipment;

    private void OnClick()
    {
        if (equipment != null)
        {
            var items = InvDatabase.list[0].items;
            if (items.Count != 0)
            {
                var max = 12;
                var id = Random.Range(0, items.Count);
                var bi = items[id];
                var item = new InvGameItem(id, bi) {
                    quality = (InvGameItem.Quality) Random.Range(0, max),
                    itemLevel = NGUITools.RandomRange(bi.minItemLevel, bi.maxItemLevel)
                };
                equipment.Equip(item);
            }
        }
    }
}

