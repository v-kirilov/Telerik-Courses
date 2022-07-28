using Dealership.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Models
{
    public abstract class Vehicle : IVehicle, ICommentable
    {
        public const int MakeMinLength = 2;
        public const int MakeMaxLength = 15;
        public const string InvalidMakeError = "Make must be between 2 and 15 characters long!";
        public const int ModelMinLength = 1;
        public const int ModelMaxLength = 15;
        public const string InvalidModelError = "Model must be between 1 and 15 characters long!";
        public const decimal MinPrice = 0.0m;
        public const decimal MaxPrice = 1000000.0m;
        public const string InvalidPriceError = "Price must be between 0.0 and 1000000.0!";

       // private readonly string make;
       // private readonly string model;
        //private readonly VehicleType type;
        //private readonly int wheels;
        //private readonly decimal price;
        public IList<IComment> comments = new List<IComment>();

        protected Vehicle(string make, string model, decimal price, VehicleType type)
        {
            Validator.ValidateIntRange(make.Length, MakeMinLength, MakeMaxLength, InvalidMakeError);
            Validator.ValidateIntRange(model.Length, ModelMinLength, ModelMaxLength, InvalidModelError);
            Validator.ValidateDecimalRange(price, MinPrice, MaxPrice, InvalidPriceError);
            this.Make = make;
            this.Model = model;
            this.Type = type;
            this.Wheels = (int)type;
            this.Price = price;
        }

        public string Make
        {
            get;
        }
        public string Model
        {
            get;
        }
        public VehicleType Type
        {
            get;
        }
        public int Wheels
        {
            get;
        }


        public decimal Price
        {
            get;
        }

        public IList<IComment> Comments
        {
            get
            {
                
                return new List<IComment>(comments);
            }
        }

        public virtual void AddComment(IComment comment)
        {
            this.comments.Add(comment);  //С малки букви защото ние връщаме копие от пропъртито
        }

        public virtual void RemoveComment(IComment comment)
        {
            this.comments.Remove(comment); //С малки букви защото ние връщаме копие от пропъртито
        }
        public abstract string MyType();

        public virtual string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"  Make: {this.Make}");
            sb.AppendLine($"  Model: {this.Model}");
            sb.AppendLine($"  Wheels: {this.Wheels}");
            sb.AppendLine($"  Price: ${this.Price}");

            return sb.ToString();
        }
    }
}
