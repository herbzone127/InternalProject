using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class UserFeedbackResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<UserFeedback> UserFeedback { get; set; }
    }
    public class UserFeedback
    {
        public int FeedbackID { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public int UserID { get; set; }
        public int RideID { get; set; }
        public int RoleId { get; set; }
        public List<FeedbackQuestion> feedbackQuestions { get; set; }
    }
    public class FeedbackQuestion
    {
        public int FeedbackQuestionID { get; set; }
        public string QustionName { get; set; }
        public bool Ans { get; set; }
        public int? FeedbackID { get; set; }
    }
}
