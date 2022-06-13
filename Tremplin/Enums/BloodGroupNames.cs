using System.ComponentModel.DataAnnotations;

namespace Tremplin.Enums
{
    /// <summary>
    /// Enumeration of blood groups
    /// </summary>
    public enum BloodGroupNames
    {
        [Display(Name = "O+")]
        OPositive,

        [Display(Name = "A+")]
        APositive,

        [Display(Name = "B+")]
        BPositive,

        [Display(Name = "AB+")]
        ABPositive,

        [Display(Name = "O-")]
        ONegative,

        [Display(Name = "A-")]
        ANegative,

        [Display(Name = "B-")]
        BNegative,

        [Display(Name = "AB-")]
        ABNegative
    }
}