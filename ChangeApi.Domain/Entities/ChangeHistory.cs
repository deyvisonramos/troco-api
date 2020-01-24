using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChangeApi.Domain.Entities
{
    public class ChangeHistory
    {
        public ChangeHistory()
        {
            Itens = new Collection<ChangeHistoryItem>();
        }

        public ChangeHistory(decimal totalChanged)
        {
            TotalChanged = totalChanged;
            Itens = new Collection<ChangeHistoryItem>();
        }

        public int Id { get; set; }
        public decimal TotalChanged { get; set; }
        public virtual ICollection<ChangeHistoryItem> Itens { get; set; }

        public void AddItem(ChangeHistoryItem item)
        {
            if (item != null)
                Itens.Add(item);
        }

        public void AddItems(IEnumerable<ChangeHistoryItem> items)
        {
            if (items != null & items.Any())
            {
                foreach (var item in items)
                {
                    Itens.Add(item);
                }
            }
                
        }
    }
}
