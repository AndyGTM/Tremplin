using System.ComponentModel.DataAnnotations;

namespace Tremplin.Core.Enums
{
    public enum SexTypes
    {
        [Display(Name = "Masculin")]
        Male,

        [Display(Name = "Féminin")]
        Female
    }
}