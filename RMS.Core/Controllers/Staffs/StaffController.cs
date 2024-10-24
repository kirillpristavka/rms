using DevExpress.Xpo;
using RMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMS.Core.Controllers.Staffs
{
    public static class StaffController
    {
        private static List<Staff> _staffs;
        public async static System.Threading.Tasks.Task<List<Staff>> GetStaffsAsync(
            Session session,
            bool isForceUpdate = false,
            bool isOnlyWorking = true,
            bool isConfigureAwait = true)
        {
            if (_staffs != null && isForceUpdate is false)
            {
                return _staffs;
            }

            _staffs = await new XPQuery<Staff>(session).ToListAsync().ConfigureAwait(isConfigureAwait);

            if (isOnlyWorking)
            {
                _staffs = _staffs.Where(w => w.DateDismissal is null || w.DateDismissal >= DateTime.Now)?.ToList();
            }

            return _staffs;
        }
    }
}
