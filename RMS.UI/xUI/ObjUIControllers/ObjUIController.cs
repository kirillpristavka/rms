using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMS.UI.xUI.ObjUIControllers
{
    internal static class ObjUIController
    {
        public static ObjUIGridController<T> CreateObjUIControl<T>(LayoutControlGroup layoutControlGroup)
        {
            var objUIControl = default(ObjUIGridController<T>);
            var baseLayoutItem = layoutControlGroup.Items.FirstOrDefault(f => f.Text.Equals(nameof(objUIControl)));
            if (baseLayoutItem is null)
            {
                objUIControl = (ObjUIGridController<T>)Activator.CreateInstance(typeof(ObjUIGridController<T>));
                var item = layoutControlGroup.AddItem(nameof(objUIControl));
                item.Control = objUIControl;
            }
            else
            {
                objUIControl = (ObjUIGridController<T>)((LayoutControlItem)baseLayoutItem).Control;
            }
            return objUIControl;
        }

        public static ObjUIGridController<T> CreateObjUIControl<T>(LayoutControlGroup layoutControlGroup, IEnumerable<T> objCollection)
        {
            var objUIControl = default(ObjUIGridController<T>);
            var baseLayoutItem = layoutControlGroup.Items.FirstOrDefault(f => f.Text.Equals(nameof(objUIControl)));
            if (baseLayoutItem is null)
            {
                objUIControl = (ObjUIGridController<T>)Activator.CreateInstance(typeof(ObjUIGridController<T>), objCollection);
                var item = layoutControlGroup.AddItem(nameof(objUIControl));
                item.Control = objUIControl;
            }
            else
            {
                objUIControl = (ObjUIGridController<T>)((LayoutControlItem)baseLayoutItem).Control;
            }
            return objUIControl;
        }
    }
}
