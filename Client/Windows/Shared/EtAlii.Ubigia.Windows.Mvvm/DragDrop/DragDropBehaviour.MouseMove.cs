namespace EtAlii.Ubigia.Windows.Mvvm
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public static partial class DragDropBehaviour
    {
        #region IsDragSource DependencyProperty

        /// <summary>
        /// attached property that defines if the source is a drag source
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static readonly DependencyProperty IsDragSourceProperty = DependencyProperty.RegisterAttached("IsDragSource", typeof(bool), typeof(DragDropBehaviour), new PropertyMetadata(false, new IsDragSourceBehaviour().PropertyChangedHandler));

        public static void SetIsDragSource(DependencyObject o, object propertyValue)
        {
            o.SetValue(IsDragSourceProperty, propertyValue);
        }
        public static object GetIsDragSource(DependencyObject o)
        {
            return o.GetValue(IsDragSourceProperty);
        }

        #endregion

        /// <summary>
        /// Internal class that starts the draging
        /// </summary>
        private class IsDragSourceBehaviour : DragDropBehaviourBase
        {
            /// <summary>
            /// Hattach the events
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="oldValue"></param>
            /// <param name="newValue"></param>
            protected override void AdjustEventHandlers(DependencyObject sender, 
                                                        object oldValue, object newValue)
            {
                var element = sender as UIElement;
                if (element == null) 
                    return;

                if (oldValue != null)
                {
                    element.RemoveHandler(UIElement.MouseMoveEvent, 
                                          new MouseEventHandler(OnMouseMove));
                }

                if (newValue != null && newValue is bool && (bool)newValue)
                {
                    element.AddHandler(UIElement.MouseMoveEvent, 
                                       new MouseEventHandler(OnMouseMove));
                }
            }

            /// <summary>
            /// eventhandler for the MouseMoveEvent
            /// </summary>
            private void OnMouseMove(object sender, MouseEventArgs e)
            {
                if (sender is Selector && e.LeftButton == MouseButtonState.Pressed)
                {
                    var lst = (Selector)sender;
                    var selectedItem = lst.SelectedItem;

                    var dragDropEffect = DragDropEffects.Move;

                    if (dragDropEffect != DragDropEffects.None)
                    {
                        DragDrop.DoDragDrop(sender as DependencyObject, selectedItem, dragDropEffect);
                    }
                }
            }
        }
   }


}
