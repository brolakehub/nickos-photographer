using System.ComponentModel;

namespace DTO
{
    public enum FileType
    {
        [Description("unknowns")]
        Unknown,

        [Description("images")]
        Image,

        [Description("videos")]
        Video
    }

    public enum InfoName
    {
        [Description("userInfo.name")]
        UserName,

        [Description("userInfo.adress")]
        UserAdress,

        [Description("userInfo.phoneNrs")]
        UserPhoneNrs,

        [Description("userInfo.emails")]
        UserEmails,

        [Description("userInfo.name")]
        UserSocialMediaLinks,

        [Description("homeDescription.name")]
        HomeDescriptionName,

        [Description("homeDescription.title")]
        HomeDescriptionTitle,

        [Description("homeDescription.text")]
        HomeDescriptionText,

        [Description("homeDescription.backGroundFileId")]
        HomeDescriptionBackgroundFileId,

        [Description("otherInfo.logo")]
        Logo,
    }
}
