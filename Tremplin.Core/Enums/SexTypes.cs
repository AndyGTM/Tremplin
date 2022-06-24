using System.ComponentModel.DataAnnotations;

namespace Tremplin.Core.Enums
{
    /// <summary>
    /// Enumeration of sex types
    /// </summary>
    public enum SexTypes
    {
        [Display(Name = "Masculin")]
        Male,

        [Display(Name = "Féminin")]
        Female
    }
}