using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Filtration;
using StaffAccounting.Models.Notation;
using StaffAccounting.Models.ViewProviders;
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

        public override bool CanBeRaised => true;

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

        public override bool IsMatch(RelationFilterOption option)
        {
            return (option.ManagerId is not null && option.ManagerId == ManagerId)
                || (option.RankId is not null && option.RankId == RankId);
        }

        public override Employee GetRisedEmployee()
        {
            Employee raised = new Manager();
            FillRaised(raised);
            return raised;
        }

        protected override void UnlinkRelatedEntities() { }
    }
}
