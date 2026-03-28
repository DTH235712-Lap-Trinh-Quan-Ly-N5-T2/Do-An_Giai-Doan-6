namespace TaskFlowManagement.Core.Constants
{
    public static class WorkflowConstants
    {
        public static string GetStatusName(int id) => id switch
        {
            1 => "CREATED",
            2 => "ASSIGNED",
            3 => "IN-PROGRESS",
            4 => "FAILED",
            5 => "REVIEW-1",
            6 => "REVIEW-2",
            7 => "APPROVED",
            8 => "IN-TEST",
            9 => "RESOLVED",
            10 => "CLOSED",
            _ => $"STATUS-{id}"
        };

        public static string GetPriorityName(int id) => id switch
        {
            4 => "Critical",
            3 => "High",
            2 => "Medium",
            1 => "Low",
            _ => "Unknown"
        };
    }
}