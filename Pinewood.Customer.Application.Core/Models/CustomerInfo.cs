using Amazon.DynamoDBv2.DataModel;

namespace Pinewood.Customer.Application.Core.Models
{
    [DynamoDBTable("customers")]
    public class CustomerInfoModel
    {
        [DynamoDBHashKey("id")]
        public Guid Id { get; init; }

        [DynamoDBProperty("first_name")]
        public string? FirstName { get; init; }

        [DynamoDBProperty("last_name")]
        public string? LastName { get; init; }

        [DynamoDBProperty("email_id")]
        [DynamoDBGlobalSecondaryIndexHashKey("email-index")]
        public string? Email { get; init; }

        [DynamoDBProperty("phone")]
        public string? Phone { get; init; }

        [DynamoDBProperty("postcode")]
        public string? Postcode { get; init; }

        [DynamoDBProperty("updated_date")]
        public DateTime UpdatedDate { get; init; } = DateTime.Now;

        [DynamoDBProperty("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}

