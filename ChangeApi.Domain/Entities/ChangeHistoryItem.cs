using ChangeApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeApi.Domain.Entities
{
    public class ChangeHistoryItem
    {
        public ChangeHistoryItem() {
        }
        public ChangeHistoryItem(ECurrencyType type, int quantity, int value)
        {
            Type = type;
            Quantity = quantity;
            Value = value;
        }

        public long Id { get; set; }
        public ECurrencyType Type { get; set; }
        public int Quantity { get; set; }
        public int Value { get; set; }
    }
}
