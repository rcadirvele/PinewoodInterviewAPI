namespace Pinewood.Customer.Application.Core.DTOs
{
    public class CustomerInfoDto
    {
        public Guid Id { get; init; }

        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        public string? Email { get; init; }

        public string? Phone { get; init; }

        public string? Postcode { get; init; }

    }

}