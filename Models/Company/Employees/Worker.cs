using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Company.Attributes;
using StaffAccounting.Models.VieweProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Workers")]
    [Notation("Рабочий")]
    public class Worker : Employee
    {
        public int? ManagerId { get; set; }
        public Manager Manager { get; set; }

        public int? RankId { get; set; }
        public Rank Rank { get; set; }

        public Worker() { }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.Worker(this, action);
        }

        public override void JoinFromDatabase(CompanyContext context)
        {
            Manager = context.Managers.FirstOrDefault(manager => manager.Id == ManagerId);
            Rank = context.Ranks.FirstOrDefault(rank => rank.Id == RankId);
        }
    }
}
