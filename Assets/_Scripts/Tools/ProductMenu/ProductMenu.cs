#if UNITY_EDITOR
namespace ProductMenu
{
    using StoreEngine;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using UnityEditor;
    using UnityEngine;
    using System.Linq;
    
    public class ProductMenu : OdinMenuEditorWindow
    {
        [MenuItem("Tools/ProductMenu")]
        private static void Open()
        {
            var window = GetWindow<ProductMenu>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;
            
            // Adds all products.
            tree.AddAllAssetsAtPath("Products", "Assets/Data/Store Engine/Products", typeof(Product), true)
                .ForEach(this.AddDragHandles);
            
            // Adds all store products overview.
            tree.AddAllAssetsAtPath("Store Products", "Assets/Data/Store Engine/Products", typeof(StoreProductsOverview), false);
            
            
            tree.EnumerateTree().AddIcons<Product>(x => x.icon);
            
            return tree;
        }
        
        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
        }
    }
}
#endif