using Microsoft.EntityFrameworkCore;

namespace StaffAccounting.Models.Company
{
    public class CompanyContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Director> Directors { get; set; } = null!;
        public DbSet<DepartmentHead> DepartmentHeads { get; set; } = null!;
        public DbSet<Accountant> Accountants { get; set; } = null!;
        public DbSet<Manager> Managers { get; set; } = null!;
        public DbSet<Worker> Workers { get; set; } = null!;

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Rank> Ranks { get; set; } = null!;

        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            //FillDataBase();
        }

        private void FillDataBase()
        {
            Department devDepartment = new () { Name = "Разработка" };
            Project projectBackend = new () { Name = "Backend" };
            Project projectFrontend = new () { Name = "Frontend" };
            Project projectDesign = new () { Name = "Design" };
            Rank teamLeadCSharp = new () { Name = "Team Lead C#" };
            Rank middleCSharp = new() { Name = "Middle C#" };
            Rank seniorJS = new() { Name = "Senior JS" };
            Rank middleJS = new() { Name = "Middle JS" };
            Rank juniorJS = new() { Name = "Junior JS" };
            Rank mainDesigner = new() { Name = "Main Designer" };
            Rank traineeDesigner = new() { Name = "Trainee Designer" };

            Departments.Add(devDepartment);
            Projects.AddRange(projectBackend, projectFrontend, projectDesign);
            Ranks.AddRange(
                teamLeadCSharp, middleCSharp,
                seniorJS, middleJS, juniorJS,
                mainDesigner, traineeDesigner
            );

            Director mainDirector = new()
            {
                FirstName = "Иван",
                MiddleName = "Иванович",
                LastName = "Петорв",
                Birthday = DateTime.Now,
                Sex = Sex.Male

            };
            Accountant accountantAnatoly = new()
            {
                FirstName = "Анатолий",
                MiddleName = "Евгеньевич",
                LastName = "Флоров",
                Birthday = DateTime.Now,
                Sex = Sex.Male,
                Director = mainDirector,
            };
            Accountant accountantAnastasia = new()
            {
                FirstName = "Анастасия",
                MiddleName = "Викторовна",
                LastName = "Смирнова",
                Birthday = DateTime.Now,
                Sex = Sex.Female,
                Director = mainDirector,
            };

            DepartmentHead departmentHeadNatalia = new()
            {
                FirstName = "Наташа",
                MiddleName = "Васильневна",
                LastName = "Неделько",
                Birthday = DateTime.Now,
                Sex = Sex.Female,
                Director = mainDirector,
                Department = devDepartment
            };

            Manager managerSergey = new()
            {
                FirstName = "Сергей",
                MiddleName = "Петрович",
                LastName = "Ким",
                Birthday = DateTime.Now,
                Sex = Sex.Male,
                DepartmentHead = departmentHeadNatalia,
                Project = projectBackend
            };
            Worker teamLeadMark = new()
            {
                FirstName = "Марк",
                MiddleName = "Антонович",
                LastName = "Дементьев",
                Birthday = DateTime.Now,
                Sex = Sex.Male,
                Manager = managerSergey,
                Rank = teamLeadCSharp
            };
            Worker middleIgnat = new()
            {
                FirstName = "Игнат",
                MiddleName = "Павлович",
                LastName = "Юдин",
                Birthday = DateTime.Now,
                Sex = Sex.Male,
                Manager = managerSergey,
                Rank = middleCSharp
            };
            Worker middleAlbert = new()
            {
                FirstName = "Альберт",
                MiddleName = "Валерьевич",
                LastName = "Фомичёв",
                Birthday = DateTime.Now,
                Sex = Sex.Male,
                Manager = managerSergey,
                Rank = middleCSharp
            };

            Manager managerAlyona = new()
            {
                FirstName = "Алёна",
                MiddleName = "Александровна",
                LastName = "Курочкина",
                Birthday = DateTime.Now,
                Sex = Sex.Female,
                DepartmentHead = departmentHeadNatalia,
                Project = projectFrontend
            };
            Worker seniorMarina = new()
            {
                FirstName = "Марина",
                MiddleName = "Якуновна",
                LastName = "Смирнова",
                Birthday = DateTime.Now,
                Sex = Sex.Female,
                Manager = managerAlyona,
                Rank = seniorJS
            };
            Worker middleNicolay = new()
            {
                FirstName = "Николай",
                MiddleName = "Григорьевич",
                LastName = "Ильин",
                Birthday = DateTime.Now,
                Sex = Sex.Male,
                Manager = managerAlyona,
                Rank = middleJS
            };
            Worker middleSophy = new()
            {
                FirstName = "София",
                MiddleName = "Васильевна",
                LastName = "Громова",
                Birthday = DateTime.Now,
                Sex = Sex.Female,
                Manager = managerAlyona,
                Rank = juniorJS
            };

            Manager managerVeronika = new()
            {
                FirstName = "Вероника",
                MiddleName = "Дмитриевна",
                LastName = "Игнатова",
                Birthday = DateTime.Now,
                Sex = Sex.Female,
                DepartmentHead = departmentHeadNatalia,
                Project = projectDesign
            };
            Worker mainDesignerChristina = new()
            {
                FirstName = "Аркадьевна",
                MiddleName = "Григорьевич",
                LastName = "Григорьева",
                Birthday = DateTime.Now,
                Sex = Sex.Female,
                Manager = managerVeronika,
                Rank = mainDesigner
            };
            Worker traineeDesignerEvgeny = new()
            {
                FirstName = "Натанович",
                MiddleName = "Васильевна",
                LastName = "Крылов",
                Birthday = DateTime.Now,
                Sex = Sex.Male,
                Manager = managerAlyona,
                Rank = traineeDesigner
            };

            Directors.Add(mainDirector);
            Accountants.AddRange(accountantAnastasia, accountantAnatoly);
            DepartmentHeads.Add(departmentHeadNatalia);
            Managers.AddRange(managerSergey, managerAlyona, managerVeronika);
            Workers.AddRange(
                    teamLeadMark, middleIgnat, middleAlbert,
                    seniorMarina, middleNicolay, middleSophy,
                    mainDesignerChristina, traineeDesignerEvgeny
            );

            SaveChanges();
        }
    }
}
