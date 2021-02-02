using Kutian.Domain.Entities;

namespace Kutian.Tests.Builders.Models
{
    public class CardBuilder
    {
        private readonly Card _card;

        public CardBuilder() => _card = new Card();

        public Card Build() => _card;
    }
}