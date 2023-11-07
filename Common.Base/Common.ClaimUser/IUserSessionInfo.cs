using System.Collections.Generic;
using System.Threading.Tasks;

namespace NAFCommon.Base.Common.ClaimUser
{
    public interface IUserSessionInfo
    {
        /// <summary>
        /// Gets current UserId or null.
        /// It can be null if no user logged in.
        /// </summary>
        int? ID { get; }

        string Name { get; }
        string UserName { get; }
        string LastName { get; }
        string Email { get; }
        int? Project { get; }
        string PermissionGroups { get; }

        /// <summary>
        /// Get permission belonging to user's group
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPermissionOfGroupAsync();
    }
}