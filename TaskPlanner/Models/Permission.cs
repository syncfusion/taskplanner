using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskPlanner.Models
{
    public static class Permission 
    {
        public static bool IsUserOwnerOfProject(string emailId, int projectId)  
        {
            return true; 
        } 

        public static bool IsUserHasAccessToProject(string emailId, int projectId)  
        {
            return true;
        }

    }
}
