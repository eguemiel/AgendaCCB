using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCCB.App.Helpers
{
    public class Group<TKey, TItem> : ObservableCollection<TItem>
    {
        public TKey Key { get; private set; }

        public Group(TKey key, IEnumerable<TItem> items)
        {
            this.Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
