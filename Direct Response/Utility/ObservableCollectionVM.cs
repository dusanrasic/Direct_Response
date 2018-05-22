using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.Utility
{
    public class ObservableCollectionVM<M> : ObservableCollection<IViewModel<M>>
    {
        private IList<M> list;
        private bool modelObserving = true;
        private Func<M, IViewModel<M>> viewModelFactory;


        public ObservableCollectionVM(IList<M> list, Func<M, IViewModel<M>> viewModelFactory)
            : base(list.Select(x => viewModelFactory(x)))
        {
            this.list = list;
            this.viewModelFactory = viewModelFactory;
            INotifyCollectionChanged notifyCollectionChanged = list as INotifyCollectionChanged;
            if (notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged += notifyCollectionChanged_CollectionChanged;
        }

        void notifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!modelObserving)
            {
                modelObserving = true;
                return;
            }
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (int i = 0; i < e.NewItems.Count; i++)
                        base.InsertItem(e.NewStartingIndex + i, viewModelFactory((M)e.NewItems[i]));
                    break;
                case NotifyCollectionChangedAction.Move:
                    base.MoveItem(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (int i = 0; i < e.OldItems.Count; i++)
                        base.RemoveItem(e.OldStartingIndex + i);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    base.SetItem(e.NewStartingIndex, viewModelFactory((M)e.NewItems[0]));
                    break;
                case NotifyCollectionChangedAction.Reset:
                    base.Items.Clear();
                    if (e.NewItems != null)
                        for (int i = 0; i < e.NewItems.Count; i++)
                            base.InsertItem(e.NewStartingIndex + i, viewModelFactory((M)e.NewItems[i]));
                    break;
            }
        }

        protected override void InsertItem(int index, IViewModel<M> item)
        {
            modelObserving = false;
            list.Insert(index, item.Model);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            modelObserving = false;
            list.RemoveAt(index);
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            modelObserving = false;
            list.Clear();
            base.ClearItems();
        }

        protected override void SetItem(int index, IViewModel<M> item)
        {
            modelObserving = false;
            list[index] = item.Model;
            base.SetItem(index, item);
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            modelObserving = false;
            ObservableCollection<M> observableCollection = list as ObservableCollection<M>;
            if (observableCollection != null)
                observableCollection.Move(oldIndex, newIndex);
            else
            {
                M temp = list[oldIndex];
                list[oldIndex] = list[newIndex];
                list[newIndex] = temp;
            }
            base.MoveItem(oldIndex, newIndex);
        }
    }
}
