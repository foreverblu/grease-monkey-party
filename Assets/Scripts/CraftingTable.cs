using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftingTable : MonoBehaviour
{
    private ListView lv;
    
    private void OnEnable()
    {
        var rootVE = GetComponent<UIDocument>().rootVisualElement;
        lv = rootVE.Q<ListView>("Inventory");
        renderListView();
    }

    private void renderListView()
    {
        const int itemCount = 10;
        var items = new List<string>(itemCount);
        for (int i = 1; i <= itemCount; i++)
            items.Add(i.ToString());

        Func<VisualElement> makeItem = () => new Label();
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i];

        const int itemHeight = 20;

        lv.itemsSource = items;
        lv.itemHeight = itemHeight;
        lv.makeItem = makeItem;
        lv.bindItem = bindItem;
    }
}
