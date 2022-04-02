namespace CarSharing_Database_GraphQL.Mutations.Records.AccountRecords
{
    public record AccountInput(
        string Username,
        string Password, 
        CustomerKey Customer
    );
}