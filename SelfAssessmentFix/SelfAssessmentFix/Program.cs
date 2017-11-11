using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SelfAssessmentFix
{
    class Program
    {
        static void Main(string[] args)
        {
            const string createBy = "data fix";
            DateTime now = DateTime.Now;;

            var csv = new CsvReader(new StreamReader(@"C:\HMI\correct questions and answers for sa activities.csv"));
            var records = csv.GetRecords<QuestionsAndAnswers>().ToList();

            var csv2 = new CsvReader(new StreamReader(@"C:\HMI\mongo issue csv.csv"));
            var records2 = csv2.GetRecords<SubscriberAndActivityId>().ToList();

            var data = from r in records
                join s in records2 on r.ActivityId equals s.ActivityId
                select
                    new QuestionsAndAnswersCompleted()
                    {
                        ActivityId = s.ActivityId,
                        CmeAnswerId = r.CmeAnswerId,
                        CmeQuestionId = r.CmeQuestionId,
                        SubscriberId = s.SubscriberId
                    };
            var groupedData = data.GroupBy(s => new Tuple<int, int>(s.SubscriberId, s.ActivityId)).ToList();
            var completed = groupedData.Select(s =>
            {
                int subscriberId = s.Key.Item1;
                int activityId = s.Key.Item2;
                var allQuestionsAnsweredForUserActivity = s.GroupBy(q => q.CmeQuestionId); 

                var pollAttempts = allQuestionsAnsweredForUserActivity.Select(p => new PollAttemptsDo()
                {
                    CmeQuestionId = p.Key,
                    IsMaxAttemptsPassed = false,
                    IsPassed = true,
                    UserAttemptResponses = new List<PollResponseDo> {new PollResponseDo()
                    {
                        AnswerText = "", 
                        AttemptNumber = 1, 
                        CmeAnswerIds = p.Select(a => a.CmeAnswerId).ToList(),
                        CmeQuestionId = p.Key,
                        IsPassed = true,
                        CreatedBy = createBy,
                        CreatedDate = now
                    } },
                    CreatedBy = createBy,
                    CreatedDate = now,
                    UpdatedBy = createBy,
                    UpdatedDate = now
                }).ToList();
                return new MyCmeSubscriberSelfAssessmentDo()
                {
                    _id = subscriberId + "-" + activityId,
                    ActivityId = activityId,
                    SubscriberId = subscriberId,
                    CreatedBy = createBy,
                    CreatedDate = now,
                    IsFinalFailed = false,
                    IsPassed = true,
                    PollAttempts = pollAttempts,
                    UpdatedBy = createBy,
                    UpdatedDate = now
                };
            }).ToList();

            string f = JsonConvert.SerializeObject(completed, Formatting.Indented);
            File.WriteAllText(@"C:\HMI\selfassessment.json", f);
        }
        
    }

    public class SubscriberAndActivityId
    {
        public int ActivityId { get; set; }
        public int SubscriberId { get; set; }

    }

    public class QuestionsAndAnswers
    {
        public int ActivityId { get; set; }
        public int CmeQuestionId { get; set; }
        public int CmeAnswerId { get; set; }
    }

    public class QuestionsAndAnswersCompleted
    {
        public int ActivityId { get; set; }
        public int CmeQuestionId { get; set; }
        public int CmeAnswerId { get; set; }
        public int SubscriberId { get; set; }
    }

    public class MyCmeSubscriberSelfAssessmentDo
    {
        // this is the concantanante string of SubscriberId-ActivityId
        public string _id { get; set; }

        public virtual int SubscriberId { get; set; }
        public virtual int ActivityId { get; set; }
        public virtual bool IsPassed { get; set; }
        public virtual bool IsFinalFailed { get; set; }
        public virtual List<PollAttemptsDo> PollAttempts { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }

        public virtual string UpdatedBy { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
    }

    public class PollAttemptsDo
    {
        public virtual int CmeQuestionId { get; set; }
        public virtual bool IsMaxAttemptsPassed { get; set; }
        public virtual bool IsPassed { get; set; }
        public virtual List<PollResponseDo> UserAttemptResponses { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }

        public virtual string UpdatedBy { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
    }

    public class PollResponseDo
    {
        public int AttemptNumber { get; set; }
        public virtual int CmeQuestionId { get; set; }
        public virtual List<int> CmeAnswerIds { get; set; }
        public virtual string AnswerText { get; set; }
        public bool IsPassed { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}
