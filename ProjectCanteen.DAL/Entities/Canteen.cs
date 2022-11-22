﻿namespace ProjectCanteen.DAL.Entities
{
    public class Canteen
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public School School { get; set; }
        public User? Terminal { get; set; }
        public List<CanteenWorker> CanteenWorkers { get; set; } = new List<CanteenWorker>();
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
