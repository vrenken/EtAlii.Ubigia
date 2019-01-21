namespace EtAlii.Ubigia.Windows.Mvvm
{
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;

    public static partial class DragDropBehaviour
    {
        #region DragDropHandler DependencyProperty

        /// <summary>
        /// attached property that handles drag and drop
        /// </summary>
        public static readonly DependencyProperty DragDropHandlerProperty =
           DependencyProperty.RegisterAttached("DragDropHandler",
           typeof(IDragDropHandler),
           typeof(DragDropBehaviour),
           new PropertyMetadata(null, new ExecuteDragDropBehaviour().PropertyChangedHandler));

        public static void SetDragDropHandler(DependencyObject o, object propertyValue)
        {
            o.SetValue(DragDropHandlerProperty, propertyValue);
        }
        public static object GetDragDropHandler(DependencyObject o)
        {
            return o.GetValue(DragDropHandlerProperty);
        }

        #endregion

        internal abstract class DragDropBehaviourBase
        {
            protected DependencyProperty Property;

            /// <summary>
            /// attach the events
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="oldValue"></param>
            /// <param name="newValue"></param>
            protected abstract void AdjustEventHandlers(DependencyObject sender,
                                                        object oldValue, object newValue);

            /// <summary>
            /// Listens for a change in the DependencyProperty
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void PropertyChangedHandler(DependencyObject sender,
                                               DependencyPropertyChangedEventArgs e)
            {
                if (Property == null)
                {
                    Property = e.Property;
                }

                object oldValue = e.OldValue;
                object newValue = e.NewValue;

                AdjustEventHandlers(sender, oldValue, newValue);
            }
        }

        /// <summary>
        /// an internal class to handle listening for the drop event and executing the dropaction
        /// </summary>
        private class ExecuteDragDropBehaviour : DragDropBehaviourBase
        {
            /// <summary>
            /// attach the events
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="oldValue"></param>
            /// <param name="newValue"></param>
            protected override void AdjustEventHandlers(DependencyObject sender, object oldValue, object newValue)
            {
                if (!(sender is UIElement element)) { return; }

                if (oldValue != null)
                {
                    element.RemoveHandler(UIElement.DropEvent, new DragEventHandler(ReceiveDrop));
                }

                if (newValue != null)
                {
                    element.AddHandler(UIElement.DropEvent, new DragEventHandler(ReceiveDrop));
                }
            }

            /// <summary>
            /// eventhandler that gets executed when the DropEvent fires
            /// </summary>
            private void ReceiveDrop(object sender, DragEventArgs e)
            {
                if (!(sender is DependencyObject dp))
                {
                    return;
                }

                if (!(dp.GetValue(Property) is IDragDropHandler action))
                {
                    return;
                }

                IEnumerable dropTarget = null;
                if (sender is ItemsControl)
                {
                    dropTarget = (sender as ItemsControl).ItemsSource;
                }

                if (action.CanDrop(e.Data, dropTarget))
                {
                    action.OnDrop(e.Data, dropTarget);
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    } 

}
