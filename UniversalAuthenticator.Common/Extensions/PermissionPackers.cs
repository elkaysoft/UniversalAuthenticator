using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Extensions
{
    public class PermissionPackers
    {
        public const char PackType = 'H';
        public const int PackedSize = 4;

        public static string PackPermissionsIntoHexString(List<string> permissions)
        {
            var permJoin = string.Join(",", permissions);
            byte[] bytes = Encoding.Default.GetBytes(permJoin);

            var hexString = Convert.ToHexString(bytes);
            return $"H4-{hexString}";
        }

        public static IEnumerable<string> UnPackPermissionValuesFromString(string packedPermissions)
        {
            var packPrefix = "H4"; 

            if (packedPermissions == null)
                throw new ArgumentNullException(nameof(packedPermissions));

            if (!packedPermissions.StartsWith(packPrefix))
            {
                throw new InvalidOperationException($"The format of the packed permissions is wrong - should start with {packPrefix}");
            }

            var splittedStr = packedPermissions.Split("-");
            var permObj = splittedStr[1];

            var hexArray = Convert.FromHexString(permObj);
            var stringValue = Encoding.Default.GetString(hexArray);

            return ExtensionsService.SplitCsv(stringValue);
        }
    }
}
