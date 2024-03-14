using Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Customer : AuditableEntity
    {
        [NotMapped]
        public const string TYPE_LOYAL = "L";
        [NotMapped]
        public const string TYPE_VISITOR = "V";

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Type { get; set; }

        public static string GetTypeName(string type)
        {
            switch (type)
            {
                case TYPE_LOYAL:
                    return "Khách hàng thân thiết";
                case TYPE_VISITOR:
                    return "Khách hàng vãng lai";
                default:
                    return "Không xác định";
            }
        }
    }
}
