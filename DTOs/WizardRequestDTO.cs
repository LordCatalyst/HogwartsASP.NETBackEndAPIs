namespace HogwartsBackEndAPIs.DTOs
{
    public class WizardRequestDTO
    {
        public int WizardId { get; set; }

        public string? WizardName { get; set; }

        public string? WizardLastName { get; set; }

        public int? WizardMuggleId { get; set; }

        public int? WizardAge { get; set; }

        public int? HouseId { get; set; }
        public string? HouseName { get; set; }
    }
}
