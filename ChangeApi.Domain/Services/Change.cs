using System.Collections.Generic;
using System.Linq;

namespace ChangeApi.Domain.Services
{
    public class Change : ServiceBase
    {
        public Change() : base()
        {
            AvailableCurrencyNotes = new List<int> { 50, 100, 20, 10 }.OrderByDescending(x => x).ToList();
            AvailableCurrencyCoins = new List<int> { 10, 50, 5, 1 }.OrderByDescending(x => x).ToList();
            Notes = new Dictionary<int, int>();
            Coins = new Dictionary<int, int>();
        }

        private List<int> AvailableCurrencyNotes { get; }
        private List<int> AvailableCurrencyCoins { get; }
        /// <summary>
        /// Key = note |=> Value = quantity
        /// </summary>
        public Dictionary<int, int> Notes { get; }

        /// <summary>
        /// Key = note |=> Value = quantity
        /// </summary>
        public Dictionary<int, int> Coins { get; }

        public string Calc(decimal amountPaid, decimal totalAmount)
        {
            var amountToChange = totalAmount - amountPaid;
            
            if (amountToChange >= 0)
                AddError("amounts", "Nothing to change. There is lack of money in this payment or it doesn't need a change.");


            amountToChange *= -1;

            if (amountToChange > 9999.99M)
                AddError("amount", "This amount is too big to change");

            if (!IsValid)
                return string.Empty;

            Notes.Clear();
            Coins.Clear();

            var intAmount = (int) amountToChange;
            var decimalAmount = (int)((amountToChange - intAmount) * 100);
            var notesAndCoinsToGiveBack = new List<string>();

            foreach (var availableCurrencyNote in AvailableCurrencyNotes)
            {
                if (intAmount - availableCurrencyNote < 0)
                    continue;

                int control = intAmount / availableCurrencyNote;
                if (control != 0)
                {
                    notesAndCoinsToGiveBack.Add($"{control} de R${availableCurrencyNote}");
                    Notes.Add(availableCurrencyNote, control);
                    intAmount %= availableCurrencyNote;
                }
            }

            var remainingAmount = decimalAmount + intAmount * 100;
            if(remainingAmount > 0)
            {
                foreach (var availableCurrencyCoin in AvailableCurrencyCoins)
                {
                    if (remainingAmount - availableCurrencyCoin < 0)
                        continue;

                    int control = remainingAmount / availableCurrencyCoin;
                    if (control != 0)
                    {
                        notesAndCoinsToGiveBack.Add($"{control} de {availableCurrencyCoin} centavos");
                        Coins.Add(availableCurrencyCoin, control);
                        remainingAmount %= availableCurrencyCoin;
                    }
                }
            }

            return string.Join(";\n", notesAndCoinsToGiveBack);
        }

        public decimal TotalChanged()
        {
            return Notes.Sum(x => x.Value * x.Key) + (Coins.Sum(x => ((decimal)x.Value / 100) * x.Key));
        }
    }
}