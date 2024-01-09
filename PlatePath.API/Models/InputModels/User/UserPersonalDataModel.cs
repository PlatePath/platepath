using PlatePath.API.Data.Models.ActivityLevels;
using PlatePath.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlatePath.API.Models.InputModels.User
{
    public record UserPersonalDataModel
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public double HeightCm { get; set; }

        [Required]
        public double WeightKg { get; set; }

        [Required]
        [EnumDataType(typeof(GenderEnum))]
        public string Gender { get; set; }

        [Required]
        [EnumDataType(typeof(ActivityLevelEnum))]
        public string ActivityLevel { get; set; }

        [Required]
        [EnumDataType(typeof(WeightGoalEnum))]
        public string WeightGoal { get; set; }

    }
}
