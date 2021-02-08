using System;

namespace Kutian.Domain.Events
{
    public class CardCreated
    {
        public CardCreated(string name, Guid cardId)
        {
            CardId = cardId;
            Name = name;
        }

        public Guid CardId { get; set; }
        public string Name { get; }
    }
}