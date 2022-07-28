namespace Dealership.Models.Contracts
{
    public interface IVehicle : IPriceable,ICommentable
    {
        string Make { get; }

        string Model { get; }

        VehicleType Type { get; }

        int Wheels { get; }

        public string MyType();
    }
}
