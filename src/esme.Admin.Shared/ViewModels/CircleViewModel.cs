using System;

namespace esme.Admin.Shared.ViewModels
{
    public class CircleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfMessages { get; set; }
    }
}
