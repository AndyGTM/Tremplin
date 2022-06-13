using System.ComponentModel.DataAnnotations;

namespace Tremplin.Enums
{
    /// <summary>
    /// Enumeration of sex types
    /// </summary>
    public enum SexTypes
    {
        [Display(Name = "Masculin")]
        Male,

        [Display(Name = "Féminin")]
        Female,

        [Display(Name = "Neutre")]
        Neutral
    }
}