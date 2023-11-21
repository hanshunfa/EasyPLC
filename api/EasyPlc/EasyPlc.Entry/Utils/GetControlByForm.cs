using DevExpress.XtraBars;

namespace EasyPlc.Entry.Utils
{
    public static class GetControlByForm
    {
        /// <summary>
        /// 遍历所有控件树递,归所有子控件，查找所有包含指定控件类型的控件，并返回所有包含指定控件类型的控件的列表。调用该方法，获取所有按钮控件：var buttons = FindControls<Button>(this);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static List<T> FindControls<T>(Control control) where T : BarButtonItem
        {
            List<T> controls = new List<T>();

            // 遍历控件集合
            foreach (Control ctrl in control.Controls)
            {
                // 判断控件类型是否为指定类型
                if (ctrl is T tControl)
                {
                    // 添加符合条件的控件
                    controls.Add(tControl);
                }

                // 如果控件还有子控件，则递归调用此方法
                if (ctrl.HasChildren)
                {
                    controls.AddRange(FindControls<T>(ctrl));
                }
            }

            // 返回符合条件的控件列表
            return controls;
        }
    }
}
