using Xamarin.Forms;

namespace JobInTown.Controls
{
    public class BindableToolbarItem : ToolbarItem
    {
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create("IsVisible", typeof(bool), typeof(BindableToolbarItem), false, propertyChanged: OnIsVisibleChanged);

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            InitVisibility();
        }

        private static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = bindable as BindableToolbarItem;

            if (item.Parent == null)
            {
                return;
            }

            if (newValue is bool newvalueBool)
            {
                var items = ((Page)item.Parent).ToolbarItems;

                if (newvalueBool && !items.Contains(item))
                {
                    items.Add(item);
                }
                else if (!newvalueBool && items.Contains(item))
                {
                    items.Remove(item);
                }
            }
        }

        private void InitVisibility()
        {
            OnIsVisibleChanged(this, false, IsVisible);
        }
    }
}
