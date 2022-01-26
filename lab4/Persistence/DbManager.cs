using AutoMapper;
using lab4.Models;
using lab4.Persistence.Schemas;
using lab4.Utility;
using Microsoft.EntityFrameworkCore;

namespace lab4.Persistence
{
    public class DbManager
    {
        public DbManager(IMapper mapper)
        {
            Mapper = mapper;
        }

        public List<UserAll> AllUsers()
        {
            var users = DbCtx.Users.ToList();
            return Mapper.Map<List<UserAll>>(users);
        }

        public bool IsUser(UserAll user)
        {
            return DbCtx.Users.Select(u => u.UserName == user.UserName).Any();
        }

        public User GetUser(string userName)
        {
            return DbCtx.Users.Find(userName);
        }

        public void AddUser(UserAll addUser)
        {
            var user = Mapper.Map<User>(addUser);
            DbCtx.Users.Add(user);
            DbCtx.SaveChanges();
        }

        public ActivitiesReport ActivitiesReport(string userName, DateTime date)
        {
            date = date.Date;
            var report = DbCtx.Reports.Include(r => r.Activities.Where(a => a.Date == date)).ThenInclude(a => a.Project).FirstOrDefault(p => p.ReportMonth == date.Dayless() && p.UserName == userName);
            if (report == null)
                return new ActivitiesReport { Activities = new(), Frozen = false };
            var activities = Mapper.Map<List<ActivityAll>>(report.Activities);
            return new ActivitiesReport { Activities = activities, Frozen = report.Frozen };
        }

        public void AddActivity(string userName, ActivityAdd add_activity)
        {
            var activity = Mapper.Map<Activity>(add_activity);
            var reportMonth = activity.Date.Dayless();
            var report = DbCtx.Reports.Find(reportMonth, userName);
            if (report == null)
            {
                report = new Report { ReportMonth = reportMonth, UserName = userName };
                report = DbCtx.Reports.Add(report).Entity;
                DbCtx.SaveChanges();
            }

            if (report.Frozen == false)
            {
                activity.ActivityPid = null;
                activity.UserName = userName;
                activity.ReportMonth = reportMonth;
                if (string.IsNullOrEmpty(activity.SubprojectCode))
                    activity.SubprojectCode = null;
                DbCtx.Activities.Add(activity);
                DbCtx.SaveChanges();
            }
        }

        public void EditActivity(string userName, ActivityAll edit_activity)
        {
            var activity = Mapper.Map<Activity>(edit_activity);

            activity.UserName = userName;
            activity.ReportMonth = edit_activity.Date.Dayless();
            if (string.IsNullOrEmpty(activity.SubprojectCode))
                activity.SubprojectCode = null;

            DbCtx.Update(activity);
            DbCtx.SaveChanges();
        }

        public void DeleteActivity(string userName, ActivityAll delete_activity)
        {
            var activity = Mapper.Map<Activity>(delete_activity);
            activity.UserName = userName;
            activity.ReportMonth = activity.Date.Dayless();
            DbCtx.Activities.Remove(activity);
            DbCtx.SaveChanges();
        }

        public List<ProjectAll> Projects()
        {
            return DbCtx.Projects.Where(p => p.Active == true).Select(p => new ProjectAll { ProjectCode = p.ProjectCode, ProjectName = p.ProjectName }).ToList();
        }

        public List<string> SubprojectCodes(string projectCode)
        {
            var project = DbCtx.Projects.Include(p => p.Subprojects).FirstOrDefault(p => p.ProjectCode == projectCode);
            return project.Subprojects.AsEnumerable().Select(s => s.SubprojectCode).ToList();
        }

        public AcceptedActivitiesReport AcceptedActivitiesReport(string userName)
        {
            var acceptedActivities = DbCtx.AcceptedActivities
                .Include(aa => aa.Report)
                .ThenInclude(r => r.Activities)
                .Include(aa => aa.Project)
                .Where(aa => aa.UserName == userName)
                .Select(aa => new AcceptedActivityAll
                {
                    ProjectCode = aa.ProjectCode,
                    ReportMonth = aa.ReportMonth,
                    ProjectName = aa.Project.ProjectName,
                    SubmitedTime = aa.Report.Activities.Where(a => a.ProjectCode == aa.ProjectCode).Sum(a => a.Time),
                    AcceptedTime = aa.Time,
                }).ToList();

            return new AcceptedActivitiesReport
            {
                AcceptedActivities = acceptedActivities
            };
        }

        private DbCtx DbCtx = new DbCtx();
        private IMapper Mapper;
    }
}