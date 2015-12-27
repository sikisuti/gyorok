using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Enumerations
{
    public enum ServiceStatusEnum
    {
        WaitingForRepair,
        WaitingForProposal,
        WaitingForApprove,
        WaitingForPart,
        Repaired,
        NotRepairable,
        ProposalRejected
    }
}
