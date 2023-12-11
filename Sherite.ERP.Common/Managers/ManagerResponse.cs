namespace Sherite.ERP.Common.Managers
{
    using System;
    using System.Collections.Generic;
    using static Sherite.ERP.Common.Enums;

    public class ManagerResponse<T2>
    {
        public Guid OperationGUID { get; set; }
        public IEnumerable<ManagerObject<T2>> ObjectsList { get; set; }
    }

    public class ManagerObject<T2>
    {
        public Guid InternalID { get; set; }
        public Modules ModuleID { get; set; }
        public Operations OperationID { get; set; }
        public string Table { get; set; }
        public string Field { get; set; }
        public T2 Object { get; set; }
        public StatusObject Status { get; set; }
        public IEnumerable<Error> ErrorsList { get; set; }
        public string AdditionalInfo { get; set; }
    }
}