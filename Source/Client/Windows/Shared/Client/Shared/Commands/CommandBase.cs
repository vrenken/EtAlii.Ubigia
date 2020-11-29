namespace EtAlii.Ubigia.Windows.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using Container = EtAlii.xTechnology.MicroContainer.Container;

    /// <summary>
    /// Basic implementation of the <see cref="ICommand"/>
    /// interface, which is also accessible as a markup
    /// extension.
    /// </summary>
    public abstract class CommandBase<T> : CommandBase, ICommand
        where T : class, ICommand, new()
    {
        protected Container Container { get; }

        private T _command;

        protected CommandBase()
        {
            Container = App.Current.Container;
        }

        private static T GetInstance()
        {
            if (!Instances.TryGetValue(typeof(T), out var instance))
            {
                instance = new T();
                Instances.Add(typeof(T), instance);
            }
            return (T)instance;
        }

        /// <summary>
        /// Gets a shared command instance.
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _command ?? (_command = GetInstance());
        }

        /// <summary>
        /// Fires when changes occur that affect whether
        /// or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.
        /// If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// Defines the method that determines whether the command
        /// can execute in its current state.
        /// </summary>
        /// <returns>
        /// This default implementation always returns true.
        /// </returns>
        /// <param name="parameter">Data used by the command.  
        /// If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        public virtual bool CanExecute(object parameter)
        {
            return !IsDesignMode;
        }


        public static bool IsDesignMode => (bool) DependencyPropertyDescriptor
            .FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement))
            .Metadata.DefaultValue;


        /// <summary>
        /// Resolves the window that owns the TaskbarIcon class.
        /// </summary>
        /// <param name="commandParameter"></param>
        /// <returns></returns>
        protected Window GetTaskbarWindow(object commandParameter)
        {
            if (IsDesignMode) return null;

            //get the showcase window off the taskbaricon
            return commandParameter is Hardcodet.Wpf.TaskbarNotification.TaskbarIcon tb 
                ? TryFindParent<Window>(tb)
                : null;
        }



        #region TryFindParent helper

        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="TDependencyObject">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        private static TDependencyObject TryFindParent<TDependencyObject>(DependencyObject child) 
            where TDependencyObject : DependencyObject
        {
            while (true)
            {
                //get parent item
                var parentObject = GetParentObject(child);

                switch (parentObject)
                {
                    //we've reached the end of the tree
                    case null:
                        return null;
                    //check if the parent matches the type we're looking for
                    case TDependencyObject parent:
                        return parent;
                    default:
                        //use recursion to proceed with next level
                        child = parentObject;
                        continue;
                }
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        private static DependencyObject GetParentObject(DependencyObject child)
        {
            switch (child)
            {
                case null: return null;
                case ContentElement contentElement:
                {
                    var parent = ContentOperations.GetParent(contentElement);
                    if (parent != null) return parent;

                    return contentElement is FrameworkContentElement fce ? fce.Parent : null;
                }
                default:
                    //if it's not a ContentElement, rely on VisualTreeHelper
                    return VisualTreeHelper.GetParent(child);
            }
        }

        #endregion
    }

    public abstract class CommandBase : MarkupExtension
    {
        // ReSharper disable once InconsistentNaming
        protected static readonly Dictionary<Type, object> Instances = new();
    }

}
