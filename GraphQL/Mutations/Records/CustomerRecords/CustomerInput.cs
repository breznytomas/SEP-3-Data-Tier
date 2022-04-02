namespace CarSharing_Database_GraphQL.Mutations.Records.CustomerRecords
{
    public record CustomerInput(
        string Cpr,
        string FirstName, 
        string LastName,
        string PhoneNo
    );
}