using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace GalaxyAPI.Services.Authentication
{
    public static class IdentityExtensions
    {
        public static string GetUserBirthDate(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var bithDate = ci.FindFirst(ClaimTypes.DateOfBirth);
                if (bithDate != null)
                {
                    return bithDate.Value;
                }
            }
            return default(string);
        }

        public static T GetUserRoleId<T>(this IIdentity identity) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var roleId = ci.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
                if (roleId != null)
                {
                    return (T)Convert.ChangeType(roleId.Value, typeof(T), CultureInfo.InvariantCulture);
                }

            }
            return default(T);
        }

        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var id = ci.FindFirst(ClaimTypes.NameIdentifier);
                if (id != null)
                {
                    return (T)Convert.ChangeType(id.Value, typeof(T), CultureInfo.InvariantCulture);
                }
            }
            return default(T);
        }

        public static T GetUserAmount<T>(this IIdentity identity) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var amount = ci.FindFirst(ClaimTypesMy.Amount.ToString());
                if (amount != null)
                {
                    return (T)Convert.ChangeType(amount.Value, typeof(T), CultureInfo.InvariantCulture);
                }
            }
            return default(T);
        }
    }
}
