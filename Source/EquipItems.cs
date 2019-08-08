using UnityEngine;

[AddComponentMenu("NGUI/Examples/Equip Items")]
public class EquipItems : MonoBehaviour
{
    public int[] itemIDs;

    private void Start()
    {
        if (itemIDs != null && itemIDs.Length > 0)
        {
            var component = GetComponent<InvEquipment>();
            if (component == null)
            {
                component = gameObject.AddComponent<InvEquipment>();
            }
            var max = 12;
            var index = 0;
            var length = itemIDs.Length;
            while (index < length)
            {
                var num4 = itemIDs[index];
                var bi = InvDatabase.FindByID(num4);
                if (bi != null)
                {
                    var item = new InvGameItem(num4, bi)
                    {
                        quality = (InvGameItem.Quality)Random.Range(0, max),
                        itemLevel = NGUITools.RandomRange(bi.minItemLevel, bi.maxItemLevel)
                    };
                    component.Equip(item);
                }
                else
                {
                    Debug.LogWarning("Can't resolve the item ID of " + num4);
                }
                index++;
            }
        }
        Destroy(this);
    }
}