using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemEditor : EditorBase
{
    Vector2 scrollView;

    Item activeItem;
    TextAsset obj;
    public UnityEngine.Sprite sprite;

    [MenuItem("Window/Editor/Items")]
    private static void ShowWindow()
    {
        GetWindow(typeof(ItemEditor));
    }

    private void Awake()
    {
        so = new SerializedObject(this);
        activeItem = new Item();
    }

    protected override void MainWindow()
    {
        scrollView = EditorGUILayout.BeginScrollView(scrollView, false, true, GUILayout.Width(position.width));
        {
            obj = PaintXMLLookup(obj, "Resource File", true);
            if (PaintButton("Save"))
            {
                Save();
            }

            PaintTextField(ref activeItem.Name, "Item Name");

                    activeItem.ItemType = (Item.Type)PaintPopup(
                        Helper.ItemTypeNames, 
                        (int)activeItem.ItemType, 
                        "Item Type");

                    activeItem.useAnimation = (Item.UseAnimation)PaintPopup(
                        Helper.ItemAnimationNames, 
                        (int)activeItem.useAnimation, 
                        "Animation Type");

            PaintSpriteField(ref sprite);
        }
        EditorGUILayout.EndScrollView();

        base.MainWindow();
    }

    protected override void ResetProperties()
    {

    }

    protected override void LoadProperties(TextAsset textAsset)
    {
        activeItem = XMLUtility.Load<Item>(textAsset);
        sprite = Resources.Load<Sprite>(activeItem.sprite);
    }

    protected override void CreationWindow()
    {
        base.CreationWindow();
    }

    private void Save()
    {
        string filePath = string.Empty;

        // UI Sprite
        if (sprite != null)
        {
            filePath = AssetDatabase.GetAssetPath(sprite).Substring(S_RESOURCE_DIR_LENGTH);
            filePath = filePath.Substring(0, filePath.Length - S_PNG_EXTENSION_LENGTH);
            activeItem.sprite = sprite == null ? string.Empty : filePath;
        }

        System.Text.StringBuilder t = new System.Text.StringBuilder(S_ITEMS_DIR);
        XMLUtility.Save(activeItem, t.ToString(), activeItem.Name);
    }
}
