using System.ComponentModel;

namespace Moj.CMS.Shared.Enums
{
    public enum RequestType
    {
        Query = 1,
        Command
    }
    public enum UploadType
    {
        [Description(@"Cases\Basic")]
        Case,

        [Description(@"Cases\Promissories")]
        Promissories,

        [Description(@"Cases\Claims")]
        Claims,

        [Description(@"Profile")]
        Profile
    }
}
