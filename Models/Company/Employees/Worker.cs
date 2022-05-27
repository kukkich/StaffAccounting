using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.VieweProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Workers")]
    [Notation("Рабочий")]
    public class Worker : Employee
    {
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public int RankId { get; set; }
        public Rank Rank { get; set; }

        public Worker() { }

        public Worker(EmployeeCreationModel model) 
            : base(model)
        {
            ManagerId = model.ManagerId;
            RankId = model.RankId;
        }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.Worker(this, action);
        }
    }
}
