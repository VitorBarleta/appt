using System;
using appt.Models.SeedWork.ValueObjects;

namespace appt.Models
{
    public class Token
    {
        public Token(
            string costCenter,
            DateTime date,
            string description)
        {
            CostCenter = costCenter;
            Date = date;
            Description = description;
        }

        private Token(
            string costCenter,
            string description)
        {
            CostCenter = costCenter;
            Description = description;

            Date = DateTime.Now;
        }

        public static Token Create(
            string costCenter,
            string description)
        {
            return new Token(costCenter, description);
        }

        public string CostCenter { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
    }
}