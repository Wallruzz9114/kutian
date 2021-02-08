using Kutian.Domain.Entities;

namespace Kutian.Tests.Builders.Models
{
    public class CardBuilder
    {
        private readonly Card _card;

        public CardBuilder(string name) => _card = new Card(name);

        public Card Build() => _card;
    }
}